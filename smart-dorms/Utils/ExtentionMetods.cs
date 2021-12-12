using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;


namespace smart_dorms.Utils
{
    public static class ExtensionMetods
    {
        public static byte[] ToByteArray(this Stream input, long fileSize)
        {
            byte[] buffer = new byte[fileSize];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}

