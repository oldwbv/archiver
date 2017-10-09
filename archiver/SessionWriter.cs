using System;

namespace archiver
{
    public class SessionWriter
    {
        int[] averageLength;
        int[] sourceTextLength;
        int[] codedTextLength;
        int iterator = 0;
        public bool WriteAverageLength(int value, int iter)
        {
            if (iter != iterator) throw new ArgumentException("Iterator");
            averageLength[iter] = value;
            return true;
        }
        public bool WriteSourceTextLength(int value, int iter)
        {
            if (iter != iterator) throw new ArgumentException("Iterator");
            sourceTextLength[iter] = value;
            return true;
        }
        public bool WriteCodedTextLength(int value, int iter)
        {
            if (iter != iterator) throw new ArgumentException("Iterator");
            codedTextLength[iter] = value;
            iterator++;
            return true;
        }
    }
}
