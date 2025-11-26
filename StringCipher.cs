using System.Security.Cryptography;
using System.Text;

namespace Hcb.Rnd.Pwn.Application.Common.Extensions;

public static class StringCipher
{
    //TODO: Move this to app config or key vault.
    private static readonly string _key = "b14ca5898a4e4133bbce2ea2315a1916";
    public static string Encrypt(this string value)
    {
        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
            using (StreamWriter streamWriter = new(cryptoStream))
            {
                streamWriter.Write(value);
            }

            array = memoryStream.ToArray();
        }
        return Convert.ToBase64String(array);
    }

    public static string Decrypt(this string value)
    {
        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(value);

        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_key);
        aes.IV = iv;
        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using MemoryStream memoryStream = new(buffer);
        using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
        using StreamReader streamReader = new(cryptoStream);
        return streamReader.ReadToEnd();
    }
}
