using System;
namespace RahyabServices.Business.SharepointAutoMapper{
    [AttributeUsage(AttributeTargets.All |
                    AttributeTargets.Struct,
        AllowMultiple = true)]
    public class SharepointFieldName : Attribute{
        private readonly string _name;
        public double Version;
        public SharepointFieldName(string name){
            _name = name;

            // Default value.
            Version = 1.0;
        }
        public string GetName(){
            return _name;
        }
    }
    [AttributeUsage(AttributeTargets.All |
                    AttributeTargets.Struct,
        AllowMultiple = true)]
    public class IgnorePropertyInSharepoint : Attribute{}
    [AttributeUsage(AttributeTargets.All |
                    AttributeTargets.Struct,
        AllowMultiple = true)]
    public class MapperLookupId : Attribute{
        private readonly string _lookupName;
        public double Version;
        public MapperLookupId(string lookupName){
            _lookupName = lookupName;

            // Default value.
            Version = 1.0;
        }
        public string GetName(){
            return _lookupName;
        }
    }
    [AttributeUsage(AttributeTargets.All |
                    AttributeTargets.Struct,
        AllowMultiple = true)]
    public class MapperLookupValue : Attribute{
        private readonly string _lookupName;
        public double Version;
        public MapperLookupValue(string lookupName){
            _lookupName = lookupName;

            // Default value.
            Version = 1.0;
        }
        public string GetName(){
            return _lookupName;
        }
    }
    [AttributeUsage(AttributeTargets.All |
                    AttributeTargets.Struct,
        AllowMultiple = true)]
    public class SharepointListName : Attribute{
        private readonly string _listName;
        public double Version;
        public SharepointListName(string listName){
            _listName = listName;

            // Default value.
            Version = 1.0;
        }
        public string GetName(){
            return _listName;
        }
    }
    public class SharepointListId : Attribute{
        private readonly string _listId;
        public double Version;
        public SharepointListId(string listId){
            _listId = listId;

            // Default value.
            Version = 1.0;
        }
        public string GetName(){
            return _listId;
        }
    }
}