using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Web;

namespace WasterWebAPI.Handlers
{
    public class IdEncryptor
    {
        byte[] Key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
        byte[] IV = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };


        public byte[] Encrypt(object id)
        {
            byte[] cipherText;
            byte[] plainText = System.Text.Encoding.Unicode.GetBytes(id.ToString());
            var cipher = new RijndaelManaged();


            // return encrypted;
            // var byteObject = stream.ToArray();

            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, cipher.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
            {
                cryptoStream.Write(plainText, 0, plainText.Length);
                cipherText = memoryStream.ToArray();
            }

            return cipherText;

        }

        public object Decrypt(byte[] encryptBytes)
        {
            return new object();
        }

    }
}