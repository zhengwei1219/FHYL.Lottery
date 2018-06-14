namespace OCAPIDemo
{
    partial class FormDemo
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonXml = new System.Windows.Forms.Button();
            this.textBoxXmlResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxXml = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonJson = new System.Windows.Forms.Button();
            this.textBoxJsonResult = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxJson = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(521, 339);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonXml);
            this.tabPage1.Controls.Add(this.textBoxXmlResult);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.textBoxXml);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(513, 313);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "采集Xml格式";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonXml
            // 
            this.buttonXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXml.Location = new System.Drawing.Point(421, 19);
            this.buttonXml.Name = "buttonXml";
            this.buttonXml.Size = new System.Drawing.Size(75, 23);
            this.buttonXml.TabIndex = 7;
            this.buttonXml.Text = "开始";
            this.buttonXml.UseVisualStyleBackColor = true;
            this.buttonXml.Click += new System.EventHandler(this.buttonXml_Click);
            // 
            // textBoxXmlResult
            // 
            this.textBoxXmlResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXmlResult.Location = new System.Drawing.Point(15, 58);
            this.textBoxXmlResult.Multiline = true;
            this.textBoxXmlResult.Name = "textBoxXmlResult";
            this.textBoxXmlResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxXmlResult.Size = new System.Drawing.Size(481, 255);
            this.textBoxXmlResult.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Xml采集地址：";
            // 
            // textBoxXml
            // 
            this.textBoxXml.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxXml.Location = new System.Drawing.Point(105, 21);
            this.textBoxXml.Name = "textBoxXml";
            this.textBoxXml.Size = new System.Drawing.Size(305, 21);
            this.textBoxXml.TabIndex = 4;
            this.textBoxXml.Text = "http://f.apiplus.net/cqssc.xml";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonJson);
            this.tabPage2.Controls.Add(this.textBoxJsonResult);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.textBoxJson);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(513, 313);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "采集Json格式";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonJson
            // 
            this.buttonJson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonJson.Location = new System.Drawing.Point(422, 17);
            this.buttonJson.Name = "buttonJson";
            this.buttonJson.Size = new System.Drawing.Size(75, 23);
            this.buttonJson.TabIndex = 11;
            this.buttonJson.Text = "开始";
            this.buttonJson.UseVisualStyleBackColor = true;
            this.buttonJson.Click += new System.EventHandler(this.buttonJson_Click);
            // 
            // textBoxJsonResult
            // 
            this.textBoxJsonResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJsonResult.Location = new System.Drawing.Point(16, 56);
            this.textBoxJsonResult.Multiline = true;
            this.textBoxJsonResult.Name = "textBoxJsonResult";
            this.textBoxJsonResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxJsonResult.Size = new System.Drawing.Size(481, 240);
            this.textBoxJsonResult.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "Json采集地址：";
            // 
            // textBoxJson
            // 
            this.textBoxJson.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxJson.Location = new System.Drawing.Point(106, 19);
            this.textBoxJson.Name = "textBoxJson";
            this.textBoxJson.Size = new System.Drawing.Size(305, 21);
            this.textBoxJson.TabIndex = 8;
            this.textBoxJson.Text = "http://f.apiplus.net/cqssc.json";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(10, 358);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(509, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "免费采集存在3-8分钟数据延迟。如需购买正式付费接口请访问www.opencai.net了解更多信息。";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // FormDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 379);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tabControl1);
            this.Name = "FormDemo";
            this.Text = "开彩网API采集演示 - www.opencai.net";
            this.Load += new System.EventHandler(this.FormDemo_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonXml;
        private System.Windows.Forms.TextBox textBoxXmlResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxXml;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonJson;
        private System.Windows.Forms.TextBox textBoxJsonResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxJson;
        private System.Windows.Forms.Label label3;
    }
}

