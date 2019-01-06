using System.IO;
using WhileLearningCzech.Domain.Services.Media;

namespace WhileLearningCzech.Web.Services
{
    public class DirService : IDirService
    {
        public string GetImagesDir()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Content/Images");
            return path;
        }
    }
}
