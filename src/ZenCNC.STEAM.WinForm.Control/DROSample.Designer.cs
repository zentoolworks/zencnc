namespace ZenCNC.STEAM.WinForm.Control
{
    partial class DROSample
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_x = new System.Windows.Forms.Label();
            this.lbl_y = new System.Windows.Forms.Label();
            this.lbl_z = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_grbl_status = new System.Windows.Forms.Label();
            this.lbl_gcode_file = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_gcode_status = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X :";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y :";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 298);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Z :";
            // 
            // lbl_x
            // 
            this.lbl_x.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_x.AutoSize = true;
            this.lbl_x.Location = new System.Drawing.Point(336, 186);
            this.lbl_x.Name = "lbl_x";
            this.lbl_x.Size = new System.Drawing.Size(28, 13);
            this.lbl_x.TabIndex = 3;
            this.lbl_x.Text = "0.00";
            // 
            // lbl_y
            // 
            this.lbl_y.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_y.AutoSize = true;
            this.lbl_y.Location = new System.Drawing.Point(336, 241);
            this.lbl_y.Name = "lbl_y";
            this.lbl_y.Size = new System.Drawing.Size(28, 13);
            this.lbl_y.TabIndex = 4;
            this.lbl_y.Text = "0.00";
            // 
            // lbl_z
            // 
            this.lbl_z.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_z.AutoSize = true;
            this.lbl_z.Location = new System.Drawing.Point(336, 298);
            this.lbl_z.Name = "lbl_z";
            this.lbl_z.Size = new System.Drawing.Size(28, 13);
            this.lbl_z.TabIndex = 5;
            this.lbl_z.Text = "0.00";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Grbl Status :";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "G Code File :";
            // 
            // lbl_grbl_status
            // 
            this.lbl_grbl_status.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_grbl_status.AutoSize = true;
            this.lbl_grbl_status.Location = new System.Drawing.Point(336, 21);
            this.lbl_grbl_status.Name = "lbl_grbl_status";
            this.lbl_grbl_status.Size = new System.Drawing.Size(31, 13);
            this.lbl_grbl_status.TabIndex = 8;
            this.lbl_grbl_status.Text = "none";
            // 
            // lbl_gcode_file
            // 
            this.lbl_gcode_file.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_gcode_file.AutoSize = true;
            this.lbl_gcode_file.Location = new System.Drawing.Point(336, 76);
            this.lbl_gcode_file.Name = "lbl_gcode_file";
            this.lbl_gcode_file.Size = new System.Drawing.Size(31, 13);
            this.lbl_gcode_file.TabIndex = 9;
            this.lbl_gcode_file.Text = "none";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 131);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "G Code Status :";
            // 
            // lbl_gcode_status
            // 
            this.lbl_gcode_status.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_gcode_status.AutoSize = true;
            this.lbl_gcode_status.Location = new System.Drawing.Point(336, 131);
            this.lbl_gcode_status.Name = "lbl_gcode_status";
            this.lbl_gcode_status.Size = new System.Drawing.Size(31, 13);
            this.lbl_gcode_status.TabIndex = 11;
            this.lbl_gcode_status.Text = "none";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lbl_z, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.lbl_gcode_status, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_y, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_x, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lbl_gcode_file, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_grbl_status, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 5);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(667, 334);
            this.tableLayoutPanel1.TabIndex = 12;
            // 
            // DROSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DROSample";
            this.Size = new System.Drawing.Size(667, 334);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_x;
        private System.Windows.Forms.Label lbl_y;
        private System.Windows.Forms.Label lbl_z;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_grbl_status;
        private System.Windows.Forms.Label lbl_gcode_file;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_gcode_status;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
