using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;

namespace VHMS.DataAccess.Discharge
{
    public class DischargeReport
    {
        public static DataSet GetDischargeSummary(int AdmissionID)
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;

            try
            {
                db = Entity.DBConnection.dbCon;
                //DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_DISCHARGESUMMARY);
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.RPT_SELECT_PRESCRIPTIONREPORT);
                db.AddInParameter(cmd, "@FK_PrescriptionID", DbType.Int32, AdmissionID);
                dsList = db.ExecuteDataSet(cmd);
            }
            catch (Exception ex)
            {
                sException = "DischargeReport GetDischargeSummary | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return dsList;
        }
    }
}
