namespace constants
{
    public class StoredProcedures
    {
        //User
        public const string USP_SELECT_USERLOGIN = "usp_Select_UserLogin";
        public const string USP_SELECT_MODULE = "usp_Select_Module";
        public const string USP_CHANGEPASSWORD = "usp_ChangePassword";

        public const string USP_INSERT_USER = "usp_Insert_User";
        public const string USP_UPDATE_USER = "usp_Update_User";
        public const string USP_DELETE_USER = "usp_Delete_User";
        public const string USP_SELECT_USER = "usp_Select_User";
        public const string USP_SELECT_USERSTATUS = "usp_Select_UserStatus";
        public const string USP_INSERT_LOG = "usp_Insert_Log";
        public const string USP_SELECT_LASTINVOICENO = "usp_Select_LastInvoiceNo";
        public const string USP_INSERT_VISITLOG = "usp_Insert_VisitLog";
        public const string USP_SELECT_VISITLOG = "usp_Select_VisitLog";
        public const string USP_SELECT_PURCHASEBARCODE = "usp_Select_PurchaseBarcode";
        public const string USP_SELECT_WHOLESALESTOTALAMOUNT = "usp_Select_WholeSalesTotalAmount";
        public const string USP_SELECT_TEMPLATE = "usp_Select_Template";
        public const string USP_UPDATE_TEMPLATE = "usp_Update_Template";

        //Dashboard
        public const string USP_SELECT_DASHBOARDCOUNT = "usp_Select_DashboardCount";
        public const string USP_SELECT_DASHBOARDWEEKLYSALES = "usp_Select_DashboardWeeklySales";

        //Settings
        public const string USP_UPDATE_SETTINGS = "usp_Update_Settings";
        public const string USP_SELECT_SETTINGS = "usp_Select_Settings";
        public const string USP_SELECT_LEDGERCUSTOMER = "usp_Select_LedgerCustomer";
        public const string USP_SELECT_LEDGERRETAILSCUSTOMER = "usp_Select_LedgerRetailsCustomer";
        public const string USP_SELECT_SALESRETURNCUSTOMER = "usp_Select_SalesReturnCustomer";
        public const string RPT_SELECT_PAYMENT = "RPT_Select_Payment";
        public const string RPT_SELECT_RECEIPT = "RPT_Select_Receipt";
        public const string RPT_SELECT_EXPENSEPAYMENT = "RPT_Select_ExpensePayment";
        //Area
        public const string USP_SELECT_AREA = "usp_Select_Area";

        //Roles
        public const string USP_INSERT_ROLE = "usp_Insert_Role";
        public const string USP_UPDATE_ROLE = "usp_Update_Role";
        public const string USP_DELETE_ROLE = "usp_Delete_Role";
        public const string USP_DELETE_PERMISSIONBYROLEID = "usp_Delete_PermissionByRoleID";
        public const string USP_SELECT_ROLE = "usp_Select_Role";

        //ShippingAddresss
        public const string USP_INSERT_SHIPPINGADDRESS = "usp_Insert_ShippingAddress";
        public const string USP_UPDATE_SHIPPINGADDRESS = "usp_Update_ShippingAddress";
        public const string USP_DELETE_SHIPPINGADDRESS = "usp_Delete_ShippingAddress";
        public const string USP_SELECT_SHIPPINGADDRESS = "usp_Select_ShippingAddress";

        //LedgerType
        public const string USP_INSERT_LEDGERTYPE = "usp_Insert_LedgerType";
        public const string USP_UPDATE_LEDGERTYPE = "usp_Update_LedgerType";
        public const string USP_DELETE_LEDGERTYPE = "usp_Delete_LedgerType";
        public const string USP_SELECT_LEDGERTYPE = "usp_Select_LedgerType";

        //Work
        public const string USP_INSERT_WORK = "usp_Insert_Work";
        public const string USP_UPDATE_WORK = "usp_Update_Work";
        public const string USP_DELETE_WORK = "usp_Delete_Work";
        public const string USP_SELECT_WORK = "usp_Select_Work";
        public const string USP_SELECT_WORKRATE = "usp_Select_WorkRate";
        public const string USP_SELECT_VENDORWORKTRANSBYAMOUNT = "usp_Select_VendorWorkTransbyAmount";

        //Ledger
        public const string USP_INSERT_LEDGER = "usp_Insert_Ledger";
        public const string USP_UPDATE_LEDGER = "usp_Update_Ledger";
        public const string USP_DELETE_LEDGER = "usp_Delete_Ledger";
        public const string USP_SELECT_LEDGER = "usp_Select_Ledger";
        public const string USP_SELECT_PARTYLEDGER = "usp_Select_PartyLedger";
        public const string USP_SELECT_EXPENSELEDGER = "usp_Select_ExpenseLedger";
        public const string USP_SELECT_LEDGERBANK = "usp_Select_LedgerBank";
        public const string USP_SELECT_LEDGEREXPENSE = "usp_Select_LedgerExpense";

        //SECTION
        public const string USP_INSERT_SECTION = "usp_Insert_Section";
        public const string USP_UPDATE_SECTION = "usp_Update_Section";
        public const string USP_DELETE_SECTION = "usp_Delete_Section";
        public const string USP_SELECT_SECTION = "usp_Select_Section";

        //Agent
        public const string USP_INSERT_AGENTMASTER = "usp_Insert_AgentMaster";
        public const string USP_UPDATE_AGENTMASTER = "usp_Update_AgentMaster";
        public const string USP_DELETE_AGENTMASTER = "usp_Delete_AgentMaster";
        public const string USP_SELECT_AGENTMASTER = "usp_Select_AgentMaster";

        //CashTills
        public const string USP_INSERT_CASHTILL = "usp_Insert_CashTill";
        public const string USP_UPDATE_CASHTILL = "usp_Update_CashTill";
        public const string USP_DELETE_CASHTILL = "usp_Delete_CashTill";
        public const string USP_SELECT_CASHTILL = "usp_Select_CashTill";
        public const string USP_SELECT_TOPCASHTILL = "usp_Select_TopCashTill";
        public const string USP_SEARCH_CASHTILL = "usp_Search_CashTill";

        //Registers
        public const string USP_INSERT_REGISTER = "usp_Insert_Register";
        public const string USP_UPDATE_REGISTER = "usp_Update_Register";
        public const string USP_DELETE_REGISTER = "usp_Delete_Register";
        public const string USP_SELECT_REGISTER = "usp_Select_Register";
        public const string USP_SELECT_CLOSEDREGISTER = "Rpt_Select_RegisterReport";
        public const string USP_SELECT_CANCELLEDREGISTER = "usp_Select_CancelledRegister";
        public const string USP_SELECT_REGISTERBYNO = "usp_Select_RegisterByNo";
        public const string USP_UPDATE_CANCELLEDREGISTER = "usp_Update_CancelledRegister";
        public const string USP_SEARCH_REGISTER = "usp_Seearch_Register";
        public const string RPT_SELECT_REGISTERREPORTS = "Rpt_Select_Register";
        public const string USP_INSERT_REGISTERSERVICE = "usp_Insert_RegisterService";
        public const string RPT_SELECT_VISITCUSTOMER = "Rpt_Select_VisitCustomer";
        public const string USP_SELECT_PRODUCTBYCODE_APP = "usp_Select_ProductByCode_App";
        public const string USP_SELECT_CLOSEDREGISTERS = "usp_Select_ClosedRegister";
        public const string USP_SELECT_REGISTERNOTIFICATION = "usp_Select_RegisterNotification";

        //Renewals
        public const string USP_INSERT_RENEWAL = "usp_Insert_Renewal";
        public const string USP_UPDATE_RENEWAL = "usp_Update_Renewal";
        public const string USP_DELETE_RENEWAL = "usp_Delete_Renewal";
        public const string USP_SELECT_RENEWAL = "usp_Select_Renewal";
        public const string USP_SEARCH_RENEWAL = "usp_search_Renewalrecords";
        public const string RPT_SELECT_RENEWALREPORTS = "Rpt_Select_RenewalReport";
        public const string RPT_SELECT_RECEIPTREPORT = "Rpt_Select_ReceiptReport";
        public const string USP_INSERT_RENEWALSERVICE = "usp_Insert_RenewalService";
        //Branch
        public const string USP_INSERT_BRANCH = "usp_Insert_Branch";
        public const string USP_UPDATE_BRANCH = "usp_Update_Branch";
        public const string USP_DELETE_BRANCH = "usp_Delete_Branch";
        public const string USP_SELECT_BRANCH = "usp_Select_Branch";
        public const string USP_SELECT_MAINBRANCH = "usp_Select_MainBranch";

        //Customer
        public const string USP_INSERT_CUSTOMER = "usp_Insert_Customer";
        public const string USP_UPDATE_CUSTOMER = "usp_Update_Customer";
        public const string USP_DELETE_CUSTOMER = "usp_Delete_Customer";
        public const string USP_SELECT_CUSTOMER = "usp_Select_Customer";
        public const string USP_SELECT_TOPCUSTOMER = "usp_Select_TopCustomer";
        public const string USP_SELECT_RETAILSCUSTOMER = "usp_Select_RetailsCustomer";
        public const string USP_SELECT_WHOLECUSTOMER = "usp_Select_WholeCustomer";
        public const string USP_SELECT_TOPCUSTOMERTYPE = "usp_Select_TopCustomerType";
        public const string USP_SEARCH_CUSTOMER = "usp_Search_Customer";
        public const string USP_SELECT_CUSTOMERBYCODE = "usp_Select_CustomerByCode";
        public const string USP_SELECT_CUSTOMERBYTYPE = "usp_Select_CustomerByType";
        public const string USP_SELECT_CUSTOMERBYDATE = "usp_Select_CustomerByDate";

        //Company
        public const string USP_INSERT_COMPANY = "usp_Insert_Company";
        public const string USP_UPDATE_COMPANY = "usp_Update_Company";
        public const string USP_SELECT_COMPANY = "usp_Select_Company";
        public const string USP_UPDATE_SETTINGS_NOTIFICATION = "usp_Update_Settings_Notification";
        public const string USP_SELECT_CASHTILLBALANCE = "usp_Select_CashtillBalance";
        public const string USP_SELECT_SETTING_NOTIFICATION = "usp_Select_Setting_Notification";

        //Rate
        public const string USP_INSERT_RATE = "usp_Insert_Rate";
        public const string USP_SELECT_CURRENTRATE = "usp_Select_CurrentRate";
        public const string USP_SELECT_RATE = "usp_Select_Rate";

        //RoleConfigurtaion
        public const string USP_SELECT_ROLECONFIGURATION = "usp_Select_RoleConfiguration";
        public const string USP_INSERT_ROLECONFIGURATION = "usp_Insert_RoleConfiguration";
        public const string USP_UPDATE_ROLECONFIGURATION = "usp_Update_RoleConfiguration";
        public const string USP_DELETE_ROLECONFIGURATION = "usp_Delete_RoleConfiguration";
        public const string USP_INSERT_AUDITALL = "usp_Insert_AuditAll"; //Log Database

        //30-05-2017
        //Department
        public const string USP_INSERT_DEPARTMENT = "usp_Insert_Department";
        public const string USP_UPDATE_DEPARTMENT = "usp_Update_Department";
        public const string USP_DELETE_DEPARTMENT = "usp_Delete_Department";
        public const string USP_SELECT_DEPARTMENT = "usp_Select_Department";

        //Description
        public const string USP_INSERT_DESCRIPTION = "usp_Insert_Description";
        public const string USP_UPDATE_DESCRIPTION = "usp_Update_Description";
        public const string USP_DELETE_DESCRIPTION = "usp_Delete_Description";
        public const string USP_SELECT_DESCRIPTION = "usp_Select_Description";

        //Description
        public const string USP_INSERT_BANKENTRY = "usp_Insert_BankEntry";
        public const string USP_UPDATE_BANKENTRY = "usp_Update_BankEntry";
        public const string USP_DELETE_BANKENTRY = "usp_Delete_BankEntry";
        public const string USP_SELECT_BANKENTRY = "usp_Select_BankEntry";

        //Description
        public const string USP_INSERT_DESCRIPTIONCATEGORY = "usp_Insert_DescriptionCategory";
        public const string USP_UPDATE_DESCRIPTIONCATEGORY = "usp_Update_DescriptionCategory";
        public const string USP_DELETE_DESCRIPTIONCATEGORY = "usp_Delete_DescriptionCategory";
        public const string USP_SELECT_DESCRIPTIONCATEGORY = "usp_Select_DescriptionCategory";

        //Specialization
        public const string USP_INSERT_SPECIALIZATION = "usp_Insert_Specialization";
        public const string USP_UPDATE_SPECIALIZATION = "usp_Update_Specialization";
        public const string USP_DELETE_SPECIALIZATION = "usp_Delete_Specialization";
        public const string USP_SELECT_SPECIALIZATION = "usp_Select_Specialization";

        //Drug
        public const string USP_INSERT_DRUG = "usp_Insert_Drug";
        public const string USP_UPDATE_DRUG = "usp_Update_Drug";
        public const string USP_DELETE_DRUG = "usp_Delete_Drug";
        public const string USP_SELECT_DRUG = "usp_Select_Drug";

        //Vendor
        public const string USP_INSERT_VENDOR = "usp_Insert_Vendor";
        public const string USP_UPDATE_VENDOR = "usp_Update_Vendor";
        public const string USP_DELETE_VENDOR = "usp_Delete_Vendor";
        public const string USP_SELECT_VENDOR = "usp_Select_Vendor";

        //Vendor
        public const string USP_INSERT_VENDORPAYMENT = "usp_Insert_VendorPayment";
        public const string USP_UPDATE_VENDORPAYMENT = "usp_Update_VendorPayment";
        public const string USP_DELETE_VENDORPAYMENT = "usp_Delete_VendorPayment";
        public const string USP_SELECT_VENDORPAYMENT = "usp_Select_VendorPayment";

        //Payment
        public const string USP_INSERT_PAYMENT = "usp_Insert_Payment";
        public const string USP_UPDATE_PAYMENT = "usp_Update_Payment";
        public const string USP_DELETE_PAYMENT = "usp_Delete_Payment";
        public const string USP_SELECT_PAYMENT = "usp_Select_Payment";
        public const string USP_SELECT_RECENTPAYMENT = "usp_Select_RecentPayment";

        //Payment
        public const string USP_INSERT_EXPENSEPAYMENT = "usp_Insert_ExpensePayment";
        public const string USP_UPDATE_EXPENSEPAYMENT = "usp_Update_ExpensePayment";
        public const string USP_DELETE_EXPENSEPAYMENT = "usp_Delete_ExpensePayment";
        public const string USP_SELECT_EXPENSEPAYMENT = "usp_Select_ExpensePayment";
        public const string USP_SELECT_RECENTEXPENSEPAYMENT = "usp_Select_RecentExpensePayment";

        //Receipt
        public const string USP_INSERT_RECEIPT = "usp_Insert_Receipt";
        public const string USP_UPDATE_RECEIPT = "usp_Update_Receipt";
        public const string USP_DELETE_RECEIPT = "usp_Delete_Receipt";
        public const string USP_SELECT_RECEIPT = "usp_Select_Receipt";
        public const string USP_SELECT_ONACCOUNTAMOUNT = "usp_Select_OnAccountAmount";
        public const string USP_SELECT_EXPENSEPAYMENTAMOUNT = "usp_Select_ExpensePaymentAmount";
        public const string USP_SELECT_RECEIPTBYSTATUS = "usp_Select_ReceiptByStatus";
        public const string USP_SELECT_RECENTRECEIPT = "usp_Select_RecentReceipt";
        //Receipt
        public const string USP_INSERT_EXCHANGERECEIPT = "usp_Insert_ExchangeReceipt";
        public const string USP_UPDATE_EXCHANGERECEIPT = "usp_Update_ExchangeReceipt";
        public const string USP_DELETE_EXCHANGERECEIPT = "usp_Delete_ExchangeReceipt";
        public const string USP_SELECT_EXCHANGERECEIPT = "usp_Select_ExchangeReceipt";
        public const string USP_SELECT_EXCHANGERECEIPTBYSTATUS = "usp_Select_ExchangeReceiptByStatus";

        //Advance
        public const string USP_INSERT_ADVANCE = "usp_Insert_Advance";
        public const string USP_UPDATE_ADVANCE = "usp_Update_Advance";
        public const string USP_DELETE_ADVANCE = "usp_Delete_Advance";
        public const string USP_SELECT_ADVANCE = "usp_Select_Advance";
        public const string USP_SELECT_ADVANCEBYID = "usp_Select_AdvanceBYID";
        public const string Rpt_Select_Advance = "Rpt_Select_Advance";
        public const string USP_SELECT_ADVANCEAMOUNT = "usp_Select_AdvanceSalary";
        public const string USP_SELECT_ADVANCEINSALARY = "usp_Select_AdvanceInSalary";
        public const string USP_SELECT_ADVANCEOUTSALARY = "usp_Select_AdvanceOutSalary";


        //31-05-2017
        //Diagonsis
        public const string USP_INSERT_DIAGONSIS = "usp_Insert_Diagonsis";
        public const string USP_UPDATE_DIAGONSIS = "usp_Update_Diagonsis";
        public const string USP_DELETE_DIAGONSIS = "usp_Delete_Diagonsis";
        public const string USP_SELECT_DIAGONSIS = "usp_Select_Diagonsis";

        //Country
        public const string USP_INSERT_COUNTRY = "usp_Insert_Country";
        public const string USP_UPDATE_COUNTRY = "usp_Update_Country";
        public const string USP_DELETE_COUNTRY = "usp_Delete_Country";
        public const string USP_SELECT_COUNTRY = "usp_Select_Country";

        //SubCategory
        public const string USP_INSERT_SUBCATEGORY = "usp_Insert_SubCategory";
        public const string USP_UPDATE_SUBCATEGORY = "usp_Update_SubCategory";
        public const string USP_DELETE_SUBCATEGORY = "usp_Delete_SubCategory";
        public const string USP_SELECT_SUBCATEGORY = "usp_Select_SubCategory";

        //CustomerType
        public const string USP_INSERT_CUSTOMERTYPE = "usp_Insert_CustomerType";
        public const string USP_UPDATE_CUSTOMERTYPE = "usp_Update_Customertype";
        public const string USP_DELETE_CUSTOMERTYPE = "usp_Delete_CustomerType";
        public const string USP_SELECT_CUSTOMERTYPE = "usp_Select_Customertype";
        public const string USP_SEARCH_CUSTOMERTYPE = "usp_Search_Customertype";

        //Chit
        public const string USP_INSERT_CHIT = "usp_Insert_Chit";
        public const string USP_UPDATE_CHIT = "usp_Update_Chit";
        public const string USP_DELETE_CHIT = "usp_Delete_Chit";
        public const string USP_SELECT_CHIT = "usp_Select_Chit";
        public const string USP_SEARCH_CHITCLOSED = "usp_Search_ChitClosed";

        //Gift
        public const string USP_INSERT_GIFT = "usp_Insert_Gift";
        public const string USP_UPDATE_GIFT = "usp_Update_Gift";
        public const string USP_DELETE_GIFT = "usp_Delete_Gift";
        public const string USP_SELECT_GIFT = "usp_Select_Gift";
        public const string USP_SELECT_GIFTAMOUNT = "usp_Select_GiftAmount";

        public const string USP_INSERT_FINANCIALYEAR = "usp_Insert_FinancialYear";
        public const string USP_UPDATE_FINANCIALYEAR = "usp_Update_FinancialYear";
        public const string USP_DELETE_FINANCIALYEAR = "usp_Delete_FinancialYear";
        public const string USP_SELECT_FINANCIALYEAR = "usp_Select_FinancialYear";

        //Stock
        public const string USP_INSERT_STOCK = "usp_Insert_Stock";
        public const string USP_UPDATE_STOCK = "usp_Update_Stock";
        public const string USP_UPDATE_STOCKSPLIT = "usp_Update_StockSplit";
        public const string USP_DELETE_STOCK = "usp_Delete_Stock";
        public const string USP_SELECT_STOCK = "usp_Select_Stock";
        public const string USP_SELECT_PRODUCTSTOCK = "usp_Select_ProductStock";
        public const string USP_SELECT_PRODUCTSTOCKBYBARCODE = "usp_Select_ProductStockByBarcode";
        public const string USP_SELECT_BARCODESTOCKBYID = "usp_Select_BarcodeStockByID";
        public const string USP_SELECT_TOPSTOCK = "usp_Select_TopStock";
        public const string USP_SEARCH_STOCK = "usp_Search_Stock";
        public const string USP_SELECT_BARCODELISTBYYARN = "usp_Select_BarcodeListByYarn";
        public const string USP_SELECT_MISSINGSTOCK = "usp_Select_MissingStock";
        public const string USP_SELECT_STOCKBYBARCODE = "usp_Select_StockByBarcode";
        public const string USP_SELECT_STOCKBYBARCODEQUOTATION = "usp_Select_StockByBarcodeQuotation";
        public const string RPT_SELECT_STOCKCHECKTRANS = "Rpt_Select_StockCheckTrans";

        //Stock
        public const string USP_SELECT_STOCKADJUEST = "usp_Select_StockAdjuest";
        public const string USP_INSERT_STOCKADJUEST = "usp_Insert_StockAdjuest";
        public const string USP_SELECT_GETALLPRODUCT = "usp_Select_GetAllProduct";
        public const string USP_SELECT_GETALLSTOCKADJUEST = "usp_Select_GetAllStockAdjuest";

        //State
        public const string USP_INSERT_STATE = "usp_Insert_State";
        public const string USP_UPDATE_STATE = "usp_Update_State";
        public const string USP_DELETE_STATE = "usp_Delete_State";
        public const string USP_SELECT_STATE = "usp_Select_State";

        //Shift
        public const string USP_INSERT_SHIFT = "usp_Insert_Shift";
        public const string USP_UPDATE_SHIFT = "usp_Update_Shift";
        public const string USP_DELETE_SHIFT = "usp_Delete_Shift";
        public const string USP_SELECT_SHIFT = "usp_Select_Shift";


        //Employee
        public const string USP_INSERT_EMPLOYEE = "usp_Insert_Employee";
        public const string USP_UPDATE_EMPLOYEE = "usp_Update_Employee";
        public const string USP_DELETE_EMPLOYEE = "usp_Delete_Employee";
        public const string USP_SELECT_EMPLOYEE = "usp_Select_EMployee";
        public const string USP_UPDATE_EMPLOYEE_AMOUNT = "usp_Update_Employee_Amount";

        //Salary
        public const string USP_INSERT_SALARY = "usp_Insert_Salary";
        public const string USP_UPDATE_SALARY = "usp_Update_Salary";
        public const string USP_DELETE_SALARY = "usp_Delete_Salary";
        public const string USP_SELECT_SALARY = "usp_Select_Salary";
        public const string USP_SELECT_SALARYBYMONTH = "usp_Select_SalarybyMonth";
        public const string RPT_SELECT_SALARY = "Rpt_Select_Salary";
        public const string RPT_SELECT_PAYSLIP = "Rpt_Select_PaySlip";

        //Attendance Log
        public const string USP_INSERT_ATTENDANCELOG = "usp_Insert_AttendanceLog";
        public const string USP_UPDATE_ATTENDANCELOG = "usp_Update_AttendanceLog";
        public const string USP_DELETE_ATTENDANCELOG = "usp_Delete_AttendanceLog";
        public const string USP_SELECT_ATTENDANCELOG = "usp_Select_AttendanceLog";
        public const string USP_SELECT_ATTENDANCELOGBYID = "usp_Select_AttendanceLogByID";
        public const string USP_TB_ATTENDANCELOGFINAL_GETDYNAMIC = "usp_Tb_AttendanceLogFinal_GetDynamic";
        public const string USP_SELECT_EMPLOYEEATTENDANCEBYID = "usp_Select_EmployeeAttendanceByID";
        public const string RPT_SELECT_ATTENDANCELOGREPORT_BYDYNAMIC = "Rpt_Select_AttendanceLogReport_ByDynamic";
        public const string RPT_SELECT_ATTENDANCELOGREPORT = "Rpt_Select_AttendanceLogReport";

        //punch
        public const string USP_SELECT_PUNCHLOG = "usp_Select_PunchLog";
        public const string USP_SELECT_PUNCHLOGBYID = "usp_Select_PunchLogByID";
        public const string USP_INSERT_PUNCHLOG = "usp_Insert_PunchLog";
        public const string USP_TB_PUNCHLOGFINAL_GETDYNAMIC = "usp_Tb_PunchLogFinal_GetDynamic";
        //Doctor
        public const string USP_INSERT_DOCTOR = "usp_Insert_Doctor";
        public const string USP_UPDATE_DOCTOR = "usp_Update_Doctor";
        public const string USP_DELETE_DOCTOR = "usp_Delete_Doctor";
        public const string USP_SELECT_DOCTOR = "usp_Select_Doctor";

        //Doctor Type
        public const string USP_INSERT_DOCTORTYPE = "usp_Insert_DoctorType";
        public const string USP_UPDATE_DOCTORTYPE = "usp_Update_DoctorType";
        public const string USP_DELETE_DOCTORTYPE = "usp_Delete_DoctorType";
        public const string USP_SELECT_DOCTORTYPE = "usp_Select_DoctorType";

        //DischargeEntry
        public const string USP_INSERT_DISCHARGEENTRY = "usp_Insert_DischargeEntry";
        public const string USP_UPDATE_DISCHARGEENTRY = "usp_Update_DischargeEntry";
        public const string USP_DELETE_DISCHARGEENTRY = "usp_Delete_DischargeEntry";
        public const string USP_SELECT_DISCHARGEENTRY = "usp_Select_DischargeEntry";

        //HiPReplacement
        public const string USP_INSERT_HIPREPLACEMENT = "usp_Insert_HipReplacement";
        public const string USP_UPDATE_HIPREPLACEMENT = "usp_Update_HipReplacement";
        public const string USP_DELETE_HIPREPLACEMENT = "usp_Delete_HipReplacement";
        public const string USP_SELECT_HIPREPLACEMENT = "usp_Select_HipReplacement";

        //KneeReplacement
        public const string USP_INSERT_KNEEREPLACEMENT = "usp_Insert_KneeReplacement";
        public const string USP_UPDATE_KNEEREPLACEMENT = "usp_Update_KneeReplacement";
        public const string USP_DELETE_KNEEREPLACEMENT = "usp_Delete_KneeReplacement";
        public const string USP_SELECT_KNEEREPLACEMENT = "usp_Select_KneeReplacement";

        //Designation
        public const string USP_INSERT_DESIGNATION = "usp_Insert_Designation";
        public const string USP_UPDATE_DESIGNATION = "usp_Update_Designation";
        public const string USP_DELETE_DESIGNATION = "usp_Delete_Designation";
        public const string USP_SELECT_DESIGNATION = "usp_Select_Designation";


        //OtherSurgery
        public const string USP_INSERT_OTHERSURGERY = "usp_Insert_OtherSurgery";
        public const string USP_UPDATE_OTHERSURGERY = "usp_Update_OtherSurgery";
        public const string USP_DELETE_OTHERSURGERY = "usp_Delete_OtherSurgery";
        public const string USP_SELECT_OTHERSURGERY = "usp_Select_OtherSurgery";

        //Prescription
        public const string USP_INSERT_PRESCRIPTION = "usp_Insert_Prescription";
        public const string USP_UPDATE_PRESCRIPTION = "usp_Update_Prescription";
        public const string USP_DELETE_PRESCRIPTION = "usp_Delete_Prescription";
        public const string USP_SELECT_PRESCRIPTION = "usp_Select_Prescription";

        //Added on 13-07-2017
        //Admission
        public const string USP_INSERT_ADMISSION = "usp_Insert_Admission";
        public const string USP_UPDATE_ADMISSION = "usp_Update_Admission";
        public const string USP_DELETE_ADMISSION = "usp_Delete_Admission";
        public const string USP_SELECT_ADMISSION = "usp_Select_Admission";

        //Added on 17-07-2017
        //WareHouse
        public const string USP_INSERT_WAREHOUSE = "usp_Insert_WareHouse";
        public const string USP_UPDATE_WAREHOUSE = "usp_Update_WareHouse";
        public const string USP_DELETE_WAREHOUSE = "usp_Delete_WareHouse";
        public const string USP_SELECT_WAREHOUSE = "usp_Select_WareHouse";

        //Unit
        public const string USP_INSERT_UNIT = "usp_Insert_Unit";
        public const string USP_UPDATE_UNIT = "usp_Update_Unit";
        public const string USP_DELETE_UNIT = "usp_Delete_Unit";
        public const string USP_SELECT_UNIT = "usp_Select_Unit";

        //Category
        public const string USP_INSERT_CATEGORY = "usp_Insert_Category";
        public const string USP_UPDATE_CATEGORY = "usp_Update_Category";
        public const string USP_DELETE_CATEGORY = "usp_Delete_Category";
        public const string USP_SELECT_CATEGORY = "usp_Select_Category";

        //Transport
        public const string USP_INSERT_TRANSPORT = "usp_Insert_Transport";
        public const string USP_UPDATE_TRANSPORT = "usp_Update_Transport";
        public const string USP_DELETE_TRANSPORT = "usp_Delete_Transport";
        public const string USP_SELECT_TRANSPORT = "usp_Select_Transport";

        //ShareScreen
        public const string USP_INSERT_SHARESCREEN = "usp_Insert_ShareScreen";
        public const string USP_UPDATE_SHARESCREEN = "usp_Update_ShareScreen";
        public const string USP_DELETE_SHARESCREEN = "usp_Delete_ShareScreen";
        public const string USP_SELECT_SHARESCREEN = "usp_Select_ShareScreen";

        //ProductType
        public const string USP_INSERT_PRODUCTTYPE = "usp_Insert_ProductType";
        public const string USP_UPDATE_PRODUCTTYPE = "usp_Update_ProductType";
        public const string USP_DELETE_PRODUCTTYPE = "usp_Delete_ProductType";
        public const string USP_SELECT_PRODUCTTYPE = "usp_Select_ProductType";

        //Product
        public const string USP_INSERT_PRODUCT = "usp_Insert_Product";
        public const string USP_UPDATE_PRODUCT = "usp_Update_Product";
        public const string USP_DELETE_PRODUCT = "usp_Delete_Product";
        public const string USP_SELECT_PRODUCT = "usp_Select_Product";
        public const string USP_SELECT_PRODUCTYARN = "usp_Select_ProductYarn";
        public const string USP_SELECT_GENERATESMSCODE = "usp_Select_GenerateSMSCode";
        public const string USP_SELECT_TOPPRODUCT = "usp_Select_TopProduct";
        public const string USP_SEARCH_PRODUCT = "usp_Search_Product";
        public const string USP_SELECT_PRODUCTBYCODE = "usp_Select_ProductByCode";
        public const string USP_SELECT_PRODUCTBYCODEBYID = "usp_Select_ProductByCodeBYID";
        public const string USP_SELECT_PRODUCTBYCODEYARN = "usp_Select_ProductByCodeYarn";
        public const string USP_SELECT_PRODUCTBYSMSCODE = "usp_Select_ProductBySMSCode";
        public const string USP_SELECT_PRODUCTBYBARCODE = "usp_Select_ProductByBarcode";
        public const string RPT_SELECT_PRODUCT = "Rpt_Select_Product";
        public const string RPT_SELECT_PRODUCTIMAGES = "Rpt_Select_ProductImages";
        public const string RPT_SELECT_REORDERPRODUCT = "Rpt_Select_ReorderProduct";
        public const string USP_SELECT_PRODUCTBYNAME = "usp_Select_ProductByName";
        public const string USP_SELECT_BARCODELIST = "usp_Select_BarcodeList";
        public const string USP_SELECT_SUBCATEGORYLIST = "usp_Select_SubCategoryList";
        //Product
        public const string USP_INSERT_PRICING = "usp_Insert_Pricing";
        public const string USP_UPDATE_PRICING = "usp_Update_Pricing";
        public const string USP_DELETE_PRICING = "usp_Delete_Pricing";
        public const string USP_SELECT_PRICING = "usp_Select_Pricing";
        public const string usp_Select_PricingByProductName = "usp_Select_PricingByProductName";
        public const string USP_SELECT_BARCODEBYID = "usp_Select_BarcodeByID";
        public const string USP_SELECT_BARCODEBYSTOCK = "usp_Select_BarcodeBystock";

        //Tax
        public const string USP_INSERT_TAX = "usp_Insert_Tax";
        public const string USP_UPDATE_TAX = "usp_Update_Tax";
        public const string USP_DELETE_TAX = "usp_Delete_Tax";
        public const string USP_SELECT_TAX = "usp_Select_Tax";

        //SurgeryType
        public const string USP_INSERT_SURGERYTYPE = "usp_Insert_SurgeryType";
        public const string USP_UPDATE_SURGERYTYPE = "usp_Update_SurgeryType";
        public const string USP_DELETE_SURGERYTYPE = "usp_Delete_SurgeryType";
        public const string USP_SELECT_SURGERYTYPE = "usp_Select_SurgeryType";

        //SurgicalProcedure
        public const string USP_INSERT_SURGICALPROCEDURE = "usp_Insert_SurgicalProcedure";
        public const string USP_UPDATE_SURGICALPROCEDURE = "usp_Update_SurgicalProcedure";
        public const string USP_DELETE_SURGICALPROCEDURE = "usp_Delete_SurgicalProcedure";
        public const string USP_SELECT_SURGICALPROCEDURE = "usp_Select_SurgicalProcedure";

        //Anesthesia
        public const string USP_INSERT_ANESTHESIA = "usp_Insert_Anesthesia";
        public const string USP_UPDATE_ANESTHESIA = "usp_Update_Anesthesia";
        public const string USP_DELETE_ANESTHESIA = "usp_Delete_Anesthesia";
        public const string USP_SELECT_ANESTHESIA = "usp_Select_Anesthesia";

        //Supplier
        public const string USP_INSERT_SUPPLIER = "usp_Insert_Supplier";
        public const string USP_UPDATE_SUPPLIER = "usp_Update_Supplier";
        public const string USP_DELETE_SUPPLIER = "usp_Delete_Supplier";
        public const string USP_SELECT_SUPPLIER = "usp_Select_Supplier";
        public const string USP_SELECT_SUPPLIERALL = "usp_Select_SupplierAll";

        //Location
        public const string USP_INSERT_LOCATION = "usp_Insert_Location";
        public const string USP_UPDATE_LOCATION = "usp_Update_Location";
        public const string USP_DELETE_LOCATION = "usp_Delete_Location";
        public const string USP_SELECT_LOCATION = "usp_Select_Location";

        //Added on 22-07-2017
        //OtherProcedure
        public const string USP_INSERT_OTHERPROCEDURE = "usp_Insert_OtherProcedure";
        public const string USP_UPDATE_OTHERPROCEDURE = "usp_Update_OtherProcedure";
        public const string USP_DELETE_OTHERPROCEDURE = "usp_Delete_OtherProcedure";
        public const string USP_SELECT_OTHERPROCEDURE = "usp_Select_OtherProcedure";

        //Added on 29-07-2017
        public const string RPT_SELECT_DISCHARGESUMMARY = "Rpt_Select_DischargeSummary";
        public const string RPT_SELECT_PRESCRIPTIONREPORT = "Rpt_Select_PrescriptionReport";
        //Added on 01-09-2017
        //Dosage
        public const string USP_INSERT_DOSAGE = "usp_Insert_Dosage";
        public const string USP_UPDATE_DOSAGE = "usp_Update_Dosage";
        public const string USP_DELETE_DOSAGE = "usp_Delete_Dosage";
        public const string USP_SELECT_DOSAGE = "usp_Select_Dosage";
        public const string USP_SEARCH_DOSAGE = "usp_Search_Dosage";

        //Duration
        public const string USP_INSERT_DURATION = "usp_Insert_Duration";
        public const string USP_UPDATE_DURATION = "usp_Update_Duration";
        public const string USP_DELETE_DURATION = "usp_Delete_Duration";
        public const string USP_SELECT_DURATION = "usp_Select_Duration";
        public const string USP_SEARCH_DURATION = "usp_Search_Duration";

        //Added on 08-09-2017
        //PatientOwnDrug
        public const string USP_INSERT_PATIENTOWNDRUG = "usp_Insert_PatientOwnDrug";
        public const string USP_UPDATE_PATIENTOWNDRUG = "usp_Update_PatientOwnDrug";
        public const string USP_DELETE_PATIENTOWNDRUG = "usp_Delete_PatientOwnDrug";
        public const string USP_SELECT_PATIENTOWNDRUG = "usp_Select_PatientOwnDrug";

        //Added on 11-09-2017
        //Dashboard Count
        public const string USP_SELECT_DASHBOARD = "usp_Select_Dashboard";
        public const string USP_SELECT_RECENTADMISSION = "usp_Select_RecentAdmission";

        //Added on 24-10-2017
        //Frequency
        public const string USP_INSERT_FREQUENCY = "usp_Insert_Frequency";
        public const string USP_UPDATE_FREQUENCY = "usp_Update_Frequency";
        public const string USP_DELETE_FREQUENCY = "usp_Delete_Frequency";
        public const string USP_SELECT_FREQUENCY = "usp_Select_Frequency";
        public const string USP_SEARCH_FREQUENCY = "usp_Search_Frequency";

        //Added on 25-10-2017 Reset Password
        public const string USP_RESET_PASSWORD = "usp_Reset_Password";

        //PATIENT
        public const string USP_INSERT_PATIENT = "usp_Insert_Patient";
        public const string USP_UPDATE_PATIENT = "usp_Update_Patient";
        public const string USP_DELETE_PATIENT = "usp_Delete_Patient";
        public const string USP_SELECT_PATIENT = "usp_Select_Patient";
        public const string USP_SELECT_PATIENTID = "usp_Select_PatientID";
        public const string USP_SELECT_PATIENTDYNAMIC = "usp_Select_PatientDynamic";
        public const string USP_SEARCH_PATIENT = "usp_Search_Patient";
        public const string USP_SELECT_PATIENTDETAILS = "usp_Select_PatientDetails";
        public const string USP_SELECT_PATIENTBYOPDNO = "usp_Select_PatientByOPDNo";
        public const string USP_SELECT_NEWPATIENTDETAILS = "usp_Select_NewPatientDetails";
        //Prescription
        public const string USP_INSERT_PRESCRIPTIONMASTER = "usp_Insert_PrescriptionMaster";
        public const string USP_UPDATE_PRESCRIPTIONMASTER = "usp_Update_PrescriptionMaster";
        public const string USP_DELETE_PRESCRIPTIONMASTER = "usp_Delete_PrescriptionMaster";
        public const string USP_SELECT_PRESCRIPTIONMASTER = "usp_Select_PrescriptionMaster";
        public const string USP_SELECT_PRESCRIPTIONSUMMARY = "usp_Select_PrescriptionSummary";
        //PrescriptionTrans
        public const string USP_INSERT_PRESCRIPTIONTRANS = "usp_Insert_PrescriptionTrans";
        public const string USP_UPDATE_PRESCRIPTIONTRANS = "usp_Update_PrescriptionTrans";
        public const string USP_DELETE_PRESCRIPTIONTRANS = "usp_Delete_PrescriptionTrans";
        public const string USP_SELECT_PRESCRIPTIONTRANS = "usp_Select_PrescriptionTrans";

        //OPBilling
        public const string USP_INSERT_OPBILLINGMASTER = "usp_Insert_OPBillingMaster";
        public const string USP_UPDATE_OPBILLINGMASTER = "usp_Update_OPBillingMaster";
        public const string USP_DELETE_OPBILLINGMASTER = "usp_Delete_OPBillingMaster";
        public const string USP_SELECT_OPBILLINGMASTER = "usp_Select_OPBillingMaster";
        public const string USP_SELECT_OPBILLINGID = "usp_Select_OPBillingID";
        public const string USP_SELECT_OPBILLINGDYNAMIC = "usp_Select_OPBillingDynamic";
        public const string USP_SELECT_OPBILLINGSUMMARY = "usp_Select_OPBillingSummary";
        public const string USP_SELECT_OPBILLINGREPORT = "usp_Select_OPBillingReport";

        //Sales
        public const string USP_INSERT_SALES = "usp_Insert_Sales";
        public const string USP_UPDATE_SALES = "usp_Update_Sales";
        public const string USP_DELETE_SALES = "usp_Delete_Sales";
        public const string USP_SELECT_SALES = "usp_Select_Sales";
        public const string USP_SELECT_SALESINVOICE = "usp_Select_SalesInvoice";
        public const string USP_SELECT_SALESID = "usp_Select_SalesID";
        public const string USP_SEARCH_SALES = "usp_Search_Sales";
        public const string USP_SELECT_TOPSALES = "usp_Select_TopSales";
        public const string USP_SELECT_SALESSUMMARY = "usp_Select_SalesSummary";
        public const string USP_SELECT_SALESREPORT = "usp_Select_SalesReport";
        public const string USP_SELECT_LASTBILLNO = "usp_Select_LastBillNo";
        public const string USP_SELECT_AUDITBILLNO = "usp_Select_AuditBillNo";
        public const string RPT_SELECT_SALESDISCOUNTREPORT = "Rpt_Select_SalesDiscountReport";
        //SalesTrans
        public const string USP_INSERT_SALESTRANS = "usp_Insert_SalesTrans";
        public const string USP_UPDATE_SALESTRANS = "usp_Update_SalesTrans";
        public const string USP_DELETE_SALESTRANS = "usp_Delete_SalesTrans";
        public const string USP_SELECT_SALESTRANS = "usp_Select_SalesTrans";

        //StockCheck
        public const string USP_INSERT_STOCKCHECK = "usp_Insert_StockCheck";
        public const string USP_UPDATE_STOCKCHECK = "usp_Update_StockCheck";
        public const string USP_DELETE_STOCKCHECK = "usp_Delete_StockCheck";
        public const string USP_SELECT_STOCKCHECK = "usp_Select_StockCheck";
        public const string USP_SELECT_STOCKCHECKINVOICE = "usp_Select_StockCheckInvoice";
        public const string USP_SELECT_STOCKCHECKID = "usp_Select_StockCheckID";
        public const string USP_SEARCH_STOCKCHECK = "usp_Search_StockCheck";
        public const string USP_SELECT_TOPSTOCKCHECK = "usp_Select_TopStockCheck";
        public const string USP_SELECT_STOCKCHECKSUMMARY = "usp_Select_StockCheckSummary";
        public const string USP_SELECT_STOCKCHECKREPORT = "usp_Select_StockCheckReport";

        //ShippingAddresss
        public const string USP_INSERT_RETAILPAYMENTMODE = "usp_Insert_RetailPaymentMode";
        public const string USP_UPDATE_RETAILPAYMENTMODE = "usp_Update_RetailPaymentMode";
        public const string USP_DELETE_RETAILPAYMENTMODE = "usp_Delete_RetailPaymentMode";
        public const string USP_SELECT_RETAILPAYMENTMODE = "usp_Select_RetailPaymentMode";

        //StockCheckTrans
        public const string USP_INSERT_STOCKCHECKTRANS = "usp_Insert_StockCheckTrans";
        public const string USP_UPDATE_STOCKCHECKTRANS = "usp_Update_StockCheckTrans";
        public const string USP_DELETE_STOCKCHECKTRANS = "usp_Delete_StockCheckTrans";
        public const string USP_SELECT_STOCKCHECKTRANS = "usp_Select_StockCheckTrans";

        //SalesChitTrans
        public const string USP_INSERT_SALESCHITTRANS = "usp_Insert_SalesChitTrans";
        public const string USP_UPDATE_SALESCHITTRANS = "usp_Update_SalesChitTrans";
        public const string USP_DELETE_SALESCHITTRANS = "usp_Delete_SalesChitTrans";
        public const string USP_SELECT_SALESCHITTRANS = "usp_Select_SalesChitTrans";

        //BranchMove
        public const string USP_INSERT_BRANCHMOVE = "usp_Insert_BranchMove";
        public const string USP_UPDATE_BRANCHMOVE = "usp_Update_BranchMove";
        public const string USP_UPDATE_BRANCHMOVESTATUS = "usp_Update_BranchMoveStatus";
        public const string USP_DELETE_BRANCHMOVE = "usp_Delete_BranchMove";
        public const string USP_SELECT_BRANCHMOVE = "usp_Select_BranchMove";
        public const string USP_SELECT_TOPBRANCHMOVE = "usp_Select_TopBranchMove";
        public const string USP_SELECT_BRANCHMOVEINVOICE = "usp_Select_BranchMoveInvoice";
        public const string USP_SELECT_BRANCHMOVEID = "usp_Select_BranchMoveID";
        public const string USP_SEARCH_BRANCHMOVE = "usp_Search_BranchMove";
        public const string USP_SELECT_BRANCHMOVESUMMARY = "usp_Select_BranchMoveSummary";
        public const string USP_SELECT_BRANCHMOVEREPORT = "usp_Select_BranchMoveReport";



        //BranchMoveTrans
        public const string USP_INSERT_BRANCHMOVETRANS = "usp_Insert_BranchMoveTrans";
        public const string USP_UPDATE_BRANCHMOVETRANS = "usp_Update_BranchMoveTrans";
        public const string USP_DELETE_BRANCHMOVETRANS = "usp_Delete_BranchMoveTrans";
        public const string USP_SELECT_BRANCHMOVETRANS = "usp_Select_BranchMoveTrans";


        //SalesReturn
        public const string USP_UPDATE_TDSPAYMENT = "usp_Update_TDSPayment";
        public const string USP_SELECT_TDSPAYMENT = "usp_Select_TDSPayment";
        public const string USP_SEARCH_TDSPAYMENT = "usp_Search_TDSPayment";
        public const string USP_INSERT_TDSPAYMENT = "usp_Insert_TDSPayment";
        public const string USP_DELETE_TDSPAYMENT = "usp_Delete_TDSPayment";

        //SalesReturn
        public const string USP_INSERT_SALESRETURN = "usp_Insert_SalesReturn";
        public const string USP_UPDATE_SALESRETURN = "usp_Update_SalesReturn";
        public const string USP_DELETE_SALESRETURN = "usp_Delete_SalesReturn";
        public const string USP_SELECT_SALESRETURN = "usp_Select_SalesReturn";
        public const string USP_SELECT_SALESRETURNINVOICE = "usp_Select_SalesReturnInvoice";
        public const string USP_SELECT_SALESRETURNID = "usp_Select_SalesReturnID";
        public const string USP_SEARCH_SALESRETURN = "usp_Search_SalesReturn";
        public const string USP_SELECT_TOPSALESRETURN = "usp_Select_TopSalesReturn";
        public const string USP_SELECT_SALESRETURNSUMMARY = "usp_Select_SalesReturnSummary";
        public const string USP_SELECT_SALESRETURNREPORT = "usp_Select_SalesReturnReport";

        //TDSPaymentTrans
        public const string USP_INSERT_TDSPAYMENTTRANS = "usp_Insert_TDSPaymentTrans";
        public const string USP_UPDATE_TDSPAYMENTTRANS = "usp_Update_TDSPaymentTrans";
        public const string USP_DELETE_TDSPAYMENTTRANS = "usp_Delete_TDSPaymentTrans";
        public const string USP_SELECT_TDSPAYMENTTRANS = "usp_Select_TDSPaymentTrans";

        //SalesReturnTrans
        public const string USP_INSERT_SALESRETURNTRANS = "usp_Insert_SalesReturnTrans";
        public const string USP_UPDATE_SALESRETURNTRANS = "usp_Update_SalesReturnTrans";
        public const string USP_DELETE_SALESRETURNTRANS = "usp_Delete_SalesReturnTrans";
        public const string USP_SELECT_SALESRETURNTRANS = "usp_Select_SalesReturnTrans";

        //Zone
        public const string USP_INSERT_ZONE = "usp_Insert_Zone";
        public const string USP_UPDATE_ZONE = "usp_Update_Zone";
        public const string USP_DELETE_ZONE = "usp_Delete_Zone";
        public const string USP_SELECT_ZONE = "usp_Select_Zone";

        //Region
        public const string USP_INSERT_REGION = "usp_Insert_Region";
        public const string USP_UPDATE_REGION = "usp_Update_Region";
        public const string USP_DELETE_REGION = "usp_Delete_Region";
        public const string USP_SELECT_REGION = "usp_Select_Region";

        //Quotation
        public const string USP_INSERT_QUOTATION = "usp_Insert_Quotation";
        public const string USP_UPDATE_QUOTATION = "usp_Update_Quotation";
        public const string USP_DELETE_QUOTATION = "usp_Delete_Quotation";
        public const string USP_SELECT_QUOTATION = "usp_Select_Quotation";
        public const string USP_SELECT_TOPQUOTATION = "usp_Select_TopQuotation";
        public const string USP_SEARCH_QUOTATION = "usp_Search_Quotation";

        //QuotationTrans
        public const string USP_INSERT_QUOTATIONTRANS = "usp_Insert_QuotationTrans";
        public const string USP_UPDATE_QUOTATIONTRANS = "usp_Update_QuotationTrans";
        public const string USP_DELETE_QUOTATIONTRANS = "usp_Delete_QuotationTrans";
        public const string USP_SELECT_QUOTATIONTRANS = "usp_Select_QuotationTrans";

        //Exchange
        public const string USP_INSERT_EXCHANGE = "usp_Insert_Exchange";
        public const string USP_UPDATE_EXCHANGE = "usp_Update_Exchange";
        public const string USP_DELETE_EXCHANGE = "usp_Delete_Exchange";
        public const string USP_SELECT_EXCHANGE = "usp_Select_Exchange";
        public const string USP_SEARCH_EXCHANGE = "usp_Search_Exchange";
        public const string USP_SELECT_TOPEXCHANGE = "usp_Select_TopExchange";
        //ExchangeTrans
        public const string USP_INSERT_EXCHANGETRANS = "usp_Insert_ExchangeTrans";
        public const string USP_UPDATE_EXCHANGETRANS = "usp_Update_ExchangeTrans";
        public const string USP_DELETE_EXCHANGETRANS = "usp_Delete_ExchangeTrans";
        public const string USP_SELECT_EXCHANGETRANS = "usp_Select_ExchangeTrans";

        public const string RPT_SELECT_CUSTOMERSALESBILL = "Rpt_Select_CustomerSalesReport";
        public const string RPT_SELECT_SALESTRANSREPORTS = "Rpt_Select_SalesTransReport";
        public const string RPT_SELECT_OPBILLING = "Rpt_Select_OPBilling";
        public const string RPT_SELECT_PURCHASEORDERINVOICE = "Rpt_Select_PurchaseOrderInvoice";
        public const string RPT_SELECT_PAYMENTINVOICE = "Rpt_Select_PaymentInvoice";
        public const string RPT_SELECT_PAYMENTEXPENESE = "Rpt_Select_PaymentExpense";
        public const string RPT_SELECT_SALESINVOICE = "Rpt_Select_SalesInvoice";
        public const string RPT_SELECT_SALESENTRYINVOICE = "Rpt_Select_SalesEntryInvoice";
        public const string RPT_SELECT_YARNSALESENTRYINVOICE = "Rpt_Select_YarnSalesEntryInvoice";

        public const string RPT_SELECT_ESTIMATESALESENTRYINVOICE = "Rpt_Select_EstimateSalesEntryInvoice";
        public const string RPT_SELECT_PURCHASEENTRYINVOICE = "Rpt_Select_PurchaseEntryInvoice";
        public const string RPT_SELECT_PRINTATTENDANCELOG = "Rpt_Select_PrintAttendanceLog";
        public const string RPT_SELECT_PURCHASERETURNINVOICE = "Rpt_Select_PurchaseReturnInvoice";
        public const string RPT_SELECT_SALESORDER = "Rpt_Select_SalesOrder";
        public const string RPT_SELECT_RETAILSSALESENTRYINVOICE = "Rpt_Select_RetailsSalesEntryInvoice";
        public const string RPT_SELECT_RETAILSESTIMATESALESENTRYINVOICE = "Rpt_Select_RetailsEstimateSalesEntryInvoice";
        public const string RPT_SELECT_SALESRETURNINVOICE = "Rpt_Select_SalesReturnInvoice";
        public const string RPT_SELECT_EXCHANGEINVOICE = "Rpt_Select_ExchangeInvoice";
        public const string RPT_SELECT_SALESRETURNBILL = "Rpt_Select_SalesReturnBill";
        public const string RPT_SELECT_QUOTATIONINVOICE = "Rpt_Select_QuotationInvoice";
        public const string RPT_SELECT_PATIENT = "Rpt_Select_Patient";
        public const string RPT_SELECT_CUSTOMER = "Rpt_Select_Customer";
        public const string RPT_SELECT_STOCK = "Rpt_Select_Stock";
        public const string RPT_SELECT_STOCKSUMMARY = "Rpt_Select_StockSummary";
        public const string RPT_SELECT_STOCKSUMMARYREPORT = "Rpt_Select_StockSummaryReport";
        public const string RPT_SELECT_STOCKDETAILED = "Rpt_Select_StockDetailed";
        public const string RPT_SELECT_SALES = "Rpt_Select_Sales";
        public const string RPT_SELECT_SALESRETURN = "Rpt_Select_SalesReturn";
        public const string RPT_SELECT_JOURNAL = "rpt_Select_Journal";
        public const string RPT_SELECT_Exchange = "Rpt_Select_Exchange";
        public const string RPT_SELECT_CLOSEDREPORT = "Rpt_Select_ClosedReport";
        public const string RPT_SELECT_SCHEMELEDGERREPORT = "Rpt_Select_SchemeLedgerReport";
        public const string USP_SELECT_UNPAIDREPORTS = "Rpt_Select_UnpaidReport";
        public const string USP_SELECT_SALESREPORTS = "Rpt_Select_SalesReports";
        public const string RPT_SELECT_PRODUCTWISESALES = "Rpt_Select_ProductWiseSales";
        public const string RPT_SELECT_PRODUCTWISESALESRETURN = "Rpt_Select_ProductWiseSalesReturn";
        public const string RPT_SELECT_PRODUCTWISESALESSUMMARY = "Rpt_Select_ProductWiseSalesSummary";
        public const string RPT_SELECT_PRODUCTWISEPURCHASE = "Rpt_Select_ProductWisePurchase";
        public const string RPT_SELECT_PRODUCTWISEPURCHASERETURN = "Rpt_Select_ProductWisePurchaseReturn";
        public const string RPT_SELECT_PRODUCTWISEPURCHASESUMMARY = "Rpt_Select_ProductWisePurchaseSummary";
        public const string RPT_SELECT_PROCESSINGPENDING = "Rpt_Select_ProcessingPending";
        public const string RPT_SELECT_PROCESSINGINWARD = "Rpt_Select_ProcessingInward";
        public const string RPT_SELECT_PROCESSINGOUTWARD = "Rpt_Select_ProcessingOutward";
        public const string RPT_SALESSTATEMENT_GETBYDATE = "Rpt_SalesStatement_GetByDate";
        public const string RPT_EXPENSESTATEMENT_GETBYDATE = "Rpt_ExpenseStatement_GetByDate";
        public const string RPT_ADVANCESTATEMENT_GETBYDATE = "Rpt_AdvanceStatement_GetByDate";
        public const string RPT_PURCHASESTATEMENT_GETBYDATE = "Rpt_PurchaseStatement_GetByDate";
        public const string RPT_VENDORSTATEMENT_GETBYDATE = "Rpt_VendorStatement_GetByDate";
        public const string RPT_SELECT_PURCHASEORDER = "Rpt_Select_PurchaseOrder";
        public const string RPT_SELECT_PURCHASE = "Rpt_Select_Purchase"; 
        public const string RPT_SELECT_PURCHASEDISCOUNTREPORT = "Rpt_Select_PurchaseDiscountReport";
        public const string RPT_SELECT_PURCHASEREPORT = "Rpt_Select_PurchaseReport";

        public const string RPT_SELECT_PURCHASECOMBINED = "Rpt_Select_PurchaseCombined";
        public const string RPT_SELECT_PURCHASERETURN = "Rpt_Select_PurchaseReturn";
        public const string RPT_BANKSTATEMENT_GETBYDATE = "Rpt_BankStatement_GetByDate";
        public const string RPT_DAYBOOK_GETBYDATE = "Rpt_DayBook_GetByDate";
        public const string RPT_SELECT_PRICING = "Rpt_Select_Pricing";
        public const string RPT_SELECT_PRICING_SMSCODE = "Rpt_Select_Pricing_SMSCode";
        public const string RPT_SELECT_PRICELOG = "Rpt_Select_PriceLog";
        public const string RPT_SELECT_CUSTOMERADDRESS = "Rpt_Select_CustomerAddress";
        public const string RPT_SELECT_SUPPLIERADDRESS = "Rpt_Select_SupplierAddress";
        public const string RPT_STOCKREPORT_GETBYDATE = "Rpt_StockReport_GetByDate";
        public const string RPT_PURCHASESTATEMENT_GETDYNAMIC = "Rpt_PurchaseStatement_GetDynamic";

        public const string RPT_PUNCHLOG_GETDYNAMIC = "Rpt_PunchLog_GetDynamic";
        public const string RPT_PUNCHLOGS_GETDYNAMIC = "Rpt_PunchLogs_GetDynamic";

        public const string RPT_PURCHASESTATEMENTNEW_GETDYNAMIC = "Rpt_PurchaseStatementNew_GetDynamic";
        public const string RPT_PURCHASEBILLSTATEMENT_GETBYDATE = "Rpt_PurchaseBillStatement_GetByDate";
        public const string RPT_SALESBILLSTATEMENT_GETBYDATE = "Rpt_SalesBillStatement_GetByDate";
        public const string USP_EMPLOYEELOG_GETDYNAMIC = "usp_EmployeeLog_GetDynamic";

        //OPBillingTrans
        public const string USP_INSERT_OPBILLINGTRANS = "usp_Insert_OPBillingTrans";
        public const string USP_UPDATE_OPBILLINGTRANS = "usp_Update_OPBillingTrans";
        public const string USP_DELETE_OPBILLINGTRANS = "usp_Delete_OPBillingTrans";
        public const string USP_SELECT_OPBILLINGTRANS = "usp_Select_OPBillingTrans";
        public const string RPT_SELECT_SALESENTRY = "Rpt_Select_SalesEntry";
        public const string RPT_SELECT_YARNSALESENTRY = "Rpt_Select_YarnSalesEntry";
        public const string RPT_SELECT_TDSPAYMENTENTRY = "Rpt_Select_TDSPaymentEntry";
        public const string USP_INSERT_SMSLOG = "usp_Insert_SMSLog";
        public const string RPT_SELECT_RETAILSSALESENTRY = "Rpt_Select_RetailsSalesEntry";
        public const string RPT_SELECT_EXPENSEREPORT = "Rpt_Select_ExpenseReport";
        public const string RPT_SELECT_PUNCHREPORT = "Rpt_Select_PunchReport";
        public const string USP_INSERT_INWARD = "usp_Insert_Inward";
        public const string USP_UPDATE_INWARD = "usp_Update_Inward";
        public const string USP_DELETE_INWARD = "usp_Delete_Inward";
        public const string USP_SELECT_INWARD = "usp_Select_Inward";
        public const string USP_SELECT_INWARDDYNAMIC = "usp_Search_Inward";

        public const string USP_INSERT_INWARDTRANS = "usp_Insert_InwardTrans";
        public const string USP_UPDATE_INWARDTRANS = "usp_Update_InwardTrans";
        public const string USP_DELETE_INWARDTRANS = "usp_Delete_InwardTrans";
        public const string USP_SELECT_INWARDTRANS = "usp_Select_InwardTrans";

        public const string USP_INSERT_VENDORENTRY = "usp_Insert_VendorEntry";
        public const string USP_UPDATE_VENDORENTRY = "usp_Update_VendorEntry";
        public const string USP_DELETE_VENDORENTRY = "usp_Delete_VendorEntry";
        public const string USP_SELECT_VENDORENTRY = "usp_Select_VendorEntry";
        public const string USP_SEARCH_VENDORENTRY = "usp_Search_VendorEntry";
        public const string USP_SELECT_VENDORENTRYBYSTATUS = "usp_Select_VendorEntryByStatus";
        public const string USP_SELECT_VENDORSTOCKCHECK = "usp_Select_VendorStockCheck";
        public const string USP_SELECT_VENDORENTRYBYOPENINGQTY = "usp_Select_VendorEntryByOpeningQty";

        public const string USP_INSERT_VENDORTRANS = "usp_Insert_VendorTrans";
        public const string USP_UPDATE_VENDORTRANS = "usp_Update_VendorTrans";
        public const string USP_DELETE_VENDORTRANS = "usp_Delete_VendorTrans";
        public const string USP_SELECT_VENDORTRANS = "usp_Select_VendorTrans";

        public const string USP_INSERT_VENDORWORKTRANS = "usp_Insert_VendorWorkTrans";
        public const string USP_UPDATE_VENDORWORKTRANS = "usp_Update_VendorWorkTrans";
        public const string USP_DELETE_VENDORWORKTRANS = "usp_Delete_VendorWorkTrans";
        public const string USP_SELECT_VENDORWORKTRANS = "usp_Select_VendorWorkTrans";

        public const string USP_INSERT_PURCHASE = "usp_Insert_Purchase";
        public const string USP_UPDATE_PURCHASE = "usp_Update_Purchase";
        public const string USP_UPDATE_PURCHASEPENIDNG = "usp_Update_PurchasePending";
        public const string USP_DELETE_PURCHASE = "usp_Delete_Purchase";
        public const string USP_SELECT_PURCHASE = "usp_Select_Purchase";
        public const string USP_SELECT_PURCHASE_GETBILLNO = "usp_Select_Purchase_GetBillNo";
        public const string USP_SELECT_PURCHASE_GETBILLNO_DISCOUNT = "usp_Select_Purchase_GetBillNo_Discount";
        public const string USP_SELECT_PURCHASE_PENDINGGETBILLNO_DISCOUNT = "usp_Select_Purchase_PendingGetBillNo_Discount";
        public const string USP_SELECT_TOPPURCHASE = "usp_Select_TopPurchase";
        public const string USP_SELECT_PURCHASEINVOICE = "usp_Select_PurchaseInvoice";
        public const string USP_SELECT_PURCHASEDYNAMIC = "usp_Search_Purchase";
        public const string USP_SEARCH_PURCHASEPENDING = "usp_Search_PurchasePending";
        public const string USP_SELECT_PENDINGPURCHASE = "usp_Select_PendingPurchase";
        public const string USP_SELECT_TOPPURCHASESUPPLIERWISE = "usp_Select_TopPurchaseSupplierWise";
        public const string USP_SELECT_TOPPURCHASEPENDING = "usp_Select_TopPurchasePending";
        public const string USP_SELECT_ADJUSTTDSPURCHASEENTRY = "usp_Select_AdjustTDSPurchaseEntry";
        public const string USP_SELECT_TDSPURCHASEENTRY = "usp_Select_TDSPurchaseEntry";
        public const string USP_SELECT_TDSPURCHASEBYID = "usp_Select_TDSPurchaseByID";

        public const string USP_SELECT_DCPURCHASE = "usp_Select_DCPurchase";
        public const string USP_SELECT_TOPDCPURCHASE = "usp_Select_TopDCPurchase";
        public const string USP_SELECT_DCPURCHASEDYNAMIC = "usp_Search_DCPurchase";
        public const string USP_SELECT_PENDINGDCPURCHASE = "usp_Select_PendingDCPurchase";


        public const string USP_INSERT_PURCHASETRANS = "usp_Insert_PurchaseTrans";
        public const string USP_UPDATE_PURCHASETRANS = "usp_Update_PurchaseTrans";
        public const string USP_DELETE_PURCHASETRANS = "usp_Delete_PurchaseTrans";
        public const string USP_SELECT_PURCHASETRANS = "usp_Select_PurchaseTrans";
        public const string USP_SELECT_PURCHASETRANSDETAILS = "usp_Select_PurchaseTransDetails";

        public const string USP_INSERT_JOURNAL = "usp_Insert_Journal";
        public const string USP_UPDATE_JOURNAL = "usp_Update_Journal";
        public const string USP_DELETE_JOURNAL = "usp_Delete_Journal";
        public const string USP_SELECT_JOURNAL = "usp_Select_Journal";
        public const string USP_SELECT_JOURNALDYNAMIC = "usp_Search_Journal";

        public const string USP_INSERT_JOURNALTRANS = "usp_Insert_JournalTrans";
        public const string USP_UPDATE_JOURNALTRANS = "usp_Update_JournalTrans";
        public const string USP_DELETE_JOURNALTRANS = "usp_Delete_JournalTrans";
        public const string USP_SELECT_JOURNALTRANS = "usp_Select_JournalTrans";

        public const string USP_INSERT_PURCHASERETURN = "usp_Insert_PurchaseReturn";
        public const string USP_UPDATE_PURCHASERETURN = "usp_Update_PurchaseReturn";
        public const string USP_DELETE_PURCHASERETURN = "usp_Delete_PurchaseReturn";
        public const string USP_SELECT_PURCHASERETURN = "usp_Select_PurchaseReturn";
        public const string USP_SELECT_PURCHASERETURNDYNAMIC = "usp_Search_PurchaseReturn";

        public const string USP_INSERT_PURCHASERETURNTRANS = "usp_Insert_PurchaseReturnTrans";
        public const string USP_UPDATE_PURCHASERETURNTRANS = "usp_Update_PurchaseReturnTrans";
        public const string USP_DELETE_PURCHASERETURNTRANS = "usp_Delete_PurchaseReturnTrans";
        public const string USP_SELECT_PURCHASERETURNTRANS = "usp_Select_PurchaseReturnTrans";
        public const string USP_SELECT_PURCHASERETURNTRANSQUANTITY = "usp_Select_PurchaseReturnTransQuantity";

        public const string USP_INSERT_SALESORDER = "usp_Insert_SalesOrder";
        public const string USP_UPDATE_SALESORDER = "usp_Update_SalesOrder";
        public const string USP_DELETE_SALESORDER = "usp_Delete_SalesOrder";
        public const string USP_SELECT_SALESORDER = "usp_Select_SalesOrder";
        public const string USP_SELECT_SALESORDERDYNAMIC = "usp_Search_SalesOrder";

        public const string USP_INSERT_SALESORDERTRANS = "usp_Insert_SalesOrderTrans";
        public const string USP_UPDATE_SALESORDERTRANS = "usp_Update_SalesOrderTrans";
        public const string USP_DELETE_SALESORDERTRANS = "usp_Delete_SalesOrderTrans";
        public const string USP_SELECT_SALESORDERTRANS = "usp_Select_SalesOrderTrans";

        public const string USP_INSERT_EXPENSE = "usp_Insert_Expense";
        public const string USP_UPDATE_EXPENSE = "usp_Update_Expense";
        public const string USP_DELETE_EXPENSE = "usp_Delete_Expense";
        public const string USP_SELECT_EXPENSE = "usp_Select_Expense";
        public const string USP_SELECT_TOPEXPENSE = "usp_Select_TopExpense";
        public const string USP_SELECT_PENDINGEXPENSE = "usp_Select_PendingExpense";
        public const string USP_SELECT_ADJUSTTDSEXPENSEENTRY = "usp_Select_AdjustTDSExpenseEntry";
        public const string USP_SELECT_TDSEXPENSEENTRY = "usp_Select_TDSExpenseEntry";
        public const string USP_SELECT_TDSEXPENSEBYID = "usp_Select_TDSExpenseByID";

        public const string USP_SELECT_EXPENSEDYNAMIC = "usp_Search_Expense";

        public const string USP_INSERT_EXPENSETRANS = "usp_Insert_ExpenseTrans";
        public const string USP_UPDATE_EXPENSETRANS = "usp_Update_ExpenseTrans";
        public const string USP_DELETE_EXPENSETRANS = "usp_Delete_ExpenseTrans";
        public const string USP_SELECT_EXPENSETRANS = "usp_Select_ExpenseTrans";

        public const string USP_INSERT_SALESENTRY = "usp_Insert_SalesEntry";
        public const string USP_UPDATE_SALESENTRY = "usp_Update_SalesEntry";
        public const string USP_DELETE_SALESENTRY = "usp_Delete_SalesEntry";
        public const string USP_SELECT_TRANSSTOCK = "usp_Select_TransStock";
        public const string USP_SELECT_SALESENTRY = "usp_Select_SalesEntry";
        public const string USP_SELECT_SALESENTRYJSON = "usp_Select_SalesEntryJSON";
        public const string USP_SELECT_SALESENTRYJSONHSN = "usp_Select_SalesEntryJSONHSN";
        public const string USP_SELECT_SALESRETURNJSONHSN = "usp_Select_SalesReturnJSONHSN";

        public const string USP_INSERT_OUTWARD = "usp_Insert_Outward";
        public const string USP_UPDATE_OUTWARD = "usp_Update_Outward";
        public const string USP_DELETE_OUTWARD = "usp_Delete_Outward";
        public const string USP_SELECT_OUTWARDTRANSSTOCK = "usp_Select_OutwardTransStock";
        public const string USP_SELECT_OUTWARD = "usp_Select_Outward";
        public const string USP_SELECT_OUTWARDJSON = "usp_Select_OutwardJSON";
        public const string USP_SELECT_SOOUTWARDNHSN = "usp_Select_OutwardJSONHSN";
        public const string USP_SELECT_TOPOUTWARD = "usp_Select_ToPOutward";
        public const string USP_SELECT_TOPOUTWARDDELETE = "usp_Select_TopOutwardDelete";
        public const string USP_SELECT_OUTWARDBOOKINGBILL = "usp_Select_OutwardBookingBill";
        public const string USP_SELECT_OUTWARDBILLBOOKINGDYNAMIC = "usp_Select_OutwardBillBookingDynamic";
        public const string USP_SELECT_OUTWARDLASTBILL = "usp_Select_OutwardLastBill";
        public const string USB_SELECT_OUTWARDRETAILAMOUNTCLEAR = "usb_Select_OutwardRetailAmountClear";
        public const string USP_SELECT_OUTWARDPENDINGRETAILBILLS = "usp_Select_OutwardPendingRetailBills";
        public const string USP_SELECT_OUTWARDRDYNAMIC = "usp_Select_OutwardDynamic";
        public const string USP_SELECT_OUTWARDTRANS = "usp_Select_OutwardTrans";
        public const string USP_INSERT_OUTWARDTRANS = "usp_Insert_OutwardTrans";
        public const string USP_UPDATE_OUTWARDTRANS = "usp_Update_OutwardTrans";
        public const string USP_DELETE_OUTWARDTRANS = "usp_Delete_OutwardTrans";



        public const string USP_SELECT_SALESENTRYBYRETURNID = "usp_Select_SalesEntryByReturnID";
        public const string USP_SELECT_SALESENTRYLASTBILL = "usp_Select_SalesEntryLastBill";
        public const string USP_SELECT_PAIDAMOUNTBYSALESENTRYID = "usp_Select_PaidAmountBySalesEntryID";
        public const string USP_SELECT_TOPSALESENTRY = "usp_Select_TopSalesEntry";
        public const string USP_SELECT_TOPSALESENTRYBYRETAILS = "usp_Select_TopSalesEntryByRetails";
        public const string USP_SELECT_TOPSALESENTRYDELETE = "usp_Select_TopSalesEntryDelete";
        public const string USP_SELECT_SALESENTRYBOOKINGBILL = "usp_Select_SalesEntryBookingBill";
        public const string USP_SELECT_PENDINGSALESENTRY = "usp_Select_PendingSalesEntry";
        public const string USP_SELECT_SALESENTRYDYNAMIC = "usp_Select_SalesEntryDynamic";
        public const string USP_SELECT_SALESENTRYDELETELISTDYNAMIC = "usp_Select_SalesEntryDeleteListDynamic";
        public const string USP_SELECT_SALESENTRYINVOICE = "usp_Select_SalesEntryInvoice";
        public const string USP_SELECT_SALESENTRYINVOICERETURN = "usp_Select_SalesEntryInvoiceReturn";
        public const string USP_SELECT_PENDINGRETAILSALES = "usp_Select_PendingRetailSales";
        public const string USP_SELECT_PENDINGRETAILBILLS = "usp_Select_PendingRetailBills";
        public const string USP_SELECT_SALESENTRYBYADDRESS = "usp_Select_SalesEntryByAddress";
        public const string USP_SELECT_SALESENTRYBILLBOOKINGDYNAMIC = "usp_Select_SalesEntryBillBookingDynamic";
        public const string USB_SELECT_RETAILAMOUNTCLEAR = "usb_Select_RetailAmountClear";
        public const string USP_SELECT_TDSSALESENTRY = "usp_Select_TDSSalesEntry";
        public const string USP_SELECT_ADJUSTTDSSALESENTRY = "usp_Select_AdjustTDSSalesEntry";

        public const string USP_SELECT_TDSSOUTWARD = "usp_Select_TDSOutward";
        public const string USP_SELECT_ADJUSTTDSOUTWARD = "usp_Select_AdjustTDSOutward";



        public const string USP_INSERT_YARNSALESENTRY = "usp_Insert_YarnSalesEntry";
        public const string USP_UPDATE_YARNSALESENTRY = "usp_Update_YarnSalesEntry";
        public const string USP_DELETE_YARNSALESENTRY = "usp_Delete_YarnSalesEntry";
        public const string USP_SELECT_YARNSALESENTRY = "usp_Select_YarnSalesEntry";
        public const string USP_SELECT_TOPYARNSALESENTRY = "usp_Select_TopYarnSalesEntry";
        public const string USP_SELECT_YARNSALESENTRYDYNAMIC = "usp_Select_YarnSalesEntryDynamic";

        public const string USP_INSERT_YARNSALESENTRYTRANS = "usp_Insert_YarnSalesEntryTrans";
        public const string USP_UPDATE_YARNSALESENTRYTRANS = "usp_Update_YarnSalesEntryTrans";
        public const string USP_DELETE_YARNSALESENTRYTRANS = "usp_Delete_YarnSalesEntryTrans";
        public const string USP_SELECT_YARNSALESENTRYTRANS = "usp_Select_YarnSalesEntryTrans";

        //YarnSalesEntryProduct
        public const string USP_SELECT_YARNSALESENTRYPRODUCT = "usp_Select_YarnSalesEntry";
        //Estimate Sales Entry
        public const string USP_INSERT_ESTIMATESALESENTRY = "usp_Insert_EstimateSalesEntry";
        public const string USP_UPDATE_ESTIMATESALESENTRY = "usp_Update_EstimateSalesEntry";
        public const string USP_DELETE_ESTIMATESALESENTRY = "usp_Delete_EstimateSalesEntry";
        public const string USP_UPDATE_ESTIMATESALESENTRYSTATUS = "usp_Update_EstimateSalesEntryStatus";

        public const string USP_SELECT_ESTIMATESALESENTRY = "usp_Select_EstimateSalesEntry";
        public const string USP_SELECT_TOPESTIMATESALESENTRY = "usp_Select_TopEstimateSalesEntry";
        public const string USP_SELECT_ESTIMATESALESENTRYBOOKINGBILL = "usp_Select_EstimateSalesEntryBookingBill";
        public const string USP_SELECT_PENDINGESTIMATESALESENTRY = "usp_Select_PendingEstimateSalesEntry";
        public const string USP_SELECT_ESTIMATESALESENTRYDYNAMIC = "usp_Select_EstimateSalesEntryDynamic";
        public const string USP_SELECT_ESTIMATESALESENTRYINVOICE = "usp_Select_EstimateSalesEntryInvoice";
        public const string USP_SELECT_PENDINGRETAILESTIMATESALES = "usp_Select_PendingRetailEstimateSales";
        public const string USP_SELECT_ESTIMATESALESENTRYBYADDRESS = "usp_Select_EstimateSalesEntryByAddress";
        public const string USP_SELECT_ESTIMATESALESENTRYBILLBOOKINGDYNAMIC = "usp_Select_EstimateSalesEntryBillBookingDynamic";
        public const string USP_SELECT_SALES_GETBILLNO_DISCOUNT = "usp_Select_Sales_GetBillNo_Discount";
        public const string USP_SELECT_SALESENTRY_PENDINGGETBILLNO_DISCOUNT = "usp_Select_SalesEntry_PendingGetBillNo_Discount";

        public const string USP_INSERT_ESTIMATESALESENTRYTRANS = "usp_Insert_EstimateSalesEntryTrans";
        public const string USP_UPDATE_ESTIMATESALESENTRYTRANS = "usp_Update_EstimateSalesEntryTrans";
        public const string USP_DELETE_ESTIMATESALESENTRYTRANS = "usp_Delete_EstimateSalesEntryTrans";
        public const string USP_SELECT_ESTIMATESALESENTRYTRANS = "usp_Select_EstimateSalesEntryTrans";
        public const string USP_SELECT_ESTIMATESALESENTRYTRANSDETAILS = "usp_Select_EstimateSalesEntryTransDetails";
        public const string USP_SELECT_ESTIMATESALESENTRYTRANSDETAILSBYSMSCODE = "usp_Select_EstimateSalesEntryTransDetailsBySMSCode";
        public const string USP_SELECT_NEWESTIMATESALESENTRYTRANSDETAILS = "usp_Select_NewEstimateSalesEntryTransDetails";

        public const string USP_SELECT_ESTIMATEOUTWARDTRANSDETAILSBYSMSCODE = "usp_Select_EstimateOutwardTransDetailsBySMSCode";

        public const string USP_INSERT_SALESENTRYTRANS = "usp_Insert_SalesEntryTrans";
        public const string USP_UPDATE_SALESENTRYTRANS = "usp_Update_SalesEntryTrans";
        public const string USP_DELETE_SALESENTRYTRANS = "usp_Delete_SalesEntryTrans";
        public const string USP_SELECT_SALESENTRYTRANS = "usp_Select_SalesEntryTrans";
        public const string USP_SELECT_SALESENTRYTRANSDETAILS = "usp_Select_SalesEntryTransDetails";
        public const string USP_SELECT_SALESENTRYTRANSDETAILSBYSMSCODE = "usp_Select_SalesEntryTransDetailsBySMSCode";
        public const string USP_SELECT_NEWSALESENTRYTRANSDETAILS = "usp_Select_NewSalesEntryTransDetails";
        public const string USP_SELECT_NEWPURCHASEENTRYTRANSDETAILS = "usp_Select_NewPurchaseEntryTransDetails";

        public const string USP_INSERT_PURCHASEORDER = "usp_Insert_PurchaseOrder";
        public const string USP_UPDATE_PURCHASEORDER = "usp_Update_PurchaseOrder";
        public const string USP_DELETE_PURCHASEORDER = "usp_Delete_PurchaseOrder";
        public const string USP_SELECT_PURCHASEORDER = "usp_Select_PurchaseOrder";
        public const string USP_SELECT_PURCHASEORDERDYNAMIC = "usp_Search_PurchaseOrder";

        public const string USP_INSERT_PURCHASEORDERTRANS = "usp_Insert_PurchaseOrderTrans";
        public const string USP_UPDATE_PURCHASEORDERTRANS = "usp_Update_PurchaseOrderTrans";
        public const string USP_DELETE_PURCHASEORDERTRANS = "usp_Delete_PurchaseOrderTrans";
        public const string USP_SELECT_PURCHASEORDERTRANS = "usp_Select_PurchaseOrderTrans";

        public const string USP_INSERT_PROCESSINGINWARD = "usp_Insert_ProcessingInward";
        public const string USP_UPDATE_PROCESSINGINWARD = "usp_Update_ProcessingInward";
        public const string USP_DELETE_PROCESSINGINWARD = "usp_Delete_ProcessingInward";
        public const string USP_SELECT_PROCESSINGINWARD = "usp_Select_ProcessingInward";
        public const string USP_SELECT_PROCESSINGINWARDDYNAMIC = "usp_Search_ProcessingInward";
        public const string USP_SELECT_PENDINGPROCESSINGINWARD = "usp_Select_PendingProcessingInward";

        public const string USP_INSERT_PROCESSINGINWARDTRANS = "usp_Insert_ProcessingInwardTrans";
        public const string USP_UPDATE_PROCESSINGINWARDTRANS = "usp_Update_ProcessingInwardTrans";
        public const string USP_DELETE_PROCESSINGINWARDTRANS = "usp_Delete_ProcessingInwardTrans";
        public const string USP_SELECT_PROCESSINGINWARDTRANS = "usp_Select_ProcessingInwardTrans";

        public const string USP_INSERT_PROCESSINGOUTWARD = "usp_Insert_ProcessingOutward";
        public const string USP_UPDATE_PROCESSINGOUTWARD = "usp_Update_ProcessingOutward";
        public const string USP_DELETE_PROCESSINGOUTWARD = "usp_Delete_ProcessingOutward";
        public const string USP_SELECT_PROCESSINGOUTWARD = "usp_Select_ProcessingOutward";
        public const string USP_SELECT_PROCESSINGOUTWARDDYNAMIC = "usp_Search_ProcessingOutward";

        public const string USP_INSERT_PROCESSINGOUTWARDTRANS = "usp_Insert_ProcessingOutwardTrans";
        public const string USP_UPDATE_PROCESSINGOUTWARDTRANS = "usp_Update_ProcessingOutwardTrans";
        public const string USP_DELETE_PROCESSINGOUTWARDTRANS = "usp_Delete_ProcessingOutwardTrans";
        public const string USP_SELECT_PROCESSINGOUTWARDTRANS = "usp_Select_ProcessingOutwardTrans";

        // Web Services
        public const string USP_SELECT_REGISTERSERVICE = "usp_Select_RegisterService";
        public const string USP_SELECT_RENEWALSERVICE = "usp_Select_RenewalService";
        public const string RPT_SELECT_UNPAIDREGISTERREPORT = "Rpt_Select_UnpaidRegisterReport";
        public const string RPT_SELECT_REGISTER = "Rpt_Select_Register";
        public const string RPT_SELECT_RENEWAL = "Rpt_Select_Renewal";
        public const string USP_SELECT_VISITCUSTOMER = "usp_Select_VisitCustomer";

        public const string USP_INSERT_LRENTRY = "usp_Insert_LREntry";
        public const string USP_UPDATE_LRENTRY = "usp_Update_LREntry";
        public const string USP_UPDATE_LRENTRYSTATUS = "usp_Update_LREntryStatus";
        public const string USP_DELETE_LRENTRY = "usp_Delete_LREntry";
        public const string USP_SELECT_LRENTRY = "usp_Select_LREntry";
        public const string USP_SELECT_LRENTRYDYNAMIC = "usp_Search_LREntry";
        public const string USP_SELECT_GETLRENTRYSALESID = "usp_Select_GetLREntrySalesID";

        public const string USP_INSERT_LRENTRYTRANS = "usp_Insert_LREntryTrans";
        public const string USP_UPDATE_LRENTRYTRANS = "usp_Update_LREntryTrans";
        public const string USP_DELETE_LRENTRYTRANS = "usp_Delete_LREntryTrans";
        public const string USP_SELECT_LRENTRYTRANS = "usp_Select_LREntryTrans";

        public const string USP_INSERT_INLRENTRY = "usp_Insert_INLREntry";
        public const string USP_UPDATE_INLRENTRY = "usp_Update_INLREntry";
        public const string USP_UPDATE_INLRENTRYSTATUS = "usp_Update_INLREntryStatus";
        public const string USP_DELETE_INLRENTRY = "usp_Delete_INLREntry";

        public const string USP_SELECT_INLRENTRY = "usp_Select_INLREntry";
        public const string USP_SELECT_INLRENTRYDYNAMIC = "usp_Search_INLREntry";
        public const string USP_SELECT_INGETLRENTRYSALESID = "usp_Select_GetINLREntryPurchaseID";

        public const string USP_INSERT_INLRENTRYTRANS = "usp_Insert_INLREntryTrans";
        public const string USP_UPDATE_INLRENTRYTRANS = "usp_Update_INLREntryTrans";
        public const string USP_DELETE_INLRENTRYTRANS = "usp_Delete_INLREntryTrans";
        public const string USP_SELECT_INLRENTRYTRANS = "usp_Select_INLREntryTrans";

        public const string USP_SELECT_PRICE = "usp_Select_Price";
        public const string USP_SELECT_WHOLESALEPRICE = "usp_Select_WholeSalePrice";
        public const string USP_SELECT_RETAILPRICE = "usp_Select_RetailPrice";
        public const string USP_SELECT_PRICEBYPRODUCT = "usp_Select_PriceByProduct";
        public const string RPT_SELECT_BARCODE = "Rpt_Select_Barcode";
        public const string RPT_SELECT_INWARD = "Rpt_Select_Inward";
        public const string RPT_SELECT_LRENTRY = "Rpt_Select_InLREntry";
        public const string RPT_SELECT_INLRENTRY = "Rpt_Select_INLREntryReport";

        public const string RPT_SELECT_SALESGST = "Rpt_Select_SalesGST";
        public const string RPT_SELECT_SALESRETURNGST = "Rpt_Select_SalesReturnGST";
        public const string RPT_SELECT_PURCHASEGST = "Rpt_Select_PurchaseGST";
        public const string RPT_SELECT_PURCHASERETURNGST = "RPT_SELECT_PURCHASERETURNGST";
        public const string RPT_SELECT_PURCHASEDISCOUNTGST = "Rpt_Select_PurchaseDiscountGST";
        public const string RPT_SELECT_SALESDISCOUNTGST = "Rpt_Select_SalesDiscountGST";


        public const string RPT_SELECT_SALESTAXRETURN = "Rpt_Select_SalesTaxReturn";
        public const string RPT_SELECT_MARGINREPORT = "Rpt_Select_MarginReport";
        public const string RPT_SELECT_MARGINREPORT_GETDYNAMIC = "Rpt_Select_MarginReport_GetDynamic";
        public const string RPT_SELECT_SALESMARGINREPORT_GETDYNAMIC = "Rpt_Select_SalesMarginReport_GetDynamic";

        //Exchange
        public const string USP_INSERT_SALESEXCHANGETRANS = "usp_Insert_SalesExchangeTrans";
        public const string USP_UPDATE_SALESEXCHANGETRANS = "usp_Update_SalesExchangeTrans";
        public const string USP_DELETE_SALESEXCHANGETRANS = "usp_Delete_SalesExchangeTrans";
        public const string USP_SELECT_SALESEXCHANGETRANS = "usp_Select_SalesExchangeTrans";
        public const string RPT_SELECT_SALESPENDING = "Rpt_Select_SalesPending";
        public const string RPT_SELECT_PURCHASEPENDING = "Rpt_Select_PurchasePending";
        public const string RPT_SELECT_EXPENSEPENDING = "Rpt_Select_ExpensePendingReport";
        public const string RPT_SELECT_RETAILPENDING = "Rpt_Select_RetailPending";

        //LetterPad
        public const string USP_INSERT_LETTERPAD = "usp_Insert_LetterPad";
        public const string USP_UPDATE_LETTERPAD = "usp_Update_Letterpad";
        public const string USP_DELETE_LETTERPAD = "usp_Delete_LetterPad";
        public const string USP_SELECT_LETTERPAD = "usp_Select_LetterPad";
        public const string USP_SEARCH_LETTERPAD = "usp_Search_LetterPad";
        public const string USP_SELECT_TOPLETTERPAD = "usp_Select_TopLetterPad";
        public const string RPT_SELECT_LETTERPAD = "Rpt_Select_LetterPad";

        public const string USP_SELECT_PRODUCTIMAGES = "usp_Select_ProductImages";
        public const string USP_INSERT_PRODUCTIMAGES = "usp_Insert_ProductImages";
        public const string USP_DELETE_PRODUCTIMAGES = "usp_Delete_ProductImages";

        //PurchaseDiscount
        public const string USP_INSERT_PURCHASEDISCOUNT = "usp_Insert_PurchaseDiscount";
        public const string USP_UPDATE_PURCHASEDISCOUNT = "usp_Update_PurchaseDiscount";
        public const string USP_SELECT_PURCHASEDISCOUNT = "usp_Select_PurchaseDiscount";
        public const string USP_SELECT_TOPPURCHASEDISCOUNT = "usp_Select_TopPurchaseDiscount";
        public const string USP_DELETE_PURCHASEDISCOUNT = "usp_Delete_PurchaseDiscount";
        public const string USP_SEARCH_PURCHASEDISCOUNT = "usp_Search_PurchaseDiscount";
        public const string RPT_SELECT_PURCHASEDISCOUNT = "Rpt_Select_PurchaseDiscount";

        //Sales Discount
        public const string USP_INSERT_SALESDISCOUNT = "usp_Insert_SalesDiscount";
        public const string USP_UPDATE_SALESDISCOUNT = "USP_UPDATE_SALESDISCOUNT";
        public const string USP_SELECT_SALESDISCOUNT = "usp_Select_SalesDiscount";
        public const string USP_SELECT_TOPSALESDISCOUNT = "usp_Select_TopSalesDiscount";
        public const string USP_DELETE_SALESDISCOUNT = "usp_Delete_SalesDiscount";
        public const string USP_SEARCH_SALESDISCOUNT = "usp_Search_SalesDiscount";
        public const string RPT_SELECT_SALESDISCOUNT = "Rpt_Select_SalesDiscount";



        public const string USP_INSERT_RETAILOUTWARD = "usp_Insert_RetailOutward";
        public const string USP_UPDATE_RETAILOUTWARD = "usp_Update_RetailOutward";
        public const string USP_DELETE_RETAILOUTWARD = "usp_Delete_RetailOutward";
        public const string USP_SELECT_RETAILOUTWARD = "usp_Select_RetailOutward";
        public const string USP_SELECT_RETAILOUTWARDRDYNAMIC = "usp_Select_RetailOutwardDynamic";
        public const string USP_SELECT_TOPRETAILOUTWARD = "usp_Select_ToPRetailOutward";
        public const string USP_SELECT_RETAILOUTWARDLASTBILL = "usp_Select_RetailOutwardLastBill";
        public const string USP_SELECT_RETAILOUTWARDTRANS = "usp_Select_RetailOutwardTrans";
        public const string USP_INSERT_RETAILOUTWARDTRANS = "usp_Insert_RetailOutwardTrans";
        public const string USP_UPDATE_RETAILOUTWARDTRANS = "usp_Update_RetailOutwardTrans";
        public const string USP_DELETE_RETAILOUTWARDTRANS = "usp_Delete_RetailOutwardTrans";


        public const string USP_CHECK_PRODUCTBYQTY = "usp_Check_ProductByQty";

        public const string USP_SELECT_PURCHASERETURNJSONHSN = "usp_Select_PurchaseReturnJSONHSN";

        //Purchase TDSPaymentTrans
        public const string USP_INSERT_PURCHASETDSPAYMENTTRANS = "usp_Insert_PurchaseTDSPaymentTrans";
        public const string USP_UPDATE_PURCHASETDSPAYMENTTRANS = "usp_Update_PurchaseTDSPaymentTrans";
        public const string USP_DELETE_PURCHASETDSPAYMENTTRANS = "usp_Delete_PurchaseTDSPaymentTrans";
        public const string USP_SELECT_PURCHASETDSPAYMENTTRANS = "usp_Select_PurchaseTDSPaymentTrans";

        //Purchase PurchaseTDS Payment
        public const string USP_UPDATE_PURCHASETDSPAYMENT = "usp_Update_PurchaseTDSPayment";
        public const string USP_SELECT_PURCHASETDSPAYMENT = "usp_Select_PurchaseTDSPayment";
        public const string USP_SEARCH_PURCHASETDSPAYMENT = "usp_Search_PurchaseTDSPayment";
        public const string USP_INSERT_PURCHASETDSPAYMENT = "usp_Insert_PurchaseTDSPayment";
        public const string USP_DELETE_PURCHASETDSPAYMENT = "usp_Delete_PurchaseTDSPayment";

        //HSNMaster
        public const string USP_INSERT_HSNMASTER = "usp_Insert_HSNMaster";
        public const string USP_UPDATE_HSNMASTER = "usp_Update_HSNMaster";
        public const string USP_DELETE_HSNMASTER = "usp_Delete_HSNMaster";
        public const string USP_SELECT_HSNMASTER = "usp_Select_HSNMaster";

        //HSNMaster
        public const string USP_INSERT_BUYERCONTACTTYPE = "usp_Insert_BuyerContactType";
        public const string USP_UPDATE_BUYERCONTACTTYPE = "usp_Update_BuyerContactType";
        public const string USP_DELETE_BUYERCONTACTTYPE = "usp_Delete_BuyerContactType";
        public const string USP_SELECT_BUYERCONTACTTYPE = "usp_Select_BuyerContactType";

    }
}