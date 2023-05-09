using System.IO;
using System.Text;

// ReSharper disable CheckNamespace

namespace System.Security.Cryptography;

/// <summary>
/// Cryptography Extensions
/// </summary>
public static class NiuXCryptographyExtensions
{
    #region DESC

    /// <summary>
    /// The RGB key
    /// </summary>
    private readonly static byte[] _rgbKey = Encoding.ASCII.GetBytes("");
    /// <summary>
    /// The RGB iv
    /// </summary>
    private readonly static byte[] _rgbIV = Encoding.ASCII.GetBytes("");

    /// <summary>
    /// DES 加密
    /// </summary>
    /// <param name="text">需要加密的值</param>
    /// <returns>
    /// 加密后的结果
    /// </returns>
    public static string Encrypt(string text)
    {
        var dsp = new DESCryptoServiceProvider();
        using (var memStream = new MemoryStream())
        {
            var crypStream = new CryptoStream(memStream, dsp.CreateEncryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);
            var sWriter = new StreamWriter(crypStream);
            sWriter.Write(text);
            sWriter.Flush();
            crypStream.FlushFinalBlock();
            memStream.Flush();
            return Convert.ToBase64String(memStream.GetBuffer(), 0, (int)memStream.Length);
        }
    }

    /// <summary>
    /// DES解密
    /// </summary>
    /// <param name="encryptText">The encrypt text.</param>
    /// <returns>
    /// 解密后的结果
    /// </returns>
    public static string Decrypt(string encryptText)
    {
        var dsp = new DESCryptoServiceProvider();
        var buffer = Convert.FromBase64String(encryptText);

        using (var memStream = new MemoryStream())
        {
            var crypStream = new CryptoStream(memStream, dsp.CreateDecryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);
            crypStream.Write(buffer, 0, buffer.Length);
            crypStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(memStream.ToArray());
        }
    }

    #endregion DESC

    #region MD5

    /// <summary>
    /// MD5加密,和动网上的16/32位MD5加密结果相同,
    /// 使用的UTF8编码
    /// </summary>
    /// <param name="source">待加密字串</param>
    /// <param name="length">16或32值之一,其它则采用.net默认MD5加密算法</param>
    /// <returns>
    /// 加密后的字串
    /// </returns>
    public static string Encrypt(string source, int length = 32) //默认参数
    {
        if (string.IsNullOrEmpty(source))
        {
            return string.Empty;
        }

        var provider = CryptoConfig.CreateFromName("MD5") as HashAlgorithm;
        var bytes = Encoding.UTF8.GetBytes(source); //这里需要区别编码的
        var hashValue = provider.ComputeHash(bytes);
        var sb = new StringBuilder();
        switch (length)
        {
            case 16: //16位密文是32位密文的9到24位字符
                for (var i = 4; i < 12; i++)
                {
                    sb.Append(hashValue[i].ToString("x2"));
                }

                break;

            case 32:
                for (var i = 0; i < 16; i++)
                {
                    sb.Append(hashValue[i].ToString("x2"));
                }

                break;

            default:
                for (var i = 0; i < hashValue.Length; i++)
                {
                    sb.Append(hashValue[i].ToString("x2"));
                }

                break;
        }

        return sb.ToString();
    }

    /// <summary>
    /// 获取文件的MD5摘要
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    public static string AbstractFile(string fileName)
    {
        using (var file = new FileStream(fileName, FileMode.Open))
        {
            return AbstractFile(file);
        }
    }

    /// <summary>
    /// 根据stream获取文件摘要
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns></returns>
    public static string AbstractFile(Stream stream)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        var retVal = md5.ComputeHash(stream);

        var sb = new StringBuilder();
        for (var i = 0; i < retVal.Length; i++)
        {
            sb.Append(retVal[i].ToString("x2"));
        }

        return sb.ToString();
    }

    #endregion MD5
}