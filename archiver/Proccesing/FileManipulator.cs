using System.IO;
using System.Windows.Forms;

namespace archiver.Proccesing
{
    public static class FileManipulator
    {
        //записать
        public static void WriteFile(string text, string file)
        {
            if (!File.Exists(file))
            {
                DialogResult dr = MessageBox.Show(
                    "Перезаписать существующий файл",
                    "Файл существует",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error);
                if (dr == DialogResult.Yes)
                {
                    File.Delete(file);
                }   
                else
                {
                    return;
                }
            }

            using (var sw = new StreamWriter(file))
            {
                sw.Write("{0}", text);
                sw.Close();
            }
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
