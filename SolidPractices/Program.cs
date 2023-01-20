// See https://aka.ms/new-console-template for more information
using SolidPractices.Models;

Console.WriteLine("Hello, World!");


SaleEntity[] dataSources = new SaleEntity[]
{
    new SaleEntity()
    {
        BranchId = 1,
        BranchName = "HCM",
        CategoryId = 1,
        CategoryName = "Small",
        ProductId = 1,
        ProductName = "Car 1",
        Price = 10.5m,
        Vat = 1.5m,
        Year = 2021
    },new SaleEntity()
    {
        BranchId = 1,
        BranchName = "HCM",
        CategoryId = 1,
        CategoryName = "Small",
        ProductId = 2,
        ProductName = "Car 2",
        Price = 10.5m,
        Vat = 1.5m,
        Year = 2021
    },new SaleEntity()
    {
        BranchId = 1,
        BranchName = "HCM",
        CategoryId = 2,
        CategoryName = "Middle",
        ProductId = 1,
        ProductName = "Car 1",
        Price = 15.5m,
        Vat = 1.5m,
        Year = 2021
    },new SaleEntity()
    {
        BranchId = 1,
        BranchName = "HCM",
        CategoryId = 3,
        CategoryName = "Large",
        ProductId = 1,
        ProductName = "Car 1",
        Price = 20.5m,
        Vat = 2.5m,
        Year = 2021
    },new SaleEntity()
    {
        BranchId = 1,
        BranchName = "HCM",
        CategoryId = 1,
        CategoryName = "Small",
        ProductId = 1,
        ProductName = "Car 1",
        Price = 10.5m,
        Vat = 1.5m,
        Year = 2022
    },
    new SaleEntity()
    {
        BranchId = 1,
        BranchName = "HCM",
        CategoryId = 1,
        CategoryName = "Small",
        ProductId = 1,
        ProductName = "Car 1",
        Price = 10.5m,
        Vat = 1.5m,
        Year = 2022
    },new SaleEntity()
    {
        BranchId = 1,
        BranchName = "HCM",
        CategoryId = 1,
        CategoryName = "Small",
        ProductId = 1,
        ProductName = "Car 1",
        Price = 10.5m,
        Vat = 1.5m,
        Year = 2022
    },new SaleEntity()
    {
        BranchId = 1,
        BranchName = "HCM",
        CategoryId = 2,
        CategoryName = "Middle",
        ProductId = 4,
        ProductName = "Car 4",
        Price = 10.5m,
        Vat = 1.5m,
        Year = 2022
    }
    ,new SaleEntity()
    {
        BranchId = 1,
        BranchName = "HCM",
        CategoryId = 2,
        CategoryName = "Middle",
        ProductId = 4,
        ProductName = "Car 4",
        Price = 10.5m,
        Vat = 1.5m,
        Year = 2023
    }
};

var search = new SearchItem()
{
    ReportType = GroupType.Branch,
    Year = 2022,
    VatInclude = true
};

var findTemplate = (GroupType type) =>
{
    if (type == GroupType.Category)
        return new CategoryTemplate();
    return new BranchTemplate();
};
var print = (IEnumerable<ReportItem> reportItems) =>
{
    foreach (var item in reportItems)
    {
        Console.WriteLine(item);
    }
};

Console.WriteLine("Group by branch\n");
var template = findTemplate(search.ReportType);
var generator = new ReportGenerator(search);
print(generator.Generate(dataSources, template));


Console.WriteLine("Group by branch and category\n");
search.ReportType = GroupType.Category;
template = findTemplate(search.ReportType);
generator = new ReportGenerator(search);
print(generator.Generate(dataSources, template));


Console.ReadLine();