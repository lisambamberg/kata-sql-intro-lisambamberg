using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SqlIntro
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetting.json")
#if DEBUG
                .AddJsonFile("appsettings.Debug.json")
#else
                .AddJsonFile($"appsettings.Release.json")
#endif 
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var repo = new DapperProductRepo(connectionString);
            //var repoDapper = new DapperProductRepo(connectionString);

            foreach (var prod in repo.GetProducts())
            {
                Console.WriteLine("Product Name:" + prod.Name + "Comments" + prod.Comments);
            }

            Console.ReadLine();
        }
    }
}
