using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;

namespace VHMS.DataAccess
{
    public class Customer
    {
        public static Collection<Entity.Customer> GetCustomer()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Customer> objList = new Collection<Entity.Customer>();
            Entity.Customer objCustomer = new Entity.Customer();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser; Entity.Transport objTransport;
            Entity.State objState = new Entity.State();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CUSTOMER);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomer = new Entity.Customer();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objTransport = new Entity.Transport();

                        objCustomer.CustomerID = Convert.ToInt32(drData["PK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Days = Convert.ToInt32(drData["Days"]);
                        objCustomer.CustomerCode = Convert.ToString(drData["CustomerCode"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.CustomerType = Convert.ToString(drData["CustomerType"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                        objCustomer.AlternateNo = Convert.ToString(drData["AlternateNo"]);
                        objCustomer.Email = Convert.ToString(drData["Email"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objCustomer.DOB = Convert.ToString(drData["DOB"]);
                        objCustomer.City = Convert.ToString(drData["City"]);
                        objCustomer.Notes = Convert.ToString(drData["Notes"]);
                        objCustomer.Pincode = Convert.ToString(drData["Pincode"]);
                        objCustomer.MaxDueDays = Convert.ToInt32(drData["MaxDueDays"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);

                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objCustomer.Transport = objTransport;

                        objCustomer.MDType = Convert.ToString(drData["MDType"]);
                        objCustomer.MDName = Convert.ToString(drData["MDName"]);
                        objCustomer.MDContact = Convert.ToString(drData["MDContact"]);
                        objCustomer.ManagerType = Convert.ToString(drData["ManagerType"]);
                        objCustomer.MangerName = Convert.ToString(drData["MangerName"]);
                        objCustomer.MangerContact = Convert.ToString(drData["MangerContact"]);

                        objState = new Entity.State();
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objCustomer.State = objState;
                        objCustomer.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objCustomer.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objCustomer.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objCustomer.Area = Convert.ToString(drData["Area"]);
                        objCustomer.Shipping_Address = Convert.ToString(drData["Shipping_Address"]);
                        objCustomer.Default_DiscountPercent = Convert.ToDecimal(drData["Default_DiscountPercent"]);
                        objCustomer.Limit_SalesAmount = Convert.ToDecimal(drData["Limit_SalesAmount"]);
                        objCustomer.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objCustomer);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer GetCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Customer> GetActiveCustomer()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Customer> objList = new Collection<Entity.Customer>();
            Entity.Customer objCustomer = new Entity.Customer();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LEDGERCUSTOMER);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objCustomer = new Entity.Customer();
                            objCreatedUser = new Entity.User();
                            objModifiedUser = new Entity.User();

                            objCustomer.CustomerID = Convert.ToInt32(drData["PK_CustomerID"]);
                            objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]) + "-" + Convert.ToString(drData["MobileNo"]);
                            objCustomer.CustomerCode = Convert.ToString(drData["CustomerCode"]);
                            objCustomer.Days = Convert.ToInt32(drData["Days"]);
                            objCustomer.Address = Convert.ToString(drData["Address"]);
                            objCustomer.Area = Convert.ToString(drData["Area"]);
                            objCustomer.CustomerType = Convert.ToString(drData["CustomerType"]);
                            objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                            objCustomer.AlternateNo = Convert.ToString(drData["AlternateNo"]);
                            objCustomer.Email = Convert.ToString(drData["Email"]);
                            objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                            objCustomer.DOB = Convert.ToString(drData["DOB"]);
                            objCustomer.IsActive = Convert.ToBoolean(drData["IsActive"]);

                            objList.Add(objCustomer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer GetCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Customer> GetArea(string type)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Customer> objList = new Collection<Entity.Customer>();
            Entity.Customer objCustomer = new Entity.Customer();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_AREA);
                db.AddInParameter(cmd, "@type", DbType.String, type);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        //if (Convert.ToBoolean(drData["IsActive"]))

                        objCustomer = new Entity.Customer();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        //objCustomer.CustomerID = Convert.ToInt32(drData["PK_CustomerID"]);  
                        objCustomer.Area = Convert.ToString(drData["Area"]);

                        objList.Add(objCustomer);
                    }
                }
            }

            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer GetCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Customer> GetRetailsActiveCustomer()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Customer> objList = new Collection<Entity.Customer>();
            Entity.Customer objCustomer = new Entity.Customer();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_LEDGERRETAILSCUSTOMER);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objCustomer = new Entity.Customer();
                            objCreatedUser = new Entity.User();
                            objModifiedUser = new Entity.User();

                            objCustomer.CustomerID = Convert.ToInt32(drData["PK_CustomerID"]);
                            objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]) + "-" + Convert.ToString(drData["MobileNo"]);
                            objCustomer.CustomerCode = Convert.ToString(drData["CustomerCode"]);
                            objCustomer.Address = Convert.ToString(drData["Address"]);
                            objCustomer.CustomerType = Convert.ToString(drData["CustomerType"]);
                            objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                            objCustomer.AlternateNo = Convert.ToString(drData["AlternateNo"]);
                            objCustomer.Email = Convert.ToString(drData["Email"]);
                            objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                            objCustomer.DOB = Convert.ToString(drData["DOB"]);
                            objCustomer.IsActive = Convert.ToBoolean(drData["IsActive"]);

                            objList.Add(objCustomer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer GetCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static List<string> GetRetailSalesCustomer(string prefix)
        {
            List<string> Products = new List<string>();
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Customer> objList = new Collection<Entity.Customer>();
            Entity.Customer objProduct = new Entity.Customer();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RETAILSCUSTOMER);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, prefix);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        Products.Add(string.Format(" {0,-300} | {1,-200} | {2,-300}", drData["CustomerName"], drData["Area"], drData["MobileNo"]));
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Master.Product GetProduct | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return Products;
        }

        public static List<string> GetWholeSalesCustomer(string prefix)
        {
            List<string> Products = new List<string>();
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Customer> objList = new Collection<Entity.Customer>();
            Entity.Customer objProduct = new Entity.Customer();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_WHOLECUSTOMER);
                db.AddInParameter(cmd, "@CustomerName", DbType.String, prefix);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        Products.Add(string.Format(" {0,-300} | {1,-200} | {2,-300}", drData["CustomerName"], drData["Area"], drData["MobileNo"]));
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Master.Product GetProduct | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return Products;
        }
        public static Collection<Entity.Customer> GetActiveSalesReturnCustomer()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Customer> objList = new Collection<Entity.Customer>();
            Entity.Customer objCustomer = new Entity.Customer();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SALESRETURNCUSTOMER);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        if (Convert.ToBoolean(drData["IsActive"]))
                        {
                            objCustomer = new Entity.Customer();
                            objCreatedUser = new Entity.User();
                            objModifiedUser = new Entity.User();

                            objCustomer.CustomerID = Convert.ToInt32(drData["PK_CustomerID"]);
                            objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]) + "-" + Convert.ToString(drData["MobileNo"]);
                            objCustomer.CustomerCode = Convert.ToString(drData["CustomerCode"]);
                            objCustomer.Days = Convert.ToInt32(drData["Days"]);
                            objCustomer.Address = Convert.ToString(drData["Address"]);
                            objCustomer.CustomerType = Convert.ToString(drData["CustomerType"]);
                            objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                            objCustomer.AlternateNo = Convert.ToString(drData["AlternateNo"]);
                            objCustomer.Email = Convert.ToString(drData["Email"]);
                            objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                            objCustomer.DOB = Convert.ToString(drData["DOB"]);
                            objCustomer.IsActive = Convert.ToBoolean(drData["IsActive"]);

                            objList.Add(objCustomer);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer GetCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Customer> GetTopCustomer(int CustomerID, string Type)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Customer> objList = new Collection<Entity.Customer>();
            Entity.Customer objCustomer = new Entity.Customer();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.State objState = new Entity.State();
            Entity.CustomerTypes objCustomerTypes = new Entity.CustomerTypes();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPCUSTOMER);
                db.AddInParameter(cmd, "@PK_CustomerID", DbType.Int32, CustomerID);
                db.AddInParameter(cmd, "@TypeName", DbType.String, Type);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomer = new Entity.Customer();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objCustomer.CustomerID = Convert.ToInt32(drData["PK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Days = Convert.ToInt32(drData["Days"]);
                        objCustomer.CustomerCode = Convert.ToString(drData["CustomerCode"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.CustomerType = Convert.ToString(drData["CustomerType"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                        objCustomer.AlternateNo = Convert.ToString(drData["AlternateNo"]);
                        objCustomer.Email = Convert.ToString(drData["Email"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objCustomer.DOB = Convert.ToString(drData["DOB"]);
                        objCustomer.City = Convert.ToString(drData["City"]);
                        objCustomer.Comments = Convert.ToString(drData["Comments"]);
                        objCustomer.Pincode = Convert.ToString(drData["Pincode"]);
                        objCustomer.MaxDueDays = Convert.ToInt32(drData["MaxDueDays"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);

                        objCustomer.MDType = Convert.ToString(drData["MDType"]);
                        objCustomer.MDName = Convert.ToString(drData["MDName"]);
                        objCustomer.MDContact = Convert.ToString(drData["MDContact"]);
                        objCustomer.ManagerType = Convert.ToString(drData["ManagerType"]);
                        objCustomer.MangerName = Convert.ToString(drData["MangerName"]);
                        objCustomer.MangerContact = Convert.ToString(drData["MangerContact"]);

                        objState = new Entity.State();
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objCustomer.State = objState;

                        objCustomerTypes = new Entity.CustomerTypes();
                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomerTypeID"]);
                        objCustomerTypes.CustomerTypeName = Convert.ToString(drData["CustomerType_Name"]);
                        objCustomer.CustomerTypes = objCustomerTypes;

                        objCustomer.Area = Convert.ToString(drData["Area"]);
                        objCustomer.Shipping_Address = Convert.ToString(drData["Shipping_Address"]);
                        objCustomer.Default_DiscountPercent = Convert.ToDecimal(drData["Default_DiscountPercent"]);
                        objCustomer.Limit_SalesAmount = Convert.ToDecimal(drData["Limit_SalesAmount"]);
                        objCustomer.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objCustomer.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objCustomer.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objCustomer.ImagePath3 = Convert.ToString(drData["ImagePath3"]);

                        objList.Add(objCustomer);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer GetCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Collection<Entity.Customer> GetTopCustomerType(string Type)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Customer> objList = new Collection<Entity.Customer>();
            Entity.Customer objCustomer = new Entity.Customer();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.State objState = new Entity.State();
            Entity.CustomerTypes objCustomerTypes = new Entity.CustomerTypes();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPCUSTOMERTYPE);
                db.AddInParameter(cmd, "@TypeName", DbType.String, Type);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomer = new Entity.Customer();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objCustomer.CustomerID = Convert.ToInt32(drData["PK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Days = Convert.ToInt32(drData["Days"]);
                        objCustomer.CustomerCode = Convert.ToString(drData["CustomerCode"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);

                        objCustomer.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                        objCustomer.CustomerType = Convert.ToString(drData["CustomerType"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.AlternateNo = Convert.ToString(drData["AlternateNo"]);
                        objCustomer.Email = Convert.ToString(drData["Email"]);

                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objCustomer.DOB = Convert.ToString(drData["DOB"]);
                        objCustomer.City = Convert.ToString(drData["City"]);
                        objCustomer.Pincode = Convert.ToString(drData["Pincode"]);
                        objCustomer.MaxDueDays = Convert.ToInt32(drData["MaxDueDays"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);

                        objState = new Entity.State();
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objCustomer.State = objState;

                        objCustomerTypes = new Entity.CustomerTypes();
                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomerTypeID"]);
                        objCustomerTypes.CustomerTypeName = Convert.ToString(drData["CustomerType_Name"]);
                        objCustomer.CustomerTypes = objCustomerTypes;

                        objCustomer.Area = Convert.ToString(drData["Area"]);
                        objCustomer.Shipping_Address = Convert.ToString(drData["Shipping_Address"]);
                        objCustomer.Default_DiscountPercent = Convert.ToDecimal(drData["Default_DiscountPercent"]);
                        objCustomer.Limit_SalesAmount = Convert.ToDecimal(drData["Limit_SalesAmount"]);
                        objCustomer.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objList.Add(objCustomer);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer GetCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Customer> GetActiveTopCustomerType(string Type)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Customer> objList = new Collection<Entity.Customer>();
            Entity.Customer objCustomer = new Entity.Customer();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.State objState = new Entity.State();
            Entity.CustomerTypes objCustomerTypes = new Entity.CustomerTypes();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TOPCUSTOMERTYPE);
                db.AddInParameter(cmd, "@TypeName", DbType.String, Type);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomer = new Entity.Customer();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objCustomer.CustomerID = Convert.ToInt32(drData["PK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Days = Convert.ToInt32(drData["Days"]);
                        objCustomer.CustomerCode = Convert.ToString(drData["CustomerCode"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.CustomerType = Convert.ToString(drData["CustomerType"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.AlternateNo = Convert.ToString(drData["AlternateNo"]);
                        objCustomer.Email = Convert.ToString(drData["Email"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objCustomer.DOB = Convert.ToString(drData["DOB"]);
                        objCustomer.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                        objCustomer.City = Convert.ToString(drData["City"]);
                        objCustomer.Pincode = Convert.ToString(drData["Pincode"]);
                        objCustomer.MaxDueDays = Convert.ToInt32(drData["MaxDueDays"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);

                        objState = new Entity.State();
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objCustomer.State = objState;

                        objCustomerTypes = new Entity.CustomerTypes();
                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomerTypeID"]);
                        objCustomerTypes.CustomerTypeName = Convert.ToString(drData["CustomerType_Name"]);
                        objCustomer.CustomerTypes = objCustomerTypes;

                        objCustomer.Area = Convert.ToString(drData["Area"]);
                        objCustomer.Shipping_Address = Convert.ToString(drData["Shipping_Address"]);
                        objCustomer.Default_DiscountPercent = Convert.ToDecimal(drData["Default_DiscountPercent"]);
                        objCustomer.Limit_SalesAmount = Convert.ToDecimal(drData["Limit_SalesAmount"]);
                        objCustomer.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objList.Add(objCustomer);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer GetCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Customer GetCustomerByID(int iCustomerID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Customer objCustomer = new Entity.Customer();
            Entity.State objState = new Entity.State();
            Entity.CustomerTypes objCustomerTypes = new Entity.CustomerTypes();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser; Entity.Transport objTransport;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CUSTOMER);
                db.AddInParameter(cmd, "@PK_CustomerID", DbType.Int32, iCustomerID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomer = new Entity.Customer();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();
                        objTransport = new Entity.Transport();

                        objCustomer.CustomerID = Convert.ToInt32(drData["PK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Days = Convert.ToInt32(drData["Days"]);
                        objCustomer.CustomerCode = Convert.ToString(drData["CustomerCode"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.CustomerType = Convert.ToString(drData["CustomerType"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                        objCustomer.AlternateNo = Convert.ToString(drData["AlternateNo"]);
                        objCustomer.Notes = Convert.ToString(drData["Notes"]);
                        objCustomer.Email = Convert.ToString(drData["Email"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objCustomer.DOB = Convert.ToString(drData["DOB"]);
                        objCustomer.City = Convert.ToString(drData["City"]);
                        objCustomer.Pincode = Convert.ToString(drData["Pincode"]);
                        objCustomer.MaxDueDays = Convert.ToInt32(drData["MaxDueDays"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.Comments = Convert.ToString(drData["Comments"]);

                        objTransport.TransportID = Convert.ToInt32(drData["FK_TransportID"]);
                        objCustomer.Transport = objTransport;

                        objState = new Entity.State();
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objState.StateCode = Convert.ToString(drData["StateCode"]);
                        objCustomer.State = objState;

                        objCustomerTypes = new Entity.CustomerTypes();
                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomerTypeID"]);
                        objCustomerTypes.CustomerTypeName = Convert.ToString(drData["CustomerType_Name"]);
                        objCustomer.CustomerTypes = objCustomerTypes;

                        objCustomer.MDType = Convert.ToString(drData["MDType"]);
                        objCustomer.MDName = Convert.ToString(drData["MDName"]);
                        objCustomer.MDContact = Convert.ToString(drData["MDContact"]);
                        objCustomer.ManagerType = Convert.ToString(drData["ManagerType"]);
                        objCustomer.MangerName = Convert.ToString(drData["MangerName"]);
                        objCustomer.MangerContact = Convert.ToString(drData["MangerContact"]);

                        objCustomer.Area = Convert.ToString(drData["Area"]);
                        objCustomer.Shipping_Address = Convert.ToString(drData["Shipping_Address"]);
                        objCustomer.Default_DiscountPercent = Convert.ToDecimal(drData["Default_DiscountPercent"]);
                        objCustomer.Balance_amount = Convert.ToDecimal(drData["BalanceAmount"]);
                        objCustomer.Limit_SalesAmount = Convert.ToDecimal(drData["Limit_SalesAmount"]);
                        objCustomer.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objCustomer.ImagePath1 = Convert.ToString(drData["ImagePath1"]);
                        objCustomer.ImagePath2 = Convert.ToString(drData["ImagePath2"]);
                        objCustomer.ImagePath3 = Convert.ToString(drData["ImagePath3"]);
                        objCustomer.ShippingAddress = ShippingAddress.GetShippingAddressByID(objCustomer.CustomerID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer GetCustomerByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objCustomer;
        }

        public static Entity.Customer GetCustomerByCode(string iCustomerID ,string iType)
        {
            string sException = string.Empty;
            Database db;
            Entity.Customer objCustomer = new Entity.Customer();
            Entity.User objCreatedUser; Entity.CustomerTypes objCustomerTypes;
            Entity.User objModifiedUser; Entity.State objState;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CUSTOMERBYCODE);
                db.AddInParameter(cmd, "@PK_CustomerID", DbType.String, iCustomerID);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, iType);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomer = new Entity.Customer();
                        objCustomerTypes = new Entity.CustomerTypes();
                        objState = new Entity.State();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objCustomer.CustomerID = Convert.ToInt32(drData["PK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Days = Convert.ToInt32(drData["Days"]);
                        objCustomer.CustomerCode = Convert.ToString(drData["CustomerCode"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.CustomerType = Convert.ToString(drData["CustomerType"]);
                        objCustomer.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomerTypes.CustomertypeID = Convert.ToInt32(drData["FK_CustomerTypeID"]);
                        objCustomer.CustomerTypes = objCustomerTypes;
                        objCustomer.MaxDueDays = Convert.ToInt32(drData["MaxDueDays"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.Area = Convert.ToString(drData["Area"]);
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objCustomer.State = objState;

                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.AlternateNo = Convert.ToString(drData["AlternateNo"]);
                        objCustomer.Email = Convert.ToString(drData["Email"]);
                        objCustomer.DOB = Convert.ToString(drData["DOB"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objCustomer.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objCustomer.ShippingAddress = ShippingAddress.GetShippingAddressByID(objCustomer.CustomerID);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer GetCustomerByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objCustomer;
        }

        public static Collection<Entity.Customer> GetCustomerByDate(string iCustomerID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Customer> objList = new Collection<Entity.Customer>();
            Entity.Customer objCustomer = new Entity.Customer();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CUSTOMERBYDATE);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, iCustomerID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomer = new Entity.Customer();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objCustomer.CustomerID = Convert.ToInt32(drData["PK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Days = Convert.ToInt32(drData["Days"]);
                        objCustomer.CustomerType = Convert.ToString(drData["CustomerType"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objCustomer);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer GetCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Customer> GetCustomerByType(string iCustomerID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Customer> objList = new Collection<Entity.Customer>();
            Entity.Customer objCustomer = new Entity.Customer();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CUSTOMERBYTYPE);
                db.AddInParameter(cmd, "@CustomerType", DbType.String, iCustomerID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomer = new Entity.Customer();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objCustomer.CustomerID = Convert.ToInt32(drData["PK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Days = Convert.ToInt32(drData["Days"]);
                        objCustomer.CustomerType = Convert.ToString(drData["CustomerType"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                        objCustomer.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objCustomer);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer GetCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }

        public static Collection<Entity.Customer> SearchCustomer(string ID, string iType)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Customer> objList = new Collection<Entity.Customer>();
            Entity.Customer objCustomer = new Entity.Customer();
            Entity.User objCreatedUser;
            Entity.User objModifiedUser;
            Entity.State objState = new Entity.State();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SEARCH_CUSTOMER);
                db.AddInParameter(cmd, "@key", DbType.String, ID);
                db.AddInParameter(cmd, "@Type", DbType.String, iType);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objCustomer = new Entity.Customer();
                        objCreatedUser = new Entity.User();
                        objModifiedUser = new Entity.User();

                        objCustomer.CustomerID = Convert.ToInt32(drData["PK_CustomerID"]);
                        objCustomer.CustomerName = Convert.ToString(drData["CustomerName"]);
                        objCustomer.Days = Convert.ToInt32(drData["Days"]);
                        objCustomer.CustomerCode = Convert.ToString(drData["CustomerCode"]);
                        objCustomer.Address = Convert.ToString(drData["Address"]);
                        objCustomer.CustomerType = Convert.ToString(drData["CustomerType"]);
                        objCustomer.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objCustomer.WhatsAppNo = Convert.ToString(drData["WhatsAppNo"]);
                        objCustomer.AlternateNo = Convert.ToString(drData["AlternateNo"]);
                        objCustomer.Email = Convert.ToString(drData["Email"]);
                        objCustomer.GSTNo = Convert.ToString(drData["GSTNo"]);
                        objCustomer.DOB = Convert.ToString(drData["DOB"]);
                        objCustomer.MaxDueDays = Convert.ToInt32(drData["MaxDueDays"]);
                        objCustomer.MinDueDays = Convert.ToInt32(drData["MinDueDays"]);
                        objCustomer.City = Convert.ToString(drData["City"]);
                        objCustomer.Pincode = Convert.ToString(drData["Pincode"]);

                        objState = new Entity.State();
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objCustomer.State = objState;

                        objCustomer.Area = Convert.ToString(drData["Area"]);
                        objCustomer.Shipping_Address = Convert.ToString(drData["Shipping_Address"]);
                        objCustomer.Default_DiscountPercent = Convert.ToDecimal(drData["Default_DiscountPercent"]);
                        objCustomer.Limit_SalesAmount = Convert.ToDecimal(drData["Limit_SalesAmount"]);
                        objCustomer.IsActive = Convert.ToBoolean(drData["IsActive"]);

                        objList.Add(objCustomer);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer GetCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static int AddCustomer(Entity.Customer objCustomer)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddCustomer(oDb, objCustomer, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tCustomer", "PK_CustomerID", objCustomer.CustomerID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objCustomer.CreatedBy.UserID);
                }
                catch (Exception ex)
                {
                    oTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    oDbCon.Close();
                }
            }
            return ID;
        }
        private static int AddCustomer(Database oDb, Entity.Customer objCustomer, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_CUSTOMER);
                oDb.AddOutParameter(cmd, "@PK_CustomerID", DbType.Int32, objCustomer.CustomerID);
                oDb.AddInParameter(cmd, "@CustomerName", DbType.String, objCustomer.CustomerName);
                oDb.AddInParameter(cmd, "@CustomerCode", DbType.String, objCustomer.CustomerCode);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objCustomer.Address);
                oDb.AddInParameter(cmd, "@MobileNo", DbType.String, objCustomer.MobileNo);
                oDb.AddInParameter(cmd, "@WhatsAppNo", DbType.String, objCustomer.WhatsAppNo);
                oDb.AddInParameter(cmd, "@Comments", DbType.String, objCustomer.Comments);
                oDb.AddInParameter(cmd, "@CustomerType", DbType.String, objCustomer.CustomerType);
                oDb.AddInParameter(cmd, "@AlternateNo", DbType.String, objCustomer.AlternateNo);
                oDb.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objCustomer.Transport.TransportID);
                oDb.AddInParameter(cmd, "@Email", DbType.String, objCustomer.Email);
                oDb.AddInParameter(cmd, "@GSTNo", DbType.String, objCustomer.GSTNo);
                oDb.AddInParameter(cmd, "@Notes", DbType.String, objCustomer.Notes);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objCustomer.IsActive);
                oDb.AddInParameter(cmd, "@DOB", DbType.String, objCustomer.DOB);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objCustomer.CreatedBy.UserID);
                oDb.AddInParameter(cmd, "@FK_StateID", DbType.String, objCustomer.State.StateID);
                oDb.AddInParameter(cmd, "@City", DbType.String, objCustomer.City);
                oDb.AddInParameter(cmd, "@Area", DbType.String, objCustomer.Area);
                oDb.AddInParameter(cmd, "@Pincode", DbType.String, objCustomer.Pincode);
                oDb.AddInParameter(cmd, "@Shipping_Address", DbType.String, objCustomer.Shipping_Address);
                oDb.AddInParameter(cmd, "@Default_DiscountPercent", DbType.String, objCustomer.Default_DiscountPercent);
                oDb.AddInParameter(cmd, "@Limit_SalesAmount", DbType.String, objCustomer.Limit_SalesAmount);
                oDb.AddInParameter(cmd, "@FK_CustomerTypeID", DbType.Int32, objCustomer.CustomerTypes.CustomertypeID);
                oDb.AddInParameter(cmd, "@MaxDueDays", DbType.Int32, objCustomer.MaxDueDays);
                oDb.AddInParameter(cmd, "@MinDueDays", DbType.Int32, objCustomer.MinDueDays);
                oDb.AddInParameter(cmd, "@Days", DbType.Int32, objCustomer.Days);
                oDb.AddInParameter(cmd, "@ImagePath1", DbType.String, objCustomer.ImagePath1);
                oDb.AddInParameter(cmd, "@ImagePath2", DbType.String, objCustomer.ImagePath2);
                oDb.AddInParameter(cmd, "@ImagePath3", DbType.String, objCustomer.ImagePath3);

                oDb.AddInParameter(cmd, "@MDType", DbType.String, objCustomer.MDType);
                oDb.AddInParameter(cmd, "@MDName", DbType.String, objCustomer.MDName);
                oDb.AddInParameter(cmd, "@MDContact", DbType.String, objCustomer.MDContact);
                oDb.AddInParameter(cmd, "@ManagerType", DbType.String, objCustomer.ManagerType);
                oDb.AddInParameter(cmd, "@MangerName", DbType.String, objCustomer.MangerName);
                oDb.AddInParameter(cmd, "@MangerContact", DbType.String, objCustomer.MangerContact);


                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_CustomerID"));
                    objCustomer.CustomerID = iID;
                }

                foreach (Entity.ShippingAddress ObjPurchaseTrans in objCustomer.ShippingAddress)
                    ObjPurchaseTrans.CustomerID = iID;

                ShippingAddress.SaveShippingAddressaction(objCustomer.ShippingAddress);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer AddCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateCustomer(Entity.Customer objCustomer)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateCustomer(oDb, objCustomer, oTrans);
                    oTrans.Commit();
                    if (IsUpdated) Framework.InsertAuditLog("tCustomer", "PK_CustomerID", objCustomer.CustomerID.ToString(), (char)Entity.Common.DatabaseAction.UPDATE, objCustomer.ModifiedBy.UserID);
                }
                catch (Exception ex)
                {
                    oTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    oDbCon.Close();
                }
            }
            return IsUpdated;
        }
        private static bool UpdateCustomer(Database oDb, Entity.Customer objCustomer, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_CUSTOMER);
                oDb.AddInParameter(cmd, "@PK_CustomerID", DbType.Int32, objCustomer.CustomerID);
                oDb.AddInParameter(cmd, "@CustomerName", DbType.String, objCustomer.CustomerName);
                oDb.AddInParameter(cmd, "@CustomerCode", DbType.String, objCustomer.CustomerCode);
                oDb.AddInParameter(cmd, "@Address", DbType.String, objCustomer.Address);
                oDb.AddInParameter(cmd, "@MobileNo", DbType.String, objCustomer.MobileNo);
                oDb.AddInParameter(cmd, "@WhatsAppNo", DbType.String, objCustomer.WhatsAppNo);
                oDb.AddInParameter(cmd, "@Comments", DbType.String, objCustomer.Comments);
                oDb.AddInParameter(cmd, "@CustomerType", DbType.String, objCustomer.CustomerType);
                oDb.AddInParameter(cmd, "@AlternateNo", DbType.String, objCustomer.AlternateNo);
                oDb.AddInParameter(cmd, "@Email", DbType.String, objCustomer.Email);
                oDb.AddInParameter(cmd, "@GSTNo", DbType.String, objCustomer.GSTNo);
                oDb.AddInParameter(cmd, "@Notes", DbType.String, objCustomer.Notes);
                oDb.AddInParameter(cmd, "@DOB", DbType.String, objCustomer.DOB);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objCustomer.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objCustomer.ModifiedBy.UserID);
                oDb.AddInParameter(cmd, "@FK_TransportID", DbType.Int32, objCustomer.Transport.TransportID);
                oDb.AddInParameter(cmd, "@FK_StateID", DbType.String, objCustomer.State.StateID);
                oDb.AddInParameter(cmd, "@City", DbType.String, objCustomer.City);
                oDb.AddInParameter(cmd, "@Area", DbType.String, objCustomer.Area);
                oDb.AddInParameter(cmd, "@Pincode", DbType.String, objCustomer.Pincode);
                oDb.AddInParameter(cmd, "@Shipping_Address", DbType.String, objCustomer.Shipping_Address);
                oDb.AddInParameter(cmd, "@Default_DiscountPercent", DbType.String, objCustomer.Default_DiscountPercent);
                oDb.AddInParameter(cmd, "@Limit_SalesAmount", DbType.String, objCustomer.Limit_SalesAmount);
                oDb.AddInParameter(cmd, "@FK_CustomerTypeID", DbType.Int32, objCustomer.CustomerTypes.CustomertypeID);
                oDb.AddInParameter(cmd, "@MaxDueDays", DbType.Int32, objCustomer.MaxDueDays);
                oDb.AddInParameter(cmd, "@MinDueDays", DbType.Int32, objCustomer.MinDueDays);
                oDb.AddInParameter(cmd, "@Days", DbType.Int32, objCustomer.Days);
                oDb.AddInParameter(cmd, "@ImagePath1", DbType.String, objCustomer.ImagePath1);
                oDb.AddInParameter(cmd, "@ImagePath2", DbType.String, objCustomer.ImagePath2);
                oDb.AddInParameter(cmd, "@ImagePath3", DbType.String, objCustomer.ImagePath3);

                oDb.AddInParameter(cmd, "@MDType", DbType.String, objCustomer.MDType);
                oDb.AddInParameter(cmd, "@MDName", DbType.String, objCustomer.MDName);
                oDb.AddInParameter(cmd, "@MDContact", DbType.String, objCustomer.MDContact);
                oDb.AddInParameter(cmd, "@ManagerType", DbType.String, objCustomer.ManagerType);
                oDb.AddInParameter(cmd, "@MangerName", DbType.String, objCustomer.MangerName);
                oDb.AddInParameter(cmd, "@MangerContact", DbType.String, objCustomer.MangerContact);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;

                foreach (Entity.ShippingAddress ObjPurchaseTrans in objCustomer.ShippingAddress)
                    ObjPurchaseTrans.CustomerID = objCustomer.CustomerID;

                ShippingAddress.SaveShippingAddressaction(objCustomer.ShippingAddress);
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer UpdateCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteCustomer(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteCustomer(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tCustomer", "PK_CustomerID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
                }
                catch (Exception ex)
                {
                    oTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    oDbCon.Close();
                }
            }
            return IsDeleted;
        }
        private static bool DeleteCustomer(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_CUSTOMER);
                oDb.AddInParameter(cmd, "@PK_CustomerID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Customer DeleteCustomer | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }

    public class ShippingAddress
    {
        public static Collection<Entity.ShippingAddress> GetShippingAddressByID(int iCustomerID = 0)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.ShippingAddress> objList = new Collection<Entity.ShippingAddress>();
            Entity.ShippingAddress objShippingAddress = new Entity.ShippingAddress();
            Entity.State objState;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_SHIPPINGADDRESS);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, iCustomerID);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objShippingAddress = new Entity.ShippingAddress();
                        objState = new Entity.State();

                        objShippingAddress.ShippingAddressID = Convert.ToInt32(drData["PK_ShippingAddressID"]);
                        objShippingAddress.CustomerID = Convert.ToInt32(drData["FK_CustomerID"]);
                        objShippingAddress.Address = Convert.ToString(drData["Address"]);
                        objShippingAddress.BranchName = Convert.ToString(drData["BranchName"]);
                        objShippingAddress.GSTIN = Convert.ToString(drData["GSTIN"]);

                        objShippingAddress.Place = Convert.ToString(drData["Place"]);
                        objShippingAddress.MobileNo = Convert.ToString(drData["MobileNo"]);
                        objShippingAddress.Pincode = Convert.ToString(drData["Pincode"]);
                        objShippingAddress.Email = Convert.ToString(drData["Email"]);
                        objShippingAddress.ContactPerson = Convert.ToString(drData["ContactPerson"]);

                        objState = new Entity.State();
                        objState.StateID = Convert.ToInt32(drData["FK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objShippingAddress.State = objState;

                        objList.Add(objShippingAddress);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "ShippingAddress GetShippingAddress | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static void SaveShippingAddressaction(Collection<Entity.ShippingAddress> ObjShippingAddressList)
        {
            int iID = 0;
            bool bResult = false;

            foreach (Entity.ShippingAddress ObjShippingAddressaction in ObjShippingAddressList)
            {
                if (ObjShippingAddressaction.StatusFlag == "I")
                    iID = AddShippingAddress(ObjShippingAddressaction);
                else if (ObjShippingAddressaction.StatusFlag == "U")
                    bResult = UpdateShippingAddress(ObjShippingAddressaction);
                else if (ObjShippingAddressaction.StatusFlag == "D")
                    bResult = DeleteShippingAddress(ObjShippingAddressaction.ShippingAddressID);
            }
        }
        public static int AddShippingAddress(Entity.ShippingAddress objShippingAddress)
        {
            string sException = string.Empty;
            int iID = 0;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_SHIPPINGADDRESS);
                db.AddOutParameter(cmd, "@PK_ShippingAddressID", DbType.Int32, objShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objShippingAddress.CustomerID);
                db.AddInParameter(cmd, "@Address", DbType.String, objShippingAddress.Address);
                db.AddInParameter(cmd, "@BranchName", DbType.String, objShippingAddress.BranchName);
                db.AddInParameter(cmd, "@GSTIN", DbType.String, objShippingAddress.GSTIN);
                db.AddInParameter(cmd, "@Place", DbType.String, objShippingAddress.Place);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, objShippingAddress.MobileNo);
                db.AddInParameter(cmd, "@Pincode", DbType.String, objShippingAddress.Pincode);
                db.AddInParameter(cmd, "@Email", DbType.String, objShippingAddress.Email);
                db.AddInParameter(cmd, "@ContactPerson", DbType.String, objShippingAddress.ContactPerson);
                db.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objShippingAddress.State.StateID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(db.GetParameterValue(cmd, "@PK_ShippingAddressID"));
            }
            catch (Exception ex)
            {
                sException = "ShippingAddress AddShippingAddress | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
        public static bool UpdateShippingAddress(Entity.ShippingAddress objShippingAddress)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_SHIPPINGADDRESS);
                db.AddInParameter(cmd, "@PK_ShippingAddressID", DbType.Int32, objShippingAddress.ShippingAddressID);
                db.AddInParameter(cmd, "@FK_CustomerID", DbType.Int32, objShippingAddress.CustomerID);
                db.AddInParameter(cmd, "@Address", DbType.String, objShippingAddress.Address);
                db.AddInParameter(cmd, "@BranchName", DbType.String, objShippingAddress.BranchName);
                db.AddInParameter(cmd, "@GSTIN", DbType.String, objShippingAddress.GSTIN);
                db.AddInParameter(cmd, "@Place", DbType.String, objShippingAddress.Place);
                db.AddInParameter(cmd, "@MobileNo", DbType.String, objShippingAddress.MobileNo);
                db.AddInParameter(cmd, "@Pincode", DbType.String, objShippingAddress.Pincode);
                db.AddInParameter(cmd, "@Email", DbType.String, objShippingAddress.Email);
                db.AddInParameter(cmd, "@ContactPerson", DbType.String, objShippingAddress.ContactPerson);
                db.AddInParameter(cmd, "@FK_StateID", DbType.Int32, objShippingAddress.State.StateID);

                iID = db.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ShippingAddress UpdateShippingAddress| " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteShippingAddress(int iShippingAddressID)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            Database db;
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_SHIPPINGADDRESS);
                db.AddInParameter(cmd, "@PK_ShippingAddressID", DbType.Int32, iShippingAddressID);
                iRemoveId = db.ExecuteNonQuery(cmd);
                if (iRemoveId != 0)
                    bResult = true;
            }
            catch (Exception ex)
            {
                sException = "ShippingAddress DeleteShippingAddress | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }

    }
}
