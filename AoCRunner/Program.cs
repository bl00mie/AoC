using Microsoft.Extensions.Configuration;

namespace AoC
{
    class Program
    {
        static IConfiguration Config = new ConfigurationBuilder().Build();
        static void Main(string[] args)
        {
            InitConfig();
            var di = new DirectoryInfo(Config["inputPath"] ?? "");

        }



        static void InitConfig()
        {
            Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();            
        }
    }

}