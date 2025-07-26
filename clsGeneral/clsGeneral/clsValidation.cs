using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace MALL
{
    /*#region ValidationClass

    public class Validation
    {
        //Validation Valid = new Validation();
        public bool IsNumeric1(string Number)
        {
            try { double x = double.Parse(Number); return true; }
            catch { return false; }
        }

        // text only Capital Number

        public void txtOnlyNumber(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii == 13) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }

        // text only Decimal Number(.0123,123)

        public void txtOnlyDecimal(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii == 46) || (ascii == 13) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }

        // text only Capital Only(AZ)

        public void txtOnlyWord(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 65 && ascii <= 90) || (ascii == 13) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }

        // text only Capital Only with Space    //With Space 

        public void txtOnlySpWord(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 65 && ascii <= 90) || (ascii == 32) || (ascii == 13) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }
        // text only Capital Only And (0-9)

        public void txtOnlyNumWord(object sender, KeyPressEventArgs e) //Without Space And Symbols
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii >= 65 && ascii <= 90) || (ascii == 13) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }

        // text only Capital Only And (0-9) with Space    //With Space And Number

        public void txtOnlySpNumWord(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii >= 65 && ascii <= 90) || (ascii == 32) || (ascii == 13) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }

        // text only Capital Only And Simpals(_, ,",#,$,),(,+,-,/,,,_,.,<,>,=,?,@,%,0-9)

        public void txtOnlyText(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 34 && ascii <= 37) || (ascii >= 43 && ascii <= 57) || (ascii >= 60 && ascii <= 90) || (ascii == 41) || (ascii == 32) || (ascii == 13) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }

        // text only E.Mail Characters Only......

        public void txtEmailOnly(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) ||  (ascii >= 64 && ascii <= 90) || (ascii >= 97 && ascii <= 122) || (ascii == 95) || (ascii == 46) || (ascii == 45) || (ascii == 13) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }
    }
    # endregion

    //NEW 
     
    */
    #region ValidationClass


    public static class Form_Validation
    {

        public static bool IsNumeric1(string Number)
        {
            try { double x = double.Parse(Number); return true; }
            catch { return false; }
        }

        // text only Capital Number

        public static void txtOnlyNumber(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57)  || (ascii == 8)))
            {
                e.Handled = true;
            }
        }

        // text only Decimal Number(.0123,123)

        public static void txtOnlyDecimal(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii == 46) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }

        // text only Capital Only(AZ)

        public static void txtOnlyWord(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 65 && ascii <= 90) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }

        // text only Capital Only with Space    //With Space 

        public static void txtOnlySpWord(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 65 && ascii <= 90) || (ascii == 32) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }
        // text only Capital Only And (0-9)

        public static void txtOnlyNumWord(object sender, KeyPressEventArgs e) //Without Space And Symbols
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii >= 65 && ascii <= 90) || (ascii == 8) || (ascii == 45)))
            {
                e.Handled = true;
            }
        }
        //
        public static void txtAvoidDBSpecialCharacter(object sender, KeyPressEventArgs e) //Without Space And Symbols
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (ascii == 34 || ascii == 39)
            {
                e.Handled = true;
            }
        }

        // text only Capital Only And (0-9) with Space    //With Space And Number

        public static void txtOnlySpNumWord(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii >= 65 && ascii <= 90) || (ascii == 32)  || (ascii == 8)))
            {
                e.Handled = true;
            }
        }
        // text only Capital Only And (0-9) with Space    //With Space And Number

        public static void txtNormalCharacters(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii >= 65 && ascii <= 90) || (ascii == 32) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }
        //NEW TEXTONLY may 10 by krp
        public static void txtOnlyTextN(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 35 && ascii <= 38) || (ascii >= 42 && ascii <= 57) || (ascii >= 60 && ascii <= 90) || (ascii >= 96 && ascii <= 122) || (ascii == 95) || (ascii == 41) || (ascii == 40) || (ascii == 32) || (ascii == 13) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }
        public static void txtOnlyNumWordN(object sender, KeyPressEventArgs e) //Without Space And Symbols
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii >= 65 && ascii <= 90) || (ascii >= 96 && ascii <= 122) || (ascii == 8) || (ascii == 45)))
            {
                e.Handled = true;
            }
        }
        public static void txtOnlySpNumWordN(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii >= 65 && ascii <= 90) || (ascii >= 97 && ascii <= 122) || (ascii == 32) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }
        public static void txtOnlyWordN(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 65 && ascii <= 90) || (ascii >= 97 && ascii <= 122) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }

        // text only Capital Only And Simpals(_, ,",#,$,),(,+,-,/,,,_,.,<,>,=,?,@,%,0-9)

        public static void txtOnlyText(object sender, KeyPressEventArgs e)
        {            
            int ascii = Convert.ToInt16(e.KeyChar);            
            if (!((ascii >= 35 && ascii <= 38) || (ascii >= 42 && ascii <= 57) || (ascii >= 60 && ascii <= 90) || (ascii == 95) || (ascii == 96) || (ascii == 41) || (ascii == 40) || (ascii == 32) || (ascii == 13) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }

        // text only E.Mail Characters Only......

        public static void txtEmailOnly(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii >= 64 && ascii <= 90) || (ascii >= 97 && ascii <= 122) || (ascii == 95) || (ascii == 46) || (ascii == 45) || (ascii == 13) || (ascii == 8) || (ascii == 44)))
            {
                e.Handled = true;
            }
        }        
        // FOR BARCODE
        public static void txtOnlyBarCode(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii >= 65 && ascii <= 90) || (ascii >= 97 && ascii <= 122) || (ascii == 45) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }

        // text only PhoneNumber Only with Space (0-9,+,-,,,  

        public static void txtOnlyPhone(object sender, KeyPressEventArgs e)
        {
            int ascii = Convert.ToInt16(e.KeyChar);
            if (!((ascii >= 48 && ascii <= 57) || (ascii == 95) || (ascii == 44) ||  (ascii == 45) || (ascii == 43) || (ascii == 32) || (ascii == 8)))
            {
                e.Handled = true;
            }
        }
        public static bool IsPositive(string Number)
        {
            try
            {
                double x = double.Parse(Number);
                if (x < 0) return false;
                return true;
            }
            catch { return false; }
        }
        public static bool IsPositiveNonZero(string Number)
        {
            try
            {
                double x = double.Parse(Number);
                if (x <= 0) return false;
                return true;
            }
            catch { return false; }
        }
        //below new code dipin mar 14
        public static bool IsPositiveNonZero(string Number, bool Decimal_NOT_Allowed)
        {
            try
            {
                double x = double.Parse(Number);
                if (x <= 0) return false;
                long y = long.Parse(Number);
                if (x != y) return false;
                return true;
            }
            catch { return false; }
        }

        public static bool IsPositive(string Number, bool Decimal_NOT_Allowed)
        {
            try
            {
                double x = double.Parse(Number);
                if (x < 0) return false;
                long y = (long)decimal.Parse(Number);//long y = long.Parse(Number);
                if (x != y) return false;
                return true;
            }
            catch { return false; }
        }

        public static bool AlphaNumeric(string Text)
        {
            foreach (char ch in Text)
            { 
                int c=ch;
                if ((c >= 65 && c <= 91) || (c >= 48 && c <= 57) || c == 46 || c == 95 || c == 96 || c == 45 || c == 32)
                {
                    continue;
                }
                else 
                {
                    return false;
                }
            }
            return true;
        }
        public static bool AlphaNumeric(string Text,bool ConvertCase)
        {
            Text = Text.ToUpper();
            foreach (char ch in Text)
            {
                int c = (ch);
                if ((c >= 65 && c <= 91) || (c >= 48 && c <= 57) || c == 46 || c == 95 || c == 96 || c == 45)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        // text only Capital Only And (0-9)
        public static bool txtOnlyNumWord(string Text, bool ConvertCase) //Without Space And Symbols
        {
            if (ConvertCase) Text = Text.ToUpper();
            foreach (char ch in Text)
            {
                int c = (ch);
                if (!((c >= 48 && c <= 57) || (c >= 65 && c <= 90) || (c == 8) || (c == 45)))
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            return true;
        }
        //
        // text only Capital Only And Simpals(_, ,",#,$,),(,+,-,/,,,_,.,<,>,=,?,@,%,0-9)

        public static bool txtOnlyText(string Text, bool ConvertCase)
        {
            if (ConvertCase) Text = Text.ToUpper();
            foreach (char ch in Text)
            {
                int c = (ch);
                if (!((c >= 34 && c <= 38) || (c >= 42 && c <= 57) || (c >= 60 && c <= 90) || (c == 95) || (c == 96) || (c == 41) || (c == 40) || (c == 32) || (c == 13) || (c == 10) || (c == 8)))
                {
                    return false;
                }
                else
                {
                    continue;
                }
            }
            return true;
        }

        public static bool IsEmail(string email)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            System.Text.RegularExpressions.Match match = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }           
        }
        //public static bool isEmail(string inputEmail)
        //{
        //    string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
        //          @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
        //          @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
        //    Regex re = new Regex(strRegex);
        //    if (re.IsMatch(inputEmail))
        //        return (true);
        //    else
        //        return (false);
        //}

        public static void SetKeys()
        {
            SetNumKey();
            CapsKey();
        
        }

        public static void  SetNumKey()
        {
            if (!Control.IsKeyLocked(Keys.NumLock))
            {
               SetKey(0x90);
                
            }
        }
        public static void CapsKey()
        {
            if (!Control.IsKeyLocked(Keys.Capital))
            {
                SetKey(0x14);

            }
        }

            [StructLayout(LayoutKind.Sequential)]
            public struct INPUT
            {
                internal int type;
                internal short wVk;
                internal short wScan;
                internal int dwFlags;
                internal int time;
                internal IntPtr dwExtraInfo;
                int dummy1;
                int dummy2;
                internal int type1;
                internal short wVk1;
                internal short wScan1;
                internal int dwFlags1;
                internal int time1;
                internal IntPtr dwExtraInfo1;
                int dummy3;
                int dummy4;
            }
            [DllImport("user32.dll")]
            static extern int SendInput(uint nInputs, IntPtr pInputs, int cbSize);

            public static void  SetKey(short _Key)
            {
                const int mouseInpSize = 28;//Hardcoded size of the MOUSEINPUT tag !!!
                INPUT input = new INPUT();
                input.type = 0x01; //INPUT_KEYBOARD
                input.wVk = _Key; //VK_NUMLOCK
                input.wScan = 0;
                input.dwFlags = 0; //key-down
                input.time = 0;
                input.dwExtraInfo = IntPtr.Zero;

                //   Const VK_CAPITAL = &H14
                //    Const VK_NUMLOCK = &H90
                //    Const VK_SCROLL = &H91

                input.type1 = 0x01;
                input.wVk1 = 0x90;
                input.wScan1 = 0;
                input.dwFlags1 = 2; //key-up
                input.time1 = 0;
                input.dwExtraInfo1 = IntPtr.Zero;

                IntPtr pI = Marshal.AllocHGlobal(mouseInpSize * 2);
                Marshal.StructureToPtr(input, pI, false);
                int result = SendInput(2, pI, mouseInpSize); //Hardcoded size of the MOUSEINPUT tag !!!

                //if (result == 0 || Marshal.GetLastWin32Error() != 0)
                // Console.WriteLine(Marshal.GetLastWin32Error());
                Marshal.FreeHGlobal(pI);
            }
        



    }
    # endregion

}
