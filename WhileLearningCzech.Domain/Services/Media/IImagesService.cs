using System.Threading.Tasks;
using WhileLearningCzech.Domain.Core.Data;

namespace WhileLearningCzech.Domain.Services.Media
{
    public interface IImagesService
    {
        Task<Image> GetImageById(int id);
    }
}
