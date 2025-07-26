using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Collections;
using System.Threading;
using System.Data;


namespace MALL
{
    public class ClsAccounts
    {
        OracleConnection con = null;
        OracleTransaction Tr = null;
        string Bank_Code_Alias = "";
        DataConnector ClsGeneral = new DataConnector();
        public string CostCenter = "";
        public ClsAccounts(OracleConnection con, OracleTransaction Tr)
        {
            this.con = con;
            this.Tr = Tr;
        }
        public ClsAccounts(OracleConnection con, OracleTransaction Tr, string Bank_Code_Alias)
        {
            this.con = con;
            this.Tr = Tr;
            this.Bank_Code_Alias = Bank_Code_Alias;
        }




        public bool IsAccountCodeExists(string Acct_Code, string Acct_Name)
        {
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader rs = null;

            bool ok = false;
            cmd.CommandText = "select acct_code ,acct_name from acct_groups where acct_code='" + Acct_Code + "' or acct_name='" + Acct_Name + "'";
            try
            {
                if (Tr != null) cmd.Transaction = Tr;
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    DataConnector.Message("This Code already given for account " + rs.GetString(0) + " - " + rs.GetString(1) + " Choose another One.", "E", "");
                    ok = true;
                }
                rs.Close();
                cmd.Dispose();
                cmd = con.CreateCommand();
                if (Tr != null) cmd.Transaction = Tr;
                cmd.CommandText = "select subsidiary_code,subsidiary_name from comn_subsidiarymaster where subsidiary_code='" + Acct_Code + "' or subsidiary_name='" + Acct_Name + "'";
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    DataConnector.Message("This Code already given for Subsidiary " + rs.GetString(0) + " - " + rs.GetString(1) + " Choose another One.", "E", "");
                    ok = true;
                }

                if (ok) return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error In Acct Checking");
                return true;
            }
            finally
            {
                rs.Close();
                cmd.Dispose();
            }
            return false;
        }

        public bool RemoveEntry(string INVOICE_NUMBER, bool _True)
        {
            string DOCUMENT_NUMBER = "";
            if (ClsGeneral.Exists("ACCT_TRANSACTIONS", "DOC_NO", "ORG_DOC_NO='" + INVOICE_NUMBER + "'", "ACCT_CODE", out DOCUMENT_NUMBER, con, Tr))
            {
                return true;
            }
            else
            {
                throw (new Exception("Accounts Posting Is Missing for " + INVOICE_NUMBER));
            }

        }
        public bool RemoveEntry(string DOCUMENT_NUMBER)
        {
            string Settled = "";
            if (ClsGeneral.Exists("ACCT_TRANSACTIONS", "SETTLED_DOC_NO", "DOC_NO='" + DOCUMENT_NUMBER + "'", "ACCT_CODE", out Settled, con, Tr))
            {
                if (Settled != "")
                {
                    throw (new Exception("Already Settled, Please Reverse Settling, Doc_no is " + Settled));
                }
                return true;
            }
            else
            {
                throw (new Exception("Accounts Posting Is Missing for " + DOCUMENT_NUMBER));
            }

        }

        public bool PostEntry(string LOC_CODE, string DOC_YEAR, string DOC_TYPE, string DOC_NO, int DOC_SERIAL, string DOC_DATE, string ACCT_Code, string NARRATION, string CURR_CODE, decimal Curr_Factor, decimal LC_DEBIT, decimal LC_CREDIT, decimal FC_DEBIT, decimal FC_CREDIT,
        bool PRINT_FLAG, string SUP_INV_NO, string SUP_INV_DATE, string SUP_INV_TYPE, string Org_Doc_No, string Org_Doc_LOC, string Org_Doc_TYPE, string SALESMAM_ID, decimal Net_DrAmount, decimal Net_CrAmount)
        {
            return PostEntry(LOC_CODE, DOC_YEAR, DOC_TYPE, DOC_NO, DOC_SERIAL, DOC_DATE, ACCT_Code, NARRATION, CURR_CODE, Curr_Factor, LC_DEBIT, LC_CREDIT, FC_DEBIT, FC_CREDIT, PRINT_FLAG, SUP_INV_NO, SUP_INV_DATE, SUP_INV_TYPE, Org_Doc_No, Org_Doc_LOC, Org_Doc_TYPE, SALESMAM_ID, Net_DrAmount, Net_CrAmount, "", "", 0, "", "", "", true, true, "CR", "", "", "", "", "", "", "");
        }

        public bool PostEntry(string LOC_CODE, string DOC_YEAR, string DOC_TYPE, string DOC_NO, int DOC_SERIAL, string DOC_DATE, string ACCT_Code, string NARRATION, string CURR_CODE, decimal Curr_Fcator, decimal LC_DEBIT, decimal LC_CREDIT, decimal FC_DEBIT, decimal FC_CREDIT,
        bool PRINT_FLAG, string SUP_INV_NO, string SUP_INV_DATE, string SUP_INV_TYPE, string Org_Doc_No, string Org_Doc_LOC, string Org_Doc_TYPE, string SALESMAM_ID, decimal Net_DrAmount, decimal Net_CrAmount,
        string ChqNumber, string ChqDate, decimal ChequeAmount, string Ref_DocNo, string Ref_DocType, string Ref_DocLoc, bool clear, bool Post, string Tran_Mode_CRCSTR)
        {
            return PostEntry(LOC_CODE, DOC_YEAR, DOC_TYPE, DOC_NO, DOC_SERIAL, DOC_DATE, ACCT_Code, NARRATION, CURR_CODE, Curr_Fcator,
                LC_DEBIT, LC_CREDIT, FC_DEBIT, FC_CREDIT, PRINT_FLAG, SUP_INV_NO, SUP_INV_DATE, SUP_INV_TYPE, Org_Doc_No, Org_Doc_LOC, Org_Doc_TYPE, SALESMAM_ID, Net_DrAmount, Net_CrAmount, ChqNumber, ChqDate, ChequeAmount, Ref_DocNo, Ref_DocType, Ref_DocLoc, clear, Post, Tran_Mode_CRCSTR, "", "", "", "", "", "", "");
        }

        public bool PostEntry(string LOC_CODE, string DOC_YEAR, string DOC_TYPE, string DOC_NO, int DOC_SERIAL, string DOC_DATE, string ACCT_Code, string NARRATION, string CURR_CODE, decimal Curr_Fcator, decimal LC_DEBIT, decimal LC_CREDIT, decimal FC_DEBIT, decimal FC_CREDIT,
        bool PRINT_FLAG, string SUP_INV_NO, string SUP_INV_DATE, string SUP_INV_TYPE, string Org_Doc_No, string Org_Doc_LOC, string Org_Doc_TYPE, string SALESMAM_ID, decimal Net_DrAmount, decimal Net_CrAmount,
        string ChqNumber, string ChqDate, decimal ChequeAmount, string Ref_DocNo, string Ref_DocType, string Ref_DocLoc, bool clear, bool Post, string Tran_Mode_CRCSTR,
            string settled_doc_no, string settled_doc_loc, string settled_doc_type, string settled_doc_date, string settled_doc_year, string Vch_Doc_no, string VCH_DOC_TYPE)
        {

            try
            {
                if (Net_CrAmount != Net_DrAmount)
                {
                    Exception E = new Exception("Debit And Credit Sides Not Matching. Please Correct and try again");
                    throw (E);
                }
                if (LC_DEBIT == 0 && LC_CREDIT == 0 && Tran_Mode_CRCSTR != "" && !(DOC_TYPE=="PJV" || DOC_TYPE=="PRV"))
                {
                    throw (new Exception("Debit And Credit Are 0 "));
                }
                if (LC_DEBIT != 0 && LC_CREDIT != 0)
                {
                    throw (new Exception("INVALID ENTRY"));
                }
                string Tran_Type = "C";
                decimal Amount = LC_CREDIT;
                if (LC_CREDIT == 0)
                {
                    Tran_Type = "D";
                    Amount = LC_DEBIT;
                }
                string Sql = "";
                /* acct Trans  */
                string Group, MainGroup, PrimaryGroup;
                ClsGeneral.Exists("ACCT_GROUPS", "ACCT_GROUP,ACCT_MAINGROUP,ACCT_PRIMARYGROUP", "ACCT_CODE='" + ACCT_Code + "' and acct_type='L'", "ACCT_CODE", 3, out Group, out MainGroup, out PrimaryGroup, con, Tr);
                // ACCT
                if (Group == "")
                {
                    throw (new Exception("INVALID LEDGER" + ACCT_Code));
                }
                int Doc_Month = DateTime.Parse(DOC_DATE).Month;
                DOC_YEAR = DateTime.Parse(DOC_DATE).Year.ToString();
                int Doc_Day = DateTime.Parse(DOC_DATE).Day;
                if (Tran_Mode_CRCSTR == "") Tran_Mode_CRCSTR = "CR";
                if (CostCenter == null || CostCenter.Trim() == "") CostCenter = "--";
                Sql = "Insert into acct_transactions(SL_NO,LOC_CODE,DOC_YEAR,DOC_TYPE,DOC_NO,DOC_SERIAL,DOC_DATE,ACCT_CODE,ACCT_GROUP,NARRATION,CURR_CODE,LC_DEBIT,LC_CREDIT,FC_DEBIT,FC_CREDIT," +
                  "CHEQUE_NO,CHEQUE_DATE,PRINT_FLAG,SUP_INV_NO,SUP_INV_DATE,SUP_INV_TYPE,SALESMAM_ID,POSTED,REF_DOC_NO,REF_DOC_LOC,REF_DOC_TYPE,CLEAR_FLAG,DOC_MONTH,DOC_DAY,Net_DrAmount,Net_CrAmount,Cheque_Amount,ACCT_MAINGROUP,ACCT_PRIMARYGROUP,TRAN_TYPE,ORG_DOC_NO,ORG_DOC_LOC,ORG_DOC_TYPE,CURR_FACTOR,TRAN_MODE,settled_doc_no,settled_loc_CODE,settled_doc_type,settled_doc_date,settled_doc_year,vch_doc_no,vch_doc_type,bank_alias,COST_CENTER) values(seq_accttransactions.nextval,'" + LOC_CODE + "','" + DOC_YEAR + "','" + DOC_TYPE + "','" + DOC_NO + "'," + DOC_SERIAL + ",'" + DOC_DATE + "','" + ACCT_Code + "','" + Group + "','" + NARRATION + "','" + CURR_CODE + "'," + LC_DEBIT + "," + LC_CREDIT + "," + FC_DEBIT + "," + FC_CREDIT + "," +
                  "'" + (ChqNumber == "" ? null : ChqNumber) + "','" + (ChqDate == "" ? null : ChqDate) + "','" + (PRINT_FLAG ? "Y" : "N") + "','" + (SUP_INV_NO == "" ? null : SUP_INV_NO) + "','" + (SUP_INV_DATE == "" ? null : SUP_INV_DATE) + "','" + SUP_INV_TYPE + "','" + SALESMAM_ID + "','" + (Post ? "Y" : "N") + "','" + (Ref_DocNo == "" ? null : Ref_DocNo) + "','" + (Ref_DocLoc == "" ? null : Ref_DocLoc) + "','" + Ref_DocType + "','" + (clear ? "Y" : "N") + "'," + Doc_Month + "," + Doc_Day + "," + Net_DrAmount + "," + Net_CrAmount + "," + ChequeAmount + ",'" + MainGroup + "','" + PrimaryGroup + "','" + Tran_Type + "','" + (Org_Doc_No == "" ? null : Org_Doc_No) + "','" + (Org_Doc_LOC == "" ? null : Org_Doc_LOC) + "','" + (Org_Doc_TYPE == "" ? null : Org_Doc_TYPE) + "'," + Curr_Fcator + ",'" + Tran_Mode_CRCSTR + "'," +
                     "'" + settled_doc_no + "','" + settled_doc_loc + "','" + settled_doc_type + "','" + settled_doc_date + "','" + settled_doc_year + "','" + Vch_Doc_no + "','" + VCH_DOC_TYPE + "','" + Bank_Code_Alias + "','" + CostCenter + "')";

                if (ClsGeneral.ExecuteCmd(Sql, con, Tr))
                {
                    UpdateGroupTotal(DOC_DATE, LOC_CODE, ACCT_Code, Amount, (Tran_Type == "D" ? "DR" : "CR"));


                    //
                    if (!ClsGeneral.AddDataTransfer(DOC_NO, "A", Defaults.Def_Base_LOC, Defaults.Def_Main_Loc, DataConnector.TransferTypes.ACCOUNTS, con, Tr))//if (!ClsGeneral.AddDataTransfer(txtDocumentNo.Text.Trim(),"A", DataConnector.TransferTypes.GRV,oraConn,Tr))
                    {
                        Tr.Rollback();
                        return false;
                    }

                    //
                    return true;
                }
                else
                {
                    throw (new Exception("Error in Accounts Entry"));
                }
                // CONSO 


                /* INV
                                Sql = "Insert into acct_transactions(SL_NO,LOC_CODE,DOC_YEAR,DOC_TYPE,DOC_NO,DOC_SERIAL,DOC_DATE,DB_ACCT,CR_ACCT,NARRATION,CURR_CODE,LC_DEBIT,LC_CREDIT,FC_DEBIT,FC_CREDIT," +
                      ",PRINT_FLAG,SUP_INV_NO,SUP_INV_DATE,SUP_INV_TYPE,SALESMAM_ID,POSTED,DOC_MONTH,DOC_DAY,Net_DrAmount,Net_CrAmount) values()";
                */

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {

            }

        }



        public bool UpdateGroupTotal(string doc_date, string Loc_code, string Acct_Code, decimal Amount, string CR_DR)
        {
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {

                string SysDate = "";
                string Sql = "";
                cmd = con.CreateCommand();
                cmd.CommandText = "select sysdate from dual";
                cmd.Transaction = Tr;
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    SysDate = rs[0].ToString();
                }
                cmd.Dispose();
                string Month = DateTime.Parse(doc_date).Month.ToString().PadLeft(2, '0');
                int Year = DateTime.Parse(doc_date).Year;
                string Day = DateTime.Parse(doc_date).Day.ToString().PadLeft(2, '0');

                string SMonth = DateTime.Parse(SysDate).Month.ToString().PadLeft(2, '0');
                int SYear = DateTime.Parse(SysDate).Year;
                string SDay = DateTime.Parse(SysDate).Day.ToString().PadLeft(2, '0');

                string Field = CR_DR + "_" + Month;
                int i = 0;
                string str = "";
                string str1 = ""; string str2 = "";
                while (Year <= SYear)
                {
                    if (i > 0)
                    {
                        str = ",OB_" + CR_DR + "=OB_" + CR_DR + "+" + Amount;
                        str1 = ",OB_" + CR_DR;
                        str2 = "," + Amount + " as OB_" + CR_DR;
                    }
                    cmd = con.CreateCommand();
                    cmd.Transaction = Tr;
                    if (i == 0) cmd.CommandText = "Update Acct_ledger set " + Field + "=" + Field + " + " + Amount + ",cb_" + CR_DR + "=cb_" + CR_DR + "+" + Amount + ",LAST_ENTRYDATE='" + doc_date + "'" + str + " where doc_year='" + Year + "' and acct_code='" + Acct_Code + "'";
                    else cmd.CommandText = "Update Acct_ledger set cb_" + CR_DR + "=cb_" + CR_DR + "+" + Amount + ",LAST_ENTRYDATE='" + doc_date + "'" + str + " where doc_year='" + Year + "' and acct_code='" + Acct_Code + "'";
                    if (cmd.ExecuteNonQuery() <= 0)
                    {

                        cmd.Dispose();
                        cmd = con.CreateCommand();
                        cmd.Transaction = Tr;
                        if (i == 0) Sql = "insert into acct_ledger(slno,LOC_CODE,DOC_YEAR,CB_" + CR_DR + "," + Field + str1 + ",ACCT_TYPE,ACCT_CODE,ACCT_GROUP,ACCT_PRIMARYGROUP,LAST_ENTRYDATE,ACCT_MAINGROUP) select seq_acctledger.nextval as slno,'" + Loc_code + "' as Loc_code,'" + Year + "' as Doc_year," + Amount + " as CB_" + CR_DR + "," + Amount + " as " + Field + str2 + ",ACCT_TYPE,ACCT_CODE,ACCT_GROUP,ACCT_PRIMARYGROUP,'" + doc_date + "',ACCT_MAINGROUP  from acct_groups where acct_code='" + Acct_Code + "'";
                        else Sql = "insert into acct_ledger(slno,LOC_CODE,DOC_YEAR,CB_" + CR_DR + str1 + ",ACCT_TYPE,ACCT_CODE,ACCT_GROUP,ACCT_PRIMARYGROUP,LAST_ENTRYDATE,ACCT_MAINGROUP) select seq_acctledger.nextval as slno,'" + Loc_code + "' as Loc_code,'" + Year + "' as Doc_year," + Amount + " as CB_" + CR_DR + str2 + ",ACCT_TYPE,ACCT_CODE,ACCT_GROUP,ACCT_PRIMARYGROUP,'" + doc_date + "',ACCT_MAINGROUP  from acct_groups where acct_code='" + Acct_Code + "'";
                        cmd.CommandText = Sql;
                        cmd.ExecuteNonQuery();
                    }
                    Year++;
                    i++;
                    cmd.Dispose();
                }
                Sql = "select acct_group from acct_groups where acct_code='" + Acct_Code + "'";
                string Group = "";
                ClsGeneral.Exists(Sql, 1, out Group, con, Tr);
                if (!(Group == ""))
                {
                    return UpdateGroupTotal(doc_date, Loc_code, Group, Amount, CR_DR);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw (new Exception("Cannot update Group Totals"));
            }
            finally
            {
                if (rs != null) rs.Close();
                if (cmd != null) cmd.Dispose();
            }
        }
        public bool SupplierPaymentPostAccounts(string ConsolidatedVcNumber, string Loc_Code, string Doc_Year, string Doc_Date, decimal Cheque_Amount, string Cheque_Date, OracleConnection con, OracleTransaction Tr)
        {
            OracleCommand cmd = null;
            OracleCommand cmd1 = null;
            OracleDataReader rs = null;
            if (ClsGeneral.Exists("Acct_transactions", "doc_no", "doc_no='" + ConsolidatedVcNumber + "'", con, Tr))
            {
                DataConnector.Message("Already Posted");
                return false;
            }
            try
            {
                string Sql = "Select * from acct_supplierpayment where CON_VCH_no='" + ConsolidatedVcNumber + "' Order By LOC_CODE,DOC_NO";  //string Sql = "Select * from acct_supplierpayment where CON_VCH_no='" + ConsolidatedVcNumber + "'";
                cmd1 = con.CreateCommand();
                cmd1.Transaction = Tr;
                cmd1.CommandText = Sql;
                OracleDataReader rs1 = null;
                string ChqNo = "";
                string AcctDoc = ConsolidatedVcNumber;
                rs1 = cmd1.ExecuteReader();
                bool TranOk = false;
                string Doc_No = "";
                
                int j = 0;
                string  tmpSPV_Location = "";

                while (rs1.Read())
                {
                    //int j = 0;
                    if (tmpSPV_Location != rs1["LOC_CODE"].ToString())
                    {
                        tmpSPV_Location = rs1["LOC_CODE"].ToString();
                        j = 0;
                    }
                    TranOk = true;
                    Doc_Date = DateTime.Parse(Doc_Date).ToString("dd/MMM/yyyy");
                    Cheque_Date = DateTime.Parse(Cheque_Date).ToString("dd/MMM/yyyy");
                    Doc_No = rs1["Doc_no"].ToString();
                    if (Doc_No == "") return false;
                    string Loc_Code1 = rs1["LOC_CODE"].ToString();
                    string Doc_Date1 = DateTime.Parse(rs1["Doc_DATE"].ToString()).ToString("dd/MMM/yyyy");
                    string Doc_Year1 = rs1["Doc_YEAR"].ToString();
                    string BAnk_Code = rs1["BANK_CODE"].ToString();
                    ChqNo = rs1["Cheque_No"].ToString();
                    string SupplierName = rs1["subsidiary_Name"].ToString();
                    string SupplierCode = rs1["subsidiary_Code"].ToString();
                    string Narration = rs1["Narration"].ToString();
                    string AcctCode = "";
                    decimal Cr = decimal.Parse(rs1["CHEQUE_AMOUNT"].ToString());
                    decimal Dr = decimal.Parse(rs1["NET_INVOICE"].ToString());

                    // Post Main Payment

                    PostEntry(Loc_Code1, Doc_Year1, "BPV", AcctDoc, (++j), Doc_Date, SupplierCode, "Cheque Issued to " + SupplierName +" "+ Narration, Defaults.Def_Currency, 1, Dr, 0, 0, 0, false, "", "", "", Doc_No, Loc_Code1, "SPV", "", Dr, Dr, ChqNo, Cheque_Date, Cheque_Amount, "", "", "", true, true, "CR", "", "", "", "", "", Doc_No, "SPV");
                    PostEntry(Loc_Code1, Doc_Year1, "BPV", AcctDoc, (++j), Doc_Date, BAnk_Code, "Cheque Issued to " + SupplierName + " " + Narration, Defaults.Def_Currency, 1, 0, Cr, 0, 0, false, "", "", "", Doc_No, Loc_Code1, "SPV", "", Cr, Cr, ChqNo, Cheque_Date, Cheque_Amount, "", "", "", true, true, "CR", "", "", "", "", "", Doc_No, "SPV");

                    // Contract
                    cmd = con.CreateCommand();
                    cmd.Transaction = Tr;
                    cmd.CommandText = "select * from ACCT_SUPPAYcontract where doc_no='" + Doc_No + "'";
                    rs = cmd.ExecuteReader();
                    while (rs.Read())
                    {
                        string RebAc = ClsGeneral.GetRebateAcct(rs["REB_NAME"].ToString(), con, Tr);
                        PostEntry(Loc_Code1, Doc_Year1, "BPV", AcctDoc, (++j), Doc_Date, RebAc, rs["REB_NAME"].ToString() + " Received From " + SupplierName, Defaults.Def_Currency, 1, 0, decimal.Parse(rs["PAID"].ToString()), 0, 0, false, "", "", "", Doc_No, Loc_Code1, "SPV", "", decimal.Parse(rs["PAID"].ToString()), decimal.Parse(rs["PAID"].ToString()), ChqNo, Cheque_Date, Cheque_Amount, "", "", "", true, true, "CR", "", "", "", "", "", Doc_No, "SPV");
                    }
                    rs.Close();
                    cmd.Dispose();

                    // Additional
                    cmd = con.CreateCommand();
                    cmd.Transaction = Tr;
                    cmd.CommandText = "select * from ACCT_SUPPAYAddl where doc_no='" + Doc_No + "'";
                    rs = cmd.ExecuteReader();
                    while (rs.Read())
                    {
                        PostEntry(Loc_Code1, Doc_Year1, "BPV", AcctDoc, (++j), Doc_Date, rs["ACCT_CODE"].ToString(), rs["NARRATION"].ToString(), Defaults.Def_Currency, 1, decimal.Parse(rs["Dr"].ToString()), decimal.Parse(rs["CR"].ToString()), 0, 0, false, "", "", "", Doc_No, Loc_Code1, "SPV", "", decimal.Parse(rs["CR"].ToString()), decimal.Parse(rs["CR"].ToString()), ChqNo, Cheque_Date, Cheque_Amount, "", "", "", true, true, "CR", "", "", "", "", "", Doc_No, "SPV");
                    }
                    rs.Close();
                    cmd.Dispose();

                    // Changed ADDED ON 9:09 AM 27/Apr/2010
                    if (TranOk && !ClsGeneral.updateTable("Acct_supplierpayment", "SETTLED_DOC_NO='" + ConsolidatedVcNumber + "',SETTLED_LOC='" + Loc_Code + "',SETTLED_DATE='" + Doc_Date + "'", "doc_no='" + Doc_No + "'", con, Tr))
                    {
                        //  Tr.Rollback();
                        return false;
                    }
                    if (TranOk && !ClsGeneral.updateTable("ACCT_TRANSACTIONS", "SETTLED_DOC_NO='" + ConsolidatedVcNumber + "',SETTLED_LOC_CODE='" + Loc_Code + "',SETTLED_DOC_YEAR='" + Doc_Year + "',SETTLED_DOC_TYPE='BPV',SETTLED_DOC_DATE='" + Doc_Date + "'", "VCH_DOC_NO='" + Doc_No + "'", con, Tr))
                    {
                        //  Tr.Rollback();
                        return false;
                    }
                    // Changed ADDED ON 9:09 AM 27/Apr/2010
                    // MessageBox.Show(ConsolidatedVcNumber);
                }
                rs1.Close();
                cmd1.Dispose();

                if (TranOk && !ClsGeneral.updateTable("ACCT_PDCDETAIL", "POSTED='Y',SETTLED_DOC_NO='" + ConsolidatedVcNumber + "',SETTLED_LOC_CODE='" + Loc_Code + "',SETTLED_DOC_YEAR='" + Doc_Year + "',SETTLED_DOC_TYPE='BPV',SETTLED_DOC_DATE='" + Doc_Date + "'", "DOC_NO='" + ConsolidatedVcNumber + "'", con, Tr))
                {
                    // Tr.Rollback();
                    return false;
                }
            }
            catch { throw; }
            finally { if (rs != null)rs.Dispose(); if (cmd != null) cmd.Dispose(); if (cmd1 != null) cmd1.Dispose(); }
            // Changed 9:09 AM 27/Apr/2010 Moved IN to loop

            //if (TranOk && !ClsGeneral.updateTable("Acct_supplierpayment", "SETTLED_DOC_NO='" + ConsolidatedVcNumber + "',SETTLED_LOC='" + Loc_Code + "',SETTLED_DATE='" + Doc_Date + "'", "doc_no='" + Doc_No  + "'", con, Tr))
            //{
            //  //  Tr.Rollback();
            //    return false;
            //}
            //if (TranOk && !ClsGeneral.updateTable("ACCT_TRANSACTIONS", "SETTLED_DOC_NO='" + ConsolidatedVcNumber + "',SETTLED_LOC_CODE='" + Loc_Code + "',SETTLED_DOC_YEAR='" + Doc_Year + "',SETTLED_DOC_TYPE='BPV',SETTLED_DOC_DATE='" + Doc_Date + "'", "VCH_DOC_NO='" + Doc_No + "'", con, Tr))
            //{              
            //  //  Tr.Rollback();
            //    return false;
            //}
            // Changed 9:09 AM 27/Apr/2010




            return true;

        }
        public bool SupplierPaymentReversePost(string Doc_No, OracleConnection con, OracleTransaction Tr)
        {
            if (!ClsGeneral.Exists("Acct_transactions", "doc_no", "doc_no='" + Doc_No + "'", con, Tr))
            {
                DataConnector.Message("Not Posted");
                return false;
            }
            try
            {

                ReverseSPVEntry(Doc_No);
                if (!ClsGeneral.updateTable("Acct_supplierpayment", "SETTLED_DOC_NO=null,SETTLED_LOC=null,SETTLED_DATE=null", "doc_no='" + Doc_No + "'", con, Tr))
                {
                    //  Tr.Rollback();
                    return false;
                }
                if (!ClsGeneral.updateTable("ACCT_TRANSACTIONS", "SETTLED_DOC_NO=null,SETTLED_LOC_CODE=null,SETTLED_DOC_YEAR=null,SETTLED_DOC_TYPE=null,SETTLED_DOC_DATE=null", "VCH_DOC_NO='" + Doc_No + "'", con, Tr))
                {
                    //  Tr.Rollback();
                    return false;
                }

                if (!ClsGeneral.updateTable("ACCT_PDCDETAIL", "POSTED='N',SETTLED_DOC_NO=null,SETTLED_LOC_CODE=null,SETTLED_DOC_YEAR=null,SETTLED_DOC_TYPE=null,SETTLED_DOC_DATE=null", "DOC_NO='" + Doc_No + "'", con, Tr))
                {
                    // Tr.Rollback();
                    return false;
                }
                ClsGeneral.Log_Audit(Defaults.Def_Base_LOC, "frmBankReconcilation", "D", "DELETE", "SupplierPaymentReversePost", Doc_No, DateTime.Today.ToString("dd/MMM/yyyy"), "BPV", con, Tr);
                return true;
            }
            catch (Exception Exp)
            {
                throw;
            }
            return false;
        }

        public bool ReverseEntry(string Doc_Number)
        {

            string Settled = ""; string Vch = "";
            if (ClsGeneral.Exists("ACCT_TRANSACTIONS", "SETTLED_DOC_NO,VCH_DOC_NO", "DOC_NO='" + Doc_Number + "'", "SETTLED_DOC_NO", 2, out Settled, out Vch, con, Tr))
            {
                if (Settled != "")
                {
                    throw (new Exception("Already Settled, Please Reverse Settling, Doc_no is " + Settled));
                }
                if (Vch != "")
                {
                    throw (new Exception("Already One Voucher Is There, Cancel It First. " + Vch));
                }
            }
            else
            {
                throw (new Exception("Accounts Posting Is Missing for " + Doc_Number));
            }

            if (ClsGeneral.Exists("ACCT_TRANSACTIONS", "DOC_NO", "REF_DOC_NO='" + Doc_Number + "'", "ACCT_CODE", out Settled, con, Tr))
            {
                if (Settled != "")
                {
                    throw (new Exception("Already Have Reference " + Settled));
                }
            }


            /*  Check "Reference" case Later  */
            string Sql = "select * from acct_transactions where doc_no='" + Doc_Number + "'";
            OracleCommand cmd = con.CreateCommand();
            try
            {
                cmd.CommandText = Sql;
                string CrDr = "";
                cmd.Transaction = Tr;
                OracleDataReader rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    decimal Amount = 0; CrDr = "CR";
                    Amount = decimal.Parse(rs["LC_Credit"].ToString());
                    if (rs["Tran_Type"].ToString().Equals("D"))
                    {
                        Amount = decimal.Parse(rs["LC_Debit"].ToString());
                        CrDr = "DR";
                    }

                    UpdateGroupTotal(DateTime.Parse(rs["Doc_Date"].ToString()).ToString("dd/MMM/yyyy"), rs["LOC_CODE"].ToString(), rs["acct_code"].ToString(), (-1 * Amount), CrDr);
                }
                rs.Close();
                int r = 0;
                ClsGeneral.deleteTable("Acct_transactions", "Doc_no='" + Doc_Number + "'", out r, con, Tr);
                if (CrDr == "" || r <= 0)
                {
                    return false;
                }
                //2
                if (!ClsGeneral.AddDataTransfer(Doc_Number, "D", Defaults.Def_Base_LOC, Defaults.Def_Main_Loc, DataConnector.TransferTypes.ACCOUNTS, con, Tr))//if (!ClsGeneral.AddDataTransfer(txtDocumentNo.Text.Trim(),"A", DataConnector.TransferTypes.GRV,oraConn,Tr))
                {
                    //Tr.Rollback();
                    return false;
                }
            }
            catch { throw; }
            finally { cmd.Dispose(); }
            //2
            return true;
        }
        public bool ReverseSPVEntry(string Doc_Number)
        {

            string Settled = ""; string Vch = "";
            //if (ClsGeneral.Exists("ACCT_TRANSACTIONS", "SETTLED_DOC_NO,VCH_DOC_NO", "DOC_NO='" + Doc_Number + "'", "SETTLED_DOC_NO", 2, out Settled, out Vch, con, Tr))
            //{
            //    if (Settled != "")
            //    {
            //        throw (new Exception("Already Settled, Please Reverse Settling, Doc_no is " + Settled));
            //    }
            //    if (Vch != "")
            //    {
            //        throw (new Exception("Already One Voucher Is There, Cancel It First. " + Vch));
            //    }
            //}
            //else
            //{
            //    throw (new Exception("Accounts Posting Is Missing for " + Doc_Number));
            //}

            //if (ClsGeneral.Exists("ACCT_TRANSACTIONS", "DOC_NO", "REF_DOC_NO='" + Doc_Number + "'", "ACCT_CODE", out Settled, con, Tr))
            //{
            //    if (Settled != "")
            //    {
            //        throw (new Exception("Already Have Reference " + Settled));
            //    }
            //}


            /*  Check "Reference" case Later  */
            string Sql = "select * from acct_transactions where doc_no='" + Doc_Number + "'";
            OracleCommand cmd = con.CreateCommand();
            try
            {
                cmd.CommandText = Sql;
                string CrDr = "";
                cmd.Transaction = Tr;
                OracleDataReader rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    decimal Amount = 0; CrDr = "CR";
                    Amount = decimal.Parse(rs["LC_Credit"].ToString());
                    if (rs["Tran_Type"].ToString().Equals("D"))
                    {
                        Amount = decimal.Parse(rs["LC_Debit"].ToString());
                        CrDr = "DR";
                    }

                    UpdateGroupTotal(DateTime.Parse(rs["Doc_Date"].ToString()).ToString("dd/MMM/yyyy"), rs["LOC_CODE"].ToString(), rs["acct_code"].ToString(), (-1 * Amount), CrDr);
                }
                rs.Close();
                int r = 0;
                ClsGeneral.deleteTable("Acct_transactions", "Doc_no='" + Doc_Number + "'", out r, con, Tr);
                if (CrDr == "" || r <= 0)
                {
                    return false;
                }
                //2
                if (!ClsGeneral.AddDataTransfer(Doc_Number, "D", Defaults.Def_Base_LOC, Defaults.Def_Main_Loc, DataConnector.TransferTypes.ACCOUNTS, con, Tr))//if (!ClsGeneral.AddDataTransfer(txtDocumentNo.Text.Trim(),"A", DataConnector.TransferTypes.GRV,oraConn,Tr))
                {
                    //Tr.Rollback();
                    return false;
                }
            }
            catch { throw; }
            finally { cmd.Dispose(); }
            //2
            return true;
        }
    }
}
