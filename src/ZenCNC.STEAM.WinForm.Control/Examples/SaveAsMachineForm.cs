using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZenCNC.STEAM.WinForm.Control.Examples
{
    public partial class SaveAsMachineForm : Form
    {
        public bool Ok { get; set; }

        public string Name { get; set; }
        public string Id { get; set; }
        public string Model { get; set; }
        public SaveAsMachineForm()
        {
            InitializeComponent();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Ok = true;
            Name = txt_name.Text;
            Model = txt_model.Text;
            Id = txt_id.Text;
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Ok = false;
            this.Close();
        }
    }
}
