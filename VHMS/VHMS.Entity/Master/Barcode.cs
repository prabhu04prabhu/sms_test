using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHMS.Entity
{
    public class Barcode
    {
        public int BarcodeID { get; set; }
        public DateTime BillDate { get; set; }
        public string sBillDate  { get; set; }
        public Master.Product Product { get; set; }
        public string Barcodes  { get; set; }
        public decimal Quantity  { get; set; }
        public decimal PurchasePrice  { get; set; }
        public string PurchaseNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string sPurchaseDate { get; set; }
        public string BillNo { get; set; }
    }
}
