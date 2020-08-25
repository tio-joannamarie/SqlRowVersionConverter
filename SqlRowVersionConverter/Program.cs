using System;

namespace SqlRowVersionConverter
{
  public class Program
  {
    public static void Main()
    {
      ConvertSerializedRowVersionStringToSqlRowVersion("AAAAAAAIo2Q=");
      ConvertSqlRowVersionToSerializedString("0x000000000008A364");
    }

    private static void ConvertSerializedRowVersionStringToSqlRowVersion(string rowVersionSerializedString)
    {
      byte[] mybyteArray = Convert.FromBase64String(rowVersionSerializedString);

      string hexString = "0x";
      foreach (var x in mybyteArray)
      {
        hexString += x.ToString("X2");
      }

      Console.WriteLine(rowVersionSerializedString + " is " + hexString);
    }

    private static void ConvertSqlRowVersionToSerializedString(string sqlRowVersion)
    {
      //Console.WriteLine(Environment.NewLine);
      var rowVersionBytes = new byte[8];

      var arrayIndex = 0;
      for (var i = 2; i <= sqlRowVersion.Length - 1; i += 2)
      {
        var hexString = sqlRowVersion.Substring(i, 2);
        rowVersionBytes[arrayIndex] = byte.Parse(hexString, System.Globalization.NumberStyles.HexNumber);
        //Console.WriteLine(hexString + " is " + byte.Parse(hexString, System.Globalization.NumberStyles.HexNumber));
        arrayIndex++;
      }

      var rowVersionString = ConvertByteArrayToSerializedRowVersionString(rowVersionBytes);
      Console.WriteLine(Environment.NewLine + sqlRowVersion + " is " + rowVersionString);
    }

    private static string ConvertByteArrayToSerializedRowVersionString(byte[] rowVersion)
    {
      string byteStr = Convert.ToBase64String(rowVersion);
      return byteStr;
      //Console.WriteLine(Environment.NewLine + string.Join(",", rowVersion) + " is " + byteStr);
    }
  }
}
