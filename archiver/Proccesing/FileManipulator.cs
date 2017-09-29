using System;
using System.IO;
using System.Windows.Forms;

namespace archiver
{
    public static class FileManipulator
    {
        public static void WriteFile(string decodedText, string fileName)
        {
            var sw1 = new StreamWriter("./" + fileName);
            sw1.Write("{0}", decodedText);
            sw1.Close();
        }

        public static string ReadFile(string file)
        {
            StreamReader reader;
            if (!File.Exists(file))
            {
                MessageBox.Show("Error:\nФайл не существует");
                return string.Empty;
            }
            reader = new StreamReader(file);
        
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
