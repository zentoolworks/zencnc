using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZenCNC.STEAM.grbl;

namespace ZenCNC.STEAM.WinForm.Examples
{
    public partial class ParamForm : Form
    {
        public GrblClient grbl = null;
        public ParamForm(GrblClient gc)
        {
            InitializeComponent();
            grbl = gc;
        }

        private void parameterEditor1_Load(object sender, EventArgs e)
        {
            this.parameterEditor1.SetGrbl(grbl);
            this.parameterEditor1.UpdateParameter();
        }

        public void RefreshParameters()
        {
            this.parameterEditor1.UpdateParameter();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
