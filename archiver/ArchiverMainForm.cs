using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using archiver.Huffman;
using archiver.MultiArchiving;
using archiver.Properties;
using archiver.TextProccesing;

namespace archiver
{
    public partial class ArchiverMainForm : Form
    {
        // Constructor
        public ArchiverMainForm()
        {
            InitializeComponent();
        }

        // event - on Form Load
        private void ArchiverMainForm_Load(object sender, EventArgs e)
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

        // event after select a node of the tree view
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
                    //костыль на форматы путей c:\path и c:\\path
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
            catch
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

        // event - On button click -  to start thread for single archivation
        private void btnToArc_Click(object sender, EventArgs e)
        {
            if (!CheckThreadExists())
                return;
            Thread one = new Thread(ArchivateOne);
            one.Start();
            threads.Add(one);
        }
        
        //Thread to start single archivation
        private void ArchivateOne()
        {
            Session session = new Session(
                (int)numElementLen.Value,
                radioPositional.Checked,
                radioBlocks.Checked);
            if (Archivate(session, 0) && (session.ElementLength >= 1))
            {
                MessageBox.Show( "Изначально " + session.SourceLength + "\n" 
                               + "Кодировано " + session.DestinationLength + "\n"
                               + "Размер метаданных " + session.InfoLength + "\n"
                               + "Компрессия " + session.GetCompression() + "\n"
                               + "Компрессия без учета метаданных " + session.GetPureCompression() + "\n"
                               + "Размер слова " + session.ElementLength + "\n"
                               + "Средний размер слова " + session.AverageElementLength + "\n"
                               + "Затрачено времени " + session.TimeSpent.ToString("g") + "\n"
                               +  session.GetCodingType() + " кодирование\n"
                               +  "тип слова " + session.GetElementType());
            }
        }

        // event - on button click - to start thread for seria
        private void btnSerial_Click(object sender, EventArgs e)
        {
            if (!CheckThreadExists())
            return;
            Thread seria = new Thread(ArchivateSeria);
            seria.Start(); 
            threads.Add(seria);  
        }

        //Thread to start serial archivation
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
                    var final = Archivate(session, i+1);
                    if (final == false || !(session.ElementLength > 0))
                    {
                        SetStatusMessage("Interrupted " + final + " " + session.ElementLength);
                        return;
                    }
                    rp.WriteExcel(session, i);
                    SetStatusMessage("Количество архиваций выполнено");
                    SetProgressValue((int) (100 * (i + 1) / numIterations.Value));

                }
                rp.Finish();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           
        }

        // when one of arc buttons clicked check probably still active threads 
        private bool CheckThreadExists()
        {
            for (int index = 0; index < threads.Count; index++)
            {
                var thread = threads[index];
                // if thread is finished - remove him from list 
                if (!thread.IsAlive)
                {
                    threads.Remove(thread);
                }
                else // ask
                {
                    var dr =
                        MessageBox.Show(
                            "Задача " + thread.ManagedThreadId + " уже запущена. Прервать (не рекомендуется) ?",
                            "Внимание",
                            MessageBoxButtons.YesNoCancel,
                            MessageBoxIcon.Question);
                    // if yes - interrupt thread then abort him and remove from list
                    if (dr == DialogResult.Yes)
                    {
                        thread.Abort();
                        threads.Remove(thread);
                    }
                    // if cancel - continue thread act, new thread will not be created
                    if (dr == DialogResult.Cancel)
                    {
                        return false;
                    }
                    //else couple of thread will act - not proceed in writing files yet
                }
            }
            return true;
        }

        //old sample arc + dearc
        private bool OldArchivate(Session session, int id)
        {
            var watch = new Stopwatch();
            watch.Start();

            var dictionary = new List<string>();
            var encodedText = new List<string>();
            var encodedBuilder = new StringBuilder();
            var decodedText = string.Empty;
            //READ file
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

            SetStatusMessage("Arc info building");

            encodedBuilder.Append("TYPE=arc17|");
            encodedBuilder.Append(session.IsPositional ? "1|" : "0|");
            encodedBuilder.Append(session.IsBlock ? '1' : '0');

            for (int i = 0; i < encodedDictionary.Count; i++)
            {
                encodedBuilder.Append("|" + dictionary[i] + "=" + encodedDictionary[i]);
            }
            session.InfoLength = encodedBuilder.Length;
            SetStatusMessage("Text encoding");
            for (int m = 0; m < splittedText.Count; m++)
            {
                int index = dictionary.IndexOf(splittedText[m]);
                string stringCode = encodedDictionary[index];
                encodedText.Add(stringCode);
                encodedBuilder.Append(stringCode);
            }

            SetStatusMessage("Encoded file writing");

            FileManipulator.WriteFile(
                encodedBuilder.ToString(),
                FileManipulator.DoFileName(openFD.FileName, id, ".arc17"),
                rewriteFilesToolStripMenuItem.Checked);
            session.SourceLength = sourceText.Length;
            session.DestinationLength = encodedBuilder.ToString().Length;

            SetStatusMessage("Decoding");

            // if (elementType is Blocks)
            if (radioBlocks.Checked)
            {
                for (int k = 0; k < encodedText.Count; k++)
                {
                    int index = encodedDictionary.IndexOf(encodedText[k]);
                    decodedText += dictionary[index];
                    SetProgressValue((int)(100 * k / session.SourceLength));
                }
                // fileout = Path.GetDirectoryName(openFD.FileName) + @"\decodedBlock" + openFD.SafeFileName;

                //fileout = id > 0 ? fileout.Replace(Path.GetFileNameWithoutExtension(fileout), Path.GetFileNameWithoutExtension(fileout) + id) : fileout;
                FileManipulator.WriteFile(decodedText,
                                           FileManipulator.DoFileName(openFD.FileName, id),
                                           rewriteFilesToolStripMenuItem.Checked);
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
                                           FileManipulator.DoFileName(openFD.FileName, id),
                                           rewriteFilesToolStripMenuItem.Checked);
            }

            SetStatusMessage("Finished");
            SetProgressValue(0);
            watch.Stop();
            session.TimeSpent = watch.Elapsed;
            return true;
        }

        // main function to archivate
        private bool Archivate(Session session, int id)
        {
            var watch = new Stopwatch();
            watch.Start();

            var dictionary = new List<string>();
            var encodedText = new List<string>();
            var encodedBuilder = new StringBuilder();
            var decodedText = string.Empty;
            //READ file
            var sourceText = FileManipulator.ReadFile(openFD.FileName);
            if (sourceText.Contains("|"))
            {
                if (MessageBox.Show(
                    text: "Файл содержит спец.символ '|', который приведет к потере данных при дешифровке.\nПродолжить?",
                    caption: "Внимание!",
                    buttons: MessageBoxButtons.YesNo,
                    icon: MessageBoxIcon.Error
                    ) == DialogResult.No)
                {
                    return false;
                }
            }
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
            
            SetStatusMessage("Arc info building");

            encodedBuilder.Append("TYPE=arc17|");
            encodedBuilder.Append(session.IsBlock ? "1|" : "0|");
            encodedBuilder.Append(session.ElementLength + "|");

            for (int i = 0; i < encodedDictionary.Count; i++)
            {
                encodedBuilder.Append(dictionary[i] + "=" + encodedDictionary[i] + "|");
            }
            session.InfoLength = encodedBuilder.Length;

            SetStatusMessage("Text encoding");
            for (int m = 0; m < splittedText.Count; m++)
            {
                int index = dictionary.IndexOf(splittedText[m]);
                string stringCode = encodedDictionary[index];
                encodedText.Add(stringCode);
                encodedBuilder.Append(stringCode);
            }

            SetStatusMessage("Encoded file writing");
            
            FileManipulator.WriteFile(
                encodedBuilder.ToString(), 
                FileManipulator.DoFileName(openFD.FileName, id, ".arc17"), 
                rewriteFilesToolStripMenuItem.Checked);
            session.SourceLength = sourceText.Length;
            session.DestinationLength = encodedBuilder.ToString().Length;
            
            SetStatusMessage("Decoding");
            /*
            // if (elementType is Blocks)
            if (radioBlocks.Checked)
            {
                for (int k = 0; k < encodedText.Count; k++)
                {
                    int index = encodedDictionary.IndexOf(encodedText[k]);
                    decodedText += dictionary[index];
                    SetProgressValue((int)(100 * k / session.SourceLength));
                }
                // fileout = Path.GetDirectoryName(openFD.FileName) + @"\decodedBlock" + openFD.SafeFileName;

                //fileout = id > 0 ? fileout.Replace(Path.GetFileNameWithoutExtension(fileout), Path.GetFileNameWithoutExtension(fileout) + id) : fileout;
                FileManipulator.WriteFile(decodedText,
                                           FileManipulator.DoFileName(openFD.FileName, id),
                                           rewriteFilesToolStripMenuItem.Checked);
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

                FileManipulator.WriteFile( decodedText,
                                           FileManipulator.DoFileName(openFD.FileName, id),
                                           rewriteFilesToolStripMenuItem.Checked);
            }
            */
            SetStatusMessage("Finished");
            SetProgressValue(0);
            watch.Stop();
            session.TimeSpent = watch.Elapsed;
            return true;
        }


        private void btnDeArchivate_Click(object sender, EventArgs e)
        {
            if (!CheckThreadExists())
                return;
            Thread deArc = new Thread(DeArchivate);
            deArc.Start();
            threads.Add(deArc);
        }

        private void DeArchivate()
        {
            var decodedText = new StringBuilder();
            var watch = new Stopwatch();
            watch.Start();
            var sourceText = FileManipulator.ReadFile(openFD.FileName);
            
            SetStatusMessage("Arc info reading");
            string[] str = sourceText.Split('|');
            Dictionary<string, string> dict = new Dictionary<string, string>();
            
            if (str[0].Contains("arc17"))
            {
                var isBlock = int.Parse(str[1]);
                var elementLen = int.Parse(str[2]);
                for (int i = 3; i < str.Length-1; i++)
                {
                    var pair = str[i].Split('=');
                    dict.Add(pair[0], pair[1]);
                }
                SetStatusMessage("Decoding");
                var encodedText = str[str.Length - 1];
                // if (elementType is Blocks)
                if (isBlock == 1)
                {
                    int k = 0;
                    //var newDict = dict.OrderBy(p => p.Value, new LengthComparer());
                    while ( k < encodedText.Length)
                    {        
                        foreach (var p in dict)
                        {
                            try
                            {

                                if (p.Value == encodedText.Substring(k, p.Value.Length))
                                {
                                    decodedText.Append(p.Key);
                                    k += p.Value.Length-1;
                                    break;
                                }
                            }
                            catch
                            {

                            }
                        }
                        k++;
                        SetProgressValue((int)(100 * k / encodedText.Length));
                    }
                    MessageBox.Show(decodedText.ToString());
                   
                    FileManipulator.WriteFile(decodedText.ToString(),
                                               FileManipulator.DoFileName(openFD.FileName, 0),
                                               rewriteFilesToolStripMenuItem.Checked);
                   // MessageBox.Show(FileManipulator.DoFileName(openFD.FileName, 0));
                }
                else //elementType is L-grams REWRITE DOESNT WORK
                {
                    /*
                    decodedText.Append(dictionary[encodedDictionary.IndexOf(encodedText[0]));
                    for (int i = 1; i < encodedText.Length; i++)
                    {
                        int index = encodedDictionary.IndexOf(encodedText[i].ToString());
                        string t = dictionary[index];
                        t = t.Substring(elementLen - 1, 1);
                        decodedText.Append(t);
                    
                    
                    FileManipulator.WriteFile(decodedText.ToString(),
                                               FileManipulator.DoFileName(openFD.FileName, 0),
                                               rewriteFilesToolStripMenuItem.Checked);*/
                }  
                SetStatusMessage("Finished");
            }
            else
            {
                SetStatusMessage("Ошибка чтения. Не определен формат");
            }
            SetProgressValue(0);
            watch.Stop();
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

        //event - copy choosen filename to data sources and views for future processing
        private void listView_MouseClick(object sender, MouseEventArgs e)
        {
            // if selected items more than 0 move it to textbox
            if (listView.SelectedItems.Count <= 0) return;
            openFD.FileName = openTextBox.Text = listView.SelectedItems[0].SubItems[3].Text;
            if (!saveAnyPathMenuItem.Checked) return;
            Settings.Default.openPath = openFD.FileName;
            Settings.Default.Save();
        }


        //inverse state menu (save previous path for files opened/saved before) and save it in file
        private void saveAnyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings.Default.saveAnyPath = saveAnyPathMenuItem.Checked = !saveAnyPathMenuItem.Checked;
            Settings.Default.openPath = (saveAnyPathMenuItem.Checked && openTextBox.Text.Length > 0) ? openTextBox.Text : Application.ExecutablePath;
            Settings.Default.savePath = (saveAnyPathMenuItem.Checked && saveTextBox.Text.Length > 0) ? openTextBox.Text : Application.ExecutablePath;
            Settings.Default.Save();
        }

        //to update the tree view, if some disks are added
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

        //Save path choose dialog open
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFD.ShowDialog() == DialogResult.OK)
            {
                saveTextBox.Text = saveFD.FileName;
            }
        }

        //after form closed
        private void ArchiverMainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //save params
            Settings.Default.openPath = (saveAnyPathMenuItem.Checked && openTextBox.Text.Length > 0) ? openTextBox.Text : Application.ExecutablePath;
            Settings.Default.savePath = (saveAnyPathMenuItem.Checked && saveTextBox.Text.Length > 0) ? openTextBox.Text : Application.ExecutablePath;
            Settings.Default.Save();
            KillThreads();
        }

        private void KillThreads()
        {
            // kill still working threads
            for (int index = 0; index < threads.Count; index++)
            {
                threads[index].Abort();
                threads.Clear();
            }
        }


        //inverse state menu (rewrite files if they exist / do not ask) and save it in file
        private void rewriteFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rewriteFilesToolStripMenuItem.Checked = !rewriteFilesToolStripMenuItem.Checked;
            Settings.Default.rewriteFiles = rewriteFilesToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private void killThreadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KillThreads();
        }
    }
   
}
