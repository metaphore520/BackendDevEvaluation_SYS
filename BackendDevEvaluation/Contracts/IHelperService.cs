using BackendDevEvaluation.API_Model;
using BackendDevEvaluation.Models;

namespace BackendDevEvaluation.Contracts
{
    public interface IHelperService
    {

        Task<ResponseDownload> ProcessWebResponse(RequestDownload request);
        byte[] GetImageBase64(string Name);
        void SaveBulkImage(List<ImageEntity> listofImageToSaveInDB);
        Task<List<ImageEntity>> GetAllImage();
        string GetFilePath();
        List<ImageEntity> SaveAllImage(Task<HttpResponseMessage>[] taskList, ResponseDownload response);

        ImageEntity SaveImage(Task<HttpResponseMessage> responseTask, string filePath, long count);

        void DeleteAllFiles();
    }
}
