using BackendDevEvaluation.Contracts;
using BackendDevEvaluation.DbContextFile;
using BackendDevEvaluation.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendDevEvaluation.Repository
{
    public class ImageEntityRepository : IImageEntityRepository
    {
        private readonly ImageDBContext _context;
        public ImageEntityRepository(ImageDBContext context)
        {
            _context = context;
        }

        public byte[] GetImageBase64(string Name)
        {
            return _context.ImageEntity.FirstOrDefault(x => x.Name == Name)?.Data;
        }

        public void SaveBulkImage(List<ImageEntity> listofImageToSaveInDB)
        {
            _context.AddRange(listofImageToSaveInDB);
            _context.SaveChanges();
        }



        public async Task<List<ImageEntity>> GetAllImage()
        {
            return await _context.ImageEntity.ToListAsync();
        }






    }
}
