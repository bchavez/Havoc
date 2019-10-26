using System;
using System.Text;

namespace Havoc.Crypto
{
   public static class ExtensionsForXXTEA
   {
      public const string DefaultKey = "havoc";
      public static readonly byte[] DefaultKeyBytes = Encoding.ASCII.GetBytes(DefaultKey);

      public static byte[] Encrypt(this byte[] data)
      {
         return XXTEA.Encrypt(data, DefaultKeyBytes);
      }

      public static byte[] Encrypt(this string data)
      {
         return XXTEA.Encrypt(data, DefaultKeyBytes);
      }

      public static byte[] Encrypt(this byte[] data, byte[] key)
      {
         return XXTEA.Encrypt(data, key);
      }

      public static byte[] Encrypt(this string data, byte[] key)
      {
         return XXTEA.Encrypt(data, key);
      }

      public static byte[] Encrypt(this byte[] data, string key)
      {
         return XXTEA.Encrypt(data, key);
      }

      public static byte[] Encrypt(this string data, string key)
      {
         return XXTEA.Encrypt(data, key);
      }

      public static string EncryptToBase64String(this byte[] data)
      {
         return XXTEA.EncryptToBase64String(data, DefaultKeyBytes);
      }

      public static string EncryptToBase64String(this string data)
      {
         return XXTEA.EncryptToBase64String(data, DefaultKeyBytes);
      }

      public static string EncryptToBase64String(this byte[] data, byte[] key)
      {
         return XXTEA.EncryptToBase64String(data, key);
      }

      public static string EncryptToBase64String(this string data, byte[] key)
      {
         return XXTEA.EncryptToBase64String(data, key);
      }

      public static string EncryptToBase64String(this byte[] data, string key)
      {
         return XXTEA.EncryptToBase64String(data, key);
      }

      public static string EncryptToBase64String(this string data, string key)
      {
         return XXTEA.EncryptToBase64String(data, key);
      }




      public static byte[] Decrypt(this byte[] data)
      {
         return XXTEA.Decrypt(data, DefaultKeyBytes);
      }

      public static byte[] Decrypt(this byte[] data, byte[] key)
      {
         return XXTEA.Decrypt(data, key);
      }

      public static byte[] Decrypt(this byte[] data, string key)
      {
         return XXTEA.Decrypt(data, key);
      }

      public static byte[] DecryptBase64String(this string data)
      {
         return XXTEA.DecryptBase64String(data, DefaultKeyBytes);
      }

      public static byte[] DecryptBase64String(this string data, byte[] key)
      {
         return XXTEA.DecryptBase64String(data, key);
      }

      public static byte[] DecryptBase64String(this string data, string key)
      {
         return XXTEA.DecryptBase64String(data, key);
      }

      public static string DecryptToString(this byte[] data)
      {
         return XXTEA.DecryptToString(data, DefaultKeyBytes);
      }

      public static string DecryptToString(this byte[] data, byte[] key)
      {
         return XXTEA.DecryptToString(data, key);
      }

      public static string DecryptToString(this byte[] data, string key)
      {
         return XXTEA.DecryptToString(data, key);
      }

      public static string DecryptBase64StringToString(this string data)
      {
         return XXTEA.DecryptBase64StringToString(data, DefaultKeyBytes);
      }
      public static string DecryptBase64StringToString(this string data, byte[] key)
      {
         return XXTEA.DecryptBase64StringToString(data, key);
      }

      public static string DecryptBase64StringToString(this string data, string key)
      {
         return XXTEA.DecryptBase64StringToString(data, key);
      }
   }
}