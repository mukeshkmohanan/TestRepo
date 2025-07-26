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
    public class ClsInventory
    {
        OracleConnection con;
        DataConnector clsGen = new DataConnector();
        OracleTransaction tr;
        private string sql = "";

        string Part_Description = "";
        public int _count = 0;
        string Bar_Code = "";
        public struct ItemPacking
        {
            public bool ITEM_FOUND;
            public bool ZERO_PACKING;
            public string Category_Code;
            public string Cat_0;
            public string Cat_1;
            public string Cat_2;
            public string Cat_3;
            public string Cat_4;
            public string Cat_5;
            public string Part_Number;
            public string Part_Number_Sup;
            public string Division_Code;
            public string Division_Name;
            public string PART_DESCRIPTION;
            public string Part_Description2;
            public string Brand_Code;
            public string Brand_Name;
            public string Item_Type;
            public string Supplier_Code;
            public string Supplier_Name;
            public string Supplier_ItemCode;
            public decimal Reorder_Level;
            public decimal Min_Order_Qty;
            public decimal Buying_Rate;
            public decimal Max_Stock;
            public string Item_List_Date;
            public string Costing_Method;
            public string Expiry_Item;
            public bool Inactive_Item;
            public string Rec_Status;
            public string Category_Name;
            public string Ref_Part_Number;
            public string Linked_Item;
            public bool Locked_GRV;
            public string Item_Valid_Till;
            public string Bar_Code;
            public string Packing;
            public string PackQty;
            public decimal Wholesale_Rate;
            public decimal Wholesale_Rate1;
            public decimal Wholesale_Rate2;
            public decimal Retail_Price;
            public decimal Min_Sale_Price;
            public decimal Pack_Multiply_Qty;
            public string Promotion_Item;
            public string Price_Code;
            public string Prev_Price_Code;
            public string Last_Price_Change;
            public bool Updated_To_Pos;
            public string Default_Unit;
            public int Packing_Order;
            public bool Locked_Item;

            string Search_Pno;
            string Search_Pno_Sup;
            string Search_BarCode;
            string Search_Packsize;
            string Search;
            public ArrayList Item;

            public void fn_initilize()
            {

            }

            public ItemPacking(string Part_Number, string Part_number_sup, string Supplier_ItemCode, string Packing, string Barcode, char Master_Detail, string Condition_Master, string Condition_Detail, string Order_By, OracleConnection con, OracleTransaction tr)
            {
                /*
                 
                 Search Based On PNo, Bar_Code, Pno_Sup 

                */
                Item = null;
                this.ITEM_FOUND = false;
                this.ZERO_PACKING = false;

                this.Search_Pno = "";
                this.Search_Pno_Sup = "";
                this.Search_BarCode = "";
                this.Search_Packsize = "";
                this.Supplier_ItemCode = "";
                this.Search = "";

                /*  */

                this.Category_Code = "";
                this.Cat_0 = "";
                this.Cat_1 = "";
                this.Cat_2 = "";
                this.Cat_3 = "";
                this.Cat_4 = "";
                this.Cat_5 = "";
                this.Part_Number = "";
                this.Part_Number_Sup = "";
                this.Division_Code = "";
                this.Division_Name = "";
                this.PART_DESCRIPTION = "";
                this.Part_Description2 = "";
                this.Brand_Code = "";
                this.Brand_Name = "";
                this.Item_Type = "";
                this.Supplier_Code = "";
                this.Supplier_Name = "";
                this.Supplier_ItemCode = "";
                this.Reorder_Level = 0;
                this.Min_Order_Qty = 0;
                this.Buying_Rate = 0;
                this.Max_Stock = 0;
                this.Item_List_Date = "";
                this.Costing_Method = "";
                this.Expiry_Item = "";
                this.Inactive_Item = false;
                this.Rec_Status = "";
                this.Category_Name = "";
                this.Ref_Part_Number = "";
                this.Linked_Item = "";
                this.Locked_GRV = false;
                this.Item_Valid_Till = "";
                this.Bar_Code = "";
                this.Packing = "";
                this.PackQty = "";
                this.Wholesale_Rate = 0;
                this.Wholesale_Rate1 = 0;
                this.Wholesale_Rate2 = 0;
                this.Retail_Price = 0;
                this.Min_Sale_Price = 0;
                this.Pack_Multiply_Qty = 0;
                this.Promotion_Item = "";
                this.Price_Code = "";
                this.Prev_Price_Code = "";
                this.Last_Price_Change = "";
                this.Updated_To_Pos = false;
                this.Default_Unit = "";
                this.Packing_Order = 0;
                this.Locked_Item = false;

                Assign_Master(Part_Number, Part_number_sup, Supplier_ItemCode, Packing, Barcode, Master_Detail, Condition_Master, Condition_Detail, Order_By, con, tr);

            }

            public ItemPacking(string Part_Number, string Part_number_sup, char Master_Detail, string Condition_Detail, string Order_By, OracleConnection con)
            {
                /*
                 
                 Search Based On PNo, Bar_Code, Pno_Sup 

                */
                Item = null;
                this.ITEM_FOUND = false;
                this.ZERO_PACKING = false;

                this.Search_Pno = "";
                this.Search_Pno_Sup = "";
                this.Search_BarCode = "";
                this.Search_Packsize = "";
                this.Supplier_ItemCode = "";
                this.Search = "";

                /*  */

                this.Category_Code = "";
                this.Cat_0 = "";
                this.Cat_1 = "";
                this.Cat_2 = "";
                this.Cat_3 = "";
                this.Cat_4 = "";
                this.Cat_5 = "";
                this.Part_Number = "";
                this.Part_Number_Sup = "";
                this.Division_Code = "";
                this.Division_Name = "";
                this.PART_DESCRIPTION = "";
                this.Part_Description2 = "";
                this.Brand_Code = "";
                this.Brand_Name = "";
                this.Item_Type = "";
                this.Supplier_Code = "";
                this.Supplier_Name = "";
                this.Supplier_ItemCode = "";
                this.Reorder_Level = 0;
                this.Min_Order_Qty = 0;
                this.Buying_Rate = 0;
                this.Max_Stock = 0;
                this.Item_List_Date = "";
                this.Costing_Method = "";
                this.Expiry_Item = "";
                this.Inactive_Item = false;
                this.Rec_Status = "";
                this.Category_Name = "";
                this.Ref_Part_Number = "";
                this.Linked_Item = "";
                this.Locked_GRV = false;
                this.Item_Valid_Till = "";
                this.Bar_Code = "";
                this.Packing = "";
                this.PackQty = "";
                this.Wholesale_Rate = 0;
                this.Wholesale_Rate1 = 0;
                this.Wholesale_Rate2 = 0;
                this.Retail_Price = 0;
                this.Min_Sale_Price = 0;
                this.Pack_Multiply_Qty = 0;
                this.Promotion_Item = "";
                this.Price_Code = "";
                this.Prev_Price_Code = "";
                this.Last_Price_Change = "";
                this.Updated_To_Pos = false;
                this.Default_Unit = "";
                this.Packing_Order = 0;
                this.Locked_Item = false;

                Assign_Master(Part_Number, Part_number_sup, "", "", "", Master_Detail, "", Condition_Detail, Order_By, con, null);

            }

            public ItemPacking(string Part_Number, string Part_number_sup, string Packing, string Barcode, char Master_Detail, OracleConnection con)
            {
                /*
                 
                 Search Based On PNo, Bar_Code, Pno_Sup 

                */
                Item = null;
                this.ITEM_FOUND = false;
                this.ZERO_PACKING = true;

                this.Search_Pno = "";
                this.Search_Pno_Sup = "";
                this.Search_BarCode = "";
                this.Search_Packsize = "";
                this.Supplier_ItemCode = "";
                this.Search = "";

                /*  */

                this.Category_Code = "";
                this.Cat_0 = "";
                this.Cat_1 = "";
                this.Cat_2 = "";
                this.Cat_3 = "";
                this.Cat_4 = "";
                this.Cat_5 = "";
                this.Part_Number = "";
                this.Part_Number_Sup = "";
                this.Division_Code = "";
                this.Division_Name = "";
                this.PART_DESCRIPTION = "";
                this.Part_Description2 = "";
                this.Brand_Code = "";
                this.Brand_Name = "";
                this.Item_Type = "";
                this.Supplier_Code = "";
                this.Supplier_Name = "";
                this.Supplier_ItemCode = "";
                this.Reorder_Level = 0;
                this.Min_Order_Qty = 0;
                this.Buying_Rate = 0;
                this.Max_Stock = 0;
                this.Item_List_Date = "";
                this.Costing_Method = "";
                this.Expiry_Item = "";
                this.Inactive_Item = false;
                this.Rec_Status = "";
                this.Category_Name = "";
                this.Ref_Part_Number = "";
                this.Linked_Item = "";
                this.Locked_GRV = false;
                this.Item_Valid_Till = "";
                this.Bar_Code = "";
                this.Packing = "";
                this.PackQty = "";
                this.Wholesale_Rate = 0;
                this.Wholesale_Rate1 = 0;
                this.Wholesale_Rate2 = 0;
                this.Retail_Price = 0;
                this.Min_Sale_Price = 0;
                this.Pack_Multiply_Qty = 0;
                this.Promotion_Item = "";
                this.Price_Code = "";
                this.Prev_Price_Code = "";
                this.Last_Price_Change = "";
                this.Updated_To_Pos = false;
                this.Default_Unit = "";
                this.Packing_Order = 0;
                this.Locked_Item = false;

                Assign_Master(Part_Number, Part_number_sup, "", Packing, Barcode, Master_Detail, "", "", "", con, null);

            }

            public void Assign_Master(string Part_Number, string Part_number_sup, string Supplier_ItemCode, string Packing, string Barcode, char Master_Detail, string Condition_Master, string Condition_Detail, string Order_By, OracleConnection con1, OracleTransaction tr)
            {
                string Sql = "";

                if (Supplier_ItemCode.Equals(""))
                {
                    if (!(Part_Number.Equals("")) && Part_number_sup.Equals(""))
                    {
                        Sql = "select * from INVT_ITEMPACKING where part_number='" + Part_Number + "'";
                    }
                    else if (Part_Number.Equals("") && !(Part_number_sup.Equals("")))
                    {
                        Sql = "select * from INVT_ITEMPACKING where part_number_sup ='" + Part_number_sup + "'";
                    }

                }
                else
                {
                    Sql = "select * from INVT_ITEMPACKING where supplier_itemcode ='" + Supplier_ItemCode + "'";
                }

                if ((!(Packing.Equals("")) && Barcode.Equals("")))
                {
                    Sql = Sql + " and packing='" + Packing + "'";
                }
                else if (!(Barcode.Equals("")) && Sql.Equals(""))
                {
                    Sql = "select * from INVT_ITEMPACKING where barcode ='" + Barcode + "'";
                }
                else if (!(Barcode.Equals("")))
                {
                    Sql = Sql + " and barcode='" + Barcode + "'";
                }

                // sql = "select * from INVT_ITEMPACKING where part_number='" + this.Part_Number + "'";

                Item = new ArrayList();

                Sql = (Condition_Detail.Length > 0) ? Sql + " and " + Condition_Detail : Sql;

                if (Order_By.Length == 0)
                    Sql = Sql + " order by Packing_Order asc ";
                else
                    Sql = Sql + " order by " + Order_By;

                bool Pkg = false;
                OracleCommand cmd = null;
                OracleDataReader rs = null;
                DataConnector dc = new DataConnector();
                OracleConnection con = con1;
                if (tr != null) cmd.Transaction = tr;

                if (Master_Detail.Equals('D') || Master_Detail.Equals('A'))
                {
                    cmd = con.CreateCommand();
                    cmd.CommandText = Sql;
                    rs = cmd.ExecuteReader();
                    while (rs.Read())
                    {
                        Pkg = true;

                        this.Category_Code = rs["Category_Code"].ToString();
                        this.Part_Number = rs["Part_Number"].ToString();
                        this.Part_Number_Sup = rs["Part_Number_Sup"].ToString();
                        this.Brand_Code = rs["Brand_Code"].ToString();
                        this.Item_Type = rs["Item_Type"].ToString();
                        this.ITEM_FOUND = true;
                        this.Bar_Code = rs["barcode"].ToString();
                        this.Packing = rs["packing"].ToString();
                        this.PackQty = rs["packqty"].ToString();
                        this.Wholesale_Rate = decimal.Parse(rs["wholesale_rate"].ToString());
                        this.Wholesale_Rate1 = decimal.Parse(rs["wholesale_rate1"].ToString());
                        this.Wholesale_Rate2 = decimal.Parse(rs["wholesale_rate2"].ToString());
                        this.Retail_Price = decimal.Parse(rs["Retail_Price"].ToString());
                        this.Min_Sale_Price = decimal.Parse(rs["Min_Sale_Price"].ToString());
                        this.Pack_Multiply_Qty = decimal.Parse(rs["Pack_Multiply_Qty"].ToString());
                        this.Promotion_Item = rs["Promotion_Item"].ToString();
                        this.Price_Code = rs["Price_Code"].ToString();
                        this.Prev_Price_Code = rs["Prev_Price_Code"].ToString();
                        this.Last_Price_Change = rs["Last_Price_Change"].ToString();
                        this.Updated_To_Pos = rs["Updated_To_Pos"].ToString().Equals("Y") ? true : false;
                        this.Default_Unit = rs["Default_Unit"].ToString();
                        this.Packing_Order = int.Parse(rs["Packing_Order"].ToString());
                        if (this.Packing_Order == 0) this.ZERO_PACKING = true;
                        this.Locked_Item = rs["Locked_GRV"].ToString().Equals("Y") ? true : false;
                        Item.Add(this);
                    }
                    rs.Close();
                    cmd.Dispose();
                }
                // 
                if (!Pkg)
                {
                    if (Supplier_ItemCode.Equals(""))
                    {
                        if (!(Part_Number.Equals("")) && Part_number_sup.Equals(""))
                        {
                            Sql = "";
                            Sql = "select * from invt_inventorymaster where part_number='" + Part_Number + "'";
                        }
                        else if (Part_Number.Equals("") && !(Part_number_sup.Equals("")))
                        {
                            Sql = "select * from invt_inventorymaster where part_number_sup ='" + Part_number_sup + "'";
                        }
                    }
                    else
                    {
                        Sql = "select * from invt_inventorymaster where supplier_itemcode ='" + Supplier_ItemCode + "'";
                    }
                }
                else
                {
                    Sql = "select * from invt_inventorymaster where part_number='" + this.Part_Number + "'";
                }

                Sql = (Condition_Master.Length > 0) ? Sql + " and " + Condition_Master : Sql;

                if ((Master_Detail.Equals('M') || Master_Detail.Equals('A')))
                {
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    cmd.CommandText = Sql;

                    rs = cmd.ExecuteReader();
                    if (rs.Read())
                    {
                        this.ITEM_FOUND = true;
                        this.Category_Code = rs["Category_Code"].ToString();
                        this.Cat_1 = rs["Cat_1"].ToString();
                        this.Cat_2 = rs["Cat_2"].ToString();
                        this.Cat_3 = rs["Cat_3"].ToString();
                        this.Cat_4 = rs["Cat_4"].ToString();
                        this.Cat_5 = rs["Cat_5"].ToString();
                        this.Part_Number = rs["Part_Number"].ToString();
                        this.Part_Number_Sup = rs["Part_Number_Sup"].ToString();
                        this.Division_Code = rs["Division_Code"].ToString();
                        this.Division_Name = rs["Division_Name"].ToString();
                        this.PART_DESCRIPTION = rs["PART_DESCRIPTION"].ToString();
                        this.Part_Description2 = rs["Part_Description2"].ToString();
                        this.Brand_Code = rs["Brand_Code"].ToString();
                        this.Brand_Name = rs["Brand_Name"].ToString();
                        this.Item_Type = rs["Item_Type"].ToString();
                        this.Supplier_Code = rs["Supplier_Code"].ToString();
                        this.Supplier_Name = rs["Supplier_Name"].ToString();
                        this.Supplier_ItemCode = rs["Supplier_ItemCode"].ToString();
                        this.Reorder_Level = decimal.Parse(rs["Reorder_Level"].ToString());
                        this.Min_Order_Qty = decimal.Parse(rs["Min_Order_Qty"].ToString());
                        this.Buying_Rate = decimal.Parse(rs["Buying_Rate"].ToString());
                        this.Max_Stock = decimal.Parse(rs["Max_Stock"].ToString());
                        this.Item_List_Date = rs["Item_List_Date"].ToString();
                        this.Costing_Method = rs["Costing_Method"].ToString();
                        this.Expiry_Item = rs["Expiry_Item"].ToString();
                        this.Inactive_Item = rs["Inactive_Item"].ToString().Equals("Y") ? true : false;
                        this.Rec_Status = rs["Rec_Status"].ToString();
                        this.Category_Name = rs["Category_Name"].ToString();
                        this.Ref_Part_Number = rs["Ref_Part_Number"].ToString();
                        this.Linked_Item = rs["Linked_Item"].ToString();
                        this.Locked_GRV = rs["Locked_Grv"].ToString().Equals("Y") ? true : false;
                        this.Item_Valid_Till = rs["Item_Valid_Till"].ToString();
                    }
                }
                rs.Close();
                cmd.Dispose();
            }
        }
        public string getItemDescription(string Part_Number_sup, out string Brand, out string Category, out string Division)
        {
            OracleCommand cmd = con.CreateCommand();
            OracleDataReader rs = null;
            Brand = ""; Category = ""; Division = "";
            cmd.CommandText = "Select part_description,brand_name,category_name,division_name from invt_inventorymaster where part_number_sup='" + Part_Number_sup + "'";
            try
            {
                if (tr != null) cmd.Transaction = tr;
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    Brand = rs.IsDBNull(1) ? "" : rs.GetString(1);
                    Category = rs.IsDBNull(2) ? "" : rs.GetString(2);
                    Division = rs.IsDBNull(3) ? "" : rs.GetString(3);
                    return rs[0].ToString();
                }
            }
            catch { }
            finally
            {
                rs.Close();
                cmd.Dispose();
            }
            return "";
        }

        public ClsInventory(string Part_Number, string Condition, OracleConnection con)
        {
            //  ItemPacking AP = new ItemPacking();
            ////  AP.Assign_Master("",Part_Number, "", "", "",'A');

            //  int i = 0;
            //  while (AP.Item.Count-1 > i)
            //  {

            //      MessageBox.Show(AP.Retail_Price.ToString());
            //      MessageBox.Show(((ItemPacking)AP.Item[i]).Retail_Price.ToString());
            //      i++;
            //  }
        }
        public ClsInventory(string Part_Number, string PackSize, string Condition, OracleConnection con)
        {

        }
        public ClsInventory(OracleConnection con, OracleTransaction tr)
        {
            this.con = con;
            this.tr = tr;
        }
        public ClsInventory(OracleConnection con, OracleTransaction tr, string Remote)
        {
            this.con = con;
            this.tr = tr;
        }
        bool DISABLE_LOC_UPDATE = false;
        public ClsInventory(OracleConnection con, OracleTransaction tr, bool DISABLE_LOC_UPDATE)
        {
            this.con = con;
            this.tr = tr;
            this.DISABLE_LOC_UPDATE = DISABLE_LOC_UPDATE;
        }
        public ClsInventory()
        {

        }
        public struct Stock
        {
            string Part_Number;
            string Part_Number_Sup;
            string Loc_Code;
            string MonthName;
            string PackSize;

            decimal OB_Qty;
            decimal Sale_Qty;
            decimal Sale_R_Qty;
            decimal Pur_Qty;
            decimal Pur_R_Qty;
            decimal Tri_Qty;
            decimal Tro_Qty;
            decimal CB_Qty;
            string Brand_Code;
            string Category_Code;

            bool AllPack;

            public Stock(string Part_Number, string Part_Number_Sup, string Loc_Code, string MonthName, string PackSize, bool AllPack)
            {
                this.Part_Number = Part_Number;
                this.Part_Number_Sup = Part_Number_Sup;
                this.Loc_Code = Loc_Code;
                this.MonthName = MonthName;
                this.PackSize = PackSize;
                this.AllPack = AllPack;
                this.Brand_Code = "";
                this.Category_Code = "";

                OB_Qty = 0;
                Sale_Qty = 0;
                Sale_R_Qty = 0;
                Pur_Qty = 0;
                Pur_R_Qty = 0;
                Tri_Qty = 0;
                Tro_Qty = 0;
                CB_Qty = 0;
            }
        }

        public int GetZeroSerial(string Part_Number_Sup, string Packing)
        {
            int ZeroSerial = 0;
            string a, b, c, d, e;
            decimal f = 0;
            GetPackingOrder(Part_Number_Sup, Packing, out ZeroSerial, out a, out  b, out  c, out  d, out e, out f);
            return ZeroSerial;

        }
        public int GetPackingOrder(string Part_Number_Sup, string Packing)
        {
            int ZeroSerial = 0;
            string a, b, c, d, e;
            decimal f = 0;
            return GetPackingOrder(Part_Number_Sup, Packing, out ZeroSerial, out a, out  b, out  c, out  d, out e, out f);
        }
        string DefaultPacking = "";
        //public int GetPackingOrder(string Part_Number_Sup, string Packing, out int ZeroSerial, out string Costing_Method, out string Barcode, out string Link_Code, out string Item_Type)
        //{
        //    OracleCommand cmd = con.CreateCommand();
        //    if (tr != null) cmd.Transaction = tr;

        //    int PackOrder = 0; ZeroSerial = 0;
        //    cmd.CommandText = "select packing_order,zero_serial,packing,linked_item,costing_method,item_type,barcode from INVT_ITEMPACKING where part_number_sup='" + Part_Number_Sup + "'";
        //    OracleDataReader rs = null;
        //    try
        //    {
        //        rs = cmd.ExecuteReader();
        //        bool f1 = false; bool f2 = false;
        //        Costing_Method = "F";
        //        Barcode = "";
        //        Item_Type = "G";
        //        Link_Code = "";
        //        while (rs.Read())
        //        {
        //            if (rs[2].Equals(Packing))
        //            {
        //                Link_Code = rs["Linked_ITEM"].ToString();
        //                if (Link_Code == "")
        //                {
        //                    Item_Type = rs["item_type"].ToString().Trim();
        //                }
        //                else
        //                {
        //                    Item_Type = "L";
        //                }
        //                Costing_Method = rs["costing_method"].ToString();
        //                ZeroSerial = int.Parse(rs[1].ToString());
        //                PackOrder = int.Parse(rs[0].ToString());
        //                Barcode = rs["barcode"].ToString();
        //                f1 = true;
        //            }
        //            if (int.Parse(rs["packing_order"].ToString()) == 1)
        //            {
        //                DefaultPacking = rs["packing"].ToString();
        //                f2 = true;
        //            }
        //            if (f1 && f2) break;
        //        }
        //    }
        //    catch { throw; }
        //    finally
        //    {
        //        if (rs != null) rs.Close();
        //        cmd.Dispose();
        //    }
        //    return PackOrder;
        //}
        string offer_pack = "";

        public int GetPackingOrder(string Part_Number_Sup, string Packing, out int ZeroSerial, out string Costing_Method, out string Barcode, out string Link_Code, out string Item_Type, out string price_code, out  decimal buying_rate)
        {
            OracleCommand cmd = con.CreateCommand();
            if (tr != null) cmd.Transaction = tr;

            int PackOrder = 0; ZeroSerial = 0;
            cmd.CommandText = "select packing_order,zero_serial,packing,linked_item,costing_method,item_type,barcode,price_code,buying_rate,pack_multiply_qty,offer_pack from INVT_ITEMPACKING where part_number_sup='" + Part_Number_Sup + "'";
            OracleDataReader rs = null;
            try
            {
                rs = cmd.ExecuteReader();
                bool f1 = false; bool f2 = false;
                Costing_Method = "F";
                Barcode = "";
                Item_Type = "G";
                Link_Code = "";
                price_code = "";
                buying_rate = 0;
                while (rs.Read())
                {
                    if (rs[2].Equals(Packing))
                    {
                        Link_Code = rs["Linked_ITEM"].ToString();
                        if (Link_Code == "")
                        {
                            Item_Type = rs["item_type"].ToString().Trim();
                        }
                        else
                        {
                            Item_Type = "L";
                        }
                        Costing_Method = rs["costing_method"].ToString();
                        ZeroSerial = int.Parse(rs[1].ToString());
                        PackOrder = int.Parse(rs[0].ToString());
                        Barcode = rs["barcode"].ToString();
                        price_code = rs["price_code"].ToString(); ;
                        buying_rate = ZeroSerial == 0 ? decimal.Parse(rs["buying_rate"].ToString()) / decimal.Parse(rs["pack_multiply_qty"].ToString()) : decimal.Parse(rs["buying_rate"].ToString());
                        offer_pack = rs["offer_pack"].ToString();
                        f1 = true;
                    }
                    if (int.Parse(rs["packing_order"].ToString()) == 1)
                    {
                        DefaultPacking = rs["packing"].ToString();
                        f2 = true;
                    }
                    if (f1 && f2) break;
                }
            }
            catch { throw; }
            finally
            {
                if (rs != null) rs.Close();
                cmd.Dispose();
            }
            return PackOrder;
        }

        //public bool AddToTranStock(string Loc_Code, string Part_Number_Sup, string Packing, string Item_Type, string Doc_No, string Doc_Type, string Grv_Type, decimal QTY_LUQ, decimal Val, decimal FOC_LUQ, decimal PackQty, decimal Pack_Multiply_Qty, string Doc_Year, string Doc_Date, string To_Loc_Code, OracleConnection con, OracleTransaction tr, string TranBaseLoc)
        //{
        //    if (Defaults.Def_MAIN_LOCATION)
        //    {
        //        string Sql = "select loc_code,link_name from sys_dbconnectionstrings where active='Y' AND INDIVIDUAL_LOC='Y' AND machine='SERVER' and loc_code not in('" + Defaults.Def_Main_Loc + "','" + TranBaseLoc + "')";
        //        OracleCommand cmd = con.CreateCommand();
        //        cmd.CommandText = Sql;
        //        cmd.Transaction = tr;
        //        OracleDataReader rs = cmd.ExecuteReader();
        //        while (rs.Read())
        //        {
        //            // CHECK THIS CONDITION LATER
        //            if (!clsGen.insertTable("TRAN_STOCK", "LOC_CODE,PART_NUMBER_SUP,PACKING,ITEM_TYPE,DOC_NO,DOC_TYPE,GRV_TYPE,LUQ,PRODUCT_VALUE,FOC_LUQ,PACKQTY,PACK_MULTIPLY_QTY,DOC_YEAR,DOC_DATE,TO_LOC,TRAN_BASE_LOC,TRAN_TO_LOC", "'" + Loc_Code + "','" + Part_Number_Sup + "','" + Packing + "','" + Item_Type + "','" + Doc_No + "','" + Doc_Type + "','" + Grv_Type + "'," + QTY_LUQ + "," + Val + "," + FOC_LUQ + "," + PackQty + "," + Pack_Multiply_Qty + ",'" + Doc_Year + "','" + DateTime.Parse(Doc_Date).ToString("dd/MMM/yyyy") + "','" + To_Loc_Code + "','" + TranBaseLoc + "','" + rs["loc_code"].ToString() + "'", con, tr))
        //            {
        //                // tr.Rollback();
        //                throw (new Exception("Cannot Insert To " + rs["loc_code"].ToString()));
        //            }
        //        }
        //    }
        //    else
        //    {
        //        clsGen.insertTable("TRAN_STOCK", "LOC_CODE,PART_NUMBER_SUP,PACKING,ITEM_TYPE,DOC_NO,DOC_TYPE,GRV_TYPE,LUQ,PRODUCT_VALUE,FOC_LUQ,PACKQTY,PACK_MULTIPLY_QTY,DOC_YEAR,DOC_DATE,TO_LOC,TRAN_BASE_LOC,TRAN_TO_LOC", "'" + Loc_Code + "','" + Part_Number_Sup + "','" + Packing + "','" + Item_Type + "','" + Doc_No + "','" + Doc_Type + "','" + Grv_Type + "'," + QTY_LUQ + "," + Val + "," + FOC_LUQ + "," + PackQty + "," + Pack_Multiply_Qty + ",'" + Doc_Year + "','" + DateTime.Parse(Doc_Date).ToString("dd/MMM/yyyy") + "','" + To_Loc_Code + "','" + Defaults.Def_Base_LOC + "','" + Defaults.Def_Main_Loc + "'", con, tr);

        //    }
        //    return true;
        //}

        /*      
         * public bool AddToTranStock(string Loc_Code, string Part_Number_Sup, string Packing, string Item_Type, string Doc_No, string Doc_Type, string Grv_Type, decimal QTY_LUQ, decimal Val, decimal FOC_LUQ, decimal PackQty, decimal Pack_Multiply_Qty, string Doc_Year, string Doc_Date, string To_Loc_Code, OracleConnection con, OracleTransaction tr,string TranBaseLoc)
        {
            if (Defaults.Def_MAIN_LOCATION)
            {
                string Sql = "select loc_code,link_name from sys_dbconnectionstrings where active='Y' AND INDIVIDUAL_LOC='Y' AND machine='SERVER' and loc_code not in('" + Defaults.Def_Main_Loc + "','" + TranBaseLoc + "')";
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = Sql;
                cmd.Transaction = tr;
                OracleDataReader rs = cmd.ExecuteReader();

                ToLog = ToLog + " Add To Tran Sel Qry " + Sql + "\r\n";

                while (rs.Read())
                {
                    // CHECK THIS CONDITION LATER
                    // if (ClsGeneral.Exists("Select link_name from sys_dbconnectionstrings where link_name='" + rs["link_name"].ToString() + "'", con, tr)) ;
                    {
                        //    continue;
                    }

                    string InsQl="TRAN_STOCK , LOC_CODE,PART_NUMBER_SUP,PACKING,ITEM_TYPE,DOC_NO,DOC_TYPE,GRV_TYPE,LUQ,PRODUCT_VALUE,FOC_LUQ,PACKQTY,PACK_MULTIPLY_QTY,DOC_YEAR,DOC_DATE,TO_LOC,TRAN_BASE_LOC,TRAN_TO_LOC" + " , " + "'" + Loc_Code + "','" + Part_Number_Sup + "','" + Packing + "','" + Item_Type + "','" + Doc_No + "','" + Doc_Type + "','" + Grv_Type + "'," + QTY_LUQ + "," + Val + "," + FOC_LUQ + "," + PackQty + "," + Pack_Multiply_Qty + ",'" + Doc_Year + "','" + DateTime.Parse(Doc_Date).ToString("dd/MMM/yyyy") + "','" + To_Loc_Code + "','" +  TranBaseLoc + "','" + rs["loc_code"].ToString() + "'";
                    ToLog = ToLog + " MAINLOC: Add To Tran INSERTION Qry " + InsQl + "\r\n";
                    

                    if (!clsGeneral.insertTable("TRAN_STOCK", "LOC_CODE,PART_NUMBER_SUP,PACKING,ITEM_TYPE,DOC_NO,DOC_TYPE,GRV_TYPE,LUQ,PRODUCT_VALUE,FOC_LUQ,PACKQTY,PACK_MULTIPLY_QTY,DOC_YEAR,DOC_DATE,TO_LOC,TRAN_BASE_LOC,TRAN_TO_LOC", "'" + Loc_Code + "','" + Part_Number_Sup + "','" + Packing + "','" + Item_Type + "','" + Doc_No + "','" + Doc_Type + "','" + Grv_Type + "'," + QTY_LUQ + "," + Val + "," + FOC_LUQ + "," + PackQty + "," + Pack_Multiply_Qty + ",'" + Doc_Year + "','" + DateTime.Parse(Doc_Date).ToString("dd/MMM/yyyy") + "','" + To_Loc_Code + "','" +  TranBaseLoc + "','" + rs["loc_code"].ToString() + "'", con, tr))
                    {
                        
                        // tr.Rollback();
                        throw (new Exception("Cannot Insert To " + rs["loc_code"].ToString()));
                    }
                }
            }
            else
            {
                string InsQl = "TRAN_STOCK, LOC_CODE,PART_NUMBER_SUP,PACKING,ITEM_TYPE,DOC_NO,DOC_TYPE,GRV_TYPE,LUQ,PRODUCT_VALUE,FOC_LUQ,PACKQTY,PACK_MULTIPLY_QTY,DOC_YEAR,DOC_DATE,TO_LOC,TRAN_BASE_LOC,TRAN_TO_LOC  '" + Loc_Code + "','" + Part_Number_Sup + "','" + Packing + "','" + Item_Type + "','" + Doc_No + "','" + Doc_Type + "','" + Grv_Type + "'," + QTY_LUQ + "," + Val + "," + FOC_LUQ + "," + PackQty + "," + Pack_Multiply_Qty + ",'" + Doc_Year + "','" + DateTime.Parse(Doc_Date).ToString("dd/MMM/yyyy") + "','" + To_Loc_Code + "','" + Defaults.Def_Base_LOC  + "','" + Defaults.Def_Main_Loc + "'";
                ToLog = ToLog + " SUBLOC: Add To Tran INSERTION Qry " + InsQl + "\r\n";
                clsGeneral.insertTable("TRAN_STOCK", "LOC_CODE,PART_NUMBER_SUP,PACKING,ITEM_TYPE,DOC_NO,DOC_TYPE,GRV_TYPE,LUQ,PRODUCT_VALUE,FOC_LUQ,PACKQTY,PACK_MULTIPLY_QTY,DOC_YEAR,DOC_DATE,TO_LOC,TRAN_BASE_LOC,TRAN_TO_LOC", "'" + Loc_Code + "','" + Part_Number_Sup + "','" + Packing + "','" + Item_Type + "','" + Doc_No + "','" + Doc_Type + "','" + Grv_Type + "'," + QTY_LUQ + "," + Val + "," + FOC_LUQ + "," + PackQty + "," + Pack_Multiply_Qty + ",'" + Doc_Year + "','" + DateTime.Parse(Doc_Date).ToString("dd/MMM/yyyy") + "','" + To_Loc_Code + "','" + Defaults.Def_Base_LOC  + "','" + Defaults.Def_Main_Loc + "'", con, tr);
            }
            return true;
        }
       
        */
        //////public bool AddToTranStock(string Loc_Code, string Part_Number_Sup, string Packing, string Item_Type, string Doc_No, string Doc_Type, string Grv_Type, decimal QTY_LUQ, decimal Val, decimal FOC_LUQ, decimal PackQty, decimal Pack_Multiply_Qty, string Doc_Year, string Doc_Date, string To_Loc_Code, OracleConnection con, OracleTransaction tr, string BASE_LOC)
        //////{
        //////    if (Defaults.Def_MAIN_LOCATION)
        //////    {
        //////        string Sql = "select DISTINCT loc_code,link_name from sys_dbconnectionstrings where ACTIVE='Y' and INDIVIDUAL_LOC='Y' AND mACHINE='SERVER' AND loc_code not in('" + Defaults.Def_Main_Loc + "','" + BASE_LOC + "')";
        //////        OracleCommand cmd = con.CreateCommand();
        //////        cmd.CommandText = Sql;
        //////        cmd.Transaction = tr;
        //////        OracleDataReader rs = cmd.ExecuteReader();
        //////        while (rs.Read())
        //////        {
        //////            // CHECK THIS CONDITION LATER
        //////            // if (ClsGeneral.Exists("Select link_name from sys_dbconnectionstrings where link_name='" + rs["link_name"].ToString() + "'", con, tr)) ;
        //////            {
        //////                //    continue;
        //////            }
        //////            if (!clsGen.insertTable("TRAN_STOCK", "LOC_CODE,PART_NUMBER_SUP,PACKING,ITEM_TYPE,DOC_NO,DOC_TYPE,GRV_TYPE,LUQ,PRODUCT_VALUE,FOC_LUQ,PACKQTY,PACK_MULTIPLY_QTY,DOC_YEAR,DOC_DATE,TO_LOC,BASE_LOC", "'" + Loc_Code + "','" + Part_Number_Sup + "','" + Packing + "','" + Item_Type + "','" + Doc_No + "','" + Doc_Type + "','" + Grv_Type + "'," + QTY_LUQ + "," + Val + "," + FOC_LUQ + "," + PackQty + "," + Pack_Multiply_Qty + ",'" + Doc_Year + "','" + Doc_Date + "','" + To_Loc_Code + "','" + rs["loc_code"].ToString() + "'", con, tr))
        //////            {
        //////                // tr.Rollback();
        //////                throw (new Exception("Cannot Insert To " + rs["loc_code"].ToString()));
        //////            }
        //////        }
        //////    }
        //////    else
        //////    {
        //////        clsGen.insertTable("TRAN_STOCK", "LOC_CODE,PART_NUMBER_SUP,PACKING,ITEM_TYPE,DOC_NO,DOC_TYPE,GRV_TYPE,LUQ,PRODUCT_VALUE,FOC_LUQ,PACKQTY,PACK_MULTIPLY_QTY,DOC_YEAR,DOC_DATE,TO_LOC,BASE_LOC", "'" + Loc_Code + "','" + Part_Number_Sup + "','" + Packing + "','" + Item_Type + "','" + Doc_No + "','" + Doc_Type + "','" + Grv_Type + "'," + QTY_LUQ + "," + Val + "," + FOC_LUQ + "," + PackQty + "," + Pack_Multiply_Qty + ",'" + Doc_Year + "','" + Doc_Date + "','" + To_Loc_Code + "','" + Defaults.Def_Main_Loc + "'", con, tr);
        //////    }   
        //////    return true;
        //////}

        string TranDate;
        bool ModifyLc = false;
        public bool Skip_LC_Update = false;
        private decimal LC_ForTranDtl = 0;
        private decimal Buying_rate = 0;
        string Price_Code = "";
        private decimal LC_ForStBalance = 0, LC_ForStBalanceToLoc = 0;

        public bool UpdateStock(string Loc_Code, string Part_Number_Sup, string Packing, string Item_Type, string Doc_No, string Doc_Type, string GRV_TYPE, decimal QTY_LUQ, decimal Val, decimal FOC_LUQ, decimal PackQty, decimal Pack_Multiply_Qty, string Doc_Year, string Doc_Date, string To_Loc_Code)
        {
            // return true;
            return UpdateStock(Loc_Code, Part_Number_Sup, Packing, Item_Type, Doc_No, Doc_Type, GRV_TYPE, QTY_LUQ, Val, FOC_LUQ, PackQty, Pack_Multiply_Qty, Doc_Year, Doc_Date, To_Loc_Code, 0, true);
        }
        public bool UpdateStock(string Loc_Code, string Part_Number_Sup, string Packing, string Item_Type, string Doc_No, string Doc_Type, string GRV_TYPE, decimal QTY_LUQ, decimal Val, decimal FOC_LUQ, decimal PackQty, decimal Pack_Multiply_Qty, string Doc_Year, string Doc_Date, string To_Loc_Code, decimal Unit_Rate)
        {
            return UpdateStock(Loc_Code, Part_Number_Sup, Packing, Item_Type, Doc_No, Doc_Type, GRV_TYPE, QTY_LUQ, Val, FOC_LUQ, PackQty, Pack_Multiply_Qty, Doc_Year, Doc_Date, To_Loc_Code, Unit_Rate, true);
        }
        public bool UpdateStock(string Loc_Code, string Part_Number_Sup, string Packing, string Item_Type, string Doc_No, string Doc_Type, string GRV_TYPE, decimal QTY_LUQ, decimal Val, decimal FOC_LUQ, decimal PackQty, decimal Pack_Multiply_Qty, string Doc_Year, string Doc_Date, string To_Loc_Code, decimal Unit_Rate, bool boolAddToTranStock)
        {
            if (Doc_Type == "SI") Doc_Type = "SAL";

            ModifyLc = false;

            /*
             * 
             * KEEP THE ORDER OF FUNCTION CALLS as,
             *   1) Dtls 2) Tran 3) Balance
             * 
             */



            Loc_Code = Loc_Code.Trim();
            To_Loc_Code = To_Loc_Code.Trim();
            Part_Number_Sup = Part_Number_Sup.Trim();
            Doc_Year = Doc_Year.Trim();
            Doc_No = Doc_No.Trim();
            Packing = Packing.Trim();

            try
            {
                if (QTY_LUQ == 0) return true;

                bool Ret = false;

                if (con.State != ConnectionState.Open)
                {
                    throw (new Exception("Connection Not Available"));//return Ret;
                }
                TranDate = Doc_Date;
                if (Doc_Date.Length > 12)
                {
                    DateTime dt1 = DateTime.Parse(Doc_Date);
                    int h = dt1.Hour;
                    if (dt1.Hour >= 0 && dt1.Hour <= 6)
                    {
                        TranDate = (dt1.AddDays(-1)).ToString("dd/MMM/yyyy");
                        TranDate = (dt1).ToString("dd/MMM/yyyy");
                    }
                }

                Doc_Date = DateTime.Parse(Doc_Date).ToString("dd/MMM/yyyy");
                Doc_Date = Doc_Date.ToUpper();

                string Linked_Item = "", Costing_Method = "";
                Item_Type = "G";
                int PackingOrder = 0;
                int ZeroSerial = 0;

                PackingOrder = GetPackingOrder(Part_Number_Sup, Packing, out ZeroSerial, out Costing_Method, out Bar_Code, out Linked_Item, out Item_Type, out Price_Code, out Buying_rate);

                if (Linked_Item != "") Item_Type = "L";
                OracleCommand cmd = null;
                OracleDataReader rs = null;
                string SysDate = Doc_Date;
                SysDate = clsGen.SysDate(con, tr, false); // comment by abi
                if (DateTime.Parse(Doc_Date) > DateTime.Parse(SysDate))
                {
                    Exception ex = new Exception("Invalid Transaction Date.");
                    throw (ex);
                }
                if (DateTime.Parse(SysDate).Year == DateTime.Parse(Doc_Date).Year && DateTime.Parse(Doc_Date).Month != DateTime.Parse(SysDate).Month)
                {
                    ///MessageBox.Show("Month");
                    //   Exception ex = new Exception("Not In Same Month, Please Report IT For Help.");
                    //   throw (ex);
                }
                Doc_Type = Doc_Type.ToUpper();
                Packing = Packing.ToUpper();
                Part_Number_Sup = Part_Number_Sup.ToUpper();
                Part_Number_Sup = Part_Number_Sup.ToUpper();
                Item_Type = Item_Type.ToUpper();
                Loc_Code = Loc_Code.ToUpper();
                //QTY_LUQ = QTY_LUQ + FOC_LUQ;


                switch (Item_Type.ToUpper())
                {
                    case "G":
                        UpdateTranDtls(Part_Number_Sup, Doc_Type, Loc_Code, Doc_Date, Val, QTY_LUQ, Packing, Doc_No, To_Loc_Code, FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);
                        UpdateItemTran(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, Unit_Rate);
                        UpdateStockBalance(Loc_Code, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, To_Loc_Code, Doc_Date, SysDate, PackingOrder, ZeroSerial, Packing);
                        break;
                    // return true;
                    case "L":
                        if (Doc_Type.Equals("SAL") || Doc_Type.Equals("SRT"))
                        {
                            //stock,grvdate,tran_type
                            string Asc = "";
                            if (Costing_Method.Equals("L")) Asc = "DESC";
                            string zz = "";
                            if (ZeroSerial == 0)
                            {

                                zz = " and packing_order=1";
                            }
                            else
                            {
                                zz = " and zero_serial=" + ZeroSerial;
                            }
                            //if (ZeroSerial == 0)
                            //{sql = "select distinct B.part_number_sup,cb_qty,last_grv_date,PACKING,pack_multiply_qty from (invt_itemtran_dtls a INNER JOIN invt_inventorybalance b  on a.loc_code=b.loc_code and a.part_number_sup=b.part_number_sup and a.zero_serial=b.zero_serial) inner join invt_itempacking c on a.part_number_sup=c.part_number_sup  and a.zero_serial=c.zero_serial WHERE b.loc_code='" + Loc_Code + "' and c.zero_serial=" + ZeroSerial + " and packing_order=1 and  b.linked_ITEM='" + Linked_Item + "'  and cb_qty>0  order by last_grv_date" + Asc; }
                            //else
                            //{sql = "select distinct B.part_number_sup,cb_qty,last_grv_date,PACKING,pack_multiply_qty from (invt_itemtran_dtls a INNER JOIN invt_inventorybalance b  on a.loc_code=b.loc_code and a.part_number_sup=b.part_number_sup and a.zero_serial=b.zero_serial) inner join invt_itempacking c on a.part_number_sup=c.part_number_sup  and a.zero_serial=c.zero_serial WHERE b.loc_code='" + Loc_Code + "' and c.zero_serial=" + ZeroSerial + " and packing_order=0 and  b.linked_ITEM='" + Linked_Item + "'  and cb_qty>0  order by last_grv_date" + Asc;
                            //}
                            sql = "select distinct B.part_number_sup,cb_qty,c.pack_multiply_qty,a.packing,last_grv_date  from (invt_itemtran_dtls a INNER JOIN invt_inventorybalance b on a.loc_code=b.loc_code and a.part_number_sup=b.part_number_sup and a.zero_serial=b.zero_serial) inner join invt_itempacking c on a.part_number_sup=c.part_number_sup  and a.zero_serial=c.zero_serial WHERE a.loc_code='" + Loc_Code + "' and c.linked_ITEM='" + Linked_Item + "' " + zz + " order by last_grv_date " + Asc;
                            OracleDataReader rs1 = null;
                            OracleCommand cmd1 = con.CreateCommand();
                            try
                            {
                                cmd1.CommandText = sql;
                                cmd1.Transaction = tr;
                                rs1 = cmd1.ExecuteReader();
                                bool Zero = true;
                                while (rs1.Read())
                                {
                                    Zero = false;
                                    decimal Balance = 0;
                                    decimal Stock = rs1.GetDecimal(1);
                                    PackingOrder = 1;
                                    Packing = rs1["packing"].ToString();
                                    Pack_Multiply_Qty = decimal.Parse(rs1["pack_multiply_qty"].ToString());
                                    if (Stock <= 0) continue;
                                    Balance = Stock - QTY_LUQ;
                                    if (Balance < 0)
                                    {
                                        UpdateTranDtls(rs1["part_number_sup"].ToString(), Doc_Type, Loc_Code, Doc_Date, (Val / QTY_LUQ) * Stock, Stock, Packing, Doc_No, "", FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);
                                        UpdateItemTran(Loc_Code, TranDate, Doc_Type, rs1["part_number_sup"].ToString(), Stock, (Val / QTY_LUQ) * Stock, FOC_LUQ, Pack_Multiply_Qty, Packing, "", SysDate, ZeroSerial, PackingOrder, Unit_Rate);
                                        UpdateStockBalance(Loc_Code, Doc_Type, rs1["part_number_sup"].ToString(), Stock, (Val / QTY_LUQ) * Stock, "", ZeroSerial, PackingOrder, Packing);
                                        //Update QTY_LUQ = "stock" 
                                        QTY_LUQ = QTY_LUQ - Stock;
                                    }
                                    else
                                    {
                                        UpdateTranDtls(rs1["part_number_sup"].ToString(), Doc_Type, Loc_Code, Doc_Date, Val, QTY_LUQ, Packing, Doc_No, "", FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);
                                        UpdateItemTran(Loc_Code, TranDate, Doc_Type, rs1["part_number_sup"].ToString(), QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, "", SysDate, ZeroSerial, PackingOrder, Unit_Rate);
                                        UpdateStockBalance(Loc_Code, Doc_Type, rs1["part_number_sup"].ToString(), QTY_LUQ, Val, "", ZeroSerial, PackingOrder, Packing);
                                        QTY_LUQ = 0;
                                        break;
                                    }
                                }
                                if (QTY_LUQ != 0 || (Zero && QTY_LUQ != 0))
                                {
                                    UpdateTranDtls(Part_Number_Sup, Doc_Type, Loc_Code, Doc_Date, Val, QTY_LUQ, Packing, Doc_No, "", FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);
                                    UpdateItemTran(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, "", SysDate, ZeroSerial, PackingOrder, Unit_Rate);
                                    UpdateStockBalance(Loc_Code, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, "", ZeroSerial, PackingOrder, Packing);
                                }
                                rs1.Close();
                                cmd1.Dispose();
                            }
                            catch { throw; }
                            finally { if (rs1 != null) rs1.Dispose(); cmd1.Dispose(); }
                        }
                        else
                        {
                            UpdateTranDtls(Part_Number_Sup, Doc_Type, Loc_Code, Doc_Date, Val, QTY_LUQ, Packing, Doc_No, To_Loc_Code, FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);
                            UpdateItemTran(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, Unit_Rate);
                            UpdateStockBalance(Loc_Code, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, To_Loc_Code, Doc_Date, SysDate, PackingOrder, ZeroSerial, Packing);

                            //UpdateStockBalance(Loc_Code, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, To_Loc_Code, Doc_Date, SysDate, PackingOrder, ZeroSerial, Packing);
                            //UpdateItemTran(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder);
                            //UpdateTranDtls(Part_Number_Sup, Doc_Type, Loc_Code, Doc_Date, Val, QTY_LUQ, Packing,Doc_No, To_Loc_Code, FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);
                        }
                        break;

                    case "X":
                    case "B":
                        //OracleCommand  cmd = con.CreateCommand();
                        int SubItemCount = 0;
                        try
                        {
                            if (Item_Type == "X")
                            {
                                UpdateTranDtls(Part_Number_Sup, Doc_Type, Loc_Code, Doc_Date, Val, QTY_LUQ, Packing, Doc_No, To_Loc_Code, FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);//UpdateTranDtls(Part_Number_Sup, Doc_Type, Loc_Code, Doc_Date, Val, QTY_LUQ, Packing,Doc_No, To_Loc_Code, FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);
                                decimal LuggageMainItemRetailPrice = 0;
                                if (ApplayLuggageValue(Part_Number_Sup, out LuggageMainItemRetailPrice))
                                {
                                    // ApplayLuggageCost(Part_Number_Sup, LuggageMainItemRetailPrice, Val / QTY_LUQ);
                                    //Added By DD && Abu On 08/Jan/2011 09:55 AM For Limiting Cost Updation Only On Purchase Time and Transfer Time
                                    if (Doc_Type == "PRH" || Doc_Type == "PRR" || Doc_Type == "OB" || Doc_Type == "GTV" || Doc_Type == "ADJ")
                                    {
                                        ApplayLuggageCost(Part_Number_Sup, LuggageMainItemRetailPrice, Val / QTY_LUQ);
                                    }
                                    //Added By DD && Abu On 08/Jan/2011 09:55 AM For Limiting Cost Updation Only On Purchase Time and Transfer Time

                                }
                            }
                            else
                            {
                                if (Doc_Type == "PRH" || Doc_Type == "PRR") throw (new Exception("Invalid Doc Type For Bundle Item"));
                            }
                            cmd = con.CreateCommand();
                            //if (Doc_Type == "PRH" || Doc_Type == "PRR")                            
                            //Updated By DD && Abu On 08/Jan/2011 09:55 AM For Limiting Value On Case of Sale Time
                            if (Doc_Type == "PRH" || Doc_Type == "PRR" || Doc_Type == "OB" || Doc_Type == "GTV" || Doc_Type == "ADJ")
                            {
                                cmd.CommandText = "select Pack_Multiply_Qty,asy_qty,item_bundle_Cost,packing,part_number_sup from invt_assemblysub where ASY_part_number_sup='" + Part_Number_Sup + "'";
                            }
                            else
                            {
                                cmd.CommandText = "select Pack_Multiply_Qty,asy_qty,item_bundle_value,packing,part_number_sup from invt_assemblysub where ASY_part_number_sup='" + Part_Number_Sup + "'";
                            }
                            if (tr != null) cmd.Transaction = tr;
                            rs = cmd.ExecuteReader();
                            while (rs.Read())
                            {
                                SubItemCount++;
                                UpdateStock(Loc_Code, rs["part_number_sup"].ToString(), rs["packing"].ToString(), "", Doc_No, Doc_Type, "", (QTY_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)), (QTY_LUQ * rs.GetDecimal(2)), (FOC_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)), rs.GetDecimal(1), rs.GetDecimal(0), Doc_Year, Doc_Date, To_Loc_Code, Unit_Rate, false);//UpdateStock(Loc_Code, rs["part_number_sup"].ToString(), rs["packing"].ToString(), "", Doc_No, Doc_Type, "", (QTY_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)),(QTY_LUQ * rs.GetDecimal(1) * rs.GetDecimal(2) * rs.GetDecimal(0)), FOC_LUQ, rs.GetDecimal(1), rs.GetDecimal(0), Doc_Year, Doc_Date, To_Loc_Code);
                            }
                            if (SubItemCount <= 0)
                            {
                                throw (new Exception(" Bundle Sub Items Not Found"));
                            }
                            UpdateItemTran_SPECIAL(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code);

                        }
                        catch { throw; }
                        finally { if (rs != null) rs.Close(); cmd.Dispose(); }
                        break;
                    default:
                        Exception Ex = new Exception("STOCK UPDATION ERROR - INVALID ITEM TYPE");
                        throw (Ex);
                }
            }
            catch (Exception Ee)
            {
                throw;
            }
            // added on apr 28 10
            //    if (!AddToTranStock(Loc_Code, Part_Number_Sup, Packing, Item_Type, Doc_No, Doc_Type, GRV_TYPE, QTY_LUQ, Val, FOC_LUQ, PackQty, Pack_Multiply_Qty, Doc_Year, Doc_Date, To_Loc_Code, con, tr,Defaults.Def_Base_LOC))
            //    {
            //        throw (new Exception("NOT ADDED TO TRANSFER"));
            //    }
            //
            if (boolAddToTranStock)
            {
                if (!AddToTranStock(Loc_Code, Part_Number_Sup, Packing, Item_Type, Doc_No, Doc_Type, GRV_TYPE, QTY_LUQ, Val, FOC_LUQ, PackQty, Pack_Multiply_Qty, Doc_Year, Doc_Date, To_Loc_Code, con, tr, (Defaults.Def_Base_Individual_Loc ? Defaults.Def_Base_LOC : Defaults.Def_Base_Management_LOC), Unit_Rate, false))
                {
                    throw (new Exception("NOT ADDED TO TRANSFER"));
                }
            }


            ModifyLc = false;
            //Skip_LC_Update = false;
            LC_ForTranDtl = 0;
            LC_ForStBalance = 0;
            LC_ForStBalanceToLoc = 0;


            return true;

        }

        public bool AddToTranStock(string Loc_Code, string Part_Number_Sup, string Packing, string Item_Type, string Doc_No, string Doc_Type, string Grv_Type, decimal QTY_LUQ, decimal Val, decimal FOC_LUQ, decimal PackQty, decimal Pack_Multiply_Qty, string Doc_Year, string Doc_Date, string To_Loc_Code, OracleConnection con, OracleTransaction tr, string TranBaseLoc, decimal Unit_Price, bool UPDATE_LC_ONLY)
        {
            if (Defaults.Def_MAIN_LOCATION)
            {
                string Sql = "select loc_code,link_name from sys_dbconnectionstrings where COUNTRY_CODE='" + Defaults.Def_Country + "' AND LOC_TYPE<>'CN' AND active='Y' AND INDIVIDUAL_LOC='Y' AND machine='SERVER' and loc_code not in('" + Defaults.Def_Main_Loc + "','" + TranBaseLoc + "')";
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText = Sql;
                cmd.Transaction = tr;
                OracleDataReader rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    // CHECK THIS CONDITION LATER
                    // if (ClsGeneral.Exists("Select link_name from sys_dbconnectionstrings where link_name='" + rs["link_name"].ToString() + "'", con, tr)) ;
                    {
                        //    continue;
                    }
                    if (!clsGen.insertTable("TRAN_STOCK", "LOC_CODE,PART_NUMBER_SUP,PACKING,ITEM_TYPE,DOC_NO,DOC_TYPE,GRV_TYPE,LUQ,PRODUCT_VALUE,FOC_LUQ,PACKQTY,PACK_MULTIPLY_QTY,DOC_YEAR,DOC_DATE,TO_LOC,TRAN_BASE_LOC,TRAN_TO_LOC,UNIT_PRICE,SKIP_LC,ONLY_LC_UPDATE,price_code", "'" + Loc_Code + "','" + Part_Number_Sup + "','" + Packing + "','" + Item_Type + "','" + Doc_No + "','" + Doc_Type + "','" + Grv_Type + "'," + QTY_LUQ + "," + Val + "," + FOC_LUQ + "," + PackQty + "," + Pack_Multiply_Qty + ",'" + Doc_Year + "','" + DateTime.Parse(Doc_Date).ToString("dd/MMM/yyyy") + "','" + To_Loc_Code + "','" + TranBaseLoc + "','" + rs["loc_code"].ToString() + "'," + Unit_Price + ",'" + (Skip_LC_Update ? "Y" : "N") + "','" + (UPDATE_LC_ONLY ? "Y" : "N") + "','" + Price_Code + "'", con, tr))
                    {                        // tr.Rollback();
                        throw (new Exception("Cannot Insert To " + rs["loc_code"].ToString()));
                    }
                }
            }
            else
            {
                clsGen.insertTable("TRAN_STOCK", "LOC_CODE,PART_NUMBER_SUP,PACKING,ITEM_TYPE,DOC_NO,DOC_TYPE,GRV_TYPE,LUQ,PRODUCT_VALUE,FOC_LUQ,PACKQTY,PACK_MULTIPLY_QTY,DOC_YEAR,DOC_DATE,TO_LOC,TRAN_BASE_LOC,TRAN_TO_LOC,UNIT_PRICE,SKIP_LC,ONLY_LC_UPDATE,price_code", "'" + Loc_Code + "','" + Part_Number_Sup + "','" + Packing + "','" + Item_Type + "','" + Doc_No + "','" + Doc_Type + "','" + Grv_Type + "'," + QTY_LUQ + "," + Val + "," + FOC_LUQ + "," + PackQty + "," + Pack_Multiply_Qty + ",'" + Doc_Year + "','" + DateTime.Parse(Doc_Date).ToString("dd/MMM/yyyy") + "','" + To_Loc_Code + "','" + TranBaseLoc + "','" + Defaults.Def_Main_Loc + "'," + Unit_Price + ",'" + (Skip_LC_Update ? "Y" : "N") + "','" + (UPDATE_LC_ONLY ? "Y" : "N") + "','" + Price_Code + "'", con, tr);
            }
            return true;
        }



        public bool ApplayLuggageValue(string stLuggageItem, out decimal LuggageMainItemRetailPrice)//public bool ApplayLuggageValue(string stLuggageItem, decimal LuggageSubItemQty, decimal LuggageSubItemRetailPrice, decimal LuggageSubItemsNetRetailPrice, decimal LuggageMainItemRetailPrice, OracleConnection oCon, OracleTransaction Tr)
        {
            decimal LuggageSubItemsNetRetailPrice = 0;            //stLuggageItem = "ROUND((("+ LuggageMainItemRetailPrice + "*(((ASY_QTY*SELLINGPRICE)*100)/" + LuggageSubItemsNetRetailPrice + "))/100),2)";
            //LuggageMainItemRetailPrice = decimal.Parse(gridPacking["RetailPrice1", 0].Value.ToString());
            OracleCommand Cmd = null;
            OracleCommand Cmd1 = null;
            OracleDataReader Rs = null;
            OracleDataReader Rs1 = null;
            long intRowsAffected = 0;
            string Sql = "";
            try
            {
                Sql = "Select RETAIL_PRICE From INVT_ITEMPACKING Where PART_NUMBER_SUP='" + stLuggageItem + "' And PACKING_ORDER=1";
                LuggageMainItemRetailPrice = decimal.Parse(clsGen.GetValue(Sql, con, tr));
                if (LuggageMainItemRetailPrice <= 0)
                {
                    Exception ex = new Exception("Error on Update LuggageValue (Price Error)");
                    throw (ex);
                }
                Sql = "Select PART_NUMBER_SUP,PACKING,ASY_QTY From INVT_ASSEMBLYSUB Where ASY_PART_NUMBER_SUP='" + stLuggageItem + "'";//0291260
                Cmd = con.CreateCommand();
                Cmd.CommandText = Sql;
                if (tr != null) Cmd.Transaction = tr;
                Rs = Cmd.ExecuteReader();
                if (Rs.HasRows)
                {
                    while (Rs.Read())
                    {
                        Sql = "Select RETAIL_PRICE as SELLINGPRICE From INVT_ITEMPACKING Where PART_NUMBER_SUP='" + Rs["PART_NUMBER_SUP"].ToString() + "' And PACKING='" + Rs["PACKING"].ToString() + "'";
                        Cmd1 = con.CreateCommand();
                        Cmd1.CommandText = Sql;
                        if (tr != null) Cmd1.Transaction = tr;
                        Rs1 = Cmd1.ExecuteReader();
                        if (Rs1.HasRows)
                        {
                            if (Rs1.Read())
                            {
                                intRowsAffected = 0;
                                LuggageSubItemsNetRetailPrice = LuggageSubItemsNetRetailPrice + decimal.Parse(Rs1["SELLINGPRICE"].ToString()) * decimal.Parse(Rs["ASY_QTY"].ToString());
                                Sql = "Update INVT_ASSEMBLYSUB Set SELLINGPRICE=" + Rs1["SELLINGPRICE"].ToString() + ",SPRICETOTAL=(ASY_QTY*(" + Rs1["SELLINGPRICE"].ToString() + "))" +
                                      " Where ASY_PART_NUMBER_SUP ='" + stLuggageItem + "' And PART_NUMBER_SUP='" + Rs["PART_NUMBER_SUP"].ToString() + "' And PACKING='" + Rs["PACKING"].ToString() + "'";
                                if (!((clsGen.ExecuteCmd(Sql, out intRowsAffected, con, tr)) && intRowsAffected > 0))
                                {
                                    Exception ex1 = new Exception("Error on Update Luggagecost");
                                    throw (ex1);
                                }
                            }
                        }
                        else
                        {
                            Exception ex = new Exception("Error on Update LuggageValue (Sub Item Selling Price)");
                            throw (ex);
                        }
                    }
                    //Sql = "Update INVT_ASSEMBLYSUB Set ITEM_BUNDLE_VALUE=" + "(" + newLandingCost + "*(ITEM_BUNDLE_VALUE*100/" + RetailPrice + ")/100)" + " Where ASY_PART_NUMBER_SUP ='" + stLuggageItem + "'";
                    intRowsAffected = 0;
                    Sql = "Update INVT_ASSEMBLYSUB Set ITEM_BUNDLE_VALUE=" +
                          "ROUND(((" + LuggageMainItemRetailPrice + "*(((ASY_QTY*SELLINGPRICE)*100)/" + LuggageSubItemsNetRetailPrice + "))/100),2)" +
                          " Where ASY_PART_NUMBER_SUP ='" + stLuggageItem + "'";
                    if (!((clsGen.ExecuteCmd(Sql, out intRowsAffected, con, tr)) && intRowsAffected > 0))
                    {
                        Exception ex = new Exception("Error on Update Luggagecost");
                        throw (ex);
                    }
                    Rs.Close();
                    return true;
                }
                else
                {
                    Exception ex = new Exception("Error on Update LuggageValue");
                    throw (ex);
                }
            }
            catch (Exception Exp)
            {
                throw (Exp);
            }
            finally
            {
                if (Cmd != null) Cmd.Dispose();
                if (Rs != null) Rs.Dispose();
            }

        }

        public bool ApplayLuggageCost(string stLuggageItem, decimal RetailPrice, decimal newLandingCost)
        {
            string Sql = "";
            try
            {
                long intRowsAffected = 0;
                Sql = "Update INVT_ASSEMBLYSUB Set ITEM_BUNDLE_COST=" +
                      "(" + newLandingCost + "*(ITEM_BUNDLE_VALUE*100/" + RetailPrice + ")/100)" +
                      " Where ASY_PART_NUMBER_SUP ='" + stLuggageItem + "'";
                if ((clsGen.ExecuteCmd(Sql, out intRowsAffected, con, tr)) && intRowsAffected > 0)
                    return true;
                else
                {
                    Exception ex = new Exception("Error on Update Luggagecost");
                    return false;
                }
            }
            catch (Exception Exp)
            {
                Exception ex = new Exception("Error on Update Luggagecost");
                throw (ex);
            }
            return false;
        }
        private bool UpdateBundleValue(string Part_number)
        {
            return true;
            //clsGen.updateTable("invt_assemblysub","item_bundle_cost=" + ""
        }
        private bool UpdateItemTran_SPECIAL(string Loc_Code, string Doc_Date, string Doc_Type, string Part_Number_Sup, decimal Qty, decimal Val, decimal FOC, decimal Pack_Multiply_Qty, string Packing, string To_Loc)
        {
            //PD,cat,Brand,sup_code,sup_name,
            DateTime Dt = DateTime.Parse(Doc_Date);
            int Result = -1;
            OracleCommand cmd = null;
            try
            {
                string Sql = "Update INVT_SPECIAL_ITEMTRANSACTION ", Sql1 = "";
                switch (Doc_Type.ToUpper())
                {
                    case "SAL":

                        Sql = Sql + " set SALE_QTY=sale_qty+" + Qty + ",sale_val=sale_val+" + Val;

                        if (FOC != 0)
                        {
                            Sql = Sql + " ,FOC_SALE=FOC_SALE+" + FOC;
                        }
                        break;
                    case "SRT":

                        Sql = Sql + " set SALE_R_QTY=sale_R_qty+" + Qty + ",sale_R_val=sale_R_val+" + Val;

                        if (FOC != 0)
                        {
                            Sql = Sql + ",FOC_SRT=FOC_SRT+" + Qty;
                        }
                        break;
                    case "PRH":

                        Sql = Sql + " set PRH_QTY=PRH_qty+" + Qty + ",PRH_val=PRH_val+" + Val;

                        if (FOC != 0)
                        {
                            Sql = Sql + " ,FOC_PRH=FOC_PRH+" + Qty;
                        }
                        break;
                    case "PRR":

                        Sql = Sql + " set PRH_R_QTY=PRH_R_qty+" + Qty + ",PRH_R_val=PRH_R_val+" + Val;

                        if (FOC != 0)
                        {
                            Sql = Sql + " set FOC_PRR=FOC_PRR+" + Qty;
                        }
                        break;
                    case "GTV":
                    case "ADJ":
                        Sql1 = Sql + " set TRI_QTY=TRI_QTY+" + Qty + ",TRI_val=TRI_val+" + Val + " where loc_code='" + To_Loc + "' and doc_date='" + Doc_Date + "' and part_number_Sup='" + Part_Number_Sup + "' and packing='" + Packing + "'";
                        Sql = Sql + " set TRO_QTY=TRO_QTY+" + Qty + ",TRo_val=TRo_val+" + Val + "";
                        break;
                    default:
                        Exception ex = new Exception("Invalid Document Type.");
                        throw (ex);
                }

                Sql = Sql + " where loc_code='" + Loc_Code + "' and doc_date='" + Doc_Date + "' and part_number_Sup='" + Part_Number_Sup + "' and packing='" + Packing + "'";
            ReSl:
                cmd = con.CreateCommand();
                cmd.CommandText = Sql;
                if (tr != null) cmd.Transaction = tr;
                Result = cmd.ExecuteNonQuery();
                if (Result == 0)
                {
                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    cmd.CommandText = "insert into INVT_SPECIAL_ITEMTRANSACTION(doc_date,loc_code,doc_year,doc_month,doc_day,packing,pack_multiply_qty,part_number_sup,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM) select  '" + Doc_Date + "' as doc_date,'" + Loc_Code + "' as loc_code,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Packing + "' as packing," + Pack_Multiply_Qty + " as pack_multiply_qty,'" + Part_Number_Sup + "' as part_number_sup,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                    Result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (Result > 0) goto ReSl;
                    return false;
                }
                if (Result > 0 && !(Sql1.Equals("")))
                {
                ReTr:
                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    cmd.CommandText = Sql1;
                    Result = cmd.ExecuteNonQuery();
                    if (Result == 0)
                    {
                        cmd.Dispose();
                        cmd = con.CreateCommand();
                        if (tr != null) cmd.Transaction = tr;
                        cmd.CommandText = "insert into INVT_SPECIAL_ITEMTRANSACTION(doc_date,loc_code,doc_year,doc_month,doc_day,packing,pack_multiply_qty,part_number_sup,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM) select  '" + Doc_Date + "' as doc_date,'" + To_Loc + "' as loc_code,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Packing + "' as packing," + Pack_Multiply_Qty + " as pack_multiply_qty,'" + Part_Number_Sup + "' as part_number_sup,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                        Result = cmd.ExecuteNonQuery();
                        if (Result > 0) goto ReTr;
                        return false;
                    }
                };
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw; ;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
            if (Result <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        private bool UpdateItemTran(string Loc_Code, string Doc_Date, string Doc_Type, string Part_Number_Sup, decimal Qty, decimal Val, decimal FOC, decimal Pack_Multiply_Qty, string Packing, string To_Loc, string SysDate, int ZeroSerial, int PackingOrder, decimal Unit_Rate)
        {
            //
            // 10 , 10*5

            //---
            DateTime Dt = DateTime.Parse(Doc_Date);
            decimal AQty = Qty;
            int Result = -1;
            OracleCommand cmd = null;
            try
            {

                if (ZeroSerial > 0 && Pack_Multiply_Qty > 1 && Doc_Type == "GTV")
                {

                }

                string Sql = "Update invt_itemtransaction ", Sql1 = "";
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
                            //
                        }
                        Sql = Sql + " set SALE_QTY=sale_qty+" + Qty + ",offer_pack='" + offer_pack + "',price_code='" + Price_Code + "',sale_val=sale_val+" + Val;

                        if (FOC != 0)
                        {
                            Sql = Sql + ",FOC_SALE=FOC_SALE+" + FOC;
                        }
                        AQty = -1 * Qty;
                        break;
                    case "SRT":

                        Sql = Sql + " set SALE_R_QTY=sale_R_qty+" + Qty + ",offer_pack='" + offer_pack + "',price_code='" + Price_Code + "',sale_R_val=sale_R_val+" + Val;

                        if (FOC != 0)
                        {
                            Sql = Sql + " ,FOC_SRT=FOC_SRT+" + FOC;
                        }
                        break;
                    case "PRH":
                        Sql = Sql + " set PRH_QTY=PRH_qty+" + Qty + ",offer_pack='" + offer_pack + "',price_code='" + Price_Code + "',PRH_val=PRH_val+" + Val + (LC_ForTranDtl <= 0 ? "" : ",LC=" + LC_ForTranDtl);
                        if (FOC != 0)
                        {
                            Sql = Sql + " ,FOC_PRH=FOC_PRH+" + FOC;
                        }
                        break;
                    case "PRR":

                        Sql = Sql + " set PRH_R_QTY=PRH_R_qty+" + Qty + ",offer_pack='" + offer_pack + "',price_code='" + Price_Code + "',PRH_R_val=PRH_R_val+" + Val;

                        if (FOC != 0)
                        {
                            Sql = Sql + " ,FOC_PRR=FOC_PRR+" + FOC;
                        }
                        AQty = -1 * Qty;
                        break;
                    case "GTV":
                    case "ADJ":
                        Sql1 = " TRI_QTY=TRI_QTY+" + Qty + ",offer_pack='" + offer_pack + "',price_code='" + Price_Code + "',TRI_val=TRI_val+" + Val + ",CB_QTY=CB_QTY+" + Qty + (ModifyLc ? ",LC=" + LC_ForTranDtl : "") + " where loc_code='" + To_Loc + "' and doc_date='" + Doc_Date + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial='" + ZeroSerial + "'";
                        Sql = Sql + " set TRR_QTY=TRR_QTY+" + Qty + ",offer_pack='" + offer_pack + "',price_code='" + Price_Code + "',TRR_val=TRR_val+" + Val;
                        AQty = -1 * Qty;
                        break;
                    default:
                        Exception ex = new Exception("Invalid Document Type.");
                        throw (ex);

                }

                string upd = ",cb_qty=cb_qty+" + AQty;
            ReSl: sql = Sql + " " + upd + " where doc_date='" + Doc_Date + "' and  loc_code='" + Loc_Code + "'  and part_number_Sup='" + Part_Number_Sup + "' and zero_serial='" + ZeroSerial + "'";
                cmd = con.CreateCommand();
                cmd.CommandText = sql;
                if (tr != null) cmd.Transaction = tr;
                Result = cmd.ExecuteNonQuery();
                if (Result > 0 && SysDate.ToUpper() != Doc_Date.ToUpper()) AdjustStockTran(Doc_Date, Loc_Code, Part_Number_Sup, SysDate, AQty, ZeroSerial);
                decimal Ob_Qty = 0;
                decimal Lc_Loc = 0;
                if (Result == 0)
                {

                    Ob_Qty = getStock_Tran(Part_Number_Sup, Loc_Code, Doc_Date, ZeroSerial, SysDate, out Lc_Loc, Pack_Multiply_Qty, false, Packing);
                    if (Lc_Loc == 0 && LC_ForTranDtl != 0) Lc_Loc = LC_ForTranDtl;
                    LC_ForStBalance = Lc_Loc;
                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    if (ZeroSerial == 0 && PackingOrder != 0)
                    {
                        cmd.CommandText = "insert into invt_itemtransaction(doc_date,loc_code,doc_year,doc_month,doc_day,part_number_sup,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name) select  '" + Doc_Date + "' as doc_date,'" + Loc_Code + "' as loc_code,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,default_packing,1," + Ob_Qty + " as ob_qty,0 as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                    }
                    else
                    {
                        cmd.CommandText = "insert into invt_itemtransaction(doc_date,loc_code,doc_year,doc_month,doc_day,part_number_sup,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name) select  '" + Doc_Date + "' as doc_date,'" + Loc_Code + "' as loc_code,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,'" + Packing + "'," + Pack_Multiply_Qty + "," + Ob_Qty + " as ob_qty," + ZeroSerial + " as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name  from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                    }
                    Result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (Result > 0) goto ReSl;
                    Exception ex = new Exception("New Item Tran Error.");
                    throw (ex);

                }
                if (Result > 0 && !(Sql1.Equals("")))
                {
                ReTr:
                    AQty = -1 * Qty;
                    Sql = "update invt_itemtransaction set " + Sql1;
                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    cmd.CommandText = Sql;
                    Result = cmd.ExecuteNonQuery();
                    if (Result > 0 && SysDate.ToUpper() != Doc_Date.ToUpper()) AdjustStockTran(Doc_Date, Loc_Code, Part_Number_Sup, SysDate, AQty, ZeroSerial);
                    if (Result == 0)
                    {
                        Ob_Qty = getStock_Tran(Part_Number_Sup, To_Loc, Doc_Date, ZeroSerial, SysDate, out Lc_Loc, Pack_Multiply_Qty, false, Packing);
                        if (Lc_Loc == 0 && LC_ForTranDtl != 0) Lc_Loc = LC_ForTranDtl;
                        LC_ForStBalanceToLoc = Lc_Loc;
                        cmd.Dispose();
                        //Ob_Qty = getStock(Part_Number_Sup, To_Loc);
                        cmd = con.CreateCommand();
                        if (tr != null) cmd.Transaction = tr;
                        if (ZeroSerial == 0 && PackingOrder != 0)
                        {
                            cmd.CommandText = "insert into invt_itemtransaction(doc_date,loc_code,doc_year,doc_month,doc_day,part_number_sup,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name) select  '" + Doc_Date + "' as doc_date,'" + To_Loc + "' as loc_code,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,default_packing,1," + Ob_Qty + " as ob_qty,0 as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name  from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                        }
                        else
                        {
                            cmd.CommandText = "insert into invt_itemtransaction(doc_date,loc_code,doc_year,doc_month,doc_day,part_number_sup,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name) select  '" + Doc_Date + "' as doc_date,'" + To_Loc + "' as loc_code,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,'" + Packing + "'," + Pack_Multiply_Qty + "," + Ob_Qty + " as ob_qty," + ZeroSerial + " as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name  from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                        }
                        Result = cmd.ExecuteNonQuery();
                        //      decimal Stock = this.getStock(Part_Number_Sup, To_Loc );
                        //      Sql1 = " ob_Qty=" + Stock + "," +Sql1;
                        if (Result > 0) goto ReTr;
                        Exception ex = new Exception("New Item Tran Error.");
                        throw (ex);

                    }
                };
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
            if (Result <= 0)
            {
                Exception ex = new Exception("Cannot Update Stock.");
                throw (ex);
            }
            else
            {
                return true;
            }
        }

        // 
        private bool AdjustStockTran(string Doc_Date, string Loc_code, string Part_Number, string sysdate, decimal Qty, int ZeroSerial)
        {
            DateTime dt = DateTime.Parse(Doc_Date);
            DateTime SysDate = DateTime.Parse(sysdate);
            dt = dt.AddDays(1);
            OracleCommand cmd = con.CreateCommand();

            cmd.Transaction = tr;
            cmd.CommandText = "update invt_itemtransaction set cb_qty=cb_qty+" + Qty + ",ob_qty=ob_qty+" + Qty + " where Loc_code='" + Loc_code + "' and doc_date>='" + dt.ToString("dd/MMM/yyyy") + "' and doc_date<='" + sysdate + "' and Part_number_sup='" + Part_Number + "' and Zero_serial=" + ZeroSerial;
            cmd.ExecuteNonQuery().ToString();
            cmd.Dispose();
            return true;
        }

        private decimal getStock(string Part_Number_Sup, string Loc_Code, int ZeroSerial)
        {
            //          MessageBox.Show("S");
            string Sql = "select cb_Qty from invt_inventorybalance where loc_code='" + Loc_Code + "' and part_number_sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial;
            OracleCommand cmd = con.CreateCommand();
            cmd.Transaction = tr;
            OracleDataReader rs = null;
            cmd.CommandText = Sql;
            try
            {
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    if (rs.IsDBNull(0)) return 0;
                    return rs.GetDecimal(0);
                }
            }
            catch { throw; }
            finally
            {
                if (rs != null) rs.Close();
                cmd.Dispose();
            }
            return 0;
        }

        private decimal getStock_Tran(string Part_Number_Sup, string Loc_Code, string doc_date, int zeroSerial, string Sysdate, out decimal LC, decimal PackMultiplyQty, bool FROM_TRAN, string Packing)
        {

            LC = 0;
            decimal Ob = 0;
            string Sql = "";
            bool Found = false;
            if (FROM_TRAN) Sql = "select cb_Qty,LC from invt_itemtransaction where doc_date<'" + doc_date + "' and  loc_code='" + Loc_Code + "'  and part_number_sup='" + Part_Number_Sup + "' and zero_serial=" + zeroSerial + " order by doc_date desc";
            //if(DateTime.Parse(doc_date).Month==DateTime.Parse(Sysdate).Month)
            else Sql = "select cb_Qty,LC from invt_inventorybalance where loc_code='" + Loc_Code + "' and part_number_sup='" + Part_Number_Sup + "' and zero_serial=" + zeroSerial;
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                cmd = con.CreateCommand();
                cmd.Transaction = tr;
                cmd.CommandText = Sql;
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    Found = true;
                    if (rs.IsDBNull(0)) Ob = 0;
                    else Ob = decimal.Parse(rs[0].ToString());
                    if (rs.IsDBNull(1)) LC = 0;
                    else LC = decimal.Parse(rs[1].ToString());
                    if (FROM_TRAN) return Ob;
                }
                rs.Close();
                cmd.Dispose();
                if (FROM_TRAN) Found = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (rs != null) rs.Dispose();
                if (cmd != null) cmd.Dispose();
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
                    //if (LC1 > 0 && zeroSerial != 0)
                    //LC1 = LC1 / PackMultiplyQty;
                    if (LC1 > 0) LC = LC1;
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return Ob;



            //LC = 0;
            //decimal Ob = 0;
            //string Sql = "select cb_Qty,LC from invt_itemtransaction where doc_date<'" + doc_date + "' and  loc_code='" + Loc_Code + "'  and part_number_sup='" + Part_Number_Sup + "' and zero_serial=" + zeroSerial + " order by doc_date desc";
            //if(DateTime.Parse(doc_date).Month==DateTime.Parse(Sysdate).Month)
            //   Sql = "select cb_Qty,LC from invt_inventorybalance where loc_code='" + Loc_Code + "' and part_number_sup='" + Part_Number_Sup + "' and zero_serial="+ zeroSerial ;
            //OracleCommand cmd = null;
            //OracleDataReader rs=null;
            //try
            //{
            //    cmd = con.CreateCommand();
            //    cmd.Transaction = tr;
            //    cmd.CommandText = Sql;
            //    rs = cmd.ExecuteReader();
            //    if (rs.Read())
            //    {
            //        if (rs.IsDBNull(0)) Ob = 0;
            //        else Ob = decimal.Parse(rs[0].ToString());
            //        if (rs.IsDBNull(1)) LC  = 0;
            //        else LC = decimal.Parse(rs[1].ToString());
            //    }
            //    rs.Close();
            //    cmd.Dispose();
            //    if (LC <= 0)
            //    {
            //        LC = getLC(Loc_Code, Part_Number_Sup, zeroSerial);
            //        if (LC > 0 && zeroSerial != 0) LC = LC / PackMultiplyQty;
            //    }
            //}
            //catch 
            //{
            //    throw;
            //}
            //finally
            //{
            //    if(rs!=null) rs.Dispose();
            //    if(cmd!=null) cmd.Dispose();
            //}
            //return Ob ;
        }

        public void getLastTran(string TRANMODE, string Part_Number_Sup, string Doc_Date, string Loc_Code, int ZeroSerial, out string Date, out  decimal Qty, out  decimal Value)
        {
            Qty = 0; Value = 0; Date = "";
            string Sel = "";
            string cond = "";
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

            string Sql = "select " + Sel + " from invt_itemtransaction where loc_code='" + Loc_Code + "' and doc_date<='" + Doc_Date + "' and part_number_sup='" + Part_Number_Sup + "' " + cond;
            OracleCommand cmd = con.CreateCommand();
            cmd.Transaction = tr;
            cmd.CommandText = Sql;
            OracleDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                if (rs.IsDBNull(0)) return;
                Date = DateTime.Parse(rs[0].ToString()).ToString("dd/MMM/yyyy");
                Qty = decimal.Parse(rs[1].ToString());
                Value = decimal.Parse(rs[2].ToString());
            }
            rs.Close();
            cmd.Dispose();
            return;
        }

        private bool UpdateTranDtls(string Part_Number_Sup, string Doc_Type, string LOC_CODE, string Doc_Date, decimal Val, decimal Qty, string Packing, string Doc_NO, string To_LOC, decimal Foc, int PackingOrder, int ZeroSerial, decimal Pack_Multi)
        {
            /* MUST DO THE UPDATE CASE PROPERLY LATER */
            //Qty = Qty + Foc;

            LC_ForTranDtl = 0;
            decimal LC = 0;
            if (Doc_Type == "PRH" || Doc_Type == "GTV")
            {
                LC = Val / Qty;
                if (ZeroSerial != 0)
                    LC = LC * Pack_Multi;
                LC_ForTranDtl = LC;
                if (LC == 0)
                {
                    LC = Buying_rate;
                    LC_ForTranDtl = Buying_rate;
                }
                if (Defaults.G_LC_CALCULATION.Equals("WEIGHTEDAVG"))
                {
                    decimal OLDLC = decimal.Parse(getLC(LOC_CODE, Part_Number_Sup, ZeroSerial, Packing).ToString());
                    decimal Stck = decimal.Parse(getStock(Part_Number_Sup, LOC_CODE, ZeroSerial).ToString());
                    if ((Stck + Qty) != 0)
                    {
                        LC = (((LC * Qty) + ((OLDLC * Stck))) / (Stck + Qty));
                        LC_ForTranDtl = LC;
                    }   
                }
            }

            int Result = -1;
            OracleCommand cmd = null;
            try
            {
                string Sql = "Update invt_itemtran_dtls ", Sql1 = "";
                switch (Doc_Type.ToUpper())
                {
                    case "SAL":
                        if (Qty < 0)
                        {
                            getLastTran("SAL", Part_Number_Sup, Doc_Date, LOC_CODE, ZeroSerial, out Doc_Date, out Qty, out Val);
                            if (Doc_Date == "")
                            {
                                Sql = Sql + " set LAST_ISSUE_QTY=0,LAST_ISSUE_DATE=null,SC=0";
                            }
                            else
                                Sql = Sql + " set LAST_ISSUE_QTY=" + Qty + ",LAST_ISSUE_DATE='" + Doc_Date + "',SC=" + LC;
                        }
                        else
                        {
                            if (Foc == Qty)
                            {
                                Sql = Sql + " set LAST_ISSUE_QTY=" + Qty + ",LAST_ISSUE_DATE='" + Doc_Date + "'";
                            }
                            else Sql = Sql + " set LAST_ISSUE_QTY=" + Qty + ",LAST_ISSUE_DATE='" + Doc_Date + "',SC=" + LC;
                        }
                        break;
                    case "PRH":
                        string LcUpd = ",LC=" + LC + ",lc_purch=" + LC;
                        if (Skip_LC_Update) LcUpd = "";
                        if (Qty < 0)
                        {
                            getLastTran("PRH", Part_Number_Sup, Doc_Date, LOC_CODE, ZeroSerial, out Doc_Date, out Qty, out Val);
                            if (Doc_Date == "")
                            {
                                Sql = Sql + " set LAST_GRV_QTY=0,LAST_GRV_DATE=null,LAST_GRV_NO=null,LC=0,lc_purch=0";
                            }
                            else
                            {
                                Sql = Sql + " set LAST_GRV_QTY=" + Qty + ",LAST_GRV_DATE='" + Doc_Date + "',LAST_GRV_NO='" + Doc_NO + "'" + LcUpd;
                                ModifyLc = true;
                            }
                        }
                        else
                        {
                            if (Foc == Qty)
                            {
                                Sql = Sql + " set LAST_GRV_QTY=" + Qty + ",LAST_GRV_DATE='" + Doc_Date + "',LAST_GRV_NO='" + Doc_NO + "'";
                            }
                            else
                            {
                                Sql = Sql + " set LAST_GRV_QTY=" + Qty + ",LAST_GRV_DATE='" + Doc_Date + "',LAST_GRV_NO='" + Doc_NO + "'" + LcUpd;
                                ModifyLc = true;
                            }
                        }
                        if (Skip_LC_Update) ModifyLc = false;
                        break;
                    case "GTV":
                        string NonFoc = "";
                        string LCDate = "";
                        string LCDebDate = "";
                        string LCDateTo = "";
                        string LCDebDateTo = "";
                        if (!Skip_LC_Update && (Foc != Qty)) NonFoc = " ,lc=" + LC + ",LC_TR=" + LC;
                        Sql = Sql + " set LAST_TRr_QTY=" + Qty + ",LAST_TRr_DATE='" + Doc_Date + "',LAST_TRr_LOC='" + To_LOC + "',LAST_TRr_NO='" + Doc_NO + "'";
                        clsGen.Exists("invt_itemtran_dtls", "case when LAST_GRV_DATE is null or ( LAST_GRV_DATE<last_tri_date and last_tri_date is not null) then last_tri_date else LAST_GRV_DATE end as LAST_GRV_DATE,last_dbn_date", "loc_code='" + LOC_CODE + "' and part_number_sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial, "", 2, out LCDate, out  LCDebDate, con, tr);
                        clsGen.Exists("invt_itemtran_dtls", "case when LAST_GRV_DATE is null or ( LAST_GRV_DATE<last_tri_date and last_tri_date is not null) then last_tri_date else LAST_GRV_DATE end as LAST_GRV_DATE,last_dbn_date", "loc_code='" + To_LOC + "' and part_number_sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial, "", 2, out LCDateTo, out  LCDebDateTo, con, tr);
                        ModifyLc = true;


                        //if (LCDate.Equals("") && LCDebDate.Equals(""))
                        //{
                        //    LCDate = clsGen.GetValue("SELECT LAST_GRV_DATE,LOC_CODE,last_dbn_date FROM invt_itemtran_dtls WHERE part_number_sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial + " order by last_grv_date desc nulls last", con, tr);
                        // }

                        if (LCDebDate != "" && LCDate != "" && DateTime.Parse(LCDate) < DateTime.Parse(LCDebDate))
                        {
                            LCDate = LCDebDate;
                        }
                        if (LCDebDateTo != "" && DateTime.Parse(LCDateTo) < DateTime.Parse(LCDebDateTo))
                        {
                            LCDateTo = LCDebDateTo;
                        }
                        if (LCDate != "" && LCDateTo != "")
                        {
                            if (DateTime.Parse(LCDate) < DateTime.Parse(LCDateTo))
                            { NonFoc = ""; ModifyLc = false; }
                        }
                        if (Skip_LC_Update) ModifyLc = false;
                        Sql1 = "update invt_itemtran_dtls set  LAST_TRI_QTY=" + Qty + ",LAST_TRI_DATE='" + Doc_Date + "',LAST_TRI_LOC='" + LOC_CODE + "',LAST_TRI_NO='" + Doc_NO + "' " + NonFoc + " where Loc_code='" + To_LOC + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial='" + ZeroSerial + "'";
                        break;
                    default:
                        return true;
                    //  Exception ex = new Exception("Invalid Document Type.");
                    //  throw (ex);
                }
                if (Skip_LC_Update)
                {
                    LC_ForTranDtl = 0;
                    ModifyLc = false;
                }

                Sql = Sql + " where loc_code='" + LOC_CODE + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial='" + ZeroSerial + "'";
            ReSl:

                if (Doc_Type.ToUpper().Equals("PRH") && Qty != Foc)
                {
                    UpdateLocLcs(LOC_CODE, LC, Part_Number_Sup, ZeroSerial, PackingOrder, Packing, Doc_Date, Doc_Type, Doc_NO, LOC_CODE);
                }
                if (Doc_Type.ToUpper().Equals("GTV") && Qty != Foc && ModifyLc)
                {
                    UpdateLocLcs(To_LOC, LC, Part_Number_Sup, ZeroSerial, PackingOrder, Packing, Doc_Date, Doc_Type, Doc_NO, LOC_CODE);
                }


                cmd = con.CreateCommand();
                cmd.CommandText = Sql;
                if (tr != null) cmd.Transaction = tr;
                Result = cmd.ExecuteNonQuery();

                /* ENABLE THIS CODE IN CASE IF NEEDED IN FUTURE
                else if (Doc_Type.ToUpper().Equals("GTV") && Qty != Foc)
                {
                    UpdateLocLcs(To_LOC  , LC, Part_Number_Sup, ZeroSerial, PackingOrder, Packing);
                }
                */

                if (Result == 0)
                {
                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    if (PackingOrder != 0)
                    {
                        cmd.CommandText = "insert into INVT_itemtran_dtls(PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,zero_serial,packing) select PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,'" + LOC_CODE + "' as LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,0 as zero_serial,'" + Packing + "' as packing from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                    }
                    else
                    {
                        cmd.CommandText = "insert into INVT_itemtran_dtls(PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,zero_serial,packing) select PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,'" + LOC_CODE + "' as LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD," + ZeroSerial + " as zero_serial,'" + Packing + "' as packing from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                    }
                    Result = cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    if (Result > 0) goto ReSl;
                    Exception ex = new Exception("Cannot Update New Item Details.");
                    throw (ex);
                }
                if (Result > 0 && !(Sql1.Equals("")))
                {
                ReTr:
                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    cmd.CommandText = Sql1;
                    Result = cmd.ExecuteNonQuery();
                    if (Result == 0)
                    {
                        cmd.Dispose();
                        cmd = con.CreateCommand();
                        if (tr != null) cmd.Transaction = tr;
                        //                    cmd.CommandText = "insert into invt_itemtran_dtls(PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,zero_serial) select PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,'" + To_LOC + "' as LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD," + ZeroSerial + " as zero_serial  from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                        if (PackingOrder != 0)
                        {
                            cmd.CommandText = "insert into INVT_itemtran_dtls(PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,zero_serial,packing) select PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,'" + To_LOC + "' as LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,0 as zero_serial,'" + Packing + "' as packing from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                        }
                        else
                        {
                            cmd.CommandText = "insert into INVT_itemtran_dtls(PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,zero_serial,packing) select PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,'" + To_LOC + "' as LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD," + ZeroSerial + " as zero_serial,'" + Packing + "' as packing from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                        }

                        Result = cmd.ExecuteNonQuery();
                        //////if (Doc_Type.ToUpper().Equals("PRH"))
                        //////{
                        //////    UpdateLocLcs(LOC_CODE, LC , Part_Number_Sup, ZeroSerial);
                        //////}
                        if (Result > 0) goto ReTr;
                        Exception ex = new Exception("Cannot Update New Item Details.");
                        throw (ex);
                    }
                };
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw; ;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
            if (Result <= 0)
            {
                Exception ex = new Exception("Cannot Update Stock.");
                throw (ex);
            }
            else
            {
                return true;
            }
        }
        /*
        private bool UpdateLocLcs(string  LOC_CODE,decimal  LC,string  Part_Number_Sup,int ZeroSerial)
        {
            string sql = "select group_loc from comn_location where loc_code='" + LOC_CODE + "'";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
            if (tr != null) cmd.Transaction = tr;
            OracleDataReader rs = cmd.ExecuteReader();
            if(rs.Read())
            {
                if (rs.IsDBNull(0)) return true;
               clsGen.updateTable("invt_itemtran_dtls", "LC=" + LC, "loc_code='" + rs[0].ToString() + "' and loc_code<>'" + LOC_CODE + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial,con,tr );
            }
            rs.Close();
            cmd.Dispose();
            return true;
        }
        */
        public bool UpdateLocLcs(string LOC_CODE, decimal LC, string Part_Number_Sup, int ZeroSerial, int PackingOrder, string Packing, string DATE, string DOC_TYPE, string DOC_NO, string BaseLoc)
        {
            if (Skip_LC_Update) return true;
            string sql = "select a.loc_code,a.loc_type from comn_location a inner join comn_location b on a.invt_group_loc=b.invt_group_loc where b.loc_code='" + LOC_CODE + "' and a.active='Y'";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
            OracleDataReader rs = null;
            if (tr != null) cmd.Transaction = tr;
            try
            {
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    int roweffect = 0;
                    if (rs.IsDBNull(0) || rs["loc_type"].ToString() == "MN") continue;
                    //if (LOC_CODE == rs["loc_code"].ToString() || BaseLoc == rs["loc_code"].ToString()) continue;

                    string DT = "";
                    if (DOC_TYPE == "PRH")
                    {
                        DT = ",LAST_GRV_NO='" + DOC_NO + "',LAST_GRV_DATE='" + DATE + "',LC_PURCH=" + LC + "";
                    }
                    else if (DOC_TYPE == "GTV")
                    {
                        DT = ",LAST_TRI_NO='" + DOC_NO + "',LAST_TRI_DATE='" + DATE + "',LC_TR=" + LC + "";
                    }
                    else if (DOC_TYPE == "GRVI")
                    {
                        DT = ",LAST_DBN_NO='" + DOC_NO + "',LAST_DBN_DATE='" + DATE + "',LAST_DBN_LC=" + LC + "";
                    }



                    if (clsGen.updateTable("invt_itemtran_dtls", "LC=" + LC + DT, "loc_code='" + rs["loc_code"].ToString() + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial, out roweffect, con, tr) && roweffect <= 0)
                    {
                        OracleCommand cmd1 = null;
                        int Result = 0;
                        cmd1 = con.CreateCommand();
                        if (tr != null) cmd1.Transaction = tr;
                        if (PackingOrder != 0)
                        {
                            cmd1.CommandText = "insert into INVT_itemtran_dtls(PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,zero_serial,packing) select PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,'" + rs["loc_code"].ToString() + "' as LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,0 as zero_serial,'" + Packing + "' as packing from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                        }
                        else
                        {
                            cmd1.CommandText = "insert into INVT_itemtran_dtls(PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,zero_serial,packing) select PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,'" + rs["loc_code"].ToString() + "' as LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD," + ZeroSerial + " as zero_serial,'" + Packing + "' as packing from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                        }
                        Result = cmd1.ExecuteNonQuery();
                        cmd1.Dispose();
                        if (Result > 0)
                        {
                            clsGen.updateTable("invt_itemtran_dtls", "LC=" + LC + DT, "loc_code='" + rs["loc_code"].ToString() + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial, out roweffect, con, tr);
                        }
                    }

                    roweffect = 0;
                    if (clsGen.updateTable("invt_inventorybalance", "LC=" + LC, "loc_code='" + rs["loc_code"].ToString() + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial, out roweffect, con, tr) && roweffect <= 0)
                    {
                        OracleCommand cmd1 = null;
                        int Result = 0;
                        cmd1 = con.CreateCommand();
                        if (tr != null) cmd1.Transaction = tr;
                        if (PackingOrder != 0)
                        {
                            //cmd1.CommandText = "insert into INVT_itemtran_dtls(PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,zero_serial,packing) select PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,'" + rs["loc_code"].ToString() + "' as LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,0 as zero_serial,'" + Packing + "' as packing from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                            cmd1.CommandText = "insert into invt_inventorybalance(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty) select distinct '" + rs["loc_code"].ToString() + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty from INVT_ITEMPACKING where part_number_sup='" + Part_Number_Sup + "' and packing_order=1";

                        }
                        else
                        {
                            cmd1.CommandText = "insert into invt_inventorybalance(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty) select distinct '" + rs["loc_code"].ToString() + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty from INVT_ITEMPACKING where part_number_sup='" + Part_Number_Sup + "' and packing='" + Packing + "'";
                        }
                        Result = cmd1.ExecuteNonQuery();
                        cmd1.Dispose();
                        if (Result > 0)
                        {
                            clsGen.updateTable("invt_inventorybalance", "LC=" + LC, "loc_code='" + rs["loc_code"].ToString() + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial, out roweffect, con, tr);
                        }
                    }
                    if (clsGen.updateTable("invt_itemtransaction", "LC=" + LC, " doc_date>='" + DateTime.Today.ToString("dd/MMM/yyyy") + "' and loc_code='" + rs["loc_code"].ToString() + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial, out roweffect, con, tr) && roweffect <= 0)
                    { }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                rs.Close();
                cmd.Dispose();
            }
            return true;
        }
        private decimal getLC(string Loc_Code, string Part_Number_sup, int Zero_Serial, string Packing)
        {
            string Lc = clsGen.GetValue("select round(LC,3) as LC from invt_itemtran_dtls where loc_code='" + Loc_Code + "' and part_number_sup='" + Part_Number_sup + "' and zero_serial=" + Zero_Serial, con, tr);
            if (!clsGen.IsNumeric1(Lc) || Lc == "")
            {
                Lc = clsGen.GetValue("select round(buying_rate,3) as LC from invt_itempacking where part_number_sup='" + Part_Number_sup + "' and (zero_serial>0 or packing_order=1) and zero_serial=" + Zero_Serial, con, tr);
            }
            if (clsGen.IsNumeric1(Lc)) return decimal.Parse(Lc);
            return 0;
        }


        private bool UpdateStockBalance(string Loc_code, string Doc_type, string part_number_Sup, decimal Qty, decimal Val, string To_Loc_Code, int ZeroSerial, int PackingOrder, string Packing)
        {
            int Result = -1;
            OracleCommand cmd = null;
            try
            {

                string Sql = "Update invt_inventorybalance ", Sql1 = "";
                switch (Doc_type.ToUpper())
                {
                    case "SAL":
                        Sql = Sql + " set YR_SALE_QTY=YR_SALE_QTY + " + Qty + ",YR_SALE_VAL=YR_SALE_VAL + " + Val + ",SALE_QTY=sale_qty+" + Qty + ",sale_val=sale_val+" + Val + ",cb_qty=cb_qty-(" + Qty + "),cb_val=cb_val-(" + Val + ")";
                        break;
                    case "SRT":
                        Sql = Sql + " set YR_SALE_R_QTY=YR_SALE_R_QTY + " + Qty + ",YR_SALE_R_VAL=YR_SALE_R_VAL + " + Val + ", SALE_R_QTY=sale_R_qty+" + Qty + ",sale_R_val=sale_R_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val;
                        break;
                    case "PRH":
                        Sql = Sql + " set YR_PRH_QTY=YR_PRH_QTY + " + Qty + ",YR_PRH_VAL=YR_PRH_VAL + " + Val + ", PRH_QTY=PRH_qty+" + Qty + ",PRH_val=PRH_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val + (LC_ForTranDtl <= 0 ? "" : ",LC=" + LC_ForTranDtl);
                        break;
                    case "PRR":
                        Sql = Sql + " set YR_PRH_R_QTY=YR_PRH_R_QTY + " + Qty + ",YR_PRH_R_VAL=YR_PRH_R_VAL + " + Val + ", PRH_R_QTY=PRH_R_qty+" + Qty + ",PRH_R_val=PRH_R_val+" + Val + ",cb_qty=cb_qty-(" + (Qty) + "),cb_val=cb_val-(" + (Val) + ")";
                        break;
                    case "OB":
                        Sql = Sql + " set OB_QTY=OB_qty+" + Qty + ",ob_val=ob_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val;
                        break;
                    case "GTV":
                    case "ADJ":
                        // Changed on JAN 2011 DD and Abu 
                        // Sql1 = Sql + " set  YR_TRI_QTY=YR_TRI_QTY+" + Qty + ",TRI_QTY=TRI_QTY+" + Qty + ",TRI_val=TRI_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val + " where loc_code='" + To_Loc_Code + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial=" + ZeroSerial;
                        //  Sql = Sql + " set YR_TRR_QTY=YR_TRR_QTY+" + Qty + ",TRR_QTY=TRR_QTY+" + Qty + ",TRR_val=TRR_val+" + Val + ",cb_qty=cb_qty-(" + (Qty) + "),cb_val=cb_val-(" + (Val) + ")";
                        string updlc = "";
                        if (ModifyLc)
                            updlc = ",LC=" + LC_ForTranDtl;
                        Sql1 = Sql + " set  YR_TRI_QTY=YR_TRI_QTY+" + Qty + ",YR_TRI_val=YR_TRI_val+" + Val + ",TRI_QTY=TRI_QTY+" + Qty + ",TRI_val=TRI_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val + updlc + " where loc_code='" + To_Loc_Code + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial=" + ZeroSerial;
                        Sql = Sql + " set YR_TRR_QTY=YR_TRR_QTY+" + Qty + ",YR_TRR_val=YR_TRR_val+" + Val + ",TRR_QTY=TRR_QTY+" + Qty + ",TRR_val=TRR_val+" + Val + ",cb_qty=cb_qty-(" + (Qty) + "),cb_val=cb_val-(" + (Val) + ")";

                        break;
                    default:
                        Exception ex = new Exception("Invalid Document Type.");
                        throw (ex);
                }

                Sql = Sql + " where loc_code='" + Loc_code + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial=" + ZeroSerial;
            ReSl:
                cmd = con.CreateCommand();
                cmd.CommandText = Sql;
                if (tr != null) cmd.Transaction = tr;
                Result = cmd.ExecuteNonQuery();
                if (Result == 0)
                {

                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    if (ZeroSerial == 0 && PackingOrder != 0)
                    {
                        cmd.CommandText = "insert into invt_inventorybalance(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC) select distinct '" + Loc_code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + this.LC_ForStBalance + "  from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing_order=1";
                    }
                    else
                    {
                        cmd.CommandText = "insert into invt_inventorybalance(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC) select distinct '" + Loc_code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + this.LC_ForStBalance + "  from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing='" + Packing + "'";
                    }
                    Result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (Result > 0) goto ReSl;
                    Exception ex = new Exception("Cannot Update New Item Balance Stock.");
                    throw (ex);
                }
                if (Result > 0 && !(Sql1.Equals("")))
                {
                ReTr:
                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    cmd.CommandText = Sql1;
                    Result = cmd.ExecuteNonQuery();
                    if (Result == 0)
                    {
                        cmd.Dispose();
                        cmd = con.CreateCommand();
                        if (tr != null) cmd.Transaction = tr;
                        if (ZeroSerial == 0 && PackingOrder != 0)
                        {
                            cmd.CommandText = "insert into invt_inventorybalance(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC) select distinct '" + To_Loc_Code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + LC_ForStBalanceToLoc + " from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing_order=1";
                        }
                        else
                        {
                            cmd.CommandText = "insert into invt_inventorybalance(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC) select distinct '" + To_Loc_Code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + LC_ForStBalanceToLoc + "  from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing='" + Packing + "'";
                        }
                        Result = cmd.ExecuteNonQuery();
                        if (Result > 0) goto ReTr;
                        Exception ex = new Exception("Cannot Update New Item Balance Stock.");
                        throw (ex);
                    }
                };
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
            if (Result <= 0)
            {
                Exception ex = new Exception("Cannot Update Balance Stock.");
                throw (ex);
            }
            else
            {
                return true;
            }
        }

        private bool UpdateStockBalance(string Loc_code, string Doc_type, string part_number_Sup, decimal Qty, decimal Val, string To_Loc_Code, string Doc_Date, string SysDate, int packingorder, int ZeroSerial, string Packing)
        {
            int Month = (DateTime.Parse(Doc_Date).Month);
            int Yr = DateTime.Parse(Doc_Date).Year;
            string Year = (Yr).ToString();
            if (!(Year.Equals(DateTime.Today.Year.ToString())))
            {
                //          DataConnector.Message("YOU CANNOT EDIT PAST YEAR' INVENTORY");
                //          return false; 
            }


            // DO THIS LATER
            //if (Yr == DateTime.Parse(SysDate).Year && Month == DateTime.Parse(SysDate).Month)

            return UpdateStockBalance(Loc_code, Doc_type, part_number_Sup, Qty, Val, To_Loc_Code, ZeroSerial, packingorder, Packing);



            int Result = -1;
            //            Ob_Qty = Ob_Qty; //q //* -1;
            //            ob_Val = ob_Val; //* -1;
            decimal Ob_Qty = Qty;
            decimal ob_Val = Val;
            string sqlYtd = "", sqlYtd1 = "";
            OracleCommand cmd = null;
            try
            {
                string Sql = "Update invt_inventorybalance_YR ", Sql1 = "";
                switch (Doc_type.ToUpper())
                {
                    case "SAL":
                        Sql = Sql + " set SALE_QTY=sale_qty+" + Qty + ",sale_val=sale_val+" + Val + ",cb_qty=cb_qty-(" + Qty + "),cb_val=cb_val-(" + Val + ")";
                        sqlYtd = " ,YR_SALE_QTY=YR_SALE_QTY+" + Qty + ",YR_SALE_VAL=YR_SALE_VAL+" + Val;
                        Qty = Qty * -1;
                        Val = Val * -1;
                        break;
                    case "SRT":
                        Sql = Sql + " set SALE_R_QTY=sale_R_qty+" + Qty + ",sale_R_val=sale_R_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val;
                        sqlYtd = " ,YR_SALE_R_QTY=YR_SALE_R_QTY+" + Qty + ",YR_SALE_R_VAL=YR_SALE_R_VAL+" + Val;
                        break;
                    case "PRH":
                        Sql = Sql + " set PRH_QTY=PRH_qty+" + Qty + ",PRH_val=PRH_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val;
                        sqlYtd = " ,YR_PRH_QTY=YR_PRH_QTY+" + Qty + ",YR_PRH_VAL=YR_PRH_VAL+" + Val;
                        break;
                    case "PRR":
                        Sql = Sql + " set PRH_R_QTY=PRH_R_qty+" + Qty + ",PRH_R_val=PRH_R_val+" + Val + ",cb_qty=cb_qty-(" + Qty + "),cb_val=cb_val-(" + Val + ")";
                        Qty = Qty * -1;
                        Val = Val * -1;
                        sqlYtd = " ,YR_PRH_R_QTY=YR_PRH_R_QTY+" + Qty + ", YR_PRH_R_VAL=YR_PRH_R_VAL+" + Val;
                        break;
                    case "OB":
                        Sql = Sql + " set OB_QTY=OB_qty+" + Qty + ",ob_val=ob_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val;
                        sqlYtd = " ,YR_OB_QTY=YR_OB_QTY+" + Qty + ",YR_OB_VAL=YR_OB_VAL+" + Val;
                        break;
                    case "GTV":
                    case "ADJ":
                        Sql1 = Sql + " set TRI_QTY=TRI_QTY+" + Qty + ",TRI_val=TRI_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val + " where loc_code='" + To_Loc_Code + "' and inv_year='" + Year + "' and inv_month='" + Month + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial=" + ZeroSerial;
                        Sql = Sql + " set TRR_QTY=TRR_QTY+" + Qty + ",TRR_val=TRR_val+" + Val + ",cb_qty=cb_qty-(" + Qty + "),cb_val=cb_val-(" + Val + ")";
                        sqlYtd = " ,YR_TRI_QTY=YR_TRI_QTY+" + Qty + ",YR_TRI_VAL=YR_TRI_VAL+" + Val;
                        sqlYtd1 = " ,YR_TRr_QTY=YR_TRr_QTY+" + Qty + ",YR_TRr_VAL=YR_TRr_VAL+" + Val;
                        break;
                    default:
                        Exception ex = new Exception("Invalid Document Type.");
                        throw (ex);
                }

                Sql = Sql + " where loc_code='" + Loc_code + "' and inv_year='" + Year + "' and inv_month='" + Month + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial =" + ZeroSerial;
            ReSl:
                cmd = con.CreateCommand();
                cmd.CommandText = Sql;
                if (tr != null) cmd.Transaction = tr;
                Result = cmd.ExecuteNonQuery();
                if (Result == 0)
                {
                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    if (ZeroSerial == 0 && packingorder != 0)
                    {
                        cmd.CommandText = "insert into invt_inventorybalance_yr(LOC_CODE,inv_year,inv_month,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty) select distinct '" + Loc_code + "' as loc_code,'" + Year + "' as inv_year," + Month + " as inv_month,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing_order=1";
                    }
                    else
                    {
                        cmd.CommandText = "insert into invt_inventorybalance_yr(LOC_CODE,inv_year,inv_month,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty) select distinct '" + Loc_code + "' as loc_code,'" + Year + "' as inv_year," + Month + " as inv_month,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing='" + Packing + "'";
                    }
                    Result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (Result > 0) goto ReSl;
                    Exception ex = new Exception("Cannot Update New Item Balance Stock.");
                    throw (ex);
                }
                if (Result > 0 && !(Sql1.Equals("")))
                {
                ReTr:
                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    cmd.CommandText = Sql1;
                    Result = cmd.ExecuteNonQuery();
                    if (Result == 0)
                    {
                        cmd.Dispose();
                        cmd = con.CreateCommand();
                        if (tr != null) cmd.Transaction = tr;
                        if (ZeroSerial != 0)
                        {
                            cmd.CommandText = "insert into invt_inventorybalance_yr(LOC_CODE,inv_year,inv_month,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty) select distinct '" + To_Loc_Code + "' as loc_code,'" + Year + "' as inv_year," + Month + " as inv_month,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing='" + Packing + "'";
                        }
                        else cmd.CommandText = "insert into invt_inventorybalance_yr(LOC_CODE,inv_year,inv_month,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty) select distinct '" + To_Loc_Code + "' as loc_code,'" + Year + "' as inv_year," + Month + " as inv_month,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing_order=1";
                        Result = cmd.ExecuteNonQuery();
                        if (Result > 0) goto ReTr;
                        Exception ex = new Exception("Cannot Update New Item Balance Stock.");
                        throw (ex);
                    }
                };
                cmd.Dispose();

                if (Doc_type.Equals("GTV") || Doc_type.Equals("ADJ"))
                {
                    int cMonth = DateTime.Parse(SysDate).Month;
                    if (Yr != DateTime.Parse(SysDate).Year) cMonth = 12;
                    while (Yr++ <= DateTime.Parse(SysDate).Year)
                    {
                        while (++Month <= cMonth)
                        {
                        re21:
                            cmd = con.CreateCommand();
                            if (tr != null) cmd.Transaction = tr;
                            cmd.CommandText = "update invt_inventorybalance_yr set ob_qty=ob_qty+" + Qty + ",ob_val=ob_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val + sqlYtd + " where loc_code='" + To_Loc_Code + "' and inv_year='" + (Yr - 1) + "' and inv_month='" + Month + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial=" + ZeroSerial;
                            Result = cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            if (Result == 0)
                            {
                                if (MakeEntry("invt_inventorybalance_yr", To_Loc_Code, (Yr - 1).ToString(), Month, part_number_Sup, Packing, ZeroSerial))
                                {
                                    goto re21;
                                }
                                {
                                    Exception ex = new Exception("Cannot Update New Item Balance Stock(Month).");
                                    throw (ex);
                                    // Error Case
                                }
                            }
                        re22:
                            cmd = con.CreateCommand();
                            if (tr != null) cmd.Transaction = tr;
                            cmd.CommandText = "update invt_inventorybalance_yr set ob_qty=ob_qty+" + (-1 * Qty) + ",ob_val=ob_val+" + (Val * -1) + ",cb_qty=cb_qty+" + (-1 * Qty) + ",cb_val=cb_val+" + (Val * -1) + sqlYtd1 + " where loc_code='" + Loc_code + "' and inv_year='" + (Yr - 1) + "' and inv_month='" + Month + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial=" + ZeroSerial;
                            Result = cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            if (Result == 0)
                                if (MakeEntry("invt_inventorybalance_yr", Loc_code, (Yr - 1).ToString(), Month, part_number_Sup, Packing, ZeroSerial))
                                {
                                    goto re22;
                                }
                                else
                                {
                                    Exception ex = new Exception("Cannot Update New Item Balance Stock(" + Month + ").");
                                    throw (ex);
                                }
                        }
                        if (Yr < DateTime.Parse(SysDate).Year) cMonth = 12;
                        else cMonth = DateTime.Parse(SysDate).Month;
                        Month = 0;
                    }
                invReX:
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    cmd.CommandText = "update invt_inventorybalance set ob_qty=ob_qty+" + Qty + ",cb_qty=cb_qty+" + Qty + ",ob_val=ob_val+" + Val + ",cb_val=cb_val+" + Val + sqlYtd + " where loc_code='" + To_Loc_Code + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial=" + ZeroSerial;
                    Result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (Result == 0)
                    {
                        cmd = con.CreateCommand();
                        if (tr != null) cmd.Transaction = tr;
                        if (ZeroSerial == 0)
                            cmd.CommandText = "insert into invt_inventorybalance(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty) select distinct '" + Loc_code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing_order=1";
                        else cmd.CommandText = "insert into invt_inventorybalance(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty) select distinct '" + Loc_code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing='" + Packing + "'";
                        Result = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        if (Result == 0)
                        {
                            // ERROR IN ITEM PACKING
                            Exception ex = new Exception("Cannot Update New Item Balance Stock(Month).");
                            throw (ex);
                        }
                        goto invReX;
                    }
                invReX1:
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    cmd.CommandText = "update invt_inventorybalance set ob_qty=ob_qty+" + (-1 * Qty) + ",cb_qty=cb_qty+" + (-1 * Qty) + ",ob_val=ob_val+" + (Val * -1) + ",cb_val=cb_val+" + (Val * -1) + sqlYtd1 + " where loc_code='" + Loc_code + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial=" + ZeroSerial; ;
                    Result = cmd.ExecuteNonQuery();
                    if (Result == 0)
                    {
                        cmd.Dispose();
                        cmd = con.CreateCommand();
                        if (tr != null) cmd.Transaction = tr;
                        if (ZeroSerial != 0) cmd.CommandText = "insert into invt_inventorybalance(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty) select distinct '" + Loc_code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing='" + Packing + "'";
                        else cmd.CommandText = "insert into invt_inventorybalance(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty) select distinct '" + Loc_code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packingorder=1";
                        Result = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        if (Result == 0)
                        {
                            // ERROR IN ITEM PACKING
                            Exception ex = new Exception("Cannot Update New Item Balance Stock(Month).");
                            throw (ex);
                        }
                        goto invReX1;
                    }
                }
                else
                {
                    int cMonth = DateTime.Parse(SysDate).Month;
                    if (Yr != DateTime.Parse(SysDate).Year) cMonth = 12;
                    while (Yr++ <= DateTime.Parse(SysDate).Year)
                    {
                        while (++Month <= cMonth)
                        {
                        res2:
                            cmd.Dispose();
                            cmd = con.CreateCommand();
                            if (tr != null) cmd.Transaction = tr;
                            cmd.CommandText = "update invt_inventorybalance_yr set ob_qty=ob_qty+" + Qty + ",cb_qty=cb_qty+" + Qty + ",ob_val=ob_val+" + Val + ",cb_val=cb_val+" + Val + sqlYtd + " where loc_code='" + Loc_code + "' and inv_year='" + (Yr - 1) + "' and inv_month='" + Month + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial=" + ZeroSerial; ;
                            Result = cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            if (Result == 0)
                            {
                                if (MakeEntry("invt_inventorybalance_yr", Loc_code, (Yr - 1).ToString(), Month, part_number_Sup, Packing, ZeroSerial))
                                {
                                    goto res2;
                                }
                                {
                                    // Error Case
                                }
                            }
                        }
                        if (Yr < DateTime.Parse(SysDate).Year) cMonth = 12;
                        else cMonth = DateTime.Parse(SysDate).Month;
                        Month = 0;
                    }
                invRe:
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    cmd.CommandText = "update invt_inventorybalance set ob_qty=ob_qty+" + Qty + ",cb_qty=cb_qty+" + Qty + ",ob_val=ob_val+" + Val + ",cb_val=cb_val+" + Val + sqlYtd + " where loc_code='" + Loc_code + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial=" + ZeroSerial;
                    Result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (Result == 0)
                    {
                        cmd = con.CreateCommand();
                        if (tr != null) cmd.Transaction = tr;
                        if (ZeroSerial == 0)
                            cmd.CommandText = "insert into invt_inventorybalance(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty) select distinct '" + Loc_code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing_order=1";
                        else
                            cmd.CommandText = "insert into invt_inventorybalance(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty) select distinct '" + Loc_code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing='" + Packing + "'";
                        Result = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        if (Result == 0)
                        {
                            // ERROR IN ITEM PACKING
                            Exception ex = new Exception("Cannot Update New Item Balance Stock(Month).");
                            throw (ex);
                        }
                        goto invRe;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
            if (Result <= 0)
            {
                Exception ex = new Exception("Cannot Update Balance Stock(Month).");
                throw (ex);
            }
            else
            {
                return true;
            }
        }

        private bool MakeEntry(string TableName, string Loc_Code, string Year, int Month, string Part_number_sup, string Packing, int ZeroSerial)
        {
            OracleCommand cmd = null;
            try
            {
                TableName = TableName.ToLower();
                cmd = con.CreateCommand();
                if (tr != null) cmd.Transaction = tr;
                switch (TableName)
                {
                    case "invt_inventorybalance_yr":
                        if (ZeroSerial == 0)
                        {
                            cmd.CommandText = "insert into invt_inventorybalance_yr(LOC_CODE,inv_year,inv_month,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,pack_multiply_qty,packing) select distinct '" + Loc_Code + "' as loc_code,'" + Year + "' as inv_year," + Month + " as inv_month,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,pack_multiply_qty,packing from INVT_ITEMPACKING where part_number_sup='" + Part_number_sup + "' and packing_order=1";
                        }
                        else
                        {
                            cmd.CommandText = "insert into invt_inventorybalance_yr(LOC_CODE,inv_year,inv_month,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,pack_multiply_qty,packing) select distinct '" + Loc_Code + "' as loc_code,'" + Year + "' as inv_year," + Month + " as inv_month,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,pack_multiply_qty,packing from INVT_ITEMPACKING where part_number_sup='" + Part_number_sup + "' and packing='" + Packing + "'";
                        }
                        break;
                }
                if (cmd.ExecuteNonQuery() > 0) return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                cmd.Dispose();
            }
            return false;
        }


        private bool LOG_STOCK_ERROR(string DOC_NO, string DOC_TYPE, string Doc_Date, string PART_NUMBER_SUP, string LOC_CODE, string TABLE_NAME, string DOC_YEAR, string REMARKS, string PACKING, decimal Qty, decimal Pack_Multiply_Qty, string Error_Desc)
        {
            OracleCommand cmd = null;
            try
            {
                cmd = con.CreateCommand();
                sql = "insert into sys_stock_errors(doc_date,doc_no,add_date,part_number_sup,packsize,doc_type,error_desc,doc_year,loc_code,pack_multiply_qty,table_name,qty) values('" + Doc_Date + "','" + DOC_NO + "','" + DateTime.Today + "','" + PART_NUMBER_SUP + "','" + PACKING + "','" + DOC_TYPE + "','" + Error_Desc + "','" + DOC_YEAR + "','" + LOC_CODE + "'," + Pack_Multiply_Qty + ",'" + TABLE_NAME + "'," + Qty + ")";
                if (tr != null) cmd.Transaction = tr;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception eq)
            {
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        public string Get_ItemType(string PART_NUMBER_SUP)
        {
            OracleCommand cmd = null;
            try
            {
                cmd = con.CreateCommand();
                if (tr != null) cmd.Transaction = tr;
                sql = "select item_type from invt_inventorymaster where part_number_sup='" + PART_NUMBER_SUP + "'";
                // if (tr != null) cmd.Transaction = tr;
                cmd.CommandText = sql;
                OracleDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    return rs[0].ToString();
                }
                return "G";
            }
            catch (Exception eq)
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
            }
        }
        private string Get_ItemType(string PART_NUMBER_SUP, out string Link_Code, string Costing_Method)
        {
            Link_Code = "";
            OracleCommand cmd = con.CreateCommand();
            try
            {
                if (tr != null) cmd.Transaction = tr;
                sql = "select item_type,linked_item,costing_method from invt_inventorymaster where part_number_sup='" + PART_NUMBER_SUP + "'";
                //if (tr != null) cmd.Transaction = tr;
                cmd.CommandText = sql;
                OracleDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    if (!rs.IsDBNull(1))
                    {
                        Link_Code = rs["Linked_ITEM"].ToString();
                        Costing_Method = rs["costing_method"].ToString();
                        return "L";
                    }
                    return rs[0].ToString();
                }
                rs.Close();
                return "G";
            }
            catch (Exception eq)
            {
                throw;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        #region BATCH STOCK UPDATE
        public bool UpdateStockBatch(string Loc_Code, string Part_Number_Sup, string Packing, string Item_Type, string Doc_No, string Doc_Type, string GRV_TYPE, decimal QTY_LUQ, decimal Val, decimal FOC_LUQ, decimal PackQty, decimal Pack_Multiply_Qty, string Doc_Year, string Doc_Date, string To_Loc_Code, string batchCode, string expiryDate)
        {
            if (Doc_Type == "SI") Doc_Type = "SAL";
            ModifyLc = false;
            decimal temp_qty = 0;
            if (_count == 0)
            {
                temp_qty = QTY_LUQ;
            }
            else
            {

            }
            DateTime expDate;
            if (!expiryDate.Equals("")) { expDate = DateTime.Parse(expiryDate); }
            else expDate = DateTime.Parse(Doc_Date);

            /*
             * 
             * KEEP THE ORDER OF FUNCTION CALLS as,
             *   1) Dtls 2) Tran 3) Balance
             * 
             */



            Loc_Code = Loc_Code.Trim();
            To_Loc_Code = To_Loc_Code.Trim();
            Part_Number_Sup = Part_Number_Sup.Trim();
            Doc_Year = Doc_Year.Trim();
            Doc_No = Doc_No.Trim();
            Packing = Packing.Trim();

            try
            {
                if (QTY_LUQ == 0) return true;

                bool Ret = false;

                if (con.State != ConnectionState.Open)
                {
                    throw (new Exception("Connection Not Available"));//return Ret;
                }
                TranDate = Doc_Date;
                if (Doc_Date.Length > 12)
                {
                    DateTime dt1 = DateTime.Parse(Doc_Date);
                    int h = dt1.Hour;
                    if (dt1.Hour >= 0 && dt1.Hour <= 6)
                    {
                        TranDate = (dt1.AddDays(-1)).ToString("dd/MMM/yyyy");
                        TranDate = (dt1).ToString("dd/MMM/yyyy");
                    }
                }

                Doc_Date = DateTime.Parse(Doc_Date).ToString("dd/MMM/yyyy");
                Doc_Date = Doc_Date.ToUpper();

                string Linked_Item = "", Costing_Method = "";
                Item_Type = "G";
                int PackingOrder = 0;
                int ZeroSerial = 0;

                PackingOrder = GetPackingOrder(Part_Number_Sup, Packing, out ZeroSerial, out Costing_Method, out Bar_Code, out Linked_Item, out Item_Type, out Price_Code, out Buying_rate);

                if (Linked_Item != "") Item_Type = "L";
                OracleCommand cmd = null;
                OracleCommand cmd2 = null;
                OracleDataReader rs = null;
                OracleDataReader rs2 = null;
                string SysDate = Doc_Date;
                SysDate = clsGen.SysDate(con, tr, false); // comment by abi
                if (DateTime.Parse(Doc_Date) > DateTime.Parse(SysDate))
                {
                    Exception ex = new Exception("Invalid Transaction Date.");
                    throw (ex);
                }
                if (DateTime.Parse(SysDate).Year == DateTime.Parse(Doc_Date).Year && DateTime.Parse(Doc_Date).Month != DateTime.Parse(SysDate).Month)
                {

                }
                Doc_Type = Doc_Type.ToUpper();
                Packing = Packing.ToUpper();
                Part_Number_Sup = Part_Number_Sup.ToUpper();
                Part_Number_Sup = Part_Number_Sup.ToUpper();
                Item_Type = Item_Type.ToUpper();
                Loc_Code = Loc_Code.ToUpper();

                decimal LC = 0;
                if (Doc_Type == "PRH" || Doc_Type == "GTV")
                {
                    LC = Val / QTY_LUQ;
                    if (ZeroSerial != 0)
                        LC = LC * Pack_Multiply_Qty;
                    LC_ForTranDtl = LC;
                    if (LC == 0)
                    {
                        LC = Buying_rate;
                        LC_ForTranDtl = Buying_rate;
                    }
                }

                switch (Item_Type.ToUpper())
                {
                    case "G":
                        //UpdateTranDtls(Part_Number_Sup, Doc_Type, Loc_Code, Doc_Date, Val, QTY_LUQ, Packing, Doc_No, To_Loc_Code, FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);
                        if (Doc_Type.ToUpper().Equals("PRH") && QTY_LUQ != FOC_LUQ)
                        {
                            UpdateLocLcsBatch(Loc_Code, LC, Part_Number_Sup, ZeroSerial, PackingOrder, Packing, Doc_Date, Doc_Type, Doc_No, Loc_Code, batchCode);
                        }
                        if (Doc_Type.ToUpper().Equals("GTV") && QTY_LUQ != FOC_LUQ && ModifyLc)
                        {
                            UpdateLocLcsBatch(To_Loc_Code, LC, Part_Number_Sup, ZeroSerial, PackingOrder, Packing, Doc_Date, Doc_Type, Doc_No, Loc_Code, batchCode);
                        }

                        UpdateItemTranBatch(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, 0, batchCode);
                        UpdateStockBalanceBatch(Loc_Code, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, To_Loc_Code, Doc_Date, SysDate, PackingOrder, ZeroSerial, Packing, batchCode, expDate);
                        break;
                    case "L":
                        if (Doc_Type.Equals("SAL") || Doc_Type.Equals("SRT"))
                        {
                            string Asc = "";
                            if (Costing_Method.Equals("L")) Asc = "DESC";
                            string zz = "";
                            if (ZeroSerial == 0)
                            {

                                zz = " and packing_order=1";
                            }
                            else
                            {
                                zz = " and zero_serial=" + ZeroSerial;
                            }
                            sql = "select distinct B.part_number_sup,cb_qty,c.pack_multiply_qty,a.packing,last_grv_date  from (invt_itemtran_dtls a INNER JOIN INVT_INVENTORYBALANCE_BATCH b on a.loc_code=b.loc_code and a.part_number_sup=b.part_number_sup and a.zero_serial=b.zero_serial) inner join invt_itempacking c on a.part_number_sup=c.part_number_sup  and a.zero_serial=c.zero_serial WHERE a.loc_code='" + Loc_Code + "' and c.linked_ITEM='" + Linked_Item + "' " + zz + " order by last_grv_date " + Asc;
                            OracleDataReader rs1 = null;
                            OracleCommand cmd1 = con.CreateCommand();
                            try
                            {
                                cmd1.CommandText = sql;
                                cmd1.Transaction = tr;
                                rs1 = cmd1.ExecuteReader();
                                bool Zero = true;
                                while (rs1.Read())
                                {
                                    Zero = false;
                                    decimal Balance = 0;
                                    decimal Stock = rs1.GetDecimal(1);
                                    PackingOrder = 1;
                                    Packing = rs1["packing"].ToString();
                                    Pack_Multiply_Qty = decimal.Parse(rs1["pack_multiply_qty"].ToString());
                                    if (Stock <= 0) continue;
                                    Balance = Stock - QTY_LUQ;
                                    if (Balance < 0)
                                    {
                                        UpdateItemTranBatch(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, 0, batchCode);
                                        UpdateStockBalanceBatch(Loc_Code, Doc_Type, rs1["part_number_sup"].ToString(), Stock, (Val / QTY_LUQ) * Stock, "", ZeroSerial, PackingOrder, Packing, batchCode, expDate);
                                        QTY_LUQ = QTY_LUQ - Stock;
                                    }
                                    else
                                    {
                                        UpdateItemTranBatch(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, 0, batchCode);
                                        UpdateStockBalanceBatch(Loc_Code, Doc_Type, rs1["part_number_sup"].ToString(), QTY_LUQ, Val, "", ZeroSerial, PackingOrder, Packing, batchCode, expDate);
                                        QTY_LUQ = 0;
                                        break;
                                    }
                                }
                                if (QTY_LUQ != 0 || (Zero && QTY_LUQ != 0))
                                {
                                    UpdateItemTranBatch(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, 0, batchCode);
                                    UpdateStockBalanceBatch(Loc_Code, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, "", ZeroSerial, PackingOrder, Packing, batchCode, expDate);
                                }
                                rs1.Close();
                                cmd1.Dispose();
                            }
                            catch { throw; }
                            finally { if (rs1 != null) rs1.Dispose(); cmd1.Dispose(); }
                        }
                        else
                        {
                            UpdateItemTranBatch(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, 0, batchCode);
                            UpdateStockBalanceBatch(Loc_Code, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, To_Loc_Code, Doc_Date, SysDate, PackingOrder, ZeroSerial, Packing, batchCode, expDate);
                        }
                        break;

                    case "X":
                    case "B":
                        //OracleCommand  cmd = con.CreateCommand();
                        int SubItemCount = 0;
                        try
                        {
                            if (Item_Type == "X")
                            {
                                //UpdateTranDtls(Part_Number_Sup, Doc_Type, Loc_Code, Doc_Date, Val, QTY_LUQ, Packing, Doc_No, To_Loc_Code, FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);//UpdateTranDtls(Part_Number_Sup, Doc_Type, Loc_Code, Doc_Date, Val, QTY_LUQ, Packing,Doc_No, To_Loc_Code, FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);
                                //decimal LuggageMainItemRetailPrice = 0;
                                //if (ApplayLuggageValue(Part_Number_Sup, out LuggageMainItemRetailPrice))
                                //{
                                //    // ApplayLuggageCost(Part_Number_Sup, LuggageMainItemRetailPrice, Val / QTY_LUQ);
                                //    //Added By DD && Abu On 08/Jan/2011 09:55 AM For Limiting Cost Updation Only On Purchase Time and Transfer Time
                                //    if (Doc_Type == "PRH" || Doc_Type == "PRR" || Doc_Type == "OB" || Doc_Type == "GTV" || Doc_Type == "ADJ")
                                //    {
                                //        ApplayLuggageCost(Part_Number_Sup, LuggageMainItemRetailPrice, Val / QTY_LUQ);
                                //    }
                                //    //Added By DD && Abu On 08/Jan/2011 09:55 AM For Limiting Cost Updation Only On Purchase Time and Transfer Time

                                //}
                            }
                            else
                            {
                                if (Doc_Type == "PRH" || Doc_Type == "PRR") throw (new Exception("Invalid Doc Type For Bundle Item"));
                            }
                            cmd = con.CreateCommand();
                            //if (Doc_Type == "PRH" || Doc_Type == "PRR")                            
                            //Updated By DD && Abu On 08/Jan/2011 09:55 AM For Limiting Value On Case of Sale Time
                            if (Doc_Type == "PRH" || Doc_Type == "PRR" || Doc_Type == "OB" || Doc_Type == "GTV" || Doc_Type == "ADJ")
                            {
                                cmd.CommandText = "select Pack_Multiply_Qty,asy_qty,item_bundle_Cost,packing,part_number_sup from invt_assemblysub where ASY_part_number_sup='" + Part_Number_Sup + "'";
                            }
                            else
                            {
                                cmd.CommandText = "select Pack_Multiply_Qty,asy_qty,item_bundle_value,packing,part_number_sup from invt_assemblysub where ASY_part_number_sup='" + Part_Number_Sup + "'";
                            }
                            if (tr != null) cmd.Transaction = tr;
                            rs = cmd.ExecuteReader();
                            while (rs.Read())
                            {
                                SubItemCount++;

                                string zSerial = clsGen.GetValue("INVT_ITEMPACKING", "ZERO_SERIAL", "PART_NUMBER_SUP = '" + rs[4].ToString() + "' AND PACKING = '" + rs[3].ToString() + "' AND DEFAULT_PACKING = 'Y' AND INACTIVE_ITEM = 'N'", "PART_NUMBER_SUP", con, tr);

                                List<ModelBatch> getList = GetBatchStockList(rs[4].ToString(), int.Parse(zSerial), Loc_Code, con, tr);
                                string defauktBatch = "210517100000AM";

                                //decimal STK = clsGen.getStock(Loc_Code, rs[4].ToString(), int.Parse(zSerial), con, tr);
                                decimal STKBATCH = getStockBatch(Loc_Code, rs[4].ToString(), int.Parse(zSerial), "", con, tr);
                                //QTY_LUQ = temp_qty;
                                decimal totalQty = QTY_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1);

                                decimal qty = 0;
                                decimal totalQtyBatch = 0;
                                decimal updatedQty = 0;

                                if (Doc_Type != "PRH")
                                {
                                    if (Defaults.Def_LOC == Loc_Code)
                                    {
                                        if (STKBATCH < totalQty)
                                        {
                                            DataConnector.Message("Not enogh Batch Qty Please Check Bundled Items " + Part_Number_Sup + "", "E", "");
                                            throw new Exception("Not Enough Qty in " + rs[4].ToString() + " Bundling/Language " + Part_Number_Sup + "");
                                        }

                                        foreach (ModelBatch batch in getList)
                                        {
                                            qty = decimal.Parse(batch.batchQty);
                                            totalQtyBatch = qty;
                                            batchCode = batch.batchCode;
                                            if (totalQtyBatch < totalQty)
                                            {
                                                if (updatedQty > 0)
                                                {
                                                    QTY_LUQ = totalQty - updatedQty;//totalQty - updatedQty;
                                                    FOC_LUQ = totalQty - updatedQty;//totalQty - updatedQty;
                                                    if (QTY_LUQ > qty)
                                                    {
                                                        QTY_LUQ = qty;
                                                        FOC_LUQ = qty;
                                                    }
                                                    else
                                                    {
                                                        UpdateStockBatch(Loc_Code, Part_Number_Sup, Packing, "", Doc_No, Doc_Type,
                                                            "", (QTY_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)),
                                                            (QTY_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)),
                                                            (FOC_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)), rs.GetDecimal(1), rs.GetDecimal(0),
                                                            Doc_Year, Doc_Date, To_Loc_Code, batchCode, expiryDate);
                                                        updatedQty = updatedQty + QTY_LUQ;
                                                        break;
                                                    }
                                                }
                                                else
                                                {
                                                    QTY_LUQ = qty;
                                                    FOC_LUQ = qty;
                                                }

                                                Part_Number_Sup = rs[4].ToString();
                                                Packing = rs[3].ToString();
                                                Pack_Multiply_Qty = decimal.Parse(rs[0].ToString());
                                                UpdateStockBatch(Loc_Code, Part_Number_Sup, Packing, "", Doc_No, Doc_Type,
                                                    "", (QTY_LUQ * rs.GetDecimal(0)), (QTY_LUQ * rs.GetDecimal(0)),
                                                    (FOC_LUQ),
                                                    rs.GetDecimal(1), rs.GetDecimal(0), Doc_Year, Doc_Date,
                                                    To_Loc_Code, batchCode, expiryDate);

                                                updatedQty = updatedQty + qty; //ORIGINAL                                           
                                            }
                                            else if (totalQtyBatch >= totalQty)
                                            {
                                                qty = totalQty - updatedQty;
                                                Part_Number_Sup = rs[4].ToString();
                                                Packing = rs[3].ToString();
                                                Pack_Multiply_Qty = decimal.Parse(rs[0].ToString());

                                                if (updatedQty != 0)
                                                {
                                                    QTY_LUQ = qty;
                                                    FOC_LUQ = qty;
                                                    Pack_Multiply_Qty = decimal.Parse(rs[0].ToString());
                                                    UpdateStockBatch(Loc_Code, Part_Number_Sup, Packing, "", Doc_No, Doc_Type,
                                                        "", (QTY_LUQ * rs.GetDecimal(0)), (QTY_LUQ * rs.GetDecimal(0)),
                                                        (FOC_LUQ),
                                                        rs.GetDecimal(1), rs.GetDecimal(0), Doc_Year, Doc_Date,
                                                        To_Loc_Code, batchCode, expiryDate);
                                                }
                                                else
                                                {
                                                    if (QTY_LUQ > decimal.Parse(batch.batchQty))
                                                    {
                                                        QTY_LUQ = decimal.Parse(batch.batchQty);
                                                    }
                                                    else
                                                    {
                                                        QTY_LUQ = temp_qty;
                                                    }
                                                    UpdateStockBatch(Loc_Code, Part_Number_Sup, Packing, "", Doc_No, Doc_Type,
                                                        "", (QTY_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)),
                                                        (QTY_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)),
                                                        (FOC_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)), rs.GetDecimal(1), rs.GetDecimal(0),
                                                        Doc_Year, Doc_Date, To_Loc_Code, batchCode, expiryDate);
                                                    updatedQty = updatedQty + QTY_LUQ;
                                                }

                                                break;
                                            }
                                        }
                                    }
                                    else//ADD FOR BUNDLINGITEM TRANSFERIN
                                    {
                                        UpdateStockBatch(Loc_Code, rs["part_number_sup"].ToString(), rs["packing"].ToString(), "", Doc_No, Doc_Type,
                                            "", (QTY_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)), (QTY_LUQ * rs.GetDecimal(2)),
                                            (FOC_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)),
                                            rs.GetDecimal(1), rs.GetDecimal(0), Doc_Year, Doc_Date,
                                            To_Loc_Code, batchCode, expiryDate);
                                    }
                                }
                                else
                                {
                                    UpdateItemTranBatch(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ,
                                           Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, 0, batchCode);

                                    UpdateStockBatch(Loc_Code, rs["part_number_sup"].ToString(), rs["packing"].ToString(), "", Doc_No, Doc_Type,
                                        "", (QTY_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)), (QTY_LUQ * rs.GetDecimal(2)),
                                        (FOC_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)),
                                        rs.GetDecimal(1), rs.GetDecimal(0), Doc_Year, Doc_Date,
                                        To_Loc_Code, batchCode, expiryDate);
                                }

                            }
                            _count = 1;

                            if (SubItemCount <= 0)
                            {
                                throw (new Exception("Bundle Sub Items Not Found"));
                            }

                        }
                        catch { throw; }
                        finally { if (rs != null) rs.Close(); cmd.Dispose(); }
                        break;
                    default:
                        Exception Ex = new Exception("STOCK UPDATION ERROR - INVALID ITEM TYPE");
                        throw (Ex);
                }



            }
            catch (Exception Ee)
            {
                throw;
            }
            // added on apr 28 10
            //    if (!AddToTranStock(Loc_Code, Part_Number_Sup, Packing, Item_Type, Doc_No, Doc_Type, GRV_TYPE, QTY_LUQ, Val, FOC_LUQ, PackQty, Pack_Multiply_Qty, Doc_Year, Doc_Date, To_Loc_Code, con, tr,Defaults.Def_Base_LOC))
            //    {
            //        throw (new Exception("NOT ADDED TO TRANSFER"));
            //    }
            //
            //if (boolAddToTranStock)
            //{
            //    if (!AddToTranStock(Loc_Code, Part_Number_Sup, Packing, Item_Type, Doc_No, Doc_Type, GRV_TYPE, QTY_LUQ, Val, FOC_LUQ, PackQty, Pack_Multiply_Qty, Doc_Year, Doc_Date, To_Loc_Code, con, tr, (Defaults.Def_Base_Individual_Loc ? Defaults.Def_Base_LOC : Defaults.Def_Base_Management_LOC), Unit_Rate, false))
            //    {
            //        throw (new Exception("NOT ADDED TO TRANSFER"));
            //    }
            //}


            ModifyLc = false;
            //Skip_LC_Update = false;
            LC_ForTranDtl = 0;
            LC_ForStBalance = 0;
            LC_ForStBalanceToLoc = 0;

            return true;

        }
        public bool UpdateStockBatch(string Loc_Code, string Part_Number_Sup, string Packing, string Item_Type, string Doc_No, string Doc_Type, string GRV_TYPE, decimal QTY_LUQ, decimal Val, decimal FOC_LUQ, decimal PackQty, decimal Pack_Multiply_Qty, decimal unitRate, string Doc_Year, string Doc_Date, string To_Loc_Code, string batchCode, string expiryDate)
        {
            if (Doc_Type == "SI") Doc_Type = "SAL";
            ModifyLc = false;

            DateTime expDate;
            if (!expiryDate.Equals("")) { expDate = DateTime.Parse(expiryDate); }
            else expDate = DateTime.Parse(Doc_Date);

            /*
             * 
             * KEEP THE ORDER OF FUNCTION CALLS as,
             *   1) Dtls 2) Tran 3) Balance
             * 
             */



            Loc_Code = Loc_Code.Trim();
            To_Loc_Code = To_Loc_Code.Trim();
            Part_Number_Sup = Part_Number_Sup.Trim();
            Doc_Year = Doc_Year.Trim();
            Doc_No = Doc_No.Trim();
            Packing = Packing.Trim();

            try
            {
                if (QTY_LUQ == 0) return true;

                bool Ret = false;

                if (con.State != ConnectionState.Open)
                {
                    throw (new Exception("Connection Not Available"));//return Ret;
                }
                TranDate = Doc_Date;
                if (Doc_Date.Length > 12)
                {
                    DateTime dt1 = DateTime.Parse(Doc_Date);
                    int h = dt1.Hour;
                    if (dt1.Hour >= 0 && dt1.Hour <= 6)
                    {
                        TranDate = (dt1.AddDays(-1)).ToString("dd/MMM/yyyy");
                        TranDate = (dt1).ToString("dd/MMM/yyyy");
                    }
                }

                Doc_Date = DateTime.Parse(Doc_Date).ToString("dd/MMM/yyyy");
                Doc_Date = Doc_Date.ToUpper();

                string Linked_Item = "", Costing_Method = "";
                Item_Type = "G";
                int PackingOrder = 0;
                int ZeroSerial = 0;

                PackingOrder = GetPackingOrder(Part_Number_Sup, Packing, out ZeroSerial, out Costing_Method, out Bar_Code, out Linked_Item, out Item_Type, out Price_Code, out Buying_rate);

                if (Linked_Item != "") Item_Type = "L";
                OracleCommand cmd = null;
                OracleDataReader rs = null;
                string SysDate = Doc_Date;
                SysDate = clsGen.SysDate(con, tr, false); // comment by abi
                if (DateTime.Parse(Doc_Date) > DateTime.Parse(SysDate))
                {
                    Exception ex = new Exception("Invalid Transaction Date.");
                    throw (ex);
                }
                if (DateTime.Parse(SysDate).Year == DateTime.Parse(Doc_Date).Year && DateTime.Parse(Doc_Date).Month != DateTime.Parse(SysDate).Month)
                {

                }
                Doc_Type = Doc_Type.ToUpper();
                Packing = Packing.ToUpper();
                Part_Number_Sup = Part_Number_Sup.ToUpper();
                Part_Number_Sup = Part_Number_Sup.ToUpper();
                Item_Type = Item_Type.ToUpper();
                Loc_Code = Loc_Code.ToUpper();

                decimal LC = 0;
                if (Doc_Type == "PRH" || Doc_Type == "GTV")
                {
                    LC = Val / QTY_LUQ;
                    if (ZeroSerial != 0)
                        LC = LC * Pack_Multiply_Qty;
                    LC_ForTranDtl = LC;
                    if (LC == 0)
                    {
                        LC = Buying_rate;
                        LC_ForTranDtl = Buying_rate;
                    }
                }

                switch (Item_Type.ToUpper())
                {
                    case "G":
                        //UpdateTranDtls(Part_Number_Sup, Doc_Type, Loc_Code, Doc_Date, Val, QTY_LUQ, Packing, Doc_No, To_Loc_Code, FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);
                        if (Doc_Type.ToUpper().Equals("PRH") && QTY_LUQ != FOC_LUQ)
                        {
                            UpdateLocLcsBatch(Loc_Code, LC, Part_Number_Sup, ZeroSerial, PackingOrder, Packing, Doc_Date, Doc_Type, Doc_No, Loc_Code, batchCode);
                        }
                        if (Doc_Type.ToUpper().Equals("GTV") && QTY_LUQ != FOC_LUQ && ModifyLc)
                        {
                            UpdateLocLcsBatch(To_Loc_Code, LC, Part_Number_Sup, ZeroSerial, PackingOrder, Packing, Doc_Date, Doc_Type, Doc_No, Loc_Code, batchCode);
                        }

                        UpdateItemTranBatch(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, unitRate, batchCode);
                        UpdateStockBalanceBatch(Loc_Code, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, To_Loc_Code, Doc_Date, SysDate, PackingOrder, ZeroSerial, Packing, batchCode, expDate);
                        break;
                    case "L":
                        if (Doc_Type.Equals("SAL") || Doc_Type.Equals("SRT"))
                        {
                            string Asc = "";
                            if (Costing_Method.Equals("L")) Asc = "DESC";
                            string zz = "";
                            if (ZeroSerial == 0)
                            {

                                zz = " and packing_order=1";
                            }
                            else
                            {
                                zz = " and zero_serial=" + ZeroSerial;
                            }
                            sql = "select distinct B.part_number_sup,cb_qty,c.pack_multiply_qty,a.packing,last_grv_date  from (invt_itemtran_dtls a INNER JOIN INVT_INVENTORYBALANCE_BATCH b on a.loc_code=b.loc_code and a.part_number_sup=b.part_number_sup and a.zero_serial=b.zero_serial) inner join invt_itempacking c on a.part_number_sup=c.part_number_sup  and a.zero_serial=c.zero_serial WHERE a.loc_code='" + Loc_Code + "' and c.linked_ITEM='" + Linked_Item + "' " + zz + " order by last_grv_date " + Asc;
                            OracleDataReader rs1 = null;
                            OracleCommand cmd1 = con.CreateCommand();
                            try
                            {
                                cmd1.CommandText = sql;
                                cmd1.Transaction = tr;
                                rs1 = cmd1.ExecuteReader();
                                bool Zero = true;
                                while (rs1.Read())
                                {
                                    Zero = false;
                                    decimal Balance = 0;
                                    decimal Stock = rs1.GetDecimal(1);
                                    PackingOrder = 1;
                                    Packing = rs1["packing"].ToString();
                                    Pack_Multiply_Qty = decimal.Parse(rs1["pack_multiply_qty"].ToString());
                                    if (Stock <= 0) continue;
                                    Balance = Stock - QTY_LUQ;
                                    if (Balance < 0)
                                    {
                                        UpdateItemTranBatch(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, unitRate, batchCode);
                                        UpdateStockBalanceBatch(Loc_Code, Doc_Type, rs1["part_number_sup"].ToString(), Stock, (Val / QTY_LUQ) * Stock, "", ZeroSerial, PackingOrder, Packing, batchCode, expDate);
                                        QTY_LUQ = QTY_LUQ - Stock;
                                    }
                                    else
                                    {
                                        UpdateItemTranBatch(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, unitRate, batchCode);
                                        UpdateStockBalanceBatch(Loc_Code, Doc_Type, rs1["part_number_sup"].ToString(), QTY_LUQ, Val, "", ZeroSerial, PackingOrder, Packing, batchCode, expDate);
                                        QTY_LUQ = 0;
                                        break;
                                    }
                                }
                                if (QTY_LUQ != 0 || (Zero && QTY_LUQ != 0))
                                {
                                    UpdateItemTranBatch(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, unitRate, batchCode);
                                    UpdateStockBalanceBatch(Loc_Code, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, "", ZeroSerial, PackingOrder, Packing, batchCode, expDate);
                                }
                                rs1.Close();
                                cmd1.Dispose();
                            }
                            catch { throw; }
                            finally { if (rs1 != null) rs1.Dispose(); cmd1.Dispose(); }
                        }
                        else
                        {
                            UpdateItemTranBatch(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, unitRate, batchCode);
                            UpdateStockBalanceBatch(Loc_Code, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, To_Loc_Code, Doc_Date, SysDate, PackingOrder, ZeroSerial, Packing, batchCode, expDate);
                        }
                        break;

                    case "X":
                    case "B":
                        //OracleCommand  cmd = con.CreateCommand();
                        int SubItemCount = 0;
                        try
                        {
                            if (Item_Type == "X")
                            {
                                //UpdateTranDtls(Part_Number_Sup, Doc_Type, Loc_Code, Doc_Date, Val, QTY_LUQ, Packing, Doc_No, To_Loc_Code, FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);//UpdateTranDtls(Part_Number_Sup, Doc_Type, Loc_Code, Doc_Date, Val, QTY_LUQ, Packing,Doc_No, To_Loc_Code, FOC_LUQ, PackingOrder, ZeroSerial, Pack_Multiply_Qty);
                                //decimal LuggageMainItemRetailPrice = 0;
                                //if (ApplayLuggageValue(Part_Number_Sup, out LuggageMainItemRetailPrice))
                                //{
                                //    // ApplayLuggageCost(Part_Number_Sup, LuggageMainItemRetailPrice, Val / QTY_LUQ);
                                //    //Added By DD && Abu On 08/Jan/2011 09:55 AM For Limiting Cost Updation Only On Purchase Time and Transfer Time
                                //    if (Doc_Type == "PRH" || Doc_Type == "PRR" || Doc_Type == "OB" || Doc_Type == "GTV" || Doc_Type == "ADJ")
                                //    {
                                //        ApplayLuggageCost(Part_Number_Sup, LuggageMainItemRetailPrice, Val / QTY_LUQ);
                                //    }
                                //    //Added By DD && Abu On 08/Jan/2011 09:55 AM For Limiting Cost Updation Only On Purchase Time and Transfer Time

                                //}
                            }
                            else
                            {
                                if (Doc_Type == "PRH" || Doc_Type == "PRR") throw (new Exception("Invalid Doc Type For Bundle Item"));
                            }
                            cmd = con.CreateCommand();
                            //if (Doc_Type == "PRH" || Doc_Type == "PRR")                            
                            //Updated By DD && Abu On 08/Jan/2011 09:55 AM For Limiting Value On Case of Sale Time
                            if (Doc_Type == "PRH" || Doc_Type == "PRR" || Doc_Type == "OB" || Doc_Type == "GTV" || Doc_Type == "ADJ")
                            {
                                cmd.CommandText = "select Pack_Multiply_Qty,asy_qty,item_bundle_Cost,packing,part_number_sup from invt_assemblysub where ASY_part_number_sup='" + Part_Number_Sup + "'";
                            }
                            else
                            {
                                cmd.CommandText = "select Pack_Multiply_Qty,asy_qty,item_bundle_value,packing,part_number_sup from invt_assemblysub where ASY_part_number_sup='" + Part_Number_Sup + "'";
                            }
                            if (tr != null) cmd.Transaction = tr;
                            rs = cmd.ExecuteReader();
                            while (rs.Read())
                            {
                                SubItemCount++;
                                UpdateItemTranBatch(Loc_Code, TranDate, Doc_Type, Part_Number_Sup, QTY_LUQ, Val, FOC_LUQ, Pack_Multiply_Qty, Packing, To_Loc_Code, SysDate, ZeroSerial, PackingOrder, unitRate, batchCode);
                                UpdateStockBatch(Loc_Code, rs["part_number_sup"].ToString(), rs["packing"].ToString(), "", Doc_No, Doc_Type, "", (QTY_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)), (QTY_LUQ * rs.GetDecimal(2)), (FOC_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)), rs.GetDecimal(1), rs.GetDecimal(0), Doc_Year, Doc_Date, To_Loc_Code, batchCode, expiryDate);//UpdateStock(Loc_Code, rs["part_number_sup"].ToString(), rs["packing"].ToString(), "", Doc_No, Doc_Type, "", (QTY_LUQ * rs.GetDecimal(0) * rs.GetDecimal(1)),(QTY_LUQ * rs.GetDecimal(1) * rs.GetDecimal(2) * rs.GetDecimal(0)), FOC_LUQ, rs.GetDecimal(1), rs.GetDecimal(0), Doc_Year, Doc_Date, To_Loc_Code);
                            }
                            if (SubItemCount <= 0)
                            {
                                throw (new Exception(" Bundle Sub Items Not Found"));
                            }

                        }
                        catch { throw; }
                        finally { if (rs != null) rs.Close(); cmd.Dispose(); }
                        break;
                    default:
                        Exception Ex = new Exception("STOCK UPDATION ERROR - INVALID ITEM TYPE");
                        throw (Ex);
                }



            }
            catch (Exception Ee)
            {
                throw;
            }
            // added on apr 28 10
            //    if (!AddToTranStock(Loc_Code, Part_Number_Sup, Packing, Item_Type, Doc_No, Doc_Type, GRV_TYPE, QTY_LUQ, Val, FOC_LUQ, PackQty, Pack_Multiply_Qty, Doc_Year, Doc_Date, To_Loc_Code, con, tr,Defaults.Def_Base_LOC))
            //    {
            //        throw (new Exception("NOT ADDED TO TRANSFER"));
            //    }
            //
            //if (boolAddToTranStock)
            //{
            //    if (!AddToTranStock(Loc_Code, Part_Number_Sup, Packing, Item_Type, Doc_No, Doc_Type, GRV_TYPE, QTY_LUQ, Val, FOC_LUQ, PackQty, Pack_Multiply_Qty, Doc_Year, Doc_Date, To_Loc_Code, con, tr, (Defaults.Def_Base_Individual_Loc ? Defaults.Def_Base_LOC : Defaults.Def_Base_Management_LOC), Unit_Rate, false))
            //    {
            //        throw (new Exception("NOT ADDED TO TRANSFER"));
            //    }
            //}


            ModifyLc = false;
            //Skip_LC_Update = false;
            LC_ForTranDtl = 0;
            LC_ForStBalance = 0;
            LC_ForStBalanceToLoc = 0;

            return true;

        }
        private bool UpdateStockBalanceBatch(string Loc_code, string Doc_type, string part_number_Sup, decimal Qty, decimal Val, string To_Loc_Code, int ZeroSerial, int PackingOrder, string Packing, string batchCode, DateTime expiryDate)
        {
            int Result = -1;
            OracleCommand cmd = null;
            try
            {




                string Sql = "Update INVT_INVENTORYBALANCE_BATCH ", Sql1 = "";
                switch (Doc_type.ToUpper())
                {
                    case "SAL":
                        Sql = Sql + " set YR_SALE_QTY=YR_SALE_QTY + " + Qty + ",YR_SALE_VAL=YR_SALE_VAL + " + Val + ",SALE_QTY=sale_qty+" + Qty + ",sale_val=sale_val+" + Val + ",cb_qty=cb_qty-(" + Qty + "),cb_val=cb_val-(" + Val + ")";
                        break;
                    case "SRT":
                        Sql = Sql + " set YR_SALE_R_QTY=YR_SALE_R_QTY + " + Qty + ",YR_SALE_R_VAL=YR_SALE_R_VAL + " + Val + ", SALE_R_QTY=sale_R_qty+" + Qty + ",sale_R_val=sale_R_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val;
                        break;
                    case "PRH":
                        Sql = Sql + " set BATCH_EXPIRY = '" + expiryDate.ToString("dd/MMM/yyyy") + "', YR_PRH_QTY=YR_PRH_QTY + " + Qty + ",YR_PRH_VAL=YR_PRH_VAL + " + Val + ", PRH_QTY=PRH_qty+" + Qty + ",PRH_val=PRH_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val + (LC_ForTranDtl <= 0 ? "" : ",LC=" + LC_ForTranDtl);
                        //Sql = Sql + " set BATCH_EXPIRY = '" + expiryDate.ToShortDateString() + "', YR_PRH_QTY=YR_PRH_QTY + " + Qty + ",YR_PRH_VAL=YR_PRH_VAL + " + Val + ", PRH_QTY=PRH_qty+" + Qty + ",PRH_val=PRH_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val + (LC_ForTranDtl <= 0 ? "" : ",LC=" + LC_ForTranDtl);
                        break;
                    case "PRR":
                        Sql = Sql + " set YR_PRH_R_QTY=YR_PRH_R_QTY + " + Qty + ",YR_PRH_R_VAL=YR_PRH_R_VAL + " + Val + ", PRH_R_QTY=PRH_R_qty+" + Qty + ",PRH_R_val=PRH_R_val+" + Val + ",cb_qty=cb_qty-(" + (Qty) + "),cb_val=cb_val-(" + (Val) + ")";
                        break;
                    case "OB":
                        Sql = Sql + " set OB_QTY=OB_qty+" + Qty + ",ob_val=ob_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val;
                        break;
                    case "GTV":
                    case "ADJ":
                        string updlc = "";
                        if (ModifyLc)
                            updlc = ",LC=" + LC_ForTranDtl;
                        Sql1 = Sql + " set BATCH_EXPIRY = '" + expiryDate.ToString("dd/MMM/yyyy") + "',  YR_TRI_QTY=YR_TRI_QTY+" + Qty + ",YR_TRI_val=YR_TRI_val+" + Val + ",TRI_QTY=TRI_QTY+" + Qty + ",TRI_val=TRI_val+" + Val + ",cb_qty=cb_qty+" + Qty + ",cb_val=cb_val+" + Val + updlc + " where loc_code='" + To_Loc_Code + "' and part_number_Sup='" + part_number_Sup + "' and BATCH_CODE = '" + batchCode + "' and zero_serial=" + ZeroSerial;
                        Sql = Sql + " set BATCH_EXPIRY = '" + expiryDate.ToString("dd/MMM/yyyy") + "', YR_TRR_QTY=YR_TRR_QTY+" + Qty + ",YR_TRR_val=YR_TRR_val+" + Val + ",TRR_QTY=TRR_QTY+" + Qty + ",TRR_val=TRR_val+" + Val + ",cb_qty=cb_qty-(" + (Qty) + "),cb_val=cb_val-(" + (Val) + ")";

                        break;
                    default:
                        Exception ex = new Exception("Invalid Document Type.");
                        throw (ex);
                }

                Sql = Sql + " where loc_code='" + Loc_code + "' and part_number_Sup='" + part_number_Sup + "' and zero_serial=" + ZeroSerial + " AND BATCH_CODE = '" + batchCode + "' ";
            ReSl:
                cmd = con.CreateCommand();
                cmd.CommandText = Sql;
                if (tr != null) cmd.Transaction = tr;
                Result = cmd.ExecuteNonQuery();
                if (Result == 0)
                {

                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    if (ZeroSerial == 0 && PackingOrder != 0)
                    {
                        cmd.CommandText = "insert into INVT_INVENTORYBALANCE_BATCH(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC,BATCH_CODE,BATCH_EXPIRY) select distinct '" + Loc_code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + this.LC_ForStBalance + ",'" + batchCode + "','" + expiryDate.ToShortDateString() + "'  from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing_order=1";
                    }
                    else
                    {
                        cmd.CommandText = "insert into INVT_INVENTORYBALANCE_BATCH(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC,BATCH_CODE,BATCH_EXPIRY) select distinct '" + Loc_code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + this.LC_ForStBalance + ",'" + batchCode + "','" + expiryDate.ToShortDateString() + "'  from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing='" + Packing + "'";
                    }
                    Result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (Result > 0) goto ReSl;
                    Exception ex = new Exception("Cannot Update New Item Balance Stock.");
                    throw (ex);
                }
                if (Result > 0 && !(Sql1.Equals("")))
                {
                ReTr:
                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    cmd.CommandText = Sql1;
                    Result = cmd.ExecuteNonQuery();
                    if (Result == 0)
                    {
                        cmd.Dispose();
                        cmd = con.CreateCommand();
                        if (tr != null) cmd.Transaction = tr;
                        if (ZeroSerial == 0 && PackingOrder != 0)
                        {
                            cmd.CommandText = "insert into INVT_INVENTORYBALANCE_BATCH(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC,BATCH_CODE,BATCH_EXPIRY) select distinct '" + To_Loc_Code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + LC_ForStBalanceToLoc + ",'" + batchCode + "','" + expiryDate.ToShortDateString() + "' from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing_order=1";
                        }
                        else
                        {
                            cmd.CommandText = "insert into INVT_INVENTORYBALANCE_BATCH(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,LC,BATCH_CODE,BATCH_EXPIRY) select distinct '" + To_Loc_Code + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty," + LC_ForStBalanceToLoc + ",'" + batchCode + "','" + expiryDate.ToShortDateString() + "'  from INVT_ITEMPACKING where part_number_sup='" + part_number_Sup + "' and packing='" + Packing + "'";
                        }
                        Result = cmd.ExecuteNonQuery();
                        if (Result > 0) goto ReTr;
                        Exception ex = new Exception("Cannot Update New Item Balance Stock.");
                        throw (ex);
                    }
                };
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
            if (Result <= 0)
            {
                Exception ex = new Exception("Cannot Update Balance Stock.");
                throw (ex);
            }
            else
            {
                return true;
            }
        }
        private bool UpdateStockBalanceBatch(string Loc_code, string Doc_type, string part_number_Sup, decimal Qty, decimal Val, string To_Loc_Code, string Doc_Date, string SysDate, int packingorder, int ZeroSerial, string Packing, string batchCode, DateTime expiryDate)
        {

            int Month = (DateTime.Parse(Doc_Date).Month);
            int Yr = DateTime.Parse(Doc_Date).Year;
            string Year = (Yr).ToString();
            if (!(Year.Equals(DateTime.Today.Year.ToString())))
            {
                //          DataConnector.Message("YOU CANNOT EDIT PAST YEAR' INVENTORY");
                //          return false; 
            }
            return UpdateStockBalanceBatch(Loc_code, Doc_type, part_number_Sup, Qty, Val, To_Loc_Code, ZeroSerial, packingorder, Packing, batchCode, expiryDate);
        }
        private bool UpdateLocLcsBatch(string LOC_CODE, decimal LC, string Part_Number_Sup, int ZeroSerial, int PackingOrder, string Packing, string DATE, string DOC_TYPE, string DOC_NO, string BaseLoc, string batchCode)
        {
            if (Skip_LC_Update) return true;
            string sql = "select a.loc_code,a.loc_type from comn_location a inner join comn_location b on a.invt_group_loc=b.invt_group_loc where b.loc_code='" + LOC_CODE + "' and a.active='Y'";
            OracleCommand cmd = con.CreateCommand();
            cmd.CommandText = sql;
            OracleDataReader rs = null;
            if (tr != null) cmd.Transaction = tr;
            try
            {
                rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    int roweffect = 0;
                    if (rs.IsDBNull(0) || rs["loc_type"].ToString() == "MN") continue;
                    //if (LOC_CODE == rs["loc_code"].ToString() || BaseLoc == rs["loc_code"].ToString()) continue;

                    string DT = "";
                    if (DOC_TYPE == "PRH")
                    {
                        DT = ",LAST_GRV_NO='" + DOC_NO + "',LAST_GRV_DATE='" + DATE + "',LC_PURCH=" + LC + "";
                    }
                    else if (DOC_TYPE == "GTV")
                    {
                        DT = ",LAST_TRI_NO='" + DOC_NO + "',LAST_TRI_DATE='" + DATE + "',LC_TR=" + LC + "";
                    }
                    else if (DOC_TYPE == "GRVI")
                    {
                        DT = ",LAST_DBN_NO='" + DOC_NO + "',LAST_DBN_DATE='" + DATE + "',LAST_DBN_LC=" + LC + "";
                    }



                    //if (clsGen.updateTable("invt_itemtran_dtls", "LC=" + LC + DT, "loc_code='" + rs["loc_code"].ToString() + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial, out roweffect, con, tr) && roweffect <= 0)
                    //{
                    //    OracleCommand cmd1 = null;
                    //    int Result = 0;
                    //    cmd1 = con.CreateCommand();
                    //    if (tr != null) cmd1.Transaction = tr;
                    //    if (PackingOrder != 0)
                    //    {
                    //        cmd1.CommandText = "insert into INVT_itemtran_dtls(PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,zero_serial,packing) select PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,'" + rs["loc_code"].ToString() + "' as LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,0 as zero_serial,'" + Packing + "' as packing from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                    //    }
                    //    else
                    //    {
                    //        cmd1.CommandText = "insert into INVT_itemtran_dtls(PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,zero_serial,packing) select PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,'" + rs["loc_code"].ToString() + "' as LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD," + ZeroSerial + " as zero_serial,'" + Packing + "' as packing from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                    //    }
                    //    Result = cmd1.ExecuteNonQuery();
                    //    cmd1.Dispose();
                    //    if (Result > 0)
                    //    {
                    //        clsGen.updateTable("invt_itemtran_dtls", "LC=" + LC + DT, "loc_code='" + rs["loc_code"].ToString() + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial, out roweffect, con, tr);
                    //    }
                    //}

                    roweffect = 0;
                    if (clsGen.updateTable("INVT_INVENTORYBALANCE_BATCH", "LC=" + LC, "loc_code='" + rs["loc_code"].ToString() + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial + " and BATCH_CODE = '" + batchCode + "'", out roweffect, con, tr) && roweffect <= 0)
                    {
                        OracleCommand cmd1 = null;
                        int Result = 0;
                        cmd1 = con.CreateCommand();
                        if (tr != null) cmd1.Transaction = tr;
                        if (PackingOrder != 0)
                        {
                            //cmd1.CommandText = "insert into INVT_itemtran_dtls(PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,zero_serial,packing) select PART_NUMBER,SUPPLIER_CODE,PART_NUMBER_SUP,'" + rs["loc_code"].ToString() + "' as LOC_CODE,LINKED_ITEM,ITEM_TYPE,COSTING_METHOD,0 as zero_serial,'" + Packing + "' as packing from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                            cmd1.CommandText = "insert into INVT_INVENTORYBALANCE_BATCH(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,BATCH_CODE) select distinct '" + rs["loc_code"].ToString() + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty,'" + batchCode + "' from INVT_ITEMPACKING where part_number_sup='" + Part_Number_Sup + "' and packing_order=1";

                        }
                        else
                        {
                            cmd1.CommandText = "insert into INVT_INVENTORYBALANCE_BATCH(LOC_CODE,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,cat_0,cat_1,cat_2,cat_3,cat_4,cat_5,zero_serial,packing,pack_multiply_qty,BATCH_CODE) select distinct '" + rs["loc_code"].ToString() + "' as loc_code,CATEGORY_CODE,part_number_sup,PART_NUMBER,BRAND_CODE,LINKED_ITEM,ITEM_TYPE,substr(category_code,1,2) as cat_0,substr(category_code,3,2) as cat_1,substr(category_code,5,2) as cat_2,substr(category_code,7,2) as cat_3,substr(category_code,9,2) as cat_4,substr(category_code,11,2) as cat_5,zero_serial,packing,pack_multiply_qty,'" + batchCode + "' from INVT_ITEMPACKING where part_number_sup='" + Part_Number_Sup + "' and packing='" + Packing + "'";
                        }
                        Result = cmd1.ExecuteNonQuery();
                        cmd1.Dispose();
                        if (Result > 0)
                        {
                            clsGen.updateTable("INVT_INVENTORYBALANCE_BATCH", "LC=" + LC, "loc_code='" + rs["loc_code"].ToString() + "' and part_number_Sup='" + Part_Number_Sup + "' And BATCH_CODE = '" + batchCode + "' and zero_serial=" + ZeroSerial, out roweffect, con, tr);
                        }
                    }
                    //if (clsGen.updateTable("invt_itemtransaction", "LC=" + LC, " doc_date>='" + DateTime.Today.ToString("dd/MMM/yyyy") + "' and loc_code='" + rs["loc_code"].ToString() + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial=" + ZeroSerial, out roweffect, con, tr) && roweffect <= 0)
                    //{ }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                rs.Close();
                cmd.Dispose();
            }
            return true;
        }
        public decimal getStockBatch(string LocCode, string PartNumberSup, int ZeroSerial, string batchCode, OracleConnection Conn, OracleTransaction Tr)
        {
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            decimal decStock = 0;
            try
            {
                cmd = Conn.CreateCommand();
                if (Tr != null) cmd.Transaction = Tr;
                cmd.CommandText = "Select Round((CB_QTY/PACK_MULTIPLY_QTY),2) as STOCK From INVT_INVENTORYBALANCE_BATCH Where LOC_CODE='" + LocCode + "' And PART_NUMBER_SUP='" + PartNumberSup + "' And ZERO_SERIAL=" + ZeroSerial + " AND BATCH_CODE = '" + batchCode + "' AND CB_QTY >0";
                if (string.IsNullOrEmpty(batchCode))
                {
                    cmd.CommandText = "Select Round (SUM(CB_QTY/PACK_MULTIPLY_QTY),2) as STOCK From INVT_INVENTORYBALANCE_BATCH Where LOC_CODE='" + LocCode + "' And PART_NUMBER_SUP='" + PartNumberSup + "' And ZERO_SERIAL=" + ZeroSerial + " AND CB_QTY >0";
                }
                rs = cmd.ExecuteReader();
                if (rs.HasRows)
                {
                    if (rs.Read())
                    {
                        if (rs["STOCK"].ToString() != "")
                        {
                            //MessageBox.Show(rs["STOCK"].ToString());
                            decStock = decimal.Parse(rs["STOCK"].ToString());
                        }
                        else
                        {
                            return 0;
                        }
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
        public List<ModelBatch> GetBatchStockList(string partNumberSup, int zeroSerial, string locCode, OracleConnection Con, OracleTransaction Tr)
        {
            OracleCommand cmd = null;
            OracleDataReader reader = null;
            string oSql = "";
            List<ModelBatch> listModelBatch = new List<ModelBatch>();
            try
            {
                cmd = Con.CreateCommand();
                //if (Tr != null)
                //{
                //    cmd.Transaction = Tr;
                //}
                oSql = "Select LOC_CODE,ROUND((OB_QTY/PACK_MULTIPLY_QTY),4) as OB_QTY,((PRH_QTY-PRH_R_QTY)/PACK_MULTIPLY_QTY) as PRHQTY," +
                       "ROUND(((SALE_QTY-SALE_R_QTY)/PACK_MULTIPLY_QTY),4) as SALEQTY,(TRI_QTY/PACK_MULTIPLY_QTY)as TRI_QTY,(TRR_QTY/PACK_MULTIPLY_QTY) as TRR_QTY," +
                       "(((OB_QTY+PRH_QTY+SALE_QTY+TRI_QTY)-(PRH_R_QTY+SALE_R_QTY+TRR_QTY))/PACK_MULTIPLY_QTY) as STOCK," +
                       "ROUND((CB_QTY/PACK_MULTIPLY_QTY),4)as CB_QTY,(ADJ_QTY/PACK_MULTIPLY_QTY)as ADJ_QTY,ZERO_SERIAL,PACKING,PACK_MULTIPLY_QTY,BATCH_CODE,to_date(BATCH_EXPIRY,'dd/Mon/yyyy')BATCH_EXPIRY " +
                       " From INVT_INVENTORYBALANCE_BATCH Where LOC_CODE<>'GDN' And PART_NUMBER_SUP='" + partNumberSup + "' AND LOC_CODE='" + locCode + "' and ZERO_SERIAL=" + zeroSerial + " AND CB_QTY >0 Order By ZERO_SERIAL,LOC_CODE Asc";

                //cmd3.CommandText = "SELECT Round((CB_QTY/PACK_MULTIPLY_QTY),2)as Stock FROM  INVT_INVENTORYBALANCE WHERE LOC_CODE='" + LocCode + "' AND PART_NUMBER_SUP='" + PartNumber + "' and ZERO_SERIAL=" + Zero + "";

                //cmd.CommandText = @"SELECT BATCH_CODE,BATCH_EXPIRY,CB_QTY,LOC_CODE FROM INVT_INVENTORYBALANCE_BATCH WHERE PART_NUMBER_SUP = '" + partNumberSup + "'" +
                //                    "AND PACKING  = '" + packing + "' AND LOC_CODE = '" + locCode + "' AND CB_QTY > 0 ORDER BY BATCH_CODE";

                cmd.CommandText = oSql;

                cmd.Transaction = Tr;
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ModelBatch modelBatch = new ModelBatch();
                        modelBatch.batchCode = reader["BATCH_CODE"].ToString();
                        modelBatch.batchQty = reader["CB_QTY"].ToString();
                        modelBatch.expiryDate = reader["BATCH_EXPIRY"].ToString() == "" ? "" : DateTime.Parse(reader["BATCH_EXPIRY"].ToString()).ToShortDateString();
                        listModelBatch.Add(modelBatch);
                    }
                }
                else
                {
                    cmd = Con.CreateCommand();
                    if (Tr != null)
                    {
                        cmd.Transaction = Tr;
                    }
                    cmd.CommandText = "SELECT BATCH_CODE,CB_QTY,EXPIRY_DATE,CREATE_DT FROM INVT_INVENTORY_BATCH WHERE PART_NUMBER='" + partNumberSup + "' AND CB_QTY > 0 AND ZERO_SERIAL=" + zeroSerial + "  AND LOC_CODE='" + locCode + "' ORDER BY  EXPIRY_DATE ASC ";
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ModelBatch modelBatch = new ModelBatch();
                            modelBatch.batchCode = reader["BATCH_CODE"].ToString();
                            modelBatch.batchQty = reader["CB_QTY"].ToString();
                            modelBatch.expiryDate = reader["EXPIRY_DATE"].ToString() == "" ? "" : DateTime.Parse(reader["EXPIRY_DATE"].ToString()).ToShortDateString();
                            listModelBatch.Add(modelBatch);

                            //long qty_b = long.Parse(reader["CB_QTY"].ToString()) / packMulQty;
                            //dgvbatch["QTY", row].Value = qty_b.ToString();
                            //dgvbatch["EXPIRY", row].Value = String.Format("{0:dd/MMM/yyyy }", reader["EXPIRY_DATE"]);
                            //dgvbatch["SHIPPING_DATE", row].Value = String.Format("{0:dd/MMM/yyyy }", reader["CREATE_DT"]);
                        }

                    }

                }

                return listModelBatch;
            }
            catch (Exception Ex)
            {
                DataConnector.Message("Error" + Ex, "E", "");
                throw Ex;
            }
            finally
            {
                if (cmd != null) { cmd.Dispose(); }
                if (reader != null) { reader.Close(); reader.Dispose(); }
            }
        }
        private bool UpdateItemTranBatch(string Loc_Code, string Doc_Date, string Doc_Type, string Part_Number_Sup, decimal Qty, decimal Val, decimal FOC, decimal Pack_Multiply_Qty, string Packing, string To_Loc, string SysDate, int ZeroSerial, int PackingOrder, decimal Unit_Rate, string batchCode)
        {
            //
            // 10 , 10*5

            //---
            DateTime Dt = DateTime.Parse(Doc_Date);
            decimal AQty = Qty;
            int Result = -1;
            OracleCommand cmd = null;
            try
            {

                if (ZeroSerial > 0 && Pack_Multiply_Qty > 1 && Doc_Type == "GTV")
                {

                }

                string Sql = "Update invt_itemtransaction_BATCH ", Sql1 = "";
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
                                clsGen.insertTable("INVT_INVENTORYLOGBATCH", "Loc_code,doc_type,part_number_sup,zero_serial,Log_Type,Mail_Sent,value_org,value_comp,doc_date,remarks,packing,margine,qty,BATCH_CODE", "'" + Loc_Code + "','SAL','" + Part_Number_Sup + "'," + ZeroSerial + ",'SALE','N'," + Unit_Rate + "," + Lc + ",'" + Doc_Date + "','" + Remarks + "','" + Packing + "'," + margine + "," + Qty + ",'" + batchCode + "'", con, tr);
                            }
                            //
                        }
                        Sql = Sql + " set SALE_QTY=sale_qty+" + Qty + ",offer_pack='" + offer_pack + "',price_code='" + Price_Code + "',sale_val=sale_val+" + Val;

                        if (FOC != 0)
                        {
                            Sql = Sql + ",FOC_SALE=FOC_SALE+" + FOC;
                        }
                        AQty = -1 * Qty;
                        break;
                    case "SRT":

                        Sql = Sql + " set SALE_R_QTY=sale_R_qty+" + Qty + ",offer_pack='" + offer_pack + "',price_code='" + Price_Code + "',sale_R_val=sale_R_val+" + Val;

                        if (FOC != 0)
                        {
                            Sql = Sql + " ,FOC_SRT=FOC_SRT+" + FOC;
                        }
                        break;
                    case "PRH":
                        Sql = Sql + " set PRH_QTY=PRH_qty+" + Qty + ",offer_pack='" + offer_pack + "',price_code='" + Price_Code + "',PRH_val=PRH_val+" + Val + (LC_ForTranDtl <= 0 ? "" : ",LC=" + LC_ForTranDtl);
                        if (FOC != 0)
                        {
                            Sql = Sql + " ,FOC_PRH=FOC_PRH+" + FOC;
                        }
                        break;
                    case "PRR":

                        Sql = Sql + " set PRH_R_QTY=PRH_R_qty+" + Qty + ",offer_pack='" + offer_pack + "',price_code='" + Price_Code + "',PRH_R_val=PRH_R_val+" + Val;

                        if (FOC != 0)
                        {
                            Sql = Sql + " ,FOC_PRR=FOC_PRR+" + FOC;
                        }
                        AQty = -1 * Qty;
                        break;
                    case "GTV":
                    case "ADJ":
                        Sql1 = " TRI_QTY=TRI_QTY+" + Qty + ",offer_pack='" + offer_pack + "',price_code='" + Price_Code + "',TRI_val=TRI_val+" + Val + ",CB_QTY=CB_QTY+" + Qty + (ModifyLc ? ",LC=" + LC_ForTranDtl : "") + " where loc_code='" + To_Loc + "' and doc_date='" + Doc_Date + "' and part_number_Sup='" + Part_Number_Sup + "' and zero_serial='" + ZeroSerial + "'";
                        Sql = Sql + " set TRR_QTY=TRR_QTY+" + Qty + ",offer_pack='" + offer_pack + "',price_code='" + Price_Code + "',TRR_val=TRR_val+" + Val;
                        AQty = -1 * Qty;
                        break;
                    default:
                        Exception ex = new Exception("Invalid Document Type.");
                        throw (ex);

                }

                string upd = ",cb_qty=cb_qty+" + AQty;
            ReSl: sql = Sql + " " + upd + " where doc_date='" + Doc_Date + "' and  loc_code='" + Loc_Code + "'  and part_number_Sup='" + Part_Number_Sup + "' and zero_serial='" + ZeroSerial + "' AND BATCH_CODE = '" + batchCode + "'";
                cmd = con.CreateCommand();
                cmd.CommandText = sql;
                if (tr != null) cmd.Transaction = tr;
                Result = cmd.ExecuteNonQuery();
                if (Result > 0 && SysDate.ToUpper() != Doc_Date.ToUpper()) AdjustStockTran(Doc_Date, Loc_Code, Part_Number_Sup, SysDate, AQty, ZeroSerial);
                decimal Ob_Qty = 0;
                decimal Lc_Loc = 0;
                if (Result == 0)
                {

                    Ob_Qty = getStock_TranBatch(Part_Number_Sup, Loc_Code, Doc_Date, ZeroSerial, SysDate, out Lc_Loc, Pack_Multiply_Qty, false, Packing, batchCode);
                    if (Lc_Loc == 0 && LC_ForTranDtl != 0) Lc_Loc = LC_ForTranDtl;
                    LC_ForStBalance = Lc_Loc;
                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    if (ZeroSerial == 0 && PackingOrder != 0)
                    {
                        cmd.CommandText = "insert into INVT_ITEMTRANSACTION_BATCH(doc_date,loc_code,doc_year,doc_month,doc_day,part_number_sup,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name,BATCH_CODE) select  '" + Doc_Date + "' as doc_date,'" + Loc_Code + "' as loc_code,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,default_packing,1," + Ob_Qty + " as ob_qty,0 as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name,'" + batchCode + "' from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                    }
                    else
                    {
                        cmd.CommandText = "insert into INVT_ITEMTRANSACTION_BATCH(doc_date,loc_code,doc_year,doc_month,doc_day,part_number_sup,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name,BATCH_CODE) select  '" + Doc_Date + "' as doc_date,'" + Loc_Code + "' as loc_code,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,'" + Packing + "'," + Pack_Multiply_Qty + "," + Ob_Qty + " as ob_qty," + ZeroSerial + " as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name,'" + batchCode + "'  from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                    }
                    Result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (Result > 0) goto ReSl;
                    Exception ex = new Exception("New Item Tran Error.");
                    throw (ex);

                }
                if (Result > 0 && !(Sql1.Equals("")))
                {
                ReTr:
                    AQty = -1 * Qty;
                    Sql = "update INVT_ITEMTRANSACTION_BATCH set " + Sql1;
                    cmd.Dispose();
                    cmd = con.CreateCommand();
                    if (tr != null) cmd.Transaction = tr;
                    cmd.CommandText = Sql;
                    Result = cmd.ExecuteNonQuery();
                    if (Result > 0 && SysDate.ToUpper() != Doc_Date.ToUpper()) AdjustStockTranBatch(Doc_Date, Loc_Code, Part_Number_Sup, SysDate, AQty, ZeroSerial, batchCode);
                    if (Result == 0)
                    {
                        Ob_Qty = getStock_Tran(Part_Number_Sup, To_Loc, Doc_Date, ZeroSerial, SysDate, out Lc_Loc, Pack_Multiply_Qty, false, Packing);
                        if (Lc_Loc == 0 && LC_ForTranDtl != 0) Lc_Loc = LC_ForTranDtl;
                        LC_ForStBalanceToLoc = Lc_Loc;
                        cmd.Dispose();
                        //Ob_Qty = getStock(Part_Number_Sup, To_Loc);
                        cmd = con.CreateCommand();
                        if (tr != null) cmd.Transaction = tr;
                        if (ZeroSerial == 0 && PackingOrder != 0)
                        {
                            cmd.CommandText = "insert into INVT_ITEMTRANSACTION_BATCH(doc_date,loc_code,doc_year,doc_month,doc_day,part_number_sup,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name,BATCH_CODE) select  '" + Doc_Date + "' as doc_date,'" + To_Loc + "' as loc_code,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,default_packing,1," + Ob_Qty + " as ob_qty,0 as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name,'" + batchCode + "'  from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                        }
                        else
                        {
                            cmd.CommandText = "insert into INVT_ITEMTRANSACTION_BATCH(doc_date,loc_code,doc_year,doc_month,doc_day,part_number_sup,part_number,PART_DESCRIPTION,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,packing,pack_multiply_qty,ob_qty,zero_serial,CB_QTY,LC,division_code,division_name,BATCH_CODE) select  '" + Doc_Date + "' as doc_date,'" + To_Loc + "' as loc_code,'" + Dt.Year + "' as doc_year," + Dt.Month + " as Doc_month," + Dt.Day + " as doc_day,'" + Part_Number_Sup + "' as part_number_sup,part_number,PART_DESCRIPTION as part_description,CATEGORY_CODE,BRAND_CODE,CATEGORY_NAME,BRAND_NAME,SUPPLIER_CODE,SUPPLIER_NAME,LINKED_ITEM,'" + Packing + "'," + Pack_Multiply_Qty + "," + Ob_Qty + " as ob_qty," + ZeroSerial + " as zero_serial," + Ob_Qty + " as Cb_qty," + Lc_Loc + " as LC,division_code,division_name,'" + batchCode + "'  from invt_inventorymaster where part_number_sup='" + Part_Number_Sup + "'";
                        }
                        Result = cmd.ExecuteNonQuery();
                        //      decimal Stock = this.getStock(Part_Number_Sup, To_Loc );
                        //      Sql1 = " ob_Qty=" + Stock + "," +Sql1;
                        if (Result > 0) goto ReTr;
                        Exception ex = new Exception("New Item Tran Error.");
                        throw (ex);

                    }
                };
                //string Loc_Code, string Doc_Date, string Doc_Type, string Part_Number_Sup, decimal Qty, decimal Val, decimal FOC, decimal Pack_Multiply_Qty, string Packing, string To_Loc, string SysDate, int ZeroSerial, int PackingOrder, decimal Unit_Rate,string batchCode                
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
            }
            if (Result <= 0)
            {
                Exception ex = new Exception("Cannot Update Stock.");
                throw (ex);
            }
            else
            {
                return true;
            }
        }
        private bool AdjustStockTranBatch(string Doc_Date, string Loc_code, string Part_Number, string sysdate, decimal Qty, int ZeroSerial, string batchCode)
        {
            DateTime dt = DateTime.Parse(Doc_Date);
            DateTime SysDate = DateTime.Parse(sysdate);
            dt = dt.AddDays(1);
            OracleCommand cmd = con.CreateCommand();

            cmd.Transaction = tr;
            cmd.CommandText = "update INVT_ITEMTRANSACTION_BATCH set cb_qty=cb_qty+" + Qty + ",ob_qty=ob_qty+" + Qty + " where Loc_code='" + Loc_code + "' and doc_date>='" + dt.ToString("dd/MMM/yyyy") + "' and doc_date<='" + sysdate + "' and Part_number_sup='" + Part_Number + "' and Zero_serial=" + ZeroSerial + "AND BATCH_CODE = '" + batchCode + "'";
            cmd.ExecuteNonQuery().ToString();
            cmd.Dispose();
            return true;
        }
        private decimal getStock_TranBatch(string Part_Number_Sup, string Loc_Code, string doc_date, int zeroSerial, string Sysdate, out decimal LC, decimal PackMultiplyQty, bool FROM_TRAN, string Packing, string batchCode)
        {

            LC = 0;
            decimal Ob = 0;
            string Sql = "";
            bool Found = false;
            if (FROM_TRAN) Sql = "select cb_Qty,LC from invt_itemtransaction_batch where doc_date<'" + doc_date + "' and  loc_code='" + Loc_Code + "'  and part_number_sup='" + Part_Number_Sup + "' and Batch_Code = '" + batchCode + "' and zero_serial=" + zeroSerial + " order by doc_date desc";
            //if(DateTime.Parse(doc_date).Month==DateTime.Parse(Sysdate).Month)
            else Sql = "select cb_Qty,LC from invt_inventorybalance_batch where loc_code='" + Loc_Code + "' and part_number_sup='" + Part_Number_Sup + "' and Batch_Code = '" + batchCode + "' and zero_serial=" + zeroSerial;
            OracleCommand cmd = null;
            OracleDataReader rs = null;
            try
            {
                cmd = con.CreateCommand();
                cmd.Transaction = tr;
                cmd.CommandText = Sql;
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    Found = true;
                    if (rs.IsDBNull(0)) Ob = 0;
                    else Ob = decimal.Parse(rs[0].ToString());
                    if (rs.IsDBNull(1)) LC = 0;
                    else LC = decimal.Parse(rs[1].ToString());
                    if (FROM_TRAN) return Ob;
                }
                rs.Close();
                cmd.Dispose();
                if (FROM_TRAN) Found = true;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (rs != null) rs.Dispose();
                if (cmd != null) cmd.Dispose();
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
                    if (LC1 > 0) LC = LC1;
                }

            }
            catch (Exception ex)
            {
                throw;
            }

            return Ob;
        }
        #endregion

    }

}
