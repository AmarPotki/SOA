using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RahyabServices.Business.SharepointAutoMapper.Models;
using Microsoft.SharePoint.Client;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;
namespace RahyabServices.Business.SharepointAutoMapper
{
    public static class SharepointMapperExtensions
    {
        public static string GetSharepointListName(this IEntitySharepointMapper value)
        {
            var attributes = value.GetType().GetCustomAttributes();
            var enumerable = attributes as Attribute[] ?? attributes.ToArray();
            if (!enumerable.Any()) return value.GetType().Name;
            foreach (SharepointListName sharepointAttribute in enumerable.OfType<SharepointListName>())
            {
                return sharepointAttribute.GetName();
            }
            return value.GetType().Name;
        }
        public static Guid GetSharepointListId(this IEntitySharepointMapper value)
        {
            var attributes = value.GetType().GetCustomAttributes();
            var enumerable = attributes as Attribute[] ?? attributes.ToArray();
            if (!enumerable.Any()) throw new Exception("Set Guid pls");
            foreach (SharepointListId sharepointAttribute in enumerable.OfType<SharepointListId>())
            {
                return new Guid(sharepointAttribute.GetName());
            }
            throw new Exception("Set Guid pls");
        }
        public static int GetIdFromEntity(this IEntitySharepointMapper value)
        {
            var propertyInfo = value.GetType().GetProperty("Id");
            var retorno = propertyInfo.GetValue(value).ToString();
            return Convert.ToInt16(retorno);
        }
        public static void ProjectListItemFromEntity<T>(this ListItem value, T entity) where T : class, IEntitySharepointMapper
        {
            var propertyInfos = typeof(T).GetProperties();
            value = BuildListItem(propertyInfos, value, entity);
        }
        public static ListItem ListItemFromEntity<T>(this  T entity, ListItem value) where T : class, IEntitySharepointMapper
        {
            var propertyInfos = typeof(T).GetProperties();
            return BuildListItem(propertyInfos, value, entity);
        }
        public static T ProjectToEntity<T>(this ListItemCollection value) where T : new()
        {
            var entidade = new T();
            var propertyInfos = typeof(T).GetProperties();
            if (value.Count > 0) entidade = BuildEntity<T>(propertyInfos, value[0]);
            return entidade;
        }
        public static IEnumerable<T> ProjectToCollectionEntity<T>(this ListItemCollection value) where T : new()
        {
            var entidade = new T();
            var propertyInfos = typeof(T).GetProperties();
            var collection = new List<T>();
            if (value.Count > 0)
            {
                foreach (ListItem item in value)
                {
                    collection.Add(BuildEntity<T>(propertyInfos, item));
                }
            }
            return collection;
        }
        private static IEnumerable<MapperDictionaryProperty> GetProperties(IEnumerable<PropertyInfo> propertyInfos)
        {
            var properties = new List<MapperDictionaryProperty>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                try
                {
                    var property = new MapperDictionaryProperty();
                    var attributes = propertyInfo.GetCustomAttributes();
                    var enumerable = attributes as Attribute[] ?? attributes.ToArray();
                    if (enumerable.Any())
                    {
                        foreach (Attribute attribute in enumerable)
                        {
                            if (attribute is SharepointFieldName)
                            {
                                var sharepointAttribute = (SharepointFieldName)attribute;
                                property.NameFieldEntity = propertyInfo.Name;
                                if (propertyInfo.PropertyType == typeof(LookupFieldMapper))
                                {
                                    property.TypeFieldEntity = "LookupFieldMapper";
                                }
                                else if (propertyInfo.PropertyType == typeof(MultiLookupFieldMapper))
                                {
                                    property.TypeFieldEntity = "MultiLookupFieldMapper";
                                }

                                property.NameFieldSharepoint = sharepointAttribute.GetName();
                                properties.Add(property);
                            }
                            else if (!(attribute is IgnorePropertyInSharepoint))
                            {
                            }

                        }
                    }
                    else
                    {
                        property.NameFieldEntity = propertyInfo.Name;
                        property.NameFieldSharepoint = propertyInfo.Name;
                        properties.Add(property);
                    }
                }
                catch (Exception e)
                {

                }

            }
            return properties;
        }
        private static ListItem BuildListItem<T>(IEnumerable<PropertyInfo> propertyInfos, ListItem listItem, T Item)
            where T : IEntitySharepointMapper
        {
            foreach (MapperDictionaryProperty property in GetProperties(propertyInfos))
            {
                try
                {
                    var propertyInfo = Item.GetType().GetProperty(property.NameFieldEntity);
                    if (property.TypeFieldEntity == "LookupFieldMapper")
                    {
                        //FieldLookupValue lookup = (FieldLookupValue)listItem[property.NameFieldSharepoint];
                        //LookupFieldMapper mapperLookup = new LookupFieldMapper()
                        //{
                        //    ID = lookup.LookupId,
                        //    Value = lookup.LookupValue
                        //};
                        if (propertyInfo.GetValue(Item) == null) continue;
                        var id = ((LookupFieldMapper)propertyInfo.GetValue(Item)).ID;
                        if (id != null)
                            listItem[property.NameFieldSharepoint] =
                                id.Value + ";#" +
                                ((LookupFieldMapper)propertyInfo.GetValue(Item)).Value;
                        //lookup.LookupId = ((LookupFieldMapper)propertyInfo.GetValue(Item)).ID.Value;
                        //lookup.LookupValue = ((LookupFieldMapper)propertyInfo.GetValue(Item)).Value;
                    }
                    else if (property.TypeFieldEntity == "MultiLookupFieldMapper")
                    {
                        if (propertyInfo.GetValue(Item) == null) continue;
                        var ids = ((MultiLookupFieldMapper)propertyInfo.GetValue(Item)).Ids;
                        var mulit = new List<FieldLookupValue>();
                        if (ids.Length > 0)
                        {
                            for (int i = 0; i < ids.Length; i++)
                            {
                                mulit.Add(new FieldLookupValue { LookupId = ids[i] });
                            }
                            listItem[property.NameFieldSharepoint] = mulit.ToArray();
                        }

                    }
                    else if (property.NameFieldSharepoint == "ID"){
                        // Do Nothing
                    }
                    else
                    {
                        if (propertyInfo.GetValue(Item) != null) listItem[property.NameFieldSharepoint] = propertyInfo.GetValue(Item);
                    }
                }
                catch(Exception ex) { }
            }
            return listItem;
        }
        private static T BuildEntity<T>(IEnumerable<PropertyInfo> propertyInfos, ListItem listItem) where T : new()
        {
            var item = new T();
            foreach (MapperDictionaryProperty property in GetProperties(propertyInfos))
            {
                try
                {
                    var propertyInfo = item.GetType().GetProperty(property.NameFieldEntity);
                    if (property.TypeFieldEntity == "LookupFieldMapper")
                    {
                        var lookup = (FieldLookupValue)listItem[property.NameFieldSharepoint];
                        var mapperLookup = new LookupFieldMapper
                        {
                            ID = lookup.LookupId,
                            Value = lookup.LookupValue
                        };
                        propertyInfo.SetValue(item, mapperLookup, null);
                    }
                    else if (property.TypeFieldEntity == "MultiLookupFieldMapper")
                    {
                        var lookup = (FieldLookupValue[])listItem[property.NameFieldSharepoint];
                        var mapperLookup = new MultiLookupFieldMapper
                        {
                            Ids = lookup.Select(x => x.LookupId).ToArray(),
                            Values = lookup.Select(x => x.LookupValue).ToArray()
                        };
                        propertyInfo.SetValue(item, mapperLookup, null);
                    }
                    else if (propertyInfo.PropertyType == typeof(Guid))
                    {
                        var t = listItem[property.NameFieldSharepoint].ToString();
                        var tt = new Guid(listItem[property.NameFieldSharepoint].ToString());
                        propertyInfo.SetValue(item, new Guid(listItem[property.NameFieldSharepoint].ToString()), null);
                    }
                    else{
                        var value = listItem[property.NameFieldSharepoint];
                        propertyInfo.SetValue(item, value, null);
                    }
                }
                catch (Exception e)
                {

                }
            }
            return item;
        }
    }
}
