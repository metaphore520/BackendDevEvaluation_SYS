using BackendDevEvaluation.API_Model;
using BackendDevEvaluation.Contracts;
using BackendDevEvaluation.DbContextFile;
using BackendDevEvaluation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BackendDevEvaluation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadUploadController : ControllerBase
    {
        private readonly IHelperService _helperService;
        public DownloadUploadController(IHelperService helperService)
        {
            this._helperService = helperService;
        }


       
        
        /// <summary>
        /// downloads all images 
        /// stores in in memory database 
        /// and local file system
        /// </summary>
        /// <param name="request"></param>
        /// <returns>ResponseDownload</returns>

        [HttpPost(Name = "DownloadUpload")]
        [Route("requestDownload")]
        public async Task<ResponseDownload> DownloadUpload(RequestDownload request)
        {
            ResponseDownload response = await this._helperService.ProcessWebResponse(request);
            return response;
        }



      
        
        /// <summary>
        /// get all saved image from in memory database 
        /// EF Core Code first is used
        /// </summary>
        /// <returns>List<ImageEntity></returns>

        [HttpGet(Name = "GetAllImage")]
        [Route("get-all-image")]
        public async Task<List<ImageEntity>> GetAllImage()
        {
            return await this._helperService.GetAllImage();
        }






        /// <summary>
        /// returns base 64 string of image by name
        /// </summary>
        /// <param name="image_name"></param>
        /// <returns>byte[]</returns>

        [HttpGet(Name = "GetImageByName")] 
        [Route("get-image-by-name/{image_name}")]
        public byte[] GetImageByName(string image_name)
        {
            return this._helperService.GetImageBase64(image_name);
        }



    }
}