using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;

namespace WhileLearningCzech.Domain.Services.Media
{
    public interface IDirService
    {
        string GetImagesDir();
    }

    public interface IHtmlImagesService
    {
        string ParseHtmlImages(string html);
    }

    public class HtmlImagesService : IHtmlImagesService
    {
        private readonly IDirService _dirService;

        public HtmlImagesService(IDirService dirService)
        {
            _dirService = dirService;
        }

        public string ParseHtmlImages(string html)
        {
            string dateDir = GetCurrentFolderDate();
            var dir = Path.Combine(_dirService.GetImagesDir(), dateDir);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            return GetContentHtml(html, dir, Path.Combine("Content/images", dateDir));
        }

        private string GetCurrentFolderDate()
        {
            return DateTime.UtcNow.ToString("yyyy_MM_dd");
        }

        public static string GetContentHtml(string html, string directory, string relative = "Content/Images")
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);

            var result = document.DocumentNode.SelectNodes("//img");
            if (result == null)
                return html;

            foreach (var img in result)
            {
                var src = img.GetAttributeValue("src", string.Empty);
                if (!string.IsNullOrEmpty(src) && src.StartsWith("data:image"))
                {
                    var f = ContentImageFile.Parse(src);

                    var path = Path.Combine(directory, f.FileName);
                    File.WriteAllBytes(path, f.Content);

                    f.FileName = Path.Combine(relative, f.FileName);
                    img.SetAttributeValue("src", f.FileName);
                }
            }

            return document.DocumentNode.InnerHtml;
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
                if (string.IsNullOrEmpty(ext))
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


}
