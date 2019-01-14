using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WhileLearningCzech.Domain.Core;
using WhileLearningCzech.Domain.Core.Data;

namespace WhileLearningCzech.Domain.Services.Media
{
    public class ImagesService : IImagesService
    {
        private readonly LearningDbContext _db;

        public ImagesService(LearningDbContext db)
        {
            _db = db;
        }

        public async Task<Image> GetImageById(int id)
        {
            return await _db.Images.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
