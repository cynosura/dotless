namespace dotless.Core.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Security.Cryptography;

    static class HashUtils
    {
        public static string ComputeMD5(string source) {
            return ComputeMD5(System.Text.Encoding.Default.GetBytes(source));
        }

        public static string ComputeMD5(byte[] source) {
            using (MD5 md5 = MD5.Create()) {
                byte[] computeHash = md5.ComputeHash(source);
                return Convert.ToBase64String(computeHash);
            }
        }
    }
}
