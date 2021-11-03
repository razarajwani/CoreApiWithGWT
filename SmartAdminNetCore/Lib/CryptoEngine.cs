using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Lib
{
	public class CryptoEngine
	{
		//https://www.c-sharpcorner.com/UploadFile/f8fa6c/data-encryption-and-decryption-in-C-Sharp/

		private static string EncryptionKey = "abcXyz321";

		public static string Encrypt(string clearText)
		{
			byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
			using (Aes encryptor = Aes.Create())
			{
				Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[]
				{
					73,
					118,
					97,
					110,
					32,
					77,
					101,
					100,
					118,
					101,
					100,
					101,
					118
				});
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(clearBytes, 0, clearBytes.Length);
						cs.Close();
					}
					clearText = Convert.ToBase64String(ms.ToArray());
				}
			}
			clearText = clearText.Replace('/', '*');
			return clearText;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002140 File Offset: 0x00000340
		public static string Decrypt(string cipherText)
		{
			cipherText = cipherText.Replace('*', '/');
			cipherText = cipherText.Replace(" ", "+");
			byte[] cipherBytes = Convert.FromBase64String(cipherText);
			using (Aes encryptor = Aes.Create())
			{
				Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[]
				{
					73,
					118,
					97,
					110,
					32,
					77,
					101,
					100,
					118,
					101,
					100,
					101,
					118
				});
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(cipherBytes, 0, cipherBytes.Length);
						cs.Close();
					}
					cipherText = Encoding.Unicode.GetString(ms.ToArray());
				}
			}
			return cipherText;
		}
	}
}