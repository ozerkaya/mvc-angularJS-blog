using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace OzerBlog.Helpers
{
    public class Security
    {
        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();
            if (value != null)
            {
                using (SHA256 hash = SHA256Managed.Create())
                {
                    Encoding enc = Encoding.UTF8;
                    Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                    foreach (Byte b in result)
                        Sb.Append(b.ToString("x2"));
                }
            }
            return Sb.ToString();
        }
    }
}