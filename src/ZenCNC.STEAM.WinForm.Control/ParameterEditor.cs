using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZenCNC.STEAM.grbl;

namespace ZenCNC.STEAM.WinForm.Control
{
    public partial class ParameterEditor : UserControl
    {
        private EditorType _editorType;

        private bool _checkX = false;
        private bool _checkY = false;
        private bool _checkZ = false;
        private bool _checkTrue = false;
        private int _intVal = 0;
        private float _floatVal = 0;
        private string _textVal = string.Empty;
        private bool _boolval = false;
        private string unitStr = string.Empty;
        private string _descriptioin = string.Empty;
        private string _message = string.Empty;
        private GrblClient grbl = null;
        public void SetGrbl(GrblClient gc)
        {
            grbl = gc;
        }
        public bool Enable
        {
            set
            {
                if (value)
                {
                    this.btn_Update.Enabled = true;

                }
                else
                {
                    this.btn_Update.Enabled = false;
                }
            }
        }

        public string Description
        {
            set
            {
                _descriptioin = value;
                lbl_desc.Text = _descriptioin;
            }
        }

        public event EventHandler Update;

        public string Value
        {
            get
            {
                string result = _textVal;
                switch (EditorType)
                {
                    case EditorType.Mask:
                        string s = string.Empty;
                        s += Convert.ToInt32(ckb_z.Checked).ToString();
                        s += Convert.ToInt32(ckb_y.Checked).ToString();
                        s += Convert.ToInt32(ckb_x.Checked).ToString();
                        int output = Convert.ToInt32(s, 2);

                        return output.ToString();
                        break;
                    case EditorType.Bool:
                        return Convert.ToInt32(ckb_x.Checked).ToString();
                        break;
                    default:
                        break;
                }
                return _textVal;
            }

        }

        private void UpdateEvent()
        {
            //Null check makes sure the main page is attached to the event
            if (this.Update != null)
                this.Update(this, new EventArgs());
        }

        private string PadLeft(string o, int len, string padChar)
        {
            string prefix = string.Empty;
            for (int i = 0; i < len - o.Length; i++)
            {
                prefix += padChar;
            }
            return prefix + o;
        }

        public string GetValue()
        {
            string ret = string.Empty;
            switch(EditorType)
            {
                case EditorType.Mask:
                    int total = 0;
                    if (ckb_x.Checked)
                        total += 1;
                    if (ckb_y.Checked)
                        total += 2;
                    if (ckb_z.Checked)
                        total += 4;
                    ret = total.ToString();
                    break;
                case EditorType.Float:
                    ret = this.txt_text.Text;
                    break;
                case EditorType.Bool:
                    if (ckb_x.Checked)
                        ret = "1";
                    else
                        ret = "0";
                    break;
            }
            return ret;
        }
        public void SetValue(string valStr, EditorType etype)
        {
            EditorType = etype;
            switch (EditorType)
            {
                case EditorType.Mask:
                    int _intVal = int.Parse(valStr);
                    string binaryStr = Convert.ToString(_intVal, 2);
                    binaryStr = PadLeft(binaryStr, 3, "0");
                    if (binaryStr.Substring(0, 1) == "0")
                    {
                        ckb_z.Checked = false;
                    }
                    else
                    {
                        ckb_z.Checked = true;
                    }
                    if (binaryStr.Substring(1, 1) == "0")
                    {
                        ckb_y.Checked = false;
                    }
                    else
                    {
                        ckb_y.Checked = true;
                    }
                    if (binaryStr.Substring(2, 1) == "0")
                    {
                        ckb_x.Checked = false;
                    }
                    else
                    {
                        ckb_x.Checked = true;
                    }

                    break;
                case EditorType.Bool:
                    _boolval = valStr.Equals("1") ? true : false;
                    ckb_x.Checked = _boolval;
                    break;
                case EditorType.Float:
                    _floatVal = float.Parse(valStr);
                    txt_text.Text = valStr;
                    break;
                case EditorType.Int:
                    _intVal = int.Parse(valStr);
                    txt_text.Text = valStr;
                    break;
            }
        }

        [Category("Test"), Description("Test Desc")]
        public EditorType EditorType
        {
            set
            {
                _editorType = value;
                switch (_editorType)
                {
                    case EditorType.Bool:
                        ckb_x.Visible = true;
                        ckb_x.Text = "True";
                        ckb_y.Visible = false;
                        ckb_z.Visible = false;
                        txt_text.Visible = false;
                        break;
                    case EditorType.Text:
                        ckb_x.Visible = false;
                        ckb_y.Visible = false;
                        ckb_z.Visible = false;
                        txt_text.Visible = true;
                        break;
                    case EditorType.Int:
                        ckb_x.Visible = false;
                        ckb_y.Visible = false;
                        ckb_z.Visible = false;
                        txt_text.Visible = true;

                        break;
                    case EditorType.Mask:
                        ckb_x.Visible = true;
                        ckb_x.Text = "X";
                        ckb_y.Visible = true;
                        ckb_y.Text = "Y";
                        ckb_z.Visible = true;
                        ckb_z.Text = "Z";
                        txt_text.Visible = false;

                        break;
                    case EditorType.Float:
                        ckb_x.Visible = false;
                        ckb_y.Visible = false;
                        ckb_z.Visible = false;
                        txt_text.Visible = true;

                        break;
                    default:
                        break;
                }
            }
            get
            {
                return _editorType;
            }
        }


        public ParameterEditor()
        {
            InitializeComponent();
            EditorType = EditorType.Text;
        }

        private void txt_text_TextChanged(object sender, EventArgs e)
        {
            string input = txt_text.Text;
            int cursor = txt_text.SelectionStart;
            _message = ValidateText(input);
            lbl_message.Text = _message;
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            string msg = ValidateText(_textVal);
            if (msg == string.Empty)
            {
                GrblParameterBase selected = (GrblParameterBase) cmb_params.SelectedItem;
                string paramVal = GetValue();

                DialogResult result = MessageBox.Show(
                    $"Update {selected.Desc} from [{selected.Val}] to [{paramVal}]?", 
                    "Warning", 
                    MessageBoxButtons.OKCancel);
                if(result == DialogResult.OK)
                {
                    grbl.UpdateParameter(selected.ID, paramVal);
                }

            }
            else
            {
                lbl_message.Text = msg;
            }
        }

        private string ValidateText(string input)
        {
            string msg = string.Empty;
            switch (EditorType)
            {
                case EditorType.Int:
                    int tryint;
                    if (int.TryParse(input, out tryint))
                    {
                        _textVal = input;
                        _intVal = tryint;
                    }
                    else
                    {
                        msg = "Invalid Integer Value:" + input;
                    }
                    break;
                case EditorType.Float:
                    float tryfloat;
                    if (float.TryParse(input, out tryfloat))
                    {
                        _textVal = input;
                        _floatVal = tryfloat;
                    }
                    else
                    {
                        msg = "Invalid Number:" + input;
                    }
                    break;
            }
            return msg;
        }

        private void txt_text_KeyDown(object sender, KeyEventArgs e)
        {
            string msg = ValidateText(_textVal);
            if (msg == string.Empty)
            {
                UpdateEvent();
                lbl_message.Text = "Updated Successfully";
            }
            else
            {
                lbl_message.Text = msg;
            }
        }

        GrblParameterBase paramSelected = null;
        private void cmb_params_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_message.Text = "";
            paramSelected = (GrblParameterBase) this.cmb_params.SelectedItem;
            Description = paramSelected.Desc + " (" + paramSelected.Unit + ")";

            string displayText = string.Empty;
            string unit = paramSelected.Unit;
            switch (paramSelected.ValueType)
            {
                case "int":
                    SetValue(paramSelected.Val, EditorType.Int);
                    break;
                case "float":
                    SetValue(paramSelected.Val, EditorType.Float);
                    break;
                case "bool":
                    SetValue(paramSelected.Val, EditorType.Bool);
                    displayText = paramSelected.Val;
                    break;
                case "mask":
                    SetValue(paramSelected.Val, EditorType.Mask);
                    int intVal = int.Parse(paramSelected.Val);
                    displayText = Pad(Convert.ToString(intVal, 2), 3, "0");
                    break;
            }
        }

        public void UpdateParameter()
        {
            List<GrblParameterBase> list = grbl.GetParameters();

            this.Invoke((MethodInvoker)delegate
            {
                cmb_params.Items.Clear();

            });


            foreach (var param in list)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    cmb_params.Items.Add(param);

                });
            }

            if (cmb_params.Items.Count > 0)
                this.Invoke((MethodInvoker)delegate
               {
                   cmb_params.SelectedIndex = 0;
               });
        }
        private string Pad(string s, int num, string padStr)
        {
            string result = string.Empty;
            for (int i = 0; i < num - s.Trim().Length; i++)
            {
                result += padStr;
            }
            return result + s.Trim();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            
        }
    }

    public enum EditorType
    {
        Text,
        Float,
        Int,
        Bool,
        Mask
    }
}
