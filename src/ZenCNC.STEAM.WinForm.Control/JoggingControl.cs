using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZenCNC.STEAM.WinForm.Control
{
    public partial class JoggingControl : UserControl
    {
        public JoggingControl()
        {
            InitializeComponent();

            DisableCusPanel();
            DisableJobPanel();
        }

        private string jogType = "CONT";

        private string custMoveType = "ABS";

        public void Disable()
        {
            DisableCusPanel();
            DisableJobPanel();
        }

        public void Enable()
        {
            DisableCusPanel();
            EnableJobPanel();
        }
        public string XValue
        {
            get
            {
                return this.txt_x.Text;
            }
            set
            {
                this.txt_x.Text = value;
            }
        }
        public string YValue
        {
            get
            {
                return this.txt_y.Text;
            }
            set
            {
                this.txt_y.Text = value;
            }
        }
        public string ZValue
        {
            get
            {
                return this.txt_z.Text;
            }
            set
            {
                this.txt_z.Text = value;
            }
        }

        public string XYStepSize
        {
            get
            {
                return editfd_xystepsize.GetValue();
            }
        }

        public string ZStepSize
        {
            get
            {
                return editfld_zstepsize.GetValue();
            }
        }

        public string JogFeedrate
        {
            get
            {
                return editfld_jogfeedrate.GetValue();
            }
        }

        private void commandButton1_Load(object sender, EventArgs e)
        {

        }

        private void commandButton8_Load(object sender, EventArgs e)
        {

        }


        private void commandButton_RunCommand(object sender, EventArgs e)
        {
            OnRunCommand((CommandEventArgs)e);
        }

        public event EventHandler RunCommand;
        protected virtual void OnRunCommand(CommandEventArgs e)
        {
            EventHandler handler = RunCommand;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJogType();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJogType();
        }

        private void UpdateJogType()
        {
            if(radioButton5.Checked)
            {
                jogType = "CUST";
                EnableCusPanel();
                DisableJobPanel();
            }
            else if(radioButton4.Checked)
            {
                jogType = "STEP";
                DisableCusPanel();
                EnableJobPanel();
                EnableJogButtonClick();
            }
            else if(radioButton3.Checked)
            {
                jogType = "CONT";
                DisableCusPanel();
                EnableJobPanel();
                DisableJobButtonClick();
            }

        }

        delegate void ZeroArgReturningVoidDelegate();
        private void EnableJogButtonClick()
        {
            if (this.commandButton1.InvokeRequired)
            {
                ZeroArgReturningVoidDelegate d = new ZeroArgReturningVoidDelegate(EnableJogButtonClick);
                this.Invoke(d, new object[] { });
            }
            else
            {
                commandButton1.IsClick = true;
                commandButton2.IsClick = true;
                commandButton3.IsClick = true;
                commandButton4.IsClick = true;
                commandButton5.IsClick = true;
                commandButton6.IsClick = true;
            }


        }

        private void DisableJobButtonClick()
        {
            if (this.commandButton1.InvokeRequired)
            {
                ZeroArgReturningVoidDelegate d = new ZeroArgReturningVoidDelegate(DisableJobButtonClick);
                this.Invoke(d, new object[] { });
            }
            else
            {
                commandButton1.IsClick = false;
                commandButton2.IsClick = false;
                commandButton3.IsClick = false;
                commandButton4.IsClick = false;
                commandButton5.IsClick = false;
                commandButton6.IsClick = false;
            }
        }


        private void DisableCusPanel()
        {
            if (this.commandButton1.InvokeRequired)
            {
                ZeroArgReturningVoidDelegate d = new ZeroArgReturningVoidDelegate(DisableCusPanel);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.btn_go.Enabled = false;
                this.radioButton1.Enabled = false;
                this.radioButton2.Enabled = false;
                this.lbl_x.Enabled = false;
                this.lbl_y.Enabled = false;
                this.lbl_z.Enabled = false;
                this.txt_x.Enabled = false;
                this.txt_y.Enabled = false;
                this.txt_z.Enabled = false;
            }
        }

        private void EnableCusPanel()
        {
            if (this.commandButton1.InvokeRequired)
            {
                ZeroArgReturningVoidDelegate d = new ZeroArgReturningVoidDelegate(EnableCusPanel);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.btn_go.Enabled = true;
                this.radioButton1.Enabled = true;
                this.radioButton2.Enabled = true;
                this.lbl_x.Enabled = true;
                this.lbl_y.Enabled = true;
                this.lbl_z.Enabled = true;
                this.txt_x.Enabled = true;
                this.txt_y.Enabled = true;
                this.txt_z.Enabled = true;
            }
        }
        private void DisableJobPanel()
        {
            if (this.commandButton1.InvokeRequired)
            {
                ZeroArgReturningVoidDelegate d = new ZeroArgReturningVoidDelegate(DisableJobPanel);
                this.Invoke(d, new object[] { });
            }
            else
            {
                commandButton1.Enabled = false;
                commandButton2.Enabled = false;
                commandButton3.Enabled = false;
                commandButton4.Enabled = false;
                commandButton5.Enabled = false;
                commandButton6.Enabled = false;
                commandButton7.Enabled = false;
                commandButton8.Enabled = false;
                commandButton9.Enabled = false;
            }
        }

        private void EnableJobPanel()
        {
            if (this.commandButton1.InvokeRequired)
            {
                ZeroArgReturningVoidDelegate d = new ZeroArgReturningVoidDelegate(EnableJobPanel);
                this.Invoke(d, new object[] { });
            }
            else
            {
                commandButton1.Enabled = true;
                commandButton2.Enabled = true;
                commandButton3.Enabled = true;
                commandButton4.Enabled = true;
                commandButton5.Enabled = true;
                commandButton6.Enabled = true;
                commandButton7.Enabled = true;
                commandButton8.Enabled = true;
                commandButton9.Enabled = true;
            }
        }


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateJogType();

        }

        private void btn_go_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                custMoveType = "ABSMOVE";
            else
            {
                custMoveType = "RELMOVE";
            }
            CommandEventArgs args = new CommandEventArgs();
            args.Command = custMoveType;
            OnRunCommand(args);

        }
    }
}
