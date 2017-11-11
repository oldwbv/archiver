using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using archiver.Huffman;
using archiver.Proccesing;
using archiver.Properties;
using archiver.TreeView;

namespace archiver
{
    public partial class ArchiverMainForm : Form
    {
        /// Constructor
        public ArchiverMainForm()
        {
            InitializeComponent();
        }

        // On Form Load
        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateDriveTreeView(this, null);
            saveAnyPathMenuItem.Checked = Settings.Default.saveAnyPath;
            openFD.InitialDirectory = openTextBox.Text = Path.GetDirectoryName(Settings.Default.openPath);
           /*string[] path = openTextBox.Text.Split('\\');
            MessageBox.Show(path.Length.ToString());
            for (var i = 1; i < path.Length; i++)
            {
                TreeNode[] nodes = treeFileView.Nodes.Find(path[i], false);
                MessageBox.Show(nodes.Length.ToString());
                nodes[0].Expand();
            }*/
            treeFileView.TopNode.Expand();
            saveFD.InitialDirectory = saveTextBox.Text = Path.GetDirectoryName(Settings.Default.savePath);
           

            // 
        }

        // event - before expanding the tree view
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
                MessageStripStatusLabel.Text = ex.Message;
            }
        }


        private void treeFileView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                TreeNode current = e.Node;
                string path = current.FullPath;
                IEnumerable<string> files = Directory.EnumerateFiles(path);
                //string[] Directories = Directory.GetDirectories(path);
                string[] subinfo = new string[4];
                listView.Clear();
                listView.Columns.Add("Name", 255);
                listView.Columns.Add("Type", 80);
                listView.Columns.Add("Size", 100);
                listView.Columns.Add("Full path");
                listView.Columns[3].Width = 60;
                foreach (string fName in files)
                {
                    var file = fName.Contains(@"\\") ? fName.Replace(@"\\", @"\") : fName;
                    subinfo[0] = Path.GetFileName(file);
                    subinfo[1] = Path.GetExtension(file).StartsWith(".")
                        ? Path.GetExtension(file).Substring(1).ToUpper()
                        : "";
                    subinfo[2] = treeParams.GetSizeinfo(file);
                    subinfo[3] = file;
                    ListViewItem fItems = new ListViewItem(subinfo);
                    listView.Items.Add(fItems);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /*
        private void Error()
        {
            MessageBox.Show("Что-то случилось нехорошее");
        }*/


        // event - on the exit menu item click;
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите выйти из программы?", "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // event - System File Dialog Open
        private void openFileMenuItem_Click(object sender, EventArgs e)
        {
            if (openFD.ShowDialog() == DialogResult.OK)
            {
                openTextBox.Text = openFD.FileName;
            }
        }

        // event - On button click - To start Archivation
        private void btnToArc_Click(object sender, EventArgs e)
        {
            Archivate((int)numElementLen.Value);
        }

        // event - on button click - to start archivation seria
        private void btnSerial_Click(object sender, EventArgs e)
        {
            ReportExporter rp = new ReportExporter((int)numIterations.Value);
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
            // bool positional = true;
            //  bool elementType = true;
            Session session = new Session();
            var dictionary = new List<string>();
            var encodedText = new List<string>();

            var encodedBuilder = new StringBuilder();
            var decodedText = string.Empty;
            var sourceText = FileManipulator.ReadFile(openFD.FileName);
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

            FileManipulator.WriteFile(encodedBuilder.ToString(), Path.GetDirectoryName(openFD.FileName) + @"\encoded.ivt");

            int encodedLength = encodedBuilder.ToString().Length;

            int sourceLength = sourceText.Length;

            float compression = (float)sourceLength / encodedLength;
            //MessageBox.Show("Длина исходного текста=" + sourceLength.ToString() + "\nДлина закодированного текста=" + encodedLength.ToString() + "\nКоэфициент сжатия=" + compression.ToString());
            session.SourceLength = sourceLength;
            session.EncodedLength = encodedLength;
            
            // if (elementType is Blocks)
            if (radioBlocks.Checked)
            {
                for (int k = 0; k < encodedText.Count; k++)
                {
                    int index = encodedDictionary.IndexOf(encodedText[k]);
                    decodedText += dictionary[index];
                }
                FileManipulator.WriteFile(decodedText, Path.GetDirectoryName(openFD.FileName) + @"\decodedBlock" + openFD.SafeFileName);
            }
            else //elementType is L-grams
            {
                decodedText += dictionary[encodedDictionary.IndexOf(encodedText[0])];
                for (int i = 1; i < encodedText.Count; i++)
                {
                    int index = encodedDictionary.IndexOf(encodedText[i]);
                    string t = dictionary[index];
                    t = t.Substring(blockLength - 1, 1);
                    decodedText += t;
                }

                FileManipulator.WriteFile(decodedText, Path.GetDirectoryName(openFD.FileName) + @"\decodedLGrum" + openFD.SafeFileName);
            }
            MessageBox.Show(session.SourceLength.ToString() + "\n"
                            + session.EncodedLength + "\n" + session.GetCompression() + "\n" + session.AverageWordLength);
            return true;
        }


        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            // if selected items more than 0 move it to textbox
            if (listView.SelectedItems.Count <= 0) return;
            openFD.FileName = openTextBox.Text = listView.SelectedItems[0].SubItems[3].Text;
            if (!saveAnyPathMenuItem.Checked) return;
            Settings.Default.openPath = openFD.FileName; 
            Settings.Default.Save();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(saveFD.FileName);
        }

        private void saveAnyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.saveAnyPath = saveAnyPathMenuItem.Checked = !saveAnyPathMenuItem.Checked;
            Settings.Default.openPath = (saveAnyPathMenuItem.Checked && openTextBox.Text.Length > 0) ? openTextBox.Text : "";
            Settings.Default.savePath = (saveAnyPathMenuItem.Checked && saveTextBox.Text.Length > 0) ? openTextBox.Text : "";
            Settings.Default.Save();
        }

        private void UpdateDriveTreeView(object sender, EventArgs e)
        {
            treeFileView.Nodes.Clear();
            foreach (DriveInfo drv in DriveInfo.GetDrives())
            {
                if (drv.IsReady)
                {
                    TreeNode tn = new TreeNode { Text = drv.Name };
                    tn.Nodes.Add("");
                    treeFileView.Nodes.Add(tn);
                }
            }
            if (treeFileView.TopNode.NextNode == null)
            {
                treeFileView.TopNode.Expand();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportExporter ex = new ReportExporter();
        }
    }
}
