using Utilities.MongoDBLib;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MongoDBCRUD mongo=new MongoDBCRUD();
            mongo.Update(null,null,null);
            Console.WriteLine("Hello, World!");
        }
    }
}