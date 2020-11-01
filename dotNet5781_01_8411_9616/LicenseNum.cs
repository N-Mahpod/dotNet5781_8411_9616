using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_8411_9616
{
    class LicenseNum
    {
        int number;
        int digits;
        string strNum;

        public LicenseNum(int _number = 0, int _digits = 8)
        {
            Restart(_number, _digits);
        }

        public LicenseNum(int _number = 0, DateTime startDate = new DateTime())
        {
            if (startDate.Year < 2018)
            {
                Restart(_number, 7);
            }
            else
                Restart(_number, 8);
        }

        public int GetNumber()
        {
            return number;
        }

        public int GetNumOfDigits()
        {
            return digits;
        }

        public string GetStringNumber()
        {
            return strNum;
        }

        public static string NumToStr(int num, int dig = 8)
        {
            string s = num.ToString();
            if (dig == 7)
            {
                // s = [0, 1, 2, 3, 4, 5, 6]
                // s = [0, 1, -, 2, 3, 4, 5, 6]
                s.Insert(2, "-");
                // s = [0, 1, -, 2, 3, 4, -, 5, 6]
                s.Insert(6, "-");
                return s;
            }
            if (dig == 8)
            {
                // s = [0, 1, 2, 3, 4, 5, 6, 7]
                // s = [0, 1, 2, -, 3, 4, 5, 6, 7]
                s.Insert(3, "-");
                // s = [0, 1, 2, -, 3, 4, -, 5, 6, 7]
                s.Insert(6, "-");
                return s;
            }

            return null;
        }

        public void Restart(int _number = 0, int _digits = 8)
        {
            number = _number;
            digits = _digits;
            strNum = NumToStr(number, digits);
        }
    }
}
