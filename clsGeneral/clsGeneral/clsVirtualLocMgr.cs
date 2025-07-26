using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Collections;
using System.Threading;
using System.Data;
using clsGeneral;
namespace MALL
{
    public class clsVirtualLocMgr
    {
        OracleConnection con;
        DataConnector clsGen = new DataConnector();
        OracleTransaction tr;
        private string sql = "";

        public clsVirtualLocMgr(OracleConnection con, OracleTransaction tr)
        {
            this.con = con;
            this.tr = tr;
        }

        public int _count = 0;
        string offer_pack = "N";
        bool ModifyLc = false;
        public bool Skip_LC_Update = false;
        private decimal LC_ForTranDtl = 0;
        string Price_Code = "";
        private decimal LC_ForStBalance = 0, LC_ForStBalanceToLoc = 0;

        public bool UpdateStockVirtualBalance(string Loc_code, string Doc_type, string part_number_Sup, decimal Qty, decimal Val, string To_Loc_Code, int ZeroSerial, int PackingOrder, string Packing, decimal BUYING_RATE, decimal Pack_Multiply_Qty, bool _specificBarcode, string _barcode, string baseLocation, decimal SALE_VAL_WO_TAX = 0)
        {
            int Result = -1;
            try
            {
                if (SALE_VAL_WO_TAX == 0) SALE_VAL_WO_TAX = Val;

                string Sql = "Update invt_vir_balance ", Sql1 = "";
                switch (Doc_type.ToUpper())
                {
                    case "SAL":
                        Sql = Sql + " set SALE_QTY=sale_qty+" + Qty + ",sale_val=sale_val+" + Val + ",cb_qty=cb_qty-(" + Qty + "),cb_val=cb_val-(" + Val + ")";
                        break;
                    case "SRT":
                        Sql = Sql + " set SALE_R_QTY=sale_R_qty+" + Qty + ",sale_R_val=sale_R_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val;
                        break;
                    case "PRH":
                        Sql = Sql + " set PRH_QTY=PRH_qty+" + Qty + ",PRH_val=PRH_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val + (LC_ForTranDtl <= 0 ? "" : ",LC=" + LC_ForTranDtl)
                        + ",AVG_LC=round((case when lc<=0 or cb_qty<=0 or cb_qty+" + Qty + "<=0 then " + LC_ForTranDtl + " else (LC*CB_QTY + " + (LC_ForTranDtl * Qty) + ")/( cb_qty+" + Qty + ")  end),2  )"
                        + ",BUYING_RATE=0";
                        break;
                    case "PRR":
                        Sql = Sql + " set PRH_R_QTY=PRH_R_qty+" + Qty + ",PRH_R_val=PRH_R_val+" + Val + ",cb_qty=cb_qty-(" + (Qty) + "),cb_val=cb_val-(" + (Val) + ")";
                        break;
                    case "OB":
                        Sql = Sql + " set OB_QTY=OB_qty+" + Qty + ",ob_val=ob_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val;
                        break;
                    case "GTV":
                    case "ADJ":
                        string updlc = "";
                        if (ModifyLc)
                            updlc = ",LC=" + LC_ForTranDtl;

                        if (!_specificBarcode)
                        {
                            Sql1 = Sql + " set  VTRI_QTY=VTRI_QTY+" + Qty + ",VTRI_val=VTRI_VAL+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val + updlc + " where vloc_code='" + To_Loc_Code + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial=" + ZeroSerial + " and barcode is null";
                        }
                        else
                        {
                            Sql1 = Sql + " set  VTRI_QTY=VTRI_QTY+" + Qty + ",VTRI_val=VTRI_VAL+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val + updlc + " where vloc_code='" + To_Loc_Code + "' and barcode='" + _barcode + "'";
                        }

                        Sql = Sql + " set VTRR_QTY=VTRR_QTY+" + Qty + ",VTRR_VAL=VTRR_VAL+" + Val + ",cb_qty=cb_qty-(" + (Qty) + "),cb_val=cb_val-(" + (Val) + ")";

                        break;
                    default:
                        Exception ex = new Exception("Invalid Document Type. UpdateStockBalance" + Doc_type + "  " + part_number_Sup);
                        throw (ex);
                }

                if (!_specificBarcode)
                {
                    Sql = Sql + " where vloc_code='" + Loc_code + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial=" + ZeroSerial + " and barcode is null";
                }
                else
                {
                    Sql = Sql + " where vloc_code='" + Loc_code + "' and barcode='" + _barcode + "'";
                }

            ReSl:
                if (!baseLocation.Equals(Loc_code))
                {
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = Sql;
                        if (tr != null)
                        {
                            cmd.Transaction = tr;
                        }
                        Result = cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    Result = 1;
                }
                if (Result == 0)
                {
                    using (var cmd1 = con.CreateCommand())
                    {
                        if (tr != null) cmd1.Transaction = tr;
                        if (!_specificBarcode)
                        {
                            if (ZeroSerial == 0 && PackingOrder != 0)
                            {
                                if (LC_ForStBalance <= 0) LC_ForStBalance = BUYING_RATE;
                                cmd1.CommandText = "insert into INVT_VIR_BALANCE(LOC_CODE,VLOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,barcode,BRAND_CODE,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC) select distinct  '" + baseLocation + "' as loc_code,'" + Loc_code + "' as VLOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,null as barcode,BRAND_CODE,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + this.LC_ForStBalance + "  from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing_order=1 AND DEFAULT_PACKING='Y'";
                            }
                            else
                            {
                                if (LC_ForStBalance <= 0) LC_ForStBalance = BUYING_RATE;
                                cmd1.CommandText = "insert into INVT_VIR_BALANCE(LOC_CODE,VLOC_CODE,CATEGORY_CODE,part_number_sup,barcode,PART_NUMBER,BRAND_CODE,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC) select distinct '" + baseLocation + "' as loc_code,'" + Loc_code + "' as VLOC_CODE,CATEGORY_CODE,part_number_sup,null as barcode,PART_NUMBER,BRAND_CODE,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + this.LC_ForStBalance + "  from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing='" + Packing + "' AND DEFAULT_PACKING='Y'";
                            }
                        }
                        else
                        {
                            if (LC_ForStBalance <= 0) LC_ForStBalance = BUYING_RATE;
                            cmd1.CommandText = "insert into INVT_VIR_BALANCE(LOC_CODE,VLOC_CODE,CATEGORY_CODE,part_number_sup,BARCODE,PART_NUMBER,BRAND_CODE,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC) select distinct '" + baseLocation + "' as loc_code,'" + Loc_code + "' as VLOC_CODE,CATEGORY_CODE,part_number_sup,BARCODE,PART_NUMBER,BRAND_CODE,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + this.LC_ForStBalance + "  from INVT_ITEMPACKING where barcode='" + _barcode + "'";
                        }

                        Result = cmd1.ExecuteNonQuery();
                        if (Result > 0) goto ReSl;
                        Exception ex = new Exception("Cannot Update New Item Balance Stock.");
                        throw (ex);
                    }
                }
                if (Result > 0 && !(Sql1.Equals("")))
                {
                ReTr:
                    if (!baseLocation.Equals(To_Loc_Code))
                    {
                        using (var cmd = con.CreateCommand())
                        {
                            if (tr != null)
                            {
                                cmd.Transaction = tr;
                            }
                            cmd.CommandText = Sql1;
                            Result = cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        Result = 1;
                    }
                    if (Result == 0)
                    {
                        using (var cmd1 = con.CreateCommand())
                        {
                            if (tr != null) cmd1.Transaction = tr;
                            if (!_specificBarcode)
                            {
                                if (ZeroSerial == 0 && PackingOrder != 0)
                                {
                                    if (LC_ForStBalanceToLoc <= 0) LC_ForStBalanceToLoc = BUYING_RATE;
                                    cmd1.CommandText = "insert into INVT_VIR_BALANCE(LOC_CODE,VLOC_CODE,CATEGORY_CODE,part_number_sup,barcode,PART_NUMBER,BRAND_CODE,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC) select  distinct '" + baseLocation + "' AS LOC_CODE,'" + To_Loc_Code + "' as VLOC_CODE,CATEGORY_CODE,part_number_sup,null as barcode,PART_NUMBER,BRAND_CODE,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + LC_ForStBalanceToLoc + " from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing_order=1 AND DEFAULT_PACKING='Y'";
                                }
                                else
                                {
                                    if (LC_ForStBalanceToLoc <= 0) LC_ForStBalanceToLoc = BUYING_RATE;
                                    cmd1.CommandText = "insert into INVT_VIR_BALANCE(LOC_CODE,VLOC_CODE,CATEGORY_CODE,part_number_sup,barcode,PART_NUMBER,BRAND_CODE,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC) select distinct '" + baseLocation + "' AS LOC_CODE,'" + To_Loc_Code + "' as VLOC_CODE,CATEGORY_CODE,part_number_sup,null as barcode,PART_NUMBER,BRAND_CODE,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + LC_ForStBalanceToLoc + "  from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing='" + Packing + "' AND DEFAULT_PACKING='Y'";
                                }
                            }
                            else
                            {
                                if (LC_ForStBalanceToLoc <= 0) LC_ForStBalanceToLoc = BUYING_RATE;
                                cmd1.CommandText = "insert into INVT_VIR_BALANCE(LOC_CODE,VLOC_CODE,CATEGORY_CODE,part_number_sup,BARCODE,PART_NUMBER,BRAND_CODE,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC) select distinct '" + baseLocation + "' AS LOC_CODE,'" + To_Loc_Code + "' as VLOC_CODE,CATEGORY_CODE,part_number_sup,BARCODE,PART_NUMBER,BRAND_CODE,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + LC_ForStBalanceToLoc + "  from INVT_ITEMPACKING where barcode='" + _barcode + "'";
                            }
                            Result = cmd1.ExecuteNonQuery();
                            if (Result > 0) goto ReTr;
                            Exception ex = new Exception("Cannot Update New Item Balance Stock. UpdateStockBalance" + part_number_Sup + " " + Packing + " " + Loc_code);
                            throw (ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex = (new Exception("Cannot Update Stock. UpdateStockBalance " + part_number_Sup + " " + Loc_code + "  " + ex.Message));
                throw ex;
            }
            if (Result <= 0)
            {
                Exception ex = (new Exception("Cannot Update Stock. UpdateStockBalance " + part_number_Sup + " " + Loc_code + "  "));
                throw (ex);
            }
            else
            {
                return true;
            }
        }

        public bool UpdateVirtualItemTran(string Loc_Code, string Doc_Date, string Doc_Type, string Part_Number_Sup, decimal Qty, decimal Val, decimal FOC, decimal Pack_Multiply_Qty, string Packing, string To_Loc, string SysDate, int ZeroSerial, int PackingOrder, decimal Unit_Rate, decimal Buying_Rate, string _Barcode, bool _SpecificBarcode, string baseLocation, bool ThrowException = true, decimal SALE_VAL_WO_TAX = 0)
        {

            if (SALE_VAL_WO_TAX == 0)
            { SALE_VAL_WO_TAX = Val; }

            try
            {
                Qty = Math.Round(Qty, 2);
                Val = Math.Round(Val, 2);
            }
            catch (Exception ex) { throw ex; }

            DateTime Dt = DateTime.Parse(Doc_Date);
            decimal AQty = Qty;
            int Result = -1;
            try
            {

                if (ZeroSerial > 0 && Pack_Multiply_Qty > 1 && Doc_Type == "GTV")
                {

                }

                string Sql = "Update INVT_VIR_TRANSACTION ", Sql1 = "";
                switch (Doc_Type.ToUpper())
                {
                    case "SAL":

                        // BelowCost Checking
                        if (Defaults.Def_MAIN_LOCATION || ((Defaults.Def_Base_LOC == Loc_Code) && Defaults.Def_Loc_Type == "SH"))
                        {
                            decimal Lc = getLC(Loc_Code, Part_Number_Sup, ZeroSerial, Packing);
                            if (ZeroSerial == 0) Lc = Lc * Pack_Multiply_Qty;
                            //
                            if (Packing == "KGM")
                            {
                                Lc = Lc * Qty;
                            }
                            //    
                            decimal margine = ((Unit_Rate - Lc) * 100) / Unit_Rate;
                            int Perc = (int)margine;
                            string Remarks = "";
                            if (Perc < 0) Remarks = "BELOW COST";
                            else if (Perc == 0) Remarks = "COST TO COST";
                            else if (Perc <= 5) Remarks = "LESS MARGINE";
                            else if (Perc > 65)
                                Remarks = "HIGH MARGINE";
                            if (Remarks.Length > 0)
                            {
                                clsGen.insertTable("invt_inventorylog", "Loc_code,doc_type,part_number_sup,zero_serial,Log_Type,Mail_Sent,value_org,value_comp,doc_date,remarks,packing,margine,qty", "'" + Loc_Code + "','SAL','" + Part_Number_Sup + "'," + ZeroSerial + ",'SALE','N'," + Unit_Rate + "," + Lc + ",'" + Doc_Date + "','" + Remarks + "','" + Packing + "'," + margine + "," + Qty, con, tr);
                            }
                        }
                        //
                        Sql = Sql + " set SALE_QTY=sale_qty+" + Qty + ",sale_val=sale_val+" + Val + ",SALE_VAL_WO_TAX=SALE_VAL_WO_TAX+" + SALE_VAL_WO_TAX + ",PRICE_CODE='" + Price_Code + "',Offer_Pack='" + offer_pack + "'";

                        if (FOC != 0)
                        {
                            Sql = Sql + ",FOC_SALE=FOC_SALE+" + FOC;
                        }
                        AQty = -1 * Qty;
                        break;
                    case "SRT":

                        Sql = Sql + " set SALE_R_QTY=sale_R_qty+" + Qty + ",SALE_R_VAL=SALE_R_VAL+" + Val + ",SALE_VAL_WO_TAX=SALE_VAL_WO_TAX+" + (-1 * SALE_VAL_WO_TAX) + ",PRICE_CODE='" + Price_Code + "',Offer_Pack='" + offer_pack + "'";

                        if (FOC != 0)
                        {
                            Sql = Sql + " ,FOC_SRT=FOC_SRT+" + FOC;
                        }
                        break;
                    case "PRH":
                        Sql = Sql + " set PRH_QTY=PRH_qty+" + Qty + ",Offer_Pack='" + offer_pack + "',PRICE_CODE='" + Price_Code + "',PRH_val=PRH_val+" + Val + (LC_ForTranDtl <= 0 ? "" : ",LC=" + LC_ForTranDtl)
                        + ",AVG_LC=round((case when lc<=0 or cb_qty<=0 or cb_qty+" + Qty + "<=0 then " + LC_ForTranDtl + " else (LC*CB_QTY + " + (LC_ForTranDtl * Qty) + ")/( cb_qty+" + Qty + ")  end ),2)"
                        + ",BUYING_RATE=0";

                        if (FOC != 0)
                        {
                            Sql = Sql + " ,FOC_PRH=FOC_PRH+" + FOC;
                        }
                        break;
                    case "PRR":

                        Sql = Sql + " set PRH_R_QTY=PRH_R_qty+" + Qty + ",Offer_Pack='" + offer_pack + "',PRH_R_val=PRH_R_val+" + Val + ",PRICE_CODE='" + Price_Code + "'";

                        if (FOC != 0)
                        {
                            Sql = Sql + " ,FOC_PRR=FOC_PRR+" + FOC;
                        }
                        AQty = -1 * Qty;
                        break;
                    case "GTV":
                    case "ADJ":
                        if (!_SpecificBarcode)
                        {
                            Sql1 = " VTRI_QTY=VTRI_QTY+" + Qty + ",VTRI_val=VTRI_val+" + Val + ",Offer_Pack='" + offer_pack + "',PRICE_CODE='" + Price_Code + "',CB_QTY=CB_QTY+" + Qty + (ModifyLc ? ",LC=" + LC_ForTranDtl : "") + " where vloc_code='" + To_Loc + "' and doc_date='" + Doc_Date + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial='" + ZeroSerial + "'  and barcode is null";
                        }
                        else
                        {
                            Sql1 = " VTRI_QTY=VTRI_QTY+" + Qty + ",VTRI_val=VTRI_val+" + Val + ",Offer_Pack='" + offer_pack + "',PRICE_CODE='" + Price_Code + "',CB_QTY=CB_QTY+" + Qty + (ModifyLc ? ",LC=" + LC_ForTranDtl : "") + " where vloc_code='" + To_Loc + "' and doc_date='" + Doc_Date + "' and part_number_Sup='" + Part_Number_Sup + "' and barcode='" + _Barcode + "'";
                        }
                        Sql = Sql + " set VTRR_QTY=VTRR_QTY+" + Qty + ",VTRR_val=VTRR_val+" + Val + ",Offer_Pack='" + offer_pack + "',PRICE_CODE='" + Price_Code + "'";
                        AQty = -1 * Qty;
                        break;

                    default:
                        Exception ex = new Exception("Invalid Document Type. UpdateItemTran " + Doc_Type + "  " + Part_Number_Sup);
                        throw (ex);
                }

                string upd = ",cb_qty=cb_qty+" + AQty;
            ReSl:
                if (!_SpecificBarcode)
                {
                    sql = Sql + " " + upd + " where doc_date='" + Doc_Date + "' and  vloc_code='" + Loc_Code + "'  and part_number_Sup='" + Part_Number_Sup + "' and zero_serial='" + ZeroSerial + "'  and barcode is null";
                }
                else
                {
                    sql = Sql + " " + upd + " where doc_date='" + Doc_Date + "' and  vloc_code='" + Loc_Code + "' and part_number_Sup='" + Part_Number_Sup + "'  and barcode='" + _Barcode + "'";
                }
                if (!baseLocation.Equals(Loc_Code))
                {
                    using (var cmd = con.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        if (tr != null) cmd.Transaction = tr;
                        Result = cmd.ExecuteNonQuery();
                        if (Result > 0 && SysDate.ToUpper() != Doc_Date.ToUpper()) AdjustStockTran(Doc_Date, Loc_Code, Part_Number_Sup, SysDate, AQty, ZeroSerial);
                    }
                }
                else
                {
                    Result = 1;
                }
                decimal Ob_Qty = 0;
                decimal Lc_Loc = 0;
                if (Result == 0)
                {

                    Ob_Qty = getStock_Tran(Part_Number_Sup, Loc_Code, Doc_Date, ZeroSerial, SysDate, out Lc_Loc, Pack_Multiply_Qty, false, Packing, _Barcode);
                    if (Lc_Loc == 0 && LC_ForTranDtl != 0) Lc_Loc = LC_ForTranDtl;
                    LC_ForStBalance = Lc_Loc;
                    using (var cmd1 = con.CreateCommand())
                    {
                        if (tr != null) cmd1.Transaction = tr;
                        if (!_SpecificBarcode)
                        {
                            if (ZeroSerial == 0 && PackingOrder != 0)
                            {
                                if (Lc_Loc <= 0) Lc_Loc = Buying_Rate;
                                cmd1.CommandText = "insert into INVT_VIR_TRANSACTION(doc_date,loc_code,vloc_code,doc_year,doc_month,doc_day,part_number_sup,barcode,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name) select  '" + Doc_Date + "' as doc_date,'" + baseLocation + "' as loc_code,'" + Loc_Code + "' as VLOC_CODE,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,null as barcode,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,default_packing,1," + Ob_Qty + " as ob_qty,0 as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                            }
                            else
                            {
                                if (Lc_Loc <= 0) Lc_Loc = Buying_Rate;
                                cmd1.CommandText = "insert into INVT_VIR_TRANSACTION(doc_date,loc_code,vloc_code,doc_year,doc_month,doc_day,part_number_sup,barcode,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name) select  '" + Doc_Date + "' as doc_date,'" + baseLocation + "' as loc_code,'" + Loc_Code + "' as VLOC_CODE,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,null as barcode,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,'" + Packing + "'," + Pack_Multiply_Qty + "," + Ob_Qty + " as ob_qty," + ZeroSerial + " as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name  from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                            }
                        }
                        else
                        {
                            if (Lc_Loc <= 0) Lc_Loc = Buying_Rate;
                            cmd1.CommandText = "insert into INVT_VIR_TRANSACTION(doc_date,loc_code,vloc_code,doc_year,doc_month,doc_day,part_number_sup,barcode,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name) select  '" + Doc_Date + "' as doc_date,'" + baseLocation + "' as loc_code,'" + Loc_Code + "' as VLOC_CODE,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,'" + _Barcode + "' as barcode,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,'" + Packing + "'," + Pack_Multiply_Qty + "," + Ob_Qty + " as ob_qty," + ZeroSerial + " as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name  from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                        }
                        Result = cmd1.ExecuteNonQuery();
                        if (Result > 0) goto ReSl;
                        Exception ex = new Exception("New Item Tran Error.(item Not Found) UpdateItemTran " + Part_Number_Sup);
                        throw (ex);
                    }
                }
                if (Result > 0 && !(Sql1.Equals("")))
                {
                ReTr:
                    AQty = -1 * Qty;
                    Sql = "update INVT_VIR_TRANSACTION set " + Sql1;
                    if (!baseLocation.Equals(To_Loc))
                    {
                        using (var cmd = con.CreateCommand())
                        {
                            if (tr != null) cmd.Transaction = tr;
                            cmd.CommandText = Sql;
                            Result = cmd.ExecuteNonQuery();
                            if (Result > 0 && SysDate.ToUpper() != Doc_Date.ToUpper()) AdjustStockTran(Doc_Date, Loc_Code, Part_Number_Sup, SysDate, AQty, ZeroSerial);
                        }
                    }
                    else
                    {
                        Result = 1;
                    }
                    if (Result == 0)
                    {
                        Ob_Qty = getStockVir_Tran(Part_Number_Sup, To_Loc, Doc_Date, ZeroSerial, SysDate, out Lc_Loc, Pack_Multiply_Qty, false, Packing);
                        if (Lc_Loc == 0 && LC_ForTranDtl != 0) Lc_Loc = LC_ForTranDtl;
                        LC_ForStBalanceToLoc = Lc_Loc;
                        using (var cmd1 = con.CreateCommand())
                        {
                            if (tr != null) cmd1.Transaction = tr;
                            if (!_SpecificBarcode)
                            {
                                if (ZeroSerial == 0 && PackingOrder != 0)
                                {
                                    if (Lc_Loc <= 0) Lc_Loc = Buying_Rate;
                                    cmd1.CommandText = "insert into INVT_VIR_TRANSACTION(doc_date,loc_code,vloc_code,doc_year,doc_month,doc_day,part_number_sup,barcode,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name) select  '" + Doc_Date + "' as doc_date,'" + baseLocation + "' as loc_code,'" + To_Loc + "' as VLOC_CODE,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,null as barcode,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,default_packing,1," + Ob_Qty + " as ob_qty,0 as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name  from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                                }
                                else
                                {
                                    if (Lc_Loc <= 0) Lc_Loc = Buying_Rate;
                                    cmd1.CommandText = "insert into INVT_VIR_TRANSACTION(doc_date,loc_code,vloc_code,doc_year,doc_month,doc_day,part_number_sup,barcode,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name) select  '" + Doc_Date + "' as doc_date,'" + baseLocation + "' as loc_code,'" + To_Loc + "' as VLOC_CODE,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,null as barcode,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,'" + Packing + "'," + Pack_Multiply_Qty + "," + Ob_Qty + " as ob_qty," + ZeroSerial + " as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name  from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                                }
                            }
                            else
                            {
                                if (Lc_Loc <= 0) Lc_Loc = Buying_Rate;
                                cmd1.CommandText = "insert into INVT_VIR_TRANSACTION(doc_date,loc_code,vloc_code,doc_year,doc_month,doc_day,part_number_sup,barcode,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name) select  '" + Doc_Date + "' as doc_date,'" + baseLocation + "' as loc_code,'" + To_Loc + "' as VLOC_CODE,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,'" + _Barcode + "' as barcode,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,'" + Packing + "'," + Pack_Multiply_Qty + "," + Ob_Qty + " as ob_qty," + ZeroSerial + " as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name  from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                            }
                            Result = cmd1.ExecuteNonQuery();
                            if (Result > 0) goto ReTr;
                            Exception ex = new Exception("New Item Tran Error. (item Missing) UpdateItemTran " + Part_Number_Sup);
                            throw (ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex = new Exception("Item Tran Error. UpdateItemTran " + Part_Number_Sup + "\r\n" + ex);
                throw ex;
            }
            if (Result <= 0)
            {
                Exception ex = new Exception("Item Tran Error. UpdateItemTran " + Part_Number_Sup + "\r\n");
                throw (ex);
            }
            else
            {
                return true;
            }
        }

        public bool AddToBarcodeSettings(string baseLoc, string vLoc, string barcode, string partNumberSup, int zeroSerial, bool _RestrictedTran, OracleConnection con, OracleTransaction tr)
        {
            try
            {
                if (!clsGen.Exists("INVT_VIR_BARCODE_SETTINGS", "PART_NUMBER_SUP", "BARCODE='" + barcode + "' AND VLOC_CODE='" + vLoc + "'", con, tr))
                {
                    clsGen.insertTable("INVT_VIR_BARCODE_SETTINGS", "LOC_CODE,VLOC_CODE,PART_NUMBER_SUP,ZERO_SERIAL,BARCODE,ACTIVE,RESTRICT_TRAN", "'" + baseLoc + "','" + vLoc + "','" + partNumberSup + "'," + zeroSerial + ",'" + barcode + "','Y','N'", con, tr);
                }
                return true;
            }
            catch (Exception)
            {
                Exception ex = (new Exception("Cannot Update Stock. UpdateStockBalance " + partNumberSup + " " + vLoc + "  "));
                throw ex;
            }
        }


        private decimal getStockVir_Tran(string Part_Number_Sup, string Loc_Code, string doc_date, int zeroSerial, string Sysdate, out decimal LC, decimal PackMultiplyQty, bool FROM_TRAN, string Packing)
        {

            LC = 0;
            decimal Ob = 0;
            string Sql = "";
            bool Found = false;
            if (FROM_TRAN) Sql = "select cb_Qty,LC from INVT_VIR_TRANSACTION where doc_date<'" + doc_date + "' and  vloc_code='" + Loc_Code + "'  and part_number_sup='" + Part_Number_Sup + "' and zero_serial=" + zeroSerial + " order by doc_date desc";
            else Sql = "select cb_Qty,LC from INVT_VIR_BALANCE where vloc_code='" + Loc_Code + "' and part_number_sup='" + Part_Number_Sup + "' and zero_serial=" + zeroSerial;
            try
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.Transaction = tr;
                    cmd.CommandText = Sql;
                    using (var rs = cmd.ExecuteReader())
                    {
                        if (rs.Read())
                        {
                            Found = true;
                            if (rs.IsDBNull(0)) Ob = 0;
                            else Ob = decimal.Parse(rs[0].ToString());
                            if (rs.IsDBNull(1)) LC = 0;
                            else LC = decimal.Parse(rs[1].ToString());
                            if (FROM_TRAN) return Ob;
                        }
                        if (FROM_TRAN) Found = true;
                    }
                }
            }
            catch (Exception ee)
            {
                ee = new Exception("getStock_tran " + Part_Number_Sup + " " + Loc_Code + " " + ee.Message);
                throw ee;
            }

            if (!FROM_TRAN && !Found)
            {
                // Ob = getStock_Tran(Part_Number_Sup, Loc_Code, doc_date, zeroSerial, Sysdate, out   LC, PackMultiplyQty, true, Packing);
            }
            try
            {
                if (!FROM_TRAN)
                {
                    if (PackMultiplyQty > 1)
                    {

                    }

                    decimal LC1 = getLC(Loc_Code, Part_Number_Sup, zeroSerial, Packing);
                    if (LC1 > 0)
                        LC = LC1;
                }
            }
            catch (Exception ex)
            {
                ex = new Exception("getStock_tran LC ERR " + Part_Number_Sup + " " + Loc_Code + " " + ex.Message);
                throw ex;
            }
            return Ob;
        }

        private bool AdjustStockTran(string Doc_Date, string Loc_code, string Part_Number, string sysdate, decimal Qty, int ZeroSerial)
        {
            using (var cmd = con.CreateCommand())
            {
                try
                {
                    DateTime dt = DateTime.Parse(Doc_Date);
                    DateTime SysDate = DateTime.Parse(sysdate);
                    dt = dt.AddDays(1);
                    cmd.Transaction = tr;
                    cmd.CommandText = "update invt_itemtransaction set cb_qty=cb_qty+" + Qty + ",ob_qty=ob_qty+" + Qty + " where Loc_code='" + Loc_code + "' and doc_date>='" + dt.ToString("dd/MMM/yyyy") + "' and doc_date<='" + sysdate + "' and Part_number_sup='" + Part_Number + "' and Zero_serial=" + ZeroSerial;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) { throw ex; }
                return true;
            }
        }

        private decimal getStock_Tran(string Part_Number_Sup, string Loc_Code, string doc_date, int zeroSerial, string Sysdate, out decimal LC, decimal PackMultiplyQty, bool FROM_TRAN, string Packing, string BARCODE)
        {

            LC = 0;
            decimal Ob = 0;
            string Sql = "";
            bool Found = false;
            if (FROM_TRAN) Sql = "select cb_Qty,LC from INVT_VIR_TRANSACTION where doc_date<'" + doc_date + "' and  vloc_code='" + Loc_Code + "'  and part_number_sup='" + Part_Number_Sup + "' and zero_serial=" + zeroSerial + " and barcode = '" + BARCODE + "' order by doc_date desc";
            else Sql = "select cb_Qty,LC from INVT_VIR_BALANCE where vloc_code='" + Loc_Code + "' and part_number_sup='" + Part_Number_Sup + "' and zero_serial=" + zeroSerial + " and barcode = '" + BARCODE + "'";
            try
            {
                using (var cmd = con.CreateCommand())
                {
                    cmd.Transaction = tr;
                    cmd.CommandText = Sql;
                    using (var rs = cmd.ExecuteReader())
                    {
                        if (rs.Read())
                        {
                            Found = true;
                            if (rs.IsDBNull(0)) Ob = 0;
                            else Ob = decimal.Parse(rs[0].ToString());
                            if (rs.IsDBNull(1)) LC = 0;
                            else LC = decimal.Parse(rs[1].ToString());
                            if (FROM_TRAN) return Ob;
                        }
                        if (FROM_TRAN) Found = true;
                    }
                }
            }
            catch (Exception ee)
            {
                ee = new Exception("getStock_tran " + Part_Number_Sup + " " + Loc_Code + " " + ee.Message);
                throw ee;
            }
            if (!FROM_TRAN && !Found)
            {
                // Ob = getStock_Tran(Part_Number_Sup, Loc_Code, doc_date, zeroSerial, Sysdate, out   LC, PackMultiplyQty, true, Packing);
            }
            try
            {
                if (!FROM_TRAN)
                {
                    if (PackMultiplyQty > 1)
                    {

                    }

                    decimal LC1 = getLC(Loc_Code, Part_Number_Sup, zeroSerial, Packing);

                    if (LC1 > 0)
                        LC = LC1;
                }
            }
            catch (Exception ex)
            {
                ex = new Exception("getStock_tran LC ERR " + Part_Number_Sup + " " + Loc_Code + " " + ex.Message);
                throw ex;
            }
            return Ob;
        }

        public void getLastTran(string TRANMODE, string Part_Number_Sup, string Doc_Date, string Loc_Code, int ZeroSerial, out string Date, out decimal Qty, out decimal Value)
        {
            Qty = 0; Value = 0; Date = "";
            string Sel = "";
            string cond = "";
            try
            {

                switch (TRANMODE)
                {
                    case "SAL":
                        Sel = " doc_date,Sale_qty,Sale_val as Sc ";
                        cond = " and Sale_qty>0";
                        break;
                    case "PRH":
                        Sel = " doc_date,prh_qty,prh_val as LC ";
                        cond = " and prh_qty>0";
                        break;
                    default:
                        Exception ex = new Exception("Invalid Document Type.");
                        throw (ex);
                }

                string Sql = "select " + Sel + " from invt_itemtransaction where doc_date<='" + Doc_Date + "' and loc_code='" + Loc_Code + "'  and part_number_sup='" + Part_Number_Sup + "' " + cond + " order by doc_date desc";
                using (var cmd = con.CreateCommand())
                {
                    cmd.Transaction = tr;
                    cmd.CommandText = Sql;
                    using (var rs = cmd.ExecuteReader())
                    {
                        if (rs.Read())
                        {
                            if (rs.IsDBNull(0)) return;
                            Date = DateTime.Parse(rs[0].ToString()).ToString("dd/MMM/yyyy");
                            Qty = decimal.Parse(rs[1].ToString());
                            Value = decimal.Parse(rs[2].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ex = new Exception("Invalid Document Type. getLastTran" + Part_Number_Sup + " " + TRANMODE + " " + ex);
                throw (ex);
            }
            return;
        }

        private decimal getLC(string Loc_Code, string Part_Number_sup, int Zero_Serial, string Packing)
        {
            try
            {
                string Lc = clsGen.GetValue("select round(LC,3) as LC from invt_itemtran_dtls where loc_code='" + Loc_Code + "' and part_number_sup='" + Part_Number_sup + "' and zero_serial=" + Zero_Serial, con, tr);
                if (!clsGen.IsNumeric1(Lc) || Lc == "")
                {
                    Lc = clsGen.GetValue("select round(buying_rate,3) as LC from invt_itempacking where part_number_sup='" + Part_Number_sup + "' and (zero_serial>0 or packing_order=1) and zero_serial=" + Zero_Serial, con, tr);
                }
                if (clsGen.IsNumeric1(Lc))
                    return decimal.Parse(Lc);
            }
            catch (Exception ex)
            {
                return 0;
            }
            return 0;
        }

        public string Get_ItemType(string PART_NUMBER_SUP)
        {
            try
            {
                using (var cmd = con.CreateCommand())
                {
                    if (tr != null) cmd.Transaction = tr;
                    sql = "select item_type from invt_inventorymaster where part_number_sup='" + PART_NUMBER_SUP + "'";
                    cmd.CommandText = sql;
                    using (var rs = cmd.ExecuteReader())
                    {
                        if (rs.Read())
                        {
                            return rs[0].ToString();
                        }
                        return "G";
                    }
                }
            }
            catch (Exception eq)
            {
                throw (new Exception("GetItemType " + PART_NUMBER_SUP + eq.Message));
            }
        }
        public bool GetVirStock(string LocCode, string PartNumber, int Zero, bool Specbarcode, string barcode, out string stock, OracleConnection oraConn)
        {
            OracleCommand cmd3 = oraConn.CreateCommand();
            OracleDataReader rsS = null;
            if (!Specbarcode)
            {
                cmd3.CommandText = "SELECT Round(SUM((CB_QTY/PACK_MULTIPLY_QTY)),2)as Stock FROM  INVT_VIR_BALANCE WHERE VLOC_CODE='" + LocCode + "' AND PART_NUMBER_SUP='" + PartNumber + "' and ZERO_SERIAL=" + Zero + "";
            }
            else
            {
                cmd3.CommandText = "SELECT Round((CB_QTY/PACK_MULTIPLY_QTY),2)as Stock FROM  INVT_VIR_BALANCE WHERE VLOC_CODE='" + LocCode + "' AND PART_NUMBER_SUP='" + PartNumber + "' and ZERO_SERIAL=" + Zero + " AND BARCODE='" + barcode + "'";
            }
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
        public bool GetActualStock(string LocCode, string PartNumber, int Zero, bool Specbarcode, string barcode, out string stock, OracleConnection oraConn)
        {
            try
            {
                string query = @"SELECT Stock - NVL(VStock,0) AS STOCK
                          FROM(SELECT ROUND(SUM((CB_QTY / PACK_MULTIPLY_QTY)), 2)     AS Stock,
                          PART_NUMBER_SUP,
                          LOC_CODE,
                          ZERO_SERIAL
                          FROM INVT_INVENTORYBALANCE
                          WHERE LOC_CODE = '" + LocCode + @"' AND PART_NUMBER_SUP = '" + PartNumber + @"' AND ZERO_SERIAL=" + Zero + @"
                          GROUP BY PART_NUMBER_SUP, LOC_CODE, ZERO_SERIAL) A
                          LEFT JOIN
                          (SELECT ROUND(SUM((CB_QTY / PACK_MULTIPLY_QTY)), 2)     AS VStock,
                          PART_NUMBER_SUP,
                          LOC_CODE,
                          ZERO_SERIAL
                          FROM INVT_VIR_BALANCE
                          WHERE LOC_CODE = '" + LocCode + @"' AND PART_NUMBER_SUP = '" + PartNumber + @"' AND ZERO_SERIAL=" + Zero + @"
                          GROUP BY PART_NUMBER_SUP, LOC_CODE, ZERO_SERIAL) B
                          ON     A.PART_NUMBER_SUP = B.PART_NUMBER_SUP
                          AND A.ZERO_SERIAL = B.ZERO_SERIAL
                          AND A.LOC_CODE = B.LOC_CODE";

                using (var cmd3 = oraConn.CreateCommand())
                {
                    if (!Specbarcode)
                    {
                        cmd3.CommandText = query;
                    }
                    using (var rsS = cmd3.ExecuteReader())
                    {
                        string SD = "0";
                        if (rsS.Read())
                        {
                            SD = rsS.GetValue(0).ToString();
                        }
                        stock = SD;
                    }
                }
            }
            catch
            {
                throw;
            }
            return true;
        }
        public bool GetStock(string LocCode, string PartNumber, int Zero, bool Specbarcode, string barcode, out string stock, OracleConnection oraConn, OracleTransaction Tr)
        {
            OracleCommand cmd3 = oraConn.CreateCommand();

            OracleDataReader rsS = null;
            cmd3.CommandText = "SELECT Round((CB_QTY/PACK_MULTIPLY_QTY),2)as Stock FROM  INVT_INVENTORYBALANCE WHERE VLOC_CODE='" + LocCode + "' AND PART_NUMBER_SUP='" + PartNumber + "' and ZERO_SERIAL=" + Zero + " AND BARCODE='" + barcode + "'";
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
        public bool GetVirStock_Acc(string PartNUmber, int Zero, out string LocationStock, out string LocationSale, OracleConnection oraConn)
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
                SQL1 = "select cl.VLOC,ROUND((ib.CB_QTY/ib.PACK_MULTIPLY_QTY),4) As CB_QTY,SALE_QTY-SALE_R_QTY as Sale from INVT_VIR_BALANCE ib RIGHT OUTER JOIN INVT_VIRTUALLOCATIONS cl  on cl.VLOC = ib.VLOC_CODE and ib.PART_NUMBER_SUP='" + PartNUmber + "' and ib.ZERO_SERIAL=" + Zero + "   Order By LOC_CODE Asc";
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
    }
}

