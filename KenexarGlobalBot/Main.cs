using System;
using System.Threading.Tasks;

namespace KenexarGlobalBot
{
    class Program
    {
        public static async Task Main(string[] args) 
            => await Startup.RunAsync(args);
    }
}
