using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace archiver.TextProccesing
{
    class LengthComparer : IComparer<String>
    {
        public int Compare(string x, string y)
        {
            int lengthComparison = x.Length.CompareTo(y.Length);
            if (lengthComparison == 0)
            {
                return x.CompareTo(y);
            }
            else
            {
                return lengthComparison;
            }
        }
    }
}
