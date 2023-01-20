using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SolidPractices.Models
{
    enum GroupType
    {
        Branch,
        Category,
        Product
    }

    class SearchItem
    {
        public GroupType ReportType { get; set; }
        public bool VatInclude { get; set; } = false;
        public int Year { get; set; }
    }


    public record ReportItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PrevYearSaleTotal { get; set; }
        public decimal ThisYearSaleTotal { get; set; }
    }

    public class SaleEntity
    {
        public int BranchId { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public string BranchName { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
    }

    interface ITemplate<T> where T : BranchTemplate
    {
        Func<SaleEntity, T> Group { get; }
        Func<IEnumerable<SaleEntity>, ReportItem> Merge { get; }
    }

    internal class ReportGenerator
    {
        private SearchItem search { get; set; }

        public ReportGenerator(SearchItem search)
        {
            this.search = search;
        }

        public virtual decimal GetTotal(decimal price, decimal vat)
        {
            return search.VatInclude ? price + vat : price;
        }

        private ReportItem GroupByYear(IGrouping<BranchTemplate, SaleEntity> groupByTemplate)
        {
            decimal totalPrevYear = 0, 
                    totalThisYear = 0;
            int thisYear = search.Year;
            int prevYear = thisYear - 1;

            foreach (var group in groupByTemplate.GroupBy(e => e.Year))
            {
                if (group.Key == thisYear)
                    totalThisYear = GetTotal(group.Sum(e => e.Price), group.Sum(e => e.Vat));
                else if (group.Key == prevYear)
                    totalPrevYear = GetTotal(group.Sum(e => e.Price), group.Sum(e => e.Vat));
            }

            ReportItem item = groupByTemplate.Key.Merge(groupByTemplate);
            item.PrevYearSaleTotal = totalPrevYear;
            item.ThisYearSaleTotal = totalThisYear;
            return item;
        }

        public IEnumerable<ReportItem> Generate(IEnumerable<SaleEntity> entities, ITemplate<BranchTemplate> template)
        {
            var listReportItems = new Dictionary<BranchTemplate, ReportItem>();
            var grouped = entities.GroupBy(template.Group);
            
            foreach (var group in grouped)
            {
                // Set total for specific year
                listReportItems[group.Key] = GroupByYear(group);
            }

            return listReportItems.Select(e => e.Value);
        }
    }
}
