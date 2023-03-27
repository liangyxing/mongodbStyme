using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.MongoDBLib;
using Yxing.MongoDBSystem.JsonData;

namespace Yxing.MongoDBSystem.Files
{
    public class FileAppServices : MongoDBSystemAppService, IFileAppServices
    {
        MongoDBCRUD Mongo=null;

        public FileAppServices()
        {
            Mongo=new MongoDBCRUD();
        }

        public bool Delete(string collectionName)
        {
            return Mongo.Delete(collectionName);
        }

        public FileStreamResult DownLoad(string collectionName)
        {
            var res= Mongo.DownloadByWeb(collectionName);
            
            return res;
        }

        public List<Dictionary<string, List<Dictionary<string, string>>>> QueryAllColletionInfo()
        {
            var res=Mongo.QueryAllColletionInfo();
            return res;
        }

        public async Task Update(string collectionName, string measuringPointName, List<Dictionary<string,string>> data)
        {
            await Mongo.Update(collectionName, measuringPointName, data);
        }

        public bool UpdateFileInfo(string collectionName, string type, string describe)
        {
            return Mongo.UpdateFileInfo(collectionName, type, describe);
        }

        public async Task<bool> Upload(IFormFile file, string collectionName, string type, string describe)
        {
            return await Mongo.InsertByMongoDB(file, collectionName, type, describe);
        }
    }
}
