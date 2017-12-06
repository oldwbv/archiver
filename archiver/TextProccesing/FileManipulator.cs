using System.IO;
using System.Windows.Forms;

namespace archiver.TextProccesing
{
    // class to read/write a file
    public static class FileManipulator
    {
        public static void WriteFile(string text, string file, bool rewrite)
        {
            if (File.Exists(file))
            {
                if (rewrite == true)
                {
                    File.Delete(file);
                }
                else
                {
                    DialogResult dr = MessageBox.Show(
                       "Перезаписать существующий файл",
                       "Файл существует",
                       MessageBoxButtons.YesNoCancel,
                       MessageBoxIcon.Error);
                    switch (dr)
                    {
                        case DialogResult.Yes:
                            File.Delete(file);
                            break;
                        case DialogResult.No:
                            file = file.Replace(oldValue: Path.GetFileNameWithoutExtension(file), newValue: "(new)" + Path.GetFileNameWithoutExtension(file)  );
                            break;
                        case DialogResult.Cancel:
                            return;
                    }
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
                MessageBox.Show(
                    text: "Проверьте, существует ли указанный файл", 
                    caption: "Ошибка", 
                    buttons: MessageBoxButtons.OK,
                    icon: MessageBoxIcon.Error);
                return string.Empty;
            }
            var reader = new StreamReader(file);
        
            var sourceText = string.Empty;
            var buffer = string.Empty;
            while ((buffer = reader.ReadLine()) != null)
            {
                sourceText += buffer + "\n";
            }
            return sourceText;
        }

        public static string DoFileName(string filename, int id, string extension)
        {
            filename = id > 0 
                ? filename.Replace(Path.GetFileNameWithoutExtension(filename),
                                   Path.GetFileNameWithoutExtension(filename)+ id.ToString())
                : filename;
            return filename + ".arc17";
        }
        public static string DoFileName(string filename, int id)
        {
            var file = filename;
            var extension = Path.GetExtension(file);
            if (extension != null && extension.ToLower() == ".arc17")
            {
                file = Path.GetDirectoryName(file) + @"\" + Path.GetFileNameWithoutExtension(file);
            }
            file = id > 0
                ? file.Replace(Path.GetFileNameWithoutExtension(file),
                                   Path.GetFileNameWithoutExtension(file)
                                   + id.ToString())
                : file;
            return file;
        }
    }
}
