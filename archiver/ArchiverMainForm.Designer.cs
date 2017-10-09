namespace archiver
{
    partial class ArchiverMainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePathStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.archivationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dearchivationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDeArchivate = new System.Windows.Forms.Button();
            this.btnSerial = new System.Windows.Forms.Button();
            this.btnArchivate = new System.Windows.Forms.Button();
            this.labelFile = new System.Windows.Forms.Label();
            this.openFD = new System.Windows.Forms.OpenFileDialog();
            this.treeFileView = new System.Windows.Forms.TreeView();
            this.listView = new System.Windows.Forms.ListView();
            this.toolMenu = new System.Windows.Forms.ToolStrip();
            this.groupWay = new System.Windows.Forms.GroupBox();
            this.radioOptimal = new System.Windows.Forms.RadioButton();
            this.radioPositional = new System.Windows.Forms.RadioButton();
            this.groupDividing = new System.Windows.Forms.GroupBox();
            this.radioLGrums = new System.Windows.Forms.RadioButton();
            this.radioBlocks = new System.Windows.Forms.RadioButton();
            this.groupSize = new System.Windows.Forms.GroupBox();
            this.numElementLen = new System.Windows.Forms.NumericUpDown();
            this.numStep = new System.Windows.Forms.NumericUpDown();
            this.numIterations = new System.Windows.Forms.NumericUpDown();
            this.labelStep = new System.Windows.Forms.Label();
            this.labeIterations = new System.Windows.Forms.Label();
            this.saveFD = new System.Windows.Forms.SaveFileDialog();
            this.mainMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupWay.SuspendLayout();
            this.groupDividing.SuspendLayout();
            this.groupSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numElementLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(766, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.savePathStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator1,
            this.archivationToolStripMenuItem,
            this.dearchivationToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.openToolStripMenuItem.Text = "Открыть";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openFile_Click);
            // 
            // savePathStripMenuItem
            // 
            this.savePathStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem});
            this.savePathStripMenuItem.Name = "savePathStripMenuItem";
            this.savePathStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.savePathStripMenuItem.Text = "Сохранять как";
            this.savePathStripMenuItem.Click += new System.EventHandler(this.savePathStripMenuItem_Click);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.saveToolStripMenuItem.Text = "Просмотр";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
            // 
            // archivationToolStripMenuItem
            // 
            this.archivationToolStripMenuItem.Name = "archivationToolStripMenuItem";
            this.archivationToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.archivationToolStripMenuItem.Text = "Архивация";
            // 
            // dearchivationToolStripMenuItem
            // 
            this.dearchivationToolStripMenuItem.Name = "dearchivationToolStripMenuItem";
            this.dearchivationToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.dearchivationToolStripMenuItem.Text = "Разархивация";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(148, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.exitToolStripMenuItem.Text = "Выйти";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.helpToolStripMenuItem.Text = "Помощь";
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.manualToolStripMenuItem.Text = "Справка";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.aboutToolStripMenuItem.Text = "О версии";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.fileTextBox);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.labelFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 334);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(766, 48);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "SavePath:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox1.Location = new System.Drawing.Point(64, 25);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(382, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TabStop = false;
            // 
            // fileTextBox
            // 
            this.fileTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.fileTextBox.Location = new System.Drawing.Point(64, 2);
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.ReadOnly = true;
            this.fileTextBox.Size = new System.Drawing.Size(382, 20);
            this.fileTextBox.TabIndex = 1;
            this.fileTextBox.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.btnDeArchivate);
            this.panel2.Controls.Add(this.btnSerial);
            this.panel2.Controls.Add(this.btnArchivate);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(537, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(229, 48);
            this.panel2.TabIndex = 0;
            // 
            // btnDeArchivate
            // 
            this.btnDeArchivate.Location = new System.Drawing.Point(3, 23);
            this.btnDeArchivate.Name = "btnDeArchivate";
            this.btnDeArchivate.Size = new System.Drawing.Size(110, 22);
            this.btnDeArchivate.TabIndex = 3;
            this.btnDeArchivate.Text = "Разархивировать";
            this.btnDeArchivate.UseVisualStyleBackColor = true;
            // 
            // btnSerial
            // 
            this.btnSerial.Location = new System.Drawing.Point(116, 0);
            this.btnSerial.Name = "btnSerial";
            this.btnSerial.Size = new System.Drawing.Size(110, 22);
            this.btnSerial.TabIndex = 2;
            this.btnSerial.Text = "Серия";
            this.btnSerial.UseVisualStyleBackColor = true;
            this.btnSerial.Click += new System.EventHandler(this.btnSerial_Click);
            // 
            // btnArchivate
            // 
            this.btnArchivate.Location = new System.Drawing.Point(3, 0);
            this.btnArchivate.Name = "btnArchivate";
            this.btnArchivate.Size = new System.Drawing.Size(110, 22);
            this.btnArchivate.TabIndex = 1;
            this.btnArchivate.Text = "Архивировать";
            this.btnArchivate.UseVisualStyleBackColor = true;
            this.btnArchivate.Click += new System.EventHandler(this.btnToArc_Click);
            // 
            // labelFile
            // 
            this.labelFile.Location = new System.Drawing.Point(4, 0);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(57, 23);
            this.labelFile.TabIndex = 0;
            this.labelFile.Text = "FilePath:";
            this.labelFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // openFD
            // 
            this.openFD.Filter = "Все файлы|*.*|ivt архивы|*.ivt";
            this.openFD.RestoreDirectory = true;
            this.openFD.SupportMultiDottedExtensions = true;
            // 
            // treeFileView
            // 
            this.treeFileView.Dock = System.Windows.Forms.DockStyle.Left;
            this.treeFileView.Location = new System.Drawing.Point(0, 63);
            this.treeFileView.Name = "treeFileView";
            this.treeFileView.Size = new System.Drawing.Size(267, 271);
            this.treeFileView.TabIndex = 2;
            this.treeFileView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeFileView_BeforeExpand);
            this.treeFileView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeFileView_AfterSelect);
            // 
            // listView
            // 
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Location = new System.Drawing.Point(267, 63);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(499, 271);
            this.listView.TabIndex = 4;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseClick);
            // 
            // toolMenu
            // 
            this.toolMenu.AutoSize = false;
            this.toolMenu.Location = new System.Drawing.Point(0, 24);
            this.toolMenu.Name = "toolMenu";
            this.toolMenu.Size = new System.Drawing.Size(766, 39);
            this.toolMenu.TabIndex = 5;
            this.toolMenu.Text = "toolStrip1";
            // 
            // groupWay
            // 
            this.groupWay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupWay.Controls.Add(this.radioOptimal);
            this.groupWay.Controls.Add(this.radioPositional);
            this.groupWay.Location = new System.Drawing.Point(7, 24);
            this.groupWay.Name = "groupWay";
            this.groupWay.Size = new System.Drawing.Size(208, 36);
            this.groupWay.TabIndex = 6;
            this.groupWay.TabStop = false;
            this.groupWay.Text = "Способ кодирования";
            // 
            // radioOptimal
            // 
            this.radioOptimal.AutoSize = true;
            this.radioOptimal.Location = new System.Drawing.Point(105, 16);
            this.radioOptimal.Name = "radioOptimal";
            this.radioOptimal.Size = new System.Drawing.Size(96, 17);
            this.radioOptimal.TabIndex = 1;
            this.radioOptimal.Text = "Оптимальный";
            this.radioOptimal.UseVisualStyleBackColor = true;
            // 
            // radioPositional
            // 
            this.radioPositional.AutoSize = true;
            this.radioPositional.Checked = true;
            this.radioPositional.Location = new System.Drawing.Point(6, 16);
            this.radioPositional.Name = "radioPositional";
            this.radioPositional.Size = new System.Drawing.Size(95, 17);
            this.radioPositional.TabIndex = 0;
            this.radioPositional.TabStop = true;
            this.radioPositional.Text = "Позиционный";
            this.radioPositional.UseVisualStyleBackColor = true;
            // 
            // groupDividing
            // 
            this.groupDividing.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupDividing.Controls.Add(this.radioLGrums);
            this.groupDividing.Controls.Add(this.radioBlocks);
            this.groupDividing.Location = new System.Drawing.Point(221, 24);
            this.groupDividing.Name = "groupDividing";
            this.groupDividing.Size = new System.Drawing.Size(145, 36);
            this.groupDividing.TabIndex = 7;
            this.groupDividing.TabStop = false;
            this.groupDividing.Text = "Способ разбиения";
            // 
            // radioLGrums
            // 
            this.radioLGrums.AutoSize = true;
            this.radioLGrums.Location = new System.Drawing.Point(68, 16);
            this.radioLGrums.Name = "radioLGrums";
            this.radioLGrums.Size = new System.Drawing.Size(75, 17);
            this.radioLGrums.TabIndex = 1;
            this.radioLGrums.Text = "L-граммы";
            this.radioLGrums.UseVisualStyleBackColor = true;
            // 
            // radioBlocks
            // 
            this.radioBlocks.AutoSize = true;
            this.radioBlocks.Checked = true;
            this.radioBlocks.Location = new System.Drawing.Point(6, 16);
            this.radioBlocks.Name = "radioBlocks";
            this.radioBlocks.Size = new System.Drawing.Size(56, 17);
            this.radioBlocks.TabIndex = 0;
            this.radioBlocks.TabStop = true;
            this.radioBlocks.Text = "Блоки";
            this.radioBlocks.UseVisualStyleBackColor = true;
            // 
            // groupSize
            // 
            this.groupSize.Controls.Add(this.numElementLen);
            this.groupSize.Location = new System.Drawing.Point(373, 24);
            this.groupSize.Name = "groupSize";
            this.groupSize.Size = new System.Drawing.Size(118, 36);
            this.groupSize.TabIndex = 8;
            this.groupSize.TabStop = false;
            this.groupSize.Text = "Размер элемента";
            // 
            // numElementLen
            // 
            this.numElementLen.Location = new System.Drawing.Point(6, 13);
            this.numElementLen.Name = "numElementLen";
            this.numElementLen.Size = new System.Drawing.Size(106, 20);
            this.numElementLen.TabIndex = 0;
            this.numElementLen.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numStep
            // 
            this.numStep.Location = new System.Drawing.Point(497, 37);
            this.numStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numStep.Name = "numStep";
            this.numStep.Size = new System.Drawing.Size(75, 20);
            this.numStep.TabIndex = 9;
            this.numStep.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numIterations
            // 
            this.numIterations.Location = new System.Drawing.Point(578, 37);
            this.numIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIterations.Name = "numIterations";
            this.numIterations.Size = new System.Drawing.Size(87, 20);
            this.numIterations.TabIndex = 10;
            this.numIterations.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelStep
            // 
            this.labelStep.AutoSize = true;
            this.labelStep.Location = new System.Drawing.Point(494, 24);
            this.labelStep.Name = "labelStep";
            this.labelStep.Size = new System.Drawing.Size(76, 13);
            this.labelStep.TabIndex = 11;
            this.labelStep.Text = "Шаг итер-ный";
            // 
            // labeIterations
            // 
            this.labeIterations.AutoSize = true;
            this.labeIterations.Location = new System.Drawing.Point(575, 24);
            this.labeIterations.Name = "labeIterations";
            this.labeIterations.Size = new System.Drawing.Size(91, 13);
            this.labeIterations.TabIndex = 12;
            this.labeIterations.Text = "Кол-во итераций";
            // 
            // saveFD
            // 
            this.saveFD.DefaultExt = "ivt|*.ivt";
            this.saveFD.Filter = "ivt архивы|*.ivt|Все файлы|*.*";
            // 
            // ArchiverMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 382);
            this.Controls.Add(this.labeIterations);
            this.Controls.Add(this.labelStep);
            this.Controls.Add(this.numIterations);
            this.Controls.Add(this.numStep);
            this.Controls.Add(this.groupSize);
            this.Controls.Add(this.groupWay);
            this.Controls.Add(this.groupDividing);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.treeFileView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolMenu);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "ArchiverMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Archiver 1.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupWay.ResumeLayout(false);
            this.groupWay.PerformLayout();
            this.groupDividing.ResumeLayout(false);
            this.groupDividing.PerformLayout();
            this.groupSize.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numElementLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIterations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem archivationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dearchivationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox fileTextBox;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.Button btnArchivate;
        private System.Windows.Forms.Button btnSerial;
        private System.Windows.Forms.OpenFileDialog openFD;
        private System.Windows.Forms.TreeView treeFileView;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ToolStrip toolMenu;
        private System.Windows.Forms.GroupBox groupWay;
        private System.Windows.Forms.RadioButton radioOptimal;
        private System.Windows.Forms.RadioButton radioPositional;
        private System.Windows.Forms.GroupBox groupDividing;
        private System.Windows.Forms.RadioButton radioLGrums;
        private System.Windows.Forms.RadioButton radioBlocks;
        private System.Windows.Forms.GroupBox groupSize;
        private System.Windows.Forms.NumericUpDown numElementLen;
        private System.Windows.Forms.NumericUpDown numStep;
        private System.Windows.Forms.NumericUpDown numIterations;
        private System.Windows.Forms.Label labelStep;
        private System.Windows.Forms.Label labeIterations;
        private System.Windows.Forms.ToolStripMenuItem savePathStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFD;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnDeArchivate;
    }
}

