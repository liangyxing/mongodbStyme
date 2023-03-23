using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Yxing.MongoDBSystem.Files
{
    public interface  IFileAppServices: IApplicationService
    {
        bool Upload(IFormFile file, string collectionName, string type, string describe);
        FileStreamResult DownLoad(string collectionName);
        Task Update(string collectionName, string measuringPointName, List<Dictionary<string,string>> data);
        bool Delete(string collectionName);
        List<Dictionary<string, List<Dictionary<string, string>>>> QueryAllColletionInfo();

        bool UpdateFileInfo(string collectionName, string type, string describe);



    }
}
