
namespace YourDecoder
{
    partial class FrmMainScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnBrowserFolder = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnDecode = new System.Windows.Forms.Button();
            this.labelFolderPath = new System.Windows.Forms.Label();
            this.txtOwnerID = new System.Windows.Forms.TextBox();
            this.txtPartnerID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnClose = new System.Windows.Forms.Button();
            this.chkAutoPopulate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnBrowserFolder
            // 
            this.btnBrowserFolder.Location = new System.Drawing.Point(23, 21);
            this.btnBrowserFolder.Name = "btnBrowserFolder";
            this.btnBrowserFolder.Size = new System.Drawing.Size(260, 44);
            this.btnBrowserFolder.TabIndex = 0;
            this.btnBrowserFolder.Text = "Select Archive Folder";
            this.toolTip1.SetToolTip(this.btnBrowserFolder, "Select the folder where your archive files (.dat) reside");
            this.btnBrowserFolder.UseVisualStyleBackColor = true;
            this.btnBrowserFolder.Click += new System.EventHandler(this.btnBrowserFolder_Click);
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(23, 643);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(126, 44);
            this.btnDecode.TabIndex = 6;
            this.btnDecode.Text = "Decode";
            this.toolTip1.SetToolTip(this.btnDecode, "Decode selected file");
            this.btnDecode.UseVisualStyleBackColor = true;
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
            // 
            // labelFolderPath
            // 
            this.labelFolderPath.AutoSize = true;
            this.labelFolderPath.Location = new System.Drawing.Point(32, 80);
            this.labelFolderPath.Name = "labelFolderPath";
            this.labelFolderPath.Size = new System.Drawing.Size(64, 25);
            this.labelFolderPath.TabIndex = 1;
            this.labelFolderPath.Text = "label1";
            // 
            // txtOwnerID
            // 
            this.txtOwnerID.Location = new System.Drawing.Point(23, 129);
            this.txtOwnerID.Name = "txtOwnerID";
            this.txtOwnerID.Size = new System.Drawing.Size(203, 29);
            this.txtOwnerID.TabIndex = 2;
            this.txtOwnerID.Text = "[Your YahooID]";
            this.toolTip1.SetToolTip(this.txtOwnerID, "Please provide your Yahoo ID accurately");
            // 
            // txtPartnerID
            // 
            this.txtPartnerID.Location = new System.Drawing.Point(414, 129);
            this.txtPartnerID.Name = "txtPartnerID";
            this.txtPartnerID.Size = new System.Drawing.Size(203, 29);
            this.txtPartnerID.TabIndex = 3;
            this.txtPartnerID.Text = "[Your Friend\'s YahooID]";
            this.toolTip1.SetToolTip(this.txtPartnerID, "Please provide your friend\'s Yahoo ID");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 133);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "was chatting to";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(23, 185);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(594, 399);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(491, 643);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 44);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // chkAutoPopulate
            // 
            this.chkAutoPopulate.AutoSize = true;
            this.chkAutoPopulate.Location = new System.Drawing.Point(23, 600);
            this.chkAutoPopulate.Name = "chkAutoPopulate";
            this.chkAutoPopulate.Size = new System.Drawing.Size(327, 29);
            this.chkAutoPopulate.TabIndex = 8;
            this.chkAutoPopulate.Text = "Populate Yahoo ID from file name";
            this.toolTip1.SetToolTip(this.chkAutoPopulate, "Please check this box if you don\'t know what it does");
            this.chkAutoPopulate.UseVisualStyleBackColor = true;
            this.chkAutoPopulate.CheckedChanged += new System.EventHandler(this.chkAutoPopulate_CheckedChanged);
            // 
            // FrmMainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 717);
            this.Controls.Add(this.chkAutoPopulate);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDecode);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPartnerID);
            this.Controls.Add(this.txtOwnerID);
            this.Controls.Add(this.labelFolderPath);
            this.Controls.Add(this.btnBrowserFolder);
            this.Name = "FrmMainScreen";
            this.Text = "YourDecoder - Yahoo Messenger Decoder based on Yammy";
            this.toolTip1.SetToolTip(this, "YourDecoder - Yahoo Messenger Decoder based on Yammy");
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMainScreen_FormClosed);
            this.Load += new System.EventHandler(this.FrmMainScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnBrowserFolder;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelFolderPath;
        private System.Windows.Forms.TextBox txtOwnerID;
        private System.Windows.Forms.TextBox txtPartnerID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button btnDecode;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkAutoPopulate;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}