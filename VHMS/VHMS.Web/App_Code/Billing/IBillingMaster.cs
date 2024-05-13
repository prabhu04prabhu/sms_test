using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.Collections.ObjectModel;
using VHMS.Entity;
using System.IO;


public partial interface IVHMSService
{

    #region Patient
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPatient();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetpatientName();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPatientID();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSearchpatient(string ID);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetLastOPDNo();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPatientDetails(VHMS.Entity.PatientFilter oJobCardFilter);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPatientByID(int ID);
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPatientByOPDNo(string ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddPatient(VHMS.Entity.patient Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdatePatient(VHMS.Entity.patient Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeletePatient(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchPatient(string key);

    //[OperationContract]
    //[WebInvoke(ResponseFormat = WebMessageFormat.Json, UriTemplate = "PostImage", Method = "POST")]
    //string PostImage(Stream sm);

    #endregion

    #region WareHouse
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetWareHouse();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetWareHouseByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddWareHouse(VHMS.Entity.Billing.WareHouse Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateWareHouse(VHMS.Entity.Billing.WareHouse Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteWareHouse(int ID);
    #endregion

    #region Unit
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetUnit();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetUnitByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddUnit(VHMS.Entity.Billing.Unit Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateUnit(VHMS.Entity.Billing.Unit Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteUnit(int ID);
    #endregion

    #region Category
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetCategory();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetCategoryByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddCategory(VHMS.Entity.Billing.Category Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateCategory(VHMS.Entity.Billing.Category Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteCategory(int ID);
    #endregion

    #region ProductType
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetProductType();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetProductTypeByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddProductType(VHMS.Entity.Billing.ProductType Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateProductType(VHMS.Entity.Billing.ProductType Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteProductType(int ID);
    #endregion

    #region Tax
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTax();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTaxByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddTax(VHMS.Entity.Billing.Tax Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateTax(VHMS.Entity.Billing.Tax Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteTax(int ID);
    #endregion

    #region Agent
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAgent();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAgentByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddAgent(VHMS.Entity.Billing.Agent Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateAgent(VHMS.Entity.Billing.Agent Objdata);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string UpdateWeaverWage(VHMS.Entity.Billing.WeaverWage Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteAgent(int ID);
    #endregion

    #region SurgeryType
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSurgeryType();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSurgeryTypeByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddSurgeryType(VHMS.Entity.Billing.SurgeryType Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateSurgeryType(VHMS.Entity.Billing.SurgeryType Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteSurgeryType(int ID);
    #endregion

    #region SurgicalProcedure
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSurgicalProcedure();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSurgicalProcedureByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddSurgicalProcedure(VHMS.Entity.Billing.SurgicalProcedure Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateSurgicalProcedure(VHMS.Entity.Billing.SurgicalProcedure Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteSurgicalProcedure(int ID);
    #endregion

    #region Anesthesia
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAnesthesia();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAnesthesiaByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddAnesthesia(VHMS.Entity.Billing.Anesthesia Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateAnesthesia(VHMS.Entity.Billing.Anesthesia Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteAnesthesia(int ID);
    #endregion

    #region Supplier
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSupplier();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAllSupplier(int iSupplierID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSupplierByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddSupplier(VHMS.Entity.Billing.Supplier Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateSupplier(VHMS.Entity.Billing.Supplier Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteSupplier(int ID);
    #endregion

    #region Vendor
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetVendor();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetVendorByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddVendor(VHMS.Entity.Billing.Vendor Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateVendor(VHMS.Entity.Billing.Vendor Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteVendor(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetVendorWorkID(int VendorID);
    #endregion

    #region Prescription
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPrescription(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPrescriptionByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddPrescription(VHMS.Entity.Billing.PrescriptionMaster Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdatePrescription(VHMS.Entity.Billing.PrescriptionMaster Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeletePrescription(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPrescriptionSummary();

    #endregion

    #region OPBilling
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetOPBilling(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetOPBillingID(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchOPBilling(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetOPBillingByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddOPBilling(VHMS.Entity.Billing.OPBillingMaster Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateOPBilling(VHMS.Entity.Billing.OPBillingMaster Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteOPBilling(int ID, string Reason);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetOPBillingSummary();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetOPBillingReport(VHMS.Entity.Billing.OPBillingFilter oJobCardFilter);

    #endregion

    #region Sales
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSales(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopSales(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesID(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchSales(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesByInvoice(string InvoiceNo);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddSales(VHMS.Entity.Billing.Sales Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateSales(VHMS.Entity.Billing.Sales Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteSales(int ID, string Reason);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesSummary();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesReport(VHMS.Entity.Billing.SalesFilter oJobCardFilter);

    #endregion

    #region Register
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetRegister(int RegisterID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetRegisterNotification();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetRegisterByStatus(int ID = 0, string Status = null);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetClosedRegisterByStatus(int ID = 0, string Status = null);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchRegister(string ID = null);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchChitClosed(string ID = null);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetRegisterByID(int ID);
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetRegisterByNo(string ID, string IsActive);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddRegister(VHMS.Entity.Billing.Register Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateRegister(VHMS.Entity.Billing.Register Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateCancelledRegister(VHMS.Entity.Billing.Register Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteRegister(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetCancelledRegister(int RegisterID = 0);
    #endregion

    #region Quotation
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetQuotation(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopQuotation(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetQuotationID(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchQuotation(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetQuotationByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetQuotationByInvoice(string InvoiceNo);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddQuotation(VHMS.Entity.Billing.Quotation Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateQuotation(VHMS.Entity.Billing.Quotation Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteQuotation(int ID, string Reason);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetQuotationSummary();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetQuotationReport(VHMS.Entity.Billing.QuotationFilter oJobCardFilter);

    #endregion

    #region Renewal
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetRenewal();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetRenewalByID(int ID);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchRenewal(string ID = null);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddRenewal(VHMS.Entity.Billing.Renewal Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateRenewal(VHMS.Entity.Billing.Renewal Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteRenewal(int ID);
    #endregion

    #region Exchange
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetExchange(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopExchange(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetExchangeID(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchExchange(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetExchangeByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddExchange(VHMS.Entity.Billing.Exchange Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateExchange(VHMS.Entity.Billing.Exchange Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteExchange(int ID, string Reason);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetExchangeSummary();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetExchangeReport(VHMS.Entity.Billing.ExchangeFilter oJobCardFilter);

    #endregion

    #region SalesReturn
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesReturn(int PublisherID = 0, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopSalesReturn(int PublisherID = 0, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesReturnID(int PublisherID = 0, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchSalesReturn(string ID = null, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesReturnJsonHSNFormat(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesReturnByID(int ID, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesReturnByInvoice(string InvoiceNo);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddSalesReturn(VHMS.Entity.Billing.SalesReturn Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateSalesReturn(VHMS.Entity.Billing.SalesReturn Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteSalesReturn(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesReturnSummary();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesReturnReport(VHMS.Entity.Billing.SalesReturnFilter oJobCardFilter);

    #endregion

    #region TDSPayment
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTDSPayment(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopTDSPayment(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTDSPaymentID(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchTDSPayment(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTDSPaymentByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTDSPaymentByInvoice(string InvoiceNo);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddTDSPayment(VHMS.Entity.Billing.TDSPayment Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateTDSPayment(VHMS.Entity.Billing.TDSPayment Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteTDSPayment(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTDSPaymentSummary();

    #endregion

    #region BranchMove
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetBranchMove(int BranchMoveID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopBranchMove(int BranchMoveID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetBranchMoveID(int BranchMoveID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchBranchMove(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetBranchMoveByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetBranchMoveByInvoice(string InvoiceNo);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddBranchMove(VHMS.Entity.Billing.BranchMove Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateBranchMove(VHMS.Entity.Billing.BranchMove Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteBranchMove(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateMoveStatus(int ID, string Status);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetBranchMoveSummary();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetBranchMoveReport(VHMS.Entity.Billing.BranchMoveFilter oJobCardFilter);

    #endregion

    #region StockCheck
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetStockCheck(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopStockCheck(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetStockCheckID(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchStockCheck(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetStockCheckByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetStockCheckByInvoice(string InvoiceNo);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddStockCheck(VHMS.Entity.Billing.StockCheck Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateStockCheck(VHMS.Entity.Billing.StockCheck Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteStockCheck(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetStockCheckSummary();



    #endregion

    #region Inward
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetInward(int PublisherID = 0);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingID(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchInward(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetInwardByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddInward(VHMS.Entity.Billing.Inward Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateInward(VHMS.Entity.Billing.Inward Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteInward(int ID);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingSummary();

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingReport(VHMS.Entity.Billing.OPBillingFilter oJobCardFilter);

    #endregion

    #region PurchaseOrder
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseOrder(int PublisherID = 0);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingID(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchPurchaseOrder(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseOrderByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddPurchaseOrder(VHMS.Entity.Billing.PurchaseOrder Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdatePurchaseOrder(VHMS.Entity.Billing.PurchaseOrder Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeletePurchaseOrder(int ID);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingSummary();

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingReport(VHMS.Entity.Billing.OPBillingFilter oJobCardFilter);

    #endregion

    #region Purchase
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchase(int PublisherID = 0, int BillType = 1);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopPurchase(int PublisherID = 0, int BillType = 1);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAdjustTDSPurchase(int ipatientID = 0, int iSupplierID = 0, int iTDSID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTDSPurchase(int ipatientID = 0, int iSupplierID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopPurchaseSupplierWise(int iSupplierID = 0, int BillType = 1, int DC = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopPurchasePending(int PublisherID = 0, int BillType = 1);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPendingPurchase(int PublisherID = 0, int BillType = 1);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchPurchase(string ID = null, int BillType = 1, int DC = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchPurchasePending(string ID = null, int BillType = 1);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetBillNo(int SupplierID = 0, int BillType = 1, int PurchaseReturnID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseDiscountBillNo(int SupplierID = 0, int BillType = 1, int PurchaseReturnID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPendingPurchaseDiscountBillNo(int SupplierID = 0, int BillType = 1, int PurchaseReturnID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseByID(int ID, int BillType = 1);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTDSPurchaseByID(int iPurchaseID, int BillType = 1);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetDCPurchase(int PublisherID = 0, int BillType = 1);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopDCPurchase(int PublisherID = 0, int BillType = 1);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPendingDCPurchase(int PublisherID = 0, int BillType = 1);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchDCPurchase(string ID = null, int BillType = 1);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetDCPurchaseByID(int ID, int BillType = 1);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseInvoice(string InvoiceNo);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddPurchase(VHMS.Entity.Billing.Purchase Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdatePurchase(VHMS.Entity.Billing.Purchase Objdata);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdatePurchasePending(VHMS.Entity.Billing.Purchase Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeletePurchase(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string CheckdeleteStockByProductID(int iProductID, decimal iQty = 0, int iPurchaseTransID = 0);


    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingSummary();

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingReport(VHMS.Entity.Billing.OPBillingFilter oJobCardFilter);

    #endregion

    #region SalesOrder
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesOrder(int PublisherID = 0);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingID(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchSalesOrder(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesOrderByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddSalesOrder(VHMS.Entity.Billing.SalesOrder Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateSalesOrder(VHMS.Entity.Billing.SalesOrder Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteSalesOrder(int ID);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingSummary();

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingReport(VHMS.Entity.Billing.OPBillingFilter oJobCardFilter);

    #endregion

    #region SalesEntry
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesEntry(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTDSSalesEntry(int PublisherID = 0, int TDSSalesID = 0, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAdjustTDSSalesEntry(int PublisherID = 0, int TDSSalesID = 0, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetLastInvoiceNo();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPendingRetailSales();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPendingSalesDiscountBillNo(int CustomerID = 0, int IsRetailBill = 1, int SalesReturnID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopSalesEntry(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0, int iCustomerID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopRetailsSalesEntry(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0, int iCustomerID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopSalesEntryDeleteList(int PublisherID = 0, int IsRetail = 0, int iCustomerID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesEntryBookingBill(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchSalesEntryBokingBill(string ID = null, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPendingSalesEntry(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAmountClearEntry(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPendingRetailBills(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchSalesEntry(string ID = null, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchSalesEntryDeleteList(string ID = null, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesEntryByInvoice(string InvoiceNo, int IsRetail);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesEntryByInvoiceReturn(string InvoiceNo, int SalesReturnID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesEntryByID(int ID, int IsRetail = 0, int IsYarnBill = 0);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTDSSalesEntryByID(int ID, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesEntryJsonFormat(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesEntryJsonHSNFormat(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesEntryReturnByID(int ID, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetLastSalesEntryByID(int ID, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddSalesEntry(VHMS.Entity.Billing.SalesEntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateSalesEntry(VHMS.Entity.Billing.SalesEntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteSalesEntry(int ID, string Reason);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingSummary();

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingReport(VHMS.Entity.Billing.OPBillingFilter oJobCardFilter);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTransStockByID(int ID, int transaction_id, string type);

    #endregion

    #region YarnSalesEntry
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetYarnSalesEntry(int PublisherID = 0, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopYarnSalesEntry(int PublisherID = 0, int IsRetail = 0, int iCustomerID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchYarnSalesEntry(string ID = null, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetYarnSalesEntryByID(int ID, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddYarnSalesEntry(VHMS.Entity.Billing.YarnSalesEntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateYarnSalesEntry(VHMS.Entity.Billing.YarnSalesEntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteYarnSalesEntry(int ID, string Reason);

    #endregion

    #region EstimateSalesEntry
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetEstimateSalesEntry(int PublisherID = 0, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPendingRetailEstimateSales();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopEstimateSalesEntry(int PublisherID = 0, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetEstimateSalesEntryBookingBill(int PublisherID = 0, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPendingEstimateSalesEntry(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetEstimateSalesEntryByInvoice(string InvoiceNo);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchEstimateSalesEntry(string ID = null, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchEstimateSalesEntryBokingBill(string ID = null, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetEstimateSalesEntryByID(int ID, int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddEstimateSalesEntry(VHMS.Entity.Billing.EstimateSalesEntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateEstimateSalesEntry(VHMS.Entity.Billing.EstimateSalesEntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteEstimateSalesEntry(int ID, string Reason);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateEstimateSalesStatus(int ID);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingSummary();

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingReport(VHMS.Entity.Billing.OPBillingFilter oJobCardFilter);

    #endregion

    #region PurchaseReturn
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseReturn(int PublisherID = 0, int iSupplierID = 0, int BillType = 1);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchPurchaseReturn(string ID = null, int BillType = 1);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseReturnJsonHSNFormat(int ID);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseReturnByID(int ID, int Type);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddPurchaseReturn(VHMS.Entity.Billing.PurchaseReturn Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdatePurchaseReturn(VHMS.Entity.Billing.PurchaseReturn Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeletePurchaseReturn(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetQuantity(int ID = 0, int PurchaseID = 0, int SupplierID = 0);

    #endregion

    #region ProcessingInward
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetProcessingInward(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPendingProcessingInward(int WorkID = 0, int VendorID = 0);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingID(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchProcessingInward(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetProcessingInwardByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddProcessingInward(VHMS.Entity.Billing.ProcessingInward Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateProcessingInward(VHMS.Entity.Billing.ProcessingInward Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteProcessingInward(int ID);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingSummary();

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingReport(VHMS.Entity.Billing.OPBillingFilter oJobCardFilter);

    #endregion

    #region ProcessingOutward
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetProcessingOutward(int PublisherID = 0);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingID(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchProcessingOutward(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetProcessingOutwardByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddProcessingOutward(VHMS.Entity.Billing.ProcessingOutward Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateProcessingOutward(VHMS.Entity.Billing.ProcessingOutward Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteProcessingOutward(int ID);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingSummary();

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingReport(VHMS.Entity.Billing.OPBillingFilter oJobCardFilter);

    #endregion

    #region Payment
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPayment(int Type);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPaymentByID(int ID, int Type);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetLastPaymentDetails(int ID, int Type = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddPayment(VHMS.Entity.Billing.Payment Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdatePayment(VHMS.Entity.Billing.Payment Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeletePayment(int ID);
    #endregion

    #region Receipt
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetReceipt(int IsRetail);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetLastReceiptDetails(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetReceiptByStatus(string Status);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetReceiptByID(int ID, int IsRetail);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetOnAccountAmount(int ID, string Type);



    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddReceipt(VHMS.Entity.Receipt Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateReceipt(VHMS.Entity.Receipt Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteReceipt(int ID);
    #endregion

    #region Advance
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAdvance(int IsRetail = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAdvanceByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAdvanceOutSalary(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAdvanceInSalary(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetEmployeeAdvanceAmount(string FromDate = "", string ToDate = "", int EmployeeID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAdvanceAmount(string FromDate = "", string ToDate = "", int EmployeeID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddAdvance(VHMS.Entity.Advance Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateAdvance(VHMS.Entity.Advance Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteAdvance(int ID);
    #endregion

    #region AttendanceLog
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAttendanceLog(int CountryID = 0, string FromDate = "", string ToDate = "", int iEmployeeID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAttendanceLogByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetEmployeeAttendanceLogByID(int EmployeeID = 0, string FromDate = "", string ToDate = "");

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddAttendanceLog(VHMS.Entity.AttendanceLog Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateAttendanceLog(VHMS.Entity.AttendanceLog Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteAttendanceLog(int ID);
    #endregion

    #region Salary
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalary(int CountryID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalaryByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetEmployeeSalaryCount(string MonthYear, int iEmployeeID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddSalary(VHMS.Entity.Salary Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateSalary(VHMS.Entity.Salary Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteSalary(int ID);
    #endregion

    #region VendorPayment
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetVendorPayment();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetVendorPaymentByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddVendorPayment(VHMS.Entity.Billing.VendorPayment Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateVendorPayment(VHMS.Entity.Billing.VendorPayment Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteVendorPayment(int ID);
    #endregion

    #region Journal
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetJournal(int PublisherID = 0);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingID(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchJournal(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetJournalByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddJournal(VHMS.Entity.Billing.Journal Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateJournal(VHMS.Entity.Billing.Journal Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteJournal(int ID);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingSummary();

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetOPBillingReport(VHMS.Entity.Billing.OPBillingFilter oJobCardFilter);

    #endregion

    #region BankEntry
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetBankEntry();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetBankEntryByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddBankEntry(VHMS.Entity.Billing.BankEntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateBankEntry(VHMS.Entity.Billing.BankEntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteBankEntry(int ID);
    #endregion

    #region ExchangeReceipt
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetExchangeReceipt();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetExchangeReceiptByStatus(string Status);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetExchangeReceiptByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddExchangeReceipt(VHMS.Entity.ExchangeReceipt Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateExchangeReceipt(VHMS.Entity.ExchangeReceipt Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteExchangeReceipt(int ID);
    #endregion

    #region Expense
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetExpense(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTDSExpense(int ipatientID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetAdjustTDSExpense(int ipatientID = 0, int iTDSID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPendingExpense(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchExpense(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetExpenseByID(int ID);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTDSExpenseByID(int iExpenseID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddExpense(VHMS.Entity.Billing.Expense Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateExpense(VHMS.Entity.Billing.Expense Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteExpense(int ID);

    #endregion

    #region LREntry
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetLREntry(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchLREntry(string ID = null);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetLREntryByID(int ID);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetLREntrySalesID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddLREntry(VHMS.Entity.Billing.LREntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateLREntry(VHMS.Entity.Billing.LREntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateLREntryStatus(VHMS.Entity.Billing.LREntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteLREntry(int ID);

    #endregion

    #region INLREntry
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetINLREntry(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchINLREntry(string ID = null);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetINLREntryByID(int ID);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetINLREntrySalesID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddINLREntry(VHMS.Entity.Billing.INLREntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateINLREntry(VHMS.Entity.Billing.INLREntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateINLREntryStatus(VHMS.Entity.Billing.INLREntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteINLREntry(int ID);

    #endregion

    #region Others
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetProductRate(int ID = 0, string type = null, int SupplierID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetWholeSaleProductRate(int ID = 0, string type = null, int SupplierID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetRetailProductRate(int ID = 0, string type = null, int SupplierID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetProductDetails(int ID = 0, int type = 0, int SupplierID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetProductDetailsBySMSCode(int ID = 0, int type = 0, int SupplierID = 0, string iSMSCode = null);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetProductDetailsEstimate(int ID = 0, int type = 0, int SupplierID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetProductDetailsEstimateBySMSCoode(int ID = 0, int type = 0, int SupplierID = 0, string iSMSCode = null);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetNewProductDetails(int ID = 0, string code = null, int type = 0, int SupplierID = 0, int SalesEntryID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseNewProductDetails(int ID = 0, string code = null, int type = 0, int SupplierID = 0, int SalesEntryID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetRateByProduct(int ID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetCurrentStock(int ID = 0, int CategoryID = 0, int SubCategoryID = 0, int SupplierID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetProductDetailsPurchase(int ID = 0, int type = 0, int SupplierID = 0);

    #endregion

    #region VendorEntry
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetVendorEntry(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchVendorEntry(string ID = null);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetVendorEntryByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetVendorStockCheck(int iVendorID, int iWorkID, int iVendorEntryID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetVendorEntryByStatus(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetVendorEntryByOpeningQty(int ID, int iVendorEntryID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddVendorEntry(VHMS.Entity.Billing.VendorEntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateVendorEntry(VHMS.Entity.Billing.VendorEntry Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteVendorEntry(int ID);

    #endregion

    #region ExpensePayment
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetExpensePayment(int Type, int iPartyID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetExpensePaymentByID(int ID, int Type);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetLastExpensePaymentDetails(int ID, int Type = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddExpensePayment(VHMS.Entity.Billing.ExpensePayment Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateExpensePayment(VHMS.Entity.Billing.ExpensePayment Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteExpensePayment(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetExpenseBalanceAmount(int ID, string Type);





    #endregion

    #region PurchaseDiscount
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseDiscount(int PublisherID = 0, int iSupplierID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopPurchaseDiscount(int PublisherID = 0, int iSupplierID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSearchPurchaseDiscount(string PublisherID, int iSupplierID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddPurchaseDiscount(VHMS.Entity.Billing.PurchaseDiscount Objdata);
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdatePurchaseDiscount(VHMS.Entity.Billing.PurchaseDiscount Objdata);
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseDiscountByID(int ID, int Type);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeletePurchaseDiscount(int ID);









    #endregion

    #region SalesDiscount
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesDiscount(int PublisherID = 0, int iSupplierID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopSalesDiscount(int PublisherID = 0, int iSupplierID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSearchSalesDiscount(String PublisherID, int iSupplierID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddSalesDiscount(VHMS.Entity.Billing.SalesDiscount Objdata);
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateSalesDiscount(VHMS.Entity.Billing.SalesDiscount Objdata);
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesDiscountByID(int ID, int Type);
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetSalesDiscountBillNo(int iCustomerID = 0, int SalesReturnID = 0, int IsRetailBill = 1, int IsActive = 1);
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteSalesDiscount(int ID);
    #endregion

    #region Outward
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetOutward(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopOutward(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0, int iCustomerID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchOutward(string ID = null, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetOutwardByID(int ID, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetLastOutwardByID(int ID, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddOutward(VHMS.Entity.Billing.Outward Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateOutward(VHMS.Entity.Billing.Outward Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteOutward(int ID, string Reason);
    #endregion

    #region RetailOutward
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetRetailOutward(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopRetailOutward(int PublisherID = 0, int IsRetail = 0, int IsYarnBill = 0, int iCustomerID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchRetailOutward(string ID = null, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetRetailOutwardByID(int ID, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetLastRetailOutwardByID(int ID, int IsRetail = 0, int IsYarnBill = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddRetailOutward(VHMS.Entity.Billing.RetailOutward Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateRetailOutward(VHMS.Entity.Billing.RetailOutward Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteRetailOutward(int ID, string Reason);
    #endregion

    #region PunchLog
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPunchLog(int CountryID = 0, string FromDate = "", string ToDate = "", int iEmployeeID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPunchLogByID(int ID);

    //[OperationContract]
    //[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    //string GetPunchLogByDynamic(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddPunchLog(VHMS.Entity.PunchLog Objdata);
    #endregion

    #region PurchaseTDSPayment
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseTDSPayment(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetTopPurchaseTDSPayment(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseTDSPaymentID(int PublisherID = 0);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string SearchPurchaseTDSPayment(string ID = null);


    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetPurchaseTDSPaymentByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddPurchaseTDSPayment(VHMS.Entity.Billing.PurchaseTDSPayment Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdatePurchaseTDSPayment(VHMS.Entity.Billing.PurchaseTDSPayment Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeletePurchaseTDSPayment(int ID);


    #endregion

    #region HSNMaster
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetHSNMaster();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetHSNMasterByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddHSNMaster(VHMS.Entity.Billing.HSNMaster Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateHSNMaster(VHMS.Entity.Billing.HSNMaster Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteHSNMaster(int ID);
    #endregion

    #region BuyerContactType
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetBuyerContactType();

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string GetBuyerContactTypeByID(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string AddBuyerContactType(VHMS.Entity.Billing.BuyerContactType Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string UpdateBuyerContactType(VHMS.Entity.Billing.BuyerContactType Objdata);

    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
    string DeleteBuyerContactType(int ID);
    #endregion
}