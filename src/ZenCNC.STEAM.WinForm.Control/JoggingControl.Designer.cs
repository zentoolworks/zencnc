namespace ZenCNC.STEAM.WinForm.Control
{
    partial class JoggingControl
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
            this.commandButton9 = new ZenCNC.STEAM.WinForm.Control.CommandButton();
            this.commandButton8 = new ZenCNC.STEAM.WinForm.Control.CommandButton();
            this.commandButton1 = new ZenCNC.STEAM.WinForm.Control.CommandButton();
            this.commandButton2 = new ZenCNC.STEAM.WinForm.Control.CommandButton();
            this.commandButton3 = new ZenCNC.STEAM.WinForm.Control.CommandButton();
            this.commandButton4 = new ZenCNC.STEAM.WinForm.Control.CommandButton();
            this.commandButton5 = new ZenCNC.STEAM.WinForm.Control.CommandButton();
            this.commandButton6 = new ZenCNC.STEAM.WinForm.Control.CommandButton();
            this.commandButton7 = new ZenCNC.STEAM.WinForm.Control.CommandButton();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.editfld_jogfeedrate = new ZenCNC.STEAM.WinForm.Control.EditableField();
            this.editfld_zstepsize = new ZenCNC.STEAM.WinForm.Control.EditableField();
            this.editfd_xystepsize = new ZenCNC.STEAM.WinForm.Control.EditableField();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.txt_z = new System.Windows.Forms.TextBox();
            this.lbl_z = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.txt_y = new System.Windows.Forms.TextBox();
            this.lbl_y = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_x = new System.Windows.Forms.Label();
            this.txt_x = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.btn_go = new System.Windows.Forms.Button();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.Controls.Add(this.commandButton9, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.commandButton8, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.commandButton1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.commandButton2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.commandButton3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.commandButton4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.commandButton5, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.commandButton6, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.commandButton7, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 175);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(613, 178);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // commandButton9
            // 
            this.commandButton9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandButton9.DownCommand = null;
            this.commandButton9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandButton9.GCodeCommand = "UNLOCK";
            this.commandButton9.IsClick = true;
            this.commandButton9.Location = new System.Drawing.Point(5, 121);
            this.commandButton9.Margin = new System.Windows.Forms.Padding(5);
            this.commandButton9.Name = "commandButton9";
            this.commandButton9.Size = new System.Drawing.Size(192, 52);
            this.commandButton9.TabIndex = 8;
            this.commandButton9.Title = "Unlock";
            this.commandButton9.UpCommand = null;
            this.commandButton9.RunCommand += new System.EventHandler(this.commandButton_RunCommand);
            // 
            // commandButton8
            // 
            this.commandButton8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandButton8.DownCommand = null;
            this.commandButton8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandButton8.GCodeCommand = "ZEROALL";
            this.commandButton8.IsClick = true;
            this.commandButton8.Location = new System.Drawing.Point(5, 5);
            this.commandButton8.Margin = new System.Windows.Forms.Padding(5);
            this.commandButton8.Name = "commandButton8";
            this.commandButton8.Size = new System.Drawing.Size(192, 48);
            this.commandButton8.TabIndex = 7;
            this.commandButton8.Title = "Zero All";
            this.commandButton8.UpCommand = null;
            this.commandButton8.RunCommand += new System.EventHandler(this.commandButton_RunCommand);
            this.commandButton8.Load += new System.EventHandler(this.commandButton8_Load);
            // 
            // commandButton1
            // 
            this.commandButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandButton1.DownCommand = "JOGZ+";
            this.commandButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandButton1.GCodeCommand = "STEPZ+";
            this.commandButton1.IsClick = false;
            this.commandButton1.Location = new System.Drawing.Point(409, 5);
            this.commandButton1.Margin = new System.Windows.Forms.Padding(5);
            this.commandButton1.Name = "commandButton1";
            this.commandButton1.Size = new System.Drawing.Size(199, 48);
            this.commandButton1.TabIndex = 0;
            this.commandButton1.Title = "Z+";
            this.commandButton1.UpCommand = "STOPJOG";
            this.commandButton1.RunCommand += new System.EventHandler(this.commandButton_RunCommand);
            this.commandButton1.Load += new System.EventHandler(this.commandButton1_Load);
            // 
            // commandButton2
            // 
            this.commandButton2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandButton2.DownCommand = "JOGZ-";
            this.commandButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandButton2.GCodeCommand = "STEPZ-";
            this.commandButton2.IsClick = false;
            this.commandButton2.Location = new System.Drawing.Point(409, 121);
            this.commandButton2.Margin = new System.Windows.Forms.Padding(5);
            this.commandButton2.Name = "commandButton2";
            this.commandButton2.Size = new System.Drawing.Size(199, 52);
            this.commandButton2.TabIndex = 1;
            this.commandButton2.Title = "Z-";
            this.commandButton2.UpCommand = "STOPJOG";
            this.commandButton2.RunCommand += new System.EventHandler(this.commandButton_RunCommand);
            // 
            // commandButton3
            // 
            this.commandButton3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandButton3.DownCommand = "JOGX-";
            this.commandButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandButton3.GCodeCommand = "STEPX-";
            this.commandButton3.IsClick = false;
            this.commandButton3.Location = new System.Drawing.Point(5, 63);
            this.commandButton3.Margin = new System.Windows.Forms.Padding(5);
            this.commandButton3.Name = "commandButton3";
            this.commandButton3.Size = new System.Drawing.Size(192, 48);
            this.commandButton3.TabIndex = 2;
            this.commandButton3.Title = "X -";
            this.commandButton3.UpCommand = "STOPJOG";
            this.commandButton3.RunCommand += new System.EventHandler(this.commandButton_RunCommand);
            // 
            // commandButton4
            // 
            this.commandButton4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandButton4.DownCommand = "JOGX+";
            this.commandButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandButton4.GCodeCommand = "STEPX+";
            this.commandButton4.IsClick = false;
            this.commandButton4.Location = new System.Drawing.Point(409, 63);
            this.commandButton4.Margin = new System.Windows.Forms.Padding(5);
            this.commandButton4.Name = "commandButton4";
            this.commandButton4.Size = new System.Drawing.Size(199, 48);
            this.commandButton4.TabIndex = 3;
            this.commandButton4.Title = "X +";
            this.commandButton4.UpCommand = "STOPJOG";
            this.commandButton4.RunCommand += new System.EventHandler(this.commandButton_RunCommand);
            // 
            // commandButton5
            // 
            this.commandButton5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandButton5.DownCommand = "JOGY+";
            this.commandButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandButton5.GCodeCommand = "STEPY+";
            this.commandButton5.IsClick = false;
            this.commandButton5.Location = new System.Drawing.Point(207, 5);
            this.commandButton5.Margin = new System.Windows.Forms.Padding(5);
            this.commandButton5.Name = "commandButton5";
            this.commandButton5.Size = new System.Drawing.Size(192, 48);
            this.commandButton5.TabIndex = 4;
            this.commandButton5.Title = "Y +";
            this.commandButton5.UpCommand = "STOPJOG";
            this.commandButton5.RunCommand += new System.EventHandler(this.commandButton_RunCommand);
            // 
            // commandButton6
            // 
            this.commandButton6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandButton6.DownCommand = "JOGY-";
            this.commandButton6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandButton6.GCodeCommand = "STEPY-";
            this.commandButton6.IsClick = false;
            this.commandButton6.Location = new System.Drawing.Point(207, 121);
            this.commandButton6.Margin = new System.Windows.Forms.Padding(5);
            this.commandButton6.Name = "commandButton6";
            this.commandButton6.Size = new System.Drawing.Size(192, 52);
            this.commandButton6.TabIndex = 5;
            this.commandButton6.Title = "Y -";
            this.commandButton6.UpCommand = "STOPJOG";
            this.commandButton6.RunCommand += new System.EventHandler(this.commandButton_RunCommand);
            // 
            // commandButton7
            // 
            this.commandButton7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandButton7.DownCommand = null;
            this.commandButton7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commandButton7.GCodeCommand = "HOME";
            this.commandButton7.IsClick = true;
            this.commandButton7.Location = new System.Drawing.Point(207, 63);
            this.commandButton7.Margin = new System.Windows.Forms.Padding(5);
            this.commandButton7.Name = "commandButton7";
            this.commandButton7.Size = new System.Drawing.Size(192, 48);
            this.commandButton7.TabIndex = 6;
            this.commandButton7.Title = "Home";
            this.commandButton7.UpCommand = null;
            this.commandButton7.RunCommand += new System.EventHandler(this.commandButton_RunCommand);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.3871F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.6129F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(619, 356);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.editfld_jogfeedrate, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.editfld_zstepsize, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.editfd_xystepsize, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel8, 1, 2);
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel9, 1, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(613, 166);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // editfld_jogfeedrate
            // 
            this.editfld_jogfeedrate.Decimal = 0;
            this.editfld_jogfeedrate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editfld_jogfeedrate.Incremental = 100D;
            this.editfld_jogfeedrate.Location = new System.Drawing.Point(3, 56);
            this.editfld_jogfeedrate.MaxValue = 1000D;
            this.editfld_jogfeedrate.MinValue = 0D;
            this.editfld_jogfeedrate.Name = "editfld_jogfeedrate";
            this.editfld_jogfeedrate.Size = new System.Drawing.Size(300, 47);
            this.editfld_jogfeedrate.TabIndex = 2;
            this.editfld_jogfeedrate.Title = "Jogging Feedrate";
            this.editfld_jogfeedrate.Value = 100D;
            // 
            // editfld_zstepsize
            // 
            this.editfld_zstepsize.Decimal = 0;
            this.editfld_zstepsize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editfld_zstepsize.Incremental = 1D;
            this.editfld_zstepsize.Location = new System.Drawing.Point(309, 3);
            this.editfld_zstepsize.MaxValue = 30D;
            this.editfld_zstepsize.MinValue = 0D;
            this.editfld_zstepsize.Name = "editfld_zstepsize";
            this.editfld_zstepsize.Size = new System.Drawing.Size(301, 47);
            this.editfld_zstepsize.TabIndex = 1;
            this.editfld_zstepsize.Title = "Z Step Size";
            this.editfld_zstepsize.Value = 1D;
            // 
            // editfd_xystepsize
            // 
            this.editfd_xystepsize.Decimal = 0;
            this.editfd_xystepsize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editfd_xystepsize.Incremental = 1D;
            this.editfd_xystepsize.Location = new System.Drawing.Point(3, 3);
            this.editfd_xystepsize.MaxValue = 50D;
            this.editfd_xystepsize.MinValue = 0D;
            this.editfd_xystepsize.Name = "editfd_xystepsize";
            this.editfd_xystepsize.Size = new System.Drawing.Size(300, 47);
            this.editfd_xystepsize.TabIndex = 0;
            this.editfd_xystepsize.Title = "X, Y Step Size";
            this.editfd_xystepsize.Value = 10D;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel7, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 109);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(300, 54);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.txt_z, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.lbl_z, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(201, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(96, 48);
            this.tableLayoutPanel7.TabIndex = 2;
            // 
            // txt_z
            // 
            this.txt_z.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_z.Location = new System.Drawing.Point(3, 27);
            this.txt_z.Name = "txt_z";
            this.txt_z.Size = new System.Drawing.Size(90, 20);
            this.txt_z.TabIndex = 2;
            // 
            // lbl_z
            // 
            this.lbl_z.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_z.AutoSize = true;
            this.lbl_z.Location = new System.Drawing.Point(38, 5);
            this.lbl_z.Name = "lbl_z";
            this.lbl_z.Size = new System.Drawing.Size(20, 13);
            this.lbl_z.TabIndex = 1;
            this.lbl_z.Text = "Z :";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.txt_y, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.lbl_y, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(102, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(93, 48);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // txt_y
            // 
            this.txt_y.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_y.Location = new System.Drawing.Point(3, 27);
            this.txt_y.Name = "txt_y";
            this.txt_y.Size = new System.Drawing.Size(87, 20);
            this.txt_y.TabIndex = 2;
            // 
            // lbl_y
            // 
            this.lbl_y.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_y.AutoSize = true;
            this.lbl_y.Location = new System.Drawing.Point(36, 5);
            this.lbl_y.Name = "lbl_y";
            this.lbl_y.Size = new System.Drawing.Size(20, 13);
            this.lbl_y.TabIndex = 1;
            this.lbl_y.Text = "Y :";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.lbl_x, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.txt_x, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(93, 48);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // lbl_x
            // 
            this.lbl_x.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbl_x.AutoSize = true;
            this.lbl_x.Location = new System.Drawing.Point(36, 5);
            this.lbl_x.Name = "lbl_x";
            this.lbl_x.Size = new System.Drawing.Size(20, 13);
            this.lbl_x.TabIndex = 0;
            this.lbl_x.Text = "X :";
            // 
            // txt_x
            // 
            this.txt_x.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_x.Location = new System.Drawing.Point(3, 27);
            this.txt_x.Name = "txt_x";
            this.txt_x.Size = new System.Drawing.Size(87, 20);
            this.txt_x.TabIndex = 1;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 3;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel8.Controls.Add(this.radioButton2, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.radioButton1, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.btn_go, 2, 0);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(309, 109);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(301, 54);
            this.tableLayoutPanel8.TabIndex = 4;
            // 
            // radioButton2
            // 
            this.radioButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(113, 18);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(44, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Rel.";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(22, 18);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(46, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Abs.";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // btn_go
            // 
            this.btn_go.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_go.Location = new System.Drawing.Point(203, 15);
            this.btn_go.Name = "btn_go";
            this.btn_go.Size = new System.Drawing.Size(75, 23);
            this.btn_go.TabIndex = 2;
            this.btn_go.Text = "Go!";
            this.btn_go.UseVisualStyleBackColor = true;
            this.btn_go.Click += new System.EventHandler(this.btn_go_Click);
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 3;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel9.Controls.Add(this.radioButton5, 2, 0);
            this.tableLayoutPanel9.Controls.Add(this.radioButton4, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.radioButton3, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(309, 56);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(301, 47);
            this.tableLayoutPanel9.TabIndex = 5;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButton5.Location = new System.Drawing.Point(223, 3);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(75, 41);
            this.radioButton5.TabIndex = 2;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Custom";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButton4.Location = new System.Drawing.Point(113, 3);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(104, 41);
            this.radioButton4.TabIndex = 1;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Step";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Checked = true;
            this.radioButton3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioButton3.Location = new System.Drawing.Point(3, 3);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(104, 41);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Continue";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // JoggingControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "JoggingControl";
            this.Size = new System.Drawing.Size(619, 356);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private CommandButton commandButton9;
        private CommandButton commandButton8;
        private CommandButton commandButton1;
        private CommandButton commandButton2;
        private CommandButton commandButton3;
        private CommandButton commandButton4;
        private CommandButton commandButton5;
        private CommandButton commandButton6;
        private CommandButton commandButton7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private EditableField editfld_jogfeedrate;
        private EditableField editfld_zstepsize;
        private EditableField editfd_xystepsize;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TextBox txt_z;
        private System.Windows.Forms.Label lbl_z;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TextBox txt_y;
        private System.Windows.Forms.Label lbl_y;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label lbl_x;
        private System.Windows.Forms.TextBox txt_x;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Button btn_go;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
    }
}
