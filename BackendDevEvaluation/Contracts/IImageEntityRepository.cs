using BackendDevEvaluation.Models;

namespace BackendDevEvaluation.Contracts
{
    public interface IImageEntityRepository
    {
        byte[] GetImageBase64(string Name);
        void SaveBulkImage(List<ImageEntity> listofImageToSaveInDB);
        Task<List<ImageEntity>> GetAllImage();

    }
}
