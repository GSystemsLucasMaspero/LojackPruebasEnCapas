using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;


namespace Servicios.Servicios
{

    public class ServicioPassword
    {
        public String Decrypt()
        {
            return null;
        }

        public String Encrypt()
        {
            return null;
        }
    }

    public class SymmetricCipher
    {
        public readonly string Texto;
        public readonly string Passphrase;
        public readonly string Semilla;
        public readonly string VectorInicio;
        public readonly int Iteraciones;
        public readonly int LongitudClave;

        private readonly string AlgoritmoHashClaveCifrado = "MD5";
        private string textoCifrado = string.Empty;
        private string textoPlano = string.Empty;
        
        public SymmetricCipher(string texto, string passphrase, string semilla, string vectorInicio, int iteraciones, int logitudClave)
        {
            Texto = texto;
            Passphrase = passphrase;
            Semilla = semilla;
            VectorInicio = vectorInicio.PadRight(16, 'x').Substring(0, 16);
            Iteraciones = iteraciones;
            LongitudClave = logitudClave;

            textoCifrado = texto;
            textoPlano = texto;
        }

        public string Decrypt()
        {
            // pasamos todos los strings a byte[]
            byte[] passphraseBytes = Encoding.UTF8.GetBytes(this.Passphrase);
            byte[] semillaBytes = Encoding.UTF8.GetBytes(this.Semilla);
            byte[] vectorBytes = Encoding.UTF8.GetBytes(this.VectorInicio);
            byte[] textoCifradoBytes = Convert.FromBase64String(this.textoCifrado);

            // generamos una clave a partir de la passphrase y semilla usando el algoritmo especificado e iterando tantas veces como se indique
            PasswordDeriveBytes password = new PasswordDeriveBytes(passphraseBytes, semillaBytes, AlgoritmoHashClaveCifrado, this.Iteraciones);

            byte[] passwordBytes = password.GetBytes(this.LongitudClave / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(passwordBytes, vectorBytes);

            MemoryStream memStream = new MemoryStream(textoCifradoBytes);

            CryptoStream cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            byte[] textoPlanoBytes = new byte[textoCifradoBytes.Length];

            // Desencriptado
            int decryptedByteCount = cryptoStream.Read(textoPlanoBytes, 0, textoPlanoBytes.Length);

            memStream.Close();
            cryptoStream.Close();

            textoPlano = Encoding.UTF8.GetString(textoPlanoBytes, 0, decryptedByteCount);

            return textoPlano;
        }

        public string Encrypt()
        {
            // pasamos todos los strings a byte[]
            byte[] passphraseBytes = Encoding.UTF8.GetBytes(this.Passphrase);
            byte[] semillaBytes = Encoding.UTF8.GetBytes(this.Semilla);
            byte[] vectorBytes = Encoding.UTF8.GetBytes(this.VectorInicio);
            byte[] textoPlanoBytes = Encoding.UTF8.GetBytes(this.textoPlano);

            // generamos una clave a partir de la passphrase y semilla usando el algoritmo especificado e iterando tantas veces como se indique
            PasswordDeriveBytes password = new PasswordDeriveBytes(passphraseBytes, semillaBytes, AlgoritmoHashClaveCifrado, this.Iteraciones);

            byte[] passwordBytes = password.GetBytes(this.LongitudClave / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(passwordBytes, vectorBytes);

            MemoryStream memStream = new MemoryStream();

            CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write);

            // Encriptado
            cryptoStream.Write(textoPlanoBytes, 0, textoPlanoBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] textoCifradoBytes = memStream.ToArray();

            memStream.Close();
            cryptoStream.Close();

            textoCifrado = Convert.ToBase64String(textoCifradoBytes);

            return textoCifrado;
        }
    }

    public class ToolsCryptography
    {
        public ToolsCryptography()
        {
        }

        public static string ComputeMD5Hash(string cleanString)
        {
            Byte[] clearBytes = new ASCIIEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

            string tempStr = BitConverter.ToString(hashedBytes);
            string MD5Hash = tempStr.Replace("-", "").Trim();

            return MD5Hash;
        }

        /*
        public static string ComputeMD5Hash(string cleanString)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
                    
            return BitConverter.ToString(hashedBytes);
        }
        */

    }

}
