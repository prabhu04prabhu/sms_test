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
    public class Template
    {
        public static Collection<Entity.Template> GetTemplate()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.Template> objList = new Collection<Entity.Template>();
            Entity.Template objTemplate = new Entity.Template();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TEMPLATE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objTemplate = new Entity.Template();

                        objTemplate.TemplateID = Convert.ToInt32(drData["PK_TemplateID"]);
                        objTemplate.TemplateName = Convert.ToString(drData["TemplateName"]);
                        objTemplate.TemplateMessage = Convert.ToString(drData["TemplateMessage"]);

                        objList.Add(objTemplate);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Template GetTemplate | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.Template GetTemplateByID(int iTemplateID)
        {
            string sException = string.Empty;
            Database db;
            Entity.Template objTemplate = new Entity.Template();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_TEMPLATE);
                db.AddInParameter(cmd, "@PK_TemplateID", DbType.Int32, iTemplateID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objTemplate = new Entity.Template();

                        objTemplate.TemplateID = Convert.ToInt32(drData["PK_TemplateID"]);
                        objTemplate.TemplateName = Convert.ToString(drData["TemplateName"]);
                        objTemplate.TemplateMessage = Convert.ToString(drData["TemplateMessage"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Template GetTemplateByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objTemplate;
        }
        public static bool UpdateTemplate(Entity.Template objTemplate)
        {
            bool IsUpdated = true;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsUpdated = UpdateTemplate(oDb, objTemplate, oTrans);
                    oTrans.Commit();
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
        private static bool UpdateTemplate(Database oDb, Entity.Template objTemplate, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iID = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_TEMPLATE);
                oDb.AddInParameter(cmd, "@PK_TemplateID", DbType.Int32, objTemplate.TemplateID);
                oDb.AddInParameter(cmd, "@TemplateName", DbType.String, objTemplate.TemplateName);
                oDb.AddInParameter(cmd, "@TemplateMessage", DbType.String, objTemplate.TemplateMessage);

                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.Template UpdateTemplate | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
