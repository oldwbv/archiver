using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using archiver.Proccesing;
using archiver.Tree;

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
            Archivate((int)numElementLen.Value);
        }

        private void btnSerial_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < numIterations.Value; i++)
            {
                var length = numElementLen.Value + numStep.Value * i;
                if (Archivate((int) length) == false)
                {
                    return;
                }
            }
        }

        private bool Archivate(int blockLength)
        {
            // 
            // bool positional = true;
            //  bool elementType = true;
            Session session = new Session();
            var dictionary = new List<string>();
            var encodedText = new List<string>();

            var encodedBuilder = new StringBuilder();
            var decodedText = string.Empty;
            var sourceText = FileManipulator.ReadFile(fileTextBox.Text);
            if (string.IsNullOrEmpty(sourceText))
            {
                return false;
            }

            //int blockLength = 1, step = 1;

           // var blockLength = (int)numElementLen.Value;
            int step = radioBlocks.Checked ? blockLength : 1;
            var splittedText = StringManipulator.SplitText(sourceText, step, blockLength, dictionary);

            // var encodedDictionary = positional ? SimpleCode.BuildCode(dictionary) : HaffmanCode.BuildCode(splittedText, dictionary);
            List<string> encodedDictionary = radioPositional.Checked ? SimpleCode.BuildCode(dictionary, session) : HaffmanCode.BuildCode(splittedText, dictionary, session);
            for (int m = 0; m < splittedText.Count; m++)
            {
                int index = dictionary.IndexOf(splittedText[m]);
                string stringCode = encodedDictionary[index];
                encodedText.Add(stringCode);
                encodedBuilder.Append(stringCode);
            }

            FileManipulator.WriteFile(encodedBuilder.ToString(), "encoded");

            int encodedLength = encodedBuilder.ToString().Length;

            int sourceLength = sourceText.Length;

            float compression = (float)sourceLength / encodedLength;
            //MessageBox.Show("Длина исходного текста=" + sourceLength.ToString() + "\nДлина закодированного текста=" + encodedLength.ToString() + "\nКоэфициент сжатия=" + compression.ToString());
            session.SourceLength = sourceLength;
            session.EncodedLength = encodedLength;
            
            // if (elementType)
            if (radioBlocks.Checked)
            {
                for (int k = 0; k < encodedText.Count; k++)
                {
                    int index = encodedDictionary.IndexOf(encodedText[k]);
                    decodedText += dictionary[index];
                }
                FileManipulator.WriteFile(decodedText, "decodedBlock");
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

                FileManipulator.WriteFile(decodedText, "decodedLGrum");
            }
            MessageBox.Show(session.SourceLength.ToString() + "\n"
                            + session.EncodedLength + "\n" + session.GetCompression() + "\n" + session.AverageWordLength);
            return true;
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
                listView.Columns.Add("Type", 80);
                listView.Columns.Add("Size", 100);
                listView.Columns.Add("Full path");
                listView.Columns[3].Width = 60;
                foreach (string Fname in Files)
                {
                    subinfo[0] = Path.GetFileName(Fname);
                    subinfo[1] = Path.GetExtension(Fname).Substring(1);
                    subinfo[2] = treeParams.GetSizeinfo(Fname);
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


        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            // if selected items more than 0 move it to textbox
            if (listView.SelectedItems.Count > 0)
            {
                fileTextBox.Text = listView.SelectedItems[0].SubItems[3].Text;
                //    fileTextBox.Text = listView.SelectedItems[0].SubItems[3].Text.Remove(2,1); // 0 - the only element in selected file collection, 3 - number of column with full path
            }
        }

        private void savePathStripMenuItem_Click(object sender, EventArgs e)
        {
            FileManipulator.path = saveFD.ShowDialog() == DialogResult.OK ? saveFD.FileName : string.Empty;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(saveFD.FileName);
        }
    }
}
