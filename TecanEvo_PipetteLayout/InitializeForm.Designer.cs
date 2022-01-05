namespace TecanEvo_PipetteLayout
{
    partial class InitializeForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSourcePlate3BoxBarcode = new System.Windows.Forms.TextBox();
            this.chkSourcePlate3 = new System.Windows.Forms.CheckBox();
            this.txtSourcePlate2BoxBarcode = new System.Windows.Forms.TextBox();
            this.chkSourcePlate2 = new System.Windows.Forms.CheckBox();
            this.txtSourcePlate1BoxBarcode = new System.Windows.Forms.TextBox();
            this.chkSourcePlate1 = new System.Windows.Forms.CheckBox();
            this.chkSourceMicronic07 = new System.Windows.Forms.CheckBox();
            this.chkSourceMicronic14 = new System.Windows.Forms.CheckBox();
            this.chkSourceEppi = new System.Windows.Forms.CheckBox();
            this.txtVolume = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkNormalize = new System.Windows.Forms.CheckBox();
            this.txtConcentration = new System.Windows.Forms.TextBox();
            this.lblConcentration = new System.Windows.Forms.Label();
            this.butCancel = new System.Windows.Forms.Button();
            this.butOkay = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbDestinationIScanPlate = new System.Windows.Forms.RadioButton();
            this.txtPlateBarcode = new System.Windows.Forms.TextBox();
            this.rbDestinationDeepWellPlate = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rbDestinationPCRPlate = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.chkDropSense = new System.Windows.Forms.CheckBox();
            this.chkMix = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSourcePlate3BoxBarcode);
            this.groupBox1.Controls.Add(this.chkSourcePlate3);
            this.groupBox1.Controls.Add(this.txtSourcePlate2BoxBarcode);
            this.groupBox1.Controls.Add(this.chkSourcePlate2);
            this.groupBox1.Controls.Add(this.txtSourcePlate1BoxBarcode);
            this.groupBox1.Controls.Add(this.chkSourcePlate1);
            this.groupBox1.Controls.Add(this.chkSourceMicronic07);
            this.groupBox1.Controls.Add(this.chkSourceMicronic14);
            this.groupBox1.Controls.Add(this.chkSourceEppi);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 122);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "used Sources";
            // 
            // txtSourcePlate3BoxBarcode
            // 
            this.txtSourcePlate3BoxBarcode.Location = new System.Drawing.Point(126, 95);
            this.txtSourcePlate3BoxBarcode.MaxLength = 30;
            this.txtSourcePlate3BoxBarcode.Name = "txtSourcePlate3BoxBarcode";
            this.txtSourcePlate3BoxBarcode.Size = new System.Drawing.Size(144, 20);
            this.txtSourcePlate3BoxBarcode.TabIndex = 23;
            this.txtSourcePlate3BoxBarcode.Visible = false;
            // 
            // chkSourcePlate3
            // 
            this.chkSourcePlate3.AutoSize = true;
            this.chkSourcePlate3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSourcePlate3.Location = new System.Drawing.Point(6, 95);
            this.chkSourcePlate3.Name = "chkSourcePlate3";
            this.chkSourcePlate3.Size = new System.Drawing.Size(112, 17);
            this.chkSourcePlate3.TabIndex = 22;
            this.chkSourcePlate3.Text = "PCR Plate3 (BOX)";
            this.chkSourcePlate3.UseVisualStyleBackColor = true;
            this.chkSourcePlate3.CheckedChanged += new System.EventHandler(this.chkSourcePlate3_CheckedChanged);
            // 
            // txtSourcePlate2BoxBarcode
            // 
            this.txtSourcePlate2BoxBarcode.Location = new System.Drawing.Point(126, 68);
            this.txtSourcePlate2BoxBarcode.MaxLength = 30;
            this.txtSourcePlate2BoxBarcode.Name = "txtSourcePlate2BoxBarcode";
            this.txtSourcePlate2BoxBarcode.Size = new System.Drawing.Size(144, 20);
            this.txtSourcePlate2BoxBarcode.TabIndex = 21;
            this.txtSourcePlate2BoxBarcode.Visible = false;
            // 
            // chkSourcePlate2
            // 
            this.chkSourcePlate2.AutoSize = true;
            this.chkSourcePlate2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSourcePlate2.Location = new System.Drawing.Point(6, 68);
            this.chkSourcePlate2.Name = "chkSourcePlate2";
            this.chkSourcePlate2.Size = new System.Drawing.Size(112, 17);
            this.chkSourcePlate2.TabIndex = 20;
            this.chkSourcePlate2.Text = "PCR Plate2 (BOX)";
            this.chkSourcePlate2.UseVisualStyleBackColor = true;
            this.chkSourcePlate2.CheckedChanged += new System.EventHandler(this.chkSourcePlate2_CheckedChanged);
            // 
            // txtSourcePlate1BoxBarcode
            // 
            this.txtSourcePlate1BoxBarcode.Location = new System.Drawing.Point(126, 42);
            this.txtSourcePlate1BoxBarcode.MaxLength = 30;
            this.txtSourcePlate1BoxBarcode.Name = "txtSourcePlate1BoxBarcode";
            this.txtSourcePlate1BoxBarcode.Size = new System.Drawing.Size(144, 20);
            this.txtSourcePlate1BoxBarcode.TabIndex = 19;
            this.txtSourcePlate1BoxBarcode.Visible = false;
            // 
            // chkSourcePlate1
            // 
            this.chkSourcePlate1.AutoSize = true;
            this.chkSourcePlate1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSourcePlate1.Location = new System.Drawing.Point(6, 42);
            this.chkSourcePlate1.Name = "chkSourcePlate1";
            this.chkSourcePlate1.Size = new System.Drawing.Size(112, 17);
            this.chkSourcePlate1.TabIndex = 4;
            this.chkSourcePlate1.Text = "PCR Plate1 (BOX)";
            this.chkSourcePlate1.UseVisualStyleBackColor = true;
            this.chkSourcePlate1.CheckedChanged += new System.EventHandler(this.chkSourcePlate1_CheckedChanged);
            // 
            // chkSourceMicronic07
            // 
            this.chkSourceMicronic07.AutoSize = true;
            this.chkSourceMicronic07.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSourceMicronic07.Location = new System.Drawing.Point(178, 19);
            this.chkSourceMicronic07.Name = "chkSourceMicronic07";
            this.chkSourceMicronic07.Size = new System.Drawing.Size(94, 17);
            this.chkSourceMicronic07.TabIndex = 3;
            this.chkSourceMicronic07.Text = "Micronic 0.7ml";
            this.chkSourceMicronic07.UseVisualStyleBackColor = true;
            this.chkSourceMicronic07.CheckedChanged += new System.EventHandler(this.chkSourceMicronic07_CheckedChanged);
            // 
            // chkSourceMicronic14
            // 
            this.chkSourceMicronic14.AutoSize = true;
            this.chkSourceMicronic14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSourceMicronic14.Location = new System.Drawing.Point(78, 19);
            this.chkSourceMicronic14.Name = "chkSourceMicronic14";
            this.chkSourceMicronic14.Size = new System.Drawing.Size(94, 17);
            this.chkSourceMicronic14.TabIndex = 2;
            this.chkSourceMicronic14.Text = "Micronic 1.4ml";
            this.chkSourceMicronic14.UseVisualStyleBackColor = true;
            this.chkSourceMicronic14.CheckedChanged += new System.EventHandler(this.chkSourceMicronic14_CheckedChanged);
            // 
            // chkSourceEppi
            // 
            this.chkSourceEppi.AutoSize = true;
            this.chkSourceEppi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSourceEppi.Location = new System.Drawing.Point(6, 19);
            this.chkSourceEppi.Name = "chkSourceEppi";
            this.chkSourceEppi.Size = new System.Drawing.Size(66, 17);
            this.chkSourceEppi.TabIndex = 1;
            this.chkSourceEppi.Text = "Eppi 2ml";
            this.chkSourceEppi.UseVisualStyleBackColor = true;
            this.chkSourceEppi.CheckedChanged += new System.EventHandler(this.chkSourceEppi_CheckedChanged);
            // 
            // txtVolume
            // 
            this.txtVolume.Location = new System.Drawing.Point(167, 222);
            this.txtVolume.MaxLength = 4;
            this.txtVolume.Name = "txtVolume";
            this.txtVolume.Size = new System.Drawing.Size(54, 20);
            this.txtVolume.TabIndex = 8;
            this.txtVolume.Text = "0";
            this.txtVolume.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtVolume.TextChanged += new System.EventHandler(this.txtVolume_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Target Volume (µl):";
            // 
            // chkNormalize
            // 
            this.chkNormalize.AutoSize = true;
            this.chkNormalize.Location = new System.Drawing.Point(18, 256);
            this.chkNormalize.Name = "chkNormalize";
            this.chkNormalize.Size = new System.Drawing.Size(73, 17);
            this.chkNormalize.TabIndex = 9;
            this.chkNormalize.Text = "normalize:";
            this.chkNormalize.UseVisualStyleBackColor = true;
            this.chkNormalize.CheckedChanged += new System.EventHandler(this.chkNormalize_CheckedChanged);
            // 
            // txtConcentration
            // 
            this.txtConcentration.Location = new System.Drawing.Point(191, 273);
            this.txtConcentration.MaxLength = 5;
            this.txtConcentration.Name = "txtConcentration";
            this.txtConcentration.Size = new System.Drawing.Size(54, 20);
            this.txtConcentration.TabIndex = 10;
            this.txtConcentration.Text = "0";
            this.txtConcentration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtConcentration.Visible = false;
            this.txtConcentration.TextChanged += new System.EventHandler(this.txtConcentration_TextChanged);
            // 
            // lblConcentration
            // 
            this.lblConcentration.AutoSize = true;
            this.lblConcentration.Location = new System.Drawing.Point(39, 276);
            this.lblConcentration.Name = "lblConcentration";
            this.lblConcentration.Size = new System.Drawing.Size(144, 13);
            this.lblConcentration.TabIndex = 17;
            this.lblConcentration.Text = "Target Concentration (ng/µl):";
            this.lblConcentration.Visible = false;
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.Location = new System.Drawing.Point(208, 351);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 12;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOkay
            // 
            this.butOkay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butOkay.Location = new System.Drawing.Point(124, 351);
            this.butOkay.Name = "butOkay";
            this.butOkay.Size = new System.Drawing.Size(75, 23);
            this.butOkay.TabIndex = 11;
            this.butOkay.Text = "OK";
            this.butOkay.UseVisualStyleBackColor = true;
            this.butOkay.Click += new System.EventHandler(this.butOkay_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbDestinationIScanPlate);
            this.groupBox2.Controls.Add(this.txtPlateBarcode);
            this.groupBox2.Controls.Add(this.rbDestinationDeepWellPlate);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.rbDestinationPCRPlate);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 140);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(276, 68);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destination";
            // 
            // rbDestinationIScanPlate
            // 
            this.rbDestinationIScanPlate.AutoSize = true;
            this.rbDestinationIScanPlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDestinationIScanPlate.Location = new System.Drawing.Point(189, 19);
            this.rbDestinationIScanPlate.Name = "rbDestinationIScanPlate";
            this.rbDestinationIScanPlate.Size = new System.Drawing.Size(79, 17);
            this.rbDestinationIScanPlate.TabIndex = 19;
            this.rbDestinationIScanPlate.TabStop = true;
            this.rbDestinationIScanPlate.Text = "iScan Plate";
            this.rbDestinationIScanPlate.UseVisualStyleBackColor = true;
            this.rbDestinationIScanPlate.CheckedChanged += new System.EventHandler(this.rbDestinationIScanPlate_CheckedChanged);
            // 
            // txtPlateBarcode
            // 
            this.txtPlateBarcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlateBarcode.Location = new System.Drawing.Point(103, 42);
            this.txtPlateBarcode.MaxLength = 30;
            this.txtPlateBarcode.Name = "txtPlateBarcode";
            this.txtPlateBarcode.Size = new System.Drawing.Size(165, 20);
            this.txtPlateBarcode.TabIndex = 7;
            // 
            // rbDestinationDeepWellPlate
            // 
            this.rbDestinationDeepWellPlate.AutoSize = true;
            this.rbDestinationDeepWellPlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDestinationDeepWellPlate.Location = new System.Drawing.Point(86, 19);
            this.rbDestinationDeepWellPlate.Name = "rbDestinationDeepWellPlate";
            this.rbDestinationDeepWellPlate.Size = new System.Drawing.Size(99, 17);
            this.rbDestinationDeepWellPlate.TabIndex = 6;
            this.rbDestinationDeepWellPlate.TabStop = true;
            this.rbDestinationDeepWellPlate.Text = "DeepWell Plate";
            this.rbDestinationDeepWellPlate.UseVisualStyleBackColor = true;
            this.rbDestinationDeepWellPlate.CheckedChanged += new System.EventHandler(this.rbDestinationDeepWellPlate_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Plate Barcode:";
            // 
            // rbDestinationPCRPlate
            // 
            this.rbDestinationPCRPlate.AutoSize = true;
            this.rbDestinationPCRPlate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDestinationPCRPlate.Location = new System.Drawing.Point(6, 19);
            this.rbDestinationPCRPlate.Name = "rbDestinationPCRPlate";
            this.rbDestinationPCRPlate.Size = new System.Drawing.Size(74, 17);
            this.rbDestinationPCRPlate.TabIndex = 5;
            this.rbDestinationPCRPlate.TabStop = true;
            this.rbDestinationPCRPlate.Text = "PCR Plate";
            this.rbDestinationPCRPlate.UseVisualStyleBackColor = true;
            this.rbDestinationPCRPlate.CheckedChanged += new System.EventHandler(this.rbDestinationPCRPlate_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Target Volume (µl):";
            // 
            // chkDropSense
            // 
            this.chkDropSense.AutoSize = true;
            this.chkDropSense.Checked = true;
            this.chkDropSense.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDropSense.Enabled = false;
            this.chkDropSense.Location = new System.Drawing.Point(42, 299);
            this.chkDropSense.Name = "chkDropSense";
            this.chkDropSense.Size = new System.Drawing.Size(145, 17);
            this.chkDropSense.TabIndex = 47;
            this.chkDropSense.Text = "DropSense measurement";
            this.chkDropSense.UseVisualStyleBackColor = true;
            this.chkDropSense.CheckedChanged += new System.EventHandler(this.chkDropSense_CheckedChanged);
            // 
            // chkMix
            // 
            this.chkMix.AutoSize = true;
            this.chkMix.Checked = true;
            this.chkMix.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMix.Enabled = false;
            this.chkMix.Location = new System.Drawing.Point(42, 322);
            this.chkMix.Name = "chkMix";
            this.chkMix.Size = new System.Drawing.Size(204, 17);
            this.chkMix.TabIndex = 53;
            this.chkMix.Text = "Mix liquid before DropSense pipetting ";
            this.chkMix.UseVisualStyleBackColor = true;
            this.chkMix.CheckedChanged += new System.EventHandler(this.chkMix_CheckedChanged);
            // 
            // InitializeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 385);
            this.Controls.Add(this.chkMix);
            this.Controls.Add(this.chkDropSense);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOkay);
            this.Controls.Add(this.chkNormalize);
            this.Controls.Add(this.txtConcentration);
            this.Controls.Add(this.lblConcentration);
            this.Controls.Add(this.txtVolume);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(311, 424);
            this.MinimumSize = new System.Drawing.Size(311, 424);
            this.Name = "InitializeForm";
            this.Text = "EVO: Pipette Layout";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkSourcePlate1;
        private System.Windows.Forms.CheckBox chkSourceMicronic07;
        private System.Windows.Forms.CheckBox chkSourceMicronic14;
        private System.Windows.Forms.CheckBox chkSourceEppi;
        private System.Windows.Forms.TextBox txtVolume;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkNormalize;
        private System.Windows.Forms.TextBox txtConcentration;
        private System.Windows.Forms.Label lblConcentration;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butOkay;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbDestinationDeepWellPlate;
        private System.Windows.Forms.RadioButton rbDestinationPCRPlate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlateBarcode;
        private System.Windows.Forms.TextBox txtSourcePlate1BoxBarcode;
        private System.Windows.Forms.RadioButton rbDestinationIScanPlate;
        private System.Windows.Forms.TextBox txtSourcePlate3BoxBarcode;
        private System.Windows.Forms.CheckBox chkSourcePlate3;
        private System.Windows.Forms.TextBox txtSourcePlate2BoxBarcode;
        private System.Windows.Forms.CheckBox chkSourcePlate2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkDropSense;
        private System.Windows.Forms.CheckBox chkMix;
    }
}

