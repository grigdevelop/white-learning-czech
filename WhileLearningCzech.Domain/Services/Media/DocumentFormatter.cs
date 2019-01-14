using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using WhileLearningCzech.Domain.Core.Abstract;
using WhileLearningCzech.Domain.Helpers.Exceptions;

namespace WhileLearningCzech.Domain.Services.Media
{
    public class DocumentFormatter
    {
        private readonly HtmlDocument _htmlDocument;

        public string Html => _htmlDocument.DocumentNode.InnerHtml;

        public DocumentFormatter(string html)
        {
            _htmlDocument = new HtmlDocument();
            _htmlDocument.LoadHtml(html);
        }

        public List<ImageData> GetImageSources()
        {
            var imageDataList = new List<ImageData>();

            var imgTags = _htmlDocument.DocumentNode.SelectNodes("//img")?.ToList();
            if (imgTags == null || imgTags.Count == 0) return imageDataList;

            foreach (var htmlNode in imgTags)
            {
                var source = htmlNode.GetAttributeValue("src", string.Empty);
                if (string.IsNullOrEmpty(source)) continue;

                if(!IsSourceImage(source)) continue;

                var imageData = GetImageData(source, htmlNode);
                imageDataList.Add(imageData);
            }

            return imageDataList;
        }

        public List<int> GetInDocumentImagesList(string relativePath = "images/source/")
        {
            List<int> idList = new List<int>();

            var imgTags = _htmlDocument.DocumentNode.SelectNodes("//img")?.ToList();
            if (imgTags == null || imgTags.Count == 0) return idList;

            foreach (var htmlNode in imgTags)
            {
                var source = htmlNode.GetAttributeValue("src", string.Empty);
                if (!source.StartsWith(relativePath)) continue;

                int startIndex = source.LastIndexOf('/') + 1;
                int endIndex = source.LastIndexOf('.');
                int length = endIndex - startIndex;
                var substr = source.Substring(startIndex, length);
                int id = int.Parse(substr);
                idList.Add(id);
            }


            return idList;
        }

        public static bool IsSourceImage(string source)
        {
            return (!string.IsNullOrEmpty(source) && source.StartsWith("data:image"));
        }

        public static ImageData GetImageData(string source, HtmlNode node)
        {
            var temp = source.Split(",");
            if (temp.Length != 2) throw new ApiException("Image source is not valid");

            var imageData = new ImageData(node);
            imageData.Data = Convert.FromBase64String(temp[1]);
            imageData.DataType = temp[0];
            imageData.Extension = GetExtensionFromMimeType(temp[0]);
            return imageData;
        }

        private static string GetExtensionFromMimeType(string mimeType)
        {
            switch (mimeType)
            {
                case "data:image/jpeg;base64":
                    return ".jpg";
                case "data:image/png;base64":
                    return ".png";
                default:
                    throw new ApiException("Unknown image format.");
            }
        }

        public class ImageData
        {
            private readonly HtmlNode _node;

            public byte[] Data;
            public string Extension;
            public string DataType;

            public IHasId Entity { get; private set; }

            private string Name => Entity == null ? string.Empty : Entity.Id + Extension;

            public ImageData(HtmlNode node)
            {
                _node = node;
            }

            public ImageData BindEntity(IHasId entity)
            {
                Entity = entity;
                return this;
            }

            public void UpdateHtmlSource(string basePath)
            {
                _node.SetAttributeValue("src", basePath + Name);
            }
        }


    }
}
