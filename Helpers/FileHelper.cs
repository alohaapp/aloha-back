using System;
using System.Net.Mime;
using Aloha.Model.Entities;

namespace Aloha.Helpers.FileHelper
{
    public class FileHelper
    {
        public static File GetFileFromBase64(string data)
        {
            try
            {
                string[] typeAndDataSplit = data.Split(";");

                var contentType = new ContentType(typeAndDataSplit[0].Replace("data:", string.Empty));
                if (contentType == null)
                {
                    return null;
                }

                var dataBytes = Convert.FromBase64String(typeAndDataSplit[1].Split(",")[1]);
                if (dataBytes.Length == 0)
                {
                    return null;
                }

                return new File
                {
                    MediaType = contentType.MediaType,
                    Data = dataBytes
                };
            }
            catch
            {
                return null;
            }
        }
    }
}