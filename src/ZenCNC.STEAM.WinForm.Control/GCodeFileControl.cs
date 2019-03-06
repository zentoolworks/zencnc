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
    public partial class GCodeFileControl : UserControl
    {
        public string GCodeFile { get; set; }
        public GCodeFileControl()
        {
            InitializeComponent();
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.ShowDialog();
            if(fileDlg.FileName != null && fileDlg.FileName.Length > 0)
            {
                GCodeFile = fileDlg.FileName;
            }
            this.label1.Text = GCodeFile;
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            RunGCodeEventArgs args = new RunGCodeEventArgs();
            args.FilePath = this.GCodeFile;
            OnRunGCodeEvent(args);
        }

        public event EventHandler RunGCode;
        protected virtual void OnRunGCodeEvent(RunGCodeEventArgs e)
        {
            EventHandler handler = RunGCode;
            if (handler != null)
            {
                handler(this, e);
            }
        }


    }
    public class RunGCodeEventArgs : EventArgs
    {
        public string FilePath { get; set; }
    }
}
