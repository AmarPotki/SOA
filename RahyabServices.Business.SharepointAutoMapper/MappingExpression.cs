using RahyabServices.Business.SharepointAutoMapper.InterFaces;
using System;

namespace RahyabServices.Business.SharepointAutoMapper
{
    public class MappingExpression : IMappingExpression
    {
        public MappingExpression(string listName, Type entity)
        {
            MappingStore.MapperModel.Add(new SharepointMapperModel
            {
                ListName = listName,
                EntityType = entity,
                EntityName = entity.Name,
            });
        }
        public IMappingExpression ForMember(string name, Action<IMemberConfigurationExpression> memberOptions)
        {
            return new MappingExpression("", null);
        }
    }
}
