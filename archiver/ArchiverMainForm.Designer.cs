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
            this.fileTextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDeArc = new System.Windows.Forms.Button();
            this.btnToArc = new System.Windows.Forms.Button();
            this.labelFile = new System.Windows.Forms.Label();
            this.openFD = new System.Windows.Forms.OpenFileDialog();
            this.treeFileView = new System.Windows.Forms.TreeView();
            this.listView = new System.Windows.Forms.ListView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.groupWay = new System.Windows.Forms.GroupBox();
            this.radioPositional = new System.Windows.Forms.RadioButton();
            this.radioOptimal = new System.Windows.Forms.RadioButton();
            this.groupDividing = new System.Windows.Forms.GroupBox();
            this.radioLGrums = new System.Windows.Forms.RadioButton();
            this.radioBlocks = new System.Windows.Forms.RadioButton();
            this.groupSize = new System.Windows.Forms.GroupBox();
            this.elementLength = new System.Windows.Forms.NumericUpDown();
            this.mainMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupWay.SuspendLayout();
            this.groupDividing.SuspendLayout();
            this.groupSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.elementLength)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(586, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
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
            this.openToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.openToolStripMenuItem.Text = "Открыть";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openFile_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.saveToolStripMenuItem.Text = "Просмотр";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // archivationToolStripMenuItem
            // 
            this.archivationToolStripMenuItem.Name = "archivationToolStripMenuItem";
            this.archivationToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.archivationToolStripMenuItem.Text = "Архивация";
            // 
            // dearchivationToolStripMenuItem
            // 
            this.dearchivationToolStripMenuItem.Name = "dearchivationToolStripMenuItem";
            this.dearchivationToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.dearchivationToolStripMenuItem.Text = "Разархивация";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
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
            this.panel1.Controls.Add(this.fileTextBox);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.labelFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 264);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 23);
            this.panel1.TabIndex = 1;
            // 
            // fileTextBox
            // 
            this.fileTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.fileTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTextBox.Location = new System.Drawing.Point(39, 0);
            this.fileTextBox.Multiline = true;
            this.fileTextBox.Name = "fileTextBox";
            this.fileTextBox.ReadOnly = true;
            this.fileTextBox.Size = new System.Drawing.Size(318, 23);
            this.fileTextBox.TabIndex = 1;
            this.fileTextBox.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.btnDeArc);
            this.panel2.Controls.Add(this.btnToArc);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(357, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(229, 23);
            this.panel2.TabIndex = 0;
            // 
            // btnDeArc
            // 
            this.btnDeArc.Location = new System.Drawing.Point(116, 0);
            this.btnDeArc.Name = "btnDeArc";
            this.btnDeArc.Size = new System.Drawing.Size(110, 22);
            this.btnDeArc.TabIndex = 2;
            this.btnDeArc.Text = "Разархивировать";
            this.btnDeArc.UseVisualStyleBackColor = true;
            // 
            // btnToArc
            // 
            this.btnToArc.Location = new System.Drawing.Point(3, 0);
            this.btnToArc.Name = "btnToArc";
            this.btnToArc.Size = new System.Drawing.Size(110, 22);
            this.btnToArc.TabIndex = 1;
            this.btnToArc.Text = "Архивировать";
            this.btnToArc.UseVisualStyleBackColor = true;
            this.btnToArc.Click += new System.EventHandler(this.btnToArc_Click);
            // 
            // labelFile
            // 
            this.labelFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelFile.Location = new System.Drawing.Point(0, 0);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(39, 23);
            this.labelFile.TabIndex = 0;
            this.labelFile.Text = "Файл:";
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
            this.treeFileView.Size = new System.Drawing.Size(267, 201);
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
            this.listView.Size = new System.Drawing.Size(319, 201);
            this.listView.TabIndex = 4;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(586, 39);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
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
            this.groupSize.Controls.Add(this.elementLength);
            this.groupSize.Location = new System.Drawing.Point(373, 24);
            this.groupSize.Name = "groupSize";
            this.groupSize.Size = new System.Drawing.Size(118, 36);
            this.groupSize.TabIndex = 8;
            this.groupSize.TabStop = false;
            this.groupSize.Text = "Размер элемента";
            // 
            // elementLength
            // 
            this.elementLength.Location = new System.Drawing.Point(6, 13);
            this.elementLength.Name = "elementLength";
            this.elementLength.Size = new System.Drawing.Size(106, 20);
            this.elementLength.TabIndex = 0;
            this.elementLength.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // ArchiverMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 287);
            this.Controls.Add(this.groupSize);
            this.Controls.Add(this.groupWay);
            this.Controls.Add(this.groupDividing);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.treeFileView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
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
            ((System.ComponentModel.ISupportInitialize)(this.elementLength)).EndInit();
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
        private System.Windows.Forms.Button btnToArc;
        private System.Windows.Forms.Button btnDeArc;
        private System.Windows.Forms.OpenFileDialog openFD;
        private System.Windows.Forms.TreeView treeFileView;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.GroupBox groupWay;
        private System.Windows.Forms.RadioButton radioOptimal;
        private System.Windows.Forms.RadioButton radioPositional;
        private System.Windows.Forms.GroupBox groupDividing;
        private System.Windows.Forms.RadioButton radioLGrums;
        private System.Windows.Forms.RadioButton radioBlocks;
        private System.Windows.Forms.GroupBox groupSize;
        private System.Windows.Forms.NumericUpDown elementLength;
    }
}

