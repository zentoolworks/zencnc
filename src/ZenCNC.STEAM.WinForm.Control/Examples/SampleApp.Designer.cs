namespace ZenCNC.STEAM.WinForm.Examples
{
    partial class SampleAppForm
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
            this.droSample1 = new ZenCNC.STEAM.WinForm.Control.DROSample();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.bottomPanel = new System.Windows.Forms.TableLayoutPanel();
            this.joggingControl1 = new ZenCNC.STEAM.WinForm.Control.JoggingControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_stop = new System.Windows.Forms.Button();
            this.gCodeFileControl1 = new ZenCNC.STEAM.WinForm.Control.GCodeFileControl();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_ports = new System.Windows.Forms.ComboBox();
            this.cmb_baudrate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.updateParameterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.machinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMachineAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // droSample1
            // 
            this.droSample1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.droSample1.Location = new System.Drawing.Point(3, 3);
            this.droSample1.Name = "droSample1";
            this.droSample1.NumOfDigDisplay = 0;
            this.droSample1.Size = new System.Drawing.Size(263, 189);
            this.droSample1.TabIndex = 0;
            this.droSample1.X = 0D;
            this.droSample1.Y = 0D;
            this.droSample1.Z = 0D;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.bottomPanel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.gCodeFileControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(781, 453);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // bottomPanel
            // 
            this.bottomPanel.ColumnCount = 2;
            this.bottomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.6129F));
            this.bottomPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.3871F));
            this.bottomPanel.Controls.Add(this.joggingControl1, 1, 0);
            this.bottomPanel.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomPanel.Location = new System.Drawing.Point(3, 93);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.RowCount = 1;
            this.bottomPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.bottomPanel.Size = new System.Drawing.Size(775, 357);
            this.bottomPanel.TabIndex = 2;
            // 
            // joggingControl1
            // 
            this.joggingControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.joggingControl1.Location = new System.Drawing.Point(278, 3);
            this.joggingControl1.Name = "joggingControl1";
            this.joggingControl1.Size = new System.Drawing.Size(494, 351);
            this.joggingControl1.TabIndex = 1;
            this.joggingControl1.XValue = "";
            this.joggingControl1.YValue = "";
            this.joggingControl1.ZValue = "";
            this.joggingControl1.RunCommand += new System.EventHandler(this.joggingControl1_RunCommand);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.droSample1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_stop, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55.77689F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.22311F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(269, 351);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // btn_stop
            // 
            this.btn_stop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_stop.BackColor = System.Drawing.Color.Red;
            this.btn_stop.Font = new System.Drawing.Font("Swis721 BlkOul BT", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_stop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_stop.Location = new System.Drawing.Point(21, 204);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(226, 137);
            this.btn_stop.TabIndex = 1;
            this.btn_stop.Text = "E-STOP";
            this.btn_stop.UseVisualStyleBackColor = false;
            this.btn_stop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_stop_MouseDown);
            // 
            // gCodeFileControl1
            // 
            this.gCodeFileControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gCodeFileControl1.GCodeFile = null;
            this.gCodeFileControl1.Location = new System.Drawing.Point(3, 48);
            this.gCodeFileControl1.Name = "gCodeFileControl1";
            this.gCodeFileControl1.Size = new System.Drawing.Size(775, 39);
            this.gCodeFileControl1.TabIndex = 1;
            this.gCodeFileControl1.RunGCode += new System.EventHandler(this.gCodeFileControl1_RunGCode);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.90076F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 77.09924F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 139F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tableLayoutPanel3.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmb_ports, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmb_baudrate, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.button1, 4, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(775, 39);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "COM Port :";
            // 
            // cmb_ports
            // 
            this.cmb_ports.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmb_ports.FormattingEnabled = true;
            this.cmb_ports.Location = new System.Drawing.Point(185, 9);
            this.cmb_ports.Name = "cmb_ports";
            this.cmb_ports.Size = new System.Drawing.Size(156, 21);
            this.cmb_ports.TabIndex = 0;
            // 
            // cmb_baudrate
            // 
            this.cmb_baudrate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmb_baudrate.FormattingEnabled = true;
            this.cmb_baudrate.Items.AddRange(new object[] {
            "115200"});
            this.cmb_baudrate.Location = new System.Drawing.Point(533, 9);
            this.cmb_baudrate.Name = "cmb_baudrate";
            this.cmb_baudrate.Size = new System.Drawing.Size(121, 21);
            this.cmb_baudrate.TabIndex = 2;
            this.cmb_baudrate.Text = "115200";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(444, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Baud Rate :";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(681, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateParameterToolStripMenuItem,
            this.machinesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(781, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // updateParameterToolStripMenuItem
            // 
            this.updateParameterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.saveMachineAsToolStripMenuItem,
            this.exltToolStripMenuItem});
            this.updateParameterToolStripMenuItem.Name = "updateParameterToolStripMenuItem";
            this.updateParameterToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.updateParameterToolStripMenuItem.Text = "File";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // exltToolStripMenuItem
            // 
            this.exltToolStripMenuItem.Name = "exltToolStripMenuItem";
            this.exltToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exltToolStripMenuItem.Text = "Exit";
            this.exltToolStripMenuItem.Click += new System.EventHandler(this.exltToolStripMenuItem_Click);
            // 
            // machinesToolStripMenuItem
            // 
            this.machinesToolStripMenuItem.Name = "machinesToolStripMenuItem";
            this.machinesToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.machinesToolStripMenuItem.Text = "Machines";
            this.machinesToolStripMenuItem.Click += new System.EventHandler(this.machinesToolStripMenuItem_Click);
            // 
            // saveMachineAsToolStripMenuItem
            // 
            this.saveMachineAsToolStripMenuItem.Name = "saveMachineAsToolStripMenuItem";
            this.saveMachineAsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.saveMachineAsToolStripMenuItem.Text = "Save Machine As";
            this.saveMachineAsToolStripMenuItem.Click += new System.EventHandler(this.saveMachineAsToolStripMenuItem_Click);
            // 
            // SampleAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 477);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SampleAppForm";
            this.Text = "Zen Toolworks Sample App";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.bottomPanel.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZenCNC.STEAM.WinForm.Control.DROSample droSample1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ZenCNC.STEAM.WinForm.Control.GCodeFileControl gCodeFileControl1;
        private System.Windows.Forms.TableLayoutPanel bottomPanel;
        private ZenCNC.STEAM.WinForm.Control.JoggingControl joggingControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem updateParameterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exltToolStripMenuItem;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_ports;
        private System.Windows.Forms.ComboBox cmb_baudrate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem machinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMachineAsToolStripMenuItem;
    }
}