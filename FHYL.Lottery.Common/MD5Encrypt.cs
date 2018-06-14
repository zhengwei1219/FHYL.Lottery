using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FHYL.Lottery.Common
{
    public class MD5Encrypt
    {
        public static string EncryptString(string str)
        {
            byte[] bs = Encoding.Unicode.GetBytes(str);
            MD5 md5 = MD5.Create();
            byte[] bs2 = md5.ComputeHash(bs);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bs2.Length; i++)
            {
                sb.Append(bs2[i].ToString("x"));
            }
            return sb.ToString();
        }
    }
}
