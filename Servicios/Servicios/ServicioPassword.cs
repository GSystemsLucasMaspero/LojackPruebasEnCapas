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
        public String Encrypt(String cleanPassword)
        {
            return ComputeMD5Hash(cleanPassword);
        }

        private string ComputeMD5Hash(string cleanString)
        {
            Byte[] clearBytes = new ASCIIEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

            string tempStr = BitConverter.ToString(hashedBytes);
            string MD5Hash = tempStr.Replace("-", "").Trim();

            return MD5Hash;
        }
    }
}
