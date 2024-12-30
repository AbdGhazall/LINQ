using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQTutorial
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Create dummy data
            List<Product> products = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", Category = "Electronics", Price = 1200.00 },
                new Product { Id = 2, Name = "Smartphone", Category = "Electronics", Price = 800.00 },
                new Product { Id = 3, Name = "Desk", Category = "Furniture", Price = 300.00 },
                new Product { Id = 4, Name = "Chair", Category = "Furniture", Price = 150.00 },
                new Product { Id = 5, Name = "Headphones", Category = "Electronics", Price = 200.00 },
                new Product { Id = 6, Name = "Notebook", Category = "Stationery", Price = 5.00 }
            };

            // WHERE - Filter elements based on a condition
            var electronics = products.Where(p => p.Category == "Electronics");
            Console.WriteLine("WHERE: Electronics:");
            foreach (var item in electronics) Console.WriteLine(item.Name);

            // CONTAINS - Check if a list contains a specific element
            var names = products.Select(p => p.Name).ToList();
            bool containsLaptop = names.Contains("Laptop");
            Console.WriteLine($"CONTAINS: List contains Laptop: {containsLaptop}");

            // ADDRANGE - Add multiple elements to a list
            List<Product> newProducts = new List<Product>
            {
                new Product { Id = 7, Name = "Tablet", Category = "Electronics", Price = 600.00 },
                new Product { Id = 8, Name = "Mouse", Category = "Electronics", Price = 50.00 }
            };
            products.AddRange(newProducts);

            // TOLIST - Convert to a List
            var productList = products.ToList();
            Console.WriteLine("TOLIST: All products:");
            foreach (var item in productList) Console.WriteLine(item.Name);

            // SELECT - Project specific fields
            var productNames = products.Select(p => p.Name);
            Console.WriteLine("SELECT: Product Names:");
            foreach (var name in productNames) Console.WriteLine(name);

            // ORDERBY & ORDERBYDESCENDING - Sort data
            var orderedByPrice = products.OrderBy(p => p.Price);
            var orderedByPriceDesc = products.OrderByDescending(p => p.Price);
            Console.WriteLine("ORDERBY: Products ordered by Price (Ascending):");
            foreach (var item in orderedByPrice) Console.WriteLine($"{item.Name} - {item.Price}");

            Console.WriteLine("ORDERBYDESCENDING: Products ordered by Price (Descending):");
            foreach (var item in orderedByPriceDesc) Console.WriteLine($"{item.Name} - {item.Price}");

            // THENBY - Secondary sort
            var orderedByCategoryAndPrice = products.OrderBy(p => p.Category).ThenBy(p => p.Price);
            Console.WriteLine("THENBY: Ordered by Category then Price:");
            foreach (var item in orderedByCategoryAndPrice) Console.WriteLine($"{item.Category} - {item.Name} - {item.Price}");

            // FIRST, LAST, ELEMENTAT - Access elements
            var firstProduct = products.First();
            var lastProduct = products.Last();
            var elementAt = products.ElementAt(2); // Zero-based index
            Console.WriteLine($"FIRST: {firstProduct.Name}, LAST: {lastProduct.Name}, ELEMENTAT(2): {elementAt.Name}");

            // SINGLEORDEFAULT - Find a single element
            var singleProduct = products.SingleOrDefault(p => p.Name == "Notebook");
            Console.WriteLine($"SINGLEORDEFAULT: {singleProduct?.Name}");

            // COUNT, MAX, AVERAGE, SUM - Aggregates
            var productCount = products.Count();
            var maxPrice = products.Max(p => p.Price);
            var averagePrice = products.Average(p => p.Price);
            var totalPrice = products.Sum(p => p.Price);
            Console.WriteLine($"COUNT: {productCount}, MAX: {maxPrice}, AVERAGE: {averagePrice}, SUM: {totalPrice}");

            // JOIN - Join two collections
            List<string> categories = new List<string> { "Electronics", "Furniture" };
            var joined = products.Join(categories, p => p.Category, c => c, (p, c) => new { p.Name, p.Category });
            Console.WriteLine("JOIN: Products and Categories:");
            foreach (var item in joined) Console.WriteLine($"{item.Name} belongs to {item.Category}");

            // GROUPJOIN - Group join collections
            var groupJoin = categories.GroupJoin(products, c => c, p => p.Category,
                (c, pList) => new { Category = c, Products = pList });
            Console.WriteLine("GROUPJOIN: Categories and their products:");
            foreach (var item in groupJoin)
            {
                Console.WriteLine($"{item.Category}:");
                foreach (var prod in item.Products) Console.WriteLine($"- {prod.Name}");
            }

            // GROUPBY - Group products by category
            var grouped = products.GroupBy(p => p.Category);
            Console.WriteLine("GROUPBY: Products grouped by Category:");
            foreach (var group in grouped)
            {
                Console.WriteLine(group.Key);
                foreach (var prod in group) Console.WriteLine($"- {prod.Name}");
            }

            // RANGE - Generate a sequence of integers
            var numbers = Enumerable.Range(1, 10); // From 1 to 10
            Console.WriteLine("RANGE: Numbers from 1 to 10:");
            foreach (var num in numbers) Console.WriteLine(num);

            // EMPTY - Create an empty sequence
            var emptySequence = Enumerable.Empty<int>();
            Console.WriteLine($"EMPTY: Sequence is empty. Count: {emptySequence.Count()}");

            // REPEAT - Repeat a value
            var repeatedValues = Enumerable.Repeat("Hello", 3);
            Console.WriteLine("REPEAT: Repeated values:");
            foreach (var value in repeatedValues) Console.WriteLine(value);

            // SELECTMANY - Flatten collections
            var categoriesWithProducts = products.GroupBy(p => p.Category)
                .SelectMany(g => g.Select(p => $"{g.Key}: {p.Name}"));
            Console.WriteLine("SELECTMANY: Flattened categories with products:");
            foreach (var item in categoriesWithProducts) Console.WriteLine(item);

            // UNION - Combine distinct elements from two sequences
            var unionExample = new List<int> { 1, 2, 3 }.Union(new List<int> { 3, 4, 5 });
            Console.WriteLine("UNION: Combined distinct elements:");
            foreach (var num in unionExample) Console.WriteLine(num);

            // CONCAT - Combine all elements (including duplicates)
            var concatExample = new List<int> { 1, 2, 3 }.Concat(new List<int> { 3, 4, 5 });
            Console.WriteLine("CONCAT: Combined all elements:");
            foreach (var num in concatExample) Console.WriteLine(num);

            // DISTINCT - Remove duplicates
            var distinctExample = new List<int> { 1, 2, 2, 3, 3, 4 }.Distinct();
            Console.WriteLine("DISTINCT: Distinct elements:");
            foreach (var num in distinctExample) Console.WriteLine(num);

            // EXCEPT - Subtract one sequence from another
            var exceptExample = new List<int> { 1, 2, 3, 4 }.Except(new List<int> { 3, 4 });
            Console.WriteLine("EXCEPT: Subtracted sequence:");
            foreach (var num in exceptExample) Console.WriteLine(num);

            // INTERSECT - Find common elements
            var intersectExample = new List<int> { 1, 2, 3, 4 }.Intersect(new List<int> { 3, 4, 5 });
            Console.WriteLine("INTERSECT: Common elements:");
            foreach (var num in intersectExample) Console.WriteLine(num);

            // ALL - Check if all elements meet a condition
            var allExpensive = products.All(p => p.Price > 100);
            Console.WriteLine($"ALL: All products are expensive: {allExpensive}");

            // ANY - Check if any element meets a condition
            var anyFurniture = products.Any(p => p.Category == "Furniture");
            Console.WriteLine($"ANY: Any furniture products: {anyFurniture}");

            // AGGREGATE - Perform custom aggregation
            var aggregatedNames = products.Select(p => p.Name).Aggregate((current, next) => current + "/ " + next);
            Console.WriteLine($"AGGREGATE: Combined product names: {aggregatedNames}");

            // DEFAULTIFEMPTY - Return a default value if sequence is empty
            var emptyList = new List<Product>();
            var defaultIfEmpty = emptyList.DefaultIfEmpty(new Product { Name = "No Products" });
            Console.WriteLine("DEFAULTIFEMPTY: Default value when list is empty:");
            foreach (var prod in defaultIfEmpty) Console.WriteLine(prod.Name);

            // SKIP - Skip a specific number of elements
            var skipExample = products.Skip(2);
            Console.WriteLine("SKIP: Skipped first 2 products:");
            foreach (var item in skipExample) Console.WriteLine(item.Name);

            // SKIPWHILE - Skip while condition is true
            var skipWhileExample = products.SkipWhile(p => p.Price < 300);
            Console.WriteLine("SKIPWHILE: Skipped products while Price < 300:");
            foreach (var item in skipWhileExample) Console.WriteLine(item.Price);

            // SKIPLAST - Skip a specific number of elements from the end
            var skipLastExample = products.SkipLast(2);
            Console.WriteLine("SKIPLAST: Skipped last 2 products:");
            foreach (var item in skipLastExample) Console.WriteLine(item.Name);

            // TAKE - Take a specific number of elements
            var takeExample = products.Take(3);
            Console.WriteLine("TAKE: Took first 3 products:");
            foreach (var item in takeExample) Console.WriteLine(item.Name);

            // TAKEWHILE - Take while condition is true
            var takeWhileExample = products.TakeWhile(p => p.Price < 1000);
            Console.WriteLine("TAKEWHILE: Took products while Price < 1000:");
            foreach (var item in takeWhileExample) Console.WriteLine(item.Name);

            // TAKELAST - Take a specific number of elements from the end
            var takeLastExample = products.TakeLast(2);
            Console.WriteLine("TAKELAST: Took last 2 products:");
            foreach (var item in takeLastExample) Console.WriteLine(item.Name);

            // REPLACE - Replace an item in the list (custom logic)
            products = products.Select(p => p.Name == "Desk" ? new Product { Id = p.Id, Name = "Office Desk", Category = p.Category, Price = p.Price } : p).ToList();
            Console.WriteLine("REPLACE: Replaced 'Desk' with 'Office Desk':");
            foreach (var item in products) Console.WriteLine(item.Name +" "+item.Price);

            //--------------------------------------------------------------------------------------------------------------------------------------------------------

            List<int> numbers2 = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            // SkipWhile Example
            var skipped = numbers2.SkipWhile(x => x < 4);
            Console.WriteLine("SkipWhile Result: " + string.Join(", ", skipped));

            // TakeWhile Example
            var taken = numbers2.TakeWhile(x => x < 4);
            Console.WriteLine("TakeWhile Result: " + string.Join(", ", taken));


            List<string> names2 = new List<string> { "Alice", "Bob", "Charlie", "David", "Eve" };
            var skipWhileResult = names2.SkipWhile(name => name.StartsWith("A"));
            Console.WriteLine("Skipped Names: "+string.Join("/",skipWhileResult));
            // Skips names starting with "A", so result = { "Bob", "Charlie", "David", "Eve" }

            var takeWhileResult = names2.TakeWhile(name => name.Length <4);
            Console.WriteLine("Take Names: "+string.Join("/",takeWhileResult));
            //It evaluates each name in the names list in order.
            //If the first element("Alice") has a length greater than 4, the method immediately stops and returns an empty collection.
            //when you want to process elements only until the condition breaks

            List<string> names3 = new List<string> { "Bob", "Eve", "Charlie", "David","kek" };
            var takeWhileResult3 = names3.TakeWhile(name => name.Length <= 4).ToList();
            // Takes elements "Bob" and "Eve" because they satisfy the condition initially.
            // Stops at "Charlie" because its length is greater than 4.
            Console.WriteLine("TakeWhile Result: " + string.Join(", ", takeWhileResult3));



        }
    }
}

