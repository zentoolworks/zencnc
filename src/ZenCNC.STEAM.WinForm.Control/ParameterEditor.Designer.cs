using System;
using System.Windows.Forms;

namespace ZenCNC.STEAM.WinForm.Control
{
    partial class ParameterEditor
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_desc = new System.Windows.Forms.Label();
            this.lbl_message = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_unit = new System.Windows.Forms.Label();
            this.ckb_z = new System.Windows.Forms.CheckBox();
            this.ckb_y = new System.Windows.Forms.CheckBox();
            this.ckb_x = new System.Windows.Forms.CheckBox();
            this.txt_text = new System.Windows.Forms.TextBox();
            this.cmb_params = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lbl_desc, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl_message, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cmb_params, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(371, 168);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbl_desc
            // 
            this.lbl_desc.AutoSize = true;
            this.lbl_desc.Location = new System.Drawing.Point(2, 33);
            this.lbl_desc.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_desc.Name = "lbl_desc";
            this.lbl_desc.Size = new System.Drawing.Size(60, 13);
            this.lbl_desc.TabIndex = 0;
            this.lbl_desc.Text = "Description";
            // 
            // lbl_message
            // 
            this.lbl_message.AutoSize = true;
            this.lbl_message.Location = new System.Drawing.Point(2, 66);
            this.lbl_message.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_message.Name = "lbl_message";
            this.lbl_message.Size = new System.Drawing.Size(50, 13);
            this.lbl_message.TabIndex = 1;
            this.lbl_message.Text = "Message";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.47956F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 102);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(367, 27);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this.lbl_unit);
            this.panel1.Controls.Add(this.ckb_z);
            this.panel1.Controls.Add(this.ckb_y);
            this.panel1.Controls.Add(this.ckb_x);
            this.panel1.Controls.Add(this.txt_text);
            this.panel1.Location = new System.Drawing.Point(74, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(218, 24);
            this.panel1.TabIndex = 1;
            // 
            // lbl_unit
            // 
            this.lbl_unit.AutoSize = true;
            this.lbl_unit.Location = new System.Drawing.Point(150, 12);
            this.lbl_unit.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_unit.Name = "lbl_unit";
            this.lbl_unit.Size = new System.Drawing.Size(0, 13);
            this.lbl_unit.TabIndex = 4;
            // 
            // ckb_z
            // 
            this.ckb_z.AutoSize = true;
            this.ckb_z.Location = new System.Drawing.Point(82, 1);
            this.ckb_z.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckb_z.Name = "ckb_z";
            this.ckb_z.Size = new System.Drawing.Size(33, 17);
            this.ckb_z.TabIndex = 3;
            this.ckb_z.Text = "Z";
            this.ckb_z.UseVisualStyleBackColor = true;
            // 
            // ckb_y
            // 
            this.ckb_y.AutoSize = true;
            this.ckb_y.Location = new System.Drawing.Point(44, 1);
            this.ckb_y.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckb_y.Name = "ckb_y";
            this.ckb_y.Size = new System.Drawing.Size(33, 17);
            this.ckb_y.TabIndex = 2;
            this.ckb_y.Text = "Y";
            this.ckb_y.UseVisualStyleBackColor = true;
            // 
            // ckb_x
            // 
            this.ckb_x.AutoSize = true;
            this.ckb_x.Location = new System.Drawing.Point(2, 1);
            this.ckb_x.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ckb_x.Name = "ckb_x";
            this.ckb_x.Size = new System.Drawing.Size(33, 17);
            this.ckb_x.TabIndex = 1;
            this.ckb_x.Text = "X";
            this.ckb_x.UseVisualStyleBackColor = true;
            // 
            // txt_text
            // 
            this.txt_text.Location = new System.Drawing.Point(0, 0);
            this.txt_text.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txt_text.Name = "txt_text";
            this.txt_text.Size = new System.Drawing.Size(160, 20);
            this.txt_text.TabIndex = 0;
            this.txt_text.TextChanged += new System.EventHandler(this.txt_text_TextChanged);
            this.txt_text.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_text_KeyDown);
            // 
            // cmb_params
            // 
            this.cmb_params.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmb_params.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_params.FormattingEnabled = true;
            this.cmb_params.Location = new System.Drawing.Point(3, 3);
            this.cmb_params.Name = "cmb_params";
            this.cmb_params.Size = new System.Drawing.Size(365, 21);
            this.cmb_params.TabIndex = 3;
            this.cmb_params.SelectedIndexChanged += new System.EventHandler(this.cmb_params_SelectedIndexChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.Controls.Add(this.btn_Update, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_reset, 3, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 135);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(364, 30);
            this.tableLayoutPanel3.TabIndex = 4;
            // 
            // btn_Update
            // 
            this.btn_Update.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Update.Location = new System.Drawing.Point(38, 3);
            this.btn_Update.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(123, 24);
            this.btn_Update.TabIndex = 0;
            this.btn_Update.Text = "Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_reset.Location = new System.Drawing.Point(202, 3);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(121, 24);
            this.btn_reset.TabIndex = 1;
            this.btn_reset.Text = "Cancel";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // ParameterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "ParameterEditor";
            this.Size = new System.Drawing.Size(371, 168);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_desc;
        private System.Windows.Forms.Label lbl_message;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox ckb_z;
        private System.Windows.Forms.CheckBox ckb_y;
        private System.Windows.Forms.CheckBox ckb_x;
        private System.Windows.Forms.TextBox txt_text;
        private System.Windows.Forms.Label lbl_unit;
        private ComboBox cmb_params;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btn_reset;
    }
}
