using System;

namespace Knuth.Tutorials
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using(var kthApi = new KnuthCsAPI("bch-testnet.cfg"))
                {
                    var memoService = new MemoService(kthApi);
                    kthApi.StartNode();
                    Console.WriteLine("Scraping...");
                    var posts = memoService.GetLatestPosts(5, OnScrapingProgressReport);
                    int i = 0;
                    foreach(string post in posts)
                    {
                        Console.WriteLine((++i) + ": " + post);
                    }
                    Console.WriteLine("Done! Press any key to finish");
                }
                Console.ReadKey();
            }
            catch(Exception e)
            {
                Console.WriteLine("Fatal error: " + e);
            }
        }

        static void OnScrapingProgressReport(string report)
        {
            Console.WriteLine(report);
        }
    }
}
