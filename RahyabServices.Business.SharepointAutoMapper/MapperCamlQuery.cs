using RahyabServices.Business.SharepointAutoMapper.InterFaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml.Linq;

namespace RahyabServices.Business.SharepointAutoMapper
{
    public static class MapperCamlQuery
    {
        private static XElement ParseNodeType(ExpressionType type)
        {
            XElement node;
            switch (type)
            {
                case ExpressionType.AndAlso:
                case ExpressionType.And:
                    node = new XElement("And");
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    node = new XElement("Or");
                    break;
                case ExpressionType.Equal:
                    node = new XElement("Eq");
                    break;
                case ExpressionType.GreaterThan:
                    node = new XElement("Gt");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    node = new XElement("Geq");
                    break;
                case ExpressionType.LessThan:
                    node = new XElement("Lt");
                    break;
                case ExpressionType.LessThanOrEqual:
                    node = new XElement("Leq");
                    break;
                default:
                    throw new Exception(string.Format("Unhandled expression type: '{0}'", type));
            }
            return node;
        }
        private static XElement VisitMemberAccess(MemberExpression member)
        {
            var expr = member.Expression;
            if (expr.NodeType != ExpressionType.Constant) return new XElement("FieldRef", new XAttribute("Name",GetFieldName(member.Member)));
            var lambda = Expression.Lambda(member);
            var fn = lambda.Compile();
            return VisitConstant(Expression.Constant(fn.DynamicInvoke(null), member.Type));
        }
        private static string GetFieldName(MemberInfo member){
            var attributes = member.GetCustomAttributes();
            var enumerable = attributes as Attribute[] ?? attributes.ToArray();
            if (!enumerable.Any()) return member.Name;
            var sharepointAttribute = enumerable.OfType<SharepointFieldName>().FirstOrDefault();
            if (sharepointAttribute != null) return sharepointAttribute.GetName();
            return member.Name;
        }
        private static XElement VisitConstant(ConstantExpression constant)
        {
            return new XElement("Value", ParseValueType(constant.Type), constant.Value);
        }
        private static XAttribute ParseValueType(Type type)
        {
            string name;
            switch (type.Name)
            {
                case "DateTime":
                    name = "DateTime";
                    break;
                case "String":
                    name = "Text";
                    break;
                case "Guid":
                    name = "Text";
                    break;
                case "Int32":
                    name = "Number";
                    break;
                case "Double":
                    name = "Number";
                    break;
                default:
                    throw new Exception(string.Format("Unhandled value type parser for: '{0}'", type.Name));
            }
            return new XAttribute("Type", name);
        }
        private static XElement VisitMethodCall(MethodCallExpression methodcall)
        {
            XElement node;
            var left = Visit(methodcall.Object);
            var right = Visit(methodcall.Arguments[0]);
            switch (methodcall.Method.Name)
            {
                case "Contains":
                    node = new XElement("Contains");
                    break;
                case "StartsWith":
                    node = new XElement("BeginsWith");
                    break;
                default:
                    throw new Exception(string.Format("Unhandled method call: '{0}'", methodcall.Method.Name));
            }
            if (left != null && right != null) { node.Add(left, right); }
            return node;
        }
        private static XElement VisitBinary(BinaryExpression binary)
        {
            var node = ParseNodeType(binary.NodeType);
            var left = Visit(binary.Left);
            var right = Visit(binary.Right);
            if (left != null && right != null) { node.Add(left, right); }
            return node;
        }
        private static XElement Visit(Expression expression)
        {
            if (expression == null) { return null; }
            switch (expression.NodeType)
            {
                case ExpressionType.Call:
                    return VisitMethodCall(expression as MethodCallExpression);
                case ExpressionType.MemberAccess:
                    return VisitMemberAccess(expression as MemberExpression);
                case ExpressionType.Constant:
                    return VisitConstant(expression as ConstantExpression);
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                case ExpressionType.LessThan:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                    return VisitBinary(expression as BinaryExpression);
                default:
                    return null;
            }
        }
        public static string BuildCamlQuery<T>(this IEntitySharepointMapper value, Expression<Func<T, bool>> expression)
            where T : IEntitySharepointMapper
        {
            return Translate(expression.Body);
        }
        public static string Translate(Expression expression)
        {
            var view = new XElement("View");
            var query=new XElement("Query");
            var where = new XElement("Where");
            view.Add(query);
            query.Add(where);
            where.Add(Visit(expression));
            return view.ToString(SaveOptions.DisableFormatting);
        }
    }
}
