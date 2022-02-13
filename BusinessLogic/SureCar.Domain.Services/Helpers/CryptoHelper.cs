using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using SureCar.Services.Interface.Helpers;
using System.Security.Cryptography;
using System.Text;

namespace SureCar.Services.Helpers
{
    public class CryptoHelper : ICryptoHelper
    {
        private readonly RSACryptoServiceProvider _privateKey;

        public CryptoHelper()
        {
            _privateKey = GetPrivateKeyFromPemFile();
        }

        public string Decrypt(string encrypted)
        {
            var decryptedBytes = _privateKey.Decrypt(Convert.FromBase64String(encrypted), false);
            return Encoding.UTF8.GetString(decryptedBytes, 0, decryptedBytes.Length);
        }

        private RSACryptoServiceProvider GetPrivateKeyFromPemFile()
        {
            using TextReader privateKeyStringReader = new StringReader(File.ReadAllText(GetPath()));
            AsymmetricCipherKeyPair pemReader = (AsymmetricCipherKeyPair)new PemReader(privateKeyStringReader).ReadObject();
            RSAParameters rsaPrivateCrtKeyParameters = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters)pemReader.Private);
            RSACryptoServiceProvider rsaCryptoServiceProvider = new RSACryptoServiceProvider();
            rsaCryptoServiceProvider.ImportParameters(rsaPrivateCrtKeyParameters);
            return rsaCryptoServiceProvider;
        }

        private string GetPath()
        {
            return Path.Combine(
                Directory.GetParent(Environment.CurrentDirectory).Parent.FullName
                + "\\BusinessLogic\\SureCar.Domain.Services\\Helpers", "private.ket.pem");
        }
    }
}
