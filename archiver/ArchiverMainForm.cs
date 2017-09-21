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
            FillDriveNodes();
        }
        void treeFileView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;
            try
            {
                if (Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    if (dirs.Length != 0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                            FillTreeNode(dirNode, dirs[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        // событие перед выделением узла
        void treeFileView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            string[] dirs;
            try
            {
                if (Directory.Exists(e.Node.FullPath))
                {
                    dirs = Directory.GetDirectories(e.Node.FullPath);
                    if (dirs.Length != 0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            TreeNode dirNode = new TreeNode(new DirectoryInfo(dirs[i]).Name);
                            FillTreeNode(dirNode, dirs[i]);
                            e.Node.Nodes.Add(dirNode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void Error()
        {
            MessageBox.Show("Что-то случилось нехорошее");
        }

        // получаем все диски на компьютере
        private void FillDriveNodes()
        {
            try
            {
                foreach (DriveInfo drive in DriveInfo.GetDrives())
                {
                    TreeNode driveNode = new TreeNode {Text = drive.Name};
                    FillTreeNode(driveNode, drive.Name);
                    treeFileView.Nodes.Add(driveNode);
                }
            }
            catch (Exception ex)
            {
            }
        }

        // получаем дочерние узлы для определенного узла
        private void FillTreeNode(TreeNode driveNode, string path)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    TreeNode dirNode = new TreeNode();
                    dirNode.Text = dir.Remove(0, dir.LastIndexOf("\\") + 1);
                    driveNode.Nodes.Add(dirNode);
                }
            }
            catch (Exception ex)
            {
            }
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
            openFD.ShowDialog();
        }

        private void btnToArc_Click(object sender, EventArgs e)
        {
            // 
            bool positional = true;
            bool elementType = true;

            var dictionary = new List<string>();
            var encodedText = new List<string>();

            var encodedBuilder = new StringBuilder();
            var decodedText = string.Empty;
            var sourceText = FileManipulator.ReadFile(openFD);
            if (string.IsNullOrEmpty(sourceText))
            {
                return;
            }
           // var blockLength = (int)oneLength.Value;
           //int step = radioBlocks.Checked ? blockLength : 1;
            int blockLength = 1,step = 1;
            var splittedText = StringManipulator.SplitText(sourceText, step, blockLength, dictionary);

           // var encodedDictionary = radioPositional.Checked ? SimpleCode.BuildCode(dictionary) : HaffmanCode.BuildCode(splittedText, dictionary);
            var encodedDictionary = positional ? SimpleCode.BuildCode(dictionary) : HaffmanCode.BuildCode(splittedText, dictionary);
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

          //  if (radioBlocks.Checked)
            if (elementType)
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
    }
}
