using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace WebBanHangOnline.Areas.admin.Data
{
    public class ToolClass
    {
        public static string RemoveUnidecode(string input)
        {
            if (input == null)
            {
                // Xử lý trường hợp biến input là null
                return string.Empty; // Hoặc giá trị mặc định khác tùy theo yêu cầu của bạn
            }

            string normalized = input.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}