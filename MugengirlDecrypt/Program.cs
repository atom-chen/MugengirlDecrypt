using System.IO;
using System.Security.Cryptography;

namespace MugengirlDecrypt
{
    class Program
    {
        static byte[] Decrypt(byte[] bytes)
        {
            var aes = Aes.Create();
            var key = new byte[] { 0xA8, 0x28, 0x81, 0xDD, 0x7D, 0xD3, 0xE8, 0x3E, 0xBE, 0xB3, 0xAA, 0x7B, 0xB2, 0x18, 0x37, 0x34, 0xE8, 0xB6, 0xC0, 0x75, 0x1D, 0x6F, 0xB0, 0x08, 0xAF, 0x59, 0xC5, 0x03, 0x77, 0x5F, 0x3C, 0xDE };
            var iv = new byte[] { 0xee, 0x57, 0xce, 0xfa, 0xf9, 0xeb, 0x0f, 0x98, 0x39, 0xff, 0xf1, 0x70, 0x49, 0x13, 0xdd, 0x1a };
            
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            aes.KeySize = 256;
            using (var decryptor = aes.CreateDecryptor(key, iv))
            {
                return decryptor.TransformFinalBlock(bytes, 0, bytes.Length);
            }
        }

        static void Main(string[] args)
        {
            foreach (var filePath in Directory.EnumerateFiles("cards", "*.png", SearchOption.AllDirectories))
            {
                var bytes = File.ReadAllBytes(filePath);
                bytes = Decrypt(bytes);
                File.WriteAllBytes(filePath, bytes);
            }
        }
    }
}
