using System.IO;

namespace archiver
{
    static class TreeViewParams
    {
        public static string GetName(string path)
        {
            int Nameindex = path.LastIndexOf('\\');
            return path.Substring(Nameindex + 1);
        }
        public static string GetTypeinfo(string path)
        {
            int Typeindex = path.LastIndexOf('.');
            string FType;
            if (Typeindex != -1)
            {
                FType = path.Substring(Typeindex + 1);
                FType = FType.ToUpper();
                return FType;
            }
            else
            {
                FType = "FILE";
                return FType;
            }
        }
        public static string GetSizeinfo(string path)
        {
            FileInfo fi = new FileInfo(path);
            long size = fi.Length;
            string txtsize = "";
            if (size < 1024)
            {
                txtsize = "byte";
            }
            else if (size > 1024)
            {
                size = size / 1024;
                txtsize = "Kb";
            }
            if (size > 1024)
            {
                size = size / 1024;
                txtsize = "Mb";
            }
            if (size > 1024)
            {
                size = size / 1024;
                txtsize = "Gb";
            }
            return size + " " + txtsize;
        }
    }
}
