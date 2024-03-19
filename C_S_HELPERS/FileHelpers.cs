namespace MoreDotNet.Helpers
{
    using System;
    using System.IO;

    public static class FileHelpers
    {
        public static string SaveStringToTempFile(string stringToWrite)
        {
            var tempFilePath = Path.GetTempFileName();
            File.WriteAllText(tempFilePath, stringToWrite);
            return tempFilePath;
        }

        public static string SaveByteArrayToTempFile(byte[] dataToWrite)
        {
            if (dataToWrite == null)
            {
                throw new ArgumentNullException(nameof(dataToWrite));
            }

            var tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, dataToWrite);
            return tempFilePath;
        }
    }
}
