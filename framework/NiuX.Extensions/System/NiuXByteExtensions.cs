using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace System;

/// <summary>
/// 
/// </summary>
public static class NiuXByteExtensions
{
    /// <summary>
    /// 转换为十六进制字符串
    /// </summary>
    /// <param name="b">The b.</param>
    /// <returns></returns>
    public static string ToHex(this byte b)
    {
        return b.ToString("X2");
    }

    /// <summary>
    /// 转换为十六进制字符串
    /// </summary>
    /// <param name="bytes">The bytes.</param>
    /// <returns></returns>
    public static string ToHex(this IEnumerable<byte> bytes)
    {
        var sb = new StringBuilder();

        foreach (var b in bytes)
        {
            sb.Append(b.ToString("X2"));
        }

        return sb.ToString();
    }

    /// <summary>
    /// 转换为 Base64 字符串
    /// </summary>
    /// <param name="bytes">The bytes.</param>
    /// <returns></returns>
    public static string ToBase64String(this byte[] bytes)
    {
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// 转换为基础数据类型
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns></returns>
    public static int ToInt(this byte[] value, int startIndex)
    {
        return BitConverter.ToInt32(value, startIndex);
    }

    /// <summary>
    /// 转换为基础数据类型
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns></returns>
    public static long ToInt64(this byte[] value, int startIndex)
    {
        return BitConverter.ToInt64(value, startIndex);
    }

    /// <summary>
    /// 转换为指定编码的字符串
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns></returns>
    public static string Decode(this byte[] data, Encoding encoding)
    {
        return encoding.GetString(data);
    }

    /// <summary>
    /// Hash
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="hashName">Name of the hash.</param>
    /// <returns></returns>
    public static byte[] Hash(this byte[] data, string hashName = null)
    {
        var algorithm = string.IsNullOrEmpty(hashName) ? HashAlgorithm.Create() : HashAlgorithm.Create(hashName);
        return algorithm.ComputeHash(data);
    }

    #region 保存为文件

    /// <summary>
    /// Saves the specified path.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="path">The path.</param>
    public static void Save(this byte[] data, string path)
    {
        File.WriteAllBytes(path, data);
    }

    #endregion 保存为文件

    #region 转换为内存流

    /// <summary>
    /// Converts to memorystream.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    public static MemoryStream ToMemoryStream(this byte[] data)
    {
        return new MemoryStream(data);
    }

    #endregion 转换为内存流

    #region 位运算

    /// <summary>
    /// index从0开始，获取取第index是否为1
    /// </summary>
    /// <param name="b">The b.</param>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    public static bool GetBit(this byte b, int index)
    {
        return (b & (1 << index)) > 0;
    }

    /// <summary>
    /// 将第index位设为1
    /// </summary>
    /// <param name="b">The b.</param>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    public static byte SetBit(this byte b, int index)
    {
        b |= (byte)(1 << index);
        return b;
    }

    /// <summary>
    /// 将第index位设为0
    /// </summary>
    /// <param name="b">The b.</param>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    public static byte ClearBit(this byte b, int index)
    {
        b &= (byte)((1 << 8) - 1 - (1 << index));
        return b;
    }

    /// <summary>
    /// 将第index位取反
    /// </summary>
    /// <param name="b">The b.</param>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    public static byte ReverseBit(this byte b, int index)
    {
        b ^= (byte)(1 << index);
        return b;
    }

    #endregion 位运算
}