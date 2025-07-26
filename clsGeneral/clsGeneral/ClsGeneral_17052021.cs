using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using FAXCOMLib;
using System.Net.Mail;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Microsoft.Win32;
using System.Diagnostics;
using Barcode;
using System.Data;
using System.Net;
using Outlook = Microsoft.Office.Interop.Outlook;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using EmailLibrary;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing.Imaging;
using System.Threading;
namespace MALL
{
    // Data base connection .................Start........... */

    # region testdtp
    /*
    public class CalendarColumn : DataGridViewColumn
    {
        public CalendarColumn()
            : base(new CalendarCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    public class CalendarCell : DataGridViewTextBoxCell
    {

        public CalendarCell()
            : base()
        {
            // Use the short date format.
            this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            CalendarEditingControl ctl =
                DataGridView.EditingControl as CalendarEditingControl;
            ctl.Value = (DateTime)(this.Value == null ? DateTime.Today : (this.Value.ToString().Trim() == "" ? DateTime.Today : this.Value));
            //ctl.Value = (DateTime)this.Value;//==null.Equals("";
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(CalendarEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.
                return typeof(DateTime);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return DateTime.Now;
            }
        }
    }

    class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        public CalendarEditingControl()
        {
            this.Format = DateTimePickerFormat.Short;
        }

        // Implements the IDataGridViewEditingControl.EditingControlFormattedValue 
        // property.
        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToShortDateString();
            }
            set
            {
                if (value is String)
                {
                    this.Value = DateTime.Parse((String)value);
                }
            }
        }

        // Implements the 
        // IDataGridViewEditingControl.GetEditingControlFormattedValue method.
        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        // Implements the 
        // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        // Implements the IDataGridViewEditingControl.EditingControlRowIndex 
        // property.
        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey 
        // method.
        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit 
        // method.
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }

        // Implements the IDataGridViewEditingControl
        // .RepositionEditingControlOnValueChange property.
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlDataGridView property.
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlValueChanged property.
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingPanelCursor property.
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            // Notify the DataGridView that the contents of the cell
            // have changed.
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }
    */
    # endregion testdtp


    public static class Defaults
    {

        public static string Def_Central_Loc = "SCL";
        /* LOCATION VARIABLES -- See Note Before Changing (DO NOT CHANGE)*/
        public static string Def_Base_Management_LOC = "";
        public static bool Def_RemoteLogin = false;
        // PRIVILEGE MANAGEMENT
        public static string Def_Base_LOC = "MAL";
        // by default its physical location 
        // in case if its not individualLoc & login location=its management location then Login Location
        // ex:- login to SAS from SHM then SAS, but login to SAS from W36=w36
        public static string Def_LOC = "MAL";
        // LOGIN LOCATION
        public static string Def_DBLOC_4Transfer = "MAL";
        // This is for managing data transfer, BASE_LOC from  sys_defaults table as its.
        // eg:- SHM,SSC,MAL,W36 (but for SAS=SHM)

        public static string Def_LOC_Name = "SAFARI MALL";
        public static string Def_Main_Loc = "MAL";
        // The central location (Only One)

        public static string Def_Loc_Type = "MN";
        // MN=main loc, WH warehouse, SH showroom
        public static string Def_Loc_Group = "MAL";
        public static bool Def_MAIN_LOCATION = true;
        public static bool Def_Base_Individual_Loc = true;
        /*                  */

        public static string Def_Data_Source = ""; //DSN
        public static OracleConnection Def_Conn = null;
        public static string Def_UpdateUrl = "";
        public static string Def_ApplicationVersion = "";
        public static string Def_UserGroup = "";

        public static string Def_User = "KRP";

        public static string Def_Authorised_User = "";      // For Authorisation Purpose eg LPO Purchase Limit
        public static string Def_Authorised_EMPID = "";     // For Authorisation Purpose eg LPO Purchase Limit
        public static string Def_Authorised_UserName = "";  // For Authorisation Purpose eg LPO Purchase Limit
        public static string Def_Authorised_UserGroup = ""; // For Authorisation Purpose eg LPO Purchase Limit

        public static string Def_YEAR = "2010";
        public static string Def_Acct_Purch = "8001";
        public static string Def_Acct_Purch_Ret = "8003";
        public static string Def_Acct_Sale = "9001";
        public static string Def_Acct_Sale_Ret = "9007"; // Added For Sales Return No Defaults In Sys_Defaults Table // 20180716
        public static string Def_Acct_SundryCr = "2000";
        public static string Def_Acct_SundryDr = "2001";
        //public static string Def_Acct_Sale_Ret = "9007";
        public static string Def_Acct_Cash = "1105";
        //new code  march 21
        public static string Def_Acct_Cs_Purchase = "8005";
        public static string Def_Acct_Cr_Purchase = "8001";
        public static string Def_Acct_Cs_PurchaseRet = "8006";
        public static string Def_Acct_Cr_PurchaseRet = "8003";
        public static string Def_Acct_Cs_Sale = "9001";
        public static string Def_Acct_Cr_Sale = "9009";
        public static string Def_Acct_Sale_Online = "9002";
        //
        //NEW CODE 22OCT2016 SHAREEF
        public static string Def_Acct_Discount = "8008";

        public static string Def_Acct_Bank = "1103";
        public static string Def_Acct_Sup_DebitNote = "8004";
        public static string Rep_Path = Environment.CurrentDirectory + "\\reports\\";

        public static string Exp_Path = Environment.CurrentDirectory + "\\exports\\";
        public static string Mail_Path_Stk = Environment.CurrentDirectory + "\\mailbox\\";
        public static string DEF_SMTP_CLIENT = "smtp.gmail.com";
        //public static string Mail_Path_Stk = "C:\\Mailbox\\STK\\STKReport.xls";
        // public static string Exp_Path = Application.ExecutablePath + "\\exports\\";
        //public static string Mail_Path_Stk = "C:\\Mailbox\\STK\\STKReport.pdf";
        // public static string Mail_Path_Stk = "C:\\Mailbox\\STK\\STKReport.xls";
        //
        public static string Rep_Path_LPO = "C:\\projects\\crystalreports\\LPO\\LpoReport1.rpt";
        public static string Rep_Path_GRV = "C:\\projects\\crystalreports\\GRV\\GrvReport1.rpt";
        public static string Rep_Path_SO = "C:\\projects\\crystalreports\\salesorder\\SalesorderReport1.rpt";
        public static string Rep_Path_GTV = "C:\\projects\\crystalreports\\GTV\\StockReport1.rpt";
        public static string Rep_Path_PR = "C:\\projects\\crystalreports\\PR\\PurchaseReturnReport1.rpt";
        public static string Rep_Path_SR = "C:\\projects\\crystalreports\\SR\\.rpt";
        public static string Rep_Path_SI = "C:\\projects\\crystalreports\\SalesInvoice\\SalesInvoiceReport1.rpt";
        public static string Rep_Path_DN = "C:\\projects\\crystalreports\\DN\\DeliveryNoteReport.rpt";
        public static string Mail_Path_LPO = "C:\\Mailbox\\Purchase\\PurchaseReport.pdf";
        public static string Mail_Path_SO = "C:\\Mailbox\\SO\\SalesOrderReport.pdf";
        public static string Mail_Path_GRV = "C:\\Mailbox\\GRV\\GRVReport.pdf";
        public static string Mail_Path_PR = "C:\\Mailbox\\PR\\PrReport.pdf";
        public static string Mail_Path_GTV = "C:\\Mailbox\\GTV\\GTVReport.pdf";
        public static string Mail_Path_SR = "C:\\Mailbox\\SR\\SRReport.pdf";
        public static string Mail_Path_SI = "C:\\Mailbox\\SI\\SIReport.pdf";
        public static string Mail_Path_DN = "C:\\Mailbox\\DN\\DNReport.pdf";

        //

        //
        public static string Def_COMP = "SAFARI";
        public static string Def_POSGROUP = "A";
        public static string Def_POSID = "P02";
        public static string Def_EMPID = "KRP";
        public static string Def_Currency = "QRS";
        //public static string Def_SHIFTCODE = "MALP02091";
        public static string Def_SHIFTCODE;
        public static string Def_POS_DB = "POS";
        public static string Def_SERVER_DB = "MALL";
        public static string Def_OWN_DB = "MALL";
        public static string Def_POSSERVER_DB = "POSSERVER";
        public static string Def_DocNo;
        public static string Def_DocNo_LPO;
        public static string Def_DocNo_GTV;
        public static string Def_DocNo_GRV;
        public static string Def_DocNo_SO;
        public static string Def_DocNo_PR;
        public static string Def_DocNo_SR;
        public static string Def_DocNo_SI;
        public static string Def_DocNo_DN;
        public static string Def_Part_num_sup;
        //NEW CODE 
        public static string Def_UserName = "ITADMINISTRATORS";
        public static string Def_PassWord = "ITTEAM";

        public static string Def_SupplierInt = "000777";
        public static string Def_SupplierIntDivisionCod = "INT";              // For Supplier Division Added 19/Apr/2018 3:09 PM
        public static string Def_SupplierIntDivisionName = "GENERAL";        // For Supplier Division Added 19/Apr/2018 3:09 PM

        public static string Def_VegSupplier = "000903";
        public static string Def_VegSupplierDivisionCod = "VEG";              // For Supplier Division Added 19/Apr/2018 3:09 PM 
        public static string Def_VegSupplierDivisionName = "GENERAL";        // For Supplier Division Added 19/Apr/2018 3:09 PM

        // NEW CODE DP
        //public static string Def_Supplier_BDL = "BDL";
        //public static string Def_CATEGORY_BDL = "BDL";
        //public static string Def_BRAND_BDL = "BDL";


        public static string Def_BDL_Supplier = "000BDL";
        public static string Def_BDL_SupplierName = "BUNDLING";

        public static string Def_BDL_CategoryCode = "BP0101000000";
        public static string Def_BDL_CategoryName = "PROMOTION SALES";

        public static string Def_BDL_BrandCode = "BDL";
        public static string Def_BDL_BrandName = "BUNDLED ITEMS";
        public static string Def_BDL_DivisionCod = "40";              // For Supplier Division
        public static string Def_BDL_DivisionName = "GENERAL";        // For Supplier Division
        public static string Def_BDL_Pur_DivisionCod = "GEN";         // For Purchase Division
        public static string Def_BDL_Pur_DivisionName = "GENERAL";    // For Purchase Division

        public static string Def_Tel_Extn = "9,";
        public static string Def_Discount_Grv;
        public static string Def_Discount_Lpo;
        public static string Def_Fax_No;
        public static Form MdiMall;


        public static string Def_Country = "ARE";
        public static string Def_Country_MainLoc = "ARE";
        public static bool Def_CENTRAL_LOCATION = false;
        public static bool Def_Taxable_Loc = false;
        public static bool Def_Required_AcctConfirm_OnPRH = false;

        public static bool Def_BatchEnabled_Loc = false;
        public static bool Def_Shared_Folder = false;
        public static string Def_TRN = "100026812600003";
        public static string Def_Base_Currency = "USD";
        public static string Def_Acct_Vat_Input = "7777";
        public static string Def_Acct_Vat_Output = "6666";
        public static string Def_Loc_Serial = "011";
        public static string Def_Over_PurchaseLimit = "0";
        public static bool G_NO_CENTRAL_LOCATION = false;
        public static bool G_NO_SHOWROOM_LOCATION = false;
        public static bool G_Def_SPV_DIRECT_POSTING = false;
        public static bool G_NO_MAIN_LOCATION = false;

        public static bool G_ITEMLOCACTIVE_REQUIRED = false;
        public static string G_LC_CALCULATION = "NORMAL";
        public static bool G_NONLPOPRH_NOTIFY_MAIL = false;
        public static bool G_NOTIFY_PREV_LC_PRH = false;
        public static bool G_BLOCK_SAMEDAY_LPOPRH = false;
        public static bool G_PRH_HIST_LPOITEMADD = false;
        public static string G_LCCALCULATION_OLDLCZERO = "NO";

        public static string G_APPR_SUBSIDIARY_LOCTYPE = "CN";
        public static string G_APPR_ITEMLISTING_LOCTYPE = "CN";
        public static string G_APPR_BARCODE_LOCTYPE = "CN";
        public static string G_APPR_BDLLUG_LOCTYPE = "CN";
        public static string G_CREATE_BRAND_LOCTYPE = "CN";
        public static string G_CREATE_CATEGORY_LOCTYPE = "CN";
        public static string G_CREATE_PRHDIV_LOCTYPE = "CN";
        public static string G_CREATE_SUPDIV_LOCTYPE = "CN";
        public static string G_BUYING_CHANGE_LOCTYPE = "MN";
        public static string G_CREATE_BDLREQ_LOCTYPE = "CN";
        //added shareef 02/apr/2020
        public static int G_SCALE_BARCODE_LENGTH = 13;
        public static int G_SCALE_BARCODE_PN_X = 2;
        public static int G_SCALE_BARCODE_PN_Length = 4;
        public static int G_SCALE_BARCODE_PRC_X = 7;
        public static int G_SCALE_BARCODE_PRC_Length = 5;
        public static short[] G_SCALE_BARCODE_FORMAT = { 660, 661, 662, 663, 664, 665, 666, 667, 668, 669 };
        //Now only added direct posting
        public static bool G_SUPPLIERTYPE_ACCTPOSTING = false;
        public static string G_SHOP_END_TIME = "05:00 AM";
        public static string Def_Lpo_ExpiryDays = "15";

        public static bool Get_Defaults_Values(OracleConnection oraConn)
        {
            // DO NOT MODIFY
            try
            {
                OracleCommand cmd;
                OracleDataReader rs;
                cmd = oraConn.CreateCommand();
                string StrQuery = "select * from SYS_DEFAULTS";
                cmd.CommandText = StrQuery;
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    Def_DBLOC_4Transfer = rs["DEF_LOC_CODE"].ToString();
                    Def_Base_LOC = Def_DBLOC_4Transfer;
                    // by default its physical location 
                    // in case if its not individualLoc & login location=its management location then Login Location
                    // ex:- login to SAS from SHM then SAS, but login to SAS from W36=w36

                    Def_LOC = Def_DBLOC_4Transfer;
                    // LOGIN LOCATION
                    Def_YEAR = rs["DEF_YEAR"].ToString();
                    Def_Currency = rs["DEF_CURRENCY"].ToString();
                    Def_Main_Loc = rs["DEF_MAIN_LOC"].ToString();
                    // The central location (Only One)
                    Def_Loc_Type = rs["DEF_LOC_TYPE"].ToString();
                    Def_Loc_Group = rs["DEF_LOC_GRP"].ToString();
                    Def_Acct_Purch = rs["DEF_ACCT_PURCHASE"].ToString();
                    Def_Acct_Purch_Ret = rs["DEF_ACCT_PURCHASE_RET"].ToString();
                    Def_Acct_Sale = rs["DEF_ACCT_SALE"].ToString();
                    Def_Acct_SundryCr = rs["DEF_ACCT_SUNDRYCR"].ToString();
                    Def_Acct_SundryDr = rs["DEF_ACCT_SUNDRYDR"].ToString();
                    Def_Acct_Cash = rs["DEF_ACCT_CASH"].ToString();
                    Def_Acct_Cs_Purchase = rs["DEF_ACCT_CS_PURCHASE"].ToString();
                    Def_Acct_Cr_Purchase = rs["DEF_ACCT_CR_PURCHASE"].ToString();
                    Def_Acct_Cs_PurchaseRet = rs["DEF_ACCT_CS_PURCHASERET"].ToString();
                    Def_Acct_Cr_PurchaseRet = rs["DEF_ACCT_CR_PURCHASERET"].ToString();
                    Def_Acct_Cs_Sale = rs["DEF_ACCT_CS_SALE"].ToString();
                    Def_Acct_Cr_Sale = rs["DEF_ACCT_CR_SALE"].ToString();
                    Def_Acct_Bank = rs["DEF_ACCT_BANK"].ToString();
                    Def_Acct_Sup_DebitNote = rs["DEF_ACCT_SUP_DEBITNOTE"].ToString();
                    Def_SupplierInt = rs["DEF_SUPPLIERINT"].ToString();
                    Def_VegSupplier = rs["DEF_VEGSUPPLIER"].ToString();
                    Def_BDL_Supplier = rs["DEF_BDL_SUPPLIER"].ToString();
                    Def_BDL_SupplierName = rs["DEF_BDL_SUPPLIERNAME"].ToString();
                    Def_BDL_CategoryCode = rs["DEF_BDL_CATEGORYCODE"].ToString();
                    Def_BDL_CategoryName = rs["DEF_BDL_CATEGORYNAME"].ToString();
                    Def_BDL_BrandCode = rs["DEF_BDL_BRANDCODE"].ToString();
                    Def_BDL_BrandName = rs["DEF_BDL_BRANDNAME"].ToString();
                    Def_BDL_DivisionCod = rs["DEF_BDL_DIVISIONCODE"].ToString();
                    Def_BDL_DivisionName = rs["DEF_BDL_DIVISIONNAME"].ToString();
                    Def_BDL_Pur_DivisionCod = rs["DEF_BDL_PUR_DIVISIONCODE"].ToString();
                    Def_BDL_Pur_DivisionName = rs["DEF_BDL_PUR_DIVISIONNAME"].ToString();
                    Def_Tel_Extn = rs["DEF_TEL_EXTN"].ToString();
                    Def_POS_DB = rs["DEF_POS_DB"].ToString();
                    Def_SERVER_DB = rs["DEF_SERVER_DB"].ToString();
                    Def_OWN_DB = rs["DEF_OWN_DB"].ToString();
                    Def_POSSERVER_DB = rs["DEF_POSSERVER_DB"].ToString();
                    DEF_SMTP_CLIENT = rs["DEF_SERVICEUPDATEURL"].ToString();

                    Def_UpdateUrl = rs["DEF_MALLUPDATEURL"].ToString();

                    if (rs["DEF_MAIN_LOCATION"].ToString().Equals("Y"))
                    {
                        Def_MAIN_LOCATION = true;
                    }
                    else
                    {
                        Def_MAIN_LOCATION = false;
                    }
                    Def_Fax_No = rs["DEF_FAX_NO"].ToString();
                    Def_ApplicationVersion = rs["CURRENT_VERSION"].ToString();
                    // Def_Data_Source = Get_Default_DSN(Def_Base_LOC, oraConn);
                    Def_Country = rs["DEF_COUNTRY_CODE"].ToString();
                    if (Defaults.G_NO_MAIN_LOCATION)
                    {
                        Def_Country_MainLoc = Defaults.Def_LOC;
                    }
                    else
                    {
                        Def_Country_MainLoc = GetCountry_MainLocByCountry(Def_Country, oraConn, null);
                    }
                    if (!G_NO_CENTRAL_LOCATION)
                    {
                        Def_Central_Loc = GetCentralLocCode(oraConn, null);
                    }

                    //Def_CENTRAL_LOCATION = rs["CENTRAL_LOCATION"].ToString().Equals("Y") ? true : false;
                    if (Def_Central_Loc.Equals(Def_LOC)) //if (Def_Loc_Type.Equals("CN"))
                    {
                        Def_CENTRAL_LOCATION = true;
                    }
                    else
                    {
                        Def_CENTRAL_LOCATION = false;
                    }
                }
                else
                {
                    return false;
                }
                rs.Close();
                cmd.Dispose();
                return true;
            }
            catch (Exception EX)
            {
                DataConnector.Message("ERROR" + EX, "E", "");
                return false;
            }

        }
        //ENDS HERE
        //
        public static string Get_Default_DSN(string stLocation, OracleConnection oCon)
        {
            OracleCommand oCmd = null;
            OracleDataReader rs = null;
            try
            {
                oCmd = oCon.CreateCommand();
                string Sql = "Select DSN From SYS_DBCONNECTIONSTRINGS Where LOC_CODE='" + stLocation + "' And ACTIVE='Y'";
                oCmd.CommandText = Sql;
                rs = oCmd.ExecuteReader();
                //Sql = "LOC_CODE,LOC_NAME,BRANCH_ACCT_CODE,ACTIVE,UPDATED_LOC,LOC_TYPE,GROUP_LOC," +
                //      "MANAGEMENT_LOC,ADDRESS,STREET,FAX,POBOX,TEL,INDIVIDUAL_LOC,CURRENT_LOC";
                if (rs.HasRows)
                {
                    if (rs.Read())
                    {
                        return rs["DSN"].ToString();
                    }
                }
                throw new Exception("Error On Defaultvalues Setting");
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                if (rs != null) rs.Dispose();
                if (oCmd != null) oCmd.Dispose();
            }
        }
        public static bool Set_Default_Values(string stLocation, OracleConnection oCon)
        {
            OracleCommand oCmd = null;
            OracleDataReader rs = null;
            try
            {
                oCmd = oCon.CreateCommand();
                string Sql = "select * from COMN_LOCATION Where LOC_CODE='" + stLocation + "'";
                oCmd.CommandText = Sql;
                rs = oCmd.ExecuteReader();
                Sql = "LOC_CODE,LOC_NAME,BRANCH_ACCT_CODE,ACTIVE,UPDATED_LOC,LOC_TYPE,GROUP_LOC," +
                      "MANAGEMENT_LOC,ADDRESS,STREET,FAX,POBOX,TEL,INDIVIDUAL_LOC,CURRENT_LOC,CENTRAL_LOCATION,LOC_SERIAL";
                if (rs.HasRows)
                {
                    if (rs.Read())
                    {
                        if (rs["INDIVIDUAL_LOC"].ToString().Equals("N"))
                        {
                            if (Defaults.Def_Base_LOC.Equals(rs["MANAGEMENT_LOC"].ToString()))
                            {
                                Def_Base_LOC = Defaults.Def_LOC;
                                Def_Loc_Type = rs["LOC_TYPE"].ToString();
                            }
                            //Def_Base_Management_LOC = rs["MANAGEMENT_LOC"].ToString();
                            Def_Base_Individual_Loc = false;
                        }
                        else
                        {
                            Def_Base_Individual_Loc = true;
                        }
                        Def_Base_Management_LOC = rs["MANAGEMENT_LOC"].ToString();
                        Def_Loc_Serial = rs["LOC_SERIAL"].ToString();
                    }
                    rs.Close();
                    return true;
                }
                else
                {
                    throw new Exception("Error On Defaultvalues Setting");
                }
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                if (rs != null) rs.Dispose();
                if (oCmd != null) oCmd.Dispose();
            }
            return false;
        }
        public static bool LoadGeneralSettings(OracleConnection Conn)
        {
            string st = "Y";
            OracleCommand oCmd = null;
            OracleDataReader rs = null;
            try
            {
                //OracleDataReader rs = GetValue(con, "select * from UTIL_SETTINGS_SOFTWARE");
                oCmd = Conn.CreateCommand();
                string Sql = "Select * From UTIL_SETTINGS_SOFTWARE";
                oCmd.CommandText = Sql;
                rs = oCmd.ExecuteReader();
                if (rs.HasRows)
                {
                    while (rs.Read())
                    {
                        string c = rs["SETTING"].ToString();
                        switch (rs["SETTING"].ToString())
                        {
                            case "EMPLOYEE_ID_MUST_POS": // SCAN EMPLOYEE ID OF CASHIER EACH TIME THEY SELL ( for small outlets with multiple cashier & 1 pos )
                                ////if (rs["VALUE1"].ToString().Equals("Y")) Defaults.G_EMPLOYEE_ID_MUST_POS = true;
                                ////if (rs["VALUE1"].ToString().Equals("N")) Defaults.G_EMPLOYEE_ID_MUST_POS = false;
                                break;

                            case "POS_LOCCODE_AUTOSYNC":  // SITUATIONS WHEN MULTIPLE LOCATION CODES SHARES THE SAME SERVER, THE POS SHOULD BE MANUALLY SET ITS LOCATION IN SYS_DEFAULTS ( SAFARI MOBILE )
                                ////if (rs["VALUE1"].ToString().Equals("Y")) Defaults.G_POS_LOCCODE_AUTOSYNC = true;
                                ////if (rs["VALUE1"].ToString().Equals("N")) Defaults.G_POS_LOCCODE_AUTOSYNC = false;
                                break;
                            case "PRICE_CHANGE_IN_POS": // CASES WHEN CASHIER NEEDS TO CHANGE PRICE AT POS ( ctrl+? )
                                ////if (rs["VALUE1"].ToString().Equals("Y")) Defaults.G_PRICE_CHANGE_IN_POS = true;
                                ////if (rs["VALUE1"].ToString().Equals("N")) Defaults.G_PRICE_CHANGE_IN_POS = false;
                                break;
                            case "COUNTERET_NO_SUPERVISOR": // WHERE no cms available for return voucher generation VIA FETCH BILL option
                                ////if (rs["VALUE1"].ToString().Equals("Y")) Defaults.G_COUNTERET_NO_SUPERVISOR = true;
                                ////if (rs["VALUE1"].ToString().Equals("N")) Defaults.G_COUNTERET_NO_SUPERVISOR = false;
                                break;
                            case "WEB_SERVICE_URL": // For Club Card Now Later will add for Coupon checking and redemption.
                                ////if (rs["VALUE1"].ToString().Equals("") || rs["VALUE1"].ToString().Equals("N")) Defaults.G_WEB_SERVICE_URL = "";
                                ////else
                                ////{
                                ////Defaults.G_WEB_SERVICE_URL = rs["VALUE1"].ToString();
                                ////Defaults.G_WEB_SERVICE_PORT = int.Parse(rs["VALUE2"].ToString());

                                //Defaults.G_WEB_SERVICE_URL = "http://localhost:3513/Service.asmx";
                                //Defaults.G_WEB_SERVICE_PORT = 3513;

                                ////}
                                break;
                            case "WEB_SERVICE_URL_ALT": // For Club Card Now Later will add for Coupon checking and redemption.
                                ////if (rs["VALUE1"].ToString().Equals("") || rs["VALUE1"].ToString().Equals("N")) Defaults.G_WEB_SERVICE_URL_ALT = "";
                                ////else
                                ////{
                                ////Defaults.G_WEB_SERVICE_URL_ALT = rs["VALUE1"].ToString();
                                ////Defaults.G_WEB_SERVICE_PORT_ALT = int.Parse(rs["VALUE2"].ToString());

                                //Defaults.G_WEB_SERVICE_URL = "http://localhost:3513/Service.asmx";
                                //Defaults.G_WEB_SERVICE_PORT = 3513;

                                ////}
                                break;
                            case "WARRANTY_IN_POS": // No CMS software separate
                                ////if (rs["VALUE1"].ToString().Equals("Y")) Defaults.G_WARRANTY_IN_POS = true;
                                ////if (rs["VALUE1"].ToString().Equals("N")) Defaults.G_WARRANTY_IN_POS = false;
                                break;

                            case "NO_POS_SERVER":  // Pos server is not separate ( for small shops keep both server & pos server TNS same )
                                // if (rs["VALUE1"].ToString().Equals("Y")) Defaults.G_NO_POS_SERVER = true;
                                //if (rs["VALUE1"].ToString().Equals("N")) Defaults.G_NO_POS_SERVER = false;
                                break;
                            case "CENTRALIZED_LOC_ACCTONLY": // Are you keeping inventory also in centralised loc or just accounts ?
                                // if (rs["VALUE1"].ToString().Equals("Y")) Defaults.G_CENTRALIZED_LOC_ACCTONLY = true;
                                // if (rs["VALUE1"].ToString().Equals("N")) Defaults.G_CENTRALIZED_LOC_ACCTONLY = false;
                                break;
                            case "CENTRALIZED_LOC": // Is there any centralized location concept ?
                                //  if (rs["VALUE1"].ToString().Equals("Y")) Defaults.G_CENTRALIZED_LOC = true;
                                //  if (rs["VALUE1"].ToString().Equals("N")) Defaults.G_CENTRALIZED_LOC = false;
                                //  if (Defaults.G_CENTRALIZED_LOC) Defaults.Def_Main_loc = rs["VALUE2"].ToString();
                                break;
                            case "SUPPLIER_PURCHASE_MUST": // ONe item to buy exclussively by a particular supplier -- add check flag in invt master (exclussive supplier)

                                break;
                            case "CLUB_CARD_ACTIVE":  // Club card active or not

                                break;
                            case "BATCH_SYSTEM_ALLOWED": // Expiry system

                                break;
                            case "WMS_ACTIVE": // Warehouse management system active
                                break;


                            case "TAXABLE_LOC": // For Identifying Taxable Loc By DD 20180824
                                Def_Taxable_Loc = rs["VALUE1"].ToString().Equals("Y") ? true : false;
                                break;

                            case "SHARED_FOLDER": // For Identifying Taxable Loc By DD 20180824
                                Def_Shared_Folder = rs["VALUE1"].ToString().Equals("Y") ? true : false;
                                break;

                            case "PRHCONF_ACC_REQUIRED": // For Identifying Taxable Loc By DD 20180824
                                Def_Required_AcctConfirm_OnPRH = rs["VALUE1"].ToString().Equals("Y") ? true : false;
                                break;

                            case "BATCHENABLED_LOC": // For Identifying Batch Wise Stock Loc By DD 20190102
                                Def_BatchEnabled_Loc = rs["VALUE1"].ToString().Equals("Y") ? true : false;
                                break;

                            case "HEADER_IMAGE_PRINT": // Warehouse management system active
                                break;
                            case "FOOTER_IMAGE_PRINT": // Warehouse management system active
                                break;
                            case "SUPERVISOR_SCAN_4GIFTVCH": // Warehouse management system active
                                break;
                            case "OVER_PURCHASE_LIMIT":

                                if (rs["VALUE1"].ToString().Equals("Y"))
                                    Def_Over_PurchaseLimit = rs["VALUE2"].ToString();
                                break;
                            case "SKIP_CENTRAL_LOCATION":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_NO_CENTRAL_LOCATION = true;
                                }
                                else
                                {
                                    G_NO_CENTRAL_LOCATION = false;
                                }
                                break;
                            case "NO_SHOWROOM_LOC":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_NO_SHOWROOM_LOCATION = true;
                                }
                                else
                                {
                                    G_NO_SHOWROOM_LOCATION = false;
                                }
                                break;
                            case "SPV_DIRECT_POSTING":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_Def_SPV_DIRECT_POSTING = true;
                                }
                                else
                                {
                                    G_Def_SPV_DIRECT_POSTING = false;
                                }
                                break;
                            case "NO_MAIN_LOC":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_NO_MAIN_LOCATION = true;
                                }
                                else
                                {
                                    G_NO_MAIN_LOCATION = false;
                                }
                                break;
                            case "ITEMLOCACTIVE_REQUIRED":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_ITEMLOCACTIVE_REQUIRED = true;
                                }
                                else
                                {
                                    G_ITEMLOCACTIVE_REQUIRED = false;
                                }
                                break;
                            case "LC_CALCULATION":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_LC_CALCULATION = rs["VALUE2"].ToString().Trim();
                                    if (G_LC_CALCULATION == "" || G_LC_CALCULATION.ToString().Length <= 0)
                                    {
                                        G_LC_CALCULATION = "NORMAL";
                                    }
                                }
                                else
                                {
                                    G_LC_CALCULATION = "NORMAL";
                                }
                                break;
                            case "NONLPOPRH_NOTIFY_MAIL":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_NONLPOPRH_NOTIFY_MAIL = true;
                                }
                                else
                                {
                                    G_NONLPOPRH_NOTIFY_MAIL = false;
                                }
                                break;
                            case "NOTIFY_PREV_LC_PRH":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_NOTIFY_PREV_LC_PRH = true;
                                }
                                else
                                {
                                    G_NOTIFY_PREV_LC_PRH = false;
                                }
                                break;
                            case "BLOCK_SAMEDAY_LPOPRH":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_BLOCK_SAMEDAY_LPOPRH = true;
                                }
                                else
                                {
                                    G_BLOCK_SAMEDAY_LPOPRH = false;
                                }
                                break;
                            case "PRH_HIST_LPOITEMADD":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_PRH_HIST_LPOITEMADD = true;
                                }
                                else
                                {
                                    G_PRH_HIST_LPOITEMADD = false;
                                }
                                break;
                            case "APPR_SUBSIDIARY_LOCTYPE":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_APPR_SUBSIDIARY_LOCTYPE = rs["VALUE2"].ToString();
                                }
                                else
                                {
                                    G_APPR_SUBSIDIARY_LOCTYPE = "CN";
                                }
                                break;
                            case "APPR_ITEMLISTING_LOCTYPE":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_APPR_ITEMLISTING_LOCTYPE = rs["VALUE2"].ToString();
                                }
                                else
                                {
                                    G_APPR_ITEMLISTING_LOCTYPE = "CN";
                                }
                                break;
                            case "CREATE_BRAND_LOCTYPE":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_CREATE_BRAND_LOCTYPE = rs["VALUE2"].ToString();
                                }
                                else
                                {
                                    G_CREATE_BRAND_LOCTYPE = "CN";
                                }
                                break;
                            case "CREATE_CATEGORY_LOCTYPE":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_CREATE_CATEGORY_LOCTYPE = rs["VALUE2"].ToString();
                                }
                                else
                                {
                                    G_CREATE_CATEGORY_LOCTYPE = "CN";
                                }
                                break;
                            case "APPR_BARCODE_LOCTYPE":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_APPR_BARCODE_LOCTYPE = rs["VALUE2"].ToString();
                                }
                                else
                                {
                                    G_APPR_BARCODE_LOCTYPE = "CN";
                                }
                                break;
                            case "APPR_BDLLUG_LOCTYPE":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_APPR_BDLLUG_LOCTYPE = rs["VALUE2"].ToString();
                                }
                                else
                                {
                                    G_APPR_BDLLUG_LOCTYPE = "CN";
                                }
                                break;
                            case "CREATE_PRHDIV_LOCTYPE":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_CREATE_PRHDIV_LOCTYPE = rs["VALUE2"].ToString();
                                }
                                else
                                {
                                    G_CREATE_PRHDIV_LOCTYPE = "CN";
                                }
                                break;
                            case "CREATE_SUPDIV_LOCTYPE":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_CREATE_SUPDIV_LOCTYPE = rs["VALUE2"].ToString();
                                }
                                else
                                {
                                    G_CREATE_SUPDIV_LOCTYPE = "CN";
                                }
                                break;
                            case "BUYING_CHANGE_LOCTYPE":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_BUYING_CHANGE_LOCTYPE = rs["VALUE2"].ToString();
                                }
                                else
                                {
                                    G_BUYING_CHANGE_LOCTYPE = "MN";
                                }
                                break;
                            case "CREATE_BDLREQ_LOCTYPE":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_CREATE_BDLREQ_LOCTYPE = rs["VALUE2"].ToString();
                                }
                                else
                                {
                                    G_CREATE_BDLREQ_LOCTYPE = "MN";
                                }
                                break;
                            case "LC_CALCULATION_ZERO":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_LCCALCULATION_OLDLCZERO = rs["VALUE2"].ToString().Trim();
                                    if (G_LCCALCULATION_OLDLCZERO == "" || G_LCCALCULATION_OLDLCZERO.ToString().Length <= 0)
                                    {
                                        G_LCCALCULATION_OLDLCZERO = "NO";
                                    }
                                }
                                else
                                {
                                    G_LCCALCULATION_OLDLCZERO = "NO";
                                }
                                break;
                            case "SUPPLIERTYPE_ACCTPOSTING":
                                if (rs["VALUE1"].ToString().Equals("Y"))
                                {
                                    G_SUPPLIERTYPE_ACCTPOSTING = true;
                                }
                                else
                                {
                                    G_SUPPLIERTYPE_ACCTPOSTING = false;
                                }
                                break;
                            case "G_SHOP_END_TIME":
                                G_SHOP_END_TIME = rs["VALUE1"].ToString().Trim();
                                break;
                            case "EXPIRYDAYS_LPO":
                                Def_Lpo_ExpiryDays = rs["VALUE1"].ToString() == "" ? "15" : rs["VALUE1"].ToString() == null ? "15" : rs["VALUE1"].ToString();
                                break;
                        }
                    }
                }
                rs.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Connection Error IN SERVER " + ex);
            }
            return false;
        }
        public static string GetCountry_MainLocByLoc(string stLocation, OracleConnection oraConn, OracleTransaction tr)
        {
            string strOracle = "";
            OracleDataReader oraReader = null;
            strOracle = "Select LOC_CODE From COMN_LOCATION Where COUNTRY_CODE =(Select COUNTRY_CODE From COMN_LOCATION Where LOC_CODE='" + stLocation + "') And  LOC_TYPE='MN'";
            OracleCommand oraCommand = new OracleCommand(strOracle, oraConn);
            if (tr != null) oraCommand.Transaction = tr;
            try
            {
                oraReader = oraCommand.ExecuteReader();
                if (oraReader.Read())
                {
                    return oraReader[0].ToString();
                }
                return "";
            }
            catch (Exception exp)
            {
                throw (exp);
            }
            finally
            {
                if (oraReader != null) oraReader.Close();
                oraCommand.Dispose();
            }
        }
        public static string GetCountry_MainLocByCountry(string stCountry, OracleConnection oraConn, OracleTransaction tr)
        {
            string strOracle = "";
            OracleDataReader oraReader = null;
            strOracle = "Select LOC_CODE From COMN_LOCATION Where COUNTRY_CODE='" + stCountry + "' And LOC_TYPE='MN'";
            OracleCommand oraCommand = new OracleCommand(strOracle, oraConn);
            if (tr != null) oraCommand.Transaction = tr;
            try
            {
                oraReader = oraCommand.ExecuteReader();

                if (oraReader.HasRows)
                {
                    if (oraReader.Read())
                    {
                        return oraReader["LOC_CODE"].ToString();
                    }
                }
                else
                {
                    throw (new Exception("COUNTRY MAIN LOCATION NOT FOUND"));
                }
                return "";
            }
            catch (Exception exp)
            {
                throw (exp);
            }
            finally
            {
                if (oraReader != null) oraReader.Close();
                oraCommand.Dispose();
            }
        }
        public static string GetCentralLocCode(OracleConnection oraConn, OracleTransaction tr)
        {
            string strOracle = "";
            OracleDataReader oraReader = null;
            strOracle = "Select LOC_CODE From COMN_LOCATION Where LOC_TYPE='CN' And ACTIVE='Y'";
            OracleCommand oraCommand = new OracleCommand(strOracle, oraConn);
            if (tr != null) oraCommand.Transaction = tr;
            try
            {
                oraReader = oraCommand.ExecuteReader();
                if (oraReader.HasRows)
                {
                    if (oraReader.Read())
                    {
                        return oraReader["LOC_CODE"].ToString();
                    }
                }
                else
                {
                    throw (new Exception("CENTRAL LOCATION NOT FOUND"));
                }
                return "";
            }
            catch (Exception exp)
            {
                throw (exp);
            }
            finally
            {
                if (oraReader != null) oraReader.Close();
                oraCommand.Dispose();
            }
        }
        public static bool GetTaxableLoc_ByCountry(string stCountry, OracleConnection oraConn, OracleTransaction tr)
        {
            decimal decTaxPercent = 0;
            return GetTaxableLoc_ByCountry(stCountry, out  decTaxPercent, oraConn, tr);
        }
        public static bool GetTaxableLoc_ByCountry(string stCountry, out decimal decTaxPercent, OracleConnection oraConn, OracleTransaction tr)
        {
            string strOracle = "";
            OracleDataReader oraReader = null;
            decTaxPercent = 0;
            strOracle = "Select TAXABLE,DEF_TAXVALUE From COMN_COUNTRYMASTER Where COUNTRY_CODE='" + stCountry + "'";
            OracleCommand oraCommand = new OracleCommand(strOracle, oraConn);
            if (tr != null) oraCommand.Transaction = tr;
            try
            {
                oraReader = oraCommand.ExecuteReader();
                if (oraReader.Read())
                {
                    decTaxPercent = decimal.Parse(oraReader["DEF_TAXVALUE"].ToString());
                    return (oraReader["TAXABLE"].ToString().Equals("Y") ? true : false);
                }
                return false;
            }
            catch (Exception exp)
            {
                throw (exp);
            }
            finally
            {
                if (oraReader != null) oraReader.Close();
                oraCommand.Dispose();
            }
        }
        public static string GetTaxableLoc_ByLoc(string stCountry, OracleConnection oraConn, OracleTransaction tr)
        {
            string strOracle = "";
            OracleDataReader oraReader = null;
            strOracle = "Select LOC_CODE From COMN_LOCATION Where COUNTRY_CODE='" + stCountry + "' And LOC_TYPE='MN'";
            OracleCommand oraCommand = new OracleCommand(strOracle, oraConn);
            if (tr != null) oraCommand.Transaction = tr;
            try
            {
                oraReader = oraCommand.ExecuteReader();
                if (oraReader.Read())
                {
                    return oraReader[0].ToString();
                }
                return "";
            }
            catch (Exception exp)
            {
                throw (exp);
            }
            finally
            {
                if (oraReader != null) oraReader.Close();
                oraCommand.Dispose();
            }
        }
    }

    public class DataConnector
    {
        // Now not using..............
        //private String strServer;
        private String strUser;
        private String strPass;
        private String strDatabase;
        private String strUser1;
        private String strPass1;
        private String strDatabase1;
        public static EmailLibrary.EmailLibrary _emailLibrary;

        //public DataConnector()
        //{
        //    setDatabaseProperties();
        //}

        public struct DataBaseConAttributes
        {
            public string DatabaseName;
            public string ODBC;
            public string USER;
            public string PWD;

            public DataBaseConAttributes(string MACHINE)
            {
                string ds = "";
                MACHINE = MACHINE.ToUpper();

                ds = "MALL";
                if (Defaults.Def_Data_Source.Trim().Equals(""))
                    ds = "MALL";
                else
                    ds = Defaults.Def_Data_Source;


                if (MACHINE == Defaults.Def_POSSERVER_DB)
                {
                    ODBC = "POSSERVER";
                    DatabaseName = "POSSERVER";
                    USER = "mall";
                    PWD = "mall";
                }
                else if (MACHINE == "REPORTSERVER")
                {
                    ODBC = "REPORTSERVER";
                    DatabaseName = "REPORTSERVER";
                    USER = "mall";
                    PWD = "mall";
                }
                else if (MACHINE == Defaults.Def_OWN_DB)
                {
                    //ODBC = "TTMALL";                     
                    //ODBC = "MALLSSC";
                    //ODBC = "MALL36";
                    ODBC = ds;
                    DatabaseName = "MALL";
                    USER = "mall";
                    PWD = "mall";
                }
                else
                {
                    Exception ex = new Exception("Database Not Found");
                    throw (ex);
                }
            }
        }

        public OracleConnection getPooledConnection(string MACHINE)
        {
            string ds = "";
            string ip = "";
            if (MACHINE == "POSSERVER")
            {
                ds = "POSSERVER";
                //if (Defaults.Def_Data_Source.Trim().Equals(""))
                //    ds = "POSSERVER";
                //else
                //    ds = Defaults.Def_Data_Source;
                strUser = "mall";
                strPass = "mall";
                // ip = "192.168.0.4";
            }
            else if (MACHINE == Defaults.Def_OWN_DB)
            {
                //ds = "TTMALL";// 
                //ds = "MALL36";
                //ds = "MALLSSC";
                ds = "MALL";
                if (Defaults.Def_Data_Source.Trim().Equals(""))
                    ds = "MALL";
                else
                    ds = Defaults.Def_Data_Source;
                strUser = "mall";
                strPass = "mall";
                ip = "192.168.0.4";
            }
            //else if (MACHINE == "SERVER")
            //{
            //    ds = "SERVER";
            //    strUser = "mall";
            //    strPass = "mall";
            //    ip = "192.168.1.10";
            //}
            else
            {
                Exception ex = new Exception("Database Not Found");
                throw (ex);
            }
            //  OracleConnection oraConn;
            //if (Defaults.Def_Conn == null || Defaults.Def_Conn.State == ConnectionState.Closed)
            //{
            //    String strConnString = "User Id=" + strUser.Trim() + ";Password=" + strPass.Trim() + ";Data Source=" + ds.Trim() + ";";
            //    try
            //    {
            //        Defaults.Def_Conn = new OracleConnection();
            //        Defaults.Def_Conn.ConnectionString = strConnString.Trim();
            //        Defaults.Def_Conn.Open();
            //        ////////;
            //        ////////;
            //        return (Defaults.Def_Conn);
            //    }

            //    catch (Exception Exp)
            //    {
            //        // System.Console.Write(Exp.ToString());
            //        //MessageBox.Show(Exp.ToString());
            //        throw;
            //    }
            //}
            //else if (Defaults.Def_Conn.State == ConnectionState.Closed)
            //    Defaults.Def_Conn.Open();
            //return Defaults.Def_Conn;
            if (Defaults.Def_Conn == null || Defaults.Def_Conn.State == ConnectionState.Closed)
            {
                String strConnString = "User Id=" + strUser.Trim() + ";Password=" + strPass.Trim() + ";Data Source=" + ds.Trim() + ";";
                try
                {
                    Defaults.Def_Conn = new OracleConnection();
                    Defaults.Def_Conn.ConnectionString = strConnString.Trim();
                    Defaults.Def_Conn.Open();
                    ////////;
                    ////////;
                    return (Defaults.Def_Conn);
                }

                catch (Exception Exp)
                {
                    // System.Console.Write(Exp.ToString());
                    //MessageBox.Show(Exp.ToString());
                    throw;
                }
            }
            else if (Defaults.Def_Conn.State == ConnectionState.Closed)
                Defaults.Def_Conn.Open();
            else
            {
                bool Con = false;
                try
                {
                    Con = Exists("select sysdate from dual", Defaults.Def_Conn, null);
                }
                catch { }
                if (Con) return Defaults.Def_Conn;
                else
                {
                    try { Defaults.Def_Conn.Close(); System.Threading.Thread.Sleep(10); }
                    catch { }
                    Defaults.Def_Conn.Open();
                }
            }
            return Defaults.Def_Conn;

        }
        public OracleConnection getPOSServerConnection(string MACHINE)
        {
            string ds = "";
            if (MACHINE == "POSSERVER")
            {
                ds = "POSSERVER";
                strUser = "mall";
                strPass = "mall";
            }
            //else if (MACHINE == "MALL")
            //{
            //    ds = "MALL";
            //    strUser = "mall";
            //    strPass = "mall";
            //}   
            else if (MACHINE == Defaults.Def_POSSERVER_DB)
            {
                ds = Defaults.Def_POSSERVER_DB;
                strUser = "mall";
                strPass = "mall";
            }
            else
            {
                throw (new Exception("Database Not Found"));
            }
            OracleConnection oraConn;
            String strConnString = "User Id=" + strUser.Trim() + ";Password=" + strPass.Trim() + ";Data Source= " + ds + ";" +
                  "Min Pool Size=10;Connection Lifetime=120;";
            try
            {
                oraConn = new OracleConnection();
                oraConn.ConnectionString = strConnString.Trim();
                oraConn.Open();
                oraConn.Close();
                return (oraConn);
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            return null;
        }

        public OracleConnection getReportServerConnection(string MACHINE)
        {
            string ds = "";
            if (MACHINE == "REPORTSERVER")
            {
                ds = "REPORTSERVER";
                strUser = "mall";
                strPass = "mall";
            }
            else
            {
                throw (new Exception("Database Not Found"));
            }
            OracleConnection oraConn;
            String strConnString = "User Id=" + strUser.Trim() + ";Password=" + strPass.Trim() + ";Data Source= " + ds + ";" +
                  "Min Pool Size=10;Connection Lifetime=120;";
            try
            {
                oraConn = new OracleConnection();
                oraConn.ConnectionString = strConnString.Trim();
                oraConn.Open();
                oraConn.Close();
                return (oraConn);
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            return null;
        }

        public string getPooledConnection(string MACHINE, string LocCode, OracleConnection con, OracleTransaction tr, out string Remote)
        {
            string ds = "";
            Remote = "";
            if (LocCode == Defaults.Def_Base_LOC)
            {
                //;
                return "";
            }
            string loc = GetManagedLocCode(LocCode, con, tr);
            if (loc == Defaults.Def_Base_LOC)
            {
                //;
                return "";
            }
            string DatabaseName = "";
            string ODBC = "";
            string USER = "";
            string PWD = "";
            Remote = "";
            if (GetConnectionAttributes(MACHINE, loc, con, tr, out DatabaseName, out ODBC, out USER, out PWD, out Remote) == "")
            {
                throw new Exception("No Database found");
            }
            return Remote;
        }

        public bool closeConnection(OracleConnection oraConn)
        {
            bool blnRetVal = false;
            try
            {
                if (oraConn.State.Equals("Open"))
                {
                    ;
                }
                ;
                ;
                blnRetVal = true;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return (blnRetVal);
        }

        public static void OnEmailException(object sender, EmailEventArgs emailEventArgs)
        {
            MessageBox.Show("Error in sending email", emailEventArgs.Details);
        }

        public static void OnEmailSent(object sender, EmailEventArgs emailEventArgs)
        {
            MessageBox.Show("Email sent successfully", "Email sent successfully");
        }

        public static void OnEmailSending(object sender, EmailEventArgs emailEventArgs)
        {
        }

        public void LoadGoogleApiMailSettings()
        {
            _emailLibrary = new EmailLibrary.EmailLibrary("", "");
            _emailLibrary.OnEmailSending += OnEmailSending;
            _emailLibrary.OnEmailSent += OnEmailSent;
            _emailLibrary.OnEmailException += OnEmailException;
        }

        public string GetConnectionAttributes(string MACHINE, string loc, OracleConnection con, OracleTransaction tr, out string DatabaseName, out string ODBC, out string USER, out string PWD, out string REMOTE)
        {
            DatabaseName = "";
            ODBC = "";
            USER = "";
            REMOTE = "";
            PWD = "";
            OracleCommand cmd = con.CreateCommand();
            if (tr != null) cmd.Transaction = tr;
            cmd.CommandText = "select * from sys_dbconnectionstrings where machine='" + MACHINE + "' and loc_code='" + loc + "'";
            OracleDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                DatabaseName = rs["database"].ToString();
                ODBC = rs["DSN"].ToString();
                USER = rs["USR"].ToString();
                PWD = rs["PWD"].ToString();
                REMOTE = rs["LINK_NAME"].ToString();
            }
            rs.Close();
            cmd.Dispose();
            return ODBC;
        }

        public string GetManagedLocCode(string Loc_Code, OracleConnection con, OracleTransaction tr)
        {
            string sql = "select management_loc from comn_location where loc_code='" + Loc_Code + "'";
            string Loc = GetValue(sql, con, tr);
            if (Loc == "") throw new Exception("No Management Location Set or Location Invalid.");
            return Loc;
        }

        public bool insertTable(string strTablename, string strFields, string strValues, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            bool bReturn = false;
            string strOracle;
            OracleCommand oraCommand = null;
            strOracle = "INSERT INTO " + strTablename.Trim() + "(" + strFields.Trim() + ") VALUES (" + strValues + ")";
            try
            {
                oraCommand = new OracleCommand(strOracle, oraConn);
                oraCommand.Transaction = oraTrns;
                bReturn = Convert.ToBoolean(oraCommand.ExecuteNonQuery());
            }
            catch (Exception exp)
            {
                // throw exp;
                if (exp.Message == "ORA-00936: missing expression\n")
                {
                    MessageBox.Show("Pls Fill All Fields Carefully.....");
                }
                else if (exp.Message == "ORA-00984: column not allowed here\n")
                {
                }
                throw;
            }
            finally
            {
                oraCommand.Dispose();
            }
            return (bReturn);
        }
        public bool InsertFileUpload(string strTablename, string strFields, string strValues, string srcFile, string desFile, string folder_name, string file_name, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            string _path = "";
            try
            {
                string des_path = "//" + desFile + "//" + folder_name + "//BDA//" + file_name;
                File.Copy(srcFile, des_path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("File Uploading Failed.....");
                return false;
            }
            bool bReturn = false;
            string strOracle;
            OracleCommand oraCommand = null;
            strOracle = "INSERT INTO " + strTablename.Trim() + "(" + strFields.Trim() + ") VALUES (" + strValues + ")";
            try
            {
                oraCommand = new OracleCommand(strOracle, oraConn);
                oraCommand.Transaction = oraTrns;
                bReturn = Convert.ToBoolean(oraCommand.ExecuteNonQuery());
            }
            catch (Exception exp)
            {
                // throw exp;
                if (exp.Message == "ORA-00936: missing expression\n")
                {
                    MessageBox.Show("Pls Fill All Fields Carefully.....");
                }
                else if (exp.Message == "ORA-00984: column not allowed here\n")
                {
                }
                throw;
            }
            finally
            {
                oraCommand.Dispose();
            }
            return (bReturn);
        }

        public string GetSubsidiaryTypeByCountry(string stSubsidiary, string stCountryCode, OracleConnection con, OracleTransaction tr)
        {   //SUBSIDIARY_TYPE_CODE,SUBSIDIARY_TYPE_NAME
            string stSubsidiaryTypeCode = "";
            string stSubsidiaryTypeName = "";
            GetSubsidiaryTypeByCountry(stSubsidiary, stCountryCode, out stSubsidiaryTypeCode, out stSubsidiaryTypeName, con, tr);
            return stSubsidiaryTypeCode;
        }

        public bool GetSubsidiaryTypeByCountry(string stSubsidiary, string stCountryCode, out string stSubsidiaryTypeCode, out string stSubsidiaryTypeName, OracleConnection con, OracleTransaction tr)
        {
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            string Sql = "";
            try
            {
                Sql = "Select SUBSIDIARY_TYPE_CODE,SUBSIDIARY_TYPE_NAME From COMN_SUBSIDIARYMASTER Where SUBSIDIARY_CODE='" + stSubsidiary + "'";
                Sql = "Select A.SUBSIDIARY_TYPE_CODE,B.SUBSIDIARY_TYPE_NAME From COMN_SUBSIDIARYMASTER A,COMN_SUBSIDIARYTYPES B" +
                      " Where  A.SUBSIDIARY_TYPE_CODE=B.SUBSIDIARY_TYPE_CODE And SUBSIDIARY_CODE='" + stSubsidiary + "' And A.COUNTRY_CODE = '" + stCountryCode + "' ";
                cmd = con.CreateCommand();
                cmd.CommandText = Sql;
                if (tr != null) cmd.Transaction = tr;
                rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    if (rs.Read())
                    {
                        stSubsidiaryTypeCode = rs["SUBSIDIARY_TYPE_CODE"].ToString();
                        stSubsidiaryTypeName = rs["SUBSIDIARY_TYPE_NAME"].ToString();
                        return true;
                    }
                }
                throw (new Exception("Subsidiary Types Not Get for this supplier [ " + stSubsidiary + " ]"));
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                if (rs != null) rs.Dispose();
                if (cmd != null) cmd.Dispose();
            }
        }


        public string GetSubsidiaryType(string stSubsidiary, OracleConnection con, OracleTransaction tr)
        {   //SUBSIDIARY_TYPE_CODE,SUBSIDIARY_TYPE_NAME
            string stSubsidiaryTypeCode = "";
            string stSubsidiaryTypeName = "";
            GetSubsidiaryType(stSubsidiary, out stSubsidiaryTypeCode, out stSubsidiaryTypeName, con, tr);
            return stSubsidiaryTypeCode;
        }
        public bool GetSubsidiaryType(string stSubsidiary, out string stSubsidiaryTypeCode, out string stSubsidiaryTypeName, OracleConnection con, OracleTransaction tr)
        {
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            string Sql = "";
            try
            {
                Sql = "Select SUBSIDIARY_TYPE_CODE,SUBSIDIARY_TYPE_NAME From COMN_SUBSIDIARYMASTER Where SUBSIDIARY_CODE='" + stSubsidiary + "'";
                Sql = "Select A.SUBSIDIARY_TYPE_CODE,B.SUBSIDIARY_TYPE_NAME From COMN_SUBSIDIARYMASTER A,COMN_SUBSIDIARYTYPES B" +
                      " Where  A.SUBSIDIARY_TYPE_CODE=B.SUBSIDIARY_TYPE_CODE And SUBSIDIARY_CODE='" + stSubsidiary + "'";
                cmd = con.CreateCommand();
                cmd.CommandText = Sql;
                if (tr != null) cmd.Transaction = tr;
                rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    if (rs.Read())
                    {
                        stSubsidiaryTypeCode = rs["SUBSIDIARY_TYPE_CODE"].ToString();
                        stSubsidiaryTypeName = rs["SUBSIDIARY_TYPE_NAME"].ToString();
                        return true;
                    }
                }
                throw (new Exception("Subsidiary Types Not Get for this supplier [ " + stSubsidiary + " ]"));
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                if (rs != null) rs.Dispose();
                if (cmd != null) cmd.Dispose();
            }
        }
        public bool GetReqSubsidiaryType(string stSubsidiary, out string stSubsidiaryTypeCode, out string stSubsidiaryTypeName, OracleConnection con, OracleTransaction tr)
        {
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            string Sql = "";
            try
            {
                Sql = "Select A.SUBSIDIARY_TYPE_CODE,B.SUBSIDIARY_TYPE_NAME From COMN_LEDGERCREATION_REQ A,COMN_SUBSIDIARYTYPES B" +
                      " Where  A.SUBSIDIARY_TYPE_CODE=B.SUBSIDIARY_TYPE_CODE And SUBSIDIARY_CODE='" + stSubsidiary + "'";
                cmd = con.CreateCommand();
                cmd.CommandText = Sql;
                if (tr != null) cmd.Transaction = tr;
                rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    if (rs.Read())
                    {
                        stSubsidiaryTypeCode = rs["SUBSIDIARY_TYPE_CODE"].ToString();
                        stSubsidiaryTypeName = rs["SUBSIDIARY_TYPE_NAME"].ToString();
                        return true;
                    }
                }
                throw (new Exception("Subsidiary Types Not Get for this supplier [ " + stSubsidiary + " ]"));
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                if (rs != null) rs.Dispose();
                if (cmd != null) cmd.Dispose();
            }
        }
        public decimal GetBuying(string PartNumber_Sup, string Packing, OracleConnection con, OracleTransaction Tr)
        {
            string Val = GetValue("INVT_itempacking", "Buying_Rate", "Part_number_sup='" + PartNumber_Sup + "' and " + " packing='" + Packing + "'", "PACKING_ORDER", con, Tr);
            decimal Lc = decimal.Parse(Val == "" ? "0" : Val);
            Lc = decimal.Parse(Val == "" ? "0" : Val);
            return Lc;
        }

        public decimal GetBuying(string PartNumber_Sup, int ZeroSerial, OracleConnection con, OracleTransaction Tr)
        {
            string Val = GetValue("INVT_itempacking", "Buying_Rate", "Part_number_sup='" + PartNumber_Sup + "' and " + " zero_serial=" + ZeroSerial, "PACKING_ORDER", con, Tr);
            decimal Lc = decimal.Parse(Val == "" ? "0" : Val);
            return Lc;
        }

        public decimal GetLc(string PartNumber_Sup, int ZeroSerial, OracleConnection con, OracleTransaction Tr)
        {
            string Val = GetValue("INVT_ITEMTRAN_DTLS", "LC", "Part_number_sup='" + PartNumber_Sup + "' and " + " zero_serial=" + ZeroSerial, "", con, Tr);
            decimal Lc = decimal.Parse(Val == "" ? "0" : Val);
            if (Lc == 0)
            {
                Val = GetValue("INVT_itempacking", "buying_rate", "Part_number_sup='" + PartNumber_Sup + "' and " + " zero_serial=" + ZeroSerial, "", con, Tr);
                Lc = decimal.Parse(Val == "" ? "0" : Val);
            }
            return Lc;
        }

        public decimal GetAvgCost(string Part_Number_Sup, int ZeroSerial, DateTime dtTo, OracleConnection con, OracleTransaction tr)
        {
            DateTime dtf = dtTo.AddYears(-1);
            string Sql = "select round(sum(prh_value)/sum(prh_qty),3) from invt_inventorybalance_yr where doc_date between '" + dtf + "' and '" + dtTo + "' and part_number_sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial;
            string Val1 = GetValue(Sql, con, tr);
            decimal val2 = 0;
            decimal val1 = decimal.Parse(Val1 == "" ? "0" : Val1);
            if (dtTo.Month >= DateTime.Today.Month && dtTo.Year == DateTime.Today.Year || val1 == 0)
            {
                Sql = "select round(sum(prh_value)/sum(prh_qty),3) from invt_inventorybalance where part_number_sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial;
                string Val2 = GetValue(Sql, con, tr);
                val2 = decimal.Parse(Val2 == "" ? "0" : Val2); ;
            }
            if (val1 == 0)
            {
                if (val2 == 0)
                {
                    val2 = GetLc(Part_Number_Sup, ZeroSerial, con, tr);
                }
                return val2;
            }
            return val2 + val1 / 11;
        }

        public static bool get_blockedStatus(string doctype, string loccode, string packing, string partnumber, OracleConnection oraConn)
        {
            OracleCommand blkCmd = null;
            OracleDataReader blkReader = null;
            try
            {
                string sql = " select * from INVT_PRODUCT_BLOCKING where loc_code='" + loccode + "' and (BLOCK_MODE='" + doctype + "' OR BLOCK_MODE='ALL') and PART_NUMBER_SUP='" + partnumber + "' and PACKING='" + packing + "' order by BLOCK_MODE asc,ALL_PACKING_BLOCK asc,PACKING asc";
                blkCmd = new OracleCommand(sql, oraConn);
                blkReader = blkCmd.ExecuteReader();
                if (blkReader.HasRows)
                {
                    while (blkReader.Read())
                    {
                        if (blkReader["BLOCK_MODE"].ToString() == "ALL")
                            return false;
                        if (blkReader["ALL_PACKING_BLOCK"].ToString() == "Y")
                            return false;
                        if (blkReader["PACKING"].ToString() == packing)
                            return false;
                    }
                }
            }
            catch (Exception ee)
            {
                DataConnector.Message("Error in get_blockedStatus \n\r Contact your IT ...\n\r" + ee.ToString(), "E", "");
                return false;
            }
            finally
            {
                if (blkReader != null)
                {
                    blkReader.Close();
                    blkReader.Dispose();
                }
                if (blkCmd != null)
                    blkCmd.Dispose();
            }
            return true;
        }

        public bool get_ItemLocationActiveStatus(string loccode, string partnumber, OracleConnection oraConn)
        {
            if (!Defaults.G_ITEMLOCACTIVE_REQUIRED)
            {
                return true;
            }
            try
            {
                if (Exists("SELECT * FROM INVT_ITEMPACKING WHERE LOC_CODE='" + loccode + "' AND PART_NUMBER_SUP='" + partnumber + "' AND DEFAULT_PACKING='Y' AND DEFAULT_UNIT='Y' AND LOC_ACTIVE='Y'", oraConn, null))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ee)
            {
                DataConnector.Message("Error in get_ItemLocationActiveStatus \n\r Contact your IT ...\n\r" + ee.ToString(), "E", "");
                return false;
            }
        }

        public static bool is_PaymentBlocked(string Subsidiary_code, OracleConnection oraConn)
        {
            OracleCommand blkCmd = null;
            OracleDataReader blkReader = null;
            try
            {
                string sql = " select BLOCK_Payment from acct_groups where acct_code='" + Subsidiary_code + "'";
                blkCmd = new OracleCommand(sql, oraConn);
                blkReader = blkCmd.ExecuteReader();
                if (blkReader.HasRows)
                {
                    while (blkReader.Read())
                    {
                        if (blkReader[0].ToString().Equals("Y"))
                            return true;
                        else return false;
                    }
                }
            }
            catch (Exception ee)
            {
                DataConnector.Message("Error in get_blockedStatus \n\r Contact your IT ...\n\r" + ee.ToString(), "E", "");
                return false;
            }
            finally
            {
                if (blkReader != null)
                {
                    blkReader.Close();
                    blkReader.Dispose();
                }
                if (blkCmd != null)
                    blkCmd.Dispose();
            }
            return false;
        }

        public static bool is_AccountsBlocked(string Subsidiary_code, OracleConnection oraConn)
        {
            OracleCommand blkCmd = null;
            OracleDataReader blkReader = null;
            try
            {
                string sql = " select BLOCK_Accounts from comn_subsidiarymaster where subsidiary_code='" + Subsidiary_code + "'";
                blkCmd = new OracleCommand(sql, oraConn);
                blkReader = blkCmd.ExecuteReader();
                if (blkReader.HasRows)
                {
                    while (blkReader.Read())
                    {
                        if (blkReader[0].ToString().Equals("Y"))
                            return true;
                        else return false;
                    }
                }
            }
            catch (Exception ee)
            {
                DataConnector.Message("Error in get_blockedStatus \n\r Contact your IT ...\n\r" + ee.ToString(), "E", "");
                return false;
            }
            finally
            {
                if (blkReader != null)
                {
                    blkReader.Close();
                    blkReader.Dispose();
                }
                if (blkCmd != null)
                    blkCmd.Dispose();
            }
            return false;
        }

        public static bool is_ReturnBlocked(string Subsidiary_code, OracleConnection oraConn)
        {
            OracleCommand blkCmd = null;
            OracleDataReader blkReader = null;
            try
            {
                string sql = " select BLOCK_PRR from comn_subsidiarymaster where subsidiary_code='" + Subsidiary_code + "'";
                blkCmd = new OracleCommand(sql, oraConn);
                blkReader = blkCmd.ExecuteReader();
                if (blkReader.HasRows)
                {
                    while (blkReader.Read())
                    {
                        if (blkReader[0].ToString().Equals("Y"))
                            return true;
                        else return false;
                    }
                }
            }
            catch (Exception ee)
            {
                DataConnector.Message("Error in get_blockedStatus \n\r Contact your IT ...\n\r" + ee.ToString(), "E", "");
                return false;
            }
            finally
            {
                if (blkReader != null)
                {
                    blkReader.Close();
                    blkReader.Dispose();
                }
                if (blkCmd != null)
                    blkCmd.Dispose();
            }
            return false;
        }


        public static string getStockValueAllLocs(string Subsidiary_code, string Loc_Code, OracleConnection oraConn)
        {
            string SV = "", Net = ""; ;

            OracleCommand blkCmd = null;
            OracleDataReader blkReader = null;
            try
            {
                string sql = " select distinct a.loc_code, sum(LC*cb_qty) over (partition by a.loc_code) as NetSv,sum(LC*cb_qty) over() as Net from (invt_inventorybalance a inner join invt_inventorymaster b on a.part_number_sup=b.part_number_sup)inner join comn_location c on a.loc_code=c.loc_code where c.active='Y' and b.supplier_code='" + Subsidiary_code + "' ";
                blkCmd = new OracleCommand(sql, oraConn);
                blkReader = blkCmd.ExecuteReader();
                if (blkReader.HasRows)
                {
                    while (blkReader.Read())
                    {
                        SV = SV + ", " + blkReader[0].ToString() + "=" + blkReader[1].ToString();
                        Net = blkReader[1].ToString();
                    }
                    if (SV.Length > 0) SV = SV.Substring(1) + "   NET:- " + Net;
                    return SV;
                }
            }
            catch (Exception ee)
            {
                DataConnector.Message("Error in get_blockedStatus \n\r Contact your IT ...\n\r" + ee.ToString(), "E", "");
                return "";
            }
            finally
            {
                if (blkReader != null)
                {
                    blkReader.Close();
                    blkReader.Dispose();
                }
                if (blkCmd != null)
                    blkCmd.Dispose();
            }
            return "";
        }

        public static string GetMonthName(int Month)
        {
            if (Month >= 1 && Month <= 12)
            {
                string[] strMonth = { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
                return strMonth[Month];
            }
            else return "Jan";
        }
        //NEW CODE BY ABU APR 21
        public static int GetMonthNumber(string month)
        {
            switch (month)
            {
                case "Jan":
                    return 1;
                    break;
                case "Feb":
                    return 2;
                    break;
                case "Mar":
                    return 3;
                    break;
                case "Apr":
                    return 4;
                    break;
                case "May":
                    return 5;
                    break;
                case "Jun":
                    return 6;
                    break;
                case "Jul":
                    return 7;
                    break;
                case "Aug":
                    return 8;
                    break;
                case "Sep":
                    return 9;
                    break;
                case "Oct":
                    return 10;
                    break;
                case "Nov":
                    return 11;
                    break;
                case "Dec":
                    return 12;
                    break;
                default:
                    return DateTime.Today.Month;
                    break;
            }
        }
        //ENDS
        public static string NumToWord(decimal Number, bool Chq)
        {
            if (Number < 0) return "";
            string snum = Number.ToString();
            snum = string.Format("{0:0.00}", Number);
            // string Sfill=Number.Substring(Number.Length - 2, 2)
            snum = snum.PadLeft(15, '0');
            string n2w = "";
            string nFW = "";

            ConvertNum(ref n2w, snum.Substring(0, 1), "Thousand ");
            ConvertNum(ref n2w, snum.Substring(1, 3), "Billion ");
            ConvertNum(ref n2w, snum.Substring(4, 2), "Million ");
            ConvertNum(ref n2w, snum.Substring(6, 3), "Thousand ");
            ConvertNum(ref n2w, snum.Substring(9, 3), "");

            ConvertNum(ref nFW, (snum.Substring(snum.Length - 2, 2)), "");


            if (int.Parse(snum.Substring(snum.Length - 2, 2)) > 0)
            {
                if (Defaults.Def_Currency == "QRS")
                {
                    if (n2w != "") n2w = n2w + "and Dirhams " + int.Parse(snum.Substring(snum.Length - 2, 2)) + "/100.";
                }
                else if (Defaults.Def_Currency == "AED")
                {
                    //if (n2w != "") n2w = n2w + "and Fills " + int.Parse(snum.Substring(snum.Length - 2, 2)) + "/100.";
                    if (n2w != "") n2w = n2w + "and  " + nFW + " Fils.";
                }
                else
                {
                    if (n2w != "") n2w = n2w + "and Dirhams " + int.Parse(snum.Substring(snum.Length - 2, 2)) + "/100.";
                }
            }
            if (Chq)
            {
                return n2w + " Only";
            }
            else
            {
                return "(QR. " + n2w + " Only)";
            }
        }
        public static string NumToWord_Currency(decimal Number, bool Chq, string CurrencyCode)
        {
            if (Number < 0) return "";
            string snum = Number.ToString();
            snum = string.Format("{0:0.00}", Number);
            snum = snum.PadLeft(15, '0');
            string n2w = "";

            ConvertNum(ref n2w, snum.Substring(0, 1), "Thousand ");
            ConvertNum(ref n2w, snum.Substring(1, 3), "Billion ");
            ConvertNum(ref n2w, snum.Substring(4, 2), "Million ");
            ConvertNum(ref n2w, snum.Substring(6, 3), "Thousand ");
            ConvertNum(ref n2w, snum.Substring(9, 3), "");
            if (int.Parse(snum.Substring(snum.Length - 2, 2)) > 0)
            {
                if (CurrencyCode == "QRS")
                {
                    if (n2w != "") n2w = n2w + "and Dirhams " + int.Parse(snum.Substring(snum.Length - 2, 2)) + "/100.";
                }
                else if (CurrencyCode == "AED")
                {
                    if (n2w != "") n2w = n2w + "and Fils " + int.Parse(snum.Substring(snum.Length - 2, 2)) + "/100.";
                }
                else
                {
                    if (n2w != "") n2w = n2w + "and Dirhams " + int.Parse(snum.Substring(snum.Length - 2, 2)) + "/100.";

                }

            }
            if (Chq)
            {
                return n2w + " Only";
            }
            else
            {
                return "(" + CurrencyCode + ". " + n2w + " Only)";
            }
        }
        public string GetArabicNumber(string Number)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            Decoder utfde;
            utfde = utf8.GetDecoder();
            StringBuilder stbilter = new StringBuilder();
            char[] charst = new char[2];
            byte[] byt = { 217, 16 };
            char[] scar = Number.ToCharArray();
            foreach (char s in scar)
            {
                if (char.IsDigit(s))
                {
                    byt[1] = Convert.ToByte(160 + Convert.ToInt32(Char.GetNumericValue(s)));
                    utfde.GetChars(byt, 0, 2, charst, 0);
                    stbilter.Append(charst[0]);
                }
                else
                {
                    stbilter.Append(s);
                }
            }

            return stbilter.ToString();
        }

        public string SPLITSTRING_FOR_IN_SQL(string StringV, char charsplit)
        {
            string[] st = StringV.Split(charsplit);
            if (st != null)
            {
                StringV = "";
                foreach (string st1 in st)
                {
                    StringV = StringV + ",'" + st1 + "'";
                }
                if (StringV.Length > 0) return StringV.Substring(1);
            }
            return "'" + StringV + "'";
        }


        public static bool BarcodeOuting(string Barcode, ref string Pn, ref int Zero, ref int Pk)
        {
            try
            {
                //string Pn = "";
                //int Zero = 0;
                if (Barcode.Substring(0, 2) == "SH")
                {
                    Pn = Barcode.Substring(2, Barcode.Length - 3);
                    if (Pn.Length == 4)
                    {
                        Pn = "0BDL" + Pn;
                    }
                    else
                    {
                        Pn = "0" + Pn;
                    }
                }
                else if (Barcode.Substring(0, 2) == "SD")
                {
                    Pn = Barcode.Substring(6, Barcode.Length - 7);
                }
                else if (Barcode.Substring(0, 1) == "S" && !(Barcode.Contains("-")))
                {
                    Pn = Barcode.Substring(1, Barcode.Length - 2);
                }
                else if (Barcode.Substring(0, 1) == "S" && (Barcode.Contains("-")))
                {
                    string[] temp = Barcode.Split('-');
                    Pn = temp[0].Substring(1, temp[0].Length - 1);
                    Zero = int.Parse(temp[1]);
                }
                //int Pk = 0;
                if (Pn != "" && DataConnector.IsNumeric(Barcode.Substring(Barcode.Length - 1, 1)))
                {
                    Pk = int.Parse(Barcode.Substring(Barcode.Length - 1, 1));
                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }

        public static bool SplitPartNumber_Sup(string IN_PartNumber_Sup, ref string Out_PartNumber_Sup, ref int ZeroSerial)
        {
            Out_PartNumber_Sup = IN_PartNumber_Sup;
            ZeroSerial = 0;
            if (IN_PartNumber_Sup.Contains('-'))
            {
                char[] sep = new char[1];
                sep[0] = '-';
                string[] part_number = IN_PartNumber_Sup.Split(sep);
                if (part_number[0].Length == 0) return false; ;
                Out_PartNumber_Sup = part_number[0];
                if (IsNumeric(part_number[1].ToString()))
                {
                    ZeroSerial = int.Parse(part_number[1]);
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        private static void ConvertNum(ref string N2W, string sn, string ps)
        {
            sn = sn.Trim();
            int no = 0;
            long Vn = long.Parse(sn);
            if (Vn > 0)
            {
                string[] Ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen" };
                string[] Tens = { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
                do
                {
                    if (Vn > 99)
                    {
                        no = int.Parse(sn.Substring(0, 1));
                        N2W = N2W + Ones[no] + " Hundred ";
                        Vn = Vn - no * 100;
                    }
                    else if (Vn > 19)
                    {
                        no = int.Parse(sn.Substring(0, 1));
                        if (no > 0)
                        {
                            N2W = N2W + Tens[no - 2] + " ";
                            Vn = Vn - no * 10;
                        }
                    }
                    else
                    {
                        N2W = N2W + Ones[Vn] + " ";
                        break; ;
                    }
                    sn = Vn.ToString().Trim();
                } while (Vn >= 0);
                N2W = N2W + ps;

            }

        }

        public static string GetSerialNumberFromWord(string Word, bool CheckFromBeginning)
        {
            string NewSerial = "", t = "";
            return GetSerialNumberFromWord(Word, CheckFromBeginning, 1, out NewSerial, out t);
        }
        public static string GetSerialNumberFromWord(string Word, bool CheckFromBeginning, int IncrementValue, out string NewSerial)
        {
            string t = "";
            return GetSerialNumberFromWord(Word, CheckFromBeginning, IncrementValue, out  NewSerial, out t);
        }
        public static string GetSerialNumberFromWord(string Word, bool CheckFromBeginning, out string StringPart)
        {
            string NewSerial = "";
            return GetSerialNumberFromWord(Word, CheckFromBeginning, 1, out NewSerial, out StringPart);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Word">Input String</param>
        /// <param name="CheckFromBeginning">Scan number at beginning or at end </param>
        /// <param name="IncrementValue">Value to increment or decrement</param>
        /// <param name="NewSerial">Generated Slno</param>
        /// <param name="StringPart">String Part Of the entered string</param>
        /// <returns>Returns Number Part Of the entered string</returns>
        public static string GetSerialNumberFromWord(string Word, bool CheckFromBeginning, int IncrementValue, out string NewSerial, out string StringPart)
        {
            // B for beginning E for End and A for search from both Loc
            NewSerial = "1";
            int i = 0; string ret = ""; StringPart = Word;

            if (IsNumeric(Word))
            {
                int x = Word.Length - ((long.Parse(Word) + 1).ToString()).Length;

                NewSerial = (long.Parse(Word) + 1).ToString();
                if (x <= 0) return NewSerial;

                return NewSerial.PadLeft(x, '0');
            }
            if (Word.Length == 0)
            {
                return "0";
            }

            if (CheckFromBeginning)
            {
                while (i < Word.Length)
                {

                    if (IsNumeric(Word[i].ToString()))
                    {
                        ret = ret + Word[i];
                    }
                    else
                    {
                        if (ret != "")
                        {
                            int pad1 = ret.Length;
                            ret = (long.Parse(ret) + IncrementValue).ToString().PadLeft(pad1, '0');
                            NewSerial = ret + Word.Substring(i, Word.Length - i);
                            StringPart = Word.Substring(i, Word.Length - i);
                        }
                        break;
                    }
                    i++;
                }
            }
            else
            {
                i = Word.Length - 1;
                while (i >= 0)
                {
                    if (IsNumeric(Word[i].ToString()))
                    {
                        ret = Word[i] + ret;
                    }
                    else
                    {
                        if (ret != "")
                        {
                            int pad = ret.Length;
                            ret = (long.Parse(ret) + IncrementValue).ToString().PadLeft(pad, '0');
                            NewSerial = Word.Substring(0, i + 1) + ret;
                            StringPart = Word.Substring(0, i + 1);
                        }
                        break;
                    }
                    i--;
                }
            }
            if (ret == "" && CheckFromBeginning)
            {
                NewSerial = "1" + Word;
            }
            else if (ret == "") NewSerial = Word + "1";
            return ret;
        }

        public bool UpdateChequeNumber(string BankCode, string ChequeNumber, bool UpdateSame, OracleConnection con, OracleTransaction tr)
        {

            string SlnO = "";
            // GetSerialNumberFromWord(ChequeNumber, false, 1,out  SlnO);
            int r = 0;
            updateTable("comn_chequenumbers", "chequenumber='" + ChequeNumber + "'", "bank_code='" + BankCode + "'", out r, con, tr);
            if (r <= 0)
            {
                insertTable("comn_chequenumbers", "bank_code,chequenumber", "'" + BankCode + "','" + ChequeNumber + "'", con, tr);
            }
            return true;
        }
        public bool UpdateChequeNumber(string BankCode, string ChequeNumber, OracleConnection con, OracleTransaction tr)
        {

            string SlnO = "";
            GetSerialNumberFromWord(ChequeNumber, false, 1, out  SlnO);
            int r = 0;
            updateTable("comn_chequenumbers", "chequenumber='" + SlnO + "'", "bank_code='" + BankCode + "'", out r, con, tr);
            if (r <= 0)
            {
                insertTable("comn_chequenumbers", "bank_code,chequenumber", "'" + BankCode + "','" + SlnO + "'", con, tr);
            }
            return true;
        }

        public string getChequeNumber(string Bank_Code, OracleConnection con, OracleTransaction tr)
        {
            return GetValue("comn_chequenumbers", "chequenumber", "bank_code='" + Bank_Code + "'", "", con, tr);
        }


        public bool insertTable(string strTablename, string strFields, string strValues, OracleConnection oraConn)
        {
            bool bReturn = false;
            string strOracle;
            strOracle = "INSERT INTO " + strTablename.Trim() + "(" + strFields.Trim() + ") VALUES (" + strValues + ")";
            OracleCommand oraCommand = null;
            try
            {
                oraCommand = new OracleCommand(strOracle, oraConn);
                bReturn = Convert.ToBoolean(oraCommand.ExecuteNonQuery());
            }
            catch (Exception exp)
            {
                // throw exp;
                throw;
            }
            finally { oraCommand.Dispose(); }
            return (bReturn);
        }
        //................... insert method.................

        //................... Update method.................

        public bool updateTable(string strTable, string strSetClause, string strWhere, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            bool bReturn = false;
            string strOracle;
            strOracle = "UPDATE " + strTable.Trim() + " SET " + strSetClause;
            if (strWhere.Length > 0)
            {
                strOracle = strOracle + " WHERE " + strWhere.Trim();
            }
            OracleCommand oraCommand = new OracleCommand(strOracle, oraConn);
            oraCommand.Transaction = oraTrns;
            try
            {
                long RES = oraCommand.ExecuteNonQuery();

                return true;
            }
            catch (Exception exp)
            {
                //MessageBox.Show(exp.ToString());
                throw exp;
            }
            finally
            {
                oraCommand.Dispose();
            }
            return false;
        }

        public bool updateTable(string strTable, string strSetClause, string strWhere, out int Rows_Affected, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            bool bReturn = false;
            string strOracle;
            strOracle = "UPDATE " + strTable.Trim() + " SET " + strSetClause;
            if (strWhere != null)
            {
                strOracle = strOracle + " WHERE " + strWhere.Trim();
            }
            OracleCommand oraCommand = new OracleCommand(strOracle, oraConn);
            oraCommand.Transaction = oraTrns;

            try
            {
                Rows_Affected = oraCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception exp)
            {
                Rows_Affected = -1;
                throw (exp); //MessageBox.Show(exp.ToString());
            }
            finally
            {
                if (oraCommand != null) oraCommand.Dispose();
            }
            return false;
        }

        public bool GetAcctOpening(DateTime dt, string Loc_Code, string acct_code, out decimal DR, out decimal CR, OracleConnection con, OracleTransaction tr)
        {
            OracleCommand cmd = con.CreateCommand();
            if (tr != null) cmd.Transaction = tr;
            cmd.CommandText = "select * from acct_ledger where loc_code='" + Loc_Code + "' and doc_year='" + dt.Year + "' and acct_code='" + acct_code + "'";
            OracleDataReader rs = cmd.ExecuteReader();
            bool flg = true;
            decimal Ob_Cr = 0, Ob_Dr = 0;
            if (rs.Read())
            {
                Ob_Cr = decimal.Parse(rs["ob_cr"].ToString());
                Ob_Dr = decimal.Parse(rs["ob_Dr"].ToString());
                for (int i = 1; i < dt.Month; i++)
                {
                    string ii = (i <= 9 ? "0" + i : "" + i);
                    Ob_Cr = Ob_Cr + decimal.Parse(rs["Cr_" + ii].ToString());
                    Ob_Dr = Ob_Dr + decimal.Parse(rs["Dr_" + ii].ToString());
                }
            }
            rs.Close();
            cmd.Dispose();
            if (flg)
            {

                cmd = con.CreateCommand();
                cmd.CommandText = "select sum(lc_debit) as lc_debit,sum(lc_credit) as lc_credit from acct_transactions where loc_code='" + Loc_Code + "' and doc_date between '01/" + GetMonthName(dt.Month) + "/" + dt.Year + "' and '" + dt.AddDays(-1).ToString("dd/MMM/yyyy") + "' and acct_code='" + acct_code + "'";
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {

                    Ob_Dr = decimal.Parse(rs["lc_debit"].ToString() == "" ? "0" : rs["lc_debit"].ToString());
                    Ob_Cr = decimal.Parse(rs["lc_CREDit"].ToString() == "" ? "0" : rs["lc_CREDit"].ToString());
                }
                rs.Close();
                cmd.Dispose();
            }
            DR = Ob_Dr;
            CR = Ob_Cr;
            return true;
        }

        public string GetDocSerial(string MACHINE, string Loc_Code, string Doc_Type, string Doc_Year, OracleConnection con, OracleTransaction tr, out string Remote)
        {

            string ORemote = GetValue("select link_name from sys_dbconnectionstrings where loc_code='" + Defaults.Def_Base_LOC + "' and machine='SERVER'", con, tr);
            Remote = "";
            string Remote_Loc = "";
            getPooledConnection(MACHINE, Loc_Code, con, tr, out Remote);
            if (Remote == ORemote || Remote == "")
            {
                Remote = "";
            }
            else Remote_Loc = "@" + Remote;

            OracleCommand cmd1 = con.CreateCommand();
            // UpdateDocSerial(Loc_Code, Doc_Type, Defaults.Def_YEAR, Remote_Loc, con, tr);
            cmd1.CommandText = "select trim(doc_prefix)||doc_serial from comn_documentserials" + Remote_Loc + " where loc_code='" + Loc_Code + "' and doc_type='" + Doc_Type + "' and PROC_YEAR='" + Doc_Year + "'";
            if (tr != null)
                cmd1.Transaction = tr;
            OracleDataReader rs = cmd1.ExecuteReader();
            try
            {
                if (rs.Read())
                {
                    string YR = "";
                    YR = Doc_Year.Substring(2, 2);
                    //if (Remote_Loc == "")
                    //{
                    return Loc_Code + YR + Doc_Type + rs.GetString(0).PadLeft(6, '0');
                    //}
                    //else
                    //{
                    //    return Loc_Code + YR + Doc_Type + "A" + rs.GetString(0).PadLeft(5, '0');
                    //}
                }
            }
            catch { throw; }
            finally { if (rs != null)rs.Close(); cmd1.Dispose(); }

            Exception ex = new Exception("No Document Serial Found For The Type " + Doc_Type + " IN " + Loc_Code);
            throw (ex);
        }

        public string GetDocSerial(string Loc_Code, string Doc_Type, string Doc_Year, OracleConnection con, OracleTransaction tr)
        {

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select trim(doc_prefix)||doc_serial from comn_documentserials where loc_code='" + Loc_Code + "' and doc_type='" + Doc_Type + "' and PROC_YEAR='" + Doc_Year + "'";
            if (tr != null)
                cmd.Transaction = tr;
            OracleDataReader rs = cmd.ExecuteReader();
            try
            {
                if (rs.Read())
                {
                    string YR = "";
                    YR = Doc_Year.Substring(2, 2);
                    return Loc_Code + YR + Doc_Type + rs.GetString(0).PadLeft(6, '0');
                }
            }
            catch { throw; }
            finally { if (rs != null) rs.Close(); cmd.Dispose(); }
            Exception ex = new Exception("No Document Serial Found For The Type " + Doc_Type + " IN " + Loc_Code);
            throw (ex);
        }

        public string GetDocSerialUpdated(string Loc_Code, string Doc_Type, string Doc_Year, OracleConnection con, OracleTransaction tr)
        {
            UpdateDocSerial(Loc_Code, Doc_Type, Defaults.Def_YEAR, con, tr);
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select trim(doc_prefix)||(doc_serial-1) from comn_documentserials where loc_code='" + Loc_Code + "' and doc_type='" + Doc_Type + "' and PROC_YEAR='" + Doc_Year + "'";
            if (tr != null)
                cmd.Transaction = tr;
            OracleDataReader rs = cmd.ExecuteReader();
            try
            {
                if (rs.Read())
                {
                    string YR = "";
                    YR = Doc_Year.Substring(2, 2);
                    return Loc_Code + YR + Doc_Type + rs.GetString(0).PadLeft(6, '0');
                }
            }
            catch { throw; }
            finally { if (rs != null) rs.Close(); cmd.Dispose(); }
            Exception ex = new Exception("No Document Serial Found For The Type " + Doc_Type + " IN " + Loc_Code);
            throw (ex);
        }
        public string GetDocSerialUpdated(string MACHINE, string Loc_Code, string Doc_Type, string Doc_Year, OracleConnection con, OracleTransaction tr, out string Remote)
        {
            string ORemote = GetValue("select link_name from sys_dbconnectionstrings where loc_code='" + Defaults.Def_Base_LOC + "' and machine='SERVER'", con, tr);
            Remote = "";
            string Remote_Loc = "";
            getPooledConnection(MACHINE, Loc_Code, con, tr, out Remote);
            if (Remote == ORemote || Remote == "")
            {
                Remote = "";
            }
            else
            {
                Remote_Loc = "@" + Remote;
            }
            OracleCommand cmd1 = con.CreateCommand();
            UpdateDocSerial(Loc_Code, Doc_Type, Defaults.Def_YEAR, Remote, con, tr);
            cmd1.CommandText = "select trim(doc_prefix)||(doc_serial-1) from comn_documentserials" + Remote_Loc + " where loc_code='" + Loc_Code + "' and doc_type='" + Doc_Type + "' and PROC_YEAR='" + Doc_Year + "'";
            if (tr != null)
                cmd1.Transaction = tr;
            OracleDataReader rs = cmd1.ExecuteReader();
            try
            {
                if (rs.Read())
                {
                    string YR = "";
                    YR = Doc_Year.Substring(2, 2);
                    return Loc_Code + YR + Doc_Type + rs.GetString(0).PadLeft(6, '0');
                }
            }
            catch { throw; }
            finally { if (rs != null)rs.Close(); cmd1.Dispose(); }

            Exception ex = new Exception("No Document Serial Found For The Type " + Doc_Type + " IN " + Loc_Code);
            throw (ex);
        }
        public string GetDocSerialUpdated(string Loc_Code, string Doc_Type, string Doc_Year, string Remote, OracleConnection con, OracleTransaction tr)
        {
            OracleCommand cmd = con.CreateCommand();
            UpdateDocSerial(Loc_Code, Doc_Type, Defaults.Def_YEAR, Remote, con, tr);

            //cmd.CommandText = "select trim(doc_prefix)||(doc_serial-1) from comn_documentserials@" + Remote + " where loc_code='" + Loc_Code + "' and doc_type='" + Doc_Type + "' and PROC_YEAR='" + Doc_Year + "'";

            // dd on 27/Nov/2010 12:26 PM
            string Remote_Loc = "";
            if (Remote != "") Remote_Loc = "@" + Remote;
            cmd.CommandText = "select trim(doc_prefix)||(doc_serial-1) from comn_documentserials" + Remote_Loc + " where loc_code='" + Loc_Code + "' and doc_type='" + Doc_Type + "' and PROC_YEAR='" + Doc_Year + "'";

            // dd on 27/Nov/2010 12:26 PM
            if (tr != null)
                cmd.Transaction = tr;
            OracleDataReader rs = cmd.ExecuteReader();
            try
            {
                if (rs.Read())
                {
                    string YR = "";
                    YR = Doc_Year.Substring(2, 2);
                    return Loc_Code + YR + Doc_Type + rs.GetString(0).PadLeft(6, '0');
                }
            }
            catch { throw; }
            finally { if (rs != null)rs.Close(); cmd.Dispose(); }

            Exception ex = new Exception("No Document Serial Found For The Type " + Doc_Type + " IN " + Loc_Code);
            throw (ex);
        }

        public string GetDocSerial(string Loc_Code, string Doc_Type, string Doc_Year, string Remote, OracleConnection con, OracleTransaction tr)
        {
            string stRemote = "";
            if (Remote != "") stRemote = "@" + Remote;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "select trim(doc_prefix)||doc_serial from comn_documentserials" + stRemote + " where loc_code='" + Loc_Code + "' and doc_type='" + Doc_Type + "' and PROC_YEAR='" + Doc_Year + "'";
            if (tr != null)
                cmd.Transaction = tr;
            OracleDataReader rs = cmd.ExecuteReader();
            try
            {
                if (rs.Read())
                {
                    string YR = "";
                    YR = Doc_Year.Substring(2, 2);
                    return Loc_Code + YR + Doc_Type + rs.GetString(0).PadLeft(6, '0');
                }
            }
            catch { throw; }
            finally { if (rs != null)rs.Close(); cmd.Dispose(); }
            Exception ex = new Exception("No Document Serial Found For The Type " + Doc_Type + " IN " + Loc_Code);
            throw (ex);

        }
        public bool UpdateDocSerial(string Loc_Code, string Doc_Type, string Doc_Year, OracleConnection con, OracleTransaction tr)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "update comn_documentserials set doc_serial =doc_serial+1 where loc_code='" + Loc_Code + "' and doc_type='" + Doc_Type + "' and PROC_YEAR='" + Doc_Year + "'";
            try
            {
                if (tr != null)
                    cmd.Transaction = tr;
                if (cmd.ExecuteNonQuery() > 0) return true;
            }
            catch { throw; }
            finally { cmd.Dispose(); }
            Exception ex = new Exception("No Document Serial Found For The Type " + Doc_Type + " IN " + Loc_Code);
            throw (ex);

        }
        public bool UpdateDocSerial(string Loc_Code, string Doc_Type, string Doc_Year, string Remote, OracleConnection con, OracleTransaction tr)
        {
            OracleCommand cmd = con.CreateCommand();
            string Remote_Loc = "";
            if (Remote != "") Remote_Loc = "@" + Remote;
            try
            {
                cmd.CommandText = "update comn_documentserials" + Remote_Loc + " set doc_serial =doc_serial+1 where loc_code='" + Loc_Code + "' and doc_type='" + Doc_Type + "' and PROC_YEAR='" + Doc_Year + "'";
                if (tr != null)
                    cmd.Transaction = tr;
                if (cmd.ExecuteNonQuery() > 0) return true;
            }
            catch { throw; }
            finally { cmd.Dispose(); }
            Exception ex = new Exception("No Document Serial Found For The Type " + Doc_Type + " IN " + Loc_Code);
            throw (ex);

        }

        public string GetDocSerial_Acct(string Loc_Code, string Doc_Type, string Doc_Year, OracleConnection con, OracleTransaction tr)
        {
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader rs = null;
            cmd.CommandText = "select trim(doc_prefix)||doc_serial from comn_documentserials where  doc_type='" + Doc_Type + "' and PROC_YEAR='" + Doc_Year + "'";
            try
            {
                if (tr != null)
                    cmd.Transaction = tr;
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    string YR = "";
                    YR = Doc_Year.Substring(2, 2);
                    return Loc_Code + YR + Doc_Type + rs.GetString(0).PadLeft(6, '0');
                }

            }
            catch
            {
                throw;
            }
            finally { if (rs != null)rs.Close(); cmd.Dispose(); }
            Exception ex = new Exception("No Document Serial Found For The Type " + Doc_Type + " IN " + Loc_Code);
            throw (ex);

        }


        public bool UpdateDocSerial_Acct(string Doc_Type, string Doc_Year, OracleConnection con, OracleTransaction tr)
        {
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = "update comn_documentserials set doc_serial =doc_serial+1 where doc_type='" + Doc_Type + "' and PROC_YEAR='" + Doc_Year + "'";
            try
            {
                if (tr != null)
                    cmd.Transaction = tr;
                if (cmd.ExecuteNonQuery() > 0) return true;
            }
            catch { throw; }
            finally
            {
                cmd.Dispose();
            }
            Exception ex = new Exception("No Document Serial Found For The Type " + Doc_Type);
            throw (ex);

        }

        public string GetDeletedDocSerial(string Loc_Code, string Doc_Type, string Doc_Year, OracleConnection con, OracleTransaction tr)
        {

            OracleCommand cmd = con.CreateCommand();
            //cmd.CommandText = "select trim(doc_prefix)||doc_serial from comn_documentserials where loc_code='" + Loc_Code + "' and doc_type='" + Doc_Type + "' and PROC_YEAR='" + Doc_Year + "'";
            cmd.CommandText = "Select doc_serial,SLNO From ACCT_SUPPAY_DELETEDDOC Where LOC_CODE='" + Loc_Code + "' And DOC_TYPE='" + Doc_Type + "' And DOC_YEAR='" + Doc_Year + "'" +
                            " And SLNO=(Select Min(SLNO)As SLNO From ACCT_SUPPAY_DELETEDDOC Where LOC_CODE='" + Loc_Code + "' And DOC_TYPE='" + Doc_Type + "' And DOC_YEAR='" + Doc_Year + "')";
            if (tr != null)
                cmd.Transaction = tr;
            OracleDataReader rs = cmd.ExecuteReader();
            try
            {
                if (rs.Read())
                {
                    int intRowsAffected = 0;
                    string stQuery = "Delete From ACCT_SUPPAY_DELETEDDOC Where SLNO=" + rs["SLNO"].ToString() + " And LOC_CODE='" + Loc_Code + "' And DOC_TYPE='" + Doc_Type + "' And DOC_YEAR='" + Doc_Year + "'";
                    if (!((ExecuteCmd(stQuery, out intRowsAffected, con, tr)) && intRowsAffected > 0))
                    {
                        throw new Exception("Error On Inserting INVT_ITEMSUPPLIER");
                    }
                    string YR = "";
                    YR = Doc_Year.Substring(2, 2);
                    return Loc_Code + YR + Doc_Type + rs["DOC_SERIAL"].ToString().PadLeft(6, '0');
                }
                return "";
            }
            catch { throw; }
            finally { if (rs != null) rs.Close(); cmd.Dispose(); }
            //Exception ex = new Exception("No Document Serial Found For The Type " + Doc_Type + " IN " + Loc_Code);
            //throw (ex);
        }

        //ADDED MUKESH 
        public bool Lock_DocSerial(bool Locks, OracleConnection connection, string Doc_type, string Loc_code, string Doc_year)
        {
            try
            {
                string remote = "";
                if (Loc_code != Defaults.Def_Base_LOC)
                {
                    string ORemote = GetValue("select link_name from sys_dbconnectionstrings where loc_code='" + Defaults.Def_Base_LOC + "' and machine='SERVER'", connection, null);
                    getPooledConnection("SERVER", Loc_code, connection, null, out remote);
                    string dbLink = remote;
                    if (dbLink.Length > 0)
                    {
                        if (ORemote == remote)
                        {
                            remote = "";
                        }
                        else
                        {
                            remote = "@" + dbLink;
                        }
                    }
                    else
                    {
                        throw new Exception("Erron On Checking Management Location..!!");
                    }
                }

                int i = 0;
                long rows = 0;
                if (Locks)
                {
                    ExecuteCmd("UPDATE COMN_DOCUMENTSERIALS" + remote + " SET  ISLOCKED='1' WHERE DOC_TYPE='" + Doc_type + "' AND LOC_CODE = '" + Loc_code + "' AND PROC_YEAR='" + Doc_year + "' AND ISLOCKED='0'", out rows, connection, null);
                    if (rows == 1)
                    {
                        return true;
                    }
                    else
                    {
                        while (i < 100)
                        {
                            Thread.Sleep(100);
                            if (ExecuteCmd("UPDATE COMN_DOCUMENTSERIALS" + remote + " SET  ISLOCKED='1' WHERE DOC_TYPE='" + Doc_type + "'AND LOC_CODE = '" + Loc_code + "' AND PROC_YEAR='" + Doc_year + "' AND ISLOCKED='0'", out rows, connection, null) && rows == 1)
                                return true;
                            i++;
                        }
                        Lock_DocSerial(false, connection, Doc_type, Loc_code, Doc_year);  // Waiting 10 seconds and forced lock - to solve any db/connection failure issues in last doc save.
                    }
                }
                else
                {
                    ExecuteCmd("UPDATE COMN_DOCUMENTSERIALS" + remote + " SET  ISLOCKED='0' WHERE DOC_TYPE='" + Doc_type + "' AND LOC_CODE = '" + Loc_code + "' AND PROC_YEAR='" + Doc_year + "' AND ISLOCKED='1'", out rows, connection, null);
                    if (rows == 1) return true;
                }
                return false;
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }

        # region MakeNewPartNumber And MakeNewPartNumberSup

        public string Get_PartNumber(string CategoryCat0, OracleConnection Conn, OracleTransaction Tr)
        {
            string ItemCode;
            string temp;
            long ItemNumber;
            string stPartNo = "";
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            String oSql = "";
            try
            {
                cmd = Conn.CreateCommand();
                if (Tr != null) cmd.Transaction = Tr;

                oSql = "Select PART_NUMBER,CAT_0,SUBSTR(PART_NUMBER,3) FROM INVT_INVENTORYMASTER WHERE CAT_0='" + CategoryCat0 + "' ORDER BY to_number(SUBSTR(PART_NUMBER,3)) DESC";
                oSql = "Select max(to_number(substr(part_number,3))) FROM INVT_INVENTORYMASTER WHERE CAT_0='" + CategoryCat0 + "'";
                cmd.CommandText = oSql;
                rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    if (rs.Read())
                    {
                        stPartNo = "";
                        ItemCode = rs[0].ToString();//rs.GetString(0);
                        if (ItemCode == "")
                        {
                            return stPartNo = CategoryCat0 + "01";
                        }
                        temp = ItemCode; //ItemCode.Substring(2, ItemCode.Length - 2);
                        ItemNumber = Convert.ToInt64(temp);
                        ItemNumber++;
                        temp = (ItemNumber.ToString()).PadLeft(2, '0');
                        return CategoryCat0 + temp;
                    }
                }
                else
                {
                    stPartNo = CategoryCat0 + "01";
                    return stPartNo;
                }
                return "";
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (rs != null) rs.Close();
            }

        }
        public string Get_PartNumberSup(string SupplierCode, OracleConnection Conn, OracleTransaction Tr)
        {
            string ItemCode;
            string temp;
            string SupplierPart;
            long ItemNumber;
            String oSql = "";
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                cmd = Conn.CreateCommand();
                // OSQL = "Select  (to_number(SUBSTR(PART_NUMBER_SUP,5)))+1 As PART_NUMBER_SU From INVT_INVENTORYMASTER Where SUPPLIER_CODE='" + SupplierCode + "' Order By to_number(SUBSTR(PART_NUMBER_SUP,5)) Desc";
                oSql = "Select PART_NUMBER_SUP From INVT_INVENTORYMASTER Where SUPPLIER_CODE='" + SupplierCode + "' Order By to_number(SUBSTR(PART_NUMBER_SUP,5)) Desc NULLS LAST";
                oSql = "Select PART_NUMBER_SUP From INVT_ITEMSUPPLIER Where SUPPLIER_CODE='" + SupplierCode + "' And substr(part_number_sup,0,4)= Substr(SUPPLIER_CODE,-4,4) Order By to_number(SUBSTR(PART_NUMBER_SUP,5)) Desc NULLS LAST";

                cmd.CommandText = oSql;
                if (Tr != null) cmd.Transaction = Tr;
                rs = cmd.ExecuteReader();
                //if (SupplierCode.Length < 4)
                //{
                //    if (DataConnector.Message("Supplier Code Is Small.  Are You Proceed", "E", "Q", MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                //    {
                //        SupplierCode = SupplierCode.PadLeft(5, '0');
                //    }
                //    else
                //    {
                //        return "";
                //    }
                //}
                if (rs.HasRows)
                {

                    if (rs.Read())
                    {
                        ItemCode = rs.GetString(0);
                        if (ItemCode.Length <= 4)
                        {
                            ItemNumber = Convert.ToInt64(ItemCode);
                            ItemNumber++;
                        }
                        else
                        {
                            temp = ItemCode.Substring(4, ItemCode.Length - 4);
                            ItemNumber = Convert.ToInt64(temp) + 1;
                            SupplierPart = SupplierCode.Substring(SupplierCode.Length - 4, 4);
                            temp = (ItemNumber.ToString()).PadLeft(2, '0');
                            return SupplierPart + temp;
                        }
                    }
                }
                else// if (!rs.HasRows)
                {
                    //                    SupplierPart = SupplierCode.Substring(2, 4);
                    SupplierPart = SupplierCode.Substring(SupplierCode.Length - 4, 4);
                    return SupplierPart + "01";
                }
                return "";
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                cmd.Dispose();
                rs.Dispose();
            }
        }
        public string Get_PartNumberSup_New(string SupplierCode, OracleConnection Conn, OracleTransaction Tr)
        {
            string ItemCode;
            string temp;
            string SupplierPart;
            long ItemNumber;
            String oSql = "";
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                cmd = Conn.CreateCommand();
                //oSql = "Select Case When Max(PART_NUMBER_SUP)is null Then 1 Else Max(PART_NUMBER_SUP) End As PART_NUMBER_SUP From (Select max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER)";
                oSql = "Select Case When Max(PART_NUMBER_SUP)is null Then 1 Else Max(PART_NUMBER_SUP) End As PART_NUMBER_SUP From (Select max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER UNION "
               + "Select max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER_REQ)";
                cmd.CommandText = oSql;
                if (Tr != null) cmd.Transaction = Tr;
                rs = cmd.ExecuteReader();

                //if (SupplierCode.Length < 4)
                //{
                //    if (DataConnector.Message("Supplier Code Is Small.  Are You Proceed", "E", "Q", MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                //    {
                //        SupplierCode = SupplierCode.PadLeft(5, '0');
                //    }
                //    else
                //    {
                //        return "";
                //    }
                //}
                if (rs.HasRows)
                {

                    if (rs.Read())
                    {
                        ItemCode = Convert.ToString(rs.GetInt64(0));
                        if (ItemCode.Length <= 4)
                        {
                            ItemNumber = Convert.ToInt64(ItemCode);
                            ItemNumber++;
                        }
                        else
                        {
                            //ItemNumber = Convert.ToInt64(ItemCode);
                            //ItemNumber++;
                            //temp = ItemCode.Substring(4, ItemCode.Length - 4);

                            //ItemNumber = Convert.ToInt64(temp) + 1;
                            //SupplierPart = SupplierCode.Substring(SupplierCode.Length - 4, 4);

                            //temp = (ItemNumber.ToString()).PadLeft(2, '0');
                            return ItemCode;//SupplierPart + temp;
                        }
                    }
                }
                else// if (!rs.HasRows)
                {
                    //                    SupplierPart = SupplierCode.Substring(2, 4);
                    SupplierPart = SupplierCode.Substring(SupplierCode.Length - 4, 4);
                    return SupplierPart + "01";
                }
                return "";
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                cmd.Dispose();
                rs.Dispose();
            }
        }
        public string Get_PartNumberSup_New20180515(string SupplierCode, OracleConnection Conn, OracleTransaction Tr)
        {
            string ItemCode;
            string temp;
            string SupplierPart;
            long ItemNumber;
            String oSql = "";
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                cmd = Conn.CreateCommand();
                //oSql = "Select Case When Max(PART_NUMBER_SUP)is null Then 1 Else Max(PART_NUMBER_SUP) End As PART_NUMBER_SUP From (Select max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER)";
                oSql = "Select Case When Max(PART_NUMBER_SUP)is null Then 1 Else Max(PART_NUMBER_SUP) End As PART_NUMBER_SUP From (Select max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER UNION "
               + "Select max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER_REQ)";

                oSql = "Select SUPPLIERPART||Lpad( (Case When Max(PART_NUMBER_SUP)is null Then  1 Else Max(PART_NUMBER_SUP) End) ,2,0)As PART_NUMBER_SUP From " +
                    " (" +
                    " Select Max(Substr(SUPPLIER_CODE,-4,4)) As SUPPLIERPART,Max(Substr(SUPPLIER_CODE,-4,4)), max(to_number(Substr(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER Where SUPPLIER_CODE='636363' And substr(part_number_sup,0,4)= Substr(SUPPLIER_CODE,-4,4) Group By SUPPLIER_CODE" +
                    " Union " +
                    " Select Max(Substr(SUPPLIER_CODE,-4,4)) As SUPPLIERPART,Max(Substr(SUPPLIER_CODE,-4,4)), max(to_number(Substr(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_ITEMSUPPLIER Where SUPPLIER_CODE='636363' And substr(part_number_sup,0,4)= Substr(SUPPLIER_CODE,-4,4) Group By SUPPLIER_CODE" +
                    " )Group By SUPPLIERPART";
                cmd.CommandText = oSql;
                if (Tr != null) cmd.Transaction = Tr;
                rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    if (rs.Read())
                    {
                        //ItemCode = Convert.ToString(rs.GetInt64(0));
                        ItemCode = rs["PART_NUMBER_SUP"].ToString();
                        //if (ItemCode.Length <= 4)
                        //{
                        //    //ItemNumber = Convert.ToInt64(ItemCode);
                        //    //ItemNumber++;
                        //}
                        //else
                        //{                           
                        return ItemCode;
                        //}
                    }
                }
                else
                {
                    SupplierPart = SupplierCode.Substring(SupplierCode.Length - 4, 4);
                    return SupplierPart + "01";
                }
                return "";
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                cmd.Dispose();
                rs.Dispose();
            }
        }

        //For Bakery ItemCode Creation B123456
        public string Get_PartNumberSupBK(string SupplierCode, OracleConnection Conn, OracleTransaction Tr)
        {
            string ItemCode;
            string temp;
            string SupplierPart;
            long ItemNumber;
            String oSql = "";
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                cmd = Conn.CreateCommand();
                oSql = "Select PART_NUMBER_SUP From INVT_INVENTORYMASTER Where SUPPLIER_CODE='" + SupplierCode + "' Order By to_number(SUBSTR(PART_NUMBER_SUP,5)) Desc NULLS LAST";
                oSql = "Select PART_NUMBER_SUP From INVT_ITEMSUPPLIER Where SUPPLIER_CODE='" + SupplierCode + "' And substr(part_number_sup,2,4)= Substr(SUPPLIER_CODE,-4,4) Order By to_number(SUBSTR(PART_NUMBER_SUP,5)) Desc NULLS LAST";
                cmd.CommandText = oSql;
                if (Tr != null) cmd.Transaction = Tr;
                rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {

                    if (rs.Read())
                    {
                        ItemCode = rs.GetString(0);
                        if (ItemCode.Length <= 4)
                        {
                            ItemNumber = Convert.ToInt64(ItemCode);
                            ItemNumber++;
                        }
                        else
                        {
                            temp = ItemCode.Substring(5, ItemCode.Length - 5);
                            ItemNumber = Convert.ToInt64(temp) + 1;
                            SupplierPart = SupplierCode.Substring(SupplierCode.Length - 4, 4);
                            temp = (ItemNumber.ToString()).PadLeft(2, '0');
                            return "B" + SupplierPart + temp;
                        }
                    }
                }
                else
                {
                    SupplierPart = SupplierCode.Substring(SupplierCode.Length - 4, 4);
                    return "B" + SupplierPart + "01";
                }
                return "";
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                cmd.Dispose();
                rs.Dispose();
            }
        }

        /*
        public string GetPartNumber_SUP(string Supplier_code, OracleConnection con, OracleTransaction Tr)
        {
            //
            //return "";
            //    string Part_number = "",sql="";
            //    OracleCommand cmd = con.CreateCommand();
            //    if (Tr != null) cmd.Transaction = Tr;
            //    sql = "select min(part_number_serial) from util_part_numberPool where supplier_code='" + Supplier_code + "'";
            //    cmd.CommandText = sql;
            //    OracleDataReader rs = cmd.ExecuteReader();
            //    if (rs.Read())
            //    {
            //    //    Supplier_code = Supplier_code.
            //   //     Part_number = rs[0].ToString(0).PadLeft(4);
            //        return Part_number;

            //    }
            //    rs.Close();
            //    cmd.Dispose();
            //    if (Part_number=="" && Supplier_code.Length == 6)
            //    {

            //    }   
        
        }
         */

        public string GetReq_PartNumber(string CategoryCat0, OracleConnection Conn, OracleTransaction Tr)
        {
            string ItemCode;
            string temp;
            long ItemNumber;
            string stPartNo = "";
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            String oSql = "";
            try
            {
                cmd = Conn.CreateCommand();
                if (Tr != null) cmd.Transaction = Tr;

                // oSql = "Select PART_NUMBER,CAT_0,SUBSTR(PART_NUMBER,3) FROM INVT_INVENTORYMASTER_REQ WHERE CAT_0='" + CategoryCat0 + "' ORDER BY to_number(SUBSTR(PART_NUMBER,3)) DESC";
                oSql = "Select max(to_number(substr(part_number,3))) FROM INVT_INVENTORYMASTER_REQ WHERE CAT_0='" + CategoryCat0 + "'";

                //                oSql = @"Select CAT_0||(Case When Max(PART_NUMBER)is null Then  1 Else Max(PART_NUMBER) End) As PART_NUMBER From
                oSql = @"Select (Case When Max(PART_NUMBER)is null Then  1 Else Max(PART_NUMBER) End) As PART_NUMBER From
                       (Select CAT_0,max(to_number(substr(part_number,3))) As PART_NUMBER From INVT_INVENTORYMASTER WHERE CAT_0='" + CategoryCat0 + "' GROUP BY CAT_0 UNION " +
                       @" Select CAT_0,max(to_number(substr(part_number,3))) As PART_NUMBER From INVT_INVENTORYMASTER_REQ WHERE CAT_0='" + CategoryCat0 + "' GROUP BY CAT_0)GROUP BY CAT_0";
                cmd.CommandText = oSql;
                rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    if (rs.Read())
                    {
                        stPartNo = "";
                        ItemCode = rs[0].ToString();//rs.GetString(0);
                        if (ItemCode == "")
                        {
                            return stPartNo = CategoryCat0 + "01";
                        }
                        temp = ItemCode; //ItemCode.Substring(2, ItemCode.Length - 2);
                        ItemNumber = Convert.ToInt64(temp);
                        ItemNumber++;
                        temp = (ItemNumber.ToString()).PadLeft(2, '0');
                        return CategoryCat0 + temp;
                    }
                }
                else
                {
                    stPartNo = CategoryCat0 + "01";
                    return stPartNo;
                }
                return "";
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (rs != null) rs.Close();
            }

        }
        public string GetReq_PartNumberSup(string SupplierCode, OracleConnection Conn, OracleTransaction Tr)
        {
            string ItemCode;
            string temp;
            string SupplierPart;
            long ItemNumber;
            String oSql = "";
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                cmd = Conn.CreateCommand();
                //oSql = "Select Case When Max(PART_NUMBER_SUP)is null Then 1 Else Max(PART_NUMBER_SUP) End As PART_NUMBER_SUP From (Select max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER)";
                oSql = "Select Case When Max(PART_NUMBER_SUP)is null Then 1 Else Max(PART_NUMBER_SUP) End As PART_NUMBER_SUP From (Select max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER UNION "
               + "Select max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER_REQ)";

                oSql = "Select SUPPLIERPART||(Case When Max(PART_NUMBER_SUP)is null Then  1 Else Max(PART_NUMBER_SUP) End) As PART_NUMBER_SUP From " +
                     " (Select MAX(SUBSTR(SUPPLIER_CODE,-4,4)) AS SUPPLIERPART,MAX(SUBSTR(SUPPLIER_CODE,-4,4)), max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER WHERE SUPPLIER_CODE='" + SupplierCode + "' GROUP BY SUPPLIER_CODE" +
                     " UNION  Select MAX(SUBSTR(SUPPLIER_CODE,-4,4)) AS SUPPLIERPART,MAX(SUBSTR(SUPPLIER_CODE,-4,4)), max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER_REQ WHERE SUPPLIER_CODE='" + SupplierCode + "' GROUP BY SUPPLIER_CODE )GROUP BY SUPPLIERPART";
                cmd.CommandText = oSql;
                if (Tr != null) cmd.Transaction = Tr;
                rs = cmd.ExecuteReader();

                //if (SupplierCode.Length < 4)
                //{
                //    if (DataConnector.Message("Supplier Code Is Small.  Are You Proceed", "E", "Q", MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                //    {
                //        SupplierCode = SupplierCode.PadLeft(5, '0');
                //    }
                //    else
                //    {
                //        return "";
                //    }
                //}
                if (rs.HasRows)
                {
                    if (rs.Read())
                    {
                        ItemCode = rs["PART_NUMBER_SUP"].ToString();
                        if (ItemCode.Length <= 4)
                        {
                            ItemNumber = Convert.ToInt64(ItemCode);
                            ItemNumber++;
                        }
                        else
                        {
                            //ItemNumber = Convert.ToInt64(ItemCode);
                            //ItemNumber++;
                            //temp = ItemCode.Substring(4, ItemCode.Length - 4);

                            //ItemNumber = Convert.ToInt64(temp) + 1;
                            //SupplierPart = SupplierCode.Substring(SupplierCode.Length - 4, 4);

                            //temp = (ItemNumber.ToString()).PadLeft(2, '0');
                            return ItemCode;//SupplierPart + temp;
                        }
                    }
                }
                if (SupplierCode.Length <= 4)
                {
                    return "1000";
                    // ItemNumber = Convert.ToInt64(ItemCode);
                    // ItemNumber++;
                }
                else// if (!rs.HasRows)
                {
                    //                    SupplierPart = SupplierCode.Substring(2, 4);
                    SupplierPart = SupplierCode.Substring(SupplierCode.Length - 4, 4);
                    return SupplierPart + "01";
                }
                return "";
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                cmd.Dispose();
                rs.Dispose();
            }
        }
        public string GetReq_PartNumberSup_New20180515(string SupplierCode, OracleConnection Conn, OracleTransaction Tr)
        {
            string ItemCode;
            string temp;
            string SupplierPart;
            long ItemNumber;
            String oSql = "";
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                cmd = Conn.CreateCommand();
                //oSql = "Select Case When Max(PART_NUMBER_SUP)is null Then 1 Else Max(PART_NUMBER_SUP) End As PART_NUMBER_SUP From (Select max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER)";
                oSql = "Select Case When Max(PART_NUMBER_SUP)is null Then 1 Else Max(PART_NUMBER_SUP) End As PART_NUMBER_SUP From (Select max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER UNION "
                    + "Select max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER_REQ)";

                oSql = "Select SUPPLIERPART||(Case When Max(PART_NUMBER_SUP)is null Then  1 Else Max(PART_NUMBER_SUP) End) As PART_NUMBER_SUP From " +
                     " (Select MAX(SUBSTR(SUPPLIER_CODE,-4,4)) AS SUPPLIERPART,MAX(SUBSTR(SUPPLIER_CODE,-4,4)), max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER WHERE SUPPLIER_CODE='" + SupplierCode + "' GROUP BY SUPPLIER_CODE" +
                     " UNION  Select MAX(SUBSTR(SUPPLIER_CODE,-4,4)) AS SUPPLIERPART,MAX(SUBSTR(SUPPLIER_CODE,-4,4)), max(to_number(SUBSTR(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER_REQ WHERE SUPPLIER_CODE='" + SupplierCode + "' GROUP BY SUPPLIER_CODE )GROUP BY SUPPLIERPART";
                oSql = "Select SUPPLIERPART||Lpad( (Case When Max(PART_NUMBER_SUP)is null Then  1 Else Max(PART_NUMBER_SUP) End) ,2,0)As PART_NUMBER_SUP From " +
                    " (" +
                    " Select Max(Substr(SUPPLIER_CODE,-4,4)) As SUPPLIERPART,Max(Substr(SUPPLIER_CODE,-4,4)), max(to_number(Substr(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER Where SUPPLIER_CODE='" + SupplierCode + "' And substr(part_number_sup,0,4)= Substr(SUPPLIER_CODE,-4,4) Group By SUPPLIER_CODE" +
                    " Union " +
                    " Select Max(Substr(SUPPLIER_CODE,-4,4)) As SUPPLIERPART,Max(Substr(SUPPLIER_CODE,-4,4)), max(to_number(Substr(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_INVENTORYMASTER_REQ Where SUPPLIER_CODE='" + SupplierCode + "' And substr(part_number_sup,0,4)= Substr(SUPPLIER_CODE,-4,4) Group By SUPPLIER_CODE" +
                    " Union " +
                    " Select Max(Substr(SUPPLIER_CODE,-4,4)) As SUPPLIERPART,Max(Substr(SUPPLIER_CODE,-4,4)), max(to_number(Substr(PART_NUMBER_SUP,5))+1) As PART_NUMBER_SUP From INVT_ITEMSUPPLIER Where SUPPLIER_CODE='" + SupplierCode + "' And substr(part_number_sup,0,4)= Substr(SUPPLIER_CODE,-4,4) Group By SUPPLIER_CODE" +
                    " )Group By SUPPLIERPART";
                cmd.CommandText = oSql;
                if (Tr != null) cmd.Transaction = Tr;
                rs = cmd.ExecuteReader();

                //if (SupplierCode.Length < 4)
                //{
                //    if (DataConnector.Message("Supplier Code Is Small.  Are You Proceed", "E", "Q", MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                //    {
                //        SupplierCode = SupplierCode.PadLeft(5, '0');
                //    }
                //    else
                //    {
                //        return "";
                //    }
                //}
                if (rs.HasRows)
                {
                    if (rs.Read())
                    {
                        ItemCode = rs["PART_NUMBER_SUP"].ToString();
                        if (ItemCode.Length <= 4)
                        {
                            // ItemNumber = Convert.ToInt64(ItemCode);
                            // ItemNumber++;
                        }
                        else
                        {
                            //ItemNumber = Convert.ToInt64(ItemCode);
                            //ItemNumber++;
                            //temp = ItemCode.Substring(4, ItemCode.Length - 4);

                            //ItemNumber = Convert.ToInt64(temp) + 1;
                            //SupplierPart = SupplierCode.Substring(SupplierCode.Length - 4, 4);

                            //temp = (ItemNumber.ToString()).PadLeft(2, '0');
                            return ItemCode;//SupplierPart + temp;
                        }
                    }
                }
                else// if (!rs.HasRows)
                {
                    //                    SupplierPart = SupplierCode.Substring(2, 4);
                    SupplierPart = SupplierCode.Substring(SupplierCode.Length - 4, 4);
                    return SupplierPart + "01";
                }
                return "";
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                cmd.Dispose();
                rs.Dispose();
            }
        }

        # endregion MakeNewPartNumber And MakeNewPartNumberSup

        /* public string getPart_Number_Sup(string supp_item_code, string Supplier_Code, OracleConnection con, OracleTransaction Tr)
        {
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                string condSup = "";
                cmd = con.CreateCommand();
                if (Supplier_Code.Length == 6) condSup = " and substr(part_number_sup,0,4)='" + Supplier_Code.Substring(2, 4) + "' ";
                //string sql = "select part_number_sup from INVT_ITEMPACKING where supplier_itemcode='" + supp_item_code + "'" + condSup + " order by case(when packingorder<>0 then packingorder),zeropacking";
                string sql = "select part_number_sup,packing_order,ZERO_SERIAL from INVT_ITEMPACKING where supplier_itemcode='" + supp_item_code + "'" +
                    condSup + " order by ZERO_SERIAL,packing_order";
                if (Tr != null)
                    cmd.Transaction = Tr;
                cmd.CommandText = sql;
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    return rs["part_number_sup"].ToString();
                }
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                rs.Dispose();
                cmd.Dispose();
            }
            return "";
        }
        */
        public string getPart_Number_Sup(string supp_item_code, string Supplier_Code, OracleConnection con, OracleTransaction Tr)
        {
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                string condSup = "";
                cmd = con.CreateCommand();
                //if (Supplier_Code.Length == 6) condSup = " and substr(part_number_sup,0,4)='" + Supplier_Code.Substring(2, 4) + "' ";
                //string sql = "select part_number_sup from INVT_ITEMPACKING where supplier_itemcode='" + supp_item_code + "'" + condSup + " order by case(when packingorder<>0 then packingorder),zeropacking";
                string sql = "select part_number_sup,packing_order,ZERO_SERIAL from INVT_ITEMPACKING where supplier_itemcode='" + supp_item_code + "'  AND SUPPLIER_CODE='" + Supplier_Code + "'";
                //condSup + " order by ZERO_SERIAL,packing_order";
                if (Tr != null)
                    cmd.Transaction = Tr;
                cmd.CommandText = sql;
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    return rs["part_number_sup"].ToString();
                }
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                rs.Dispose();
                cmd.Dispose();
            }
            return "";
        }
        /// <summary>
        /// Added DD on 24/May/2011 03:15 PM
        /// </summary>
        /// <param name="stPartNumberSup"></param>
        /// <param name="oraConn"></param>
        /// <param name="Tr"></param>
        /// <returns></returns>
        public bool ReCreateProducrPackings(string stPartNumberSup, OracleConnection oraConn, OracleTransaction Tr)
        {
            OracleCommand Cmd = null;
            OracleDataReader Rs = null;
            int intRowAffect = 0;
            string Sql = "";
            try
            {
                Sql = "Select part_number_sup,max(packing_order) as packing_order from invt_itempacking";
                Sql = Sql + " Where PART_NUMBER_SUP='" + stPartNumberSup + "'";
                Sql = Sql + " group by part_number_sup";

                Cmd = oraConn.CreateCommand();
                Cmd.CommandText = Sql;
                if (Tr != null) Cmd.Transaction = Tr;
                Rs = Cmd.ExecuteReader();

                if (Rs.HasRows)
                {
                    string StPackings = "";
                    if (Rs.Read()) // while(Rs.Read())
                    {
                        StPackings = "";
                        if (!(int.Parse(Rs["packing_order"].ToString()).Equals(1)))
                        {
                            Sql = "Update INVT_ITEMPACKING Set PRODUCTPACKINGS=" +
                                  "(" +
                                  "Select case packing_order" +

                                      " when 4 then ( '1 x ' || (select distinct packqty from invt_itempacking where part_number_sup='" + Rs["PART_NUMBER_SUP"].ToString() + "' and packing_order=4)" +
                                        " ||' x ' || (select distinct packqty from invt_itempacking where part_number_sup='" + Rs["PART_NUMBER_SUP"].ToString() + "' and packing_order=3)" +
                                        " ||' x ' || (select distinct packqty from invt_itempacking where part_number_sup='" + Rs["PART_NUMBER_SUP"].ToString() + "' and packing_order=2))" +

                                      " when 3 then ( '1 x ' || (select distinct packqty from invt_itempacking where part_number_sup='" + Rs["PART_NUMBER_SUP"].ToString() + "' and packing_order=3)" +
                                        " ||' x ' || (select distinct packqty from invt_itempacking where part_number_sup='" + Rs["PART_NUMBER_SUP"].ToString() + "' and packing_order=2))" +

                                      " when 2 then ( '1 x ' || (select distinct packqty from invt_itempacking where part_number_sup='" + Rs["PART_NUMBER_SUP"].ToString() + "' and packing_order=2))" +

                                      " else '1'" +

                                      " end as productpackings" +

                                   " from invt_itempacking where part_number_sup='" + Rs["PART_NUMBER_SUP"].ToString() + "' and packing_order=" + int.Parse(Rs["packing_order"].ToString()) + " and default_packing='Y'" +
                                   ")" +
                                   " Where part_number_sup='" + Rs["PART_NUMBER_SUP"].ToString() + "' And zero_serial=0";
                        }
                        else
                        {
                            Sql = "Update INVT_ITEMPACKING Set PRODUCTPACKINGS='" + "1" + "' Where part_number_sup='" + Rs["PART_NUMBER_SUP"].ToString() + "' And zero_serial=0";
                        }
                        ExecuteCmd(Sql, out intRowAffect, oraConn, Tr);
                    }
                    Rs.Close();
                }
                Cmd.Dispose();
                if (intRowAffect <= 0) { throw new Exception("Error On Productpackings Updation"); }
                return true;
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                if (Rs != null) Rs.Dispose();
                if (Cmd != null) Cmd.Dispose();
            }
            return false;
        }
        public bool updateTable(string strTable, string strSetClause, string strWhere, OracleConnection oraConn)
        {
            bool bReturn = false;
            long RET = 0;
            string strOracle;
            strOracle = "UPDATE " + strTable.Trim() + " SET " + strSetClause;
            if (strWhere != null)
            {
                strOracle = strOracle + " WHERE " + strWhere.Trim();
            }
            OracleCommand oraCommand = new OracleCommand(strOracle, oraConn);
            try
            {
                RET = oraCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception exp)
            {

                MessageBox.Show(exp.ToString());
            }

            finally
            {
                if (oraCommand != null) oraCommand.Dispose();
            }
            return false;
        }

        //................... Update method.................


        //..................................... Delete method.................
        public static int GetDateDifference(DateTime DtFrom, DateTime DtTo)
        {
            return 1;// (((MinDate.Year - MaxDate.Year) * 12) + 1 + (MinDate.Month - MaxDate.Month));
        }

        public enum TransferTypes
        {
            ACCTHEAD, CURRENCY, DELIVERY_LOCS, LOCATIONS, SUBSIDIARY, CONTRACT, BUNDLING, BARCODELOG, BRAND, CATEGORY, DELIVERY_NOTE,
            DIVISION, DIVISION_INT, ITEMLINKING, NEWBARCODE, GONDOLA, GRVI, INVT_MASTER, INVOICE, ITEMPACKING, SPLIT_UNIT, PRICE_HISTORY, GRV, LPO, REQ, SO, SPLIT, GTV, GTVREC, SCT, ACCOUNTS,
            PRICE_BATCH, TRANREQ, TENPRC, GPO, REM, CSUBREQ, CITEMSREQ, CPACKINGREQ, CBARCODEREQ, CBUNDLEREQ, CLUGGREQ, CSUPADDL, CNEWDIV, IADREQ
        }
        // Added 20181112 CSUBREQ,CITEMSREQ,CPACKINGREQ,CBARCODEREQ,CBUNDLEREQ,CLUGGREQ
        //public bool AddDataTransfer(string Doc_No, string MODE_A_E_D, TransferTypes Typ, OracleConnection con, OracleTransaction tr)
        //{
        //    return true; 
        //}
        public bool AddDataTransfer(string Doc_No, string MODE_A_E_D, string Base_Loc_Code, string To_REC_Loc_Code, TransferTypes Typ, OracleConnection con, OracleTransaction tr)
        {
            return AddDataTransfer(Doc_No, MODE_A_E_D, Base_Loc_Code, To_REC_Loc_Code, Typ, con, tr, false, false);
        }
        public bool AddDataTransfer(string Doc_No, string MODE_A_E_D, string Base_Loc_Code, string To_REC_Loc_Code, TransferTypes Typ, OracleConnection con, OracleTransaction tr, bool MasterOnly, bool ReferenceOnly)
        {
            OracleCommand cmd = null;
            OracleCommand cmd1 = null;

            try
            {
                if (MODE_A_E_D == "E") MODE_A_E_D = "U";
                if (MODE_A_E_D == "A") MODE_A_E_D = "I";

                int Priority = 5;
                string TableName = "";
                OracleDataReader rs = null;
                string Sql = "";
                switch (Typ)
                {
                    case TransferTypes.DIVISION:
                        Priority = 2;
                        break;
                    case TransferTypes.DIVISION_INT:
                        Priority = 2;
                        break;
                    case TransferTypes.ITEMLINKING:
                        Priority = 2;
                        break;
                    case TransferTypes.NEWBARCODE:
                        Priority = 2;
                        break;
                    case TransferTypes.GONDOLA:
                        Priority = 8;
                        break;
                    case TransferTypes.ITEMPACKING:
                        Priority = 4;
                        break;
                    case TransferTypes.SPLIT_UNIT:
                        Priority = 5;
                        break;
                    case TransferTypes.PRICE_HISTORY:
                        Priority = 5;
                        break;
                    case TransferTypes.ACCTHEAD:
                        Priority = 3;
                        break;
                    case TransferTypes.CURRENCY:
                        Priority = 3;
                        break;
                    case TransferTypes.DELIVERY_LOCS:
                        Priority = 3;
                        break;
                    case TransferTypes.LOCATIONS:
                        Priority = 1;
                        break;
                    case TransferTypes.SUBSIDIARY:
                        Priority = 3;
                        break;
                    case TransferTypes.BRAND:
                        Priority = 2;
                        break;
                    case TransferTypes.CATEGORY:
                        Priority = 2;
                        break;
                    case TransferTypes.BARCODELOG:
                        Priority = 7;
                        break;
                    case TransferTypes.GRVI:
                        Priority = 7; // Changed on April 19 2012 04 51 PM (6->7)
                        break;
                    case TransferTypes.INVOICE:
                        Priority = 6;
                        break;
                    case TransferTypes.GRV:
                        Priority = 6;
                        break;
                    case TransferTypes.LPO:
                        Priority = 6;
                        break;
                    case TransferTypes.GPO:
                        Priority = 6;
                        break;

                    case TransferTypes.REQ:
                        Priority = 8;
                        break;
                    case TransferTypes.SO:
                        Priority = 8;
                        break;
                    case TransferTypes.SPLIT:
                        Priority = 3;
                        break;
                    case TransferTypes.GTV:
                        Priority = 6;
                        break;
                    case TransferTypes.CONTRACT:
                        Priority = 7;
                        break;
                    case TransferTypes.BUNDLING:
                        Priority = 5;
                        break;
                    case TransferTypes.DELIVERY_NOTE:
                        Priority = 8;
                        break;
                    case TransferTypes.PRICE_BATCH:
                        Priority = 5;
                        break;
                    case TransferTypes.ACCOUNTS:
                        Priority = 5; // DONT CHANGE PRIORITY < GRV
                        break;
                    case TransferTypes.TRANREQ:
                        Priority = 7;
                        break;
                    case TransferTypes.TENPRC:
                        Priority = 5;
                        break;
                    case TransferTypes.CSUBREQ:// Added 20181112
                        Priority = 4;
                        break;
                    case TransferTypes.CITEMSREQ:// Added 20181112
                        Priority = 4;
                        break; ;
                    case TransferTypes.CPACKINGREQ:// Added 20181112
                        Priority = 5;
                        break;
                    case TransferTypes.CBARCODEREQ:// Added 20181112
                        Priority = 2;
                        break;
                    case TransferTypes.CBUNDLEREQ:// Added 20181112
                        Priority = 5;
                        break;
                    case TransferTypes.CLUGGREQ:// Added 20181112
                        Priority = 5;
                        break;
                    case TransferTypes.CSUPADDL:// Added 20181112
                        Priority = 6;
                        break;
                    case TransferTypes.CNEWDIV:// Added 20181112
                        Priority = 6;
                        break;
                    case TransferTypes.IADREQ:
                        Priority = 4;
                        break;
                }
                TableName = "TRAN_MASTERUPDATE";
                if (Defaults.Def_MAIN_LOCATION)
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = "select * from TRAN_locations where TRAN_TYPE = '" + Typ.ToString() + "' and Prepare_loc='" + Base_Loc_Code + "' and TO_REC_LOC='" + To_REC_Loc_Code + "' AND ACTIVE='Y'";
                    if (tr != null) cmd.Transaction = tr;
                    rs = cmd.ExecuteReader();
                    while (rs.Read())
                    {
                    re1:
                        if (TableName == "TRAN_MASTERUPDATE")
                        {
                            Sql = "insert into " + TableName + " (LOC_CODE,TO_LOC,CODE,TRAN_TYPE,MODE_FAG,priority,MASTERONLY,REFONLY) values('" + Base_Loc_Code + "','" + rs["COPY_LOC"].ToString() + "','" + Doc_No + "','" + Typ.ToString() + "','" + MODE_A_E_D + "'," + Priority + ",'" + (MasterOnly ? "Y" : "N") + "','" + (ReferenceOnly ? "Y" : "N") + "')";
                        }
                        cmd1 = con.CreateCommand();
                        cmd1.CommandText = Sql;
                        if (tr != null) cmd1.Transaction = tr;
                        try
                        {
                            cmd1.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            {
                                MessageBox.Show("Error" + ex);
                            }
                        }
                        cmd1.Dispose();
                    }
                    rs.Close();
                }
                else
                {
                re:
                    try
                    {
                        Sql = "insert into " + TableName + " (LOC_CODE,TO_LOC,CODE,TRAN_TYPE,MODE_FAG,priority,MASTERONLY,REFONLY) values('" + Base_Loc_Code + "','" + To_REC_Loc_Code + "','" + Doc_No + "','" + Typ.ToString() + "','" + MODE_A_E_D + "'," + Priority + ",'" + (MasterOnly ? "Y" : "N") + "','" + (ReferenceOnly ? "Y" : "N") + "')";
                        cmd1 = con.CreateCommand();
                        cmd1.CommandText = Sql;
                        if (tr != null) cmd1.Transaction = tr;
                        cmd1.ExecuteNonQuery();
                        cmd1.Dispose();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("Error" + ee);
                        //if (ee.Message.Contains("unique constraint"))
                        //{
                        //    cmd1 = con.CreateCommand();
                        //    cmd1.CommandText = "delete from tran_masterUpdate where CODE='" + Doc_No + "' AND MODE_fag='" + MODE_A_E_D + "'";
                        //    if (tr != null) cmd1.Transaction = tr;
                        //    int R = cmd1.ExecuteNonQuery();
                        //    cmd1.Dispose();
                        //    if (R > 0) goto re;
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (cmd1 != null) cmd1.Dispose();
            }
            return true;
        }



        public bool deleteTable(string strTable, string strWhereClause, out int Rows_Affected, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            bool bReturn = false;
            Rows_Affected = -1;
            string strOracle;
            OracleCommand oraCommand = null;
            if (strWhereClause != null)
            {
                strOracle = "DELETE FROM " + strTable.Trim() + " WHERE " + strWhereClause.Trim();
            }
            else
            {
                strOracle = "DELETE FROM " + strTable.Trim();
            }
            try
            {
                oraCommand = new OracleCommand(strOracle, oraConn);
                if (oraTrns != null) oraCommand.Transaction = oraTrns;
                Rows_Affected = oraCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            finally
            {
                if (oraCommand != null) oraCommand.Dispose();
            }
            return false;
        }


        public bool deleteTable(string strTable, string strWhereClause, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            bool bReturn = false;
            string strOracle;

            if (strWhereClause != null)
            {
                strOracle = "DELETE FROM " + strTable.Trim() + " WHERE " + strWhereClause.Trim();
            }
            else
            {
                strOracle = "DELETE FROM " + strTable.Trim();
            }
            OracleCommand oraCommand = null;
            try
            {
                oraCommand = new OracleCommand(strOracle, oraConn);
                oraCommand.Transaction = oraTrns;
                oraCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            finally
            {
                if (oraCommand != null) oraCommand.Dispose();
            }
            return false;
        }

        public bool deleteTable(string strTable, string strWhereClause, OracleConnection oraConn)
        {
            bool bReturn = false;
            string strOracle;
            if (strWhereClause != null)
            {
                strOracle = "DELETE FROM " + strTable.Trim() + " WHERE " + strWhereClause.Trim();
            }
            else
            {
                strOracle = "DELETE FROM " + strTable.Trim();
            }
            OracleCommand oraCommand = null;
            try
            {
                oraCommand = new OracleCommand(strOracle, oraConn);
                oraCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());

            }
            finally
            {
                if (oraCommand != null) oraCommand.Dispose();
            }
            return false; ;
        }
        //..................................... Delete method.................

        //......................................Execute Command.................
        public bool ExecuteCmd(string strOracle, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            bool bReturn = false;
            OracleCommand oraCommand = null;
            try
            {
                oraCommand = new OracleCommand(strOracle, oraConn);
                oraCommand.Transaction = oraTrns;
                int D = oraCommand.ExecuteNonQuery();
                return true;

            }
            catch (Exception exp)
            {
                throw;

            }
            finally
            {
                if (oraCommand != null)
                {
                    oraCommand.Dispose();
                }
            }
            return false;
        }
        public bool ExecuteCmd(string strOracle, out long Rows_Affected, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            Rows_Affected = -1;
            bool bReturn = false;
            OracleCommand oraCommand = null;
            try
            {
                oraCommand = new OracleCommand(strOracle, oraConn);
                oraCommand.Transaction = oraTrns;
                Rows_Affected = oraCommand.ExecuteNonQuery();
                return true;

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());

            }
            finally
            {
                if (oraCommand != null) oraCommand.Dispose();
            }
            return false;
        }
        public bool ExecuteCmd(string strOracle, out  int Rows_Affected, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            Rows_Affected = -1;
            bool bReturn = false;
            OracleCommand oraCommand = null;
            try
            {
                oraCommand = new OracleCommand(strOracle, oraConn);
                if (oraTrns != null) oraCommand.Transaction = oraTrns;
                Rows_Affected = oraCommand.ExecuteNonQuery();
                return true;

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());

            }
            finally
            {
                if (oraCommand != null) oraCommand.Dispose();
            }
            return false;
        }

        public bool ExecuteCmd(string strOracle, OracleConnection oraConn)
        {
            bool bReturn = false;
            OracleCommand oraCommand = null;
            try
            {
                oraCommand = new OracleCommand(strOracle, oraConn);
                oraCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());

            }
            finally
            {
                if (oraCommand != null) oraCommand.Dispose();
            }

            return false;
        }






        // Data base properties........... using Database connection....................



        public bool Exists(string strTable, string strFieldName, string strWhereClause, OracleConnection oraConn)
        {
            string strResult = "", strResult1 = "", strResult2 = "", strResult3 = "";
            return Exists(strTable, strFieldName, strWhereClause, strFieldName, 1, out strResult, out strResult1, out strResult2, out  strResult3, oraConn);
        }

        public bool Exists(string strTable, string strFieldName, string strWhereClause, string strOrderBy, OracleConnection oraConn)
        {
            string strResult = "", strResult1 = "", strResult2 = "", strResult3 = "";
            return Exists(strTable, strFieldName, strWhereClause, strOrderBy, 1, out strResult, out strResult1, out strResult2, out  strResult3, oraConn);
        }

        public bool Exists(string strTable, string strFieldName, string strWhereClause, string strOrderBy, out string strResult, OracleConnection oraConn)
        {
            string strResult1 = "", strResult2 = "", strResult3 = ""; ;
            return Exists(strTable, strFieldName, strWhereClause, strOrderBy, 1, out strResult, out strResult1, out strResult2, out  strResult3, oraConn);
        }

        public bool Exists(string strTable, string strFieldName, string strWhereClause, string strOrderBy, short NoOfArgs, out string strResult, out string strResult1, OracleConnection oraConn)
        {
            string strResult2 = "", strResult3 = ""; ;
            return Exists(strTable, strFieldName, strWhereClause, strOrderBy, 2, out strResult, out strResult1, out strResult2, out  strResult3, oraConn);
        }
        public bool Exists(string strTable, string strFieldName, string strWhereClause, string strOrderBy, short NoOfArgs, out string strResult, out string strResult1, out string strResult2, OracleConnection oraConn)
        {
            string strResult3 = "";
            return Exists(strTable, strFieldName, strWhereClause, strOrderBy, 3, out strResult, out strResult1, out strResult2, out  strResult3, oraConn);
        }


        public bool Exists(string strTable, string strFieldName, string strWhereClause, string strOrderBy, short NoOfArgs, out string strResult, out string strResult1, out string strResult2, out string strResult3, OracleConnection oraConn)
        {
            string strOracle;
            strResult = "";
            strResult1 = "";
            strResult2 = "";
            strResult3 = "";

            strOracle = "SELECT " + strFieldName + " FROM " + strTable;
            if (!strWhereClause.Equals("")) strOracle = strOracle + " WHERE " + strWhereClause;
            if (!strOrderBy.Equals("")) strOracle = strOracle + " ORDER BY " + strOrderBy;

            OracleDataReader oraReader = null;
            OracleCommand oraCommand = new OracleCommand(strOracle, oraConn);
            try
            {
                oraReader = oraCommand.ExecuteReader();

                if (oraReader.Read())
                {
                    if (NoOfArgs == 1)
                    {
                        strResult = oraReader[0].ToString();
                    }
                    if (NoOfArgs == 2)
                    {
                        strResult = oraReader[0].ToString();
                        strResult1 = oraReader[1].ToString();
                    }
                    if (NoOfArgs == 3)
                    {
                        strResult = oraReader[0].ToString();
                        strResult1 = oraReader[1].ToString();
                        strResult2 = oraReader[2].ToString();
                    }
                    if (NoOfArgs == 4)
                    {
                        strResult = oraReader[0].ToString();
                        strResult1 = oraReader[1].ToString();
                        strResult2 = oraReader[2].ToString();
                        strResult3 = oraReader[3].ToString();
                    }

                    return true;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
                return false;

            }
            finally
            {
                oraReader.Close();
                oraCommand.Dispose();
            }
            return false;
            // ;
        }
        public string GetValue(string strTable, string strFieldName, string strWhereClause, string strOrderBy, OracleConnection orac, OracleTransaction tr)
        {
            string x, y, z;
            return GetValue(strTable, strFieldName, strWhereClause, strOrderBy, 1, out x, out y, out z, orac, tr);
        }
        public string GetValue(string strTable, string strFieldName, string strWhereClause, string strOrderBy, short NoOfArgs, out string strResult, out string strResult1, out string strResult2, OracleConnection oraConn, OracleTransaction tr)
        {
            string strOracle;
            strResult = "";
            strResult1 = "";
            strResult2 = "";

            strOracle = "SELECT " + strFieldName + " FROM " + strTable;
            if (!strWhereClause.Equals("")) strOracle = strOracle + " WHERE " + strWhereClause;
            if (!strOrderBy.Equals("")) strOracle = strOracle + " ORDER BY " + strOrderBy;

            OracleDataReader oraReader = null;
            OracleCommand oraCommand = new OracleCommand(strOracle, oraConn);
            if (tr != null) oraCommand.Transaction = tr;
            try
            {


                oraReader = oraCommand.ExecuteReader();

                if (oraReader.Read())
                {
                    if (NoOfArgs == 1)
                    {
                        strResult = oraReader[0].ToString();
                    }
                    if (NoOfArgs == 2)
                    {
                        strResult = oraReader[0].ToString();
                        strResult1 = oraReader[1].ToString();
                    }
                    if (NoOfArgs == 3)
                    {
                        strResult = oraReader[0].ToString();
                        strResult1 = oraReader[1].ToString();
                        strResult2 = oraReader[2].ToString();
                    }
                    return strResult;
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
            finally
            {
                if (oraReader != null) oraReader.Close();
                oraCommand.Dispose();
            }

            return "";
            // ;
        }

        public OracleDataReader GetValue(OracleConnection oraConn, OracleTransaction tr, string Query)
        {
            OracleDataReader oraReader = null;
            OracleCommand oraCommand = new OracleCommand(Query, oraConn);
            if (tr != null) oraCommand.Transaction = tr;
            try
            {
                oraReader = oraCommand.ExecuteReader();
                return oraReader;
            }
            catch (Exception exp)
            {
                throw;
            }
            finally
            {
                //  if (oraReader != null) oraReader.Close();
                oraCommand.Dispose();
            }

            return null;
            // ;
        }

        public string GetAcctOpening(string Account, string Date, OracleConnection oCon, OracleTransaction Tr)
        {
            Date = DateTime.Parse(Date).ToString("dd/MMM/yyyy");
            string Sql = "";
            Sql = "Select Sum(LC_DEBIT)-Sum(LC_CREDIT) As OPENINGBALANCE From ACCT_TRANSACTIONS" +
                  " Where ACCT_CODE='" + Account + "' And DOC_DATE < TO_DATE('" + Date + "','DD/MON/YYYY')";
            string Opening = this.GetValue(Sql, oCon, Tr);
            if (Opening.Length <= 0) Opening = "0";
            return Opening;
        }
        public string GetAcctClosing(string Account, string Date, OracleConnection oCon, OracleTransaction Tr)
        {
            Date = DateTime.Parse(Date).ToString("dd/MMM/yyyy");
            string Sql = "";
            Sql = "Select Sum(LC_DEBIT)-Sum(LC_CREDIT) As CLOSINGBALANCE From ACCT_TRANSACTIONS" +
                  " Where ACCT_CODE='" + Account + "' And DOC_DATE <= TO_DATE('" + Date + "','DD/MON/YYYY')";
            string Closing = this.GetValue(Sql, oCon, Tr);
            if (Closing.Length <= 0) Closing = "0";
            return Closing;
        }

        public string GetBankReconNarration(string Doc_Number, OracleConnection oCon, OracleTransaction Tr)
        {
            string Sql = "";
            Sql = "Select (NARRATION||' Chq Details : '||CHEQUE_NO ||' - '||to_char(CHEQUE_DATE,'DD/MON/YYYY')) As NARRATION" +
                  " From ACCT_TRANSACTIONS Where DOC_NO='" + Doc_Number + "' And ACCT_GROUP='BANK'";
            return this.GetValue(Sql, oCon, Tr);
        }

        //byt ABU  ON APRIL 22

        /*public string getSupplierCreditPeriod(string stSupplier, OracleConnection oCon)
        {
            OracleCommand oCmd = null;
            OracleDataReader oRs = null;
            try
            {
                string stCreditLimit = "0";
                oCmd = oCon.CreateCommand();
                //string oSql = "Select CREDDAYLIMIT,CREDAMTLIMIT From COMN_SUBSIDIARYMASTER Where SUBSIDIARY_CODE = '" + txtSupplierCode.Text + "' And SUBSIDIARY_TYPE_CODE NOT  IN ('02','06')";
                string oSql = "Select CREDDAYLIMIT,CREDAMTLIMIT From COMN_SUBSIDIARYMASTER Where SUBSIDIARY_CODE='" + stSupplier + "' And SUBSIDIARY_CATEGORY='SUPPLIER'";
                oCmd.CommandText = oSql;
                oRs = oCmd.ExecuteReader();
                if (!(oRs.HasRows)) { return stCreditLimit = "0"; }
                if (oRs.Read())
                {
                    stCreditLimit = oRs["CREDDAYLIMIT"].ToString();
                }
                return stCreditLimit;
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error : \n\r" + Exp, "E", "");
                return "0";
            }
            finally
            {
                oRs.Dispose();
                oCmd.Dispose();
            }
        }*/
        public bool getSupplierCreditPeriod(string stSupplier, OracleConnection oCon, out string creditPeriod, out decimal rebate)
        {
            creditPeriod = "0";
            rebate = 0;
            OracleCommand oCmd = null;
            OracleDataReader oRs = null;
            try
            {

                oCmd = oCon.CreateCommand();
                //string oSql = "Select CREDDAYLIMIT,CREDAMTLIMIT From COMN_SUBSIDIARYMASTER Where SUBSIDIARY_CODE = '" + txtSupplierCode.Text + "' And SUBSIDIARY_TYPE_CODE NOT  IN ('02','06')";
                string oSql = "Select CREDDAYLIMIT,REBATE From COMN_SUBSIDIARYMASTER Where SUBSIDIARY_CODE='" + stSupplier + "' And SUBSIDIARY_CATEGORY='SUPPLIER'";
                oCmd.CommandText = oSql;
                oRs = oCmd.ExecuteReader();
                //if (!(oRs.HasRows)) { 
                //    return stCreditLimit = "0"; }
                if (oRs.Read())
                {
                    creditPeriod = oRs["CREDDAYLIMIT"].ToString();
                    rebate = decimal.Parse(oRs["REBATE"].ToString());
                }
                else
                {
                    creditPeriod = "0";
                    rebate = 0;
                    return false;
                }
                return true;
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error : \n\r" + Exp, "E", "");
                return false;
            }
            finally
            {
                oRs.Dispose();
                oCmd.Dispose();
            }
        }
        public bool GetCreditDate(string Supplier, OracleConnection oCon, out string StartDate, out string EndDate)
        {
            OracleCommand oCmd = null;
            OracleDataReader oRs = null;
            try
            {
                StartDate = "";
                EndDate = "";
                oCmd = oCon.CreateCommand();
                oCmd.CommandText = "Select FROM_DATE,TO_DATE From CON_MASTER" +
                  " Where SUBSIDIARY_CODE='" + Supplier + "'";
                oRs = oCmd.ExecuteReader();
                if (oRs.Read())
                {
                    StartDate = oRs["FROM_DATE"].ToString();
                    EndDate = oRs["TO_DATE"].ToString();

                }

                return true;

            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error : \n\r" + Exp, "E", "");
                StartDate = "";
                EndDate = "";
                return false;
            }
            finally
            {
                oRs.Dispose();
                oCmd.Dispose();
            }

        }

        //new function added on april 19 by abu
        public string GetOpening(string Account, string Date, string Location, OracleConnection oCon, OracleTransaction Tr)
        {
            Date = DateTime.Parse(Date).ToString("dd/MMM/yyyy");
            string Sql = "";
            Sql = "Select Sum(LC_DEBIT)-Sum(LC_CREDIT) As OPENINGBALANCE From ACCT_TRANSACTIONS" +
                  " Where ACCT_CODE='" + Account + "' and Loc_code='" + Location + "' And DOC_DATE < TO_DATE('" + Date + "','DD/MON/YYYY')";
            string Opening = GetValue(Sql, oCon, Tr);
            if (Opening.Length <= 0) Opening = "0";
            return Opening;
        }

        //new function added on July 17 by DD
        public string GetGroupOpening(string GroupCode, string Date, string Location, OracleConnection oCon, OracleTransaction Tr)
        {
            Date = DateTime.Parse(Date).ToString("dd/MMM/yyyy");
            string Sql = "";
            Sql = "Select Sum(LC_DEBIT)-Sum(LC_CREDIT) As OPENINGBALANCE From ACCT_TRANSACTIONS" +
                  " Where ACCT_GROUP='" + GroupCode + "' and Loc_code='" + Location + "' And DOC_DATE < TO_DATE('" + Date + "','DD/MON/YYYY')";
            string Opening = GetValue(Sql, oCon, Tr);
            if (Opening.Length <= 0) Opening = "0";
            return Opening;
        }
        //new code added by krp on april 24

        public string GetGroupedLocations(string GroupLoc_Code, OracleConnection con, OracleTransaction tr)
        {
            string rLoc_Code = "";
            string Sql = "select loc_code from comn_location where group_loc='" + GroupLoc_Code + "'";
            OracleDataReader rs = null;
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = Sql;
            try
            {
                if (tr != null) cmd.Transaction = tr;
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    rLoc_Code = rLoc_Code.Length > 0 ? rLoc_Code + "," : "";
                    rLoc_Code = rLoc_Code + "'" + rs["loc_code"].ToString() + "'";
                }
            }
            catch { throw; }
            finally
            {
                if (rs != null) rs.Close();
                cmd.Dispose();
            }
            if (rLoc_Code.Length == 0) throw (new Exception("Invalid Payment Location"));
            return rLoc_Code;
        }


        public string GetValue(string Query, OracleConnection oraConn, OracleTransaction tr)
        {
            string strOracle = Query;

            OracleDataReader oraReader = null;
            OracleCommand oraCommand = new OracleCommand(strOracle, oraConn);
            if (tr != null) oraCommand.Transaction = tr;
            try
            {
                oraReader = oraCommand.ExecuteReader();
                if (oraReader.Read())
                {
                    return oraReader[0].ToString();
                }
                return "";
            }
            catch (Exception exp)
            {
                throw (exp);// MessageBox.Show(exp.ToString());
                //return "";
            }
            finally
            {
                if (oraReader != null) oraReader.Close();
                oraCommand.Dispose();
            }

            // ;
        }

        public long GetSequence(string Table_SEQ_Name, OracleConnection con, OracleTransaction Tr)
        {
            string sql = "select " + Table_SEQ_Name + ".nextval from dual";
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                cmd = con.CreateCommand();
                if (Tr != null) cmd.Transaction = Tr;
                cmd.CommandText = sql;
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    return long.Parse(rs.GetOracleNumber(0).ToString());
                }
            }
            catch { }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (rs != null) rs.Close();
            }
            return 0;
        }
        public string GetRebateAcct(string Name, OracleConnection con, OracleTransaction Tr)
        {
            OracleCommand cmd = null;

            OracleDataReader Rs = null;
            try
            {
                string Sql = "select Acct_code from con_contracttype where contractname='" + Name + "'";
                cmd = con.CreateCommand();
                if (Tr != null) cmd.Transaction = Tr;
                cmd.CommandText = Sql;
                Rs = cmd.ExecuteReader();
                if (Rs.Read())
                {
                    return Rs[0].ToString();
                }


            }
            catch { }
            finally { if (Rs != null)Rs.Close(); cmd.Dispose(); }
            return "";
        }



        // WITH TRANSACTION NEW CODE.....
        public bool Exists(string strTable, string strFieldName, string strWhereClause, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            string strResult = "", strResult1 = "", strResult2 = "";
            //                return Exists(strTable, strFieldName, strWhereClause, strGroupBy, 1, out strResult, out strResult1, out strResult2);
            return Exists(strTable, strFieldName, strWhereClause, strFieldName, 1, out strResult, out strResult1, out strResult2, oraConn, oraTrns);
        }

        public bool Exists(string strTable, string strFieldName, string strWhereClause, string strOrderBy, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            string strResult = "", strResult1 = "", strResult2 = "";
            //string strGroupBy = "";
            return Exists(strTable, strFieldName, strWhereClause, strOrderBy, 1, out strResult, out strResult1, out strResult2, oraConn, oraTrns);
        }

        public bool Exists(string strTable, string strFieldName, string strWhereClause, string strOrderBy, out string strResult, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            string strResult1 = "", strResult2 = "";
            return Exists(strTable, strFieldName, strWhereClause, strOrderBy, 1, out strResult, out strResult1, out strResult2, oraConn, oraTrns);
        }

        public bool Exists(string strTable, string strFieldName, string strWhereClause, string strOrderBy, short NoOfArgs, out string strResult, out string strResult1, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            string strResult2 = "";
            return Exists(strTable, strFieldName, strWhereClause, strOrderBy, 2, out strResult, out strResult1, out strResult2, oraConn, oraTrns);
        }
        public bool Exists(string strTable, string strFieldName, string strWhereClause, string strOrderBy, short NoOfArgs, out string strResult, out string strResult1, out string strResult2, OracleConnection oraConn, OracleTransaction oraTrns)
        {
            // OracleConnection oraConn;
            //OracleCommand oraCommand = new OracleCommand();
            //OracleDataReader oraReader = new OracleDataReader();
            string strOracle;
            strResult = "";
            strResult1 = "";
            strResult2 = "";

            strOracle = "SELECT " + strFieldName + " FROM " + strTable;
            if (!strWhereClause.Equals("")) strOracle = strOracle + " WHERE " + strWhereClause;
            //if (!strGroupBy.Equals("")) strOracle = strOracle + "GROUP BY " + strGroupBy;
            if (!strOrderBy.Equals("")) strOracle = strOracle + " ORDER BY " + strOrderBy;

            // OracleConnection oraConn;
            OracleDataReader oraReader = null;
            OracleCommand oraCommand = new OracleCommand(strOracle, oraConn);
            if (oraTrns != null) oraCommand.Transaction = oraTrns;
            try
            {
                //;
                oraReader = oraCommand.ExecuteReader();

                //oraReader = oraCommand.ExecuteReader();

                if (oraReader.Read())
                {
                    if (NoOfArgs == 1)
                    {
                        strResult = oraReader[0].ToString();//.GetValue(0).ToString();
                        //strResult = (oraReader.GetString(0).ToString()); //  CHECK HERe VALUE AND STRING/////////////
                    }
                    if (NoOfArgs == 2)
                    {
                        strResult = oraReader[0].ToString();//.GetValue(0).ToString());
                        strResult1 = oraReader[1].ToString();//.GetString(1).ToString());
                    }
                    if (NoOfArgs == 3)
                    {
                        strResult = oraReader[0].ToString();//.GetString(0).ToString());
                        strResult1 = oraReader[1].ToString();//.GetString(1).ToString());
                        strResult2 = oraReader[2].ToString();//.GetString(2).ToString());
                    }
                    return true;
                }
            }
            catch (Exception exp)
            {
                throw;
                //  throw exp;
                // ................Optional
                //Logger.Log("Error in Function Exists : " + exp.ToString() + ". The SQL :" + strOracle);
                // ;                    
            }
            finally
            {
                if (oraReader != null) oraReader.Close();
                if (oraCommand != null) oraCommand.Dispose();
            }
            return false;
            // ;
        }

        // FAX

        public int SendFax(string DocumentName, string FileName, string RecipientName, string FaxNumber)
        {
            if (FaxNumber != "")
            {
                try
                {
                    FAXCOMLib.FaxServer faxServer = new FAXCOMLib.FaxServerClass();
                    faxServer.Connect(Environment.MachineName);
                    FAXCOMLib.FaxDoc faxDoc = (FAXCOMLib.FaxDoc)faxServer.CreateDocument(FileName);
                    faxDoc.RecipientName = RecipientName;
                    faxDoc.FaxNumber = FaxNumber;
                    faxDoc.DisplayName = DocumentName;
                    int Response = faxDoc.Send();
                    faxServer.Disconnect();
                    return Response;
                }
                catch (Exception Ex) { MessageBox.Show(Ex.Message); }
            }
            return 0;
        }

        // E-mail

        public bool SendEmail(string Address, string FromAddr, string Subject, string BodyText, string Attachments, string pwd, bool viaGoogleApi, string Def_MailID_From)
        {
            if (viaGoogleApi)
            {
                try
                {
                    string tempToAddress = Address;
                    string tempFromAddr = Def_MailID_From == "" ? FromAddr : Def_MailID_From;
                    string tempPwd = pwd;
                    bool hasAttachment = Attachments == "" ? false : true;
                    string tempAttachments = Attachments;
                    if (tempToAddress.Contains(","))
                    {
                        string[] Address1 = tempToAddress.Split(',');
                        foreach (string st in Address1)
                        {
                            tempToAddress = st;
                            _emailLibrary.SendEmail(tempFromAddr, tempPwd, tempToAddress, "", Subject, BodyText, hasAttachment, tempAttachments);
                        }
                    }
                    else
                    {
                        _emailLibrary.SendEmail(tempFromAddr, tempPwd, tempToAddress, "", Subject, BodyText, hasAttachment, tempAttachments);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }
            else
            {
                #region old mail sending

                string Password = "Saf#PRH*170ari", smtp = "smtp.gmail.com";
                int port = 587; bool SSL = true, Outlook1st = true, DefaultCred = false;

                string sfrom = "";
                if (FromAddr == "") sfrom = "itsupport@safarigroup.net";
                else
                    sfrom = FromAddr;

                if (pwd != "") Password = pwd;

                if (Message("Send From Outlook?", "", "Q", MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    LoadMailSettings(FromAddr, out Password, out smtp, out port, out SSL, out Outlook1st, out Outlook1st);
                    if (Outlook1st && SendMailOutLook(Address, FromAddr, Subject, BodyText, Attachments)) return true;
                    else Message("Sending from outlook failed!!! " + Outlook1st + ":" + Address + ":" + FromAddr + ":" + Subject);
                }


                bool ErrSend = false;
                try
                {
                    char[] ch = { ',' };
                    string[] Address1 = Address.Split(',');
                    foreach (string st in Address1)
                    {
                        Address = st;

                        MailAddress sto = new MailAddress(Address);

                        MailMessage ms;
                        if (!(BodyText == "") || !(Subject == ""))
                        {
                            ms = new MailMessage(sfrom, Address, Subject, BodyText);
                        }
                        else
                        {
                            ms = new MailMessage(sfrom, Address);
                        }
                        if (!(Attachments == ""))
                        {
                            Attachment att = new Attachment(Attachments);
                            ms.Attachments.Add(att);
                        }

                        ms.IsBodyHtml = true;
                        ms.Priority = MailPriority.High;
                        SmtpClient smail = new SmtpClient(smtp);
                        smail.Port = port;
                        smail.EnableSsl = SSL;
                        smail.Timeout = 25000;
                        smail.UseDefaultCredentials = DefaultCred;
                        smail.Credentials = new NetworkCredential(sfrom, Password);
                        smail.Send(ms);

                    }
                    return true;
                }
                catch (Exception ex) { Message("Error Sending " + ex.ToString()); ErrSend = true; }

                if (ErrSend)
                {
                    try
                    {
                        char[] ch = { ',' };
                        string[] Address1 = Address.Split(',');
                        foreach (string st in Address1)
                        {
                            Address = st;

                            MailAddress sto = new MailAddress(Address);

                            if (FromAddr == "") sfrom = "safariautorecover@gmail.com";
                            else
                                sfrom = FromAddr;

                            MailMessage ms;
                            if (!(BodyText == "") || !(Subject == ""))
                            {
                                ms = new MailMessage(sfrom, Address, Subject, BodyText);
                            }
                            else
                            {
                                ms = new MailMessage(sfrom, Address);
                            }
                            if (!(Attachments == ""))
                            {
                                Attachment att = new Attachment(Attachments);
                                ms.Attachments.Add(att);
                            }
                            ms.IsBodyHtml = true;
                            ms.Priority = MailPriority.High;
                            SmtpClient smail = new SmtpClient("smtp.gmail.com");
                            //                     SmtpClient smail = new SmtpClient("smtpout.secureserver.net");
                            smail.Port = 587;
                            //                     smail.Port = 465;
                            smail.Timeout = 25000;
                            smail.EnableSsl = true;
                            // smail.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smail.UseDefaultCredentials = false;

                            smail.Credentials = new NetworkCredential("purchase@safarigroup.net", "Saf#PRH*170ari");
                            smail.Send(ms);
                        }

                        return true;
                    }
                    catch (Exception ex) { Message("Error Sending " + ex.ToString()); }
                }

                return false;
                #endregion
            }
        }

        private bool LoadMailSettings(string Address, out string Password, out string smtp, out int port, out bool SSL, out bool Outlook1st, out  bool DefaultCred)
        {
            port = 587;
            smtp = "smtp.gmail.com";
            Password = "Safari159";
            SSL = true;
            Outlook1st = true;
            DefaultCred = false;
            try
            {


                OracleDataReader rs = GetValue(Defaults.Def_Conn, null, "select * from SYS_MAIL_SETTINGS where email='" + Address + "'");
                if (rs.HasRows)
                {
                    if (rs.Read())
                    {
                        port = int.Parse(rs["SSL_PORT"].ToString());
                        smtp = rs["smtp"].ToString();
                        Password = rs["password"].ToString();
                        SSL = rs["SSL"].ToString() == "Y" ? true : false;
                        Outlook1st = rs["OUTLOOK1ST"].ToString() == "Y" ? true : false;
                        DefaultCred = rs["DEFAULT_CRED"].ToString() == "Y" ? true : false;
                        return true;
                    }

                    else return false;
                }
                else
                {
                    throw new Exception("Email setting is missing please contact IT");
                }

            }
            catch (Exception EX)
            {
                DataConnector.Message(EX.ToString(), "E", "SGC");
                return false;
            }
        }



        public bool SendMailOutLook(string Address, string FromAddr, string Subject, string BodyText, string Attachments)
        {
            try
            {
                Outlook.Application oApp = new Outlook.Application();
                Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                oMsg.HTMLBody = BodyText;
                String sDisplayName = "FindAttachment";
                int iPosition = (int)oMsg.Body.Length + 1;
                int iAttachType = (int)Outlook.OlAttachmentType.olByValue;
                Outlook.Attachment oAttach = oMsg.Attachments.Add(Attachments, iAttachType, iPosition, sDisplayName);
                oMsg.Subject = Subject;
                Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;

                char[] ch = { ',' };
                string[] Address1 = Address.Split(',');
                Outlook.Recipient oRecip = null;
                foreach (string st in Address1)
                {
                    if (st.Length > 4 && st.Contains("@") && st.Contains("."))
                    {
                        try
                        {
                            oRecip = (Outlook.Recipient)oRecips.Add(st);
                            oRecip.Resolve();
                        }
                        catch { }
                    }
                }

                oMsg.Save();
                oMsg.Send();
                // Clean up.
                oRecip = null;
                oRecips = null;
                oMsg = null;
                oApp = null;
                return true;
            }//end of try block
            catch (Exception ex)
            {
                Message("Error!!!   " + ex.Message + ":" + ex.ToString());
                return false;
            }//end of catch
        }//end of Email Method

        public bool PrintReport(string RptName, string ExpPath, string ExpType, string qry, string database, string[] FormulaName, string[] FormulaValue, bool ExportOnly)
        {
            return (PrintReport(RptName, ExpPath, ExpType, qry, database, FormulaName, FormulaValue, ExportOnly, false, 1, 1, 1, false, false, false));
        }
        public bool PrintReport(string RptName, string ExpPath, string ExpType, string qry, string database, string[] FormulaName, string[] FormulaValue, bool ExportOnly, bool DISABLE_EXPORT)
        {
            return (PrintReport(RptName, ExpPath, ExpType, qry, database, FormulaName, FormulaValue, ExportOnly, false, 1, 1, 1, false, false, DISABLE_EXPORT));
        }
        public bool PrintReport(string RptName, string qry, string database, string[] FormulaName, string[] FormulaValue, bool printToPrinter, int NoOfCopies, int StartPage, int EndPage, bool collate)
        {
            return (PrintReport(RptName, "", "", qry, database, FormulaName, FormulaValue, false, printToPrinter, NoOfCopies, StartPage, EndPage, collate, false, false));
        }


        public static void ReportSourceSetup(ReportDocument crDoc, ConnectionInfo crConnectionInfo)
        {
            // Each table in report needs to have logoninfo setup:
            Tables crTables = crDoc.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table crTable in crTables)
            {
                TableLogOnInfo crTableLogonInfo = crTable.LogOnInfo;
                crTableLogonInfo.ConnectionInfo = crConnectionInfo;
                crTable.ApplyLogOnInfo(crTableLogonInfo);
            }
        }
        public bool PrintReport(string RptName, string ExpPath, string ExpType, string qry, string database, string[] FormulaName, string[] FormulaValue, bool ExportOnly, bool printToPrinter, int NoOfCopies, int StartPage, int EndPage, bool collate, bool ViewReport)
        {
            return (PrintReport(RptName, "", "", qry, database, FormulaName, FormulaValue, false, printToPrinter, NoOfCopies, StartPage, EndPage, collate, false, false));
        }
        public bool PrintReport(string RptName, string ExpPath, string ExpType, string qry, string database, string[] FormulaName, string[] FormulaValue, bool ExportOnly, bool printToPrinter, int NoOfCopies, int StartPage, int EndPage, bool collate, bool ViewReport, bool DISABLE_EXPORT)
        {
            ReportDocument cryRpt = new ReportDocument();
            try
            {
                DataBaseConAttributes dc = new DataBaseConAttributes(database);
                ConnectionInfo ci = new ConnectionInfo();
                ci.ServerName = dc.ODBC;
                ci.DatabaseName = dc.DatabaseName;
                ci.UserID = dc.USER;
                ci.Password = dc.PWD;
                cryRpt.Load(Defaults.Rep_Path + RptName);
                ReportSourceSetup(cryRpt, ci);
                //foreach (CrystalDecisions.Shared.IConnectionInfo connection in cryRpt.DataSourceConnections)
                //{
                //   connection.SetConnection(dc.ODBC , dc.DatabaseName ,dc.USER ,dc.PWD );
                //    connection.SetLogon(dc.USER , dc.PWD );
                //}
                cryRpt.VerifyDatabase();
                //
                //try
                //{
                //    cryRpt.DataDefinition.SortFields[0].SortDirection = SortDirection.AscendingOrder;
                //}
                //catch { }

                if (qry != "") cryRpt.RecordSelectionFormula = qry;

                int i = 0;
                while (FormulaName != null && i < FormulaName.Length)
                {
                    cryRpt.DataDefinition.FormulaFields[FormulaName.GetValue(i).ToString()].Text = "'" + FormulaValue.GetValue(i).ToString() + "';";
                    i++;
                }

                cryRpt.Refresh();
                //
                if (printToPrinter)
                {
                    cryRpt.PrintToPrinter(NoOfCopies, collate, StartPage, EndPage);
                    try
                    {
                        cryRpt.Close();
                        cryRpt.Dispose();
                    }
                    catch { }
                    if (!ViewReport) return true;
                }
                //
                if (!(ExpType == ""))
                {
                    if (ExpPath == "") ExpPath = Defaults.Exp_Path + RptName + "." + ExpType;
                    ExportOptions CrExportOptions;
                    DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    ExpType = ExpType.ToUpper();
                    if (ExpType == "PDF" || ExpType == "RTF" || ExpType == "DOC")
                    {
                        //ExcelFormatOptions CrFormatTypeOptions = new ExcelFormatOptions();
                        CrystalDecisions.Shared.PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                        CrDiskFileDestinationOptions.DiskFileName = ExpPath;
                        CrExportOptions = cryRpt.ExportOptions;
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;


                        if (ExpType == "DOC") CrExportOptions.ExportFormatType = ExportFormatType.WordForWindows;
                        else if (ExpType == "PDF") CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                        else if (ExpType == "RTF") CrExportOptions.ExportFormatType = ExportFormatType.RichText;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                        cryRpt.Export();//excel commended for testing only
                    }
                    else if (ExpType == "XLS")
                    {
                        CrystalDecisions.Shared.ExcelFormatOptions CrFormatTypeOptions = new ExcelFormatOptions();
                        CrDiskFileDestinationOptions.DiskFileName = ExpPath;
                        CrExportOptions = cryRpt.ExportOptions;
                        CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                        if (ExpType == "XLS")
                            CrExportOptions.ExportFormatType = ExportFormatType.Excel;
                        CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                        CrExportOptions.FormatOptions = CrFormatTypeOptions;
                        cryRpt.Export();//excel commended for testing only
                    }
                }
                if (ExportOnly)
                {
                    cryRpt.Close();
                    cryRpt.Dispose();
                    return true;
                }
                ///cryRpt.
                frmReportForm rp = null;
                if (DISABLE_EXPORT)
                    rp = new frmReportForm(cryRpt, true);
                else
                    rp = new frmReportForm(cryRpt);
                rp.Show();
            }
            catch (Exception ex)
            {
                if (cryRpt != null)
                {
                    try
                    {
                        cryRpt.Close();
                        cryRpt.Dispose();
                    }
                    catch { }
                } throw;
            }
            finally
            {

            }
            return true;
        }



        // ADD COMBO

        // for  ADDING
        public double RoundDhs(double Amount)
        {
            double x1 = 0;
            x1 = Math.Round(Amount - (int)Amount, 2);
            if (x1 == 0 && Math.Round(Amount - (int)Amount, 3) != 0) x1 = 0.01;
            if (x1 == 0) return (int)Amount;
            int Amt = (int)Amount;
            int x = (int)(x1 * 100);

            if (x <= 25)
            {
                return (((double)Amt * 100) + 25) / 100;
            }
            else if (x > 25 && x <= 50)
            {
                return (((double)Amt * 100) + 50) / 100;
            }
            else if (x > 50 && x <= 75)
            {
                return (((double)Amt * 100) + 75) / 100;
            }
            else
            {
                return Amt + 1;
            }
        }
        //
        // ADD TO CONTRACT
        public string IsItemExisInConrtact(string Part_Number, OracleConnection con, OracleTransaction tr)
        {
            return IsItemExisInConrtact(Part_Number, "", con, tr);
        }
        //public string  IsDivisionExisInConrtact(string supplier_code,string DivisionCode , OracleConnection con, OracleTransaction tr)
        //{
        //  string s = "",s1="";
        //  string sql = "select distinct a.contract_no from con_rebatemaster a inner join con_supdivision b on a.contract_no=b.contract_no where a.subsidiary_code='" + supplier_code + "' and division_code='" + DivisionCode + "' and closed='N' and cancelled='N'";
        //  if (Exists(sql, 1, out s, con, tr))
        //  {
        //      return s;
        //  }
        //  return "";
        //}

        public OracleDataReader IsDivisionExisInConrtact(string supplier_code, string DivisionCode, OracleConnection con, OracleTransaction tr)
        {
            string s = "", s1 = "";
            OracleDataReader rs = null;
            OracleCommand cmd = null;
            try
            {
                string sql = "select distinct a.contract_no from con_rebatemaster a inner join con_supdivision b on a.contract_no=b.contract_no where a.subsidiary_code='" + supplier_code + "' and division_code='" + DivisionCode + "' and closed='N' and cancelled='N'";
                cmd = con.CreateCommand();
                cmd.CommandText = sql;
                if (tr != null) cmd.Transaction = tr;
                return rs = cmd.ExecuteReader();
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
        }



        public string IsItemExisInConrtact(string Part_Number, string Supplier_Code, OracleConnection con, OracleTransaction tr)
        {
            string sq = " supplier_code='" + Supplier_Code + "' and ";
            string s = "", s1 = "";
            if (Supplier_Code == "") sq = "";
            string sql = "select distinct a.contract_no from con_rebatemaster a inner join con_rebateitem b on a.contract_no=b.contract_no where " + sq + " a.part_number='" + Part_Number + "'";
            if (Exists(sql, 1, out s, out s1, con, tr))
            {
                return s;
            }
            return "";
        }

        //public bool AddItemToContract(string Supplier_Code, string Division_Code, string Category_Code, string Brand_Code, string Item_Code, out string Con_No, OracleConnection con, OracleTransaction tr)
        //{
        //    Con_No=IsDivisionExisInConrtact(Supplier_Code, Division_Code, con, tr);
        //    if (Con_No!="")
        //    {
        //        string x=GetValue("select max(slno) from con_rebatemaster a inner join con_rebateitem b on a.contract_no=b.contract_no where a.subsidiary_code='" + Supplier_Code + "' and division_code='" + Division_Code + "'",con,tr  );
        //        if(x=="") x="0";
        //        if (!insertTable("con_rebateitem", "CONTRACT_NO,SLNO,REBATE_PART_NUMBER_SUP,REBATE_ITEM_FLAG,CATEGORY_CODE,BRAND_CODE,DIVISION_CODE", "'" + Con_No + "'," + (int.Parse(x)+1) + ",'" + Item_Code + "','D','" + Category_Code + "','" + Brand_Code + "','" + Division_Code + "'", con, tr))
        //        {
        //            if (DataConnector.Message("Item Not Added To Contract " + Item_Code + "\n\r Want To UNDO ALL CHANGES?", "", "Q", MessageBoxDefaultButton.Button1) == DialogResult.Yes)
        //            {
        //                return false;
        //            }
        //        }                
        //    }
        //    return true;
        //}

        public bool AddItemToContract(string Supplier_Code, string Division_Code, string Category_Code, string Brand_Code, string Item_Code, out string Con_No, OracleConnection con, OracleTransaction tr)
        {
            Con_No = "";
            OracleDataReader rs = null;
            rs = IsDivisionExisInConrtact(Supplier_Code, Division_Code, con, tr);
            if (rs.HasRows)
            {
                Con_No = "";
                while (rs.Read())
                {
                    string x = GetValue("select max(slno) from con_rebatemaster a inner join con_rebateitem b on a.contract_no=b.contract_no where a.subsidiary_code='" + Supplier_Code + "' and b.contract_no='" + rs["contract_no"].ToString() + "' and division_code='" + Division_Code + "'", con, tr);
                    if (x == "") x = "0";
                    try
                    {
                        if (!insertTable("con_rebateitem", "CONTRACT_NO,SLNO,REBATE_PART_NUMBER_SUP,REBATE_ITEM_FLAG,CATEGORY_CODE,BRAND_CODE,DIVISION_CODE", "'" + rs["contract_no"].ToString() + "'," + (int.Parse(x) + 1) + ",'" + Item_Code + "','D','" + Category_Code + "','" + Brand_Code + "','" + Division_Code + "'", con, tr))
                        {
                            if (DataConnector.Message("Item Not Added To Contract " + Item_Code + "\n\r Want To UNDO ALL CHANGES?", "", "Q", MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                return false;
                            }
                        }
                        Con_No = Con_No + "," + rs["contract_no"].ToString();
                    }
                    catch (Exception Exp)
                    {
                        if (!(Exp.Message.Contains("ORA-00001: unique constraint (MALL.IDX_CONREBITEMPKEY) violated")))
                        {
                            throw (Exp);
                        }
                    }
                }
            }
            return true;
        }

        public void AddCombo(string TableName, string FieldName, ComboBox ComboName, string Condition,
            string OrderField, Boolean KeepValue, Boolean onlyDistinct, OracleConnection con)
        {
            String strOrder;
            String oldVal = ComboName.Text;

            if (KeepValue.Equals("1"))
            {
                oldVal = ComboName.Text;
            }
            string Distinct = "";
            if (onlyDistinct)
            {
                Distinct = "Distinct";
            }
            ComboName.Items.Clear();
            if (Condition != "")
            {
                Condition = " Where " + Condition;
            }

            if (OrderField == "")
            {
                strOrder = FieldName;
            }
            else if (!(OrderField.Equals("")))
            {
                strOrder = OrderField;
            }
            else
            {
                strOrder = FieldName;
            }

            string SQL = "SELECT " + Distinct + " " + FieldName + " from " + TableName + "" + Condition + " order by " + strOrder + "";
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader rs = null;
            try
            {
                cmd.CommandText = SQL;
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    ComboName.Items.Add(rs[0].ToString());
                }
            }
            catch { throw; }
            finally { if (rs != null) rs.Close(); cmd.Dispose(); }
            if (KeepValue) ComboName.Text = oldVal;

        }

        //ENDS HERE COMBO

        //ADDING SECOND COMBO HERE
        public void AddCombo(string TableName, string FieldName, ComboBox ComboName, string FieldName1, ComboBox ComboName1,
            string Condition, string OrderField, Boolean KeepValue, Boolean onlyDistinct, OracleConnection con)
        {
            string strOrder;
            String oldVal = ComboName.Text;
            oldVal = ComboName1.Text;

            if (KeepValue.Equals("1"))
            {
                oldVal = ComboName.Text;
                oldVal = ComboName1.Text;
            }
            string Distinct = "";
            if (onlyDistinct)
            {
                Distinct = "Distinct";
            }
            ComboName.Items.Clear();
            ComboName1.Items.Clear();

            if (Condition != "")
            {
                Condition = " Where " + Condition;
            }
            if (OrderField == "")
            {
                strOrder = FieldName;
            }
            else if (!(OrderField.Equals("")))
            {
                strOrder = OrderField;
            }
            else
            {
                strOrder = FieldName;
            }

            string SQL = "SELECT " + onlyDistinct + " " + FieldName + "," + FieldName1 + " from " + TableName + Condition + " order by " + strOrder;
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader rs = null;
            try
            {
                cmd.CommandText = SQL;
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    ComboName.Items.Add(rs[0].ToString());
                    ComboName1.Items.Add(rs[1].ToString());
                }
            }
            catch { throw; }
            finally
            {
                if (rs != null) rs.Close();
                cmd.Dispose();
            }
            if (KeepValue)
                ComboName.Text = oldVal;
            //
            ComboName1.Text = oldVal;
        }

        public string GetPartNumber_SUP(string Supplier_code, OracleConnection con, OracleTransaction Tr)
        {
            //
            return "";
            string Part_number = "", sql = "";
            OracleCommand cmd = con.CreateCommand();
            if (Tr != null) cmd.Transaction = Tr;
            sql = "select min(part_number_serial) from util_part_numberPool where supplier_code='" + Supplier_code + "'";
            cmd.CommandText = sql;
            OracleDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                //    Supplier_code = Supplier_code.
                //     Part_number = rs[0].ToString(0).PadLeft(4);
                return Part_number;
            }
            rs.Close();
            cmd.Dispose();
            if (Part_number == "" && Supplier_code.Length == 6)
            {
            }
        }

        public bool AddPricechangeHistory(string PART_NUMBER_SUP, string Doc_Date, string PACKING, string PRICE_CODE, string OLD_PRICE_CODE, string EFF_DATE, string VALID_UPTO, string CHANGE_PERCENT, string BAR_CODE, decimal RETAIL_PRICE, decimal WHOLESALE_RATE, decimal WHOLESALE_RATE1, decimal WHOLESALE_RATE2, decimal BUYING_RATE, decimal OLD_BUYING_RATE, decimal OLD_RETAIL_PRICE, decimal OLD_WHOLESALE_RATE, decimal OLD_WHOLESALE_RATE1, decimal OLD_WHOLESALE_RATE2, string OLD_BAR_CODE, string DOC_YEAR, string LOC_CODE, string CONFIRMED_ID, string EMP_ID, string Entry_Mode_PRC_BAR_NEWi_NEWp_DBN_BARd, OracleConnection con, OracleTransaction Tr)
        {
            if (Entry_Mode_PRC_BAR_NEWi_NEWp_DBN_BARd == "")
            {
                throw (new Exception("Mention Entry Mode"));
            }

            string sql = "insert into invt_pricechange_history(PART_NUMBER_SUP,PACKING,PRICE_CODE,OLD_PRICE_CODE,EFF_DATE,VALID_UPTO,CHANGE_PERCENT,BAR_CODE,RETAIL_PRICE,WHOLESALE_RATE,WHOLESALE_RATE1,WHOLESALE_RATE2,BUYING_RATE,OLD_BUYING_RATE,OLD_RETAIL_PRICE,OLD_WHOLESALE_RATE,OLD_WHOLESALE_RATE1,OLD_WHOLESALE_RATE2,OLD_BAR_CODE,DOC_YEAR,LOC_CODE,CONFIRMED_ID, EMP_ID,doc_date,ENTRY_MODE)" +
                " values('" + PART_NUMBER_SUP + "','" + PACKING + "','" + PRICE_CODE + "','" + (OLD_PRICE_CODE == "" ? null : OLD_PRICE_CODE) + "',to_date('" + (EFF_DATE == "" ? null : EFF_DATE) + "','DD/MON/YYYY HH:MI:SS AM'),to_date('" + (VALID_UPTO == "" ? null : VALID_UPTO) + "','DD/MON/YYYY HH:MI:SS AM')," + (CHANGE_PERCENT == "" ? "0" : CHANGE_PERCENT) + ",'" + BAR_CODE + "'," + RETAIL_PRICE + "," + WHOLESALE_RATE + "," + WHOLESALE_RATE1 + "," + WHOLESALE_RATE2 + "," + BUYING_RATE + "," + OLD_BUYING_RATE + "," + OLD_RETAIL_PRICE + "," + OLD_WHOLESALE_RATE + "," + OLD_WHOLESALE_RATE1 + "," + OLD_WHOLESALE_RATE2 + ",'" + OLD_BAR_CODE + "','" + DOC_YEAR + "','" + LOC_CODE + "','" + CONFIRMED_ID + "','" + EMP_ID + "',to_date('" + Doc_Date + "','DD/MON/YYYY HH:MI:SS AM'),'" + Entry_Mode_PRC_BAR_NEWi_NEWp_DBN_BARd + "')";
            if (ExecuteCmd(sql, con, Tr))
            {
                AddDataTransfer(PRICE_CODE, "A", LOC_CODE, Defaults.Def_Base_LOC, TransferTypes.PRICE_HISTORY, con, Tr);
                return true;
            }

            return false;
        }


        public bool AddPricechangeHistory(string PART_NUMBER_SUP, string Doc_Date, string PACKING, string PRICE_CODE, string OLD_PRICE_CODE, string EFF_DATE, string VALID_UPTO, string CHANGE_PERCENT, string BAR_CODE, decimal RETAIL_PRICE, decimal WHOLESALE_RATE, decimal WHOLESALE_RATE1, decimal WHOLESALE_RATE2, decimal BUYING_RATE, decimal OLD_BUYING_RATE, decimal OLD_RETAIL_PRICE, decimal OLD_WHOLESALE_RATE, decimal OLD_WHOLESALE_RATE1, decimal OLD_WHOLESALE_RATE2, string OLD_BAR_CODE, string DOC_YEAR, string LOC_CODE, string CONFIRMED_ID, string EMP_ID, string Entry_Mode_PRC_BAR_NEWi_NEWp_DBN_BARd, string Remarks, OracleConnection con, OracleTransaction Tr)
        {
            if (Entry_Mode_PRC_BAR_NEWi_NEWp_DBN_BARd == "")
            {
                throw (new Exception("Mention Entry Mode"));
            }

            string sql = "insert into invt_pricechange_history(PART_NUMBER_SUP,PACKING,PRICE_CODE,OLD_PRICE_CODE,EFF_DATE,VALID_UPTO,CHANGE_PERCENT,BAR_CODE,RETAIL_PRICE,WHOLESALE_RATE,WHOLESALE_RATE1,WHOLESALE_RATE2,BUYING_RATE,OLD_BUYING_RATE,OLD_RETAIL_PRICE,OLD_WHOLESALE_RATE,OLD_WHOLESALE_RATE1,OLD_WHOLESALE_RATE2,OLD_BAR_CODE,DOC_YEAR,LOC_CODE,CONFIRMED_ID, EMP_ID,doc_date,ENTRY_MODE,REMARKS)" +
                " values('" + PART_NUMBER_SUP + "','" + PACKING + "','" + PRICE_CODE + "','" + (OLD_PRICE_CODE == "" ? null : OLD_PRICE_CODE) + "',to_date('" + (EFF_DATE == "" ? null : EFF_DATE) + "','DD/MON/YYYY HH:MI:SS AM'),to_date('" + (VALID_UPTO == "" ? null : VALID_UPTO) + "','DD/MON/YYYY HH:MI:SS AM')," + (CHANGE_PERCENT == "" ? "0" : CHANGE_PERCENT) + ",'" + BAR_CODE + "'," + RETAIL_PRICE + "," + WHOLESALE_RATE + "," + WHOLESALE_RATE1 + "," + WHOLESALE_RATE2 + "," + BUYING_RATE + "," + OLD_BUYING_RATE + "," + OLD_RETAIL_PRICE + "," + OLD_WHOLESALE_RATE + "," + OLD_WHOLESALE_RATE1 + "," + OLD_WHOLESALE_RATE2 + ",'" + OLD_BAR_CODE + "','" + DOC_YEAR + "','" + LOC_CODE + "','" + CONFIRMED_ID + "','" + EMP_ID + "',to_date('" + Doc_Date + "','DD/MON/YYYY HH:MI:SS AM'),'" + Entry_Mode_PRC_BAR_NEWi_NEWp_DBN_BARd + "','" + Remarks + "')";
            if (ExecuteCmd(sql, con, Tr))
            {
                AddDataTransfer(PRICE_CODE, "A", LOC_CODE, Defaults.Def_Base_LOC, TransferTypes.PRICE_HISTORY, con, Tr);
                return true;
            }

            return false;
        }

        public string GetNewPricecode(char Mode_Add_Edit_Batch, OracleConnection con, OracleTransaction Tr)
        {
            string sql = "select seq_pricechange_hist.nextval as serial from dual", Price_Code = "";

            if (!Exists(sql, 1, out Price_Code, con, Tr)) return "";
            switch (Mode_Add_Edit_Batch)
            {
                case 'A':
                    Price_Code = "A" + Defaults.Def_Base_LOC + Price_Code.PadLeft(8, '0');
                    break;
                case 'E':
                    Price_Code = "E" + Defaults.Def_Base_LOC + Price_Code.PadLeft(8, '0');
                    break;
                case 'B':
                    Price_Code = "B" + Defaults.Def_Base_LOC + Price_Code.PadLeft(8, '0');
                    break;
            }
            return Price_Code;
        }


        #region For Vat Tax

        public string GetTax(string stPartNumberSup, OracleConnection oCon, OracleTransaction Tr)
        {
            OracleCommand oCmd = null;
            OracleDataReader oRs = null;
            try
            {
                oCmd = oCon.CreateCommand();
                if (Tr != null) oCmd.Transaction = Tr;
                String oSql = "Select TAX From INVT_ITEMPACKING Where PART_NUMBER_SUP ='" + stPartNumberSup + "' ";
                oCmd.CommandText = oSql;
                oRs = oCmd.ExecuteReader();

                if (oRs.Read())
                {
                    return (oRs["TAX"].ToString());
                }
                oRs.Close();
                return "0";
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error Taking On Taking Tax " + Exp, "E", "Taking Tax");
                return "0";
            }
            finally
            {
                if (oRs != null) oRs.Close();
                oCmd.Dispose();
            }
        }

        //public decimal TaxCalc(decimal TaxPercent, decimal AmountWithTax, out decimal Diff)
        //{
        //    AmountWithTax = Math.Round(AmountWithTax, 2);
        //    decimal Mrp = Math.Round(AmountWithTax / (1 + TaxPercent / 100), 2);
        //    decimal ReTaxVal = Math.Round(Mrp + Math.Round(Mrp * ((TaxPercent / 100)), 2), 2);
        //    Diff = ReTaxVal - AmountWithTax;
        //    //Ret = ReTaxVal;
        //    return Mrp;
        //}

        public decimal TaxCalc(decimal TaxPercent, decimal AmountWithTax, out decimal Diff)
        {
            AmountWithTax = Math.Round(AmountWithTax, 2);
            decimal Mrp = Math.Round(AmountWithTax / (1 + TaxPercent / 100), 2);
            decimal TAXX = Mrp * ((TaxPercent / 100));
            decimal ReTaxVal = Math.Round(Mrp + Mrp * ((TaxPercent / 100)), 2, MidpointRounding.AwayFromZero);
            Diff = ReTaxVal - AmountWithTax;
            return Mrp;
        }

        public decimal GetItemTaxPercentageNew(decimal RetailPrice, decimal TaxPerc, decimal QTY, decimal discountValue_ITEM)
        {
            decimal value = 0;
            try
            {
                return Math.Round((Math.Round((RetailPrice - discountValue_ITEM) * (TaxPerc / 100), 2, MidpointRounding.AwayFromZero)) * QTY, 2);
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error Taking On Taking TaxValue " + Exp, "E", "Taking TaxValue");
                return value;
            }
        }

        public decimal GetItemTaxPercentage(decimal RetailPrice, decimal TaxPerc, decimal QTY, decimal discountValue)
        {
            decimal value = 0;
            try
            {
                value = (RetailPrice * QTY) - discountValue;
                value = value * (TaxPerc / 100);
                return Math.Round(value, 2);
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error Taking On Taking TaxValue " + Exp, "E", "Taking TaxValue");
                return value;
            }
        }
        public decimal GetItemTaxValueNew(decimal RetailPrice, decimal TaxPerc, decimal QTY, decimal discountValue_ITEM)
        {
            decimal value = 0;
            try
            {
                return Math.Round((Math.Round((RetailPrice - discountValue_ITEM) + Math.Round((RetailPrice - discountValue_ITEM) * (TaxPerc / 100), 2, MidpointRounding.AwayFromZero), 2)) * QTY, 2);
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error Taking On Taking TaxValue " + Exp, "E", "Taking TaxValue");
                return value;
            }
        }

        public decimal GetItemTaxValue(decimal RetailPrice, decimal TaxPerc, decimal QTY, decimal discountValue)
        {
            decimal value = 0;
            try
            {
                value = (RetailPrice * QTY) - discountValue;
                value += value * (TaxPerc / 100);
                return Math.Round(value, 2);
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error Taking On Taking TaxValue " + Exp, "E", "Taking TaxValue");
                return value;
            }
        }

        public decimal GetItemRetailPriceFromTaxMrp(decimal decTaxIncludedRetailPrice, decimal TaxPerc)
        {
            decimal RetailPriceExptTax = 0;
            try
            {
                RetailPriceExptTax = (decTaxIncludedRetailPrice / (100 + TaxPerc)) * 100;
                return RetailPriceExptTax;
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error Taking On Taking TaxValue " + Exp, "E", "Taking TaxValue");
                return RetailPriceExptTax;
            }
            finally
            {
            }
        }
        public decimal GetItemRetailPriceFromTaxMrp____(decimal RetailPrice, decimal TaxPerc, decimal QTY, decimal discountValue)
        {
            decimal value = 0;
            try
            {
                value = (RetailPrice * QTY) - discountValue;
                value = value * (TaxPerc / 100);
                return Math.Round(value, 2);
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error Taking On Taking TaxValue " + Exp, "E", "Taking TaxValue");
                return value;
            }
            finally
            {
            }
        }
        #endregion  For Vat Tax

        // 
        // ENDS HERE 
        // NEW CODE FOR LISTBOX 
        // 

        public void AddList(string TableName, string FieldName, ListBox ListBoxName, string Condition, string OrderField, Boolean KeepValue, Boolean onlyDistinct, OracleConnection con)
        {
            string strOrder;
            string oldVal = ListBoxName.Text;

            if (KeepValue.Equals("1"))
            {
                oldVal = ListBoxName.Text;
            }
            string Distinct = "";
            if (onlyDistinct)
            {
                Distinct = "Distinct";
            }
            ListBoxName.Items.Clear();
            if (Condition != "")
            {
                Condition = " Where " + Condition;
            }
            if (OrderField == "")
            {
                strOrder = FieldName;
            }
            else if (!(OrderField.Equals("")))
            {
                strOrder = OrderField;
            }
            else
            {
                strOrder = FieldName;
            }

            string SQL = "";
            SQL = "SELECT " + Distinct + " " + FieldName + " from " + TableName + "" + Condition + " order by " + strOrder + "";
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader rs = null;
            try
            {
                cmd.CommandText = SQL;
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    ListBoxName.Items.Add(rs[0].ToString());
                }
            }
            catch { throw; }
            finally
            {
                if (rs != null) rs.Close();
                cmd.Dispose();
            }
            if (KeepValue)
                ListBoxName.Text = oldVal;
        }




        public bool Exists(string Query, short No_Args, out string Value, OracleConnection con, OracleTransaction Tr)
        {
            OracleDataReader rs = null;
            OracleCommand cmd = null;
            Value = "";
            try
            {
                cmd = con.CreateCommand();
                if (Tr != null) cmd.Transaction = Tr;
                cmd.CommandText = Query;
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    Value = rs[0].ToString();
                    return true;
                }

            }
            catch (Exception e)
            { throw (e); }
            finally
            {
                if (rs != null) rs.Close();
                if (cmd != null) cmd.Dispose();
            }
            return false;
        }

        public bool Exists(string Query, short No_Args, out string Value, out string Value1, OracleConnection con, OracleTransaction Tr)
        {
            Value = "";
            Value1 = "";
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                cmd = con.CreateCommand();
                if (Tr != null) cmd.Transaction = Tr;
                cmd.CommandText = Query;
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    Value = rs[0].ToString();
                    Value1 = rs[1].ToString();
                    return true;
                }
            }

            catch (Exception e) { throw (e); }
            finally
            {
                rs.Dispose();
                cmd.Dispose();
            }
            return false;
        }

        public bool Exists(string Query, OracleConnection con, OracleTransaction Tr)
        {
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                cmd = con.CreateCommand();
                if (Tr != null) cmd.Transaction = Tr;
                cmd.CommandText = Query;
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    return true;
                }
                rs.Close();
                cmd.Dispose();
            }
            catch (Exception e)
            { throw (e); }
            finally
            {
                rs.Close();
                cmd.Dispose();
            }
            return false;
        }


        public bool IsReserved(string Text, string EntryType, OracleConnection con, OracleTransaction Tr)
        {
            string sql = "select * from util_reservedcodes where upper(entry_type)='" + EntryType.ToUpper() + "' and value=(case when check_type='S' then substr('" + Text + "',1,length(value)) end)";
            return Exists(sql, con, Tr);
        }
        public bool IsManagedLocation(string Loc_Code, OracleConnection oraCon, OracleTransaction Tr)
        {
            try
            {
                string LocCode = "", MangLoc = "";
                if (Exists("COMN_LOCATION", "LOC_CODE,MANAGEMENT_LOC", "LOC_CODE='" + Loc_Code + "'", "LOC_CODE", 2, out LocCode, out MangLoc, oraCon, Tr))
                {
                    if (LocCode == MangLoc)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //ends list here

        //starts the Error message here
        public static bool IsNumeric(string Text)
        {
            try { double.Parse(Text); }
            catch { return false; }
            return true;
        }

        public static bool IsDate(string Text)
        {
            try
            {
                if (Text == null || Text.Length == 0)
                    return false;
                DateTime.Parse(Text);
            }
            catch { return false; }
            return true;
        }


        public static bool IsNumericPositive(string Text)
        {
            double x = 0;
            try { x = double.Parse(Text); }
            catch { return false; }
            return (x > 0) ? true : false;
        }


        public static bool IsNumeric(object Text)
        {
            try { double.Parse(Text.ToString()); }
            catch { return false; }
            return true;
        }

        public static decimal RN2(string Value) // Round Number To 2 Decimal Place
        {
            try
            {
                return RN2(decimal.Parse(Value));
            }
            catch
            {
                throw;
            }
        }
        public static decimal RN3(string Value) // Round Number To 3 Decimal Place
        {
            try
            {
                return RN3(decimal.Parse(Value));
            }
            catch
            {
                throw;
            }
        }
        public static decimal RN2(decimal Value) // Round Number To 2 Decimal Place
        {
            try
            {
                return Math.Round(Value, 2);
            }
            catch
            {
                throw;
            }
        }
        public static decimal RN3(decimal Value) // Round Number To 3 Decimal Place
        {
            try
            {
                return Math.Round(Value, 3);
            }
            catch
            {
                throw;
            }
        }
        private static ImageCodecInfo GetEncoderInfo(System.Drawing.Imaging.ImageFormat mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].FormatID == mimeType.Guid)
                    return encoders[j];
            }
            return null;
        }
        public Bitmap CreateBitmapImage(string sImageText, int width = 200, int fontSize = 12, Boolean right = true)
        {
            //EncoderParameter myEncoderParameter;
            //EncoderParameters myEncoderParameters;
            //ImageCodecInfo myImageCodecInfo = GetEncoderInfo(System.Drawing.Imaging.ImageFormat.Bmp);
            //myEncoderParameters = new EncoderParameters(1);
            //myEncoderParameter = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            //myEncoderParameters.Param[0] = myEncoderParameter;

            Bitmap objBmpImage = new Bitmap(1, 1);
            try
            {
                int intWidth = 0;
                int intHeight = 0;

                // Create the Font object for the image text drawing.
                Font objFont = new Font("Arial", fontSize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);

                // Create a graphics object to measure the text's width and height.
                Graphics objGraphics = Graphics.FromImage(objBmpImage);

                // This is where the bitmap size is determined.
                intWidth = width;// (int)objGraphics.MeasureString(sImageText, objFont).Width;
                //intHeight = 20;// (int)objGraphics.MeasureString(sImageText, objFont).Height;
                intHeight = (int)objGraphics.MeasureString(sImageText, objFont).Height + 2;

                int stringLen = (int)objGraphics.MeasureString(sImageText, objFont).Width;

                while (stringLen >= intWidth)
                {
                    if (sImageText.IndexOf(' ') < 0)
                    {
                        sImageText = sImageText.Substring(1);
                    }
                    else
                    {
                        sImageText = sImageText.Substring(sImageText.IndexOf(' ') + 1).Trim();
                    }
                    stringLen = (int)objGraphics.MeasureString(sImageText, objFont).Width;
                }


                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;


                // Create the bmpImage again with the correct size for the text and font.
                objBmpImage = new Bitmap(objBmpImage, new Size(intWidth, intHeight + 4));

                // Add the colors to the new bitmap.
                objGraphics = Graphics.FromImage(objBmpImage);
                // Set Background color
                objGraphics.Clear(Color.White);
                //            objGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                //            objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                objGraphics.DrawString(sImageText, objFont, new SolidBrush(Color.Black), right ? (intWidth - stringLen) : 0, 4, stringFormat);
                objGraphics.Flush();
                objFont.Dispose();
                objGraphics.Dispose();
            }
            catch (Exception ex)
            {

            }

            return (objBmpImage);
        }




        public static void Message(String Msg)
        {
            Message(Msg, "I", "");
        }

        public static void Message(String Msg, string Type, string caption)
        {
            caption = Application.CompanyName;

            if (Type.Equals("E"))
            {
                MessageBox.Show(Msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Type.Equals("X"))
            {
                MessageBox.Show(Msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (Type.Equals("Q"))
            {
                MessageBox.Show(Msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else if (Type.Equals("I"))
            {
                MessageBox.Show(Msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Type.Equals("W"))
            {
                MessageBox.Show(Msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Type.Equals("S"))
            {
                MessageBox.Show(Msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        public static DialogResult Message(String Msg, String caption, string Type, MessageBoxDefaultButton m)
        {
            caption = Application.CompanyName;
            DialogResult Result = DialogResult.None;
            if (Type.Equals("Q"))
            {
                Result = MessageBox.Show(Msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, m);
            }
            else if (Type.Equals("QC"))
            {
                Result = MessageBox.Show(Msg, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, m);
            }
            return Result;
        }

        public static DialogResult Message(String Msg, String caption, string Type, out DialogResult Result)
        {
            caption = Application.CompanyName;
            Result = DialogResult.None;
            if (Type.Equals("Q"))
            {
                Result = MessageBox.Show(Msg, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
            else if (Type.Equals("QC"))
            {
                Result = MessageBox.Show(Msg, caption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            }
            return Result;
        }


        public bool IsNumeric1(string Number)
        {
            try { double x = double.Parse(Number); return true; }
            catch { return false; }
        }

        public decimal getLocRetailPrice(string stGroupLocation, string stPartNumberSup, string Packing, OracleConnection oCon, OracleTransaction Tr)
        {
            string Sql = "Select RETAIL_PRICE From INVT_ITEMPACKING_LOC Where LOC_CODE='" + stGroupLocation + "' And PART_NUMBER_SUP='" + stPartNumberSup + "' And PACKING='" + Packing + "'";
            string RetailPrice = GetValue(Sql, oCon, Tr);
            if (RetailPrice.Length == 0) RetailPrice = "0";
            return decimal.Parse(RetailPrice);
        }
        public decimal getLocRetailPrice(string stGroupLocation, string stPartNumberSup, int PackingOrder, int Zero_Serial, OracleConnection oCon, OracleTransaction Tr)
        {
            string Sql = "Select RETAIL_PRICE From INVT_ITEMPACKING_LOC Where LOC_CODE='" + stGroupLocation + "' And PART_NUMBER_SUP='" + stPartNumberSup + "'";
            if (PackingOrder > 0) Sql = Sql + " And PACKING_ORDER=" + PackingOrder;
            else Sql = Sql + " And ZERO_SERIAL=" + Zero_Serial;
            string RetailPrice = GetValue(Sql, oCon, Tr);
            if (RetailPrice.Length == 0) RetailPrice = "0";
            return decimal.Parse(RetailPrice);
        }

        public string getLandingCost(string stLocation, string stPartNumberSup, decimal PackMulQty, int Zero_Serial, OracleConnection oCon, OracleTransaction Tr)
        {
            OracleCommand oCmd = null;
            OracleDataReader oRs = null;
            try
            {
                if (stLocation.Equals(Defaults.Def_Main_Loc))
                {
                    //stLocation = GetValue("Select LOC_CODE From COMN_LOCATION Where LOC_TYPE='SH' And GROUP_LOC='" + stLocation + "' And ACTIVE='Y'", oCon, Tr);
                    stLocation = GetValue("Select LOC_CODE From COMN_LOCATION Where LOC_TYPE='SH' And INVT_GROUP_LOC='" + stLocation + "' And ACTIVE='Y'", oCon, Tr);
                }

                string ZeroPacking = "";
                ZeroPacking = " And ZERO_SERIAL =" + Zero_Serial;
                oCmd = oCon.CreateCommand();
                if (Tr != null) oCmd.Transaction = Tr;
                String oSql = "Select LC From INVT_ITEMTRAN_DTLS Where LOC_CODE='" + stLocation + "' And PART_NUMBER_SUP ='" + stPartNumberSup + "'" + ZeroPacking + " Order By LAST_GRV_DATE DESC";
                oCmd.CommandText = oSql;
                oRs = oCmd.ExecuteReader();
                if (oRs.Read())
                {
                    // return (decimal.Parse(oRs["LC"].ToString()) * PackMulQty).ToString();                    
                    return (decimal.Parse(oRs["LC"].ToString()) * (Zero_Serial.Equals(0) ? PackMulQty : 1)).ToString();
                }
                oRs.Close();
                return "0";
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error Taking Landing Cost " + Exp, "E", "Taking Landing Cost");
                return "0";
            }
            finally
            {
                if (oRs != null) oRs.Close();
                oCmd.Dispose();
            }
        }

        public decimal getStock(string LocCode, string PartNumberSup, int ZeroSerial, OracleConnection Conn, OracleTransaction Tr)
        {
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            decimal decStock = 0;
            try
            {
                cmd = Conn.CreateCommand();
                if (Tr != null) cmd.Transaction = Tr;
                cmd.CommandText = "Select Round((CB_QTY/PACK_MULTIPLY_QTY),2) as STOCK From INVT_INVENTORYBALANCE Where LOC_CODE='" + LocCode + "' And PART_NUMBER_SUP='" + PartNumberSup + "' And ZERO_SERIAL=" + ZeroSerial;
                rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    if (rs.Read())
                    {
                        decStock = decimal.Parse(rs["STOCK"].ToString());
                    }
                    rs.Close();
                }
            }
            catch { throw; }
            finally
            {
                if (rs != null) rs.Dispose();
                if (cmd != null) cmd.Dispose();
            }
            return decStock;
        }
        public bool GetStock(string LocCode, string PartNumber, int Zero, out string stock, OracleConnection oraConn)
        {
            OracleCommand cmd3 = oraConn.CreateCommand();
            OracleDataReader rsS = null;
            cmd3.CommandText = "SELECT Round((CB_QTY/PACK_MULTIPLY_QTY),2)as Stock FROM  INVT_INVENTORYBALANCE WHERE LOC_CODE='" + LocCode + "' AND PART_NUMBER_SUP='" + PartNumber + "' and ZERO_SERIAL=" + Zero + "";
            try
            {
                rsS = cmd3.ExecuteReader();
                string SD = "0";
                if (rsS.Read())
                {
                    SD = rsS.GetValue(0).ToString();
                }
                stock = SD;
            }
            catch { throw; }
            finally
            {
                if (rsS != null) rsS.Close();
                cmd3.Dispose();
            }
            return true;
        }
        public bool GetStock(string LocCode, string PartNumber, int Zero, out string stock, OracleConnection oraConn, OracleTransaction Tr)
        {
            OracleCommand cmd3 = oraConn.CreateCommand();

            OracleDataReader rsS = null;
            cmd3.CommandText = "SELECT Round((CB_QTY/PACK_MULTIPLY_QTY),2)as Stock FROM  INVT_INVENTORYBALANCE WHERE LOC_CODE='" + LocCode + "' AND PART_NUMBER_SUP='" + PartNumber + "' and ZERO_SERIAL=" + Zero + "";
            try
            {
                if (Tr != null) cmd3.Transaction = Tr;
                rsS = cmd3.ExecuteReader();
                string SD = "0";
                if (rsS.Read())
                {
                    SD = rsS.GetValue(0).ToString();
                }
                stock = SD;
            }
            catch { throw; }
            finally
            {
                if (rsS != null) rsS.Close();
                cmd3.Dispose();
            }
            return true;
        }
        /* public bool GetStock(string LocCode, string PartNumber, out string stock, OracleConnection oraConn)
         {
             OracleCommand cmd = oraConn.CreateCommand();
             cmd.CommandText = "SELECT CB_QTY FROM  INVT_INVENTORYBALANCE WHERE LOC_CODE='" + LocCode + "' AND PART_NUMBER_SUP='" + PartNumber + "'";
             OracleDataReader rs = cmd.ExecuteReader();
             string SD = "";
             if (rs.Read())
             {
                 SD = rs.GetValue(0).ToString();
             }
             stock = SD;
            
             rs.Close();
             cmd.Dispose();
             return true;
         }*/
        //
        public bool GetStock_Acc(string PartNUmber, int Zero, out string LocationStock, OracleConnection oraConn)
        {
            string LocationSale = "";
            return GetStock_Acc(PartNUmber, Zero, out  LocationStock, out  LocationSale, oraConn);
        }
        public bool GetStock_Acc(string PartNUmber, int Zero, out string LocationStock, out string LocationSale, OracleConnection oraConn)
        {
            OracleCommand cmd1;
            OracleDataReader rs1 = null;
            LocationSale = "";
            string lc = "";
            string cb = "";
            string LocStck = "";
            cmd1 = oraConn.CreateCommand();

            string SQL1;
            try
            {
                // SQL1 = "select l.LOC_CODE,i.CB_QTY from INVT_INVENTORYBALANCE i,COMN_LOCATION l where l.LOC_CODE=i.LOC_CODE and i.PART_NUMBER_SUP='" + PartNUmber + "'";
                SQL1 = "select cl.LOC_CODE,ib.CB_QTY/ib.PACK_MULTIPLY_QTY,SALE_QTY-SALE_R_QTY as Sale from INVT_INVENTORYBALANCE ib RIGHT OUTER JOIN COMN_LOCATION cl  on cl.LOC_CODE=ib.LOC_CODE and ib.PART_NUMBER_SUP='" + PartNUmber + "' and ib.ZERO_SERIAL=" + Zero + "";
                SQL1 = "select cl.LOC_CODE,ib.CB_QTY/ib.PACK_MULTIPLY_QTY,SALE_QTY-SALE_R_QTY as Sale from INVT_INVENTORYBALANCE ib RIGHT OUTER JOIN COMN_LOCATION cl  on cl.LOC_CODE=ib.LOC_CODE and ib.PART_NUMBER_SUP='" + PartNUmber + "' and ib.ZERO_SERIAL=" + Zero + " AND cl.LOC_TYPE<>'MN' Order By INDIVIDUAL_LOC Desc,LOC_TYPE,LOC_CODE Asc";
                SQL1 = "select cl.LOC_CODE,ib.CB_QTY/ib.PACK_MULTIPLY_QTY,SALE_QTY-SALE_R_QTY as Sale from INVT_INVENTORYBALANCE ib RIGHT OUTER JOIN COMN_LOCATION cl  on cl.LOC_CODE=ib.LOC_CODE and ib.PART_NUMBER_SUP='" + PartNUmber + "' and ib.ZERO_SERIAL=" + Zero + " AND cl.LOC_TYPE<>'MN' Where cl.COUNTRY_CODE='" + Defaults.Def_Country + "' Order By INDIVIDUAL_LOC Desc,LOC_TYPE,LOC_CODE Asc";
                SQL1 = "select cl.LOC_CODE,ROUND((ib.CB_QTY/ib.PACK_MULTIPLY_QTY),4) As CB_QTY,SALE_QTY-SALE_R_QTY as Sale from INVT_INVENTORYBALANCE ib RIGHT OUTER JOIN COMN_LOCATION cl  on cl.LOC_CODE=ib.LOC_CODE and ib.PART_NUMBER_SUP='" + PartNUmber + "' and ib.ZERO_SERIAL=" + Zero + " AND cl.LOC_TYPE<>'MN' Where cl.COUNTRY_CODE='" + Defaults.Def_Country + "' Order By INDIVIDUAL_LOC Desc,LOC_TYPE,LOC_CODE Asc";
                cmd1.CommandText = SQL1;
                rs1 = cmd1.ExecuteReader();
                while (rs1.Read())
                {
                    lc = rs1.GetString(0);
                    if (rs1.IsDBNull(1))
                    {
                        cb = "0";
                    }
                    else
                    {
                        cb = rs1.GetValue(1).ToString();
                    }
                    // LocStck = LocStck + lc + " - " + cb + " , ";
                    LocStck = LocStck + lc + "->" + cb + ", ";
                    LocationSale = LocationSale + "," + lc + " - " + (rs1.IsDBNull(2) ? "0" : rs1[2].ToString());
                    //txtLocStock.Text = txtLocStock.Text + lc + " - " + cb + " , ";
                }
                if (LocationSale != "") LocationSale = LocationSale.Substring(1, LocationSale.Length - 1);
                LocationStock = LocStck;
                return true;
            }
            catch { throw; }
            finally
            {
                if (rs1 != null) rs1.Close();
                cmd1.Dispose();
            }
        }

        public bool GetStock_Acc_New(string PartNUmber, int Zero, out string LocationStock, OracleConnection oraConn)
        {
            string LocationSale = "";
            return GetStock_Acc_New(PartNUmber, Zero, out  LocationStock, out  LocationSale, oraConn);
        }
        public bool GetStock_Acc_New(string PartNUmber, int Zero, out string LocationStock, out string LocationSale, OracleConnection oraConn)
        {
            OracleCommand cmd1;
            OracleDataReader rs1 = null;
            LocationSale = "";
            string lc = "";
            string cb = "";
            string LocStck = "";
            cmd1 = oraConn.CreateCommand();

            string SQL1;
            try
            {
                // SQL1 = "select l.LOC_CODE,i.CB_QTY from INVT_INVENTORYBALANCE i,COMN_LOCATION l where l.LOC_CODE=i.LOC_CODE and i.PART_NUMBER_SUP='" + PartNUmber + "'";
                SQL1 = "select cl.LOC_CODE,ib.CB_QTY/ib.PACK_MULTIPLY_QTY,SALE_QTY-SALE_R_QTY as Sale from INVT_INVENTORYBALANCE ib RIGHT OUTER JOIN COMN_LOCATION cl  on cl.LOC_CODE=ib.LOC_CODE and ib.PART_NUMBER_SUP='" + PartNUmber + "' and ib.ZERO_SERIAL=" + Zero + "";
                SQL1 = "select cl.LOC_CODE,ib.CB_QTY/ib.PACK_MULTIPLY_QTY,SALE_QTY-SALE_R_QTY as Sale from INVT_INVENTORYBALANCE ib RIGHT OUTER JOIN COMN_LOCATION cl  on cl.LOC_CODE=ib.LOC_CODE and ib.PART_NUMBER_SUP='" + PartNUmber + "' and ib.ZERO_SERIAL=" + Zero + " AND cl.LOC_TYPE<>'MN' Order By INDIVIDUAL_LOC Desc,LOC_TYPE,LOC_CODE Asc";
                SQL1 = "select cl.LOC_CODE,ib.CB_QTY/ib.PACK_MULTIPLY_QTY,SALE_QTY-SALE_R_QTY as Sale from INVT_INVENTORYBALANCE ib RIGHT OUTER JOIN COMN_LOCATION cl  on cl.LOC_CODE=ib.LOC_CODE and ib.PART_NUMBER_SUP='" + PartNUmber + "' and ib.ZERO_SERIAL=" + Zero + " AND cl.LOC_TYPE<>'MN' Where cl.COUNTRY_CODE='" + Defaults.Def_Country + "' Order By INDIVIDUAL_LOC Desc,LOC_TYPE,LOC_CODE Asc";
                SQL1 = "select cl.LOC_CODE,ROUND((ib.CB_QTY/ib.PACK_MULTIPLY_QTY),4) As CB_QTY,SALE_QTY-SALE_R_QTY as Sale from INVT_INVENTORYBALANCE ib RIGHT OUTER JOIN COMN_LOCATION cl  on cl.LOC_CODE=ib.LOC_CODE and ib.PART_NUMBER_SUP='" + PartNUmber + "' and ib.ZERO_SERIAL=" + Zero + " AND cl.LOC_TYPE<>'MN' Where cl.COUNTRY_CODE='" + Defaults.Def_Country + "' Order By INDIVIDUAL_LOC Desc,LOC_TYPE,LOC_CODE Asc";
                SQL1 = "select cl.LOC_CODE,ROUND((ib.CB_QTY/ib.PACK_MULTIPLY_QTY),4) As CB_QTY,SALE_QTY-SALE_R_QTY as Sale from INVT_INVENTORYBALANCE ib RIGHT OUTER JOIN COMN_LOCATION cl  on cl.LOC_CODE=ib.LOC_CODE and ib.PART_NUMBER_SUP='" + PartNUmber + "' and ib.ZERO_SERIAL=" + Zero + " AND cl.LOC_TYPE<>'MN' Where cl.COUNTRY_CODE='" + Defaults.Def_Country + "' AND CL.LOC_TYPE IN ('SH','WH') AND CL.LOC_CODE <> 'GDN' AND CL.ACTIVE='Y' Order By INDIVIDUAL_LOC Desc,LOC_TYPE,LOC_CODE Asc";
                cmd1.CommandText = SQL1;
                rs1 = cmd1.ExecuteReader();
                while (rs1.Read())
                {
                    lc = rs1.GetString(0);
                    if (rs1.IsDBNull(1))
                    {
                        cb = "0";
                    }
                    else
                    {
                        cb = rs1.GetValue(1).ToString();
                    }
                    // LocStck = LocStck + lc + " - " + cb + " , ";
                    LocStck = LocStck + lc + "->" + cb + ", ";
                    LocationSale = LocationSale + "," + lc + " - " + (rs1.IsDBNull(2) ? "0" : rs1[2].ToString());
                    //txtLocStock.Text = txtLocStock.Text + lc + " - " + cb + " , ";
                }
                if (LocationSale != "") LocationSale = LocationSale.Substring(1, LocationSale.Length - 1);
                LocationStock = LocStck;
                return true;
            }
            catch { throw; }
            finally
            {
                if (rs1 != null) rs1.Close();
                cmd1.Dispose();
            }
        }


        public string SysDate(OracleConnection con, OracleTransaction tr, bool GET_DATE_AND_TIME)
        {
            OracleCommand cmd = con.CreateCommand();
            if (tr != null) cmd.Transaction = tr;
            if (GET_DATE_AND_TIME)
                cmd.CommandText = "select sysdate from dual";
            else
                cmd.CommandText = "select to_char(sysdate,'DD/MON/YYYY') from dual";
            string Sysdate = "";
            OracleDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                Sysdate = rs[0].ToString().ToUpper();
            }
            rs.Close();
            cmd.Dispose();
            return Sysdate;
        }

        //
        // FOR LOCATION        
        public string GetLocationName(string LocCod, OracleConnection oraConn)
        {
            OracleCommand cmdl = null;
            OracleDataReader rsl = null;
            try
            {
                cmdl = oraConn.CreateCommand();
                string SQL;
                SQL = "SELECT LOC_NAME FROM COMN_LOCATION WHERE LOC_CODE='" + LocCod + "'";
                cmdl.CommandText = SQL;
                rsl = cmdl.ExecuteReader();
                if (rsl.Read())
                {
                    return rsl.GetString(0);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (rsl != null) rsl.Close();
                if (cmdl != null) cmdl.Dispose();
            }
            return "";
        }
        public string getGroupLocation(string Loc_Code, OracleConnection con, OracleTransaction tr)
        {
            string Sql = "Select GROUP_LOC,LOC_CODE From COMN_LOCATION Where LOC_CODE='" + Loc_Code + "'";
            return GetValue(Sql, con, tr);
        }
        //
        public bool ChangeItem_PARTNUMBER(string OldPart_Number, string NewPart_Number, OracleConnection con)
        {
            OracleTransaction tr = con.BeginTransaction();
            bool b = false;
            int r = 0;
            b = this.updateTable("INVT_INVENTORYMASTER", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", out r, con, tr);
            if (b) b = this.updateTable("INVT_ITEMPACKING", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_Inventorybalance", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_Inventorybalance_YR", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("invt_assemblymain", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("invt_assemblysub", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("invt_assemblysub", "ASY_Part_number_sup='" + NewPart_Number + "'", "ASY_part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_deliverynotesub", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_invoicesub", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_purchaseordersub", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_purchasesub", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_salesordersub", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_itemtran_dtls", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_itemtransaction", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_splittingsub", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_transfersub", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_special_itemtransaction", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_pricechange_detail", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_pricechange_history", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (b) b = this.updateTable("INVT_internalgrvsub", "Part_number_sup='" + NewPart_Number + "'", "part_number='" + OldPart_Number + "'", con, tr);
            if (!b || r == 0)
            {
                MessageBox.Show("ITEM NOT FOUND");
                tr.Rollback();
                return false;
            }

            this.insertTable("INVT_ITEMCODE_CHANGE_HIST", "OLD_PART_NUMBER,NEW_PART_NUMBER,doc_date", "'" + OldPart_Number + "','" + NewPart_Number + "','" + DateTime.Today.ToString("dd/MMM/yyyy") + "'", con, tr);
            tr.Commit();
            return true;
        }

        //}

        public void Log_Audit(string Loc_Code, string Form_name, string Action_type, string Action, string Description, string Doc_No, string Doc_Date, string Doc_Type)
        {
            OracleConnection Conn = null;
            try
            {
                Conn = getPooledConnection(Defaults.Def_SERVER_DB);
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error On Connection.\n\r" + Exp.Message, "E", "");
            }
            try
            {
                Log_Audit(Loc_Code, Form_name, Action_type, Action, Description, Doc_No, Doc_Date, Doc_Type, Conn, null);
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error On Connection.", "E", "");
            }
            finally
            {
            }
        }
        public void Log_Audit(string Loc_Code, string Form_name, string Action_type, string Action, string Description, string Doc_No, string Doc_Date, string Doc_Type, OracleConnection oraConn, OracleTransaction Tr)
        {
            if (Form_name.Length > 30) Form_name = Form_name.Substring(0, 29);
            if (Action.Length > 50) Action = Action.Substring(0, 49);
            if (Description.Length > 255) Description = Description.Substring(0, 254);
            if (Doc_No.Length > 200) Doc_No = Doc_No.Substring(0, 199);
            if (Doc_Type.Length > 6) Doc_Type = Doc_Type.Substring(0, 29);
            string fields = "";
            string Values = "";
            string strOracle = "";
            OracleCommand oraCommand = null;
            try
            {
                string myHost = System.Net.Dns.GetHostName();
                string myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();
                string localDate = DateTime.Now.ToString();
                //string serverTime = SysDate(oraConn, Tr, true);
                //Updated By DD 11/Jan/2011 03:18 PM SEQ_ACCTGROUPS <--> SEQ_SYSAUDIT -300000                 
                fields = "SLNO,LOC_CODE_ORIGINAL,LOC_CODE,SYSTEM_NAME,IPADDRESS,OS,SYSTEM_USER,SERVER_TIME,LOCAL_TIME,APPLICATION,APPLICATION_LOGIN,FORM_NAME,ACTION_TYPE,ACTION,DESCRIPTION,DOC_NO,DOC_DATE,DOC_YEAR,DOC_TYPE";
                fields = "SLNO,LOC_CODE_ORIGINAL,LOC_CODE,SYSTEM_NAME,IPADDRESS,OS,SYSTEM_USER,SERVER_TIME,LOCAL_TIME,APPLICATION,APPLICATION_LOGIN,FORM_NAME,ACTION_TYPE,ACTION,DESCRIPTION,DOC_NO,DOC_DATE,DOC_YEAR,DOC_TYPE,APPLICATION_VERSION";
                Values = "SEQ_SYSAUDIT.nextval,'" + Defaults.Def_Base_LOC + "','" + Loc_Code + "','" + Environment.MachineName + "','" + myIP + "','" + Environment.OSVersion + "','" + Environment.UserName + "',SYSDATE,to_date('" + localDate + "','DD/MON/YYYY HH:MI:SS AM'),'" + Application.ProductName + "','" + Defaults.Def_User + "','" + Form_name + "','" + Action_type + "','" + Action + "','" + Description + "','" + Doc_No + "','" + Doc_Date + "','" + Defaults.Def_YEAR + "','" + Doc_Type + "'";
                Values = "SEQ_SYSAUDIT.nextval,'" + Defaults.Def_Base_LOC + "','" + Loc_Code + "','" + Environment.MachineName + "','" + myIP + "','" + Environment.OSVersion + "','" + Environment.UserName + "',SYSDATE,to_date('" + localDate + "','DD/MON/YYYY HH:MI:SS AM'),'" + Application.ProductName + "','" + Defaults.Def_User + "','" + Form_name + "','" + Action_type + "','" + Action + "','" + Description + "','" + Doc_No + "','" + Doc_Date + "','" + Defaults.Def_YEAR + "','" + Doc_Type + "','" + Application.ProductVersion + "'";
                strOracle = "INSERT INTO  SYS_AUDIT  (" + fields.Trim() + ") VALUES (" + Values + ")";
                oraCommand = new OracleCommand(strOracle, oraConn);
                if (Tr != null)
                {
                    oraCommand.Transaction = Tr;
                }
                oraCommand.ExecuteNonQuery();
            }
            catch (Exception Exp)
            {
                throw (Exp);   //DataConnector.Message("Database Connection Error in Server" + ex, "W", "");
            }
            finally
            {
                if (oraCommand != null) oraCommand.Dispose();
            }
        }

        public void Log_Error(string Loc_Code, string Form_name, string Event, string Function_name, string sQl_Detail, string Error_Msg, string Remarks, string Notified)
        {
            if (Form_name.Length > 30) Form_name = Form_name.Substring(0, 29);
            if (Event.Length > 50) Event = Event.Substring(0, 49);
            if (Function_name.Length > 30) Function_name = Function_name.Substring(0, 29);
            if (sQl_Detail.Length > 255) sQl_Detail = sQl_Detail.Substring(0, 224);
            if (Error_Msg.Length > 200) Error_Msg = Error_Msg.Substring(0, 199);
            if (Remarks.Length > 200) Remarks = Remarks.Substring(0, 199);


            string fields = "";
            string Values = "";
            string strOracle = "";
            OracleConnection oraConn = new OracleConnection();
            try
            {
                oraConn = getPooledConnection(Defaults.Def_SERVER_DB);
                ;
            }
            catch (Exception ex)
            {
                DataConnector.Message("Database Connection Error in Server" + ex, "W", "");
                return;
            }
            OracleCommand oraCommand = null;
            try
            {
                string myHost = System.Net.Dns.GetHostName();
                string myIP = System.Net.Dns.GetHostEntry(myHost).AddressList[0].ToString();
                string localDate = DateTime.Now.ToString();
                // string serverTime = SysDate(oraConn, null, true);
                //Updated By DD 11/Jan/2011 03:18 PM SEQ_ACCTGROUPS <--> SEQ_SYSERROR -300000
                //Application.ProductVersion                 
                fields = "SLNO,LOC_CODE_ORIGINAL,LOC_CODE,SYSTEM_NAME,IPADDRESS,OS,SYSTEM_USER,SERVER_TIME,LOCAL_TIME,APPLICATION,APPLICATION_LOGIN,FORM_NAME,EVENT,FUNCTION_NAME,SQL_DETAIL,ERROR_MESSAGE,REMARKS,NOTIFIED";
                fields = "SLNO,LOC_CODE_ORIGINAL,LOC_CODE,SYSTEM_NAME,IPADDRESS,OS,SYSTEM_USER,SERVER_TIME,LOCAL_TIME,APPLICATION,APPLICATION_LOGIN,FORM_NAME,EVENT,FUNCTION_NAME,SQL_DETAIL,ERROR_MESSAGE,REMARKS,NOTIFIED,APPLICATION_VERSION";
                Values = "SEQ_SYSERROR.nextval,'" + Defaults.Def_Base_LOC + "','" + Loc_Code + "','" + Environment.MachineName + "','" + myIP + "','" + Environment.OSVersion + "','" + Environment.UserName + "',SYSDATE,to_date('" + localDate + "','DD/MON/YYYY HH:MI:SS AM'),'" + Application.ProductName + "','" + Defaults.Def_User + "','" + Form_name + "','" + Event + "','" + Function_name + "','" + sQl_Detail + "','" + Error_Msg + "','" + Remarks + "','" + Notified + "'";
                Values = "SEQ_SYSERROR.nextval,'" + Defaults.Def_Base_LOC + "','" + Loc_Code + "','" + Environment.MachineName + "','" + myIP + "','" + Environment.OSVersion + "','" + Environment.UserName + "',SYSDATE,to_date('" + localDate + "','DD/MON/YYYY HH:MI:SS AM'),'" + Application.ProductName + "','" + Defaults.Def_User + "','" + Form_name + "','" + Event + "','" + Function_name + "','" + sQl_Detail + "','" + Error_Msg + "','" + Remarks + "','" + Notified + "','" + Application.ProductVersion + "'";
                strOracle = "INSERT INTO  SYS_ERROR  (" + fields.Trim() + ") VALUES (" + Values + ")";
                oraCommand = new OracleCommand(strOracle, oraConn);
                oraCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                DataConnector.Message("Database Connection Error in Server" + ex, "W", "");
            }
            finally
            {
                if (oraCommand != null) oraCommand.Dispose();
                if (oraConn != null && oraConn.State == ConnectionState.Open)
                {
                    ;
                    ;
                }
            }
        }

        /// <summary>
        /// 
        ///  TRIAL REPORT
        ///
        ///
        int slno = 0;
        public string Make_AcctTEMP(string AcctHead, string HeadColl, string NameColl, OracleConnection con, OracleTransaction tr, DateTime dt, string Location)
        {
            //string Sql = "select * from acct_groups where acct_group='" + AcctHead + "' and acct_type<>'P'";
            // ChangeDateFormat BY DD  ON 10:45 AM 22/Mar/2011
            string Sql = "select * from acct_groups where acct_group='" + AcctHead + "' and acct_type<>'P' ORDER BY ACCT_CODE";
            //   MessageBox.Show(AcctHead + "  " + HeadColl );


            OracleCommand cmd = con.CreateCommand();
            OracleDataReader rs = null;
            try
            {
                if (tr != null) cmd.Transaction = tr;
                cmd.CommandText = Sql;
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {

                    if (rs["acct_code"].ToString() == "OWN-CU")
                    {

                    }

                    if (rs["acct_type"].ToString().Equals("L"))
                    {
                        char[] ch = { ',' };
                        string[] AcctName = NameColl.Split(ch);
                        int i = 0;
                        string Fld = "", val = "";
                        while (i < AcctName.Length)
                        {
                            Fld = Fld + ",GRP_CODE" + (i + 1);
                            val = val + ",GRP_NAME" + (i + 1);
                            i++;
                        }
                        // Add to table
                        if (i > 1)
                        {
                            //HeadColl = HeadColl.Substring(1, HeadColl.Length - 1);
                            Fld = Fld.Substring(1, Fld.Length - 1);
                            // NameColl = NameColl.Substring(1, NameColl.Length - 1);
                            val = val.Substring(1, val.Length - 1);

                        }
                        slno = slno + 1;
                        string SQL = "insert into TEmp_TrialBalance(loc_code,slno,dt," + Fld + "," + val + ",acct_code,acct_name,dr,cr) values('" + Defaults.Def_Main_Loc + "'," + (slno++) + ",TO_DATE('" + dt + "','DD/MON/YYYY HH:MI:SS AM')," + HeadColl + "," + NameColl + ",'" + rs["acct_code"].ToString() + "','" + rs["acct_name"].ToString() + "',0,0)";
                        if (!ExecuteCmd(SQL, con, tr)) MessageBox.Show("");
                    }
                    else
                    {

                        string HeadColl1 = HeadColl + ",'" + rs["acct_code"].ToString() + "'";
                        string NameColl1 = NameColl + ",'" + rs["acct_name"].ToString() + "'";
                        Make_AcctTEMP(rs["acct_code"].ToString(), HeadColl1, NameColl1, con, tr, dt, Defaults.Def_Main_Loc);
                    }

                }
            }
            catch { throw; }
            finally
            {
                if (rs != null) rs.Close();
                cmd.Dispose();
            }
            return "";

        }

        public void AccountGroupListingHirarchy(OracleConnection con, OracleTransaction tr, string FromDt, string ToDt, string Location)
        {
            string Sql = "select distinct acct_code,acct_name,acct_primarygroup from acct_groups where acct_type='P'";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = Sql;
            OracleDataReader rs = null;
            if (tr != null) cmd.Transaction = tr;
            try
            {
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    string pm = "";

                    if (rs["acct_primarygroup"].ToString() == "ASS") pm = "01";
                    if (rs["acct_primarygroup"].ToString() == "LIA") pm = "02";
                    if (rs["acct_primarygroup"].ToString() == "INC") pm = "03";
                    if (rs["acct_primarygroup"].ToString() == "EXP") pm = "04";



                    Make_AcctTEMP(rs["acct_code"].ToString(), "'" + pm + "','" + rs["acct_code"].ToString() + "'", "'" + rs["acct_primarygroup"].ToString() + "','" + rs["acct_name"].ToString() + "'", con, tr, DateTime.Now, Location);
                    //rs[""].ToString();
                }
            }
            catch { throw; }
            finally
            {
                if (rs != null) rs.Close();
                cmd.Dispose();
            }

        }

        public void PrepareTrialBalance(OracleConnection con, OracleTransaction tr, string FromDt, string ToDt, string Location)
        {
            slno = 0;
            string st = "";
            if (Exists("select max(dt) from temp_trialbalance", 1, out st, con, tr))
            {
                if (st != "")
                {
                    DateTime dt = DateTime.Parse(st);
                    if (dt.ToString("dd/MMM/yyyy") == DateTime.Today.ToString("dd/MMM/yyyy"))
                    {
                        if (DataConnector.Message("Last Prepared Data today at " + st + " You want to re generate? \r\n(YES IF YOU ADDED NEW A/C HEADS, NO IF NO NEW HEADS)", "", "Q", MessageBoxDefaultButton.Button1) == DialogResult.No)
                        {
                            //updateTable("temp_trialbalance", "cr=0,dr=0", null, con, tr);
                            // CHANGED BY DD LENGTH OF NULL ERROR
                            updateTable("temp_trialbalance", "cr=0,dr=0", "", con, tr);
                            goto skip;
                        }
                    }
                    deleteTable("temp_trialbalance", null, con, tr);
                }
            }
            AccountGroupListingHirarchy(con, tr, FromDt, ToDt, Location);
        skip: OracleCommand cmd;
            cmd = con.CreateCommand();
            try
            {
                if (tr != null) cmd.Transaction = tr;
                //  cmd.CommandText = "update temp_trialbalance a set (dr,cr) = (select CASE WHEN sum( Lc_debit)IS NULL THEN 0 ELSE sum( Lc_debit) END as dr,CASE WHEN sum( Lc_CREDIT)IS NULL THEN 0 ELSE sum( Lc_CREDIT) END as cr from T_acct_transactions b where a.acct_code=b.acct_code)";

                cmd.CommandText = "update temp_trialbalance a set (dr,cr) = (select CASE WHEN sum( Lc_debit)IS NULL THEN 0 ELSE sum( Lc_debit) END as dr,CASE WHEN sum( Lc_CREDIT)IS NULL THEN 0 ELSE sum( Lc_CREDIT) END as cr from acct_transactions b where loc_code in(" + Location + ") and doc_date between '" + FromDt + "' and '" + ToDt + "' and a.acct_code=b.acct_code)";
                cmd.ExecuteNonQuery();

            }
            catch { throw; }
            finally { cmd.Dispose(); }
        }

        /// </summary>
        //FOR ARABI LETTERS CONVETION
        public string getHexCode(string s)
        {
            string Hex = " ";
            char[] values = s.ToCharArray();
            foreach (char letter in values)
            {
                int value = Convert.ToInt32(letter);
                string hexOutput = String.Format("{0:X}", value);
                Hex = Hex.Trim().Length > 0 ? Hex + " " + hexOutput : hexOutput;
                // textBox2.Text = textBox2.Text + (char)Int32.Parse(hexOutput);
            }
            return Hex;
        }
        public static string getHexCodeSTRING(string s)
        {
            string Str = "";
            string[] hexValuesSplit = s.Split(' ');
            foreach (String hex in hexValuesSplit)
            {
                //if (hex == " ") continue;
                int value = Convert.ToInt32(hex, 16);
                string stringValue = Char.ConvertFromUtf32(value);
                char charValue = (char)value;
                Str = Str.Length > 0 ? Str + "" + charValue.ToString() : charValue.ToString();
            }
            return Str;
        }

        public static string getReverseString(string s)
        {
            if (s.Length <= 1) return s;
            string ret = "";
            for (int i = s.Length - 1; i >= 0; i--)
            {
                ret = ret + s[i];
            }
            return ret;
        }
        public enum BarcodeType
        {
            SHELF, WAREHOUSE, INHOUSE, DELIVERYNOTE, TEXTILES
        }

        public bool PrintBarcode(OracleConnection con, OracleTransaction tr, BarcodeType BarcodeType, string CompanyName, string Barcode, string PartDescripion, string Part_number_sup, string Qty, string Price, string BCP_PORT, bool ShowCompanyName, bool ShowQty, bool ShowPrice, bool ShowDescription, bool ShowPartNum, string Logos)
        {
            return PrintBarcode(con, tr, BarcodeType, CompanyName, Barcode, PartDescripion, Part_number_sup, Qty, Price, "", "", BCP_PORT, ShowCompanyName, ShowQty, ShowPrice, ShowDescription, ShowPartNum, false, false, "", 0, 0);
        }
        public bool PrintBarcode(OracleConnection con, OracleTransaction tr, BarcodeType BarcodeType, string CompanyName, string Barcode, string PartDescripion, string Part_number_sup, string Qty, string Price, string Packing, string OtherInfo, string BCP_PORT, bool ShowCompanyName, bool ShowQty, bool ShowPrice, bool ShowDescription, bool ShowPartNum, bool ShowOtherInfo, bool ShowPacking, string Logos)
        {
            return PrintBarcode(con, tr, BarcodeType, CompanyName, Barcode, PartDescripion, Part_number_sup, Qty, Price, Packing, OtherInfo, BCP_PORT, ShowCompanyName, ShowQty, ShowPrice, ShowDescription, ShowPartNum, ShowOtherInfo, ShowPacking, Logos, 0, 0);
        }
        public bool PrintBarcode(OracleConnection con, OracleTransaction tr, BarcodeType BarcodeType, string CompanyName, string Barcode, string PartDescripion, string Part_number_sup, string Qty, string Price, string Packing, string OtherInfo, string BCP_PORT, bool ShowCompanyName, bool ShowQty, bool ShowPrice, bool ShowDescription, bool ShowPartNum, bool ShowOtherInfo, bool ShowPacking, string Logos, int LeftMarginAdj, int TopMarginAdj)
        {
            string Cond = "";
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                switch (BarcodeType)
                {
                    case BarcodeType.DELIVERYNOTE:
                        Cond = " where LABEL_NAME='DELIVERYNOTE'";
                        break;
                    case BarcodeType.INHOUSE:
                        Cond = " where LABEL_NAME='INHOUSE'";
                        break;
                    case BarcodeType.TEXTILES:
                        Cond = " where LABEL_NAME='TEXTILE'";
                        break;
                    case BarcodeType.SHELF:
                        Cond = " where LABEL_NAME='SHELF'";
                        break;
                    case BarcodeType.WAREHOUSE:
                        Cond = " where LABEL_NAME='WAREHOUSE'";
                        break;
                    default:
                        Cond = " where LABEL_NAME='INHOUSE'";
                        break;
                }

                clsBarcode bc = new clsBarcode();
                string Sql = "select * from invt_labelconfig " + Cond;
                cmd = con.CreateCommand();
                cmd.CommandText = Sql;
                if (tr != null) cmd.Transaction = tr;
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    bc.BarcodeH = rs["BarcodeH"].ToString();
                    bc.BarcodeN = rs["BarcodeN"].ToString();
                    bc.BarcodeType = rs["BarcodeType"].ToString();
                    bc.BarcodeX = (int.Parse(rs["BarcodeX"].ToString()) + LeftMarginAdj).ToString();
                    bc.BarcodeY = (int.Parse(rs["BarcodeY"].ToString()) + TopMarginAdj).ToString();

                    bc.CheckDigit = rs["CheckDigit"].ToString().Equals("1") ? true : false;

                    bc.CompanyNameFont = rs["BarcodeH"].ToString();
                    bc.CompanyNameH = rs["CompanyNameH"].ToString();
                    bc.CompanyNameW = rs["CompanyNameW"].ToString();
                    bc.CompanyNameX = rs["CompanyNameX"].ToString();
                    bc.CompanyNameY = rs["CompanyNameY"].ToString();
                    bc.Darkness = rs["Darkness"].ToString();
                    bc.Interpretation = rs["Interpretation"].ToString().Equals("1") ? true : false;
                    bc.InterpretationAbove = rs["InterpretationAbove"].ToString().Equals("1") ? true : false;
                    bc.LogoL = rs["LogoL"].ToString();
                    bc.logos = Logos;
                    bc.LogoT = rs["LogoT"].ToString();
                    bc.Orientation = rs["Orientation"].ToString();

                    bc.PartDescriptionFont = rs["PartDescriptionFont"].ToString();
                    bc.PartDescriptionH = rs["PartDescriptionH"].ToString();
                    bc.PartDescriptionW = rs["PartDescriptionW"].ToString();
                    bc.PartDescriptionX = rs["PartDescriptionX"].ToString();
                    bc.PartDescriptionY = rs["PartDescriptionY"].ToString();

                    bc.PartNumberFont = rs["PartNumberFont"].ToString();
                    bc.PartNumberH = rs["PartNumberH"].ToString();
                    bc.PartNumberW = rs["PartNumberW"].ToString();
                    bc.PartNumberX = rs["PartNumberX"].ToString();
                    bc.PartNumberY = rs["PartNumberY"].ToString();

                    bc.PriceFont = rs["PriceFont"].ToString();
                    bc.PriceH = rs["PriceH"].ToString();
                    bc.PriceW = rs["PriceW"].ToString();
                    bc.PriceX = rs["PriceX"].ToString();
                    bc.PriceY = rs["PriceY"].ToString();

                    bc.PackFont = rs["PackFont"].ToString();
                    bc.PackH = rs["PackH"].ToString();
                    bc.PackW = rs["PackW"].ToString();
                    bc.PackX = rs["PackX"].ToString();
                    bc.PackY = rs["PackY"].ToString();

                    bc.OTHERINFOFONT = rs["OTHERINFOFONT"].ToString();
                    bc.OTHERINFOH = rs["OTHERINFOH"].ToString();
                    bc.OTHERINFOW = rs["OTHERINFOW"].ToString();
                    bc.OTHERINFOX = rs["OTHERINFOX"].ToString();
                    bc.OTHERINFOY = rs["OTHERINFOY"].ToString();
                }


                bc.CompanyName = CompanyName;
                bc.BCP_Port = BCP_PORT;
                bc.PartDescription = PartDescripion;
                bc.Price = Price;
                bc.PartNumber = Part_number_sup;
                bc.Quantity = Qty;
                bc.ShowCompany = ShowCompanyName;
                bc.ShowDescription = ShowDescription;
                bc.ShowPartNumber = ShowPartNum;
                bc.ShowPrice = ShowPrice;
                bc.ShowPacking = ShowPacking;
                bc.ShowOtherInfo = ShowOtherInfo;
                bc.TestNumber = Barcode;
                bc.PACKING = Packing;
                bc.OTHERINFO = OtherInfo;
                bc.Zebraprint();
            }
            catch { throw; }
            finally
            {
                if (rs != null) rs.Close();
                if (cmd != null) cmd.Dispose();
            }
            return true;
        }


        // TO GENERAL

        public enum RegSettings
        {
            FirstRun, LastUserName, LastLogoutTime, Version, ApplicationPath, ReportsVersion, DependencyVersion, AutoUpdate, AutoUpdateDate
        }

        public bool SetRegistry(RegSettings reg, string Text)
        {
            RegistryKey key = null;
            try
            {
                key = Registry.LocalMachine.OpenSubKey("Software\\MALL\\SETTINGS", true);

                // If the return value is null, the key doesn't exist
                if (key == null)
                {
                    // The key doesn't exist; create it / open it
                    key = Registry.LocalMachine.CreateSubKey("Software\\MALL\\SETTINGS");
                }

                //SAVE SETTING
                switch (reg)
                {
                    case RegSettings.FirstRun:
                        key.SetValue("FIRSTRUN", DateTime.Today.ToString());
                        key.SetValue("AutoUpdateDate", DateTime.Now.ToString());
                        break;
                    case RegSettings.LastUserName:
                        key.SetValue("LastUserName", Defaults.Def_User);
                        break;
                    case RegSettings.LastLogoutTime:
                        key.SetValue("LastLogoutTime", DateTime.Now.ToString());
                        break;
                    case RegSettings.Version:
                        key.SetValue("Version", Text.ToString());
                        break;
                    case RegSettings.ApplicationPath:
                        key.SetValue("ApplicationPath", Text.ToString());
                        break;
                    case RegSettings.ReportsVersion:
                        key.SetValue("ReportsVersion", Text.ToString());
                        break;
                    case RegSettings.DependencyVersion:
                        key.SetValue("DependencyVersion", Text.ToString());
                        break;
                    case RegSettings.AutoUpdate:
                        key.SetValue("AutoUpdate", Text.ToString());
                        break;
                    case RegSettings.AutoUpdateDate:
                        key.SetValue("AutoUpdateDate", DateTime.Now.ToString());
                        break;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                if (key != null)
                {
                    key.Close();
                }
            }
            return true;
        }

        public string GetRegistry(RegSettings reg)
        {
            RegistryKey key = null;
            string st = "";
            try
            {
                key = Registry.LocalMachine.OpenSubKey("Software\\MALL\\SETTINGS");
                if (key == null)
                    return null;

                switch (reg)
                {
                    case RegSettings.FirstRun:
                        return key.GetValue("FIRSTRUN").ToString();

                    case RegSettings.LastUserName:
                        return key.GetValue("LastUserName").ToString();

                    case RegSettings.LastLogoutTime:
                        return key.GetValue("LastLogoutTime").ToString();

                    case RegSettings.Version:
                        return key.GetValue("Version").ToString();

                    case RegSettings.ApplicationPath:
                        return key.GetValue("ApplicationPath").ToString();

                    case RegSettings.DependencyVersion:
                        return key.GetValue("DependencyVersion").ToString();

                    case RegSettings.ReportsVersion:
                        return key.GetValue("ReportsVersion").ToString();
                    case RegSettings.AutoUpdateDate:
                        return key.GetValue("AutoUpdateDate").ToString();

                    case RegSettings.AutoUpdate:
                        try
                        {
                            st = key.GetValue("AutoUpdate").ToString();
                        }
                        catch
                        {
                            if (st == "")
                            {
                                SetRegistry(RegSettings.AutoUpdate, "True");
                                st = "True";
                            }
                        }
                        return st;
                }
            }
            catch
            {
                return null;
            }
            finally
            {
                if (key != null) key.Close();
            }
            return null;
        }

        public static bool Registar_Dlls()
        {
            // DEFINE DLL FILES HERE
            string[] Files = { "Barcode.dll" };    // { "PrintProj.dll" };
            //
            string filePath = "";
            try
            {
                foreach (string F in Files)
                {
                    filePath = "\"" + Application.StartupPath + "\\" + F + "\"";
                    Process reg = new Process();
                    //This file registers .dll files as command components in the registry.
                    reg.StartInfo.FileName = "regsvr32.exe";
                    reg.StartInfo.Arguments = filePath;
                    reg.StartInfo.UseShellExecute = false;
                    reg.StartInfo.CreateNoWindow = true;
                    reg.StartInfo.RedirectStandardOutput = true;
                    reg.Start();
                    reg.WaitForExit();
                    reg.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool ChangeDateFormat()
        {
            try
            {
                string[] Mnth = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
                string Dt = DateTime.Today.ToString().ToUpper();

                foreach (string st in Mnth)
                {
                    if (Dt.Contains(st))
                        return true;
                }

                string DateFormat = "dd/MMM/yyyy";
                string TimeFormat = "hh:mm:ss tt";
                string Currency = "QRS";
                RegistryKey rkey = Registry.CurrentUser.OpenSubKey(@"Control Panel\International", true);
                rkey.SetValue("sTimeFormat", TimeFormat);
                rkey.SetValue("sShortDate", DateFormat);
                rkey.SetValue("sCurrency", Currency);
                rkey.Close();
                MessageBox.Show("Please Reastart The Program");
                Application.Restart();
                return true;
            }
            catch
            {
                MessageBox.Show("SET DATE FORMAT TO dd/MMM/yyyy", Application.CompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
                return false;
            }
        }

        public bool CheckDocumentEditable(string DocumentNumber, string DocumentType, string DocumentDate, OracleConnection Conn, OracleTransaction Tr)
        {
            OracleDataReader Rs = null;
            OracleCommand Cmd = null;
            string Sql = "";
            string Condition = "";
            int EditDays = 0;

            try
            {
                string ServerDate = this.GetValue("SELECT SYSDATE FROM DUAL", Conn, Tr);
                ServerDate = DateTime.Parse(ServerDate).ToString("dd/MMM/yyyy");
                if (DocumentType != "") Condition = " Where DOC_TYPE='" + DocumentType + "'";
                Sql = "Select DOC_TYPE,EDIT_DAYS From SYS_LOCK_DAYS" + Condition;
                Cmd = Conn.CreateCommand();
                Cmd.CommandText = Sql;
                if (Tr != null) Cmd.Transaction = Tr;
                Rs = Cmd.ExecuteReader();

                if (Rs.HasRows)
                {
                    if (Rs.Read())
                    {
                        EditDays = int.Parse(Rs["EDIT_DAYS"].ToString());
                        if (EditDays > 0)
                        {
                            int D = (DateTime.Parse(ServerDate) - DateTime.Parse(DocumentDate)).Days;
                            if (D < EditDays)
                            {
                                MessageBox.Show("Days from today: Diff" + D + " , EditDays" + EditDays + " ,  [ServerDate-DocDate][" + ServerDate + "-" + DocumentDate + "]");
                                return true;
                            }
                        }   //else { goto UnblockedDoc; }
                    }//   else { goto UnblockedDoc; }
                }//            UnblockedDoc:
                if (Cmd != null) Cmd.Dispose();
                if (Rs != null)
                {
                    Rs.Close();
                    Rs.Dispose();
                }
                Sql = "Select DOC_NO,UNLOCK_DATE,USER_ID From SYS_UNLOCK_DOCS Where DOC_NO='" + DocumentNumber + "'";

                Cmd = Conn.CreateCommand();
                Cmd.CommandText = Sql;
                if (Tr != null) Cmd.Transaction = Tr;
                Rs = Cmd.ExecuteReader();
                if (Rs.HasRows)
                {
                    if (Rs.Read())
                    {
                        if (Rs["DOC_NO"].ToString().Equals(DocumentNumber))
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                }
                return false;
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error :" + Exp.Message, "E", "");
                return false;
            }
            finally
            {
                if (Rs != null) Rs.Dispose();
                if (Cmd != null) Cmd.Dispose();
            }
            return false;
        }


        //   THE RE-ORDER FUNCTIONS BELOW


        decimal varDecAdjustPercent = 10;
        public string GenerateReorderFormatReport(string stLocation, string stSupplier, string stSupDivision, string stPurDivision, string stBrand, string stCategory, bool isGroupCategory, string stPartNumberSup, string LocationCode)
        {
            string Sql = "";
            string Condition = "";
            OracleCommand Cmd = null;
            OracleConnection oraConn = new OracleConnection();
            OracleDataReader Rs = null;
            try
            {
                oraConn = getPooledConnection(Defaults.Def_SERVER_DB);
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error On Connection.\n\r" + Exp.Message, "E", "");
                return "";
            }
            try
            {

                # region Condition

                if (!(stSupplier.Equals("")))
                {
                    Condition = Condition + " And a.SUPPLIER_CODE='" + stSupplier + "'";
                    if (!(stSupDivision.Equals("")))
                    {
                        Condition = Condition + " And a.DIVISION_CODE='" + stSupDivision + "'";
                    }
                }
                if (!(stBrand.Equals("")))
                {
                    Condition = Condition + " And a.BRAND_CODE='" + stBrand + "'";
                }

                if (!(stPartNumberSup.Equals("")))
                {
                    if (!(stPartNumberSup.Contains("-")))
                    {
                        Condition = Condition + " And a.PART_NUMBER_SUP='" + stPartNumberSup + "'";
                    }
                    else
                    {
                        //Condition = Condition + " And a.PART_NUMBER_SUP='" + stPartNumberSup.Remove(stPartNumberSup.IndexOf("-")) + "' And b.ZERO_SERIAL=" + 0 + "";
                    }
                }
                if (!(stCategory.Equals("")))
                {
                    if (!isGroupCategory)
                    {
                        Condition = Condition + " And a.CATEGORY_CODE='" + stCategory + "'";
                    }
                    else
                    {
                        Condition = Condition + " And b.CAT_0='" + stCategory.Substring(0, 2) + "'";

                        if (!(stCategory.Substring(2, 2).Equals("00")))
                        {
                            Condition = Condition + " And b.CAT_1='" + stCategory.Substring(2, 2) + "'";
                            //Sql = Sql + " And a.CAT_0='" + stCategory.Substring(0, 2) + "'" +
                            //            " And a.CAT_1='" + stCategory.Substring(2, 2) + "'";
                        }
                        if (!(stCategory.Substring(4, 2).Equals("00")))
                        {
                            Condition = Condition + " And b.CAT_2='" + stCategory.Substring(4, 2) + "'";
                        }
                        if (!(stCategory.Substring(6, 2).Equals("00")))
                        {
                            Condition = Condition + " And b.CAT_3='" + stCategory.Substring(6, 2) + "'";
                        }
                        if (!(stCategory.Substring(8, 2).Equals("00")))
                        {
                            Condition = Condition + " And b.CAT_4='" + stCategory.Substring(8, 2) + "'";
                        }
                        if (!(stCategory.Substring(10, 2).Equals("00")))
                        {
                            Condition = Condition + " And b.CAT_5='" + stCategory.Substring(10, 2) + "'";
                        }
                    }
                }

                if (!(stPurDivision.Equals("")))
                {
                    Condition = Condition + " And b.DIVISION_CODE_INTERNAL='" + stPurDivision + "'";
                }

                # endregion Condition

                Sql = "Select a.part_number,a.part_number_sup,a.supplier_itemcode,a.part_description_pack," +
                    "a.zero_serial,a.packing,(case when a.linked_item is null then 'N' else 'Y' end) as is_linked_item," +
                    "a.linked_item,a.packqty,a.pack_multiply_qty," +
                    "a.buying_rate,a.packing_order,a.productpackings," +
                    "b.supplier_code,b.supplier_name,b.division_code,b.division_name," +
                    "b.brand_code,b.brand_name,b.category_code,b.category_name," +
                    "b.division_code_internal,b.division_name_internal," +
                    //"c.last_grv_date,c.last_grv_qty,(c.last_grv_qty/a.pack_multiply_qty) as clast_grv_qty,c.lc,c.lc_purch," +
                    //"d.cb_qty,(d.cb_qty/a.pack_multiply_qty) as ccb_qty" +
                    "nvl(c.last_grv_date,'31/Dec/9999') as last_grv_date," +
                    "nvl(c.last_grv_qty,0) as last_grv_qty," +
                    "nvl((c.last_grv_qty / a.pack_multiply_qty),0) as clast_grv_qty," +
                    "nvl(c.lc,0) as lc," +
                    "nvl(c.lc_purch,0)as lc_purch," +
                    "nvl(d.cb_qty,0) as cb_qty," +
                    "nvl((d.cb_qty / a.pack_multiply_qty),0) as ccb_qty" +
                    " from (((invt_itempacking a inner join invt_inventorymaster b" +
                    " on a.supplier_code=b.supplier_code and a.division_code=b.division_code" +
                    " and a.part_number_sup=b.part_number_sup" +
                    //" and a.supplier_code='" + txtSupplier.Text + "'"+
                    //" and a.division_code ='" + txtSupDivision.Text + "'"+
                    Condition + " And a.INACTIVE_ITEM='N' And a.LOCKED_GRV='N' And a.BLOCKED_PACKING='N'" +
                    " and a.default_unit='Y' and a.default_packing='Y')" +
                    " left join invt_itemtran_dtls c on c.loc_code='" + stLocation + "'" +
                    " and  a.part_number_sup=c.part_number_sup and a.zero_serial=c.zero_serial)" +
                    " left join invt_inventorybalance d on d.loc_code='" + stLocation + "'" +
                    " and  a.part_number_sup=d.part_number_sup and a.zero_serial=d.zero_serial)";

                Cmd = oraConn.CreateCommand();

                Cmd.CommandText = Sql;
                Rs = Cmd.ExecuteReader();
                if (Rs.HasRows)
                {
                    decimal decAverageSaleQty = 0;
                    decimal decCurStock = 0;
                    decimal decPendingLPOQty = 0;
                    decimal decExpectDays = 0;
                    decimal decStockLimit = 0;
                    decimal decPackMulQty = 1;
                    decimal decMaximumOrderQty = 0;
                    decimal decRequiredQty = 0;
                    string ReturnString = "";

                    string stLinkedProducts = "";
                    while (Rs.Read())
                    {
                        # region Calculation

                        decAverageSaleQty = 0; //IN Basic Unit Numbers
                        decCurStock = 0;       //IN Basic Unit Numbers
                        decPendingLPOQty = 0;  //IN LUQ Unit Numbers
                        decExpectDays = 0;
                        decStockLimit = 0;     //IN Basic Unit Numbers SAME decAverageSaleQty
                        decPackMulQty = 1;
                        decMaximumOrderQty = 0;//IN Basic Unit Numbers

                        stLinkedProducts = ""; //For Linked Products 

                        if (Rs["part_number_sup"].ToString().Equals("016217"))
                        {

                        }
                        if (Rs["part_number_sup"].ToString().Equals("03301037"))
                        {

                        }

                        if (Rs["IS_LINKED_ITEM"].ToString().Equals("Y"))
                        {
                            stLinkedProducts = getLinkedProducts(Rs["LINKED_ITEM"].ToString(), oraConn, null);
                        }


                        if (Rs["IS_LINKED_ITEM"].ToString().Equals("N"))
                        {
                            decAverageSaleQty = getAverageSaleQty(stLocation, Rs["part_number_sup"].ToString(), int.Parse(Rs["zero_serial"].ToString()), varDecAdjustPercent, oraConn, null);
                            decCurStock = getStock(stLocation, Rs["part_number_sup"].ToString(), int.Parse(Rs["zero_serial"].ToString()), oraConn, null);
                            decPendingLPOQty = getPendingLPO_Qty(stLocation, Rs["part_number_sup"].ToString(), int.Parse(Rs["zero_serial"].ToString()), oraConn);
                        }
                        else
                        {
                            decAverageSaleQty = getLinkedAverageSaleQty(stLocation, stLinkedProducts, Rs["part_number_sup"].ToString(), int.Parse(Rs["zero_serial"].ToString()), varDecAdjustPercent, oraConn, null);
                            decCurStock = getLinkedStock(stLocation, stLinkedProducts, Rs["part_number_sup"].ToString(), int.Parse(Rs["zero_serial"].ToString()), oraConn, null);
                            decPendingLPOQty = getLinkedPendingLPO_Qty(stLocation, stLinkedProducts, Rs["part_number_sup"].ToString(), int.Parse(Rs["zero_serial"].ToString()), oraConn);
                        }

                        if (decAverageSaleQty < 0) decAverageSaleQty = 0;
                        decStockLimit = decAverageSaleQty;

                        // Commented as per Abhi & Anoop 24/Sep/2014
                        //Commented as per Abhi & Anoop 24/Sep/2014//if (decCurStock < 0) decCurStock = 0;                        


                        if (!(int.Parse(Rs["zero_serial"].ToString()).Equals(0)))
                        {
                            decPackMulQty = decimal.Parse(Rs["pack_multiply_qty"].ToString());
                            if (decPackMulQty > 1)
                            {
                            }
                            decPendingLPOQty = decPendingLPOQty / decPackMulQty;
                        }
                        if (decAverageSaleQty > 0)
                        {
                            decExpectDays = (((decCurStock + decPendingLPOQty) / decAverageSaleQty) * 30);
                        }
                        decMaximumOrderQty = (decAverageSaleQty - (decCurStock + decPendingLPOQty));

                        # endregion Calculation

                        decimal decThisWeek = 0;
                        decimal decWeek1 = 0;
                        decimal decWeek2 = 0;
                        decimal decWeek3 = 0;

                        decimal decThisMonth = 0;
                        decimal decMonth1 = 0;
                        decimal decMonth2 = 0;
                        decimal decMonth3 = 0;

                        if (Rs["IS_LINKED_ITEM"].ToString().Equals("N"))
                        {
                            getWeeklySaleQty(DateTime.Today.ToString("dd/MMM/yyyy"), LocationCode,
                                Rs["PART_NUMBER_SUP"].ToString(), int.Parse(Rs["ZERO_SERIAL"].ToString()),
                                out decThisWeek, out decWeek1, out decWeek2, out decWeek3, oraConn, null);

                            getMonthlySaleQty(DateTime.Today.ToString("dd/MMM/yyyy"), LocationCode,
                                Rs["PART_NUMBER_SUP"].ToString(), int.Parse(Rs["ZERO_SERIAL"].ToString()),
                                out decThisMonth, out decMonth1, out decMonth2, out decMonth3, oraConn, null);
                        }
                        else
                        {

                            getLinkedWeeklySaleQty(DateTime.Today.ToString("dd/MMM/yyyy"), LocationCode, stLinkedProducts,
                                Rs["PART_NUMBER_SUP"].ToString(), int.Parse(Rs["ZERO_SERIAL"].ToString()),
                                out decThisWeek, out decWeek1, out decWeek2, out decWeek3, oraConn, null);

                            getLinkedMonthlySaleQty(DateTime.Today.ToString("dd/MMM/yyyy"), LocationCode, stLinkedProducts,
                                Rs["PART_NUMBER_SUP"].ToString(), int.Parse(Rs["ZERO_SERIAL"].ToString()),
                                out decThisMonth, out decMonth1, out decMonth2, out decMonth3, oraConn, null);
                        }

                        ReturnString = "L.GRVQ" + Rs["clast_grv_qty"].ToString() + ", " + DateTime.Parse(Rs["last_grv_date"].ToString()).ToString("dd/MMM/yyyy")
                            + ",  Buy: " + decimal.Parse(Rs["buying_rate"].ToString()) + "  ,W0: " + decThisWeek + "  ,W1: " + decWeek1 + "  ,W2: " + decWeek2 + "  ,W3: " + decWeek3
                            + "  ,M0: " + decThisMonth + "  ,M1: " + decMonth1 + "  ,M2: " + decMonth2 + "  ,M3: " + decMonth3;
                    }
                    return ReturnString;
                }
                else
                {
                    throw (new Exception("Product Not Found"));
                }
                return "";
            }
            catch (Exception Exp)
            {
                DataConnector.Message("Error :\n\r" + Exp.Message, "E", "");
                Log_Error(Defaults.Def_LOC, "Reorder List Report", "GenerateReorderFormatReport()", "GenerateReorderFormatReport()", Sql, Exp.ToString(), "", "Y");
            }
            finally
            {
                Rs.Close();
                Rs.Dispose();
            }
            return "";
        }

        private bool getWeeklySaleQty(string stBaseDate, string stLocation, string stPartNumberSup, int intZeroSerial,
            out decimal decCurrWeekSale, out decimal decWeek1Sale, out decimal decWeek2Sale, out decimal decWeek3Sale,
            OracleConnection Conn, OracleTransaction Tr)
        {
            string Sql = "";
            decCurrWeekSale = 0;
            decWeek1Sale = 0;
            decWeek2Sale = 0;
            decWeek3Sale = 0;

            Sql = "Select part_number_sup,zero_serial,NVL(sum(curr_week_sale),0) as curr_week_sale," +
                "NVL(sum(week1_sale),0) as week1_sale,NVL(sum(week2_sale),0)as week2_sale," +
                "NVL(sum(week3_sale),0)as week3_sale from " +
                " ( " +
                "Select part_number_sup,zero_serial," +
                "case when(doc_date)>=sysdate-7 then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as curr_week_sale," +
                "case when(doc_date)<sysdate-7 and (doc_date)>=sysdate-14 then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as week1_sale," +
                "case when(doc_date)<sysdate-14 and (doc_date)>=sysdate-21 then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as week2_sale," +
                "case when(doc_date)<sysdate-21 and (doc_date)>=sysdate-28 then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as week3_sale " +
                "From invt_itemtransaction " +
                "Where doc_date>=(sysdate-28) and loc_code='" + stLocation + "' and part_number_sup='" + stPartNumberSup + "' and zero_serial=" + intZeroSerial + "" +
                " group by part_number_sup,zero_serial,doc_date" +
                " ) " +
                "group by part_number_sup,zero_serial " +
                "order by  part_number_sup,zero_serial";

            OracleDataReader oDr = null;
            OracleCommand oCmd = new OracleCommand(Sql, Conn);
            if (Tr != null) oCmd.Transaction = Tr;
            try
            {
                oDr = oCmd.ExecuteReader();
                if (oDr.HasRows)
                {
                    if (oDr.Read())
                    {
                        decCurrWeekSale = decimal.Parse(oDr["curr_week_sale"].ToString());
                        decWeek1Sale = decimal.Parse(oDr["week1_sale"].ToString());
                        decWeek2Sale = decimal.Parse(oDr["week2_sale"].ToString());
                        decWeek3Sale = decimal.Parse(oDr["week3_sale"].ToString());
                    }
                    return true;
                }
            }
            catch (Exception Exp)
            {
                throw;
            }
            finally
            {
                oDr.Close();
                oCmd.Dispose();
            }
            return false;
        }

        private bool getLinkedWeeklySaleQty(string stBaseDate, string stLocation, string stLinkedProducts, string stPartNumberSup, int intZeroSerial,
     out decimal decCurrWeekSale, out decimal decWeek1Sale, out decimal decWeek2Sale, out decimal decWeek3Sale,
     OracleConnection Conn, OracleTransaction Tr)
        {
            string Sql = "";
            decCurrWeekSale = 0;
            decWeek1Sale = 0;
            decWeek2Sale = 0;
            decWeek3Sale = 0;

            Sql = "Select part_number_sup,zero_serial,NVL(sum(curr_week_sale),0) as curr_week_sale," +
                "NVL(sum(week1_sale),0) as week1_sale,NVL(sum(week2_sale),0)as week2_sale," +
                "NVL(sum(week3_sale),0)as week3_sale from " +
                " ( " +
                "Select '" + stPartNumberSup + "' as part_number_sup,zero_serial," +
                "case when(doc_date)>=sysdate-7 then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as curr_week_sale," +
                "case when(doc_date)<sysdate-7 and (doc_date)>=sysdate-14 then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as week1_sale," +
                "case when(doc_date)<sysdate-14 and (doc_date)>=sysdate-21 then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as week2_sale," +
                "case when(doc_date)<sysdate-21 and (doc_date)>=sysdate-28 then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as week3_sale " +
                "From invt_itemtransaction " +
                "Where doc_date>=(sysdate-28) and loc_code='" + stLocation + "' and part_number_sup in(" + stLinkedProducts + ") and zero_serial=" + intZeroSerial + "" +
                " group by part_number_sup,zero_serial,doc_date" +
                " ) " +
                "group by part_number_sup,zero_serial " +
                "order by  part_number_sup,zero_serial";

            OracleDataReader oDr = null;
            OracleCommand oCmd = new OracleCommand(Sql, Conn);
            if (Tr != null) oCmd.Transaction = Tr;
            try
            {
                oDr = oCmd.ExecuteReader();
                if (oDr.HasRows)
                {
                    if (oDr.Read())
                    {
                        decCurrWeekSale = decimal.Parse(oDr["curr_week_sale"].ToString());
                        decWeek1Sale = decimal.Parse(oDr["week1_sale"].ToString());
                        decWeek2Sale = decimal.Parse(oDr["week2_sale"].ToString());
                        decWeek3Sale = decimal.Parse(oDr["week3_sale"].ToString());
                    }
                    return true;
                }
            }
            catch (Exception Exp)
            {
                throw;
            }
            finally
            {
                oDr.Close();
                oCmd.Dispose();
            }
            return false;
        }
        private bool getMonthlySaleQty(string stBaseDate, string stLocation, string stPartNumberSup, int intZeroSerial,
            out decimal decCurrMonthSale, out decimal decMonth1Sale, out decimal decMonth2Sale, out decimal decMonth3Sale,
            OracleConnection Conn, OracleTransaction Tr)
        {
            string Sql = "";
            decCurrMonthSale = 0;
            decMonth1Sale = 0;
            decMonth2Sale = 0;
            decMonth3Sale = 0;

            Sql = "Select part_number_sup,zero_serial,NVL(sum(curr_month_sale),0)as curr_month_sale," +
                "NVL(sum(month_1sale),0)as month_1sale,NVL(sum(month_2sale),0)as month_2sale," +
                "NVL(sum(month_3sale),0)as month_3sale From " +
                " ( " +
                "Select part_number_sup,zero_serial," +
                "case when(to_char(doc_date,'MON')=to_char( sysdate,'MON')) then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as curr_month_sale," +
                "case when(to_char(doc_date,'MON')=to_char( add_months( sysdate,-1),'MON')) then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as month_1sale," +
                "case when(to_char(doc_date,'MON')=to_char( add_months( sysdate,-2),'MON')) then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as month_2sale," +
                "case when(to_char(doc_date,'MON')=to_char( add_months( sysdate,-3),'MON')) then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as month_3sale " +
                "From invt_itemtransaction " +
                "Where doc_date>=add_months(sysdate,-4) and loc_code='" + stLocation + "' and part_number_sup='" + stPartNumberSup + "' and zero_serial=" + intZeroSerial + "" +
                " group by part_number_sup,zero_serial,to_char(doc_date,'MON')" +
                " ) " +
                " Group By part_number_sup,zero_serial" +
                " Order By  part_number_sup,zero_serial";

            OracleDataReader oDr = null;
            OracleCommand oCmd = new OracleCommand(Sql, Conn);
            if (Tr != null) oCmd.Transaction = Tr;
            try
            {
                oDr = oCmd.ExecuteReader();
                if (oDr.HasRows)
                {
                    if (oDr.Read())
                    {
                        decCurrMonthSale = decimal.Parse(oDr["curr_month_sale"].ToString());
                        decMonth1Sale = decimal.Parse(oDr["month_1sale"].ToString());
                        decMonth2Sale = decimal.Parse(oDr["month_2sale"].ToString());
                        decMonth3Sale = decimal.Parse(oDr["month_3sale"].ToString());
                    }
                    return true;
                }
            }
            catch (Exception Exp)
            {
                throw;
            }
            finally
            {
                oDr.Close();
                oCmd.Dispose();
            }
            return false;
        }
        private bool getLinkedMonthlySaleQty(string stBaseDate, string stLocation, string stLinkedProducts, string stPartNumberSup, int intZeroSerial,
            out decimal decCurrMonthSale, out decimal decMonth1Sale, out decimal decMonth2Sale, out decimal decMonth3Sale,
            OracleConnection Conn, OracleTransaction Tr)
        {
            string Sql = "";
            decCurrMonthSale = 0;
            decMonth1Sale = 0;
            decMonth2Sale = 0;
            decMonth3Sale = 0;

            Sql = "Select part_number_sup,zero_serial,NVL(sum(curr_month_sale),0)as curr_month_sale," +
                "NVL(sum(month_1sale),0)as month_1sale,NVL(sum(month_2sale),0)as month_2sale," +
                "NVL(sum(month_3sale),0)as month_3sale From " +
                " ( " +
                "Select '" + stPartNumberSup + "' as part_number_sup,zero_serial," +
                "case when(to_char(doc_date,'MON')=to_char( sysdate,'MON')) then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as curr_month_sale," +
                "case when(to_char(doc_date,'MON')=to_char( add_months( sysdate,-1),'MON')) then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as month_1sale," +
                "case when(to_char(doc_date,'MON')=to_char( add_months( sysdate,-2),'MON')) then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as month_2sale," +
                "case when(to_char(doc_date,'MON')=to_char( add_months( sysdate,-3),'MON')) then sum((sale_qty-sale_r_qty)/pack_multiply_qty) end as month_3sale " +
                "From invt_itemtransaction " +
                "Where doc_date>=add_months(sysdate,-4) and loc_code='" + stLocation + "' and part_number_sup in(" + stLinkedProducts + ") and zero_serial=" + intZeroSerial + "" +
                " group by part_number_sup,zero_serial,to_char(doc_date,'MON')" +
                " ) " +
                " Group By part_number_sup,zero_serial" +
                " Order By  part_number_sup,zero_serial";

            OracleDataReader oDr = null;
            OracleCommand oCmd = new OracleCommand(Sql, Conn);
            if (Tr != null) oCmd.Transaction = Tr;
            try
            {
                oDr = oCmd.ExecuteReader();
                if (oDr.HasRows)
                {
                    if (oDr.Read())
                    {
                        decCurrMonthSale = decimal.Parse(oDr["curr_month_sale"].ToString());
                        decMonth1Sale = decimal.Parse(oDr["month_1sale"].ToString());
                        decMonth2Sale = decimal.Parse(oDr["month_2sale"].ToString());
                        decMonth3Sale = decimal.Parse(oDr["month_3sale"].ToString());
                    }
                    return true;
                }
            }
            catch (Exception Exp)
            {
                throw;
            }
            finally
            {
                oDr.Close();
                oCmd.Dispose();
            }
            return false;
        }

        private string getLinkedProducts(string stLinkedPartNumber, OracleConnection Conn, OracleTransaction Tr)
        {
            string Sql = "";
            string stLinkedProducts = "";
            Sql = "Select distinct part_number_sup From invt_itempacking Where LINKED_ITEM='" + stLinkedPartNumber + "' and zero_serial=0";
            OracleDataReader oDr = null;
            OracleCommand oCmd = new OracleCommand(Sql, Conn);
            if (Tr != null) oCmd.Transaction = Tr;
            try
            {
                oDr = oCmd.ExecuteReader();
                if (oDr.HasRows)
                {
                    while (oDr.Read())
                    {
                        if (stLinkedProducts.Equals(""))
                        {
                            stLinkedProducts = "'" + oDr["part_number_sup"].ToString() + "'";
                        }
                        else
                        {
                            stLinkedProducts = stLinkedProducts + ",'" + oDr["part_number_sup"].ToString() + "'";
                        }
                    }
                    return stLinkedProducts;
                }
                throw new Exception("Linked Product Details Not Found.");
            }
            catch (Exception Exp)
            {
                throw;
            }
            finally
            {
                oDr.Close();
                oCmd.Dispose();
            }
        }
        private decimal getAverageSaleQty(string stLocation, string stPartNumberSup, int intZeroSerial, decimal decAdjustPercent, OracleConnection oraConn, OracleTransaction Tr)
        {
            string Sql = "";
            Sql = "Select SUPPLIER_CODE,CATEGORY_CODE,BRAND_CODE,PART_NUMBER_SUP,ZERO_SERIAL,AVG_SQ,AVG_SV,AVG_SC From INVT_AVGSALE Where PART_NUMBER_SUP='01761751' And ZERO_SERIAL=0";
            Sql = "Select AVG_SQ,AVG_SV,AVG_SC From INVT_AVGSALE Where PART_NUMBER_SUP='" + stPartNumberSup + "' And ZERO_SERIAL=" + intZeroSerial;
            decimal decAverageSaleQty = 0;
            decimal decAverageSaleValue = 0;
            decimal decAverageSaleCost = 0;
            return getAverageSaleQty(stLocation, stPartNumberSup, intZeroSerial, decAdjustPercent, oraConn, Tr, out decAverageSaleValue, out decAverageSaleCost);
        }
        private decimal getAverageSaleQty(string stLocation, string stPartNumberSup, int intZeroSerial, decimal decAdjustPercent, OracleConnection oraConn, OracleTransaction Tr, out decimal decAverageSaleValue, out decimal decAverageSaleCost)
        {
            decimal decAverageSaleQty = 0;
            decAverageSaleValue = 0;
            decAverageSaleCost = 0;

            string Sql = "";
            Sql = "Select LOC_CODE,SUPPLIER_CODE,CATEGORY_CODE,BRAND_CODE,PART_NUMBER_SUP,ZERO_SERIAL,AVG_SQ,AVG_SV,AVG_SC From INVT_AVGSALE Where PART_NUMBER_SUP='01761751' And ZERO_SERIAL=0";
            Sql = "Select AVG_SQ,AVG_SV,AVG_SC From INVT_AVGSALE Where LOC_CODE='" + stLocation + "' And PART_NUMBER_SUP='" + stPartNumberSup + "' And ZERO_SERIAL=" + intZeroSerial;

            OracleCommand Cmd = null;
            OracleDataReader Rs = null;
            try
            {
                Sql = "Select round(AVG_SQ,2)as AVG_SQ,round(AVG_SV,2)as AVG_SV,round(AVG_SC,2)as AVG_SC From INVT_AVGSALE" +
                    " Where LOC_CODE='" + stLocation + "' And PART_NUMBER_SUP='" + stPartNumberSup + "' And ZERO_SERIAL=" + intZeroSerial + "";
                Cmd = oraConn.CreateCommand();
                if (Tr != null) Cmd.Transaction = Tr;
                Cmd.CommandText = Sql;
                Rs = Cmd.ExecuteReader();
                if (Rs.HasRows)
                {
                    if (Rs.Read())
                    {
                        decAverageSaleQty = decimal.Parse(Rs["AVG_SQ"].ToString());
                        decAverageSaleValue = decimal.Parse(Rs["AVG_SV"].ToString());
                        decAverageSaleCost = decimal.Parse(Rs["AVG_SC"].ToString());
                        if (decAdjustPercent > 0)
                        {
                            decAverageSaleQty = decAverageSaleQty + (decAverageSaleQty * decAdjustPercent / 100);
                        }
                    }
                    Rs.Close();
                }
                Cmd.Dispose();
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                if (Rs != null) Rs.Dispose();
                if (Cmd != null) Cmd.Dispose();
            }
            return decAverageSaleQty;
        }
        private decimal getPendingLPO_Qty(string stLocation, string stPartNumberSup, int intZeroSerial, OracleConnection Conn)
        {
            string Sql = "01761955";
            string PendingQty = "0";
            Sql = "Select sum(RCVD_QTY*PACK_MULTIPLY_QTY)as PENDING_QTY From INVT_PURCHASEORDERMAIN a,INVT_PURCHASEORDERSUB b" +
                  " Where a.loc_code in (select distinct a.loc_code from comn_location a inner join comn_location b on A.group_loc=b.group_loc where b.loc_code='" + stLocation + "' and a.active='Y') And b.PART_NUMBER_SUP='" + stPartNumberSup + "' And ZERO_SERIAL=" + intZeroSerial +
                  " And a.DOC_NO=b.DOC_NO And a.DOC_DATE >='" + DateTime.Today.AddDays(-15).ToString("dd/MMM/yyyy") + "' And CLOSED='N'";
            PendingQty = GetValue(Sql, Conn, null);
            if (PendingQty.Equals("")) PendingQty = "0";
            return decimal.Parse(PendingQty);
        }
        private decimal getOrderQtyOnList(bool boolUpdateList, int intUpdateRow, string stPartNumberSup, int intZeroSerial)
        {
            decimal decOrderQtyOnList = 0;
            //////decimal decOrderQtyOnUpdateRow = 0;
            //////try
            //////{
            //////    if (boolUpdateList)
            //////    {
            //////        if (gridPurchase["ItemCode", intUpdateRow].Value.ToString().Equals((intZeroSerial.Equals(0) ? stPartNumberSup : (stPartNumberSup + "-" + intZeroSerial))))
            //////        {
            //////            decOrderQtyOnUpdateRow = (decimal.Parse(gridPurchase["QTY", intUpdateRow].Value.ToString()) * decimal.Parse(gridPurchase["GridPACKMULQTY", intUpdateRow].Value.ToString()));
            //////        }
            //////    }
            //////    for (int i = 0; i < gridPurchase.RowCount - 1; i++)
            //////    {
            //////        if (gridPurchase["ItemCode", i].Value.ToString().Equals((intZeroSerial.Equals(0) ? stPartNumberSup : (stPartNumberSup + "-" + intZeroSerial))) &&
            //////            int.Parse(gridPurchase["ZeroSerial", i].Value.ToString()).Equals(intZeroSerial))
            //////        {
            //////            decOrderQtyOnList = decOrderQtyOnList + (decimal.Parse(gridPurchase["QTY", i].Value.ToString()) * decimal.Parse(gridPurchase["GridPACKMULQTY", i].Value.ToString()));
            //////        }
            //////    }
            //////    decOrderQtyOnList = decOrderQtyOnList - decOrderQtyOnUpdateRow;
            //////}
            //////catch (Exception Exp)
            //////{
            //////    throw (Exp);
            //////}
            //////finally
            //////{
            //////}
            return decOrderQtyOnList;
        }

        private decimal getLinkedStock(string grpLoc_code, string stLinkedProducts, string stPartNumberSup, int ZeroSerial, OracleConnection oraConn, OracleTransaction tr)
        {
            //string Sql = "select sum(cb_qty) from invt_inventorybalance where loc_code in (select distinct a.loc_code from comn_location a inner join comn_location b on A.group_loc=b.group_loc where b.loc_code='" + grpLoc_code + "' and a.active='Y') and part_number_sup='" + stPartNumberSup + "' and zero_serial=" + ZeroSerial;
            string Sql = "select round(sum(cb_qty/pack_multiply_qty),2)as cb_qty from invt_inventorybalance where loc_code in (select distinct a.loc_code from comn_location a inner join comn_location b on A.group_loc=b.group_loc where b.loc_code='" + grpLoc_code + "' and a.active='Y') and part_number_sup in(" + stLinkedProducts + ") and zero_serial=" + ZeroSerial;
            string ret = GetValue(Sql, oraConn, tr);
            return DataConnector.IsNumeric(ret) ? decimal.Parse(ret) : 0;
        }

        private decimal getLinkedAverageSaleQty(string stLocation, string stLinkedProducts, string stPartNumberSup, int intZeroSerial, decimal decAdjustPercent, OracleConnection oraConn, OracleTransaction Tr)
        {
            string Sql = "";
            Sql = "Select SUPPLIER_CODE,CATEGORY_CODE,BRAND_CODE,PART_NUMBER_SUP,ZERO_SERIAL,AVG_SQ,AVG_SV,AVG_SC From INVT_AVGSALE Where PART_NUMBER_SUP='01761751' And ZERO_SERIAL=0";
            Sql = "Select AVG_SQ,AVG_SV,AVG_SC From INVT_AVGSALE Where PART_NUMBER_SUP='" + stPartNumberSup + "' And ZERO_SERIAL=" + intZeroSerial;
            decimal decAverageSaleQty = 0;
            decimal decAverageSaleValue = 0;
            decimal decAverageSaleCost = 0;
            return getLinkedAverageSaleQty(stLocation, stLinkedProducts, stPartNumberSup, intZeroSerial, decAdjustPercent, oraConn, Tr, out decAverageSaleValue, out decAverageSaleCost);
        }
        private decimal getLinkedAverageSaleQty(string stLocation, string stLinkedProducts, string stPartNumberSup, int intZeroSerial, decimal decAdjustPercent, OracleConnection oraConn, OracleTransaction Tr, out decimal decAverageSaleValue, out decimal decAverageSaleCost)
        {
            decimal decAverageSaleQty = 0;
            decAverageSaleValue = 0;
            decAverageSaleCost = 0;

            string Sql = "";
            Sql = "Select LOC_CODE,SUPPLIER_CODE,CATEGORY_CODE,BRAND_CODE,PART_NUMBER_SUP,ZERO_SERIAL,AVG_SQ,AVG_SV,AVG_SC From INVT_AVGSALE Where PART_NUMBER_SUP='01761751' And ZERO_SERIAL=0";
            Sql = "Select AVG_SQ,AVG_SV,AVG_SC From INVT_AVGSALE Where LOC_CODE='" + stLocation + "' And PART_NUMBER_SUP='" + stPartNumberSup + "' And ZERO_SERIAL=" + intZeroSerial;

            OracleCommand Cmd = null;
            OracleDataReader Rs = null;
            try
            {
                Sql = "Select NVL(round(sum(AVG_SQ),2),0)as AVG_SQ,NVL(round(sum(AVG_SV),2),0)as AVG_SV,NVL(round(sum(AVG_SC),2),0)as AVG_SC From INVT_AVGSALE" +
                    " Where LOC_CODE='" + stLocation + "' And PART_NUMBER_SUP in(" + stLinkedProducts + ") And ZERO_SERIAL=" + intZeroSerial + "";
                Cmd = oraConn.CreateCommand();
                if (Tr != null) Cmd.Transaction = Tr;
                Cmd.CommandText = Sql;
                Rs = Cmd.ExecuteReader();
                if (Rs.HasRows)
                {
                    if (Rs.Read())
                    {
                        decAverageSaleQty = decimal.Parse(Rs["AVG_SQ"].ToString());
                        decAverageSaleValue = decimal.Parse(Rs["AVG_SV"].ToString());
                        decAverageSaleCost = decimal.Parse(Rs["AVG_SC"].ToString());
                        if (decAdjustPercent > 0)
                        {
                            decAverageSaleQty = decAverageSaleQty + (decAverageSaleQty * decAdjustPercent / 100);
                        }
                    }
                    Rs.Close();
                }
                Cmd.Dispose();
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                if (Rs != null) Rs.Dispose();
                if (Cmd != null) Cmd.Dispose();
            }
            return decAverageSaleQty;
        }
        private decimal getLinkedPendingLPO_Qty(string stLocation, string stLinkedProducts, string stPartNumberSup, int intZeroSerial, OracleConnection Conn)
        {
            string Sql = "01761955";
            string PendingQty = "0";
            Sql = "Select sum(RCVD_QTY*PACK_MULTIPLY_QTY)as PENDING_QTY From INVT_PURCHASEORDERMAIN a,INVT_PURCHASEORDERSUB b" +
                  " Where a.loc_code in (select distinct a.loc_code from comn_location a inner join comn_location b on A.group_loc=b.group_loc where b.loc_code='" + stLocation + "' and a.active='Y') And b.PART_NUMBER_SUP in(" + stLinkedProducts + ") And ZERO_SERIAL=" + intZeroSerial +
                  " And a.DOC_NO=b.DOC_NO And a.DOC_DATE >='" + DateTime.Today.AddDays(-15).ToString("dd/MMM/yyyy") + "' And CLOSED='N'";
            PendingQty = GetValue(Sql, Conn, null);
            if (PendingQty.Equals("")) PendingQty = "0";
            return decimal.Parse(PendingQty);
        }

        /*

                //   THE RE-ORDER FUNCTIONS ABOVE


                /// Vijeesh
                /// Excel Reports - can be used with and without thread
                /// param name="arabicColumn" --- if there is arabic text column, enter column number else put -1
                /// param name="fileName" --- destination file name, if it is null it will ask at runtime
                /// param name="sheetName" --- name of excel sheet
                /// param name="sourceFileName" --- source excel file to show pre-formatted excel data
                /// param name="status" --- its text will pu updated with progress information
                /// param name="grp" --- this control will be enabled after completion
                public void ExportToExcel(string qry, string tns, string username, string password, int arabicColumn, string fileName, string sheetName, string sourceFileName, Control status, Control grp)
                {
                    OracleConnection con = null;

                    try
                    {
                        con = getConnection(tns, username, password);
                        con.Open();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(tns + " ::: " + ex.ToString());
                        return;
                    }

                    updateStatus(status, "Fetching data from server");
                    DataTable myTable = new DataTable();
                    try
                    {
                        //using (OracleCommand cmd = con.CreateCommand())
                        //{
                            //cmd.CommandText = qry;
                            //cmd.CommandText = 
                            //    qry = "Select 'MAL' as loc_code,'18/Jul/2017' as Ph_Date,substr(Category_code,0,6) As CATEGORY,SUM(LUQ_SYSTEM/PACK_MULTIPLY_QTY) as SYS_STOCK,SUM((LUQ_SYSTEM/PACK_MULTIPLY_QTY)*(case when LC>0 then LC else BUYING_RATE end)) as SYSSTOCK_VALUE,SUM(LUQ_COUNT/PACK_MULTIPLY_QTY) as PH_STOCK,SUM((LUQ_COUNT/PACK_MULTIPLY_QTY)*(case when LC>0 then LC else BUYING_RATE end)) as PHSTOCK_VALUE,SUM((LUQ_COUNT-LUQ_SYSTEM)/PACK_MULTIPLY_QTY) as Diff_STOCK,SUM(((LUQ_COUNT-LUQ_SYSTEM)/PACK_MULTIPLY_QTY)*(case when LC>0 then LC else BUYING_RATE end)) as Diff_VALUE,(Case When SUM((LUQ_SYSTEM/PACK_MULTIPLY_QTY)*(case when LC>0 then LC else BUYING_RATE end))=0 then 999 else ROUND(((SUM(((LUQ_COUNT-LUQ_SYSTEM)/PACK_MULTIPLY_QTY)*(case when LC>0 then LC else BUYING_RATE end))*100)/SUM((case when  LUQ_SYSTEM<=0 then 1 else LUQ_SYSTEM end/PACK_MULTIPLY_QTY)*(case when LC>0 then LC else BUYING_RATE end))),4) end ) AS DiffP From INVT_PHSTOCK_MALCOMP20170718Y Where substr(category_code,0,6) in('NF0101','NF0102','NF0103','NF0104','NF0105','NF0106','NF0107','NF0109') Group By substr(Category_code,0,6) Order By substr(Category_code,0,6)";
                            using (OracleDataAdapter adapter = new OracleDataAdapter(qry, con))
                            {
                                adapter.ReturnProviderSpecificTypes = true;
                             //       myTable.Load(reader);
                                adapter.Fill(myTable);
                            }
                        //}
                        if (myTable.Rows.Count > 0)
                        {
                            myTable.TableName = sheetName;
                            ExportDataSetToExcelFast(ref myTable, arabicColumn, fileName, sourceFileName, status);
                        }
                        myTable.Dispose();

                        updateStatus(status, "Completed Successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        if (myTable != null)
                        {
                            myTable.Dispose();
                        }
                        if (con != null)
                        {
                            if (con.State == ConnectionState.Open) con.Close();
                            con.Dispose();
                        }
                        if(grp!=null) {
                            if (grp.InvokeRequired)
                            {
                                grp.Invoke(new del(() => { grp.Enabled = true; }));
                            }
                            else
                            {
                                grp.Enabled = true;
                            }
                        }
                        GC.Collect();
                    }
                }
                private delegate void del();

                private void updateStatus(Control view, string status)
                {
                    if (view != null)
                    {
                        if (view.InvokeRequired)
                        {
                            view.Invoke(
                                new del(() => { view.Text = status; })
                            );
                        }
                        else
                        {
                            view.Text = status;
                        }
                    }
                }

                private void ExportDataSetToExcelFast(ref DataTable table, int arabicColumn, string fileName, string sourceFileName, Control status)
                {
                    if (fileName == null || fileName.Equals(""))
                    {
                        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                        {
                            fileName = saveFileDialog1.FileName;
                        }
                        else {
                            return;
                        }
                    }
                        Excel.Application excelApp=null;
                        Excel.Workbook excelWorkBook = null;
                        Excel.Sheets xlSheets = null;
                        Excel.Worksheet excelWorkSheet = null;
                        try
                        {
                            //Creae an Excel application instance
                            excelApp = new Excel.Application();
                    
                            if(sourceFileName!=null && !sourceFileName.Equals("")) {
                                //Create an Excel workbook instance and open it from the predefined location
                                excelWorkBook = excelApp.Workbooks.Open(sourceFileName, 0, false, 5, "", "",
                                    false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
                            }
                            else {
                                excelWorkBook = excelApp.Workbooks.Add(Type.Missing);
                            }
                            //Add a new worksheet to workbook with the Datatable name
                            xlSheets = excelWorkBook.Sheets as Excel.Sheets;
                            int sheet = 0;
                            bool moreData = true;
                            int insertLimit = 50000;
                            int start = -insertLimit;
                            int end = 0;
                            int rowCount = table.Rows.Count;
                            while (moreData)
                            {
                                sheet++;
                                moreData = false;
                                if (sheet <= xlSheets.Count)
                                {
                                    excelWorkSheet = (Excel.Worksheet)xlSheets.get_Item(sheet);
                                }
                                else
                                {
                                    excelWorkSheet = (Excel.Worksheet)xlSheets.Add(Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                                }
                                excelWorkSheet.Name = table.TableName+sheet;

                                for (int i = 1; i < table.Columns.Count + 1; i++)
                                {
                                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                                }
                        
                                int maxRowCount = Convert.ToInt32(((Int64)excelWorkSheet.Rows.CountLarge)-1);
                                int startEx = -insertLimit+2;
                                int endEx = 1;
                                for (int i = 0; end<rowCount-1; i++)
                                {
                                    startEx += insertLimit;
                                    endEx += insertLimit;
                                    endEx = endEx <= rowCount ? endEx : rowCount + 1;
                                    if (endEx > maxRowCount)
                                    {
                                        moreData = true;
                                        break;
                                    }
                                    start += insertLimit;
                                    end += insertLimit;
                                    end = end < rowCount ? end : rowCount;

                                    FastDtToExcel(ref table, start, end,excelWorkSheet, startEx, 1, endEx, table.Columns.Count, arabicColumn, status);
                                }

                            }
                            excelWorkBook.SaveAs(fileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, Excel.XlSaveAsAccessMode.xlNoChange, Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        } catch (Exception EX) {
                            throw EX;
                        }
                        finally
                        {
                            if(excelWorkBook!=null)
                                excelWorkBook.Close(Type.Missing, Type.Missing, Type.Missing);
                            if(excelApp!=null)
                                excelApp.Quit();

                    
                                if(excelWorkSheet!=null)
                                Marshal.ReleaseComObject(excelWorkSheet);
                                if (xlSheets != null) 
                                    Marshal.ReleaseComObject(xlSheets);
                                if (excelWorkBook != null) 
                                    Marshal.ReleaseComObject(excelWorkBook);
                                if (excelApp != null) 
                                    Marshal.ReleaseComObject(excelApp);
                    
                    
                            excelApp = null;
                        }
            
                }

                private void FastDtToExcel(ref DataTable dt, int start, int end, Excel.Worksheet sheet, int firstRow, int firstCol, int lastRow, int lastCol, int arabicColumn, Control status)
                {
                    Excel.Range top = (Excel.Range)sheet.Cells[firstRow, firstCol];
                    Excel.Range bottom = (Excel.Range)sheet.Cells[lastRow, lastCol];
                    Excel.Range all = sheet.get_Range(top, bottom);
                    string[,] arrayDT = new string[end-start, dt.Columns.Count];
                    for (int i = start; i < end; i++)
                    {

                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (arabicColumn >= 0 && j == arabicColumn && dt.Rows[j].ItemArray[j].ToString().Length > 0)
                            {
                                arrayDT[i - start, j] = getHexCodeSTRING(dt.Rows[i][j].ToString());
                            }
                            else
                            {
                                arrayDT[i - start, j] = dt.Rows[i][j].ToString();
                            }
                        }
                        if(i%1000==0)
                            updateStatus(status, "Inserting : " + (i+1) + " / " + dt.Rows.Count);
                        if(i==end)
                            updateStatus(status, "Inserting : " + (i + 1) + " / " + dt.Rows.Count);
                    }
                    all.Value2 = arrayDT;
                }

                private OracleConnection getConnection(string tnsName, string username, string password)
                {
                    OracleConnection oraConn = null;
                    String strConnString = "User Id=" + username.Trim() + ";Password=" + password.Trim() + ";Data Source= " +
                                           tnsName.Trim() + ";";
                    //           "Min Pool Size=2;Max Pool Size=5;Connection Lifetime=30;";
                    try
                    {
                        oraConn = new OracleConnection();
                        oraConn.ConnectionString = strConnString.Trim();
                        //oraConn.Open();
                        //if (oraConn != null && oraConn.State==ConnectionState.Open) oraConn.Close();;
                    }
                    catch (Exception Exp)
                    {
                        //System.Console.Write(Exp.ToString());
                        MessageBox.Show(Exp.ToString() , Exp.Message);
                    }
                    return oraConn;
                }
                */
        /////////
    }





    //PRIVILEGES
    //PRIVILEGES
    public class Privileges
    {
        public bool Add = false;
        public bool Edit = false;
        public bool Delete = false;
        public bool Search = false;
        public bool Show = false;
        public bool Print = false;
        public bool Visible = false;
        public bool Enable = false;
        public bool Full = false;
        public bool Prev_OK = true;
        public bool Prev_CAT = false;
        public List<string> CategoryList = null;
        public bool Prev_CatView = false;
        public bool Prev_CatEdit = false;
        public bool Prev_CatFull = false;
        public bool Prev_CatAdd = false;
        public bool Prev_CatRemove = false;

        public Privileges(string User_ID, string Menu_Code, bool Is_Form, OracleConnection con)
        {
            Form_Validation.SetKeys();
            OracleCommand cmd = null; ;
            OracleDataReader rs = null;
            try
            {
                int flg = 0;

                cmd = con.CreateCommand();
                String sql = "";
                if (Defaults.Def_UserGroup == "ADM")
                {
                    sql = "SELECT DISTINCT P_FULL,P_ADD,P_EDIT,P_DELETE,P_SHOW,P_SEARCH,P_PRINT,P_ENABLE,P_VISIBLE,MAIN_SECTION FROM USER_USERPRIVILEGES WHERE USER_ID='" + User_ID + "' And MENU_CODE='" + Menu_Code.ToUpper() + "' And MENU_FORM='" + (Is_Form ? "F" : "M") + "' And REC_STATUS='A' ORDER BY MAIN_SECTION";
                }
                else
                {
                    sql = "SELECT DISTINCT P_FULL,P_ADD,P_EDIT,P_DELETE,P_SHOW,P_SEARCH,P_PRINT,P_ENABLE,P_VISIBLE,MAIN_SECTION FROM USER_USERPRIVILEGES WHERE USER_ID='" + User_ID + "' And MENU_CODE='" + Menu_Code.ToUpper() + "' And LOC_CODE='" + Defaults.Def_Base_Management_LOC + "' And MENU_FORM='" + (Is_Form ? "F" : "M") + "' And REC_STATUS='A' ORDER BY MAIN_SECTION";

                }
                cmd.CommandText = sql;
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    Full = (rs.GetString(0).Equals("N")) ? false : true;
                    Add = (rs.GetString(1).Equals("N")) ? false : true;
                    Edit = (rs.GetString(2).Equals("N")) ? false : true;
                    Delete = (rs.GetString(3).Equals("N")) ? false : true;
                    Search = (rs.GetString(5).Equals("N")) ? false : true;
                    Show = (rs.GetString(4).Equals("N")) ? false : true;   // Show means Form . button View visible or not visible
                    Print = (rs.GetString(6).Equals("N")) ? false : true;
                    Visible = (rs.GetString(8).Equals("N")) ? false : true;
                    Enable = (rs.GetString(7).Equals("N")) ? false : true;
                    flg = 1;
                }
                if (flg == 0) Prev_OK = false;
            }
            catch (Exception Exp)
            {
                Prev_OK = false;
                throw;
                //DataConnector.Message("Error on Setting Privileges :-> " + Exp.ToString(), "I", "Setting Privileges");
            }
            finally
            {
                if (rs != null) rs.Close();
                if (cmd != null) cmd.Dispose();
            }
        }

        public Privileges(string User_ID, string Menu_Code, bool Is_Form, bool Check_User_Category, OracleConnection con)
        {
            Form_Validation.SetKeys();
            OracleCommand cmd = null; ;
            OracleDataReader rs = null;
            try
            {
                int flg = 0;

                cmd = con.CreateCommand();
                String sql = "";
                if (Defaults.Def_UserGroup == "ADM")
                {
                    sql = "SELECT DISTINCT P_FULL,P_ADD,P_EDIT,P_DELETE,P_SHOW,P_SEARCH,P_PRINT,P_ENABLE,P_VISIBLE,MAIN_SECTION FROM USER_USERPRIVILEGES WHERE USER_ID='" + User_ID + "' And MENU_CODE='" + Menu_Code.ToUpper() + "' And MENU_FORM='" + (Is_Form ? "F" : "M") + "' And REC_STATUS='A' ORDER BY MAIN_SECTION";
                }
                else
                {
                    sql = "SELECT DISTINCT P_FULL,P_ADD,P_EDIT,P_DELETE,P_SHOW,P_SEARCH,P_PRINT,P_ENABLE,P_VISIBLE,MAIN_SECTION FROM USER_USERPRIVILEGES WHERE USER_ID='" + User_ID + "' And MENU_CODE='" + Menu_Code.ToUpper() + "' And LOC_CODE='" + Defaults.Def_Base_Management_LOC + "' And MENU_FORM='" + (Is_Form ? "F" : "M") + "' And REC_STATUS='A' ORDER BY MAIN_SECTION";

                }
                cmd.CommandText = sql;
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    Full = (rs.GetString(0).Equals("N")) ? false : true;
                    Add = (rs.GetString(1).Equals("N")) ? false : true;
                    Edit = (rs.GetString(2).Equals("N")) ? false : true;
                    Delete = (rs.GetString(3).Equals("N")) ? false : true;
                    Search = (rs.GetString(5).Equals("N")) ? false : true;
                    Show = (rs.GetString(4).Equals("N")) ? false : true;   // Show means Form . button View visible or not visible
                    Print = (rs.GetString(6).Equals("N")) ? false : true;
                    Visible = (rs.GetString(8).Equals("N")) ? false : true;
                    Enable = (rs.GetString(7).Equals("N")) ? false : true;
                    flg = 1;
                }
                if (flg == 0) Prev_OK = false;
                if (Prev_OK && Check_User_Category)
                {
                    CategoryList = GetCatList(User_ID, con);
                }
            }
            catch (Exception Exp)
            {
                Prev_OK = false;
                throw;
                //DataConnector.Message("Error on Setting Privileges :-> " + Exp.ToString(), "I", "Setting Privileges");
            }
            finally
            {
                if (rs != null) rs.Close();
                if (cmd != null) cmd.Dispose();
            }
        }
        public List<string> GetCatList(string User_ID, OracleConnection con)
        {
            return GetCatList(User_ID, "", con);
        }

        public List<string> GetCatList(string User_ID, string CAT_Code, OracleConnection con)
        {
            CategoryList = null;
            OracleCommand cmd = null; ;
            OracleDataReader rs = null;
            try
            {
                int flg = 0;

                cmd = con.CreateCommand();
                String sql = "";
                if (Defaults.Def_UserGroup == "ADM")
                {
                    Prev_CatFull = true;
                    return CategoryList;
                }
                else
                {
                    if (CAT_Code.Equals(""))
                        sql = "SELECT DISTINCT P_FULL,P_ADD,P_EDIT,P_Remove,P_View,CAT_CODE FROM USER_CATEGORY WHERE USER_ID='" + User_ID + "' ";
                    else
                        sql = "SELECT DISTINCT P_FULL,P_ADD,P_EDIT,P_Remove,P_View,CAT_CODE FROM USER_CATEGORY WHERE USER_ID='" + User_ID + "' and cat_code='" + CAT_Code + "'";
                }
                cmd.CommandText = sql;
                rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    CategoryList = new List<string>();
                    Prev_CAT = true;
                    while (rs.Read())
                    {
                        if (rs["CAT_CODE"].ToString().Equals("0000"))
                        {
                            Prev_CatFull = true;
                            return CategoryList;
                        }
                        Prev_CatFull = (rs.GetString(0).Equals("N")) ? false : true;
                        Prev_CatAdd = (rs.GetString(1).Equals("N")) ? false : true;
                        Prev_CatEdit = (rs.GetString(2).Equals("N")) ? false : true;
                        Prev_CatRemove = (rs.GetString(3).Equals("N")) ? false : true;
                        Prev_CatView = (rs.GetString(4).Equals("N")) ? false : true;
                        CategoryList.Add(rs.GetString(5));
                    }
                }
                rs.Close();
            }
            catch (Exception ex)
            {
                Prev_OK = false;
                throw;
            }
            finally
            {
                if (rs != null) rs.Close();
                if (cmd != null) cmd.Dispose();
            }
            return CategoryList;
        }
    }

    /*
    public class Privileges
    {
        public bool Add = false;
        public bool Edit = false;
        public bool Delete = false;
        public bool Search = false;
        public bool Show = false;
        public bool Print = false;
        public bool Visible = false;
        public bool Enable = false;
        public bool Full = false;
        public bool Prev_OK = true;

        public Privileges(string User_ID, string Menu_Code, bool Is_Form, OracleConnection con)
        {
            Form_Validation.SetKeys();
            OracleCommand cmd = null; ;
            OracleDataReader rs=null ;
            try
            {
                int flg = 0;

                cmd = con.CreateCommand();
                String sql = "";
                if (Defaults.Def_UserGroup == "ADM")
                {
                    sql = "SELECT DISTINCT P_FULL,P_ADD,P_EDIT,P_DELETE,P_SHOW,P_SEARCH,P_PRINT,P_ENABLE,P_VISIBLE,MAIN_SECTION FROM USER_USERPRIVILEGES WHERE USER_ID='" + User_ID + "' And MENU_CODE='" + Menu_Code.ToUpper() + "' And MENU_FORM='" + (Is_Form ? "F" : "M") + "' And REC_STATUS='A' ORDER BY MAIN_SECTION";
                }
                else
                {
                    sql = "SELECT DISTINCT P_FULL,P_ADD,P_EDIT,P_DELETE,P_SHOW,P_SEARCH,P_PRINT,P_ENABLE,P_VISIBLE,MAIN_SECTION FROM USER_USERPRIVILEGES WHERE USER_ID='" + User_ID + "' And MENU_CODE='" + Menu_Code.ToUpper() + "' And LOC_CODE='" + Defaults.Def_Base_Management_LOC + "' And MENU_FORM='" + (Is_Form ? "F" : "M") + "' And REC_STATUS='A' ORDER BY MAIN_SECTION";

                }
                cmd.CommandText = sql;
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    Full = (rs.GetString(0).Equals("N")) ? false : true;
                    Add = (rs.GetString(1).Equals("N")) ? false : true;
                    Edit = (rs.GetString(2).Equals("N")) ? false : true;
                    Delete = (rs.GetString(3).Equals("N")) ? false : true;
                    Search = (rs.GetString(5).Equals("N")) ? false : true;
                    Show = (rs.GetString(4).Equals("N")) ? false : true;   // Show means Form . button View visible or not visible
                    Print = (rs.GetString(6).Equals("N")) ? false : true;
                    Visible = (rs.GetString(8).Equals("N")) ? false : true;
                    Enable = (rs.GetString(7).Equals("N")) ? false : true;
                    flg = 1;
                }
                if (flg == 0) Prev_OK = false;
            }
            catch (Exception Exp)
            {
                Prev_OK = false;
                throw;
                //DataConnector.Message("Error on Setting Privileges :-> " + Exp.ToString(), "I", "Setting Privileges");
            }
            finally 
            {
              if(rs!=null) rs.Close();
              if(cmd!=null)cmd.Dispose();
            }
        }
    }
    */


    // COMBOBOX DOUBLE VALUE
    public class SETCOMBO
    {
        public SETCOMBO(string TableName, string FieldList, short No_Of_Args, ComboBox ComboName, string Condition, string OrderField, Boolean KeepValue, Boolean onlyDistinct, OracleConnection con, OracleTransaction Tr)
        {
            GETCOMBO? oldVal = null;
            string strOrder = "";
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                if (KeepValue.Equals("1"))
                {
                    if (ComboName.SelectedItem != null)
                        oldVal = (GETCOMBO)ComboName.SelectedItem;
                }
                string Distinct = "";
                if (onlyDistinct)
                {
                    Distinct = "Distinct";
                }
                ComboName.Items.Clear();
                if (Condition != "")
                {
                    Condition = " Where " + Condition;
                }

                if (!(OrderField.Equals("")))
                {
                    strOrder = OrderField;
                }

                string SQL = "SELECT " + Distinct + " " + FieldList + " from " + TableName + "" + Condition + " order by " + strOrder + "";
                cmd = con.CreateCommand();
                cmd.CommandText = SQL;
                if (Tr != null) cmd.Transaction = Tr;
                rs = cmd.ExecuteReader();

                while (rs.Read())
                {
                    string F1;
                    string F2;
                    string F3;
                    string F4;

                    switch (No_Of_Args)
                    {
                        case 2:
                            F1 = rs[0].ToString();
                            F2 = rs[1].ToString();
                            F3 = "";
                            F4 = "";
                            break;
                        case 3:
                            F1 = rs[0].ToString();
                            F2 = rs[1].ToString();
                            F3 = rs[2].ToString();
                            F4 = "";
                            break;
                        case 4:
                            F1 = rs[0].ToString();
                            F2 = rs[1].ToString();
                            F3 = rs[2].ToString();
                            F4 = rs[3].ToString();
                            break;
                        case 1:
                        default:
                            MessageBox.Show("YOU MUST ADD THIS FOR LIST COUNT > 2");
                            return;
                    }
                    ComboName.Items.Add(new GETCOMBO(F1, F2, 0, F3, F4));
                }
                rs.Close();
                cmd.Dispose();
            }
            catch { }
            finally { }
            try
            {
                if (KeepValue && oldVal != null)
                    ComboName.SelectedItem = oldVal;
            }
            catch
            {
                if (cmd != null) cmd.Dispose();
                if (rs != null) rs.Close();
            }

        }
    }

    public struct GETCOMBO
    {
        public string Name;
        public string Code;
        public long NumCode;
        public string OtherInfo1;
        public string OtherInfo2;

        public GETCOMBO(string Name, string Code, long NumCode, string OtherInfo1, string OtherInfo2)
        {
            this.Name = Name;
            this.Code = Code;
            this.NumCode = NumCode;
            this.OtherInfo1 = OtherInfo1;
            this.OtherInfo2 = OtherInfo2;
        }
        public override string ToString()
        {
            return Name;
        }
        public override bool Equals(object obj)
        {
            return base.Equals((object)Code);
        }
        public static bool ContainsCode(ComboBox cmb, string Text)
        {
            foreach (GETCOMBO cm in cmb.Items)
            {
                if (cm.Code.Equals(Text)) return true;
            }
            return false;
        }

        public static bool Contains(ComboBox cmb, string Text)
        {

            foreach (GETCOMBO cm in cmb.Items)
            {
                if (cm.Name.Equals(Text)) return true;
            }
            return false;
        }

        public static GETCOMBO? SelectItem_Code(ComboBox cmb, string Text)
        {
            if (Text == null) return null;
            foreach (GETCOMBO cm in cmb.Items)
            {
                if (cm.Code.Equals(Text)) return cm;
            }
            return null;
        }
        public static GETCOMBO? SelectItem_Name(ComboBox cmb, string Text)
        {
            if (Text == null) return null;
            foreach (GETCOMBO cm in cmb.Items)
            {
                if (cm.Name.Equals(Text)) return cm;
            }
            return null;
        }

        public static string GetCode(ComboBox cmb)
        {
            if (cmb.SelectedItem == null) return "";
            else return ((GETCOMBO)cmb.SelectedItem).Code;
        }
        public static string SelectOtherInfo1(ComboBox cmb)
        {
            if (cmb.SelectedItem == null) return "";
            else return ((GETCOMBO)cmb.SelectedItem).OtherInfo1;
        }
        public static string SelectOtherInfo2(ComboBox cmb)
        {
            if (cmb.SelectedItem == null) return "";
            else return ((GETCOMBO)cmb.SelectedItem).OtherInfo2;
        }
        public static GETCOMBO? SelectItem(ComboBox cmb, string Code)
        {
            int i = 0;
            foreach (GETCOMBO cm in cmb.Items)
            {
                if (cm.Code.Equals(Code))
                {
                    cmb.SelectedIndex = i;
                    return cm;
                }
                i++;
            }
            cmb.SelectedIndex = -1;
            return null;
        }
        public static GETCOMBO? SelectItemByName(ComboBox cmb, string Name)
        {
            int i = 0;
            foreach (GETCOMBO cm in cmb.Items)
            {
                if (cm.Name.Equals(Name))
                {
                    cmb.SelectedIndex = i;
                    return cm;
                }
                i++;
            }
            cmb.SelectedIndex = -1;
            return null;
        }
    }
}
