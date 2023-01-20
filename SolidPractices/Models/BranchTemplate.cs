using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidPractices.Models
{
    public class BranchTemplate : ITemplate<BranchTemplate>
    {
        public int BranchId { get; protected set; }
        public string BranchName { get; protected set; }
        public int Year { get; protected set; }

        public virtual bool Equals(BranchTemplate? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return BranchId == other.BranchId;
        }

        public override int GetHashCode()
        {
            return BranchId;
        }

        public virtual Func<SaleEntity, BranchTemplate> Group => (e) => new BranchTemplate { Year = e.Year, BranchId = e.BranchId, BranchName = e.BranchName };


        public virtual Func<IEnumerable<SaleEntity>, ReportItem> Merge => (e) => new ReportItem { Id = e.First().BranchId, Name = e.First().BranchName };
    }

}
