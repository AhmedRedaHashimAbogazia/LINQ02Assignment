using Solution;

var products = Source.ProductList;

// 1.Get top 3 most expensive products
var top3Products = products
    .OrderByDescending(p => p.UnitPrice)
    .Take(3);

foreach (var product in top3Products)
    Console.WriteLine($"{product.ProductName} - {product.UnitPrice:C}");

Console.WriteLine("============================================");
// 2. show page 2 of products, with page size = 5
int pageNumber = 2;
int pageSize = 5;

var page2 = products
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize);

foreach (var product in page2)
    Console.WriteLine(product.ProductName);

// 3. Take products from the list as long as Their UnitPrice is less than $25(list is ordered by price).
var cheapProducts = products
    .TakeWhile(p => p.UnitPrice < 25);

foreach (var product in cheapProducts)
    Console.WriteLine($"{product.ProductName} - {product.UnitPrice:C}");

// 4.Check if ALL products in the "Seafood" category are in stock
bool allSeafoodInStock = Source.ProductList
    .Where(p => p.Category == "Seafood")
    .All(p => p.UnitsInStock > 0);

Console.WriteLine(allSeafoodInStock);

// 5. Check if the ID list contains 9
int[] ids = [3, 9, 13, 18];

bool contains9 = ids.Contains(9);

Console.WriteLine(contains9);

// 6.Group all products by Category and print each group with its product count.
var groupedProducts = Source.ProductList
    .GroupBy(p => p.Category);

foreach (var group in groupedProducts)
    Console.WriteLine($"{group.Key} : {group.Count()} products");

// 7. Group products by Category and project only product names per group
var groupedNames = Source.ProductList
    .GroupBy(p => p.Category)
    .Select(g => new
    {
        Category = g.Key,
        ProductNames = g.Select(p => p.ProductName)
    });

foreach (var group in groupedNames)
{
    Console.WriteLine(group.Category);

    foreach (var name in group.ProductNames)
        Console.WriteLine($"  {name}");

    Console.WriteLine();
}

//8.Find all categories that have MORE THAN 3 products
var categories = Source.ProductList
    .GroupBy(p => p.Category)
    .Where(g => g.Count() > 3);

foreach (var group in categories)
    Console.WriteLine($"{group.Key} - {group.Count()} products");

//9. Using QUERY SYNTAX, group customers by Country, and for each group select { Country, Count, TotalOrderValue }.
var result =
    from c in Source.CustomerList
    group c by c.Country into g
    select new
    {
        Country = g.Key,
        Count = g.Count(),
        TotalOrderValue = g.Sum(c => c.Orders.Sum(o => o.Total))
    };

foreach (var item in result)
    Console.WriteLine($"{item.Country} | Customers: {item.Count} | Total Orders: {item.TotalOrderValue}");

//10.Calculate the total number of units in stock across all products
int totalUnits = Source.ProductList
    .Sum(p => p.UnitsInStock);

Console.WriteLine(totalUnits);

//11. Find the CHEAPEST and MOST EXPENSIVE product prices
decimal cheapest = Source.ProductList
    .Min(p => p.UnitPrice);

decimal mostExpensive = Source.ProductList
    .Max(p => p.UnitPrice);

Console.WriteLine($"Cheapest: {cheapest}");
Console.WriteLine($"Most Expensive: {mostExpensive}");

// 12. Get a distinct list of all product categories
var distinctCategories = Source.ProductList
    .Select(p => p.Category)
    .Distinct();

foreach (var category in categories)
    Console.WriteLine(category);

// 13. find product IDs that are in setA but NOT in setB
int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
int[] setB = { 3, 6, 9, 12, 15, 13 };

var result2 = setA.Except(setB);

foreach (var id in result2)
    Console.WriteLine(id);

//14.Find countries that appear in list1 but NOT in list2 (case-insensitive).
string[] list1 = { "Germany", "France", "UK", "Spain" };
string[] list2 = { "france", "SPAIN", "Italy" };

var result3 = list1.Except(list2, StringComparer.OrdinalIgnoreCase);

foreach (var country in result3)
    Console.WriteLine(country);

//15.Build a Dictionary<int, Product> keyed by ProductID.Then retrieve and print the product with ID = 18.
var productDictionary = Source.ProductList
    .ToDictionary(p => p.ProductID);

Product product = productDictionary[18];

Console.WriteLine($"{product.ProductID} - {product.ProductName} - {product.UnitPrice}");

//16. Get the first product whose price is greater than $50.
var product2 = Source.ProductList
    .First(p => p.UnitPrice > 50);

Console.WriteLine($"{product2.ProductName} - {product2.UnitPrice}");

//17.Try to get the first product with a price > $500. it returns null instead of throwing.
var product4 = Source.ProductList
    .FirstOrDefault(p => p.UnitPrice > 500);

if (product4 == null)
    Console.WriteLine("No product found.");
else
    Console.WriteLine(product.ProductName);

//18. Generate a multiplication table row for 7
var table = Enumerable.Range(1, 10)
    .Select(x => $"7 x {x} = {7 * x}");

foreach (var item in table)
    Console.WriteLine(item);

//19. Generate even numbers between 1 and 30.
var evenNumbers = Enumerable.Range(1, 30)
    .Where(x => x % 2 == 0);

foreach (var number in evenNumbers)
    Console.WriteLine(number);

//20. Concatenate the first 3 product names with the first 3 customer company names into a single sequence.
var result5 = Source.ProductList
    .Select(p => p.ProductName)
    .Take(3)
    .Concat(
        Source.CustomerList
            .Select(c => c.CompanyName)
            .Take(3));

foreach (var item in result5)
    Console.WriteLine(item);

//21. Pair each product with a customer (by position) and produce a string "ProductName sold to CompanyName".
var result6 = Source.ProductList.Zip(
    Source.CustomerList,
    (product, customer) => $"{product.ProductName} sold to {customer.CompanyName}");

foreach (var item in result6)
    Console.WriteLine(item);