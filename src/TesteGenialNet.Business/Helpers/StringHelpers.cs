using System;
using System.Text.RegularExpressions;

namespace TesteGenialNet.Business.Helpers
{
    public static class StringHelpers
    {
        public static string FormatCnpj(this string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                return cnpj;
            var value = Regex.Replace(cnpj, @"[^\d]", "");
            return Convert.ToUInt64(value).ToString(@"00\.000\.000\/0000\-00");
        }

        public static bool ValidateCNPJ(this string cnpj)
        {
            int[] array = new int[12]
            {
            5, 4, 3, 2, 9, 8, 7, 6, 5, 4,
            3, 2
            };
            int[] array2 = new int[13]
            {
            6, 5, 4, 3, 2, 9, 8, 7, 6, 5,
            4, 3, 2
            };
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
            {
                return false;
            }

            string text = cnpj.Substring(0, 12);
            int num = 0;
            for (int i = 0; i < 12; i++)
            {
                num += int.Parse(text[i].ToString()) * array[i];
            }

            int num2 = num % 11;
            string text2 = ((num2 >= 2) ? (11 - num2) : 0).ToString();
            text += text2;
            num = 0;
            for (int j = 0; j < 13; j++)
            {
                num += int.Parse(text[j].ToString()) * array2[j];
            }

            num2 = num % 11;
            text2 += ((num2 >= 2) ? (11 - num2) : 0);
            return cnpj.EndsWith(text2);
        }

    }
}
