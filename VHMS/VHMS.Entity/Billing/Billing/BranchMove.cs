using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class BranchMove
    {
        public int BranchMoveID { get; set; }
        public string MoveNo { get; set; }
        public DateTime MoveDate { get; set; }
        public string sMoveDate { get; set; }
        public Branch ToBranch { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalWeight { get; set; }
        public Branch FromBranch { get; set; }
        public string Status { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Collection<BranchMoveTrans> BranchMoveTrans { get; set; }
    }

    public class BranchMoveTrans
    {
        public int BranchMoveTransID { get; set; }
        public int BranchMoveID { get; set; }
        public int StockID { get; set; }
        public int Quantity { get; set; }
        public Stock Stock { get; set; }
        public string StatusFlag { get; set; }      
        public DateTime StockDate { get; set; }
        public string sStockDate { get; set; }
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public decimal NetWeight { get; set; }
        public string Barcode { get; set; }
        public decimal WastagePercent { get; set; }
        public decimal Wastage { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal Ratti { get; set; }
        public decimal PureWeight { get; set; }
        public decimal Lacquer { get; set; }
        public string Making { get; set; }
        public string Design { get; set; }
        public string Karat { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string StoneName { get; set; }
        public decimal StoneWeight { get; set; }
        public int StoneQuantity { get; set; }
        public decimal StoneRate { get; set; }
        public decimal StonePrice { get; set; }
        public string StoneColor { get; set; }
        public string StoneCut { get; set; }
        public string StoneClarity { get; set; }
        public int BranchID { get; set; }
        public string Status { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
    }

    public class BranchMoveFilter
    {
        public int CustomerID { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public int BranchID { get; set; }
        public int BranchMoveID { get; set; }
        public int UserID { get; set; }       
    }
}
