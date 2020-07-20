namespace WhatsMerged.WinForms.Forms
{
    partial class UserSettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserSettingsForm));
            this.lblFolders = new System.Windows.Forms.Label();
            this.txtFolders = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblExamples = new System.Windows.Forms.Label();
            this.lblExamplesTitle = new System.Windows.Forms.Label();
            this.lblExclusions = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblFolders
            // 
            this.lblFolders.AutoSize = true;
            this.lblFolders.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolders.Location = new System.Drawing.Point(12, 9);
            this.lblFolders.Name = "lblFolders";
            this.lblFolders.Size = new System.Drawing.Size(275, 16);
            this.lblFolders.TabIndex = 0;
            this.lblFolders.Text = "Starting folders for \'.git\' folder scanner:\r\n";
            // 
            // txtFolders
            // 
            this.txtFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFolders.Location = new System.Drawing.Point(12, 28);
            this.txtFolders.Multiline = true;
            this.txtFolders.Name = "txtFolders";
            this.txtFolders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFolders.Size = new System.Drawing.Size(440, 144);
            this.txtFolders.TabIndex = 1;
            this.txtFolders.WordWrap = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(296, 178);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(378, 178);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblExamples
            // 
            this.lblExamples.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExamples.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExamples.Location = new System.Drawing.Point(9, 204);
            this.lblExamples.Name = "lblExamples";
            this.lblExamples.Size = new System.Drawing.Size(307, 35);
            this.lblExamples.TabIndex = 5;
            this.lblExamples.Text = "X:\\path\\to\\source\r\nY:\\other\\path^folderToExclude1^folderToExclude2\r\n";
            // 
            // lblExamplesTitle
            // 
            this.lblExamplesTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExamplesTitle.AutoSize = true;
            this.lblExamplesTitle.Location = new System.Drawing.Point(9, 183);
            this.lblExamplesTitle.Name = "lblExamplesTitle";
            this.lblExamplesTitle.Size = new System.Drawing.Size(55, 13);
            this.lblExamplesTitle.TabIndex = 4;
            this.lblExamplesTitle.Text = "Examples:";
            // 
            // lblExclusions
            // 
            this.lblExclusions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExclusions.Location = new System.Drawing.Point(9, 242);
            this.lblExclusions.Name = "lblExclusions";
            this.lblExclusions.Size = new System.Drawing.Size(435, 39);
            this.lblExclusions.TabIndex = 6;
            this.lblExclusions.Text = resources.GetString("lblExclusions.Text");
            // 
            // UserSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(464, 288);
            this.ControlBox = false;
            this.Controls.Add(this.lblExclusions);
            this.Controls.Add(this.lblExamplesTitle);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtFolders);
            this.Controls.Add(this.lblFolders);
            this.Controls.Add(this.lblExamples);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(440, 260);
            this.Name = "UserSettingsForm";
            this.Text = "WhatsMerged User Settings";
            this.Load += new System.EventHandler(this.UserSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFolders;
        private System.Windows.Forms.TextBox txtFolders;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblExamples;
        private System.Windows.Forms.Label lblExamplesTitle;
        private System.Windows.Forms.Label lblExclusions;
    }
}