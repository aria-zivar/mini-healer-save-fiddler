using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;

namespace Mini_Save_Fiddler
{
    internal class Save_Fiddler
    {
        private ResourceManager crypto_params = Mini_Save_Fiddler.Properties.save_params.ResourceManager;


        public string Decrypt(string base64_str)
        {
            
            ICryptoTransform transform = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.None
            }.CreateDecryptor(new Rfc2898DeriveBytes(
                crypto_params.GetString("pass"),
                Encoding.ASCII.GetBytes(crypto_params.GetString("salt"))).GetBytes(32),
                Encoding.ASCII.GetBytes(crypto_params.GetString("iv"))
            );

            MemoryStream memory_stream = new MemoryStream(Convert.FromBase64String(base64_str));
            CryptoStream crypto_stream = new CryptoStream(memory_stream, transform, CryptoStreamMode.Read);

            byte[] save_content = new byte[Convert.FromBase64String(base64_str).Length];
            int count = crypto_stream.Read(save_content, 0, save_content.Length);

            memory_stream.Close();
            crypto_stream.Close();

            return Encoding.UTF8.GetString(save_content, 0, count).TrimEnd("\0".ToCharArray());
        }

        public string Encrypt(string save_json)
        {
            ICryptoTransform transform = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros
            }.CreateEncryptor(new Rfc2898DeriveBytes(
                crypto_params.GetString("pass"),
                Encoding.ASCII.GetBytes(crypto_params.GetString("salt"))).GetBytes(32),
                Encoding.ASCII.GetBytes(crypto_params.GetString("iv"))
            );

            byte[] save_data;

            using (MemoryStream memory_stream = new MemoryStream())
            {
                using (CryptoStream crypto_stream = new CryptoStream(memory_stream, transform, CryptoStreamMode.Write))
                {
                    crypto_stream.Write(Encoding.UTF8.GetBytes(save_json), 0, Encoding.UTF8.GetBytes(save_json).Length);
                    crypto_stream.FlushFinalBlock();
                    save_data = memory_stream.ToArray();
                    crypto_stream.Close();
                }
                memory_stream.Close();
            }

            return Convert.ToBase64String(save_data);
        }
    }
}