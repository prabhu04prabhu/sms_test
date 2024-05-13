using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace VHMS.Entity.Billing
{
    public class PurchaseReturnJSON
    {
        public string Version { get; set; }
        public PurchaseReturnTransDetails TranDtls { get; set; }
        public PurchaseReturnDocumentDetails DocDtls { get; set; }
        public PurchaseReturnSellerDetails SellerDtls { get; set; }
        public PurchaseReturnBuyerDetails BuyerDtls { get; set; }
        public PurchaseReturnShippingDetails ShipDtls { get; set; }
        public PurchaseReturnValueDetails ValDtls { get; set; }
        public string PayDtls { get; set; }
        public string RefDtls { get; set; }
        public Collection<PurchaseReturnAdditionalDetails> ReturnAddlDocDtls { get; set; }
        public Collection<PurchaseReturnItemDetails> ItemList { get; set; }

    }

    public class PurchaseReturnTransDetails
    {
        public string TaxSch { get; set; }
        public string SupTyp { get; set; }
        public string IgstOnIntra { get; set; }
        public string RegRev { get; set; }
        public string EcmGstin { get; set; }
    }
    public class PurchaseReturnDocumentDetails
    {
        public string Typ { get; set; }
        public string No { get; set; }
        public string Dt { get; set; }
       
    }
    public class PurchaseReturnSellerDetails
    {
        public string Gstin { get; set; }
        public string LglNm { get; set; }
        public string TrdNm { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public int Pin { get; set; }
        public string Stcd { get; set; }
        public string Ph { get; set; }
        public string Em { get; set; }
    }
    public class PurchaseReturnBuyerDetails
    {
        public string Gstin { get; set; }
        public string LglNm { get; set; }
        public string TrdNm { get; set; }
        public string Pos { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public int Pin { get; set; }
        public string Stcd { get; set; }
        public string Ph { get; set; }
        public string Em { get; set; }
    }
    public class PurchaseReturnShippingDetails
    {
        public string Gstin { get; set; }
        public string LglNm { get; set; }
        public string TrdNm { get; set; }
        public string Addr1 { get; set; }
        public string Addr2 { get; set; }
        public string Loc { get; set; }
        public int Pin { get; set; }
        public string Stcd { get; set; }
    }
    public class PurchaseReturnValueDetails
    {
        public decimal AssVal { get; set; }
        public decimal IgstVal { get; set; }
        public decimal CgstVal { get; set; }
        public decimal SgstVal { get; set; }
        public decimal CesVal { get; set; }
        public decimal StCesVal { get; set; }
        public decimal Discount { get; set; }
        public decimal OthChrg { get; set; }
        public decimal RndOffAmt { get; set; }
        public decimal TotInvVal { get; set; }
        public decimal TotInvValFc { get; set; }
    }
    public class PurchaseReturnAdditionalDetails
    {
        public string Url { get; set; }
        public string Docs { get; set; }
        public string Info { get; set; }       
    }
    public class PurchaseReturnItemDetails
    {
        public string SlNo { get; set; }
        public string PrdDesc { get; set; }
        public string IsServc { get; set; }
        public string HsnCd { get; set; }
        public decimal Qty { get; set; }
        public string Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotAmt { get; set; }
        public decimal Discount { get; set; }
        public decimal PreTaxVal { get; set; }
        public decimal AssAmt { get; set; }
        public decimal GstRt { get; set; }
        public decimal IgstAmt { get; set; }
        public decimal CgstAmt { get; set; }
        public decimal SgstAmt { get; set; }
        public decimal CesRt { get; set; }
        public decimal CesAmt { get; set; }
        public decimal CesNonAdvlAmt { get; set; }
        public decimal StateCesRt { get; set; }
        public decimal StateCesAmt { get; set; }
        public decimal StateCesNonAdvlAmt { get; set; }
        public decimal OthChrg { get; set; }
        public decimal TotItemVal { get; set; }
    }
}
