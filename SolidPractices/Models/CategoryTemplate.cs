using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPractices.Models
{
    public class CategoryTemplate : BranchTemplate, ITemplate<CategoryTemplate>
    {
        public int CatId { get; protected set; }
        public string CatName { get; protected set; }

        public virtual bool Equals(CategoryTemplate? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return BranchId == other.BranchId && CatId == other.CatId;
        }

        public override int GetHashCode()
        {
            return BranchId;
        }

        public override Func<SaleEntity, CategoryTemplate> Group => (e) => new CategoryTemplate { Year = e.Year, BranchId = e.BranchId, BranchName = e.BranchName, CatId = e.CategoryId, CatName = e.CategoryName };

        public override Func<IEnumerable<SaleEntity>, ReportItem> Merge => (e) => new ReportItem { Id = e.First().CategoryId, Name = $"{e.First().BranchName}-{e.First().CategoryName}" };
    }

}
