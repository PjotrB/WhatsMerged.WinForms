namespace WhatsMerged.WinForms.Forms
{
    partial class MainForm
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
            this.cbxProject = new System.Windows.Forms.ComboBox();
            this.lblProject = new System.Windows.Forms.Label();
            this.lblWorkBranches = new System.Windows.Forms.Label();
            this.btnWorkVsMerge = new System.Windows.Forms.Button();
            this.txtError = new System.Windows.Forms.TextBox();
            this.grid = new System.Windows.Forms.DataGridView();
            this.lblMergeBranches = new System.Windows.Forms.Label();
            this.lblIgnoreBranches = new System.Windows.Forms.Label();
            this.lstWorkBranches = new System.Windows.Forms.ListBox();
            this.lstMergeBranches = new System.Windows.Forms.ListBox();
            this.lstIgnoreBranches = new System.Windows.Forms.ListBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnMergeSortByDate = new System.Windows.Forms.Button();
            this.btnWorkSortByDate = new System.Windows.Forms.Button();
            this.btnIgnoreSortByDate = new System.Windows.Forms.Button();
            this.btnReloadProject = new System.Windows.Forms.Button();
            this.btnWorkAndMergeVsMerge = new System.Windows.Forms.Button();
            this.btnWorkVsItself = new System.Windows.Forms.Button();
            this.btnMergeVsWork = new System.Windows.Forms.Button();
            this.btnMergeVsItself = new System.Windows.Forms.Button();
            this.btnUserSettings = new System.Windows.Forms.Button();
            this.btnWorkAndMergeVsItself = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxProject
            // 
            this.cbxProject.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbxProject.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbxProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProject.FormattingEnabled = true;
            this.cbxProject.Location = new System.Drawing.Point(89, 12);
            this.cbxProject.Name = "cbxProject";
            this.cbxProject.Size = new System.Drawing.Size(297, 21);
            this.cbxProject.TabIndex = 1;
            this.cbxProject.DropDownClosed += new System.EventHandler(this.CbxProject_DropDownClosed);
            this.cbxProject.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CbxProject_KeyDown);
            // 
            // lblProject
            // 
            this.lblProject.AutoSize = true;
            this.lblProject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProject.Location = new System.Drawing.Point(38, 15);
            this.lblProject.Name = "lblProject";
            this.lblProject.Size = new System.Drawing.Size(51, 13);
            this.lblProject.TabIndex = 0;
            this.lblProject.Text = "Project:";
            // 
            // lblWorkBranches
            // 
            this.lblWorkBranches.AutoSize = true;
            this.lblWorkBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkBranches.Location = new System.Drawing.Point(7, 50);
            this.lblWorkBranches.Name = "lblWorkBranches";
            this.lblWorkBranches.Size = new System.Drawing.Size(93, 13);
            this.lblWorkBranches.TabIndex = 4;
            this.lblWorkBranches.Text = "Work branches";
            // 
            // btnWorkVsMerge
            // 
            this.btnWorkVsMerge.Location = new System.Drawing.Point(9, 183);
            this.btnWorkVsMerge.Name = "btnWorkVsMerge";
            this.btnWorkVsMerge.Size = new System.Drawing.Size(132, 24);
            this.btnWorkVsMerge.TabIndex = 13;
            this.btnWorkVsMerge.Text = "Work vs Merge";
            this.btnWorkVsMerge.UseVisualStyleBackColor = true;
            this.btnWorkVsMerge.Click += new System.EventHandler(this.BtnWorkVsMerge_Click);
            // 
            // txtError
            // 
            this.txtError.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtError.BackColor = System.Drawing.Color.White;
            this.txtError.ForeColor = System.Drawing.Color.Red;
            this.txtError.Location = new System.Drawing.Point(285, 184);
            this.txtError.Multiline = true;
            this.txtError.Name = "txtError";
            this.txtError.ReadOnly = true;
            this.txtError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtError.Size = new System.Drawing.Size(529, 83);
            this.txtError.TabIndex = 19;
            // 
            // grid
            // 
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(10, 276);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(804, 313);
            this.grid.TabIndex = 20;
            this.grid.TabStop = false;
            this.grid.SelectionChanged += new System.EventHandler(this.Grid_SelectionChanged);
            // 
            // lblMergeBranches
            // 
            this.lblMergeBranches.AutoSize = true;
            this.lblMergeBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMergeBranches.Location = new System.Drawing.Point(231, 50);
            this.lblMergeBranches.Name = "lblMergeBranches";
            this.lblMergeBranches.Size = new System.Drawing.Size(98, 13);
            this.lblMergeBranches.TabIndex = 7;
            this.lblMergeBranches.Text = "Merge branches";
            // 
            // lblIgnoreBranches
            // 
            this.lblIgnoreBranches.AutoSize = true;
            this.lblIgnoreBranches.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIgnoreBranches.Location = new System.Drawing.Point(456, 50);
            this.lblIgnoreBranches.Name = "lblIgnoreBranches";
            this.lblIgnoreBranches.Size = new System.Drawing.Size(99, 13);
            this.lblIgnoreBranches.TabIndex = 10;
            this.lblIgnoreBranches.Text = "Ignore branches";
            // 
            // lstWorkBranches
            // 
            this.lstWorkBranches.FormattingEnabled = true;
            this.lstWorkBranches.Location = new System.Drawing.Point(9, 69);
            this.lstWorkBranches.Name = "lstWorkBranches";
            this.lstWorkBranches.Size = new System.Drawing.Size(218, 108);
            this.lstWorkBranches.TabIndex = 6;
            this.lstWorkBranches.SelectedIndexChanged += new System.EventHandler(this.LstWorkBranches_SelectedIndexChanged);
            this.lstWorkBranches.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstWorkBranches_KeyDown);
            this.lstWorkBranches.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstWorkBranches_MouseDoubleClick);
            this.lstWorkBranches.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LstWorkBranches_MouseDown);
            // 
            // lstMergeBranches
            // 
            this.lstMergeBranches.FormattingEnabled = true;
            this.lstMergeBranches.Location = new System.Drawing.Point(234, 69);
            this.lstMergeBranches.Name = "lstMergeBranches";
            this.lstMergeBranches.Size = new System.Drawing.Size(218, 108);
            this.lstMergeBranches.TabIndex = 9;
            this.lstMergeBranches.SelectedIndexChanged += new System.EventHandler(this.LstMergeBranches_SelectedIndexChanged);
            this.lstMergeBranches.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstMergeBranches_KeyDown);
            this.lstMergeBranches.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstMergeBranches_MouseDoubleClick);
            this.lstMergeBranches.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LstMergeBranches_MouseDown);
            // 
            // lstIgnoreBranches
            // 
            this.lstIgnoreBranches.FormattingEnabled = true;
            this.lstIgnoreBranches.Location = new System.Drawing.Point(459, 69);
            this.lstIgnoreBranches.Name = "lstIgnoreBranches";
            this.lstIgnoreBranches.Size = new System.Drawing.Size(218, 108);
            this.lstIgnoreBranches.TabIndex = 12;
            this.lstIgnoreBranches.SelectedIndexChanged += new System.EventHandler(this.LstIgnoreBranches_SelectedIndexChanged);
            this.lstIgnoreBranches.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstIgnoreBranches_KeyDown);
            this.lstIgnoreBranches.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LstIgnoreBranches_MouseDoubleClick);
            this.lstIgnoreBranches.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LstIgnoreBranches_MouseDown);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(419, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(397, 31);
            this.lblStatus.TabIndex = 3;
            // 
            // btnMergeSortByDate
            // 
            this.btnMergeSortByDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMergeSortByDate.Location = new System.Drawing.Point(335, 45);
            this.btnMergeSortByDate.Name = "btnMergeSortByDate";
            this.btnMergeSortByDate.Size = new System.Drawing.Size(118, 23);
            this.btnMergeSortByDate.TabIndex = 8;
            this.btnMergeSortByDate.Text = "Sort by last commit date";
            this.btnMergeSortByDate.UseVisualStyleBackColor = true;
            this.btnMergeSortByDate.Click += new System.EventHandler(this.BtnMergeSortByDate_Click);
            // 
            // btnWorkSortByDate
            // 
            this.btnWorkSortByDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWorkSortByDate.Location = new System.Drawing.Point(110, 45);
            this.btnWorkSortByDate.Name = "btnWorkSortByDate";
            this.btnWorkSortByDate.Size = new System.Drawing.Size(118, 23);
            this.btnWorkSortByDate.TabIndex = 5;
            this.btnWorkSortByDate.Text = "Sort by last commit date";
            this.btnWorkSortByDate.UseVisualStyleBackColor = true;
            this.btnWorkSortByDate.Click += new System.EventHandler(this.BtnWorkSortByDate_Click);
            // 
            // btnIgnoreSortByDate
            // 
            this.btnIgnoreSortByDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIgnoreSortByDate.Location = new System.Drawing.Point(560, 45);
            this.btnIgnoreSortByDate.Name = "btnIgnoreSortByDate";
            this.btnIgnoreSortByDate.Size = new System.Drawing.Size(118, 23);
            this.btnIgnoreSortByDate.TabIndex = 11;
            this.btnIgnoreSortByDate.Text = "Sort by last commit date";
            this.btnIgnoreSortByDate.UseVisualStyleBackColor = true;
            this.btnIgnoreSortByDate.Click += new System.EventHandler(this.BtnIgnoreSortByDate_Click);
            // 
            // btnReloadProject
            // 
            this.btnReloadProject.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnReloadProject.Font = new System.Drawing.Font("Webdings", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnReloadProject.Location = new System.Drawing.Point(393, 11);
            this.btnReloadProject.Name = "btnReloadProject";
            this.btnReloadProject.Size = new System.Drawing.Size(23, 23);
            this.btnReloadProject.TabIndex = 2;
            this.btnReloadProject.Text = "q";
            this.btnReloadProject.UseVisualStyleBackColor = true;
            this.btnReloadProject.Click += new System.EventHandler(this.BtnReloadProject_Click);
            // 
            // btnWorkAndMergeVsMerge
            // 
            this.btnWorkAndMergeVsMerge.Location = new System.Drawing.Point(9, 243);
            this.btnWorkAndMergeVsMerge.Name = "btnWorkAndMergeVsMerge";
            this.btnWorkAndMergeVsMerge.Size = new System.Drawing.Size(132, 24);
            this.btnWorkAndMergeVsMerge.TabIndex = 15;
            this.btnWorkAndMergeVsMerge.Text = "Work+Merge vs Merge";
            this.btnWorkAndMergeVsMerge.UseVisualStyleBackColor = true;
            this.btnWorkAndMergeVsMerge.Click += new System.EventHandler(this.BtnWorkAndMergeVsMerge_Click);
            // 
            // btnWorkVsItself
            // 
            this.btnWorkVsItself.Location = new System.Drawing.Point(147, 183);
            this.btnWorkVsItself.Name = "btnWorkVsItself";
            this.btnWorkVsItself.Size = new System.Drawing.Size(132, 24);
            this.btnWorkVsItself.TabIndex = 16;
            this.btnWorkVsItself.Text = "Work vs itself";
            this.btnWorkVsItself.UseVisualStyleBackColor = true;
            this.btnWorkVsItself.Click += new System.EventHandler(this.BtnWorkVsItself_Click);
            // 
            // btnMergeVsWork
            // 
            this.btnMergeVsWork.Location = new System.Drawing.Point(9, 213);
            this.btnMergeVsWork.Name = "btnMergeVsWork";
            this.btnMergeVsWork.Size = new System.Drawing.Size(132, 24);
            this.btnMergeVsWork.TabIndex = 14;
            this.btnMergeVsWork.Text = "Merge vs Work";
            this.btnMergeVsWork.UseVisualStyleBackColor = true;
            this.btnMergeVsWork.Click += new System.EventHandler(this.BtnMergeVsWork_Click);
            // 
            // btnMergeVsItself
            // 
            this.btnMergeVsItself.Location = new System.Drawing.Point(147, 213);
            this.btnMergeVsItself.Name = "btnMergeVsItself";
            this.btnMergeVsItself.Size = new System.Drawing.Size(132, 24);
            this.btnMergeVsItself.TabIndex = 17;
            this.btnMergeVsItself.Text = "Merge vs itself";
            this.btnMergeVsItself.UseVisualStyleBackColor = true;
            this.btnMergeVsItself.Click += new System.EventHandler(this.BtnMergeVsItself_Click);
            // 
            // btnUserSettings
            // 
            this.btnUserSettings.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnUserSettings.Font = new System.Drawing.Font("Webdings", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnUserSettings.Location = new System.Drawing.Point(9, 11);
            this.btnUserSettings.Name = "btnUserSettings";
            this.btnUserSettings.Size = new System.Drawing.Size(23, 23);
            this.btnUserSettings.TabIndex = 21;
            this.btnUserSettings.Text = "~";
            this.btnUserSettings.UseVisualStyleBackColor = true;
            this.btnUserSettings.Click += new System.EventHandler(this.BtnUserSettings_Click);
            // 
            // btnWorkAndMergeVsItself
            // 
            this.btnWorkAndMergeVsItself.Location = new System.Drawing.Point(147, 243);
            this.btnWorkAndMergeVsItself.Name = "btnWorkAndMergeVsItself";
            this.btnWorkAndMergeVsItself.Size = new System.Drawing.Size(132, 24);
            this.btnWorkAndMergeVsItself.TabIndex = 18;
            this.btnWorkAndMergeVsItself.Text = "Work+Merge vs itself";
            this.btnWorkAndMergeVsItself.UseVisualStyleBackColor = true;
            this.btnWorkAndMergeVsItself.Click += new System.EventHandler(this.BtnWorkAndMergeVsItself_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 601);
            this.Controls.Add(this.btnWorkAndMergeVsItself);
            this.Controls.Add(this.btnUserSettings);
            this.Controls.Add(this.btnMergeVsItself);
            this.Controls.Add(this.btnMergeVsWork);
            this.Controls.Add(this.btnWorkVsItself);
            this.Controls.Add(this.btnWorkAndMergeVsMerge);
            this.Controls.Add(this.btnReloadProject);
            this.Controls.Add(this.btnIgnoreSortByDate);
            this.Controls.Add(this.btnWorkSortByDate);
            this.Controls.Add(this.btnMergeSortByDate);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lstIgnoreBranches);
            this.Controls.Add(this.lstMergeBranches);
            this.Controls.Add(this.lstWorkBranches);
            this.Controls.Add(this.lblIgnoreBranches);
            this.Controls.Add(this.lblMergeBranches);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.btnWorkVsMerge);
            this.Controls.Add(this.lblWorkBranches);
            this.Controls.Add(this.cbxProject);
            this.Controls.Add(this.lblProject);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(702, 364);
            this.Name = "MainForm";
            this.Text = "WhatsMerged";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbxProject;
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.Label lblWorkBranches;
        private System.Windows.Forms.Button btnWorkVsMerge;
        private System.Windows.Forms.TextBox txtError;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Label lblMergeBranches;
        private System.Windows.Forms.Label lblIgnoreBranches;
        private System.Windows.Forms.ListBox lstWorkBranches;
        private System.Windows.Forms.ListBox lstMergeBranches;
        private System.Windows.Forms.ListBox lstIgnoreBranches;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnMergeSortByDate;
        private System.Windows.Forms.Button btnWorkSortByDate;
        private System.Windows.Forms.Button btnIgnoreSortByDate;
        private System.Windows.Forms.Button btnReloadProject;
        private System.Windows.Forms.Button btnWorkAndMergeVsMerge;
        private System.Windows.Forms.Button btnWorkVsItself;
        private System.Windows.Forms.Button btnMergeVsWork;
        private System.Windows.Forms.Button btnMergeVsItself;
        private System.Windows.Forms.Button btnUserSettings;
        private System.Windows.Forms.Button btnWorkAndMergeVsItself;
    }
}

