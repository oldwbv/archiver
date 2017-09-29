using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace archiver
{
    public partial class ArchiverMainForm : Form
    {
        public ArchiverMainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (DriveInfo drv in DriveInfo.GetDrives())
            {
                if (drv.IsReady)
                {
                    TreeNode t2 = new TreeNode();
                    t2.Text = drv.Name;
                    t2.Nodes.Add("");
                    treeFileView.Nodes.Add(t2);
                }
            }
        }
        void treeFileView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                TreeNode parentnode = e.Node;
                DirectoryInfo dr = new DirectoryInfo(parentnode.FullPath);
                parentnode.Nodes.Clear();
                foreach (DirectoryInfo dir in dr.GetDirectories())
                {
                    TreeNode node = new TreeNode();
                    node.Text = dir.Name;
                    node.Nodes.Add("");
                    parentnode.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!!");
            }
        }
     
        private void Error()
        {
            MessageBox.Show("Что-то случилось нехорошее");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти из программы?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void openFile_Click(object sender, EventArgs e)
        {
            if (openFD.ShowDialog() == DialogResult.OK)
            {
                fileTextBox.Text = openFD.FileName;
            }
        }

        private void btnToArc_Click(object sender, EventArgs e)
        {
            // 
           // bool positional = true;
          //  bool elementType = true;

            var dictionary = new List<string>();
            var encodedText = new List<string>();

            var encodedBuilder = new StringBuilder();
            var decodedText = string.Empty;
            var sourceText = FileManipulator.ReadFile(fileTextBox.Text);
            if (string.IsNullOrEmpty(sourceText))
            {
                return;
            }

            //int blockLength = 1, step = 1;
            
             var blockLength = (int)elementLength.Value;
             int step = radioBlocks.Checked ? blockLength : 1;
             var splittedText = StringManipulator.SplitText(sourceText, step, blockLength, dictionary);

            // var encodedDictionary = positional ? SimpleCode.BuildCode(dictionary) : HaffmanCode.BuildCode(splittedText, dictionary);
            var encodedDictionary = radioPositional.Checked ? SimpleCode.BuildCode(dictionary) : HaffmanCode.BuildCode(splittedText, dictionary);
           
            for (int m = 0; m < splittedText.Count; m++)
            {
                int index = dictionary.IndexOf(splittedText[m]);
                string stringCode = encodedDictionary[index];
                encodedText.Add(stringCode);
                encodedBuilder.Append(stringCode);
            }

            FileManipulator.WriteFile(encodedBuilder.ToString(), "encoded.txt");

            int encodedLength = encodedBuilder.ToString().Length;

            int sourceLength = sourceText.Length;

            float compression = (float)sourceLength / encodedLength;
            MessageBox.Show("Длина исходного текста=" + sourceLength.ToString() + "\nДлина закодированного текста=" + encodedLength.ToString() + "\nКоэфициент сжатия=" + compression.ToString());
            // if (elementType)
            if (radioBlocks.Checked)
            {
                for (int k = 0; k < encodedText.Count; k++)
                {
                    int index = encodedDictionary.IndexOf(encodedText[k]);
                    decodedText += dictionary[index];
                }
                FileManipulator.WriteFile(decodedText, "decodedBlock.txt");
            }
            else //L-grums
            {
                decodedText += dictionary[encodedDictionary.IndexOf(encodedText[0])];
                for (int i = 1; i < encodedText.Count; i++)
                {
                    int index = encodedDictionary.IndexOf(encodedText[i]);
                    string t = dictionary[index];
                    t = t.Substring(blockLength - 1, 1);
                    decodedText += t;
                }

                FileManipulator.WriteFile(decodedText, "decodedLGramm.txt");
            }
        }

        private void treeFileView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode current = e.Node;
                string path = current.FullPath;
                string[] Files = Directory.GetFiles(path);
                //string[] Directories = Directory.GetDirectories(path);
                string[] subinfo = new string[4];
                listView.Clear();
                listView.Columns.Add("Name", 255);
                listView.Columns.Add("Size", 100);
                listView.Columns.Add("Type", 80);
                listView.Columns.Add("Full path");
                listView.Columns[3].Width = 60;
                foreach (string Fname in Files)
                {
                    subinfo[0] = GetName(Fname);
                    subinfo[1] = GetSizeinfo(Fname);
                    subinfo[2] = GetTypeinfo(Fname);
                    subinfo[3] = Fname;
                    ListViewItem FItems = new ListViewItem(subinfo);
                    listView.Items.Add(FItems);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: ");
            }
        }
        public string GetName(string path)
        {
            int Nameindex = path.LastIndexOf('\\');
            return path.Substring(Nameindex + 1);
        }
        public string GetTypeinfo(string path)
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
        public string GetSizeinfo(string path)
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

        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                fileTextBox.Text = listView.SelectedItems[0].SubItems[3].Text.Remove(3,1); // 0 - the only element in selected file collection, 3 - number of column with full path
            }
        }
    }
}
