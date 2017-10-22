using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.SharePoint.Client;
using RahyabServices.Business.SharepointAutoMapper.Models;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;

namespace RahyabServices.Business.SharepointAutoMapper
{
    public static class SharepointMapper
    {
        private static readonly ConfigurationStore _configuration = new ConfigurationStore();
        public static IConfiguration Configuration
        {
            get { return _configuration; }
        }
        private static IEnumerable<MapperDictionaryProperty> GetProperties(IEnumerable<PropertyInfo> propertyInfos)
        {
            var properties = new List<MapperDictionaryProperty>();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                var property = new MapperDictionaryProperty();
                var attributes = propertyInfo.GetCustomAttributes();
                var enumerable = attributes as Attribute[] ?? attributes.ToArray();
                if (enumerable.Any())
                {
                    foreach (Attribute attribute in enumerable)
                    {
                        var name = attribute as SharepointFieldName;
                        if (name != null)
                        {
                            var sharepointAttribute = name;
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
                        else if (!(attribute is IgnorePropertyInSharepoint)) { }
                    }
                }
                else
                {
                    property.NameFieldEntity = propertyInfo.Name;
                    property.NameFieldSharepoint = propertyInfo.Name;
                    properties.Add(property);
                }
            }
            return properties;
        }
        public static List<T> ProjectToListEntity<T>(this ListItemCollection value) where T : new()
        {
            var propertyInfos = typeof(T).GetProperties();
            return value.Select(listItem => BuildEntity<T>(propertyInfos, listItem)).ToList();
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
                    else
                    {
                        propertyInfo.SetValue(item, listItem[property.NameFieldSharepoint], null);
                    }
                }
                catch { }
            }
            return item;
        }

        //public static void Initialize(Action<IConfiguration> action)
        //{
        //    Reset();

        //    action(Configuration);

        //    Configuration.Seal();
        //}
        public static IMappingExpression CreateMap<T>(String listName) where T : class
        {
            return Configuration.CreateMap<T>(listName);
        }

        //public static SharepointMapper ForMember(this SharepointMapper value)
        //{
        //    return value;
        //}
    }
}
