using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace RahyabServices.Common.Cryptography
{
    public class Cryptographer : ICryptographer
    {
        // Rijndael Key of 16-bytes
        private static readonly byte[] EnKey = { 45 };
        private static readonly byte[] AesKey =
        {
            0x38
        };
        private static readonly byte[] AesIv =
        {
            0x00
        };
        public string ComputeHash(string valueToHash)
        {
            var algorithm = SHA512.Create();
            var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(valueToHash));
            return Convert.ToBase64String(hash);
        }
        public string CreateSalt()
        {
            var size = 64;
            //Generate a cryptographic random number.
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }
        public string Decrypt(string data, byte[] key = null)
        {
            key = key ?? EnKey;
            try
            {
                // byte data
                var internalData = Convert.FromBase64String(data.Replace("_", "/").Replace("-", "+"));

                // Create a new Rijndael object to generate a key
                // and initialization vector (IV).
                var rijndaelAlgo = new RijndaelManaged { Key = key, Padding = PaddingMode.PKCS7 };
                rijndaelAlgo.IV = new byte[rijndaelAlgo.IV.Length];

                // Create a CryptoStream using the MemoryStream
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, rijndaelAlgo.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(internalData, 0, internalData.Length);
                        cs.FlushFinalBlock();
                        return Encoding.ASCII.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception)
            {
                return "UnKnown";
            }
        }
        public CryptoInfo DecryptCryptoInfo(string data, byte[] key = null)
        {
            key = key ?? EnKey;
            try
            {
                // byte data
                var internalData = Convert.FromBase64String(data.Replace("_", "/").Replace("-", "+"));

                // Create a new Rijndael object to generate a key
                // and initialization vector (IV).
                var rijndaelAlgo = new RijndaelManaged { Key = key, Padding = PaddingMode.PKCS7 };
                rijndaelAlgo.IV = new byte[rijndaelAlgo.IV.Length];

                // Create a CryptoStream using the MemoryStream
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, rijndaelAlgo.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(internalData, 0, internalData.Length);
                        cs.FlushFinalBlock();
                        var decryptString = Encoding.ASCII.GetString(ms.ToArray());
                        var decryptedValue = decryptString.Split(',');
                        return new CryptoInfo
                        {
                            UserName = decryptedValue[0],
                            Ticks = long.Parse(decryptedValue[1]),
                        };
                    }
                }
            }
            catch (Exception)
            {
                return new CryptoInfo {Ticks = 0,UserName = ""};
            }
        }
        public long DecryptForLong(string data, byte[] key = null)
        {
            key = key ?? EnKey;
            try
            {
                // byte data
                var internalData = Convert.FromBase64String(data.Replace("_", "/").Replace("-", "+"));

                // Create a new Rijndael object to generate a key
                // and initialization vector (IV).
                var rijndaelAlgo = new RijndaelManaged { Key = key, Padding = PaddingMode.PKCS7 };
                rijndaelAlgo.IV = new byte[rijndaelAlgo.IV.Length];

                // Create a CryptoStream using the MemoryStream
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, rijndaelAlgo.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(internalData, 0, internalData.Length);
                        cs.FlushFinalBlock();
                        var str = Encoding.ASCII.GetString(ms.ToArray());
                        long id;
                        var res = long.TryParse(str, out id);
                        return res ? id : 0;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public string Encrypt(string data, byte[] key = null)
        {
            key = key ?? EnKey;
            try
            {
                // byte data
                var internalData = Encoding.ASCII.GetBytes(data);

                // Create a new Rijndael object to generate a key
                // and initialization vector (IV).
                var rijndaelAlgo = new RijndaelManaged { Key = key, Padding = PaddingMode.PKCS7 };
                rijndaelAlgo.IV = new byte[rijndaelAlgo.IV.Length];

                // Create a CryptoStream using the MemoryStream
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, rijndaelAlgo.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(internalData, 0, internalData.Length);
                        cs.FlushFinalBlock();
                        return (Convert.ToBase64String(ms.ToArray())).Replace("/", "_").Replace("+", "-");
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string EncryptCryptoInfo(CryptoInfo cryptoInfo, byte[] key = null)
        {
            key = key ?? EnKey;
            try{
                var data = cryptoInfo.UserName + "," + cryptoInfo.Ticks;
                // byte data
                var internalData = Encoding.ASCII.GetBytes(data);

                // Create a new Rijndael object to generate a key
                // and initialization vector (IV).
                var rijndaelAlgo = new RijndaelManaged { Key = key, Padding = PaddingMode.PKCS7 };
                rijndaelAlgo.IV = new byte[rijndaelAlgo.IV.Length];

                // Create a CryptoStream using the MemoryStream
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, rijndaelAlgo.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(internalData, 0, internalData.Length);
                        cs.FlushFinalBlock();
                        return (Convert.ToBase64String(ms.ToArray())).Replace("/", "_").Replace("+", "-");
                    }
                }
            }
            catch (Exception e)
            {
                return "Encrypted Error";
            }
        }
        public string EncryptMac(string data)
        {
            var key = new byte[] { 41};
            var IV = new byte[8];
            var internalData = Encoding.ASCII.GetBytes(data);
            var DESalg = DES.Create();
            DESalg.Mode = CipherMode.CBC;
            DESalg.Padding = PaddingMode.None;
            var crypt = DESalg.CreateEncryptor(key, IV);
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, DESalg.CreateEncryptor(key, IV), CryptoStreamMode.Write))
                {
                    cs.Write(internalData, 0, internalData.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
        public string GetHashFromKeyAndSalt(string key, string salt)
        {
            return GetPasswordHash(key, salt);
        }
        public string GetPasswordHash(string password, string salt)
        {
            return ComputeHash(password + salt);
        }
        public string CreateMd5(string filePath)
        {
            // Use input string to calculate MD5 hash
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var hashBytes = md5.ComputeHash(stream);
                    var tt = Encoding.Default.GetString(md5.ComputeHash(stream));
                    var sb = new StringBuilder();
                    foreach (var t in hashBytes) { sb.Append(t.ToString("X2")); }
                    return sb.ToString();
                }
            }
        }
        public byte[] EncryptStringToBytes_Aes(string plainText, byte[] key = null,
            byte[] iV = null)
        {
            key = key ?? AesKey;
            iV = iV ?? AesIv;
            // Check arguments.
            if (plainText == null || plainText.Length <= 0) throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0) throw new ArgumentNullException("key");
            if (iV == null || iV.Length <= 0) throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iV;
                //  aesAlg.BlockSize = 128;
                // aesAlg.KeySize = 128;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.Zeros;
                // Create a decrytor to perform the stream transform.
                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key
                    , aesAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt
                        , encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(
                            csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }
        public string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] key = null,
            byte[] iV = null)
        {
            key = key ?? AesKey;
            iV = iV ?? AesIv;
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0) throw new ArgumentNullException("cipherText");
            if (EnKey == null || EnKey.Length <= 0) throw new ArgumentNullException("Key");
            if (iV == null || iV.Length <= 0) throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (var aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iV;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.Zeros;
                // Create a decrytor to perform the stream transform.
                var decryptor = aesAlg.CreateDecryptor(aesAlg.Key
                    , aesAlg.IV);

                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt
                        , decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(
                            csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}
