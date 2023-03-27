using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using Yxing.MongoDBSystem.JsonData;

namespace Utilities.MongoDBLib
{
    public class MongoDBCRUD
    {


        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<BsonDocument> collection;

        public MongoDBCRUD()
        {
            client = new MongoClient("mongodb://root:634889443@47.96.136.34:27017");
            database = client.GetDatabase("FileSystem");

        }

        public async Task<bool>DownloadyCsvHelperAsync(string name, string collectionName)
        {
            this.collection = database.GetCollection<BsonDocument>(collectionName);
            try
            {
                using (var writer = new StreamWriter("data.csv", false, Encoding.UTF8))
                {
                    var cursor = await this.collection.FindAsync(new BsonDocument());
                    var documents = await cursor.ToListAsync();
                    var fieldNames = documents.First().Names.ToList();
                    fieldNames.Remove("_id");
                    writer.WriteLineAsync(string.Join(",", fieldNames));

                    foreach (var document in documents)
                    {
                        var values = fieldNames.Select(fieldName =>
                        {
                            var value = document.GetValue(fieldName, BsonNull.Value);
                            return value == BsonNull.Value ? "" : value.ToString();
                        });
                        await writer.WriteLineAsync(string.Join(",", values));
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool  DownloadCsvHelper(string name, string collectionName)
        {
            this.collection = database.GetCollection<BsonDocument>(collectionName);
            try
            {
                using (var writer = new StreamWriter("data.csv", false, Encoding.UTF8))
                {
                    var cursor = this.collection.Find(new BsonDocument());
                    var documents = cursor.ToList();
                    var fieldNames = documents.First().Names.ToList();
                    fieldNames.Remove("_id");
                    writer.WriteLine(string.Join(",", fieldNames));

                    foreach (var document in documents)
                    {
                        var values = fieldNames.Select(fieldName =>
                        {
                            var value = document.GetValue(fieldName, BsonNull.Value);
                            return value == BsonNull.Value ? "" : value.ToString();
                        });
                        writer.WriteLine(string.Join(",", values));
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public FileStreamResult DownloadByWeb(string collectionName)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);



            MemoryStream stream = new MemoryStream();

            StreamWriter writer = new StreamWriter(stream);

            List<BsonDocument> documents = collection.Find(new BsonDocument()).ToList();
            IEnumerable<string> headers = documents.First().Names;

            // Write headers to CSV file
            writer.WriteLine(string.Join(",", headers));

            // Write data rows to CSV file
            foreach (var document in documents)
            {
                var values = document.Values.Select(v => v.ToString()).ToArray();
                writer.WriteLine(string.Join(",", values));
            }

            writer.Flush();
            stream.Position = 0;
            FileStreamResult content = new FileStreamResult(stream, "text/csv");
            content.FileDownloadName = collectionName;
            return content;

        }

        public bool InsertByMongoDB(IFormFile file, string collectionName, string type, string describe)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    //读取CSV文件头
                    var header = reader.ReadLine().Split(',');

                    //读取每行数据并插入MongoDB数据库
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        //创建键/值对字典并插入MongoDB数据库
                        var document = new Dictionary<string, object>();
                        for (int i = 0; i < header.Length; i++)
                        {
                            document.Add(header[i], values[i]);
                        }
                        collection.InsertOne(new BsonDocument(document));
                    }
                }
                collection.InsertOne(new BsonDocument()
                    {
                            { "type",type },
                            {"describe",describe },
                            {"DateTime",DateTime.Now.ToString()}

                        });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public List<Dictionary<string, List<Dictionary<string, string>>>> QueryAllColletionInfo()
        {
            var res = database.ListCollectionNames().ToList();
            List<Dictionary<string, List<Dictionary<string, string>>>> result = new List<Dictionary<string, List<Dictionary<string, string>>>>();
            foreach (var item in res)
            {
                var collection = database.GetCollection<BsonDocument>(item);
                var filter = Builders<BsonDocument>.Filter.Empty;
                var projection = Builders<BsonDocument>.Projection.Include("type").Include("describe").Include("DateTime");

                var cursor = collection.Find(filter).Project(projection).ToList().Last();
                IEnumerable<BsonElement> resa = cursor.Where(x => x.Name != "_id");
                List<Dictionary<string, string>> temp = new();
                foreach (var items in resa)
                {
                    temp.Add(new Dictionary<string, string>
                    {
                        {items.Name.ToString(),items.Value.ToString()}
                    });
                }

                result.Add(new Dictionary<string, List<Dictionary<string, string>>>
                {
                    {item.ToString(),temp }
                });
            }
            return result;
        }

        public async Task Update(string collectionName, string measuringPointName, List<Dictionary<string, string>> data)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);

            var filter = Builders<BsonDocument>.Filter.Eq("MeasuringPointName", measuringPointName);
            var updateBuilder = Builders<BsonDocument>.Update;
            BsonDocument bsonElements = new BsonDocument();
            List<Dictionary<string, string>> pairs = new List<Dictionary<string, string>>();

            foreach (var items in data)
            {
                foreach (var item in items)
                {
                    bsonElements.Add(item.Key, item.Value);
                }
            }
            var update = updateBuilder.Combine(bsonElements.Select(u => updateBuilder.Set(u.Name, u.Value)));
            var result = collection.UpdateOne(filter, update);

        }
        public bool Delete(string collectionName)
        {
            bool isCollectionExistsBefore = database.ListCollections().ToList().Any(x => x["name"].AsString == collectionName);
            database.DropCollection(collectionName);
            bool isCollectionExistsAfter = database.ListCollections().ToList().Any(x => x["name"].AsString == collectionName);

            if (isCollectionExistsBefore && !isCollectionExistsAfter)
            {
                return true;

            }
            else
            {
                return false;
            }
        }


        public bool UpdateFileInfo(string collectionName, string type ,string describe)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);

            var filter = Builders<BsonDocument>.Filter.Exists("type");
            var update= Builders<BsonDocument>.Update.Set("type",type).Set("describe",describe);

            var result = collection.UpdateOne(filter, update);
            if(result.ModifiedCount>0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }


}
