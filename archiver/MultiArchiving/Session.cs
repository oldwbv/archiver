using System;

namespace archiver.MultiArchiving
{
    public class Session
    {
        const string Optimal = "оптимальное", Positional = "позиционное", Block = "блок", LGram = "L-грамм";
        public readonly bool IsPositional;
        public readonly bool IsBlock;

        public Session(int elementLength, bool isPositional, bool isBlock)
        {
            ElementLength = elementLength;
            IsPositional = isPositional;
            IsBlock = isBlock;
        }
  
        // element length
        public int ElementLength { get; } = 0;

        // average length of element
        public double AverageElementLength { get; set; } = 0;

        // source text length
        public int SourceLength { get; set; } = 0;

        // (meta + encoded) end text length
        public int DestinationLength { get; set; } = 0;

        //meta data: arc description + dictionary length 
        public int InfoLength { get; set; } = 0;

        //time of processing
        public TimeSpan TimeSpent { get; set; } = new TimeSpan(0);
        

        // get type of encoding
        public string GetCodingType()
        {
            return IsPositional ? Positional : Optimal;
        }

        // Get type of element
        public string GetElementType()
        {
            return IsBlock ? Block : LGram;
        }

        // ratio of source text len to end text len 
        public double GetCompression()
        {
            if (DestinationLength != 0)
            {
                return SourceLength/(double) DestinationLength;
            }
            return 0;
        }

        // ratio of source text len to encoded text len
        public double GetPureCompression()
        {
            var pureEncoded = DestinationLength - InfoLength;
            if (pureEncoded != 0)
            {
                return SourceLength / (double)pureEncoded;
            }
            return 0;
        }

    }
}
