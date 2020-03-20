namespace GetWebImg
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.cBox_IsSavaText = new System.Windows.Forms.CheckBox();
            this.txt_RegularText = new System.Windows.Forms.TextBox();
            this.rdb_Other = new System.Windows.Forms.RadioButton();
            this.rdb_Wx = new System.Windows.Forms.RadioButton();
            this.lbl_SavaPath = new System.Windows.Forms.Label();
            this.btn_PathSava = new System.Windows.Forms.Button();
            this.lbl_Url = new System.Windows.Forms.Label();
            this.btn_ClearLog = new System.Windows.Forms.Button();
            this.txt_Log = new System.Windows.Forms.TextBox();
            this.cBox_Enable = new System.Windows.Forms.CheckBox();
            this.txt_Url = new System.Windows.Forms.TextBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cBox_IsSavaText
            // 
            this.cBox_IsSavaText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cBox_IsSavaText.AutoSize = true;
            this.cBox_IsSavaText.Checked = true;
            this.cBox_IsSavaText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cBox_IsSavaText.Location = new System.Drawing.Point(12, 699);
            this.cBox_IsSavaText.Name = "cBox_IsSavaText";
            this.cBox_IsSavaText.Size = new System.Drawing.Size(144, 16);
            this.cBox_IsSavaText.TabIndex = 20;
            this.cBox_IsSavaText.Text = "是否保存网页上的文字";
            this.cBox_IsSavaText.UseVisualStyleBackColor = true;
            this.cBox_IsSavaText.CheckedChanged += new System.EventHandler(this.cBox_IsSavaText_CheckedChanged);
            // 
            // txt_RegularText
            // 
            this.txt_RegularText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_RegularText.Location = new System.Drawing.Point(66, 61);
            this.txt_RegularText.Multiline = true;
            this.txt_RegularText.Name = "txt_RegularText";
            this.txt_RegularText.Size = new System.Drawing.Size(482, 32);
            this.txt_RegularText.TabIndex = 14;
            this.txt_RegularText.Text = "<img\\b[^<>]*?\\bdata-src[\\s\\t\\r\\n]*=[\\s\\t\\r\\n]*[\"\"\']?[\\s\\t\\r\\n]*(?<imgUrl>[^\\s\\t\\r" +
    "\\n\"\"\'<>]*)[^<>]*?/?[\\s\\t\\r\\n]*>";
            // 
            // rdb_Other
            // 
            this.rdb_Other.AutoSize = true;
            this.rdb_Other.Location = new System.Drawing.Point(21, 77);
            this.rdb_Other.Name = "rdb_Other";
            this.rdb_Other.Size = new System.Drawing.Size(47, 16);
            this.rdb_Other.TabIndex = 12;
            this.rdb_Other.Text = "其他";
            this.rdb_Other.UseVisualStyleBackColor = true;
            this.rdb_Other.CheckedChanged += new System.EventHandler(this.rdb_Other_CheckedChanged);
            // 
            // rdb_Wx
            // 
            this.rdb_Wx.AutoSize = true;
            this.rdb_Wx.Checked = true;
            this.rdb_Wx.Location = new System.Drawing.Point(21, 61);
            this.rdb_Wx.Name = "rdb_Wx";
            this.rdb_Wx.Size = new System.Drawing.Size(47, 16);
            this.rdb_Wx.TabIndex = 13;
            this.rdb_Wx.TabStop = true;
            this.rdb_Wx.Text = "微信";
            this.rdb_Wx.UseVisualStyleBackColor = true;
            this.rdb_Wx.CheckedChanged += new System.EventHandler(this.rdb_Wx_CheckedChanged);
            // 
            // lbl_SavaPath
            // 
            this.lbl_SavaPath.AutoSize = true;
            this.lbl_SavaPath.Location = new System.Drawing.Point(94, 41);
            this.lbl_SavaPath.Name = "lbl_SavaPath";
            this.lbl_SavaPath.Size = new System.Drawing.Size(53, 12);
            this.lbl_SavaPath.TabIndex = 18;
            this.lbl_SavaPath.Text = "<=请选择";
            // 
            // btn_PathSava
            // 
            this.btn_PathSava.Location = new System.Drawing.Point(12, 34);
            this.btn_PathSava.Name = "btn_PathSava";
            this.btn_PathSava.Size = new System.Drawing.Size(75, 23);
            this.btn_PathSava.TabIndex = 10;
            this.btn_PathSava.Text = "保 存 至";
            this.btn_PathSava.UseVisualStyleBackColor = true;
            this.btn_PathSava.Click += new System.EventHandler(this.btn_PathSava_Click);
            // 
            // lbl_Url
            // 
            this.lbl_Url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Url.AutoSize = true;
            this.lbl_Url.Location = new System.Drawing.Point(19, 10);
            this.lbl_Url.Name = "lbl_Url";
            this.lbl_Url.Size = new System.Drawing.Size(41, 12);
            this.lbl_Url.TabIndex = 15;
            this.lbl_Url.Text = "网址：";
            // 
            // btn_ClearLog
            // 
            this.btn_ClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ClearLog.Location = new System.Drawing.Point(671, 700);
            this.btn_ClearLog.Name = "btn_ClearLog";
            this.btn_ClearLog.Size = new System.Drawing.Size(75, 23);
            this.btn_ClearLog.TabIndex = 17;
            this.btn_ClearLog.Text = "清空日志";
            this.btn_ClearLog.UseVisualStyleBackColor = true;
            this.btn_ClearLog.Click += new System.EventHandler(this.btn_ClearLog_Click);
            // 
            // txt_Log
            // 
            this.txt_Log.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Log.Location = new System.Drawing.Point(12, 99);
            this.txt_Log.Multiline = true;
            this.txt_Log.Name = "txt_Log";
            this.txt_Log.ReadOnly = true;
            this.txt_Log.Size = new System.Drawing.Size(734, 595);
            this.txt_Log.TabIndex = 19;
            // 
            // cBox_Enable
            // 
            this.cBox_Enable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cBox_Enable.AutoSize = true;
            this.cBox_Enable.Location = new System.Drawing.Point(554, 64);
            this.cBox_Enable.Name = "cBox_Enable";
            this.cBox_Enable.Size = new System.Drawing.Size(192, 16);
            this.cBox_Enable.TabIndex = 16;
            this.cBox_Enable.Text = "是否启用二级获取二级链接数据";
            this.cBox_Enable.UseVisualStyleBackColor = true;
            this.cBox_Enable.CheckedChanged += new System.EventHandler(this.cBox_Enable_CheckedChanged);
            // 
            // txt_Url
            // 
            this.txt_Url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_Url.Location = new System.Drawing.Point(66, 7);
            this.txt_Url.Name = "txt_Url";
            this.txt_Url.Size = new System.Drawing.Size(680, 21);
            this.txt_Url.TabIndex = 9;
            this.txt_Url.Text = resources.GetString("txt_Url.Text");
            this.txt_Url.LostFocus += new System.EventHandler(this.txt_Url_LostFocus);
            // 
            // btn_Start
            // 
            this.btn_Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Start.Location = new System.Drawing.Point(671, 34);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 11;
            this.btn_Start.Text = "开  始";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 741);
            this.Controls.Add(this.cBox_IsSavaText);
            this.Controls.Add(this.txt_RegularText);
            this.Controls.Add(this.rdb_Other);
            this.Controls.Add(this.rdb_Wx);
            this.Controls.Add(this.lbl_SavaPath);
            this.Controls.Add(this.btn_PathSava);
            this.Controls.Add(this.lbl_Url);
            this.Controls.Add(this.btn_ClearLog);
            this.Controls.Add(this.txt_Log);
            this.Controls.Add(this.cBox_Enable);
            this.Controls.Add(this.txt_Url);
            this.Controls.Add(this.btn_Start);
            this.MinimumSize = new System.Drawing.Size(775, 780);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cBox_IsSavaText;
        private System.Windows.Forms.TextBox txt_RegularText;
        private System.Windows.Forms.RadioButton rdb_Other;
        private System.Windows.Forms.RadioButton rdb_Wx;
        private System.Windows.Forms.Label lbl_SavaPath;
        private System.Windows.Forms.Button btn_PathSava;
        private System.Windows.Forms.Label lbl_Url;
        private System.Windows.Forms.Button btn_ClearLog;
        private System.Windows.Forms.TextBox txt_Log;
        private System.Windows.Forms.CheckBox cBox_Enable;
        private System.Windows.Forms.TextBox txt_Url;
        private System.Windows.Forms.Button btn_Start;
    }
}