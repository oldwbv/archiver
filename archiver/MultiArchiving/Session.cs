namespace archiver.MultiArchiving
{
    public class Session
    {
        const string Optimal = "оптимальный", Positional = "позиционный", Block = "Блок", LGram = "L-грамм";
 
        readonly bool _isPositional;
        readonly bool _isBlock;

        public Session(int elementLength, bool isPositional, bool isBlock)
        {
            ElementLength = elementLength;
            _isPositional = isPositional;
            _isBlock = isBlock;
        }
  

        public int ElementLength { get; } = 0;     

        public int SourceLength { get; set; } = 0;
   
        public int EncodedLength { get; set; } = 0;

        public double AverageElementLength { get; set; } = 0;

        public string GetCodingType()
        {
            return _isPositional ? Optimal : Positional;
        }
        public string GetElementType()
        {
            return _isBlock ? Block : LGram;
        }
        public double GetCompression()
        {
            if (EncodedLength != 0)
            {
                return SourceLength/(double) EncodedLength;
            }
            return 0;
        }

    }
}
