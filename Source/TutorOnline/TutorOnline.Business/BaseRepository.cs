using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TutorOnline.DataAccess;


namespace TutorOnline.Business
{
    public class BaseRepository
    {
        public readonly TutorOnlineContext _dbContext = new TutorOnlineContext();

        private string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public bool SearchForString(string str, string searchStr)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            return StandardString(convertToUnSign(str)).Contains(StandardString(convertToUnSign(searchStr)));
        }

        private string StandardString(string strInput)
        {
            strInput = strInput.Trim().ToLower();
            while (strInput.Contains("  "))
                strInput = strInput.Replace("  ", " ");
            return strInput.TrimEnd();
        }
    }
}