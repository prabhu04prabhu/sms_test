using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;

namespace VHMS.DataAccess.Billing
{
    public class SalesReturn
    {
        public static Collection<Entity.Billing.SalesReturn> GetSalesReturn(int ipatientID = 0,int FK_FinancialYearID = 0, int IsRetail = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesReturn> objList = new Collection<Entity.Billing.SalesReturn>();
            Entity.Billing.SalesReturn objSalesReturn;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESRETURN);
                db.AddInParameter(cmd, "@PK_SalesReturnID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Int32, IsRetail);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesReturn = new Entity.Billing.SalesReturn();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objSalesReturn.SalesReturnID = Convert.ToInt32(drData["PK_SalesReturnID"]);
                        objSalesReturn.ReturnNo = Convert.ToString(drData["ReturnNo"]);
                        objSalesReturn.ReturnDate = Convert.ToDateTime(drData["ReturnDate"]);
                        objSalesReturn.sReturnDate = objSalesReturn.ReturnDate.ToString("dd/MM/yyyy");
                        objSalesReturn.OldInvoiceNo = Convert.ToString(drData["OldInvoiceNo"]);
                        objSalesReturn.BillNo = Convert.ToString(drData["BillNo"]);
                        objSalesReturn.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSalesReturn.sBillDate = objSalesReturn.BillDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objSalesReturn.Customer = objCustomer;
                      
                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSalesReturn.Tax = objTax;
                        objSalesReturn.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesReturn.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesReturn.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesReturn.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesReturn.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesReturn.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesReturn.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objSalesReturn.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesReturn.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesReturn.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesReturn.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objSalesReturn.InvoiceBillType = Convert.ToBoolean(drData["Invoice_BillType"]);
                        objSalesReturn.CustomerBillType = Convert.ToBoolean(drData["Customer_BillType"]);
                        objSalesReturn.Roundoff = Convert.ToDecimal(drData["RoundOff"]);
                        objSalesReturn.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesReturn.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesReturn.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesReturn.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesReturn.ACK_No = Convert.ToString(drData["ACK_No"]);
                        objSalesReturn.IRN_No = Convert.ToString(drData["IRN_No"]);

                        objList.Add(objSalesReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesReturn GetSalesReturn | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Billing.SalesReturnJSON GetSalesReturnJsonHSNFormat(int iSalesEntryID)
        {
            string sException = string.Empty;
            Database db;

            Entity.Billing.SalesReturnJSON objSalesJSON = new Entity.Billing.SalesReturnJSON();
            Entity.Billing.ReturnTransDetails objTransDetails;
            Entity.Billing.ReturnDocumentDetails objDocumentDetails; Entity.Billing.ReturnSellerDetails objSellerDetails;
            Entity.Billing.ReturnBuyerDetails objBuyerDetails; Entity.Billing.ReturnShippingDetails objShippingDetails;
            Entity.Billing.ReturnValueDetails objValueDetails;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESRETURNJSONHSN);
                db.AddInParameter(cmd, "@PK_SalesReturnID", DbType.Int32, iSalesEntryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesJSON = new Entity.Billing.SalesReturnJSON();
                        objTransDetails = new Entity.Billing.ReturnTransDetails();
                        objDocumentDetails = new Entity.Billing.ReturnDocumentDetails();
                        objSellerDetails = new Entity.Billing.ReturnSellerDetails();
                        objBuyerDetails = new Entity.Billing.ReturnBuyerDetails();
                        objShippingDetails = new Entity.Billing.ReturnShippingDetails();
                        objValueDetails = new Entity.Billing.ReturnValueDetails();

                        objSalesJSON.Version = "1.1";

                        //Trans Details
                        objTransDetails.TaxSch = "GST";
                        objTransDetails.SupTyp = "B2B";
                        objTransDetails.IgstOnIntra = "N";
                        objTransDetails.RegRev = "N";
                        objTransDetails.EcmGstin = null;
                        objSalesJSON.TranDtls = objTransDetails;

                        //Document Details
                        objDocumentDetails.Typ = "CRN";
                        objDocumentDetails.No = Convert.ToString(drData["ReturnNo"]);
                        objDocumentDetails.Dt = Convert.ToDateTime(drData["ReturnDate"]).ToString("dd/MM/yyyy");
                        objSalesJSON.DocDtls = objDocumentDetails;

                        //Seller Details
                        foreach (DataRow drCompany in dsList.Tables[2].Rows)
                        {
                            objSellerDetails.Gstin = Convert.ToString(drCompany["CSTNo"]);
                            objSellerDetails.LglNm = Convert.ToString(drCompany["CompanyName"]);
                            objSellerDetails.TrdNm = Convert.ToString(drCompany["CompanyName"]);
                            objSellerDetails.Addr1 = Convert.ToString(drCompany["CompanyAddress"]);
                            objSellerDetails.Addr2 = null;
                            objSellerDetails.Loc = "OMALUR";
                            objSellerDetails.Pin = 636455;
                            objSellerDetails.Stcd = "33";
                            string iContact = Convert.ToString(drCompany["ContactNo"]);
                            objSellerDetails.Ph = iContact.Substring(iContact.Length - 10);
                            objSellerDetails.Em = Convert.ToString(drCompany["Email"]);
                            objSalesJSON.SellerDtls = objSellerDetails;
                        }

                        // Buyer Details
                        objBuyerDetails.Gstin = Convert.ToString(drData["GSTNo"]);
                        objBuyerDetails.LglNm = Convert.ToString(drData["CustomerName"]);
                        objBuyerDetails.TrdNm = Convert.ToString(drData["CustomerName"]);
                        objBuyerDetails.Pos = Convert.ToString(drData["StateCode"]);

                        string message = Convert.ToString(drData["Address"]);
                        string firstline = message;
                        string secondline = null;
                        if (message.Length > 100)
                        {
                            for (int i = 100; i > 0;)
                            {
                                if (message[i] == ' ')
                                {
                                    firstline = message.Substring(0, i);
                                    secondline = message.Substring(i + 1);
                                    break;
                                }
                                else
                                {
                                    i--;
                                }
                            }
                        }
                        objBuyerDetails.Addr1 = firstline; ;
                        objBuyerDetails.Addr2 = secondline;
                        objBuyerDetails.Loc = Convert.ToString(drData["City"]);
                        objBuyerDetails.Pin = Convert.ToInt32(drData["Pincode"]);
                        objBuyerDetails.Stcd = Convert.ToString(drData["StateCode"]);
                        string iBuyerMobile = Convert.ToString(drData["MobileNo"]);
                        objBuyerDetails.Ph = iBuyerMobile.Length > 0 ? iBuyerMobile.Substring(iBuyerMobile.Length - 10) : null;
                        //objBuyerDetails.Ph = Convert.ToString(drData["MobileNo"]);
                        objBuyerDetails.Em = Convert.ToString(drData["Email"]).Length > 0 ? Convert.ToString(drData["Email"]) : null;
                        objSalesJSON.BuyerDtls = objBuyerDetails;

                        // Shipping Details
                        objShippingDetails.Gstin = Convert.ToString(drData["ShippingGSTIN"]);
                        objShippingDetails.LglNm = Convert.ToString(drData["CustomerName"]);
                        objShippingDetails.TrdNm = Convert.ToString(drData["CustomerName"]);

                        string s_message = Convert.ToString(drData["ShippingAddress"]);
                        string s_firstline = s_message;
                        string s_secondline = null;
                        if (s_message.Length > 100)
                        {
                            for (int i = 100; i > 0;)
                            {
                                if (s_message[i] == ' ')
                                {
                                    s_firstline = s_message.Substring(0, i);
                                    s_secondline = s_message.Substring(i + 1);
                                    break;
                                }
                                else
                                {
                                    i--;
                                }
                            }
                        }
                        objShippingDetails.Addr1 = s_firstline;
                        objShippingDetails.Addr2 = s_secondline;
                        objShippingDetails.Loc = Convert.ToString(drData["ShippingBranch"]);
                        objShippingDetails.Pin = Convert.ToInt32(drData["Pincode"]);
                        objShippingDetails.Stcd = Convert.ToString(drData["StateCode"]);
                        objSalesJSON.ShipDtls = objShippingDetails;

                        // Value Details
                        objValueDetails.AssVal = Convert.ToDecimal(drData["TotalAmount"]);
                        objValueDetails.IgstVal = Convert.ToDecimal(drData["IGSTAmount"]);
                        objValueDetails.CgstVal = Convert.ToDecimal(drData["CGSTAmount"]);
                        objValueDetails.SgstVal = Convert.ToDecimal(drData["SGSTAmount"]);
                        objValueDetails.CesVal = 0;
                        objValueDetails.StCesVal = 0;
                        objValueDetails.Discount = 0;
                        objValueDetails.OthChrg = 0;// Convert.ToDecimal(drData["OtherCharges"]);
                        objValueDetails.RndOffAmt = Convert.ToDecimal(drData["Roundoff"]);
                        objValueDetails.TotInvVal = Convert.ToDecimal(drData["ReturnAmount"]);
                        objValueDetails.TotInvValFc = 0;
                        objSalesJSON.ValDtls = objValueDetails;

                        objSalesJSON.PayDtls = null;
                        objSalesJSON.RefDtls = null;

                        Collection<Entity.Billing.ReturnAdditionalDetails> objAddCollection = new Collection<Entity.Billing.ReturnAdditionalDetails>();
                        Entity.Billing.ReturnAdditionalDetails objAdditional = new Entity.Billing.ReturnAdditionalDetails();
                        objAdditional.Url = null;
                        objAdditional.Docs = null;
                        objAdditional.Info = null;
                        objAddCollection.Add(objAdditional);
                        objSalesJSON.ReturnAddlDocDtls = objAddCollection;


                        Collection<Entity.Billing.ReturnItemDetails> objItemList = new Collection<Entity.Billing.ReturnItemDetails>();
                        Entity.Billing.ReturnItemDetails objItemDetails;
                        int cnt_val = 1;
                        foreach (DataRow drTrans in dsList.Tables[1].Rows)
                        {
                            objItemDetails = new Entity.Billing.ReturnItemDetails();
                            objItemDetails.SlNo = cnt_val.ToString();
                            objItemDetails.PrdDesc = Convert.ToString(drTrans["ProductName"]);
                            objItemDetails.IsServc = "N";
                            objItemDetails.PreTaxVal = Convert.ToDecimal(drTrans["SubTotal"]);
                            objItemDetails.AssAmt = Convert.ToDecimal(drTrans["SubTotal"]);
                            objItemDetails.HsnCd = Convert.ToString(drTrans["HSNCode"]).Length > 0 ? Convert.ToString(drTrans["HSNCode"]) : null;
                            objItemDetails.Qty = Convert.ToDecimal(drTrans["Quantity"]);
                            objItemDetails.Unit = Convert.ToString(drTrans["UnitName"]);
                            objItemDetails.Discount = Convert.ToDecimal(drTrans["DiscountAmount"]);
                            objItemDetails.TotAmt = Convert.ToDecimal((objItemDetails.AssAmt + objItemDetails.Discount).ToString("0.00"));
                            objItemDetails.UnitPrice = Convert.ToDecimal((objItemDetails.TotAmt / objItemDetails.Qty).ToString("0.00"));


                            objItemDetails.GstRt = Convert.ToDecimal(drTrans["TaxPercent"]);
                            objItemDetails.IgstAmt = Convert.ToDecimal(drTrans["IGSTAmount"]);
                            objItemDetails.CgstAmt = Convert.ToDecimal(drTrans["CGSTAmount"]);
                            objItemDetails.SgstAmt = Convert.ToDecimal(drTrans["SGSTAmount"]);
                            objItemDetails.CesRt = 0;
                            objItemDetails.CesAmt = 0;
                            objItemDetails.CesNonAdvlAmt = 0;
                            objItemDetails.StateCesRt = 0;
                            objItemDetails.StateCesAmt = 0;
                            objItemDetails.StateCesNonAdvlAmt = 0;
                            objItemDetails.OthChrg = 0;
                            objItemDetails.TotItemVal = objItemDetails.AssAmt + objItemDetails.IgstAmt + objItemDetails.CgstAmt + objItemDetails.SgstAmt;
                            cnt_val++;

                            objItemList.Add(objItemDetails);
                        }
                        objSalesJSON.ItemList = objItemList;
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesEntry GetSalesEntryByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesJSON;
        }
        public static Collection<Entity.Billing.SalesReturn> GetTopSalesReturn(int ipatientID = 0, int iBranchID=0, int FK_FinancialYearID = 0, int IsRetail = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesReturn> objList = new Collection<Entity.Billing.SalesReturn>();
            Entity.Billing.SalesReturn objSalesReturn;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {

                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPSALESRETURN);
                db.AddInParameter(cmd, "@PK_SalesReturnID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Int32, IsRetail);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesReturn = new Entity.Billing.SalesReturn();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objSalesReturn.SalesReturnID = Convert.ToInt32(drData["PK_SalesReturnID"]);
                        objSalesReturn.ReturnNo = Convert.ToString(drData["ReturnNo"]);
                        objSalesReturn.ReturnDate = Convert.ToDateTime(drData["ReturnDate"]);
                        objSalesReturn.sReturnDate = objSalesReturn.ReturnDate.ToString("dd/MM/yyyy");

                        objSalesReturn.BillNo = Convert.ToString(drData["BillNo"]);
                        objSalesReturn.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSalesReturn.sBillDate = objSalesReturn.BillDate.ToString("dd/MM/yyyy");
                        objSalesReturn.OldInvoiceNo = Convert.ToString(drData["OldInvoiceNo"]);
                        objSalesReturn.Comments = Convert.ToString(drData["Comments"]);

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objSalesReturn.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSalesReturn.Tax = objTax;
                        objSalesReturn.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesReturn.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesReturn.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesReturn.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesReturn.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesReturn.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesReturn.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesReturn.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objSalesReturn.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesReturn.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesReturn.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objSalesReturn.InvoiceBillType = Convert.ToBoolean(drData["Invoice_BillType"]);
                        objSalesReturn.CustomerBillType = Convert.ToBoolean(drData["Customer_BillType"]);
                        objSalesReturn.Roundoff = Convert.ToDecimal(drData["RoundOff"]);
                        objSalesReturn.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesReturn.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesReturn.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesReturn.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesReturn.ACK_No = Convert.ToString(drData["ACK_No"]);
                        objSalesReturn.IRN_No = Convert.ToString(drData["IRN_No"]);

                        objList.Add(objSalesReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesReturn GetSalesReturn | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.SalesReturn> GetSalesReturnID(int ipatientID = 0, int FK_FinancialYearID = 0, int IsRetail = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesReturn> objList = new Collection<Entity.Billing.SalesReturn>();
            Entity.Billing.SalesReturn objSalesReturn;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESRETURN);
                db.AddInParameter(cmd, "@PK_SalesReturnID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Int32, IsRetail);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesReturn = new Entity.Billing.SalesReturn();

                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objSalesReturn.SalesReturnID = Convert.ToInt32(drData["PK_SalesReturnID"]);
                        objSalesReturn.ReturnNo = Convert.ToString(drData["ReturnNo"]);
                        objSalesReturn.ReturnDate = Convert.ToDateTime(drData["ReturnDate"]);
                        objSalesReturn.sReturnDate = objSalesReturn.ReturnDate.ToString("dd/MM/yyyy");
                        objSalesReturn.BillNo = Convert.ToString(drData["BillNo"]);
                        objSalesReturn.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSalesReturn.sBillDate = objSalesReturn.BillDate.ToString("dd/MM/yyyy");
                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objSalesReturn.Customer = objCustomer;
                        objSalesReturn.OldInvoiceNo = Convert.ToString(drData["OldInvoiceNo"]);

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSalesReturn.Tax = objTax;
                        objSalesReturn.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesReturn.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesReturn.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesReturn.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesReturn.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesReturn.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objSalesReturn.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesReturn.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesReturn.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesReturn.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesReturn.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objSalesReturn.InvoiceBillType = Convert.ToBoolean(drData["Invoice_BillType"]);
                        objSalesReturn.CustomerBillType = Convert.ToBoolean(drData["Customer_BillType"]);
                        objSalesReturn.Roundoff = Convert.ToDecimal(drData["RoundOff"]);
                        objSalesReturn.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesReturn.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesReturn.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesReturn.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesReturn.ACK_No = Convert.ToString(drData["ACK_No"]);
                        objSalesReturn.IRN_No = Convert.ToString(drData["IRN_No"]);

                        objList.Add(objSalesReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesReturn GetSalesReturn | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.SalesReturn> SearchSalesReturn(string ID, int iBranchID, int FK_FinancialYearID = 0, int IsRetail = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesReturn> objList = new Collection<Entity.Billing.SalesReturn>();
            Entity.Billing.SalesReturn objSalesReturn;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_SALESRETURN);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Int32, IsRetail);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesReturn = new Entity.Billing.SalesReturn();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objSalesReturn.SalesReturnID = Convert.ToInt32(drData["PK_SalesReturnID"]);
                        objSalesReturn.ReturnNo = Convert.ToString(drData["ReturnNo"]);
                        objSalesReturn.ReturnDate = Convert.ToDateTime(drData["ReturnDate"]);
                        objSalesReturn.sReturnDate = objSalesReturn.ReturnDate.ToString("dd/MM/yyyy");

                        objSalesReturn.BillNo = Convert.ToString(drData["BillNo"]);
                        objSalesReturn.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSalesReturn.sBillDate = objSalesReturn.BillDate.ToString("dd/MM/yyyy");
                        objSalesReturn.OldInvoiceNo = Convert.ToString(drData["OldInvoiceNo"]);

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objSalesReturn.Customer = objCustomer;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSalesReturn.Tax = objTax;
                        objSalesReturn.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesReturn.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesReturn.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesReturn.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesReturn.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesReturn.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesReturn.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesReturn.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesReturn.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesReturn.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objSalesReturn.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objSalesReturn.InvoiceBillType = Convert.ToBoolean(drData["Invoice_BillType"]);
                        objSalesReturn.CustomerBillType = Convert.ToBoolean(drData["Customer_BillType"]);
                        objSalesReturn.Roundoff = Convert.ToDecimal(drData["RoundOff"]);
                        objSalesReturn.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesReturn.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesReturn.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesReturn.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesReturn.ACK_No = Convert.ToString(drData["ACK_No"]);
                        objSalesReturn.IRN_No = Convert.ToString(drData["IRN_No"]);

                        objList.Add(objSalesReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesReturn GetSalesReturn | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        
        public static Entity.Billing.SalesReturn GetSalesReturnByID(int iSalesReturnID, int FK_FinancialYearID = 0, int IsRetail = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.SalesReturn objSalesReturn = new Entity.Billing.SalesReturn();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESRETURN);
                db.AddInParameter(cmd, "@PK_SalesReturnID", DbType.Int32, iSalesReturnID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Int32, IsRetail);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesReturn = new Entity.Billing.SalesReturn();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objSalesReturn.SalesReturnID = Convert.ToInt32(drData["PK_SalesReturnID"]);
                        objSalesReturn.ReturnNo = Convert.ToString(drData["ReturnNo"]);
                        objSalesReturn.ReturnDate = Convert.ToDateTime(drData["ReturnDate"]);
                        objSalesReturn.sReturnDate = objSalesReturn.ReturnDate.ToString("dd/MM/yyyy");

                        objSalesReturn.BillNo = Convert.ToString(drData["BillNo"]);
                        objSalesReturn.Comments = Convert.ToString(drData["Comments"]);
                        objSalesReturn.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSalesReturn.sBillDate = objSalesReturn.BillDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesReturn.Customer = objCustomer;
                        objSalesReturn.OldInvoiceNo = Convert.ToString(drData["OldInvoiceNo"]);

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSalesReturn.Tax = objTax;
                        objSalesReturn.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesReturn.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesReturn.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesReturn.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesReturn.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesReturn.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objSalesReturn.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesReturn.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesReturn.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesReturn.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesReturn.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objSalesReturn.InvoiceBillType = Convert.ToBoolean(drData["Invoice_BillType"]);
                        objSalesReturn.CustomerBillType = Convert.ToBoolean(drData["Customer_BillType"]);
                        objSalesReturn.Roundoff = Convert.ToDecimal(drData["RoundOff"]);
                        objSalesReturn.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objSalesReturn.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objSalesReturn.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objSalesReturn.IsRetailBill = Convert.ToBoolean(drData["IsRetailBill"]);
                        objSalesReturn.ACK_No = Convert.ToString(drData["ACK_No"]);
                        objSalesReturn.IRN_No = Convert.ToString(drData["IRN_No"]);

                        objSalesReturn.SalesReturnTrans = SalesReturnTrans.GetSalesReturnTransBySalesReturnID(objSalesReturn.SalesReturnID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesReturn GetSalesReturnByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesReturn;
        }

        public static Entity.Billing.SalesReturn GetSalesReturnByReturn(string ReturnNo, int FK_FinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.SalesReturn objSalesReturn = new Entity.Billing.SalesReturn();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESRETURNINVOICE);
                db.AddInParameter(cmd, "@ReturnNo", DbType.String, ReturnNo);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, FK_FinancialYearID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesReturn = new Entity.Billing.SalesReturn();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();

                        objSalesReturn.SalesReturnID = Convert.ToInt32(drData["PK_SalesReturnID"]);
                        objSalesReturn.ReturnNo = Convert.ToString(drData["ReturnNo"]);
                        objSalesReturn.ReturnDate = Convert.ToDateTime(drData["ReturnDate"]);
                        objSalesReturn.sReturnDate = objSalesReturn.ReturnDate.ToString("dd/MM/yyyy");

                        objSalesReturn.BillNo = Convert.ToString(drData["BillNo"]);
                        objSalesReturn.BillDate = Convert.ToDateTime(drData["BillDate"]);
                        objSalesReturn.sBillDate = objSalesReturn.BillDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objSalesReturn.Customer = objCustomer;
                        objSalesReturn.OldInvoiceNo = Convert.ToString(drData["OldInvoiceNo"]);

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objSalesReturn.Tax = objTax;
                        objSalesReturn.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesReturn.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesReturn.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objSalesReturn.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesReturn.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesReturn.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesReturn.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objSalesReturn.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objSalesReturn.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesReturn.ReturnAmount = Convert.ToDecimal(drData["ReturnAmount"]);
                        objSalesReturn.Roundoff = Convert.ToDecimal(drData["RoundOff"]);

                        objSalesReturn.SalesReturnTrans = SalesReturnTrans.GetSalesReturnTransBySalesReturnID(objSalesReturn.SalesReturnID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesReturn GetSalesReturnByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objSalesReturn;
        }
        public static int AddSalesReturn(Entity.Billing.SalesReturn objSalesReturn)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SALESRETURN);
                db.AddOutParameter(cmd, "@PK_SalesReturnID", DbType.Int32, objSalesReturn.SalesReturnID);
                db.AddInParameter(cmd, "@ReturnNo", DbType.String, objSalesReturn.ReturnNo);
                db.AddInParameter(cmd, "@ReturnDate", DbType.String, objSalesReturn.sReturnDate);
                db.AddInParameter(cmd, "@BillNo", DbType.String, objSalesReturn.BillNo);
                db.AddInParameter(cmd, "@OldInvoiceNo", DbType.String, objSalesReturn.OldInvoiceNo);
                db.AddInParameter(cmd, "@BillDate", DbType.String, objSalesReturn.sBillDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objSalesReturn.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objSalesReturn.SalesEntryID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objSalesReturn.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSalesReturn.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSalesReturn.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSalesReturn.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSalesReturn.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesReturn.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objSalesReturn.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objSalesReturn.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesReturn.DiscountAmount);
                db.AddInParameter(cmd, "@Invoice_BillType", DbType.Boolean, objSalesReturn.InvoiceBillType);
                db.AddInParameter(cmd, "@Customer_BillType", DbType.Boolean, objSalesReturn.CustomerBillType);
                db.AddInParameter(cmd, "@RoundOff", DbType.Decimal, objSalesReturn.Roundoff);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objSalesReturn.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objSalesReturn.ImagePath2);
                db.AddInParameter(cmd, "@Comments", DbType.String, objSalesReturn.Comments);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objSalesReturn.ImagePath3);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, objSalesReturn.IsRetailBill);

                db.AddInParameter(cmd, "@ReturnAmount", DbType.Decimal, objSalesReturn.ReturnAmount);               
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objSalesReturn.CreatedBy.UserID);
                db.AddInParameter(cmd, "@ACK_No", DbType.String, objSalesReturn.ACK_No);
                db.AddInParameter(cmd, "@IRN_No", DbType.String, objSalesReturn.IRN_No);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objSalesReturn.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_SalesReturnID"));

                foreach (Entity.Billing.SalesReturnTrans ObjSalesReturnTrans in objSalesReturn.SalesReturnTrans)
                {
                    ObjSalesReturnTrans.SalesReturnID = iID;
                    if (objSalesReturn.IsRetailBill == false)
                        ObjSalesReturnTrans.SalesEntry.SalesEntryID = objSalesReturn.SalesEntryID;
                }

                    SalesReturnTrans.SaveSalesReturnTransaction(objSalesReturn.SalesReturnTrans);
            }
            catch (Exception ex)
            {
                sException = "SalesReturn AddSalesReturn | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSalesReturn(Entity.Billing.SalesReturn objSalesReturn)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SALESRETURN);
                db.AddInParameter(cmd, "@PK_SalesReturnID", DbType.Int32, objSalesReturn.SalesReturnID);
                db.AddInParameter(cmd, "@ReturnDate", DbType.String, objSalesReturn.sReturnDate);
                db.AddInParameter(cmd, "@BillNo", DbType.String, objSalesReturn.BillNo);
                db.AddInParameter(cmd, "@BillDate", DbType.String, objSalesReturn.sBillDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objSalesReturn.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objSalesReturn.SalesEntryID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objSalesReturn.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSalesReturn.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSalesReturn.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSalesReturn.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSalesReturn.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesReturn.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objSalesReturn.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objSalesReturn.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesReturn.DiscountAmount);
                db.AddInParameter(cmd, "@ReturnAmount", DbType.Decimal, objSalesReturn.ReturnAmount);                
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objSalesReturn.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@Invoice_BillType", DbType.Boolean, objSalesReturn.InvoiceBillType);
                db.AddInParameter(cmd, "@Customer_BillType", DbType.Boolean, objSalesReturn.CustomerBillType);
                db.AddInParameter(cmd, "@OldInvoiceNo", DbType.String, objSalesReturn.OldInvoiceNo);
                db.AddInParameter(cmd, "@RoundOff", DbType.Decimal, objSalesReturn.Roundoff);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objSalesReturn.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objSalesReturn.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objSalesReturn.ImagePath3);
                db.AddInParameter(cmd, "@Comments", DbType.String, objSalesReturn.Comments);
                db.AddInParameter(cmd, "@ACK_No", DbType.String, objSalesReturn.ACK_No);
                db.AddInParameter(cmd, "@IRN_No", DbType.String, objSalesReturn.IRN_No);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objSalesReturn.FinancialYear.FinancialYearID);
                db.AddInParameter(cmd, "@IsRetailBill", DbType.Boolean, objSalesReturn.IsRetailBill);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                foreach (Entity.Billing.SalesReturnTrans ObjSalesReturnTrans in objSalesReturn.SalesReturnTrans) {
                    ObjSalesReturnTrans.SalesReturnID = objSalesReturn.SalesReturnID;
                    if (objSalesReturn.IsRetailBill == false)
                        ObjSalesReturnTrans.SalesEntry.SalesEntryID = objSalesReturn.SalesEntryID;
                }

                SalesReturnTrans.SaveSalesReturnTransaction(objSalesReturn.SalesReturnTrans);
            }
            catch (Exception ex)
            {
                sException = "SalesReturn UpdateSalesReturn| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSalesReturn(int iSalesReturnId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SALESRETURN);
                db.AddInParameter(cmd, "@PK_SalesReturnID", DbType.Int32, iSalesReturnId);
               // db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
              //  db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesReturn DeleteSalesReturn | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet GetSalesReturnSummary()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESRETURNSUMMARY);
                dsList = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "SalesReturn GetSalesReturnSummary | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
    }

    public class SalesReturnTrans
    {
        public static Collection<Entity.Billing.SalesReturnTrans> GetSalesReturnTransBySalesReturnID(int iSalesReturnID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.SalesReturnTrans> objList = new Collection<Entity.Billing.SalesReturnTrans>();
            Entity.Billing.SalesReturnTrans objSalesReturnTrans = new Entity.Billing.SalesReturnTrans();
            Entity.Master.Product ObjProduct; Entity.Billing.Tax objTax; Entity.Billing.SalesEntry objSalesEntry;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESRETURNTRANS);
                db.AddInParameter(cmd, "@FK_SalesReturnID", DbType.Int32, iSalesReturnID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesReturnTrans = new Entity.Billing.SalesReturnTrans();
                        ObjProduct = new Entity.Master.Product();
                        objTax = new Entity.Billing.Tax();
                        objSalesEntry = new Entity.Billing.SalesEntry();


                        objSalesReturnTrans.SalesReturnTransID = Convert.ToInt32(drData["PK_SalesReturnTransID"]);
                        objSalesReturnTrans.SalesReturnID = Convert.ToInt32(drData["FK_SalesReturnID"]);
                        ObjProduct.ProductID = Convert.ToInt32(drData["Fk_ProductID"]);
                        ObjProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        ObjProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        ObjProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objSalesReturnTrans.Product = ObjProduct;
                        objSalesReturnTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objSalesReturnTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objSalesReturnTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                        objSalesReturnTrans.SalesEntryTransID = Convert.ToInt32(drData["FK_SalesEntryTransID"]);
                        objSalesReturnTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objSalesReturnTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objSalesReturnTrans.SubTotal = Convert.ToDecimal(drData["Subtotal"]);
                        objSalesReturnTrans.Notes = Convert.ToString(drData["Notes"]);

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxPercentage = Convert.ToDecimal(drData["TaxPercent"]);
                        objTax.SGSTPercent = Convert.ToDecimal(drData["SGSTPercent"]);
                        objTax.IGSTPercent = Convert.ToDecimal(drData["IGSTPercent"]);
                        objTax.CGSTPercent = Convert.ToDecimal(drData["CGSTPercent"]);
                        objSalesReturnTrans.Tax = objTax;

                        objSalesEntry.SalesEntryID = Convert.ToInt32(drData["FK_SalesEntryID"]);
                        objSalesEntry.InvoiceNo = Convert.ToString(drData["InvoiceNo"]);
                        objSalesReturnTrans.SalesEntry = objSalesEntry;

                        objSalesReturnTrans.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objSalesReturnTrans.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objSalesReturnTrans.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objSalesReturnTrans.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);

                        objList.Add(objSalesReturnTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "SalesReturnTrans GetSalesReturnTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveSalesReturnTransaction(Collection<Entity.Billing.SalesReturnTrans> ObjSalesReturnTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.SalesReturnTrans ObjSalesReturnTransaction in ObjSalesReturnTransList)
            {
                if (ObjSalesReturnTransaction.StatusFlag == "I")
                    iID = AddSalesReturnTrans(ObjSalesReturnTransaction);
                else if (ObjSalesReturnTransaction.StatusFlag == "U")
                    bResult = UpdateSalesReturnTrans(ObjSalesReturnTransaction);
                else if (ObjSalesReturnTransaction.StatusFlag == "D")
                    bResult = DeleteSalesReturnTrans(ObjSalesReturnTransaction.SalesReturnTransID);
            }
        }
        public static int AddSalesReturnTrans(Entity.Billing.SalesReturnTrans objSalesReturnTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SALESRETURNTRANS);
                db.AddOutParameter(cmd, "@PK_SalesReturnTransID", DbType.Int32, objSalesReturnTrans.SalesReturnTransID);
                db.AddInParameter(cmd, "@FK_SalesReturnID", DbType.Int32, objSalesReturnTrans.SalesReturnID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, objSalesReturnTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objSalesReturnTrans.Barcode);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objSalesReturnTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objSalesReturnTrans.Rate);
                db.AddInParameter(cmd, "@Subtotal", DbType.Decimal, objSalesReturnTrans.SubTotal);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objSalesReturnTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesReturnTrans.DiscountAmount);
                db.AddInParameter(cmd, "@FK_SalesEntryTransID", DbType.Int32, objSalesReturnTrans.SalesEntryTransID);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objSalesReturnTrans.Product.ProductName);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objSalesReturnTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSalesReturnTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objSalesReturnTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objSalesReturnTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objSalesReturnTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSalesReturnTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSalesReturnTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSalesReturnTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesReturnTrans.TaxAmount);
                db.AddInParameter(cmd, "@Notes", DbType.String, objSalesReturnTrans.Notes);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objSalesReturnTrans.SalesEntry.SalesEntryID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_SalesReturnTransID"));
            }
            catch (Exception ex)
            {
                sException = "SalesReturnTrans AddSalesReturnTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateSalesReturnTrans(Entity.Billing.SalesReturnTrans objSalesReturnTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SALESRETURNTRANS);
                db.AddInParameter(cmd, "@PK_SalesReturnTransID", DbType.Int32, objSalesReturnTrans.SalesReturnTransID);
                db.AddInParameter(cmd, "@FK_SalesReturnID", DbType.Int32, objSalesReturnTrans.SalesReturnID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.Int32, objSalesReturnTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objSalesReturnTrans.Barcode);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objSalesReturnTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objSalesReturnTrans.Rate);
                db.AddInParameter(cmd, "@Subtotal", DbType.Decimal, objSalesReturnTrans.SubTotal);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objSalesReturnTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objSalesReturnTrans.DiscountAmount);
                db.AddInParameter(cmd, "@FK_SalesEntryTransID", DbType.Int32, objSalesReturnTrans.SalesEntryTransID);
                db.AddInParameter(cmd, "@ProductName", DbType.String, objSalesReturnTrans.Product.ProductName);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.String, objSalesReturnTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objSalesReturnTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objSalesReturnTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objSalesReturnTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objSalesReturnTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objSalesReturnTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objSalesReturnTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objSalesReturnTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objSalesReturnTrans.TaxAmount);
                db.AddInParameter(cmd, "@Notes", DbType.String, objSalesReturnTrans.Notes);
                db.AddInParameter(cmd, "@FK_SalesEntryID", DbType.Int32, objSalesReturnTrans.SalesEntry.SalesEntryID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesReturnTrans UpdateSalesReturnTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteSalesReturnTrans(int iSalesReturnTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SALESRETURNTRANS);
                db.AddInParameter(cmd, "@PK_SalesReturnTransID", DbType.Int32, iSalesReturnTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "SalesReturnTrans DeleteSalesReturnTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        
    }
}
