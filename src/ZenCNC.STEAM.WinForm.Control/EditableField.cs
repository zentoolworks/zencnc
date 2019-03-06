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
    public partial class EditableField : UserControl
    {
        public string Title { get; set; }

        public int Decimal { get; set; }

        public double Incremental { get; set; }

        public double MaxValue { get; set; }

        public double MinValue { get; set; }

        public double Value { get; set; }

        public EditableField()
        {
            InitializeComponent();

            this.textbox.MouseWheel += Textbox_MouseWheel;


        }

        private void Textbox_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = e.Delta;
            if(delta > 0)
            {
                if (Value + Incremental <= MaxValue)
                {
                    Value += Incremental;
                }
            }
            else
            {
                if (Value - Incremental >= MinValue)
                {
                    Value -= Incremental;
                }
            }

            this.textbox.Text = Value.ToString();
        }

        private void EditableField_Load(object sender, EventArgs e)
        {
            this.label1.Text = Title;

            this.textbox.Text = Value.ToString();
        }

        public string GetValue()
        {
            return this.textbox.Text;
        }
    }
}
