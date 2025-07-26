using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
using System.Runtime.CompilerServices;

namespace MALL
{
    public class Barcode
    {
            private int GetCharValue(string inchar)
            {

                int i5 = 0;
                int i1 = Strings.Asc(inchar);
                int i4 = 32;
                int i3 = 18;
                if (i1 > 144)
                {
                    i5 = ((i1 - i4) - i3);
                }
                else
                {
                    i5 = (i1 - i4);
                }
                if (i1 == 128)
                {
                    i5 = 0;
                }
                return i5;
            }

            private string GetCheckDigit(string Data)
            {
                int i1 = 0;
                string string1 = null;
                int i6 = 0;
                int i3 = 32;
                int i2 = 18;
                int i4 = 1;
                int i5 = (Data).Length;
                int i7 = i5;
                for (i1 = 1; true; i1++)
                {
                    if (i1 > i7)
                    {
                        i6 %= 103;
                        if (i6 == 0)
                        {
                            return (Strings.Chr(128)).ToString();
                        }
                        else if ((i6 + i3) >= sbyte.MaxValue)
                        {
                            return (Strings.Chr(((int)((i6 + i3) + i2)))).ToString();
                        }
                        //if (i6 => 0)
                        //{
                        //    return (Strings.Chr(128)).ToString();
                        //}
                        //else if ((i6 + i3) >= sbyte.MaxValue)
                        //{
                        //    return (Strings.Chr(((int)((i6 + i3) + i2)))).ToString();
                        //}
                        else
                        {
                            return (Strings.Chr(((int)(i6 + i3)))).ToString();
                        }
                    }
                    string1 = Strings.Mid(Data, i1, 1);
                    if (i1 != 1)
                    {
                        i6 += (this.GetCharValue(string1) * i4);
                        i4++;
                    }
                    else
                    {
                        i6 += this.GetCharValue(string1);
                    }
                }
            }

            public object GetCode128(object data)
            {
                int i1 = 0;
                string string2 = null;
                string string3 = null;
                bool b1 = false;
                string string4 = null;
                int i5 = 32;
                int i4 = 18;
                int i3 = 0;
                int i7 = 1;
                int i8 = 2;
                string string1 = "";
                string string5 = (data).ToString();
                int i6 = (string5).Length;
                if (i6 == 0)
                {
                    return "";
                }
                int i2 = 1;
                int i9 = i6;
                for (i1 = 0; (i1 <= i9); i1++)
                {
                    if (i2 != i6)
                    {
                        string2 = Strings.Mid(string5, i2, 1);
                        string4 = Strings.Mid(string5, ((int)(i2 + 1)), 1);
                        b1 = this.IsNumeric(string2);
                        if (b1)
                        {
                            b1 = this.IsNumeric(string4);
                        }
                        if (b1)
                        {
                            string3 = (string2 + string4);
                            if (i3 == 0)
                            {
                                string1 = (Strings.Chr(((int)((105 + i5) + i4)))).ToString();
                                i3 = i8;
                            }
                            if (i3 == i7)
                            {
//                                string1 = (string1 + (Strings.Chr(((int)((99 + i5) + i5)))).ToString());
                                string1 = (string1 + (Strings.Chr(((int)((99 + i5) + i4)))).ToString());
                                i3 = i8;
                            }
                            string1 = (string1 + (Strings.Chr(Conversions.ToInteger(this.GetCode3Char(string3)))).ToString());
                            i2 += 2;
                        }
                        else
                        {
                            string2 = Strings.Mid(string5, i2, 1);
                            if (i3 == 0)
                            {
                                string1 = (string1 + (Strings.Chr(((int)((104 + i5) + i4)))).ToString());
                                i3 = i7;
                            }
                            if (i3 == i8)
                            {
                                string1 = (string1 + (Strings.Chr(((int)((100 + i5) + i4)))).ToString());
                                i3 = i7;
                            }
                            string1 = (string1 + string2);
                            i2++;
                        }
                    }
                    else
                    {
                        string2 = Strings.Mid(string5, i2, 1);
                        if (i3 == 0)
                        {
                            string1 = (Strings.Chr(((int)((104 + i5) + i4)))).ToString();
                            i3 = i7;
                        }
                        if (i3 == i8)
                        {
                            string1 = (string1 + (Strings.Chr(((int)((100 + i5) + i4)))).ToString());
                            i3 = i7;
                        }
                        string1 = (string1 + string2);
                        i2++;
                    }
                    if (i2 > i6)
                    {
                        break;
                    }
                }
                string1 = Strings.Replace(string1, " ", (Strings.Chr(128)).ToString(), 1, -1, CompareMethod.Binary);
                string1 = (string1 + this.GetCheckDigit(string1));
                string1 = (string1 + (Strings.Chr(((int)((106 + i5) + i4)))).ToString());
                return (string1);
            }

            private string GetCode3Char(string inputStr)
            {
                int i1 = 32;
                int i2 = 18;
                int i3 = ((int)Math.Round(Conversion.Val(inputStr)));
                if (i3 == 0)
                {
                    return (((int)(96 + i1))).ToString();
                }
                else if ((i3 > 0) && (i3 < 95))
                {
                    return (((int)(i3 + i1))).ToString();
                }
                else
                {
                    return (((int)((i3 + i1) + i2))).ToString();
                }
            }

            private bool IsNumeric(object testString)
            {
                object[] objectArray1 = new object[] { RuntimeHelpers.GetObjectValue(testString) };
                int i1 = Conversions.ToInteger(NewLateBinding.LateGet(null, typeof(Strings), "Asc", objectArray1, ((string[])null), ((Type[])null), ((bool[])null)));
                bool b2 = false;
                return (b2 || ((i1 >= 48) && (i1 <= 57)));
            }


            /////
            //public object GetCode1281(object data)
            //{
            //    int i1 = 0;
            //    string string2 = null;
            //    string string3 = null;
            //    bool b1 = false;
            //    string string4 = null;
            //    int i5 = 32;
            //    int i4 = 18;
            //    int i3 = 0;
            //    int i7 = 1;
            //    int i8 = 2;
            //    string string1 = "";
            //    string string5 = (data).ToString();
            //    int i6 = (string5).Length;
            //    if (i6 == 0)
            //    {
            //        return "";
            //    }
            //    int i2 = 1;
            //    int i9 = i6;
            //    for (i1 = 0; (i1 <= i9); i1++)
            //    {
            //        if (i2 != i6)
            //        {
            //            string2 = Strings.Mid(string5, i2, 1);
            //            string4 = Strings.Mid(string5, ((int)(i2 + 1)), 1);
            //            b1 = this.IsNumeric(string2);
            //            if (b1)
            //            {
            //                b1 = this.IsNumeric(string4);
            //            }
            //            if (b1)
            //            {
            //                string3 = (string2 + string4);
            //                if (i3 == 0)
            //                {
            //                    string1 = (Strings.Chr(((int)((105 + i5) + i4)))).ToString();
            //                    i3 = i8;
            //                }
            //                if (i3 == i7)
            //                {
            //                    string1 = (string1 + (Strings.Chr(((int)((99 + i5) + i4)))).ToString());
            //                    i3 = i8;
            //                }
            //                string1 = (string1 + (Strings.Chr(Conversions.ToInteger(this.GetCode3Char(string3)))).ToString());
            //                i2 += 2;
            //            }
            //            else
            //            {
            //                string2 = Strings.Mid(string5, i2, 1);
            //                if (i3 == 0)
            //                {
            //                    string1 = (string1 + (Strings.Chr(((int)((104 + i5) + i4)))).ToString());
            //                    i3 = i7;
            //                }
            //                if (i3 == i8)
            //                {
            //                    string1 = (string1 + (Strings.Chr(((int)((100 + i5) + i4)))).ToString());
            //                    i3 = i7;
            //                }
            //                string1 = (string1 + string2);
            //                i2++;
            //            }
            //        }
            //        else
            //        {
            //            string2 = Strings.Mid(string5, i2, 1);
            //            if (i3 == 0)
            //            {
            //                string1 = (Strings.Chr(((int)((104 + i5) + i4)))).ToString();
            //                i3 = i7;
            //            }
            //            if (i3 == i8)
            //            {
            //                string1 = (string1 + (Strings.Chr(((int)((100 + i5) + i4)))).ToString());
            //                i3 = i7;
            //            }
            //            string1 = (string1 + string2);
            //            i2++;
            //        }
            //        if (i2 > i6)
            //        {
            //            break;
            //        }
            //    }
            //    string1 = Strings.Replace(string1, " ", (Strings.Chr(128)).ToString(), 1, -1, CompareMethod.Binary);
            //    string1 = (string1 + this.GetCheckDigit(string1));
            //    string1 = (string1 + (Strings.Chr(((int)((106 + i5) + i4)))).ToString());
            //    return (string1);
            //}



        }
	
    }

