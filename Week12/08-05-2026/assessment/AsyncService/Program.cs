using System;
using System.Threading.Tasks;

namespace AsyncServiceOrchestration
{
    // Base Class
    public class AsyncService
    {
        // Properties
        protected int requestCount;
        protected long lastResponseTime;

        // Constructor
        public AsyncService()
        {
            requestCount = 0;
            lastResponseTime = 0;
        }

        // Virtual Async Methods
        public virtual async Task<string> FetchDataAsync(string endpoint)
        {
            await Task.Delay(2000);
            return "Base Fetch Completed";
        }

        public virtual async Task<string> GetStatusAsync()
        {
            await Task.Delay(100);
            return $"Requests:{requestCount}";
        }
    }

    // WeatherService Class
    public class WeatherService : AsyncService
    {
        // Additional Properties
        private string city;
        private int temperature;

        // Constructor
        public WeatherService(string city)
        {
            this.city = city;
            this.temperature = 22; // Sample temperature
        }

        // Override FetchDataAsync
        public override async Task<string> FetchDataAsync(string endpoint)
        {
            requestCount++;

            Console.WriteLine($"Weather Fetch Started,{city}");

            // Simulate async delay
            await Task.Delay(2000);

            string result = $"Weather Data Received,{city},{temperature}°C";

            Console.WriteLine(result);

            return result;
        }

        // Override GetStatusAsync
        public override async Task<string> GetStatusAsync()
        {
            await Task.Delay(100);

            string status = $"Weather Service Status,Requests:{requestCount}";

            Console.WriteLine(status);

            return status;
        }
    }

    // StockService Class
    public class StockService : AsyncService
    {
        // Additional Properties
        private string symbol;
        private double currentPrice;

        // Constructor
        public StockService(string symbol)
        {
            this.symbol = symbol;
            this.currentPrice = 145.75; // Sample stock price
        }

        // Override FetchDataAsync
        public override async Task<string> FetchDataAsync(string endpoint)
        {
            requestCount++;

            Console.WriteLine($"Stock Fetch Started,{symbol}");

            // Simulate async delay
            await Task.Delay(2000);

            string result = $"Stock Price Update,{symbol},${currentPrice}";

            Console.WriteLine(result);

            return result;
        }

        // Override GetStatusAsync
        public override async Task<string> GetStatusAsync()
        {
            await Task.Delay(100);

            string status = $"Stock Service Status,Requests:{requestCount}";

            Console.WriteLine(status);

            return status;
        }
    }

    // Main Program
    class Program
    {
        static async Task Main(string[] args)
        {
            // Input
            string serviceType = Console.ReadLine().Trim();
            string identifier = Console.ReadLine().Trim();
            string command = Console.ReadLine().Trim();

            AsyncService service;

            // Create Service Object
            if (serviceType.Equals("Weather", StringComparison.OrdinalIgnoreCase))
            {
                service = new WeatherService(identifier);
            }
            else
            {
                service = new StockService(identifier);
            }

            // Execute Command
            if (command.Equals("FetchDataAsync", StringComparison.OrdinalIgnoreCase))
            {
                await service.FetchDataAsync(identifier);
            }
            else if (command.Equals("GetStatusAsync", StringComparison.OrdinalIgnoreCase))
            {
                await service.GetStatusAsync();
            }
        }
    }
}