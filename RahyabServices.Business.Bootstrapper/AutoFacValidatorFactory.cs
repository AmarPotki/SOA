using System;
using Autofac;
using FluentValidation;
namespace RahyabServices.Business.Bootstrapper{
    public class AutoFacValidatorFactory : IValidatorFactory{
        private readonly IComponentContext _container;
        public AutoFacValidatorFactory(IComponentContext provider){
            _container = provider;
        }

        //public IValidator<T> GetValidator<T>()
        //{
        //    return _container.Resolve<IValidator<T>>();
        //}
        public IValidator<T> GetValidator<T>(){
            return (IValidator<T>) GetValidator(typeof (T));
        }
        public IValidator GetValidator(Type type){
            var genericType = typeof (IValidator<>).MakeGenericType(type);
            object validator;
            if (_container.TryResolve(genericType, out validator)) return (IValidator) validator;
            return null;
        }
    }
}