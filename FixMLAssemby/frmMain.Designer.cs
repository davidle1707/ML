namespace FixMLAssemby
{
	partial class frmMain
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbxSourePath = new System.Windows.Forms.TextBox();
            this.btnBrowserSourcePath = new System.Windows.Forms.Button();
            this.dlgSourcePath = new System.Windows.Forms.FolderBrowserDialog();
            this.btnAssemblyFilePath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dlgAssembyFilePath = new System.Windows.Forms.OpenFileDialog();
            this.btnFixAssembly = new System.Windows.Forms.Button();
            this.dgAssemblies = new System.Windows.Forms.DataGridView();
            this.colAssemblyRemove = new System.Windows.Forms.DataGridViewButtonColumn();
            this.colAssemblyName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAssemblyRuntime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAssemblyVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAssemblyDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAssemblyPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgProjects = new System.Windows.Forms.DataGridView();
            this.colProjectSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colProjectPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxAssemblyFolderName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgAssemblies)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgProjects)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source Path";
            // 
            // tbxSourePath
            // 
            this.tbxSourePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxSourePath.BackColor = System.Drawing.SystemColors.Window;
            this.tbxSourePath.Location = new System.Drawing.Point(77, 9);
            this.tbxSourePath.Name = "tbxSourePath";
            this.tbxSourePath.ReadOnly = true;
            this.tbxSourePath.Size = new System.Drawing.Size(203, 20);
            this.tbxSourePath.TabIndex = 1;
            // 
            // btnBrowserSourcePath
            // 
            this.btnBrowserSourcePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowserSourcePath.Location = new System.Drawing.Point(286, 7);
            this.btnBrowserSourcePath.Name = "btnBrowserSourcePath";
            this.btnBrowserSourcePath.Size = new System.Drawing.Size(51, 23);
            this.btnBrowserSourcePath.TabIndex = 2;
            this.btnBrowserSourcePath.Text = "...";
            this.btnBrowserSourcePath.UseVisualStyleBackColor = true;
            this.btnBrowserSourcePath.Click += new System.EventHandler(this.btnBrowserSourcePath_Click);
            // 
            // btnAssemblyFilePath
            // 
            this.btnAssemblyFilePath.Location = new System.Drawing.Point(88, 6);
            this.btnAssemblyFilePath.Name = "btnAssemblyFilePath";
            this.btnAssemblyFilePath.Size = new System.Drawing.Size(51, 23);
            this.btnAssemblyFilePath.TabIndex = 5;
            this.btnAssemblyFilePath.Text = "Add";
            this.btnAssemblyFilePath.UseVisualStyleBackColor = true;
            this.btnAssemblyFilePath.Click += new System.EventHandler(this.btnAssemblyFilePath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Assembly Files";
            // 
            // dlgAssembyFilePath
            // 
            this.dlgAssembyFilePath.Filter = "Assembly files|*.dll";
            this.dlgAssembyFilePath.Multiselect = true;
            // 
            // btnFixAssembly
            // 
            this.btnFixAssembly.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFixAssembly.Location = new System.Drawing.Point(19, 313);
            this.btnFixAssembly.Name = "btnFixAssembly";
            this.btnFixAssembly.Size = new System.Drawing.Size(101, 31);
            this.btnFixAssembly.TabIndex = 6;
            this.btnFixAssembly.Text = "Fix Assemblies";
            this.btnFixAssembly.UseVisualStyleBackColor = true;
            this.btnFixAssembly.Click += new System.EventHandler(this.btnFixAssembly_Click);
            // 
            // dgAssemblies
            // 
            this.dgAssemblies.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgAssemblies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgAssemblies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAssemblies.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAssemblyRemove,
            this.colAssemblyName,
            this.colAssemblyRuntime,
            this.colAssemblyVersion,
            this.colAssemblyDescription,
            this.colAssemblyPath});
            this.dgAssemblies.Location = new System.Drawing.Point(10, 35);
            this.dgAssemblies.MultiSelect = false;
            this.dgAssemblies.Name = "dgAssemblies";
            this.dgAssemblies.ReadOnly = true;
            this.dgAssemblies.Size = new System.Drawing.Size(426, 254);
            this.dgAssemblies.TabIndex = 7;
            this.dgAssemblies.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAssemblies_CellContentClick);
            // 
            // colAssemblyRemove
            // 
            this.colAssemblyRemove.HeaderText = "";
            this.colAssemblyRemove.Name = "colAssemblyRemove";
            this.colAssemblyRemove.Text = "Remove";
            this.colAssemblyRemove.UseColumnTextForButtonValue = true;
            this.colAssemblyRemove.Width = 21;
            // 
            // colAssemblyName
            // 
            this.colAssemblyName.DataPropertyName = "Name";
            this.colAssemblyName.HeaderText = "Name";
            this.colAssemblyName.Name = "colAssemblyName";
            this.colAssemblyName.ReadOnly = true;
            this.colAssemblyName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colAssemblyName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colAssemblyName.Width = 41;
            // 
            // colAssemblyRuntime
            // 
            this.colAssemblyRuntime.DataPropertyName = "Runtime";
            this.colAssemblyRuntime.HeaderText = "Runtime";
            this.colAssemblyRuntime.Name = "colAssemblyRuntime";
            this.colAssemblyRuntime.ReadOnly = true;
            this.colAssemblyRuntime.Width = 71;
            // 
            // colAssemblyVersion
            // 
            this.colAssemblyVersion.DataPropertyName = "Version";
            this.colAssemblyVersion.HeaderText = "Version";
            this.colAssemblyVersion.Name = "colAssemblyVersion";
            this.colAssemblyVersion.ReadOnly = true;
            this.colAssemblyVersion.Width = 67;
            // 
            // colAssemblyDescription
            // 
            this.colAssemblyDescription.DataPropertyName = "Description";
            this.colAssemblyDescription.HeaderText = "Description";
            this.colAssemblyDescription.Name = "colAssemblyDescription";
            this.colAssemblyDescription.ReadOnly = true;
            this.colAssemblyDescription.Width = 85;
            // 
            // colAssemblyPath
            // 
            this.colAssemblyPath.DataPropertyName = "Path";
            this.colAssemblyPath.HeaderText = "Path";
            this.colAssemblyPath.Name = "colAssemblyPath";
            this.colAssemblyPath.ReadOnly = true;
            this.colAssemblyPath.Width = 54;
            // 
            // dgProjects
            // 
            this.dgProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgProjects.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgProjects.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProjects.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colProjectSelected,
            this.colProjectPath});
            this.dgProjects.Location = new System.Drawing.Point(7, 35);
            this.dgProjects.MultiSelect = false;
            this.dgProjects.Name = "dgProjects";
            this.dgProjects.ReadOnly = true;
            this.dgProjects.Size = new System.Drawing.Size(426, 254);
            this.dgProjects.TabIndex = 8;
            // 
            // colProjectSelected
            // 
            this.colProjectSelected.DataPropertyName = "Selected";
            this.colProjectSelected.HeaderText = "Selected";
            this.colProjectSelected.Name = "colProjectSelected";
            this.colProjectSelected.ReadOnly = true;
            this.colProjectSelected.Width = 55;
            // 
            // colProjectPath
            // 
            this.colProjectPath.DataPropertyName = "Path";
            this.colProjectPath.HeaderText = "Path";
            this.colProjectPath.Name = "colProjectPath";
            this.colProjectPath.ReadOnly = true;
            this.colProjectPath.Width = 54;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(9, 6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.dgAssemblies);
            this.splitContainer1.Panel1.Controls.Add(this.btnAssemblyFilePath);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.dgProjects);
            this.splitContainer1.Panel2.Controls.Add(this.tbxSourePath);
            this.splitContainer1.Panel2.Controls.Add(this.btnBrowserSourcePath);
            this.splitContainer1.Size = new System.Drawing.Size(904, 301);
            this.splitContainer1.SplitterDistance = 443;
            this.splitContainer1.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(126, 313);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 31);
            this.button1.TabIndex = 10;
            this.button1.Text = "Clear All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 324);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Assembly Folder Name";
            // 
            // tbxAssemblyFolderName
            // 
            this.tbxAssemblyFolderName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbxAssemblyFolderName.Location = new System.Drawing.Point(368, 320);
            this.tbxAssemblyFolderName.Name = "tbxAssemblyFolderName";
            this.tbxAssemblyFolderName.Size = new System.Drawing.Size(88, 20);
            this.tbxAssemblyFolderName.TabIndex = 12;
            this.tbxAssemblyFolderName.Text = "ML";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 358);
            this.Controls.Add(this.tbxAssemblyFolderName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnFixAssembly);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fix ML Assemblies";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgAssemblies)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgProjects)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tbxSourePath;
		private System.Windows.Forms.Button btnBrowserSourcePath;
		private System.Windows.Forms.FolderBrowserDialog dlgSourcePath;
		private System.Windows.Forms.Button btnAssemblyFilePath;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.OpenFileDialog dlgAssembyFilePath;
		private System.Windows.Forms.Button btnFixAssembly;
		private System.Windows.Forms.DataGridView dgAssemblies;
		private System.Windows.Forms.DataGridView dgProjects;
		private System.Windows.Forms.DataGridViewCheckBoxColumn colProjectSelected;
		private System.Windows.Forms.DataGridViewTextBoxColumn colProjectPath;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.DataGridViewButtonColumn colAssemblyRemove;
		private System.Windows.Forms.DataGridViewTextBoxColumn colAssemblyName;
		private System.Windows.Forms.DataGridViewTextBoxColumn colAssemblyRuntime;
		private System.Windows.Forms.DataGridViewTextBoxColumn colAssemblyVersion;
		private System.Windows.Forms.DataGridViewTextBoxColumn colAssemblyDescription;
		private System.Windows.Forms.DataGridViewTextBoxColumn colAssemblyPath;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tbxAssemblyFolderName;
	}
}

