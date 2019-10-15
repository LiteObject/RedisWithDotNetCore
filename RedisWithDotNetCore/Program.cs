namespace RedisWithDotNetCore
{
    using System;
    using System.Diagnostics;

    using StackExchange.Redis;

    /// <summary>
    /// The program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        private static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            // I am running redis instance in docker.
            var option = new ConfigurationOptions
                                              {
                                                  AbortOnConnectFail = false,
                                                  EndPoints = { "127.0.0.1:6379" }
                                              };

            using (var redis = ConnectionMultiplexer.Connect(option))
            {
                // var redis = ConnectionMultiplexer.Connect(option);
                var db = redis.GetDatabase();

                Console.WriteLine($"The numeric identifier of this database {db.Database}");

                db.StringSet("name", "mohammed");
                var nameValue = db.StringGet("name");

                if (nameValue.HasValue)
                {
                    Console.WriteLine($"Name: {nameValue.ToString()}");
                }
            }

            stopWatch.Stop();
            Console.WriteLine($"Press any key to exit. Elapsed: {stopWatch.Elapsed}");
            Console.Read();
        }
    }
}
