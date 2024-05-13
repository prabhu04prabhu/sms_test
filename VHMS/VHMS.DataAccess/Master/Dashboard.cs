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
    public class Dashboard
    {
        public static Entity.Dashboard GetDashboardCount()
        {
            string sException = string.Empty;
            Database db;
            Entity.Dashboard objDashboard = new Entity.Dashboard();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_DASHBOARDCOUNT);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objDashboard = new Entity.Dashboard();
                        objDashboard.CountValue = 1;
                        objDashboard.TotalCustomer = Convert.ToInt32(drData["TotalCustomer"]);
                        objDashboard.TotalSales = Convert.ToInt32(drData["TotalSales"]);
                        objDashboard.TotalPurchase = Convert.ToInt32(drData["TotalPurchase"]);
                        objDashboard.TotalProducts = Convert.ToInt32(drData["TotalProducts"]);
                        objDashboard.VIPCustomer = Convert.ToInt32(drData["VIPCustomer"]);
                        objDashboard.WholeSaleCustomer = Convert.ToInt32(drData["WholeSaleCustomer"]);
                        objDashboard.RetailCustomer = Convert.ToInt32(drData["RetailCustomer"]);
                        objDashboard.NewCustomer = Convert.ToInt32(drData["NewCustomer"]);
                        objDashboard.Day1 = Convert.ToInt32(drData["Day1"]);
                        objDashboard.Day2 = Convert.ToInt32(drData["Day2"]);
                        objDashboard.Day3 = Convert.ToInt32(drData["Day3"]);
                        objDashboard.Day4 = Convert.ToInt32(drData["Day4"]);
                        objDashboard.Day5 = Convert.ToInt32(drData["Day5"]);
                        objDashboard.Day6 = Convert.ToInt32(drData["Day6"]);
                        objDashboard.Day7 = Convert.ToInt32(drData["Day7"]);
                        objDashboard.Day1Value = Convert.ToDecimal(drData["Day1Value"]);
                        objDashboard.Day2Value = Convert.ToDecimal(drData["Day2Value"]);
                        objDashboard.Day3Value = Convert.ToDecimal(drData["Day3Value"]);
                        objDashboard.Day4Value = Convert.ToDecimal(drData["Day4Value"]);
                        objDashboard.Day5Value = Convert.ToDecimal(drData["Day5Value"]);
                        objDashboard.Day6Value = Convert.ToDecimal(drData["Day6Value"]);
                        objDashboard.Day7Value = Convert.ToDecimal(drData["Day7Value"]);
                        objDashboard.Day1OldValue = Convert.ToDecimal(drData["Day1OldValue"]);
                        objDashboard.Day2OldValue = Convert.ToDecimal(drData["Day2OldValue"]);
                        objDashboard.Day3OldValue = Convert.ToDecimal(drData["Day3OldValue"]);
                        objDashboard.Day4OldValue = Convert.ToDecimal(drData["Day4OldValue"]);
                        objDashboard.Day5OldValue = Convert.ToDecimal(drData["Day5OldValue"]);
                        objDashboard.Day6OldValue = Convert.ToDecimal(drData["Day6OldValue"]);
                        objDashboard.Day7OldValue = Convert.ToDecimal(drData["Day7OldValue"]);
                        objDashboard.Q1 = Convert.ToString(drData["Q1"]);
                        objDashboard.Q2 = Convert.ToString(drData["Q2"]);
                        objDashboard.Q3 = Convert.ToString(drData["Q3"]);
                        objDashboard.Q4 = Convert.ToString(drData["Q4"]);
                        objDashboard.Q5 = Convert.ToString(drData["Q5"]);
                        objDashboard.Q6 = Convert.ToString(drData["Q6"]);
                        objDashboard.Q7 = Convert.ToString(drData["Q7"]);
                        objDashboard.Q8 = Convert.ToString(drData["Q8"]);
                        objDashboard.Q9 = Convert.ToString(drData["Q9"]);
                        objDashboard.Q10 = Convert.ToString(drData["Q10"]);
                        objDashboard.Q11 = Convert.ToString(drData["Q11"]);
                        objDashboard.Q12 = Convert.ToString(drData["Q12"]);
                        objDashboard.Q1Value = Convert.ToDecimal(drData["Q1Value"]);
                        objDashboard.Q2Value = Convert.ToDecimal(drData["Q2Value"]);
                        objDashboard.Q3Value = Convert.ToDecimal(drData["Q3Value"]);
                        objDashboard.Q4Value = Convert.ToDecimal(drData["Q4Value"]);
                        objDashboard.Q5Value = Convert.ToDecimal(drData["Q5Value"]);
                        objDashboard.Q6Value = Convert.ToDecimal(drData["Q6Value"]);
                        objDashboard.Q7Value = Convert.ToDecimal(drData["Q7Value"]);
                        objDashboard.Q8Value = Convert.ToDecimal(drData["Q8Value"]);
                        objDashboard.Q9Value = Convert.ToDecimal(drData["Q9Value"]);
                        objDashboard.Q10Value = Convert.ToDecimal(drData["Q10Value"]);
                        objDashboard.Q11Value = Convert.ToDecimal(drData["Q11Value"]);
                        objDashboard.Q12Value = Convert.ToDecimal(drData["Q12Value"]);
                        objDashboard.Month1 = Convert.ToString(drData["Month1"]);
                        objDashboard.Month2 = Convert.ToString(drData["Month2"]);
                        objDashboard.Month3 = Convert.ToString(drData["Month3"]);
                        objDashboard.Month4 = Convert.ToString(drData["Month4"]);
                        objDashboard.Month5 = Convert.ToString(drData["Month5"]);
                        objDashboard.Month6 = Convert.ToString(drData["Month6"]);
                        objDashboard.Month7 = Convert.ToString(drData["Month7"]);
                        objDashboard.Month8 = Convert.ToString(drData["Month8"]);
                        objDashboard.Month9 = Convert.ToString(drData["Month9"]);
                        objDashboard.Month10 = Convert.ToString(drData["Month10"]);
                        objDashboard.Month11 = Convert.ToString(drData["Month11"]);
                        objDashboard.Month12 = Convert.ToString(drData["Month12"]);
                        objDashboard.Silk1Value = Convert.ToDecimal(drData["Silk1Value"]);
                        objDashboard.Silk2Value = Convert.ToDecimal(drData["Silk2Value"]);
                        objDashboard.Silk3Value = Convert.ToDecimal(drData["Silk3Value"]);
                        objDashboard.Silk4Value = Convert.ToDecimal(drData["Silk4Value"]);
                        objDashboard.Silk5Value = Convert.ToDecimal(drData["Silk5Value"]);
                        objDashboard.Silk6Value = Convert.ToDecimal(drData["Silk6Value"]);
                        objDashboard.Silk7Value = Convert.ToDecimal(drData["Silk7Value"]);
                        objDashboard.Silk8Value = Convert.ToDecimal(drData["Silk8Value"]);
                        objDashboard.Silk9Value = Convert.ToDecimal(drData["Silk9Value"]);
                        objDashboard.Silk10Value = Convert.ToDecimal(drData["Silk10Value"]);
                        objDashboard.Silk11Value = Convert.ToDecimal(drData["Silk11Value"]);
                        objDashboard.Silk12Value = Convert.ToDecimal(drData["Silk12Value"]);
                        objDashboard.Cotton1Value = Convert.ToDecimal(drData["Cotton1Value"]);
                        objDashboard.Cotton2Value = Convert.ToDecimal(drData["Cotton2Value"]);
                        objDashboard.Cotton3Value = Convert.ToDecimal(drData["Cotton3Value"]);
                        objDashboard.Cotton4Value = Convert.ToDecimal(drData["Cotton4Value"]);
                        objDashboard.Cotton5Value = Convert.ToDecimal(drData["Cotton5Value"]);
                        objDashboard.Cotton6Value = Convert.ToDecimal(drData["Cotton6Value"]);
                        objDashboard.Cotton7Value = Convert.ToDecimal(drData["Cotton7Value"]);
                        objDashboard.Cotton8Value = Convert.ToDecimal(drData["Cotton8Value"]);
                        objDashboard.Cotton9Value = Convert.ToDecimal(drData["Cotton9Value"]);
                        objDashboard.Cotton10Value = Convert.ToDecimal(drData["Cotton10Value"]);
                        objDashboard.Cotton11Value = Convert.ToDecimal(drData["Cotton11Value"]);
                        objDashboard.Cotton12Value = Convert.ToDecimal(drData["Cotton12Value"]);
                        objDashboard.Saleduration = Convert.ToString(drData["Saleduration"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Dashboard GetDashboardCount | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objDashboard;
        }
        
    }
}
