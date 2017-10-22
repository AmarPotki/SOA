using System;
using RahyabServices.Business.SharepointAutoMapper.InterFaces;

namespace RahyabServices.Business.SharepointAutoMapper
{
    public interface IConfiguration
    {
        IMappingExpression CreateMap<TSource>(String listName);
    }
}
