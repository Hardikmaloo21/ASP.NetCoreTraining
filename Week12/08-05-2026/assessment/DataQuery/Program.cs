using System;
using System.Collections.Generic;
using System.Linq;

namespace DataQueryPipeline
{
    // Base Query Class
    public class Query
    {
        // Properties
        protected List<int> dataSource;
        protected bool isExecuted;

        // Constructor
        public Query(List<int> data)
        {
            dataSource = data;
            isExecuted = false;
        }

        // Virtual Methods
        public virtual IEnumerable<int> Apply()
        {
            // Deferred execution
            return dataSource.AsEnumerable();
        }

        public virtual List<int> Execute()
        {
            isExecuted = true;
            return Apply().ToList();
        }

        public virtual string GetQueryType()
        {
            return "Base Query";
        }
    }

    // FilterQuery Class
    public class FilterQuery : Query
    {
        // Additional Properties
        private string predicate;
        private int filteredCount;

        // Constructor
        public FilterQuery(List<int> data, string predicate)
            : base(data)
        {
            this.predicate = predicate;
            filteredCount = 0;
        }

        // Override Apply
        public override IEnumerable<int> Apply()
        {
            // Deferred execution filtering
            if (predicate.StartsWith(">"))
            {
                int value = int.Parse(predicate.Substring(1));
                return dataSource.Where(x => x > value);
            }
            else if (predicate.StartsWith("<"))
            {
                int value = int.Parse(predicate.Substring(1));
                return dataSource.Where(x => x < value);
            }
            else if (predicate.ToLower() == "even")
            {
                return dataSource.Where(x => x % 2 == 0);
            }
            else if (predicate.ToLower() == "odd")
            {
                return dataSource.Where(x => x % 2 != 0);
            }

            return dataSource;
        }

        // Override Execute
        public override List<int> Execute()
        {
            var result = Apply().ToList();
            filteredCount = result.Count;
            isExecuted = true;

            Console.WriteLine($"Filter Executed,Predicate:{predicate},Result Count:{filteredCount}");

            return result;
        }

        // Override GetQueryType
        public override string GetQueryType()
        {
            return "Filter Query";
        }
    }

    // AggregateQuery Class
    public class AggregateQuery : Query
    {
        // Additional Properties
        private string operation;
        private double result;

        // Constructor
        public AggregateQuery(List<int> data, string operation)
            : base(data)
        {
            this.operation = operation;
        }

        // Override Apply
        public override IEnumerable<int> Apply()
        {
            // Deferred execution preparation
            return dataSource.AsEnumerable();
        }

        // Override Execute
        public override List<int> Execute()
        {
            var data = Apply().ToList();

            switch (operation.ToLower())
            {
                case "sum":
                    result = data.Sum();
                    break;

                case "average":
                    result = data.Average();
                    break;

                case "max":
                    result = data.Max();
                    break;

                case "min":
                    result = data.Min();
                    break;

                default:
                    result = 0;
                    break;
            }

            isExecuted = true;

            Console.WriteLine($"Aggregation Executed,Operation:{operation},Result:{result}");

            return data;
        }

        // Override GetQueryType
        public override string GetQueryType()
        {
            return "Aggregate Query";
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            // Input
            string queryType = Console.ReadLine().Trim();

            List<int> data = Console.ReadLine()
                .Split(' ')
                .Select(int.Parse)
                .ToList();

            string condition = Console.ReadLine().Trim();

            Query query;

            // Create Query Object
            if (queryType.Equals("Filter", StringComparison.OrdinalIgnoreCase))
            {
                query = new FilterQuery(data, condition);
            }
            else
            {
                query = new AggregateQuery(data, condition);
            }

            // Execute Query
            query.Execute();
        }
    }
}