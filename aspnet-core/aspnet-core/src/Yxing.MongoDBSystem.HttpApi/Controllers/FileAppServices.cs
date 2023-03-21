using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Yxing.MongoDBSystem.Files;

namespace Yxing.MongoDBSystem.Controllers
{
    [RemoteService]
    [Route("/api/[controller]/[action]")]
    [ControllerName("Files")]
    public class FileAppServices : AbpControllerBase, IFileAppServices
    {
        private readonly IFileAppServices fileAppServices;

        public FileAppServices(IFileAppServices fileAppServices)
        {
            this.fileAppServices = fileAppServices;
        }
        [HttpPost]
        public bool Delete(string collectionName)
        {
            return fileAppServices.Delete(collectionName);
        }
        [HttpGet]
        public FileStreamResult DownLoad(string collectionName)
        {
            return fileAppServices.DownLoad(collectionName);
        }
        [HttpGet]
        public List<Dictionary<string, List<Dictionary<string, string>>>> QueryAllColletionInfo()
        {
            return fileAppServices.QueryAllColletionInfo();
        }

        [HttpPost]
        public async Task Update(string collectionName, string measuringPointName, [FromBody] List<Dictionary<string, string>> data)
        {
            fileAppServices.Update(collectionName, measuringPointName, data);
        }

        [HttpPost]
        public bool Upload(IFormFile file, string collectionName, string type, string describe)
        {
            return fileAppServices.Upload(file, collectionName, type, describe);
        }
    }
}
