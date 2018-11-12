using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Run in NuGet console:
    /// Update-Package EnyimMemcached
    ///
    /// Update-Package –reinstall EnyimMemcached
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
            //load configuration at run time
            //var config = new MemcachedClientConfiguration();
            //config.AddServer("localhost:11211");
            //config.AddServer("localhost:11212");  // let's add second server
            //using (var memcachedClient = new MemcachedClient(config))

            // get default configuration from app.config
            // https://github.com/enyim/EnyimMemcached/wiki/MemcachedClient-Configuration
            using (var memcachedClient = new MemcachedClient())
            {
                memcachedClient.Store(Enyim.Caching.Memcached.StoreMode.Set, "checkVar", "blah-blah");

                var testValue = DateTime.Now.ToString();
                Console.WriteLine("Set testValue = " + testValue);

                // save to cache
                var timeSpan = DateTime.Now.AddSeconds(4) - DateTime.Now;
                memcachedClient.Store(Enyim.Caching.Memcached.StoreMode.Set, "testValue", testValue, timeSpan);

                // retrieve
                string value = memcachedClient.Get<string>("testValue");
                Console.WriteLine("retrieved testValue = " + value);

                Thread.Sleep(4000); // sleep 4 sec
                value = memcachedClient.Get<string>("testValue");
                Console.WriteLine("retrieved after 3 sec testValue = " + value);

            }

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }

    // running two memcached servers at one machine:
    //
    // cd "C:\Program Files (x86)\memcached"
    //
    // with daemonize (-d)
    // memcached -p 11211 -d   
    // memcached -p 11212 -d
    //
    // memcached -p 11211 
    // memcached -p 11212 
    //
    // memcached -p 11211 -vvv -m1
    // memcached -p 11212 -vvv -m1
    //
    // telnet localhost 11211
    // telnet localhost 11212

}
