using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace YourDecoder
{
    public partial class FrmMainScreen : Form
    {
        const string HTML_OUTPUT = "output.html";
        private MySettings settings = MySettings.Load();        

        public FrmMainScreen()
        {           
            InitializeComponent();                        
        }

        public void SetUpControls()
        {
            labelFolderPath.Text = "[Selected Path]";
            string defaultPath = GetSetting("CurrentPath");
            if ( defaultPath.Length > 0 && Directory.Exists(defaultPath) )
            {
                folderBrowserDialog1.SelectedPath = defaultPath;
                RefreshFileList();
            }            
            txtOwnerID.Text = GetSetting("OwnerID");
            txtPartnerID.Text = GetSetting("PartnerID");
            chkAutoPopulate.Checked = GetSetting("IsAutoPopulateID") == "Yes" ? true : false;
        }

        private void btnBrowserFolder_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {          
                RefreshFileList();
            }
        }

        private string GetSetting(string settingName)
        {
            /*string query = string.Format("SELECT * FROM tblsetting WHERE SettingName = '{0}'", settingName);
            DataTable data = mLocalDB.SelectDataTable(query);
            if (data.Rows.Count > 0)
            {
                return data.Rows[0]["SettingValue"] + "";
            }*/
            System.Reflection.FieldInfo prop = typeof(MySettings).GetField(settingName);
            return (string)prop.GetValue(settings);
        }

        private void SaveSetting(string settingName, string value)
        {
            /*Dictionary<string, string> dicDB = new Dictionary<string, string>();
            dicDB.Add("SettingName", settingName);
            dicDB.Add("SettingValue", value);
            mLocalDB.UpdateOrInsert("tblsetting", dicDB, "SettingName");
            */
            System.Reflection.FieldInfo prop = typeof(MySettings).GetField(settingName);
            prop.SetValue(settings, value);
            settings.Save();
        }

        private void FrmMainScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSetting("CurrentPath", folderBrowserDialog1.SelectedPath);
            SaveSetting("OwnerID", txtOwnerID.Text);
            SaveSetting("PartnerID", txtPartnerID.Text);
        }

        private void FrmMainScreen_Load(object sender, EventArgs e)
        {            
            SetUpControls();
        }

        private void RefreshFileList()
        {
            PopulatePartnerID();
            labelFolderPath.Text = folderBrowserDialog1.SelectedPath;
            listView1.Items.Clear();
            DirectoryInfo nodeDirInfo = new DirectoryInfo(folderBrowserDialog1.SelectedPath);            
            ListViewItem item = null;

            /*foreach (DirectoryInfo dir in nodeDirInfo.GetDirectories())
            {
                item = new ListViewItem(dir.Name, 0);
                subItems = new ListViewItem.ListViewSubItem[]
                    {new ListViewItem.ListViewSubItem(item, "Directory"),
                    new ListViewItem.ListViewSubItem(item,
                    dir.LastAccessTime.ToShortDateString())};
                    item.SubItems.AddRange(subItems);
                    listView1.Items.Add(item);
            }*/
            foreach (FileInfo file in nodeDirInfo.GetFiles())
            {
                item = new ListViewItem(file.Name, 1);
                /*subItems = new ListViewItem.ListViewSubItem[]
                    { new ListViewItem.ListViewSubItem(item, "File"),
                    new ListViewItem.ListViewSubItem(item,
                file.LastAccessTime.ToShortDateString())};

                item.SubItems.AddRange(subItems);*/
                listView1.Items.Add(item);
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void PopulatePartnerID()
        {
            if (!chkAutoPopulate.Checked)
            {
                return;
            }
            string folderPath = folderBrowserDialog1.SelectedPath;
            if (folderPath.Length > 0 && folderPath.Contains("Archive\\Messages"))
            {
                string folderName = Path.GetFileName(folderPath.TrimEnd(Path.DirectorySeparatorChar));
                if (folderName.Length > 0 && folderName.ToLower().Equals(folderName))
                {
                    txtPartnerID.Text = folderName;
                }
            }
        }

        private void PopulateOwnerID()
        {
            if (!chkAutoPopulate.Checked)
            {
                return;
            }
            if (listView1.SelectedItems.Count > 0)
            {
                string ownerID = GetOwnerIDFromDATFile(listView1.SelectedItems[0].Text);
                if (ownerID.Length > 0)
                {
                    txtOwnerID.Text = ownerID;
                }
            }
           
        }

        private string GetOwnerIDFromDATFile(string fileName)
        {
            fileName = fileName.ToLower();
            if (fileName.EndsWith(".dat")) fileName = fileName.Substring(0, fileName.Length - 4);
            string[] parts = fileName.Split('-');
            if (parts.Length == 2)
            {
                return parts[1];
            }
            return "";
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a valid archive file (*.dat)");
                return;
            }
            if (listView1.SelectedItems.Count > 0 && IsInputValid())
            {                
                string fullPath = folderBrowserDialog1.SelectedPath + "\\" + listView1.SelectedItems[0].Text;
                PopupDecodedPage(fullPath);                
            }
        }

        private bool IsInputValid()
        {
            txtOwnerID.Text = txtOwnerID.Text.Trim();
            txtPartnerID.Text = txtPartnerID.Text.Trim();
            if (txtOwnerID.Text.Length == 0)
            {
                MessageBox.Show("Please enter Your Yahoo ID or Name");
                txtOwnerID.Focus();
                return false;
            }
            if (txtPartnerID.Text.Length == 0)
            {
                MessageBox.Show("Please enter Your Friend's Yahoo ID or Name");
                txtPartnerID.Focus();
                return false;
            }
            return true;
        }

        public string fixHTMLOutput(string html)
        {            
            html = html.Replace("[1m[#2b7edbm", "");
            html = html.Replace("[#0080ffm", "");            
            html = html.Replace("[1m[#3695f3m", "");
            //html = html.Replace("[#c40062m", "");
            html = html.Replace("[34m", "");
            html = html.Replace("", "");
            return html;
        }

        private void PopupDecodedPage(string fullPath)
        {
            Yammy.Decoder decoder = new Yammy.Decoder(fullPath, txtOwnerID.Text, txtPartnerID.Text);
            string fullHTML = decoder.Decode(false, false, null);
            fullHTML = fixHTMLOutput(fullHTML);
            fullHTML = "<link rel=\"stylesheet\" type=\"text/css\" href=\"print.css\">" + fullHTML;
            File.WriteAllText(HTML_OUTPUT, fullHTML, Encoding.Unicode);
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "\\" + HTML_OUTPUT);
            }
            catch { }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnDecode.PerformClick();
        }

        private void chkAutoPopulate_CheckedChanged(object sender, EventArgs e)
        {
            SaveSetting("IsAutoPopulateID", chkAutoPopulate.Checked ? "Yes" : "No");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateOwnerID();
        }
    }

    class MySettings : AppSettings<MySettings>
    {
        public string CurrentPath = "";
        public string OwnerID = "";
        public string PartnerID = "";
        public string IsAutoPopulateID = "Yes";
    }
}
