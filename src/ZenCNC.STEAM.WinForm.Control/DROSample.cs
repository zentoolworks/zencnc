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
    public partial class DROSample : UserControl
    {
        private double x;
        private double y;
        private double z;

        public int NumOfDigDisplay { get; set; }
        public double X
        {
            get { return x; }
            set { x = value; }
        }
        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        public double Z
        {
            get { return z; }
            set { z = value; }
        }
        public void Update(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Refresh();
        }

        
        delegate void ReturningVoidDelegate(string status);
        delegate void ThreeArgVoidDelegate(string state, int currentLine, int totalLine);
        public void UpdateGrblStatus(string status)
        {
            if(this.lbl_grbl_status.InvokeRequired)
            {
                ReturningVoidDelegate d = new ReturningVoidDelegate(UpdateGrblStatus);
                this.Invoke(d, new object[] { status});
            }
            else
            {
                this.lbl_grbl_status.Text = status;
            }
        }

        public void UpdateGCodeStatus(string status, int currentLine, int totalLine)
        {
            if(this.lbl_gcode_status.InvokeRequired)
            {
                ThreeArgVoidDelegate d = new ThreeArgVoidDelegate(UpdateGCodeStatus);
                this.Invoke(d, new Object[] { status, currentLine, totalLine });
            }
            else
            {
                this.lbl_gcode_status.Text = currentLine + "/" + totalLine;
                this.lbl_gcode_file.Text = status;
            }
        }
        delegate void StringArgReturningVoidDelegate();
        private void Refresh()
        {
            if (this.lbl_x.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(Refresh);
                this.Invoke(d, new object[] {});
            }
            else
            {
                this.lbl_x.Text = X.ToString("0.00");
                this.lbl_y.Text = Y.ToString("0.00");
                this.lbl_z.Text = Z.ToString("0.00");

            }
        }

        public DROSample()
        {
            InitializeComponent();
        }
    }
}
