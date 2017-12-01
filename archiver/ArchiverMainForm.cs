using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using archiver.Huffman;
using archiver.MultiArchiving;
using archiver.Properties;
using archiver.TextProccesing;

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
            rewriteFilesToolStripMenuItem.Checked = Settings.Default.rewriteFiles;
            saveAnyPathMenuItem.Checked = Settings.Default.saveAnyPath;
            openFD.InitialDirectory = openTextBox.Text = Path.GetDirectoryName(Settings.Default.openPath);
            saveFD.InitialDirectory = saveTextBox.Text = Path.GetDirectoryName(Settings.Default.savePath);
            treeFileView.TopNode.Expand();
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
                    subinfo[2] = TreeViewParams.GetSizeinfo(file);
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
            Thread one = new Thread(ArchivateOne);
            one.Start();
        }

        private void ArchivateOne()
        {
            Session session = new Session(
                (int)numElementLen.Value,
                radioPositional.Checked,
                radioBlocks.Checked);
            if (Archivate(session) && (session.ElementLength >= 1))
            {
                MessageBox.Show(session.SourceLength + "\n"
                                  + session.EncodedLength + "\n"
                                  + session.GetCompression() + "\n"
                                  + session.AverageElementLength);
            }
        }
        // event - on button click - to start archivation seria
        private void btnSerial_Click(object sender, EventArgs e)
        {
            Thread seria = new Thread(ArchivateSeria);
            seria.Start();   
        }

        private void ArchivateSeria()
        {

            if (!File.Exists(openFD.FileName))
            {
                MessageBox.Show("Проверьте, существует ли указанный файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                ReportExporter rp = new ReportExporter(
                    (int) numIterations.Value,
                    Path.GetFileNameWithoutExtension(openFD.FileName)
                    );
                for (int i = 0; i < numIterations.Value; i++)
                {
                    Session session = new Session(
                        (int) (numElementLen.Value + numStep.Value*i),
                        radioPositional.Checked,
                        radioBlocks.Checked);
                    var final = Archivate(session);
                    if (final == false || !(session.ElementLength > 0))
                    {
                        SetStatusMessage("Interrupted " + final + " " + session.ElementLength);
                        return;
                    }
                    rp.WriteExcel(session, i);
                    SetStatusMessage("Количество архиваций выполнено");
                    SetProgressValue((int) (100*(i + 1)/numIterations.Value));

                }
                rp.Finish();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
        }

        //Archivation process
        private bool Archivate(Session session)
        {
            var dictionary = new List<string>();
            var encodedText = new List<string>();
            var encodedBuilder = new StringBuilder();
            var decodedText = string.Empty;
            var sourceText = FileManipulator.ReadFile(openFD.FileName);
            if (string.IsNullOrEmpty(sourceText))
            {
                SetStatusMessage("Empty source");
                return false;
            }
            int step = radioBlocks.Checked ? session.ElementLength : 1;

            SetStatusMessage("Text spliting");
            var splittedText = StringManipulator.SplitText(sourceText, step, session.ElementLength, dictionary);

            SetStatusMessage("Dictionary building");
            List<string> encodedDictionary = radioPositional.Checked
                ? CodeSimplifier.BuildCode(dictionary, session)
                : HaffmanCode.BuildCode(splittedText, dictionary, session);
     
            SetStatusMessage("Text encoding");
            for (int m = 0; m < splittedText.Count; m++)
            {
                int index = dictionary.IndexOf(splittedText[m]);
                string stringCode = encodedDictionary[index];
                encodedText.Add(stringCode);
                encodedBuilder.Append(stringCode);
            }
            
            SetStatusMessage("Encoded file writing");
            FileManipulator.WriteFile(encodedBuilder.ToString(),
                Path.GetDirectoryName(openFD.FileName) + @"\encoded.ivt", rewriteFilesToolStripMenuItem.Checked);
            
            SetStatusMessage("Dictionary writing");
            var s = encodedDictionary.ToArray();
            var v = dictionary.ToArray();
            string end = "";
            for (int i = 0; i < encodedDictionary.Count; i++)
            {
                end +=  dictionary[i] + "-" + encodedDictionary[i] + "|";
            }
            FileManipulator.WriteFile(end,
                Path.GetDirectoryName(openFD.FileName) + @"\dictionary.txt", rewriteFilesToolStripMenuItem.Checked);

            session.SourceLength = sourceText.Length;
            session.EncodedLength = encodedBuilder.ToString().Length;
            
            SetStatusMessage("Decoding");

            // if (elementType is Blocks)
            if (radioBlocks.Checked)
            {
                for (int k = 0; k < encodedText.Count; k++)
                {
                    int index = encodedDictionary.IndexOf(encodedText[k]);
                    decodedText += dictionary[index];
                    //SeriaStripProgressBar.Value = (int) (100 * k / session.EncodedLength);
                    SetProgressValue((int)(100 * k / session.SourceLength));
                }
                FileManipulator.WriteFile(decodedText,
                    Path.GetDirectoryName(openFD.FileName) + @"\decodedBlock" + openFD.SafeFileName, rewriteFilesToolStripMenuItem.Checked);
            }
            else //elementType is L-grams
            {
                decodedText += dictionary[encodedDictionary.IndexOf(encodedText[0])];
                for (int i = 1; i < encodedText.Count; i++)
                {
                    int index = encodedDictionary.IndexOf(encodedText[i]);
                    string t = dictionary[index];
                    t = t.Substring(session.ElementLength - 1, 1);
                    decodedText += t;
                }

                FileManipulator.WriteFile(decodedText,
                    Path.GetDirectoryName(openFD.FileName) + @"\decodedLGrum" + openFD.SafeFileName, rewriteFilesToolStripMenuItem.Checked);
            }
            SetStatusMessage("Finished");
            SetProgressValue(0);
            return true;
        }

        // DELEGATING WinControl methods for a thread call catching
        delegate void StatusMessageCallback(string text);
        delegate void StatusProgressCallback(int value);
        private void SetStatusMessage(string text)
        {
            if (statusStrip.InvokeRequired)
            {
                StatusMessageCallback d = new StatusMessageCallback(SetStatusMessage);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                MessageStripStatusLabel.Text = text;
            }
        }

        private void SetProgressValue(int value)
        {

            if (statusStrip.InvokeRequired)
            {
                StatusProgressCallback p = new StatusProgressCallback(SetProgressValue);
                this.Invoke(p, new object[] { value });
            }
            else
            {
                SeriaStripProgressBar.Value = value;
            }
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

        private void saveAnyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.saveAnyPath = saveAnyPathMenuItem.Checked = !saveAnyPathMenuItem.Checked;
            Settings.Default.openPath = (saveAnyPathMenuItem.Checked && openTextBox.Text.Length > 0) ? openTextBox.Text : Application.ExecutablePath;
            Settings.Default.savePath = (saveAnyPathMenuItem.Checked && saveTextBox.Text.Length > 0) ? openTextBox.Text : Application.ExecutablePath;
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

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFD.ShowDialog() == DialogResult.OK)
            {
                saveTextBox.Text = saveFD.FileName;
            }
        }

        private void ArchiverMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.openPath = (saveAnyPathMenuItem.Checked && openTextBox.Text.Length > 0) ? openTextBox.Text : Application.ExecutablePath;
            Settings.Default.savePath = (saveAnyPathMenuItem.Checked && saveTextBox.Text.Length > 0) ? openTextBox.Text : Application.ExecutablePath;
            Settings.Default.Save();
        }

        private void btnDeArchivate_Click(object sender, EventArgs e)
        {

        }

        private void rewriteFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rewriteFilesToolStripMenuItem.Checked = !rewriteFilesToolStripMenuItem.Checked;
            Settings.Default.rewriteFiles = rewriteFilesToolStripMenuItem.Checked;
            Settings.Default.Save();
        }
    }
}
