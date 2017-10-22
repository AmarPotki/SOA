using RahyabServices.Business.Contracts.Interfaces;

namespace RahyabServices.Business.Contracts.Implementations
{
    public class CallbackEventArgs : ICallbackEventArgs
    {
        public string Method { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}