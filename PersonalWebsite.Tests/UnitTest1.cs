using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PersonalWebsite.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TryParsingHtmlAndGetImage()
        {
            string content = File.ReadAllText("C:/temp/content.txt");
            var dir = "C:/temp/" + DateTime.UtcNow.ToString("yyyy_MM_dd");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var contentImageFiles = HtmlContentDocumentUtility.GetContentHtml(content, dir);
        }


    }

    public class HtmlContentDocumentUtility
    {
        public static string GetContentHtml(string html, string directory)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);

            var result = document.DocumentNode.SelectNodes("//img");

            
            foreach (var img in result)
            {
                var src = img.GetAttributeValue("src", string.Empty);
                if (!string.IsNullOrEmpty(src) && src.StartsWith("data:image"))
                {
                    var f = ContentImageFile.Parse(src);

                    f.FileName = Path.Combine(directory, f.FileName);
                    File.WriteAllBytes(f.FileName, f.Content);

                    img.SetAttributeValue("src", f.FileName);
                }
            }

            return document.DocumentNode.InnerHtml;
        }
    }

    public class ContentImageFile
    {
        public string FileName { get; set; }

        public byte[] Content { get; private set; }

        private ContentImageFile()
        {
            
        }

        public static ContentImageFile Parse(string img64)
        {
            ContentImageFile cmf = new ContentImageFile();

            var temp = img64.Split(',');
            cmf.Content = Convert.FromBase64String(temp[1]);
            string ext = Base64ImageHelper.GetExtension(temp[0]);
            if(string.IsNullOrEmpty(ext))
                throw new Exception($"Format {temp[0]} not supported.");
            cmf.FileName = Guid.NewGuid() + ext;

            return cmf;
        }
    }

    public static class Base64ImageHelper
    {
        private static readonly Dictionary<string, string> formats = new Dictionary<string, string>
        {
            {"data:image/jpeg;base64", ".jpg" },
            {"data:image/png;base64", ".png" }
        };

        public static string GetExtension(string dataType)
        {
            if (!formats.ContainsKey(dataType))
                return null;

            return formats[dataType];
        }
    }
}
