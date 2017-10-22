using System;
namespace RahyabServices.Business.SharepointAutoMapper.InterFaces{
    public interface IMappingExpression{
        IMappingExpression ForMember(string name, Action<IMemberConfigurationExpression> memberOptions);
    }
}