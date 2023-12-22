namespace WorldofWatches.Console
{
    partial class FormMain
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
            this.grbInfo = new System.Windows.Forms.GroupBox();
            this.cmsProductTypes = new WorldofWatches.Console.ControlMultiSelect();
            this.cmsBrands = new WorldofWatches.Console.ControlMultiSelect();
            this.nudStartRecordIndex = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudDelaySecondsEachProduct = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nudDelaySecondsEachPage = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nudStartPage = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpdatePriceProducts = new System.Windows.Forms.Button();
            this.btnGetProducts = new System.Windows.Forms.Button();
            this.rtbReport = new System.Windows.Forms.RichTextBox();
            this.lbnClear = new System.Windows.Forms.LinkLabel();
            this.grbInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartRecordIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelaySecondsEachProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelaySecondsEachPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartPage)).BeginInit();
            this.SuspendLayout();
            // 
            // grbInfo
            // 
            this.grbInfo.Controls.Add(this.cmsProductTypes);
            this.grbInfo.Controls.Add(this.cmsBrands);
            this.grbInfo.Controls.Add(this.nudStartRecordIndex);
            this.grbInfo.Controls.Add(this.label6);
            this.grbInfo.Controls.Add(this.nudDelaySecondsEachProduct);
            this.grbInfo.Controls.Add(this.label5);
            this.grbInfo.Controls.Add(this.nudDelaySecondsEachPage);
            this.grbInfo.Controls.Add(this.label4);
            this.grbInfo.Controls.Add(this.nudStartPage);
            this.grbInfo.Controls.Add(this.label3);
            this.grbInfo.Controls.Add(this.label2);
            this.grbInfo.Controls.Add(this.label1);
            this.grbInfo.Location = new System.Drawing.Point(14, 9);
            this.grbInfo.Name = "grbInfo";
            this.grbInfo.Size = new System.Drawing.Size(268, 361);
            this.grbInfo.TabIndex = 0;
            this.grbInfo.TabStop = false;
            // 
            // cmsProductTypes
            // 
            this.cmsProductTypes.Location = new System.Drawing.Point(24, 47);
            this.cmsProductTypes.Name = "cmsProductTypes";
            this.cmsProductTypes.Size = new System.Drawing.Size(223, 28);
            this.cmsProductTypes.TabIndex = 13;
            // 
            // cmsBrands
            // 
            this.cmsBrands.Location = new System.Drawing.Point(24, 99);
            this.cmsBrands.Name = "cmsBrands";
            this.cmsBrands.Size = new System.Drawing.Size(223, 28);
            this.cmsBrands.TabIndex = 12;
            // 
            // nudStartRecordIndex
            // 
            this.nudStartRecordIndex.Location = new System.Drawing.Point(26, 209);
            this.nudStartRecordIndex.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStartRecordIndex.Name = "nudStartRecordIndex";
            this.nudStartRecordIndex.Size = new System.Drawing.Size(214, 21);
            this.nudStartRecordIndex.TabIndex = 11;
            this.nudStartRecordIndex.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Start Record Index:";
            // 
            // nudDelaySecondsEachProduct
            // 
            this.nudDelaySecondsEachProduct.Location = new System.Drawing.Point(26, 313);
            this.nudDelaySecondsEachProduct.Name = "nudDelaySecondsEachProduct";
            this.nudDelaySecondsEachProduct.Size = new System.Drawing.Size(214, 21);
            this.nudDelaySecondsEachProduct.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 290);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(175, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "Delay Second(s) each Product:";
            // 
            // nudDelaySecondsEachPage
            // 
            this.nudDelaySecondsEachPage.Location = new System.Drawing.Point(26, 261);
            this.nudDelaySecondsEachPage.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDelaySecondsEachPage.Name = "nudDelaySecondsEachPage";
            this.nudDelaySecondsEachPage.Size = new System.Drawing.Size(214, 21);
            this.nudDelaySecondsEachPage.TabIndex = 7;
            this.nudDelaySecondsEachPage.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(162, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Delay Second(s) each Page:";
            // 
            // nudStartPage
            // 
            this.nudStartPage.Location = new System.Drawing.Point(26, 157);
            this.nudStartPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStartPage.Name = "nudStartPage";
            this.nudStartPage.Size = new System.Drawing.Size(214, 21);
            this.nudStartPage.TabIndex = 5;
            this.nudStartPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Start Page:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Brand(s):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Product(s):";
            // 
            // btnUpdatePriceProducts
            // 
            this.btnUpdatePriceProducts.Location = new System.Drawing.Point(106, 383);
            this.btnUpdatePriceProducts.Name = "btnUpdatePriceProducts";
            this.btnUpdatePriceProducts.Size = new System.Drawing.Size(143, 33);
            this.btnUpdatePriceProducts.TabIndex = 5;
            this.btnUpdatePriceProducts.Text = "Update Price Products";
            this.btnUpdatePriceProducts.UseVisualStyleBackColor = true;
            this.btnUpdatePriceProducts.Click += new System.EventHandler(this.btnUpdatePriceProducts_Click);
            // 
            // btnGetProducts
            // 
            this.btnGetProducts.Location = new System.Drawing.Point(14, 383);
            this.btnGetProducts.Name = "btnGetProducts";
            this.btnGetProducts.Size = new System.Drawing.Size(86, 33);
            this.btnGetProducts.TabIndex = 4;
            this.btnGetProducts.Text = "Get Products";
            this.btnGetProducts.UseVisualStyleBackColor = true;
            this.btnGetProducts.Click += new System.EventHandler(this.btnGetProducts_Click);
            // 
            // rtbReport
            // 
            this.rtbReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbReport.BackColor = System.Drawing.Color.White;
            this.rtbReport.Location = new System.Drawing.Point(299, 14);
            this.rtbReport.Name = "rtbReport";
            this.rtbReport.ReadOnly = true;
            this.rtbReport.Size = new System.Drawing.Size(350, 440);
            this.rtbReport.TabIndex = 1;
            this.rtbReport.Text = "";
            // 
            // lbnClear
            // 
            this.lbnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbnClear.AutoSize = true;
            this.lbnClear.Location = new System.Drawing.Point(211, 437);
            this.lbnClear.Name = "lbnClear";
            this.lbnClear.Size = new System.Drawing.Size(76, 15);
            this.lbnClear.TabIndex = 6;
            this.lbnClear.TabStop = true;
            this.lbnClear.Text = "Clear Report";
            this.lbnClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbnClear_LinkClicked);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 466);
            this.Controls.Add(this.lbnClear);
            this.Controls.Add(this.btnUpdatePriceProducts);
            this.Controls.Add(this.rtbReport);
            this.Controls.Add(this.btnGetProducts);
            this.Controls.Add(this.grbInfo);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.grbInfo.ResumeLayout(false);
            this.grbInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartRecordIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelaySecondsEachProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelaySecondsEachPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartPage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbReport;
        private System.Windows.Forms.Button btnGetProducts;
        private System.Windows.Forms.Button btnUpdatePriceProducts;
        private System.Windows.Forms.NumericUpDown nudDelaySecondsEachProduct;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudDelaySecondsEachPage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudStartPage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudStartRecordIndex;
        private System.Windows.Forms.Label label6;
        private ControlMultiSelect cmsBrands;
        private ControlMultiSelect cmsProductTypes;
        private System.Windows.Forms.LinkLabel lbnClear;
    }
}