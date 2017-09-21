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

        public static string ReadFile(OpenFileDialog openFD)
        {
            StreamReader reader;
            if (openFD.ShowDialog() == DialogResult.OK)
            {
                if (!File.Exists(openFD.FileName))
                {
                    throw new FileNotFoundException();
                }
                reader = new StreamReader(openFD.FileName);
            }
            else
            {
                return string.Empty;
            }
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
