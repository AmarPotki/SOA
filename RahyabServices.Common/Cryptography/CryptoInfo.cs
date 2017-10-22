using System;
namespace RahyabServices.Common.Cryptography{
    public class CryptoInfo
    {
        public CryptoInfo()
        {

        }
        public CryptoInfo(string userName)
        {
            Ticks = DateTime.Now.Ticks;
            UserName = userName;
        }
        public string UserName { get; set; }
        public long Ticks { get; set; }
    }
}