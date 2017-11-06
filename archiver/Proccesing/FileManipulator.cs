using System.IO;
using System.Windows.Forms;

namespace archiver.Proccesing
{
    public static class FileManipulator
    {
        public static string path  = "";
        public static void WriteFile(string text, string path)
        {
           /* var locPath = path;
            if (string.IsNullOrEmpty(locPath))
            {
                locPath = "./";
                locPath += typeFile;
            }
            else
            {
                var ind = locPath.LastIndexOf(".");
                if (ind < 0)
                {
                    locPath += typeFile;
                }
                else
                {
                    locPath = locPath.Insert(ind, typeFile);
                }
            }*/
            var sw1 = new StreamWriter(path);
            sw1.Write("{0}", text);
            sw1.Close();
        }

        public static string ReadFile(string file)
        {
            if (!File.Exists(file))
            {
                MessageBox.Show("Проверьте, существует ли указанный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Empty;
            }
            var reader = new StreamReader(file);
        
            var sourceText = string.Empty;
            var buffer = string.Empty;
            while ((buffer = reader.ReadLine()) != null)
            {
                sourceText += buffer;
            }
            return sourceText;
        }
    }
}
