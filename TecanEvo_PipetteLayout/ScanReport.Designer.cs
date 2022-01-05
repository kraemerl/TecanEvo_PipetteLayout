namespace TecanEvo_PipetteLayout
{
    partial class ScanReport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblMethod = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.grdTransferOverview = new System.Windows.Forms.DataGridView();
            this.source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.patient_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sample_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.source_barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.source_volume = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.source_concentration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destination_well = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destination_patient_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sample_needed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buffer_needed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTargetVolume = new System.Windows.Forms.Label();
            this.lblTargetConcentration = new System.Windows.Forms.Label();
            this.lblTargetConcentrationText = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.butContinue = new System.Windows.Forms.Button();
            this.butCancel = new System.Windows.Forms.Button();
            this.lblSampleDestination = new System.Windows.Forms.Label();
            this.lblSampleSource = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSampleDestinationPlate = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDropSenseMeasurement = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferOverview)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMethod
            // 
            this.lblMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMethod.AutoSize = true;
            this.lblMethod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMethod.Location = new System.Drawing.Point(740, 9);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(28, 13);
            this.lblMethod.TabIndex = 38;
            this.lblMethod.Text = "???";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(590, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "Method:";
            // 
            // grdTransferOverview
            // 
            this.grdTransferOverview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTransferOverview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.grdTransferOverview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.source,
            this.patient_id,
            this.sample_id,
            this.source_barcode,
            this.category,
            this.source_volume,
            this.source_concentration,
            this.destination_well,
            this.destination_patient_id,
            this.sample_needed,
            this.buffer_needed});
            this.grdTransferOverview.Location = new System.Drawing.Point(12, 91);
            this.grdTransferOverview.Name = "grdTransferOverview";
            this.grdTransferOverview.ReadOnly = true;
            this.grdTransferOverview.Size = new System.Drawing.Size(813, 412);
            this.grdTransferOverview.TabIndex = 36;
            // 
            // source
            // 
            this.source.HeaderText = "source";
            this.source.Name = "source";
            this.source.ReadOnly = true;
            this.source.Width = 64;
            // 
            // patient_id
            // 
            this.patient_id.HeaderText = "patient_id";
            this.patient_id.Name = "patient_id";
            this.patient_id.ReadOnly = true;
            this.patient_id.Width = 78;
            // 
            // sample_id
            // 
            this.sample_id.HeaderText = "sample_id";
            this.sample_id.Name = "sample_id";
            this.sample_id.ReadOnly = true;
            this.sample_id.Width = 79;
            // 
            // source_barcode
            // 
            this.source_barcode.HeaderText = "source BC";
            this.source_barcode.Name = "source_barcode";
            this.source_barcode.ReadOnly = true;
            this.source_barcode.Width = 81;
            // 
            // category
            // 
            this.category.HeaderText = "category";
            this.category.Name = "category";
            this.category.ReadOnly = true;
            this.category.Width = 73;
            // 
            // source_volume
            // 
            this.source_volume.HeaderText = "volume (µl)";
            this.source_volume.Name = "source_volume";
            this.source_volume.ReadOnly = true;
            this.source_volume.Width = 83;
            // 
            // source_concentration
            // 
            this.source_concentration.HeaderText = "conc (ng/µl)";
            this.source_concentration.Name = "source_concentration";
            this.source_concentration.ReadOnly = true;
            this.source_concentration.Width = 90;
            // 
            // destination_well
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.destination_well.DefaultCellStyle = dataGridViewCellStyle5;
            this.destination_well.HeaderText = "well";
            this.destination_well.Name = "destination_well";
            this.destination_well.ReadOnly = true;
            this.destination_well.Width = 50;
            // 
            // destination_patient_id
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.destination_patient_id.DefaultCellStyle = dataGridViewCellStyle6;
            this.destination_patient_id.HeaderText = "patient_id";
            this.destination_patient_id.Name = "destination_patient_id";
            this.destination_patient_id.ReadOnly = true;
            this.destination_patient_id.Width = 78;
            // 
            // sample_needed
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.sample_needed.DefaultCellStyle = dataGridViewCellStyle7;
            this.sample_needed.HeaderText = "sample (µl)";
            this.sample_needed.Name = "sample_needed";
            this.sample_needed.ReadOnly = true;
            this.sample_needed.Width = 82;
            // 
            // buffer_needed
            // 
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.buffer_needed.DefaultCellStyle = dataGridViewCellStyle8;
            this.buffer_needed.HeaderText = "TE (µl)";
            this.buffer_needed.Name = "buffer_needed";
            this.buffer_needed.ReadOnly = true;
            this.buffer_needed.Width = 63;
            // 
            // lblTargetVolume
            // 
            this.lblTargetVolume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTargetVolume.AutoSize = true;
            this.lblTargetVolume.Location = new System.Drawing.Point(740, 29);
            this.lblTargetVolume.Name = "lblTargetVolume";
            this.lblTargetVolume.Size = new System.Drawing.Size(25, 13);
            this.lblTargetVolume.TabIndex = 35;
            this.lblTargetVolume.Text = "???";
            // 
            // lblTargetConcentration
            // 
            this.lblTargetConcentration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTargetConcentration.AutoSize = true;
            this.lblTargetConcentration.Location = new System.Drawing.Point(740, 46);
            this.lblTargetConcentration.Name = "lblTargetConcentration";
            this.lblTargetConcentration.Size = new System.Drawing.Size(25, 13);
            this.lblTargetConcentration.TabIndex = 34;
            this.lblTargetConcentration.Text = "???";
            // 
            // lblTargetConcentrationText
            // 
            this.lblTargetConcentrationText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTargetConcentrationText.AutoSize = true;
            this.lblTargetConcentrationText.Location = new System.Drawing.Point(590, 46);
            this.lblTargetConcentrationText.Name = "lblTargetConcentrationText";
            this.lblTargetConcentrationText.Size = new System.Drawing.Size(144, 13);
            this.lblTargetConcentrationText.TabIndex = 33;
            this.lblTargetConcentrationText.Text = "Target Concentration (ng/µl):";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(590, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Target Volume (µl):";
            // 
            // butContinue
            // 
            this.butContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butContinue.Enabled = false;
            this.butContinue.Location = new System.Drawing.Point(669, 511);
            this.butContinue.Name = "butContinue";
            this.butContinue.Size = new System.Drawing.Size(75, 23);
            this.butContinue.TabIndex = 40;
            this.butContinue.Text = "Continue";
            this.butContinue.UseVisualStyleBackColor = true;
            this.butContinue.Click += new System.EventHandler(this.butContinue_Click);
            // 
            // butCancel
            // 
            this.butCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.butCancel.Location = new System.Drawing.Point(750, 511);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 39;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // lblSampleDestination
            // 
            this.lblSampleDestination.AutoSize = true;
            this.lblSampleDestination.Location = new System.Drawing.Point(119, 29);
            this.lblSampleDestination.Name = "lblSampleDestination";
            this.lblSampleDestination.Size = new System.Drawing.Size(25, 13);
            this.lblSampleDestination.TabIndex = 44;
            this.lblSampleDestination.Text = "???";
            // 
            // lblSampleSource
            // 
            this.lblSampleSource.AutoSize = true;
            this.lblSampleSource.Location = new System.Drawing.Point(119, 9);
            this.lblSampleSource.Name = "lblSampleSource";
            this.lblSampleSource.Size = new System.Drawing.Size(25, 13);
            this.lblSampleSource.TabIndex = 43;
            this.lblSampleSource.Text = "???";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Sample Destination:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Sample Sources:";
            // 
            // lblSampleDestinationPlate
            // 
            this.lblSampleDestinationPlate.AutoSize = true;
            this.lblSampleDestinationPlate.Location = new System.Drawing.Point(119, 46);
            this.lblSampleDestinationPlate.Name = "lblSampleDestinationPlate";
            this.lblSampleDestinationPlate.Size = new System.Drawing.Size(25, 13);
            this.lblSampleDestinationPlate.TabIndex = 46;
            this.lblSampleDestinationPlate.Text = "???";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "Destination Plate:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(590, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(130, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "DropSense Measurement:";
            // 
            // lblDropSenseMeasurement
            // 
            this.lblDropSenseMeasurement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDropSenseMeasurement.AutoSize = true;
            this.lblDropSenseMeasurement.Location = new System.Drawing.Point(740, 65);
            this.lblDropSenseMeasurement.Name = "lblDropSenseMeasurement";
            this.lblDropSenseMeasurement.Size = new System.Drawing.Size(21, 13);
            this.lblDropSenseMeasurement.TabIndex = 48;
            this.lblDropSenseMeasurement.Text = "No";
            // 
            // ScanReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 546);
            this.Controls.Add(this.lblDropSenseMeasurement);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblSampleDestinationPlate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblSampleDestination);
            this.Controls.Add(this.lblSampleSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.butContinue);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.lblMethod);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.grdTransferOverview);
            this.Controls.Add(this.lblTargetVolume);
            this.Controls.Add(this.lblTargetConcentration);
            this.Controls.Add(this.lblTargetConcentrationText);
            this.Controls.Add(this.label3);
            this.Name = "ScanReport";
            this.Text = "EVO: Scan Report";
            this.Load += new System.EventHandler(this.ScanReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdTransferOverview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView grdTransferOverview;
        private System.Windows.Forms.Label lblTargetVolume;
        private System.Windows.Forms.Label lblTargetConcentration;
        private System.Windows.Forms.Label lblTargetConcentrationText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butContinue;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Label lblSampleDestination;
        private System.Windows.Forms.Label lblSampleSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSampleDestinationPlate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn source;
        private System.Windows.Forms.DataGridViewTextBoxColumn patient_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn sample_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn source_barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn category;
        private System.Windows.Forms.DataGridViewTextBoxColumn source_volume;
        private System.Windows.Forms.DataGridViewTextBoxColumn source_concentration;
        private System.Windows.Forms.DataGridViewTextBoxColumn destination_well;
        private System.Windows.Forms.DataGridViewTextBoxColumn destination_patient_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn sample_needed;
        private System.Windows.Forms.DataGridViewTextBoxColumn buffer_needed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDropSenseMeasurement;
    }
}