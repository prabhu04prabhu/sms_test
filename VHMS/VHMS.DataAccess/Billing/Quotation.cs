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
    public class Quotation
    {
        public static Collection<Entity.Billing.Quotation> GetQuotation(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Quotation> objList = new Collection<Entity.Billing.Quotation>();
            Entity.Billing.Quotation objQuotation;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_QUOTATION);
                db.AddInParameter(cmd, "@PK_QuotationID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objQuotation = new Entity.Billing.Quotation();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();


                        objQuotation.QuotationID = Convert.ToInt32(drData["PK_QuotationID"]);
                        objQuotation.QuotationNo = Convert.ToString(drData["QuotationNo"]);
                        objQuotation.QuotationDate = Convert.ToDateTime(drData["QuotationDate"]);
                        objQuotation.sQuotationDate = objQuotation.QuotationDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objQuotation.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objQuotation.Branch = objBranch;


                       

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objQuotation.Tax = objTax;
                        objQuotation.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objQuotation.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objQuotation.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objQuotation.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objQuotation.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objQuotation.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objQuotation.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objQuotation.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);
                        
                        objQuotation.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objQuotation.Employee = objEmployee;
                        objList.Add(objQuotation);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Quotation GetQuotation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Quotation> GetTopQuotation(int ipatientID = 0, int iBranchID=0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Quotation> objList = new Collection<Entity.Billing.Quotation>();
            Entity.Billing.Quotation objQuotation;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPQUOTATION);
                db.AddInParameter(cmd, "@PK_QuotationID", DbType.Int32, ipatientID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objQuotation = new Entity.Billing.Quotation();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();

                        objQuotation.QuotationID = Convert.ToInt32(drData["PK_QuotationID"]);
                        objQuotation.QuotationNo = Convert.ToString(drData["QuotationNo"]);
                        objQuotation.QuotationDate = Convert.ToDateTime(drData["QuotationDate"]);
                        objQuotation.sQuotationDate = objQuotation.QuotationDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objQuotation.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objQuotation.Branch = objBranch;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objQuotation.Tax = objTax;
                        objQuotation.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objQuotation.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objQuotation.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objQuotation.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objQuotation.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objQuotation.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objQuotation.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objQuotation.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);

                        objQuotation.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objQuotation.Employee = objEmployee;
                        objList.Add(objQuotation);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Quotation GetQuotation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Billing.Quotation> GetQuotationID(int ipatientID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Quotation> objList = new Collection<Entity.Billing.Quotation>();
            Entity.Billing.Quotation objQuotation;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee;

            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_QUOTATION);
                db.AddInParameter(cmd, "@PK_QuotationID", DbType.Int32, ipatientID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objQuotation = new Entity.Billing.Quotation();

                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();

                        objQuotation.QuotationID = Convert.ToInt32(drData["PK_QuotationID"]);
                        objQuotation.QuotationNo = Convert.ToString(drData["QuotationNo"]);
                        objQuotation.QuotationDate = Convert.ToDateTime(drData["QuotationDate"]);
                        objQuotation.sQuotationDate = objQuotation.QuotationDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objQuotation.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objQuotation.Branch = objBranch;

                     

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objQuotation.Tax = objTax;
                        objQuotation.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objQuotation.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objQuotation.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objQuotation.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objQuotation.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objQuotation.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objQuotation.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objQuotation.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);
                        
                        objQuotation.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objQuotation.Employee = objEmployee;
                        objList.Add(objQuotation);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Quotation GetQuotation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Billing.Quotation> SearchQuotation(string ID, int iBranchID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.Quotation> objList = new Collection<Entity.Billing.Quotation>();
            Entity.Billing.Quotation objQuotation;
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee;
            try
            {
                db = VHMS.Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_QUOTATION);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, iBranchID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objQuotation = new Entity.Billing.Quotation();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();

                        objQuotation.QuotationID = Convert.ToInt32(drData["PK_QuotationID"]);
                        objQuotation.QuotationNo = Convert.ToString(drData["QuotationNo"]);
                        objQuotation.QuotationDate = Convert.ToDateTime(drData["QuotationDate"]);
                        objQuotation.sQuotationDate = objQuotation.QuotationDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objQuotation.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objQuotation.Branch = objBranch;

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objQuotation.Tax = objTax;
                        objQuotation.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objQuotation.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objQuotation.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objQuotation.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objQuotation.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objQuotation.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objQuotation.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objQuotation.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);

                        objQuotation.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objQuotation.Employee = objEmployee;
                        objList.Add(objQuotation);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Quotation GetQuotation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        //public static Collection<Entity.Billing.Quotation> GetQuotationReport(Entity.Billing.QuotationFilter oJobCardFilter)
        //{
        //    string sException = string.Empty;
        //    Database db;
        //    DataSet dsList = null;
        //    Collection<Entity.Billing.Quotation> objList = new Collection<Entity.Billing.Quotation>();
        //    Entity.Billing.Quotation objQuotation;
        //    Entity.patient objpatient; Entity.Discharge.Doctor objdoctor;

        //    try
        //    {
        //        db = VHMS.Entity.DBConnection.dbCon;
        //        DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_QUOTATIONREPORT);
        //        db.AddInParameter(cmd, "@FK_PatientID", DbType.Int32, oJobCardFilter.PatientID);
        //        db.AddInParameter(cmd, "@DateFrom", DbType.String, oJobCardFilter.DateFrom);
        //        db.AddInParameter(cmd, "@DateTo", DbType.String, oJobCardFilter.DateTo);
        //        db.AddInParameter(cmd, "@FK_DoctorID", DbType.Int32, oJobCardFilter.DoctorID);
        //        db.AddInParameter(cmd, "@FK_DescriptionID", DbType.Int32, oJobCardFilter.DescriptionID);
        //        db.AddInParameter(cmd, "@FK_UserID", DbType.Int32, oJobCardFilter.UserID);
        //        dsList = db.ExecuteDataSet(cmd);

        //        if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow drData in dsList.Tables[0].Rows)
        //            {
        //                objQuotation = new Entity.Billing.Quotation();
        //                objpatient = new Entity.patient();
        //                objdoctor = new Entity.Discharge.Doctor();

        //                objQuotation.QuotationID = Convert.ToInt32(drData["PK_QuotationID"]);
        //                objQuotation.BillNo = Convert.ToString(drData["BillNo"]);
        //              //  objQuotation.BillDate = Convert.ToDateTime(drData["BillDate"]);
        //                objQuotation.sBillDate = (Convert.ToDateTime(drData["BillDate"])).ToString("dd/MM/yyyy");

        //               // objpatient.patientID = Convert.ToInt32(drData["FK_patientID"]);
        //              //  objpatient.HName = Convert.ToString(drData["HName"]);
        //                objpatient.WName = Convert.ToString(drData["WName"]);
        //                objpatient.WMobileNo = Convert.ToString(drData["WMobileNo"]);
        //               // objpatient.OPDNo = Convert.ToString(drData["OPDNo"]);
        //                objQuotation.Patient = objpatient;
        //               // objdoctor.DoctorID = Convert.ToInt32(drData["FK_DoctorID"]);
        //                objdoctor.DoctorName = Convert.ToString(drData["DoctorName"]);
        //                objQuotation.Doctor = objdoctor;
        //               // objQuotation.PaymentMode = Convert.ToString(drData["PaymentMode"]);
        //                objQuotation.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
        //               // objQuotation.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
        //              //  objQuotation.NetAmount = Convert.ToDecimal(drData["NetAmount"]);
        //                //objQuotation.PaidAmount = Convert.ToDecimal(drData["PaidAmount"]);
        //                //objQuotation.BalanceAmount = Convert.ToDecimal(drData["BalanceAmount"]);
        //                objQuotation.Subtotal = Convert.ToDecimal(drData["Subtotal"]);
        //                objQuotation.DescriptionName = Convert.ToString(drData["DescriptionName"]);
        //                objQuotation.CUserName = Convert.ToString(drData["CUserName"]);
        //                objQuotation.IsCancelled = Convert.ToBoolean(drData["IsCancelled"]);
        //                objQuotation.CancelReason = Convert.ToString(drData["CancelReason"]);
        //                objList.Add(objQuotation);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        sException = "Quotation GetQuotation | " + ex.ToString();
        //        Log.Write(sException);
        //        throw;
        //    }
        //    return objList;
        //}
        public static Entity.Billing.Quotation GetQuotationByID(int iQuotationID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Quotation objQuotation = new Entity.Billing.Quotation();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_QUOTATION);
                db.AddInParameter(cmd, "@PK_QuotationID", DbType.Int32, iQuotationID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objQuotation = new Entity.Billing.Quotation();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();

                        objQuotation.QuotationID = Convert.ToInt32(drData["PK_QuotationID"]);
                        objQuotation.QuotationNo = Convert.ToString(drData["QuotationNo"]);
                        objQuotation.QuotationDate = Convert.ToDateTime(drData["QuotationDate"]);
                        objQuotation.sQuotationDate = objQuotation.QuotationDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objQuotation.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objQuotation.Branch = objBranch;

                       

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objQuotation.Tax = objTax;
                        objQuotation.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objQuotation.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objQuotation.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objQuotation.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objQuotation.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objQuotation.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objQuotation.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objQuotation.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);
                       
                        objQuotation.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objQuotation.Employee = objEmployee;
                        objQuotation.QuotationTrans = QuotationTrans.GetQuotationTransByQuotationID(objQuotation.QuotationID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Quotation GetQuotationByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objQuotation;
        }

        public static Entity.Billing.Quotation GetQuotationByQuotation(string QuotationNo)
        {
            string sException = string.Empty;
            Database db;
            Entity.Billing.Quotation objQuotation = new Entity.Billing.Quotation();
            Entity.Customer objCustomer; Entity.Branch objBranch; Entity.Billing.Tax objTax;
            Entity.User objEmployee;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_QUOTATION);
                db.AddInParameter(cmd, "@QuotationNo", DbType.String, QuotationNo);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objQuotation = new Entity.Billing.Quotation();
                        objCustomer = new Entity.Customer();
                        objBranch = new Entity.Branch();
                        objTax = new Entity.Billing.Tax();
                        objEmployee = new Entity.User();

                        objQuotation.QuotationID = Convert.ToInt32(drData["PK_QuotationID"]);
                        objQuotation.QuotationNo = Convert.ToString(drData["QuotationNo"]);
                        objQuotation.QuotationDate = Convert.ToDateTime(drData["QuotationDate"]);
                        objQuotation.sQuotationDate = objQuotation.QuotationDate.ToString("dd/MM/yyyy");

                        objCustomer.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objQuotation.Customer = objCustomer;

                        objBranch.BranchID = Convert.ToInt32(drData["FK_BranchID"]);
                        objBranch.BranchName = Convert.ToString(drData["BranchName"]);
                        objQuotation.Branch = objBranch;

                      

                        objTax.TaxID = Convert.ToInt32(drData["FK_TaxID"]);
                        objTax.TaxName = Convert.ToString(drData["TaxName"]);
                        objQuotation.Tax = objTax;
                        objQuotation.CGSTAmount = Convert.ToDecimal(drData["CGSTAmount"]);
                        objQuotation.SGSTAmount = Convert.ToDecimal(drData["SGSTAmount"]);
                        objQuotation.IGSTAmount = Convert.ToDecimal(drData["IGSTAmount"]);
                        objQuotation.TaxAmount = Convert.ToDecimal(drData["TaxAmount"]);
                        objQuotation.TotalAmount = Convert.ToDecimal(drData["TotalAmount"]);
                        objQuotation.DiscountPercent = Convert.ToDecimal(drData["DiscountPercent"]);
                        objQuotation.DiscountAmount = Convert.ToDecimal(drData["DiscountAmount"]);
                        objQuotation.InvoiceAmount = Convert.ToDecimal(drData["InvoiceAmount"]);

                        objEmployee.UserID = Convert.ToInt32(drData["FK_EmployeeID"]);
                        objEmployee.EmployeeName = Convert.ToString(drData["EmployeeName"]);
                        objEmployee.EmployeeCode = Convert.ToString(drData["EmployeeCode"]);
                        objQuotation.Employee = objEmployee;

                        objQuotation.IsActive = Convert.ToBoolean(drData["IsActive"]);
                       
                        objQuotation.QuotationTrans = QuotationTrans.GetQuotationTransByQuotationID(objQuotation.QuotationID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "Quotation GetQuotationByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objQuotation;
        }
        public static int AddQuotation(Entity.Billing.Quotation objQuotation)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_QUOTATION);
                db.AddOutParameter(cmd, "@PK_QuotationID", DbType.Int32, objQuotation.QuotationID);
                db.AddInParameter(cmd, "@QuotationNo", DbType.String, objQuotation.QuotationNo);
                db.AddInParameter(cmd, "@QuotationDate", DbType.String, objQuotation.sQuotationDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objQuotation.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objQuotation.Branch.BranchID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objQuotation.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objQuotation.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objQuotation.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objQuotation.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objQuotation.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objQuotation.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objQuotation.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objQuotation.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objQuotation.DiscountAmount);
                db.AddInParameter(cmd, "@InvoiceAmount", DbType.Decimal, objQuotation.InvoiceAmount);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, objQuotation.Employee.UserID);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, true);
                db.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objQuotation.CreatedBy.UserID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_QuotationID"));

                foreach (Entity.Billing.QuotationTrans ObjQuotationTrans in objQuotation.QuotationTrans)
                    ObjQuotationTrans.QuotationID = iID;

                QuotationTrans.SaveQuotationTransaction(objQuotation.QuotationTrans);
            }
            catch (Exception ex)
            {
                sException = "Quotation AddQuotation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateQuotation(Entity.Billing.Quotation objQuotation)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_QUOTATION);
                db.AddInParameter(cmd, "@PK_QuotationID", DbType.Int32, objQuotation.QuotationID);
               // db.AddInParameter(cmd, "@BillNo", DbType.String, objQuotation.QuotationNo);
                db.AddInParameter(cmd, "@QuotationDate", DbType.String, objQuotation.sQuotationDate);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objQuotation.Customer.CustomerID);
                db.AddInParameter(cmd, "@FK_BranchID", DbType.Int32, objQuotation.Branch.BranchID);
                db.AddInParameter(cmd, "@FK_TaxID", DbType.Int32, objQuotation.Tax.TaxID);
                db.AddInParameter(cmd, "@TaxPercent", DbType.Decimal, objQuotation.TaxPercent);
                db.AddInParameter(cmd, "@CGSTAmount", DbType.Decimal, objQuotation.CGSTAmount);
                db.AddInParameter(cmd, "@SGSTAmount", DbType.Decimal, objQuotation.SGSTAmount);
                db.AddInParameter(cmd, "@IGSTAmount", DbType.Decimal, objQuotation.IGSTAmount);
                db.AddInParameter(cmd, "@TaxAmount", DbType.Decimal, objQuotation.TaxAmount);
                db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, objQuotation.TotalAmount);
                db.AddInParameter(cmd, "@DiscountPercent", DbType.Decimal, objQuotation.DiscountPercent);
                db.AddInParameter(cmd, "@DiscountAmount", DbType.Decimal, objQuotation.DiscountAmount);
                db.AddInParameter(cmd, "@InvoiceAmount", DbType.Decimal, objQuotation.InvoiceAmount);
                db.AddInParameter(cmd, "@FK_EmployeeID", DbType.Int32, objQuotation.Employee.UserID);
                db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objQuotation.ModifiedBy.UserID);
                db.AddInParameter(cmd, "@IsActive", DbType.Boolean, true);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                foreach (Entity.Billing.QuotationTrans ObjQuotationTrans in objQuotation.QuotationTrans)
                    ObjQuotationTrans.QuotationID = objQuotation.QuotationID;

                QuotationTrans.SaveQuotationTransaction(objQuotation.QuotationTrans);
            }
            catch (Exception ex)
            {
                sException = "Quotation UpdateQuotation| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteQuotation(int iQuotationId, string Reason, int iUserId)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_QUOTATION);
                db.AddInParameter(cmd, "@PK_QuotationID", DbType.Int32, iQuotationId);
               // db.AddInParameter(cmd, "@Reason", DbType.String, Reason);
              //  db.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, iUserId);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "Quotation DeleteQuotation | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static DataSet GetQuotationSummary()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_QUOTATION);
                dsList = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "Quotation GetQuotationSummary | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
    }

    public class QuotationTrans
    {
        public static Collection<Entity.Billing.QuotationTrans> GetQuotationTransByQuotationID(int iQuotationID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Billing.QuotationTrans> objList = new Collection<Entity.Billing.QuotationTrans>();
            Entity.Billing.QuotationTrans objQuotationTrans = new Entity.Billing.QuotationTrans();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_QUOTATIONTRANS);
                db.AddInParameter(cmd, "@FK_QuotationID", DbType.Int32, iQuotationID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objQuotationTrans = new Entity.Billing.QuotationTrans();

                        objQuotationTrans.QuotationTransID = Convert.ToInt32(drData["PK_QuotationTransID"]);
                        objQuotationTrans.QuotationID = Convert.ToInt32(drData["FK_QuotationID"]);
                        objQuotationTrans.StockID = Convert.ToInt32(drData["Fk_StockID"]);
                        objQuotationTrans.Quantity = Convert.ToDecimal(drData["Quantity"]);
                        objQuotationTrans.Rate = Convert.ToDecimal(drData["Rate"]);
                        objQuotationTrans.Subtotal = Convert.ToDecimal(drData["Subtotal"]);
                        objQuotationTrans.CategoryName = Convert.ToString(drData["CategoryName"]);
                        objQuotationTrans.Barcode = Convert.ToString(drData["Barcode"]);
                        objQuotationTrans.ProductName = Convert.ToString(drData["ProductName"]);
                        objQuotationTrans.WastagePercent = Convert.ToDecimal(drData["WastagePercent"]);
                        objQuotationTrans.Wastage = Convert.ToDecimal(drData["Wastage"]);
                        objQuotationTrans.TotalWeight = Convert.ToDecimal(drData["TotalWeight"]);
                        objQuotationTrans.NetWeight = Convert.ToDecimal(drData["NetWeight"]);
                        objQuotationTrans.Ratti = Convert.ToDecimal(drData["Ratti"]);
                        objQuotationTrans.PureWeight = Convert.ToDecimal(drData["PureWeight"]);
                        objQuotationTrans.Lacquer = Convert.ToDecimal(drData["Lacquer"]);
                        objQuotationTrans.Design = Convert.ToString(drData["Design"]);
                        objQuotationTrans.Karat = Convert.ToString(drData["Karat"]);
                        objQuotationTrans.SellingPrice = Convert.ToDecimal(drData["SellingPrice"]);
                        objQuotationTrans.StoneName = Convert.ToString(drData["StoneName"]);
                        objQuotationTrans.StoneQuantity = Convert.ToInt32(drData["StoneQuantity"]);
                        objQuotationTrans.StonePrice = Convert.ToDecimal(drData["StonePrice"]);
                        objQuotationTrans.StoneWeight = Convert.ToDecimal(drData["StoneWeight"]);

                        objList.Add(objQuotationTrans);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "QuotationTrans GetQuotationTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveQuotationTransaction(Collection<Entity.Billing.QuotationTrans> ObjQuotationTransList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.Billing.QuotationTrans ObjQuotationTransaction in ObjQuotationTransList)
            {
                if (ObjQuotationTransaction.StatusFlag == "I")
                    iID = AddQuotationTrans(ObjQuotationTransaction);
                else if (ObjQuotationTransaction.StatusFlag == "U")
                    bResult = UpdateQuotationTrans(ObjQuotationTransaction);
                else if (ObjQuotationTransaction.StatusFlag == "D")
                    bResult = DeleteQuotationTrans(ObjQuotationTransaction.QuotationTransID);
            }
        }
        public static int AddQuotationTrans(Entity.Billing.QuotationTrans objQuotationTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_QUOTATIONTRANS);
                db.AddOutParameter(cmd, "@PK_QuotationTransID", DbType.Int32, objQuotationTrans.QuotationTransID);
                db.AddInParameter(cmd, "@FK_QuotationID", DbType.Int32, objQuotationTrans.QuotationID);
                db.AddInParameter(cmd, "@FK_StockID", DbType.Int32, objQuotationTrans.Stock.StockID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objQuotationTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objQuotationTrans.Rate);
                db.AddInParameter(cmd, "@Subtotal", DbType.Decimal, objQuotationTrans.Subtotal);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_QuotationTransID"));
            }
            catch (Exception ex)
            {
                sException = "QuotationTrans AddQuotationTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateQuotationTrans(Entity.Billing.QuotationTrans objQuotationTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_QUOTATIONTRANS);
                db.AddInParameter(cmd, "@PK_QuotationTransID", DbType.Int32, objQuotationTrans.QuotationTransID);
                db.AddInParameter(cmd, "@FK_QuotationID", DbType.Int32, objQuotationTrans.QuotationID);
                db.AddInParameter(cmd, "@FK_StockID", DbType.Int32, objQuotationTrans.Stock.StockID);
                db.AddInParameter(cmd, "@Quantity", DbType.Decimal, objQuotationTrans.Quantity);
                db.AddInParameter(cmd, "@Rate", DbType.Decimal, objQuotationTrans.Rate);
                db.AddInParameter(cmd, "@Subtotal", DbType.Decimal, objQuotationTrans.Subtotal);
                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "QuotationTrans UpdateQuotationTrans| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteQuotationTrans(int iQuotationTransID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_QUOTATIONTRANS);
                db.AddInParameter(cmd, "@PK_QuotationTransID", DbType.Int32, iQuotationTransID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "QuotationTrans DeleteQuotationTrans | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        
    }
}
