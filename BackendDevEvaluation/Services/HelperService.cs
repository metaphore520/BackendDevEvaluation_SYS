using BackendDevEvaluation.API_Model;
using BackendDevEvaluation.Contracts;
using BackendDevEvaluation.DbContextFile;
using BackendDevEvaluation.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendDevEvaluation.Services
{
    public class HelperService : IHelperService
    {
        private readonly IKeyService _keyService;
        private readonly IImageEntityRepository _ImageEntityRepository;
        private readonly string ImagePath = Path.Combine(Directory.GetCurrentDirectory(), $"DownloadedImages\\");

        public HelperService(IKeyService keyService, IImageEntityRepository ImageEntityRepository)
        {
            _keyService = keyService;
            _ImageEntityRepository = ImageEntityRepository;
        }
        public string GetFilePath()
        {
            return ImagePath;
        }

        public async Task<ResponseDownload> ProcessWebResponse(RequestDownload request)
        {
            ResponseDownload response = new ResponseDownload();
            response.Success = true;
            response.Message = "Images Saved Successfully";
            response.UrlAndNames = new Dictionary<string, string>();
            List<ImageEntity> listOfImage = new List<ImageEntity>();
            Task<HttpResponseMessage>[] taskList = new Task<HttpResponseMessage>[request.MaxDownloadAtOnce];
            HttpClient httpClient = new HttpClient();
            long imageUrlCount = request.ImageUrls.Count();
            int slicedArrayIndex = 0;

            for (int j = 0; j < imageUrlCount; j++)
            {
                for (int i = 0; i < request.MaxDownloadAtOnce; i++)
                {
                    var imageTask = httpClient.GetAsync(request.ImageUrls.ElementAt(j));
                    taskList[i] = imageTask;
                    j++;
                    if (j == imageUrlCount)
                    {
                        break;
                    }
                    slicedArrayIndex = i;
                }
                Task.WaitAll(taskList);
                var listImage = SaveAllImage(taskList.Take(slicedArrayIndex+1).ToArray(), response);
                listOfImage.AddRange(listImage);
                j--;
            }
            SaveBulkImage(listOfImage);
            return response;
        }

        public List<ImageEntity> SaveAllImage(Task<HttpResponseMessage>[] taskList, ResponseDownload response)
        {
            List<ImageEntity> listOfImage = new List<ImageEntity>();
            for (int i = 0; i < taskList.Length; i++)
            {
                ImageEntity image = SaveImage(taskList[i], GetFilePath(), _keyService.GetNextKey());
                listOfImage.Add(image);
                response.UrlAndNames[image.Url] = image.Name;
            }
            return listOfImage;
        }
        public ImageEntity SaveImage(Task<HttpResponseMessage> responseTask, string filePath, long count)
        {
            ImageEntity imageEntity = new ImageEntity();
            imageEntity.Name = count + "-" + "image.png";
            imageEntity.Id = count;
            imageEntity.Url = responseTask.Result.RequestMessage.RequestUri.ToString();

            imageEntity.Data = responseTask.Result.Content.ReadAsByteArrayAsync().Result;
            File.WriteAllBytes(GetFilePath() + imageEntity.Name, imageEntity.Data);
            return imageEntity;
        }


        public void DeleteAllFiles()
        {
            DirectoryInfo di = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "DownloadedImages"));

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public async Task<List<ImageEntity>> GetAllImage()
        {
            return await this._ImageEntityRepository.GetAllImage();
        }

        public void SaveBulkImage(List<ImageEntity> listofImageToSaveInDB)
        {
            this._ImageEntityRepository.SaveBulkImage(listofImageToSaveInDB);
        }
        public byte[] GetImageBase64(string Name)
        {
            return this._ImageEntityRepository.GetImageBase64(Name);
        }

    }
}
