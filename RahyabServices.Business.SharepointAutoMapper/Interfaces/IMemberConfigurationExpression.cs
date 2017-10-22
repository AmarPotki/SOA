namespace RahyabServices.Business.SharepointAutoMapper.InterFaces{
    public interface IMemberConfigurationExpression{
        void MapFrom(string sourceMember);
        void Ignore();
    }
}