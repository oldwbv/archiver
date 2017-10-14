namespace archiver
{
    public class Session
    {
        public int SourceLength { get; set; } = 0;

        public int EncodedLength { get; set; } = 0;

        public double AverageWordLength { get; set; } = 0;
        
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
