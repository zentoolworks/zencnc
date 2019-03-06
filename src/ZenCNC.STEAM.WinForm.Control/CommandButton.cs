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
    public partial class CommandButton : UserControl
    {
        public string GCodeCommand { get; set; }

        public string DownCommand { get; set; }

        public string UpCommand { get; set; }

        public bool IsClick { get; set; }

        public string Title {
            get
            {
                return this.Button.Text;
            }
            set
            {
                this.Button.Text = value;
            }
        }

        public CommandButton()
        {
            InitializeComponent();
            

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


        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if(!IsClick)
            {
                CommandEventArgs args = new CommandEventArgs();
                args.Command = DownCommand;
                OnRunCommand(args);
            }
        }

        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsClick)
            {
                CommandEventArgs args = new CommandEventArgs();
                args.Command = GCodeCommand;
                OnRunCommand(args);
            }
            else
            {
                CommandEventArgs args = new CommandEventArgs();
                args.Command = UpCommand;
                OnRunCommand(args);
            }
        }
    }

    
}
