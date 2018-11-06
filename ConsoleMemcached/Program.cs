using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace ConsoleMemcached
{
    /// <summary>
    /// test of memcached with Enyim library
    /// http://downloads.northscale.com 
    /// memcached-win64-1.4.4-14.zip
    /// 
    /// run server: 
    /// memcached.exe --vv
    /// 
    /// test server:
    /// telnet localhost 11211
    /// 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            var config = new MemcachedClientConfiguration();
            config.AddServer("localhost:11211");

            using (var client = new MemcachedClient(config))
            {

                var currentTime = DateTime.Now.ToString();
                Console.WriteLine("currentTime = " + currentTime);

                // save to cache
                client.Store(Enyim.Caching.Memcached.StoreMode.Set, "currentTime", currentTime);

                // retrieve
                string value = client.Get<string>("currentTime");
                Console.WriteLine("retrieved currentTime = " + value);

            }


            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");
            Console.ReadKey();

                // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
            }
    }
}
