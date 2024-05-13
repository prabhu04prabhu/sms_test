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
    public class PurchaseReturn
    {
        public static Collection<Entity.Billing.PurchaseReturn> GetPurchaseReturn(int ipatientID = 0, int iSupplierID = 0, int BillType = 1, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseReturn> objList = new Collection<Entity.Billing.PurchaseReturn>();
            Entity.Billing.PurchaseReturn objPurchaseReturn;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.Purchase objPurchase;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASERETURN);
                db.AddInParameter(cmd, "@PK_PurchaseReturnID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, iSupplierID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseReturn = new Entity.Billing.PurchaseReturn();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchase = new Entity.Billing.Purchase();

                        objPurchaseReturn.PurchaseReturnID = Convert.ToInt32(drData["PK_PurchaseReturnID"]);
                        objPurchaseReturn.ReturnNo = Convert.ToString(drData["ReturnNo"]);
                        objPurchaseReturn.ReturnDate = Convert.ToDateTime(drData["ReturnDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchaseReturn.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchaseReturn.Tax = objTax;

                        objPurchase.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchaseReturn.Purchase = objPurchase;

                        objPurchaseReturn.Notes = Convert.ToString(drData["Notes"]);
                        objPurchaseReturn.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchaseReturn.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchaseReturn.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchaseReturn.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchaseReturn.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchaseReturn.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchaseReturn.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchaseReturn.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchaseReturn.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchaseReturn.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchaseReturn.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchaseReturn.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchaseReturn.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchaseReturn.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objPurchaseReturn.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchaseReturn.IsDiscount = Convert.ToBoolean(drData["IsDiscount"]);
                        objPurchaseReturn.sReturnDate = objPurchaseReturn.ReturnDate.ToString("dd/MM/yyyy") + " " + objPurchaseReturn.CreatedOn.ToString("h:mm");
                        objPurchaseReturn.CEmployeeName = Convert.ToString(drData["CEmployeeName"]);
                        objPurchaseReturn.MEmployeeName = Convert.ToString(drData["MEmployeeName"]);
                        objPurchaseReturn.ACK_No = Convert.ToString(drData["ACK_No"]);
                        objPurchaseReturn.IRN_No = Convert.ToString(drData["IRN_No"]);

                        objList.Add(objPurchaseReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseReturn GetPurchaseReturn | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }


        public static Entity.Billing.PurchaseReturnJSON GetPurchaseReturnJsonHSNFormat(int iSalesEntryID)
        {
            string sException = string.Empty;
            Database db;

            Entity.Billing.PurchaseReturnJSON objSalesJSON = new Entity.Billing.PurchaseReturnJSON();
            Entity.Billing.PurchaseReturnTransDetails objTransDetails;
            Entity.Billing.PurchaseReturnDocumentDetails objDocumentDetails; Entity.Billing.PurchaseReturnSellerDetails objSellerDetails;
            Entity.Billing.PurchaseReturnBuyerDetails objBuyerDetails; Entity.Billing.PurchaseReturnShippingDetails objShippingDetails;
            Entity.Billing.PurchaseReturnValueDetails objValueDetails;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASERETURNJSONHSN);
                db.AddInParameter(cmd, "@PK_PurchaseReturnID", DbType.Int32, iSalesEntryID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objSalesJSON = new Entity.Billing.PurchaseReturnJSON();
                        objTransDetails = new Entity.Billing.PurchaseReturnTransDetails();
                        objDocumentDetails = new Entity.Billing.PurchaseReturnDocumentDetails();
                        objSellerDetails = new Entity.Billing.PurchaseReturnSellerDetails();
                        objBuyerDetails = new Entity.Billing.PurchaseReturnBuyerDetails();
                        objShippingDetails = new Entity.Billing.PurchaseReturnShippingDetails();
                        objValueDetails = new Entity.Billing.PurchaseReturnValueDetails();

                        objSalesJSON.Version = "1.1";

                        //Trans Details
                        objTransDetails.TaxSch = "GST";
                        objTransDetails.SupTyp = "B2B";
                        objTransDetails.IgstOnIntra = "N";
                        objTransDetails.RegRev = "N";
                        objTransDetails.EcmGstin = null;
                        objSalesJSON.TranDtls = objTransDetails;

                        //Document Details
                        objDocumentDetails.Typ = "DBN";
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
                        objBuyerDetails.Gstin = Convert.ToString(drData["Fax"]);
                        objBuyerDetails.LglNm = Convert.ToString(drData["SupplierName"]);
                        objBuyerDetails.TrdNm = Convert.ToString(drData["SupplierName"]);
                        objBuyerDetails.Pos = Convert.ToString(drData["StateCode"]);

                        string message = Convert.ToString(drData["SupplierAddress"]);
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
                        string iBuyerMobile = Convert.ToString(drData["PhoneNo1"]);
                        objBuyerDetails.Ph = iBuyerMobile.Length > 0 ? iBuyerMobile.Substring(iBuyerMobile.Length - 10) : null;
                        //objBuyerDetails.Ph = Convert.ToString(drData["MobileNo"]);
                        objBuyerDetails.Em = Convert.ToString(drData["Email"]).Length > 0 ? Convert.ToString(drData["Email"]) : null;
                        objSalesJSON.BuyerDtls = objBuyerDetails;

                        // Shipping Details
                        objShippingDetails.Gstin = Convert.ToString(drData["Fax"]);
                        objShippingDetails.LglNm = Convert.ToString(drData["SupplierName"]);
                        objShippingDetails.TrdNm = Convert.ToString(drData["SupplierName"]);

                        string s_message = Convert.ToString(drData["SupplierAddress"]);
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
                        objShippingDetails.Loc = Convert.ToString(drData["City"]);
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
                        objValueDetails.TotInvVal = Convert.ToDecimal(drData["NetAmount"]);
                        objValueDetails.TotInvValFc = 0;
                        objSalesJSON.ValDtls = objValueDetails;

                        objSalesJSON.PayDtls = null;
                        objSalesJSON.RefDtls = null;

                        Collection<Entity.Billing.PurchaseReturnAdditionalDetails> objAddCollection = new Collection<Entity.Billing.PurchaseReturnAdditionalDetails>();
                        Entity.Billing.PurchaseReturnAdditionalDetails objAdditional = new Entity.Billing.PurchaseReturnAdditionalDetails();
                        objAdditional.Url = null;
                        objAdditional.Docs = null;
                        objAdditional.Info = null;
                        objAddCollection.Add(objAdditional);
                        objSalesJSON.ReturnAddlDocDtls = objAddCollection;


                        Collection<Entity.Billing.PurchaseReturnItemDetails> objItemList = new Collection<Entity.Billing.PurchaseReturnItemDetails>();
                        Entity.Billing.PurchaseReturnItemDetails objItemDetails;
                        int cnt_val = 1;
                        foreach (DataRow drTrans in dsList.Tables[1].Rows)
                        {
                            objItemDetails = new Entity.Billing.PurchaseReturnItemDetails();
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
        public static Collection<Entity.Billing.PurchaseReturn> SearchPurchaseReturn(string ID, int BillType, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseReturn> objList = new Collection<Entity.Billing.PurchaseReturn>();
            Entity.Billing.PurchaseReturn objPurchaseReturn;
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.Purchase objPurchase;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASERETURNDYNAMIC);
                db.AddInParameter(cmd, "@PK_PurchaseReturnID", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseReturn = new Entity.Billing.PurchaseReturn();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchase = new Entity.Billing.Purchase();

                        objPurchaseReturn.PurchaseReturnID = Convert.ToInt32(drData["PK_PurchaseReturnID"]);
                        objPurchaseReturn.ReturnNo = Convert.ToString(drData["ReturnNo"]);
                        objPurchaseReturn.ReturnDate = Convert.ToDateTime(drData["ReturnDate"]);
                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchaseReturn.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchaseReturn.Tax = objTax;

                        objPurchase.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchase.BillNo = Convert.ToString(drData["BillNo"]);
                        objPurchaseReturn.Purchase = objPurchase;

                        objPurchaseReturn.Notes = Convert.ToString(drData["Notes"]);
                        objPurchaseReturn.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchaseReturn.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchaseReturn.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchaseReturn.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchaseReturn.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchaseReturn.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchaseReturn.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchaseReturn.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchaseReturn.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchaseReturn.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchaseReturn.CreatedOn = Convert.ToDateTime(drData["CreatedOn"]);
                        objPurchaseReturn.TotalQty = Convert.ToDecimal(drData["TotalQty"]);
                        objPurchaseReturn.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchaseReturn.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchaseReturn.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchaseReturn.IsDiscount = Convert.ToBoolean(drData["IsDiscount"]);
                        objPurchaseReturn.sReturnDate = objPurchaseReturn.ReturnDate.ToString("dd/MM/yyyy") + " " + objPurchaseReturn.CreatedOn.ToString("h:mm");
                        objPurchaseReturn.CEmployeeName = Convert.ToString(drData["CEmployeeName"]);
                        objPurchaseReturn.MEmployeeName = Convert.ToString(drData["MEmployeeName"]);

                        objList.Add(objPurchaseReturn);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseReturn GetPurchaseReturn | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Entity.Billing.PurchaseReturn GetPurchaseReturnByID(int iPurchaseReturnID, int BillType, int iFinancialYearID = 0)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.PurchaseReturn objPurchaseReturn = new Entity.Billing.PurchaseReturn();
            Entity.Billing.Supplier objSupplier; Entity.Billing.Tax objTax; Entity.Billing.Purchase objPurchase;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASERETURN);
                db.AddInParameter(cmd, "@PK_PurchaseReturnID", DbType.Int32, iPurchaseReturnID);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, iFinancialYearID);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, BillType);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseReturn = new Entity.Billing.PurchaseReturn();
                        objSupplier = new Entity.Billing.Supplier();
                        objTax = new Entity.Billing.Tax();
                        objPurchase = new Entity.Billing.Purchase();

                        objPurchaseReturn.PurchaseReturnID = Convert.ToInt32(drData["PK_PurchaseReturnID"]);
                        objPurchaseReturn.ReturnNo = Convert.ToString(drData["ReturnNo"]);
                        objPurchaseReturn.ReturnDate = Convert.ToDateTime(drData["ReturnDate"]);
                        objPurchaseReturn.sReturnDate = objPurchaseReturn.ReturnDate.ToString("dd/MM/yyyy");

                        objSupplier.SupplierID = Convert.ToInt32(drData["FK_SupplierID"]);
                        objSupplier.SupplierName = Convert.ToString(drData["SupplierName"]);
                        objPurchaseReturn.Supplier = objSupplier;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objPurchaseReturn.Tax = objTax;

                        objPurchase.PurchaseID = Convert.ToInt32(drData["FK_PurchaseID"]);
                        objPurchase.PurchaseNo = Convert.ToString(drData["PurchaseNo"]);
                        objPurchaseReturn.Purchase = objPurchase;

                        objPurchaseReturn.Notes = Convert.ToString(drData["Notes"]);
                        objPurchaseReturn.TaxPercent = Convert.ToDecimal(drData["TaxPercent"]);
                        objPurchaseReturn.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchaseReturn.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchaseReturn.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchaseReturn.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objPurchaseReturn.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objPurchaseReturn.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objPurchaseReturn.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchaseReturn.Roundoff = Convert.ToDecimal(drData["Roundoff"]);
                        objPurchaseReturn.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
                        objPurchaseReturn.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objPurchaseReturn.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objPurchaseReturn.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objPurchaseReturn.IsDiscount = Convert.ToBoolean(drData["IsDiscount"]);
                        objPurchaseReturn.ACK_No = Convert.ToString(drData["ACK_No"]);
                        objPurchaseReturn.IRN_No = Convert.ToString(drData["IRN_No"]);

                        objPurchaseReturn.PurchaseReturnTrans = PurchaseReturnTrans.GetPurchaseReturnTransByPurchaseReturnID(objPurchaseReturn.PurchaseReturnID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseReturn GetPurchaseReturnByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchaseReturn;
        }
        public static int AddPurchaseReturn(Entity.Billing.PurchaseReturn objPurchaseReturn)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PURCHASERETURN);
                db.AddOutParameter(cmd, "@PK_PurchaseReturnID", DbType.Int32, objPurchaseReturn.PurchaseReturnID);
                db.AddInParameter(cmd, "@ReturnNo", DbType.String, objPurchaseReturn.ReturnNo);
                db.AddInParameter(cmd, "@ReturnDate", DbType.String, objPurchaseReturn.sReturnDate);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objPurchaseReturn.Supplier.SupplierID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objPurchaseReturn.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, objPurchaseReturn.Purchase.PurchaseID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objPurchaseReturn.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objPurchaseReturn.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objPurchaseReturn.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objPurchaseReturn.IGSTAmount);
                db.AddInParameter(cmd, "@BillType", DbType.Boolean, objPurchaseReturn.BillType);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objPurchaseReturn.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objPurchaseReturn.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objPurchaseReturn.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objPurchaseReturn.DiscountAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objPurchaseReturn.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objPurchaseReturn.NetAmount);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objPurchaseReturn.CreatedBy.UserID);
                db.AddInParameter(cmd, "@Notes", DbType.String, objPurchaseReturn.Notes);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objPurchaseReturn.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objPurchaseReturn.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objPurchaseReturn.ImagePath3);
                db.AddInParameter(cmd, "@IsDiscount", DbType.Boolean, objPurchaseReturn.IsDiscount);
                db.AddInParameter(cmd, "@ACK_No", DbType.String, objPurchaseReturn.ACK_No);
                db.AddInParameter(cmd, "@IRN_No", DbType.String, objPurchaseReturn.IRN_No);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objPurchaseReturn.FinancialYear.FinancialYearID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_PurchaseReturnID"));

                foreach (Entity.Billing.PurchaseReturnTrans ObjPurchaseReturnTrans in objPurchaseReturn.PurchaseReturnTrans)
                    ObjPurchaseReturnTrans.PurchaseReturnID = iID;

                PurchaseReturnTrans.SavePurchaseReturnTransaction(objPurchaseReturn.PurchaseReturnTrans);
            }
            catch (Exception ex)
            {
                sException = "PurchaseReturn AddPurchaseReturn | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePurchaseReturn(Entity.Billing.PurchaseReturn objPurchaseReturn)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PURCHASERETURN);
                db.AddInParameter(cmd, "@PK_PurchaseReturnID", DbType.Int32, objPurchaseReturn.PurchaseReturnID);
                db.AddInParameter(cmd, "@ReturnNo", DbType.String, objPurchaseReturn.ReturnNo);
                db.AddInParameter(cmd, "@ReturnDate", DbType.String, objPurchaseReturn.sReturnDate);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, objPurchaseReturn.Supplier.SupplierID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objPurchaseReturn.Tax.TaxID);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, objPurchaseReturn.Purchase.PurchaseID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objPurchaseReturn.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objPurchaseReturn.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objPurchaseReturn.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objPurchaseReturn.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objPurchaseReturn.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objPurchaseReturn.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objPurchaseReturn.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objPurchaseReturn.DiscountAmount);
                db.AddInParameter(cmd, "@Roundoff", DbType.Decimal, objPurchaseReturn.Roundoff);
                db.AddInParameter(cmd, "@NetAmount", DbType.Decimal, objPurchaseReturn.NetAmount);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objPurchaseReturn.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@ImagePath1", DbType.String, objPurchaseReturn.ImagePath1);
                db.AddInParameter(cmd, "@ImagePath2", DbType.String, objPurchaseReturn.ImagePath2);
                db.AddInParameter(cmd, "@ImagePath3", DbType.String, objPurchaseReturn.ImagePath3);
                db.AddInParameter(cmd, "@Notes", DbType.String, objPurchaseReturn.Notes);
                db.AddInParameter(cmd, "@IsDiscount", DbType.Boolean, objPurchaseReturn.IsDiscount);
                db.AddInParameter(cmd, "@ACK_No", DbType.String, objPurchaseReturn.ACK_No);
                db.AddInParameter(cmd, "@IRN_No", DbType.String, objPurchaseReturn.IRN_No);
                db.AddInParameter(cmd, "@FK_FinancialYearID", DbType.Int32, objPurchaseReturn.FinancialYear.FinancialYearID);

                foreach (Entity.Billing.PurchaseReturnTrans ObjPurchaseReturnTrans in objPurchaseReturn.PurchaseReturnTrans)
                {
                    ObjPurchaseReturnTrans.PurchaseReturnID = objPurchaseReturn.PurchaseReturnID;
                    //ObjPurchaseReturnTrans.StatusFlag = "I";
                }
                //foreach (Entity.Billing.PurchaseReturnTrans ObjPurchaseReturnTransaction in objPurchaseReturn.PurchaseReturnTrans)
                //{
                //    PurchaseReturnTrans.DeletePurchaseReturnTrans(ObjPurchaseReturnTransaction.PurchaseReturnTransID);
                //}

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
               
                PurchaseReturnTrans.SavePurchaseReturnTransaction(objPurchaseReturn.PurchaseReturnTrans);
            }
            catch (Exception ex)
            {
                sException = "PurchaseReturn UpdatePurchaseReturn| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePurchaseReturn(int iPurchaseReturnId, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PURCHASERETURN);
                db.AddInParameter(cmd, "@PK_PurchaseReturnID", DbType.Int32, iPurchaseReturnId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PurchaseReturn DeletePurchaseReturn | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        //public static DataSet GetPurchaseReturnSummary()
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;

        //    try
        //    {
        //        db = Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PurchaseReturnSUMMARY);
        //        dsList = db.ExecuteDataSet(cmd);
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "PurchaseReturn GetPurchaseReturnSummary | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return dsList;
        //}
    }

    public class PurchaseReturnTrans
    {
        public static Collection<Entity.Billing.PurchaseReturnTrans> GetPurchaseReturnTransByPurchaseReturnID(int iPurchaseReturnID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.PurchaseReturnTrans> objList = new Collection<Entity.Billing.PurchaseReturnTrans>();
            Entity.Billing.PurchaseReturnTrans objPurchaseReturnTrans = new Entity.Billing.PurchaseReturnTrans();
            Entity.Master.Product objProduct; Entity.Billing.Tax objTax;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASERETURNTRANS);
                db.AddInParameter(cmd, "@FK_PurchaseReturnID", DbType.Int32, iPurchaseReturnID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseReturnTrans = new Entity.Billing.PurchaseReturnTrans();
                        objProduct = new Entity.Master.Product();
                        objTax = new Entity.Billing.Tax();

                        objPurchaseReturnTrans.PurchaseReturnTransID = Convert.ToInt32(drData["PK_PurchaseReturnTransID"]);
                        objPurchaseReturnTrans.PurchaseReturnID = Convert.ToInt32(drData["FK_PurchaseReturnID"]);

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxPercentage = Convert.ToDecimal(drData["TaxPercent"]);
                        objTax.SGSTPercent = Convert.ToDecimal(drData["SGSTPercent"]);
                        objTax.IGSTPercent = Convert.ToDecimal(drData["IGSTPercent"]);
                        objTax.CGSTPercent = Convert.ToDecimal(drData["CGSTPercent"]);
                        objPurchaseReturnTrans.Tax = objTax;

                        objPurchaseReturnTrans.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchaseReturnTrans.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchaseReturnTrans.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchaseReturnTrans.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);

                        objPurchaseReturnTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objPurchaseReturnTrans.Rate = Convert.ToInt32(drData["Rate"]);
                        objPurchaseReturnTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objPurchaseReturnTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchaseReturnTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                        objPurchaseReturnTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objPurchaseReturnTrans.Notes = Convert.ToString(drData["Notes"]);

                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objPurchaseReturnTrans.Product = objProduct;

                        objList.Add(objPurchaseReturnTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseReturnTrans GetPurchaseReturnTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Entity.Billing.PurchaseReturnTrans GetQuantity(int ID, int PurchaseID, int SupplierID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Tax objTax;
            Entity.Billing.PurchaseReturnTrans objPurchaseReturnTrans = new Entity.Billing.PurchaseReturnTrans();
            Entity.Master.Product objProduct;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_PURCHASERETURNTRANSQUANTITY);
                db.AddInParameter(cmd, "@PK_ProductID", DbType.Int32, ID);
                db.AddInParameter(cmd, "@FK_PurchaseID", DbType.Int32, PurchaseID);
                db.AddInParameter(cmd, "@FK_SupplierID", DbType.Int32, SupplierID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objPurchaseReturnTrans = new Entity.Billing.PurchaseReturnTrans();
                        objProduct = new Entity.Master.Product();
                        objTax = new Entity.Billing.Tax();

                        objPurchaseReturnTrans.PurchaseReturnTransID = Convert.ToInt32(drData["PK_PurchaseReturnTransID"]);
                        objPurchaseReturnTrans.PurchaseReturnID = Convert.ToInt32(drData["FK_PurchaseReturnID"]);

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxPercentage = Convert.ToDecimal(drData["TaxPercent"]);
                        objTax.SGSTPercent = Convert.ToDecimal(drData["SGSTPercent"]);
                        objTax.IGSTPercent = Convert.ToDecimal(drData["IGSTPercent"]);
                        objTax.CGSTPercent = Convert.ToDecimal(drData["CGSTPercent"]);
                        objPurchaseReturnTrans.Tax = objTax;

                        objPurchaseReturnTrans.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objPurchaseReturnTrans.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objPurchaseReturnTrans.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objPurchaseReturnTrans.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);

                        objPurchaseReturnTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objPurchaseReturnTrans.Rate = Convert.ToInt32(drData["Rate"]);
                        objPurchaseReturnTrans.SubTotal = Convert.ToDecimal(drData["SubTotal"]);
                        objPurchaseReturnTrans.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objPurchaseReturnTrans.DiscountPercentage = Convert.ToDecimal(drData["DiscountPercentage"]);
                        objPurchaseReturnTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objPurchaseReturnTrans.Notes = Convert.ToString(drData["Notes"]);

                        objProduct.ProductCode = Convert.ToString(drData["ProductCode"]);
                        objProduct.ProductName = Convert.ToString(drData["ProductName"]);
                        objProduct.SMSCode = Convert.ToString(drData["SMSCode"]);
                        objProduct.ProductID = Convert.ToInt32(drData["FK_ProductID"]);
                        objPurchaseReturnTrans.Product = objProduct;


                    }
                }
            }
            catch (Exception ex)
            {
                sException = "PurchaseReturnTrans GetPurchaseReturnTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objPurchaseReturnTrans;
        }



        public static void SavePurchaseReturnTransaction(Collection<Entity.Billing.PurchaseReturnTrans> ObjPurchaseReturnTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.PurchaseReturnTrans ObjPurchaseReturnTransaction in ObjPurchaseReturnTransList)
            {
                if (ObjPurchaseReturnTransaction.StatusFlag == "I")
                    iID = AddPurchaseReturnTrans(ObjPurchaseReturnTransaction);
                else if (ObjPurchaseReturnTransaction.StatusFlag == "U")
                    bResult = UpdatePurchaseReturnTrans(ObjPurchaseReturnTransaction);
                else if (ObjPurchaseReturnTransaction.StatusFlag == "D")
                    bResult = DeletePurchaseReturnTrans(ObjPurchaseReturnTransaction.PurchaseReturnTransID);
            }
        }
        public static int AddPurchaseReturnTrans(Entity.Billing.PurchaseReturnTrans objPurchaseReturnTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_PURCHASERETURNTRANS);
                db.AddOutParameter(cmd, "@PK_PurchaseReturnTransID", DbType.Int32, objPurchaseReturnTrans.PurchaseReturnTransID);
                db.AddInParameter(cmd, "@FK_PurchaseReturnID", DbType.Int32, objPurchaseReturnTrans.PurchaseReturnID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objPurchaseReturnTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objPurchaseReturnTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objPurchaseReturnTrans.Rate);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objPurchaseReturnTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objPurchaseReturnTrans.DiscountAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objPurchaseReturnTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objPurchaseReturnTrans.Barcode);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objPurchaseReturnTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@Notes", DbType.String, objPurchaseReturnTrans.Notes);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objPurchaseReturnTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objPurchaseReturnTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objPurchaseReturnTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objPurchaseReturnTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objPurchaseReturnTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objPurchaseReturnTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objPurchaseReturnTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objPurchaseReturnTrans.TaxAmount);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_PurchaseReturnTransID"));
            }
            catch (Exception ex)
            {
                sException = "PurchaseReturnTrans AddPurchaseReturnTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdatePurchaseReturnTrans(Entity.Billing.PurchaseReturnTrans objPurchaseReturnTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_PURCHASERETURNTRANS);
                db.AddInParameter(cmd, "@PK_PurchaseReturnTransID", DbType.Int32, objPurchaseReturnTrans.PurchaseReturnTransID);
                db.AddInParameter(cmd, "@FK_PurchaseReturnID", DbType.Int32, objPurchaseReturnTrans.PurchaseReturnID);
                db.AddInParameter(cmd, "@FK_ProductID", DbType.String, objPurchaseReturnTrans.Product.ProductID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objPurchaseReturnTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objPurchaseReturnTrans.Rate);
                db.AddInParameter(cmd, "@DiscountPercentage", DbType.Decimal, objPurchaseReturnTrans.DiscountPercentage);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objPurchaseReturnTrans.DiscountAmount);
                db.AddInParameter(cmd, "@SubTotal", DbType.Decimal, objPurchaseReturnTrans.SubTotal);
                db.AddInParameter(cmd, "@Barcode", DbType.String, objPurchaseReturnTrans.Barcode);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objPurchaseReturnTrans.Tax.TaxID);
                db.AddInParameter(cmd, "@Notes", DbType.String, objPurchaseReturnTrans.Notes);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objPurchaseReturnTrans.Tax.TaxPercentage);
                db.AddInParameter(cmd, "@IGSTPercent", DbType.Decimal, objPurchaseReturnTrans.Tax.IGSTPercent);
                db.AddInParameter(cmd, "@SGSTPercent", DbType.Decimal, objPurchaseReturnTrans.Tax.SGSTPercent);
                db.AddInParameter(cmd, "@CGSTPercent", DbType.Decimal, objPurchaseReturnTrans.Tax.CGSTPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objPurchaseReturnTrans.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objPurchaseReturnTrans.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objPurchaseReturnTrans.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objPurchaseReturnTrans.TaxAmount);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PurchaseReturnTrans UpdatePurchaseReturnTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeletePurchaseReturnTrans(int iPurchaseReturnTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_PURCHASERETURNTRANS);
                db.AddInParameter(cmd, "@PK_PurchaseReturnTransID", DbType.Int32, iPurchaseReturnTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "PurchaseReturnTrans DeletePurchaseReturnTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
