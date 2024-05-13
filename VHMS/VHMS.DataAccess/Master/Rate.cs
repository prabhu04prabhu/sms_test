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
    public class Rate
    {
        public static Collection<Entity.Rate> GetRate()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Rate> objList = new Collection<Entity.Rate>();
            Entity.Rate objRate = new Entity.Rate();
            Entity.User objCreatedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_RATE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRate = new Entity.Rate();
                        objCreatedUser = new Entity.User();

                        objRate.RateID = Convert.ToInt32(drData["PK_RateID"]);
                        objRate.RateDate = Convert.ToDateTime(drData["RateDate"]);
                        objRate.sRateDate = objRate.RateDate.ToString("dd/MM/yyyy");
                        objRate.Gold_22Sales = Convert.ToDecimal(drData["Gold_22Sales"]);
                        objRate.Gold_24Sales = Convert.ToDecimal(drData["Gold_24Sales"]);
                        objRate.Gold_22Purchase = Convert.ToDecimal(drData["Gold_22Purchase"]);
                        objRate.Gold_24Purchase = Convert.ToDecimal(drData["Gold_24Purchase"]);
                        objRate.SilverPurchase = Convert.ToDecimal(drData["SilverPurchase"]);
                        objRate.SilverSales = Convert.ToDecimal(drData["SilverSales"]);
                        objRate.Diamond_1CentPurchase = Convert.ToDecimal(drData["Diamond_1CentPurchase"]);
                        objRate.Diamond_1CentSales = Convert.ToDecimal(drData["Diamond_1CentSales"]);
                        objRate.Diamond_1CTPurchase =  Convert.ToDecimal(drData["Diamond_1CTPurchase"]);
                        objRate.Diamond_1CTSales = Convert.ToDecimal(drData["Diamond_1CTSales"]);

                        objList.Add(objRate);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Rate GetRate | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Rate GetCurrentRate()
        {
            string sException = string.Empty;
            Database db;
            Entity.Rate objRate = new Entity.Rate();
            Entity.User objCreatedUser;

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_CURRENTRATE);
                db.AddInParameter(cmd, "@PK_RateID", DbType.Int32, 0);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objRate = new Entity.Rate();
                        objCreatedUser = new Entity.User();

                        objRate.RateID = Convert.ToInt32(drData["PK_RateID"]);
                        objRate.RateDate = Convert.ToDateTime(drData["RateDate"]);
                        objRate.sRateDate = objRate.RateDate.ToString("dd/MM/yyyy");
                        objRate.Gold_22Sales = Convert.ToDecimal(drData["Gold_22Sales"]);
                        objRate.Gold_24Sales = Convert.ToDecimal(drData["Gold_24Sales"]);
                        objRate.Gold_22Purchase = Convert.ToDecimal(drData["Gold_22Purchase"]);
                        objRate.Gold_24Purchase = Convert.ToDecimal(drData["Gold_24Purchase"]);
                        objRate.SilverPurchase = Convert.ToDecimal(drData["SilverPurchase"]);
                        objRate.SilverSales = Convert.ToDecimal(drData["SilverSales"]);
                        objRate.Diamond_1CentPurchase = Convert.ToDecimal(drData["Diamond_1CentPurchase"]);
                        objRate.Diamond_1CentSales = Convert.ToDecimal(drData["Diamond_1CentSales"]);
                        objRate.Diamond_1CTPurchase = Convert.ToDecimal(drData["Diamond_1CTPurchase"]);
                        objRate.Diamond_1CTSales = Convert.ToDecimal(drData["Diamond_1CTSales"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Rate GetRateByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objRate;
        }
        public static int AddRate(Entity.Rate objRate)
        {
            int ID = 0;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    ID = AddRate(oDb, objRate, oTrans);
                    oTrans.Commit();
                    if (ID > 0)
                        Framework.InsertAuditLog("tRate", "PK_RateID", objRate.RateID.ToString(), (char)Entity.Common.DatabaseAction.INSERT, objRate.CreatedBy.UserID);
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
        private static int AddRate(Database oDb, Entity.Rate objRate, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_RATE);
                oDb.AddOutParameter(cmd, "@PK_RateID", DbType.Int32, objRate.RateID);
                oDb.AddInParameter(cmd, "@Gold_22Sales", DbType.Decimal, objRate.Gold_22Sales);
                oDb.AddInParameter(cmd, "@Gold_24Sales", DbType.Decimal, objRate.Gold_24Sales);
                oDb.AddInParameter(cmd, "@Gold_22Purchase", DbType.Decimal, objRate.Gold_22Purchase);
                oDb.AddInParameter(cmd, "@Gold_24Purchase", DbType.Decimal, objRate.Gold_24Purchase);
                oDb.AddInParameter(cmd, "@SilverPurchase", DbType.Decimal, objRate.SilverPurchase);
                oDb.AddInParameter(cmd, "@SilverSales", DbType.Decimal, objRate.SilverSales);
                oDb.AddInParameter(cmd, "@Diamond_1CentPurchase", DbType.Decimal, objRate.Diamond_1CentPurchase);
                oDb.AddInParameter(cmd, "@Diamond_1CentSales", DbType.Decimal, objRate.Diamond_1CentSales);
                oDb.AddInParameter(cmd, "@Diamond_1CTPurchase", DbType.Decimal, objRate.Diamond_1CTPurchase);
                oDb.AddInParameter(cmd, "@Diamond_1CTSales", DbType.Decimal, objRate.Diamond_1CTSales);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objRate.CreatedBy.UserID);

                iID = oDb.ExecuteNonQuery(cmd, oTrans);
                if (iID != 0)
                {
                    iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_RateID"));
                    objRate.RateID = iID;
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Rate AddRate | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }
       
    }
}
