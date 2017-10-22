namespace RahyabServices.Common.Cryptography
{
    public interface ICryptographer
    {
        string CreateSalt();
        string ComputeHash(string valueToHash);
        string GetPasswordHash(string password, string salt);
        string Encrypt(string data, byte[] key = null);
        string EncryptMac(string data);
        string Decrypt(string data, byte[] key = null);
        long DecryptForLong(string data, byte[] key = null);
        string EncryptCryptoInfo(CryptoInfo cryptoInfo, byte[] key = null);
        string GetHashFromKeyAndSalt(string key, string salt);
        CryptoInfo DecryptCryptoInfo(string data, byte[] key = null);
        string CreateMd5(string input);
        byte[] EncryptStringToBytes_Aes(string plainText, byte[] key = null,
            byte[] iV = null);
        string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key = null,
            byte[] iV = null);
    }
}
