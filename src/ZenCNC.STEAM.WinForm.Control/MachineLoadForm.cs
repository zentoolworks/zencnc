using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using ZenCNC.STEAM.grbl;

namespace ZenCNC.STEAM.WinForm.Control
{
    public partial class MachineLoadForm : Form
    {
        public string MachineConfigFolder { get; set; }

        public bool Ok { get; set; }

        
        public MachineLoadForm(string folder)
        {
            InitializeComponent();

            MachineConfigFolder = folder;


            LoadMachineList();
        }

        public void LoadMachineList()
        {
            if(MachineConfigFolder != null && Directory.Exists(MachineConfigFolder))
            {
                string[] machineFiles = Directory.GetFiles(MachineConfigFolder, "machine_*.xml");

                foreach(string machineFile in machineFiles)
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(machineFile);
                    XmlNode machineNode = xml.SelectSingleNode("/Machine");
                    string name = machineNode.Attributes["name"].Value;
                    this.cmb_machines.Items.Add(name);
                }
            }
        }

        public XmlNode GetMachineByName(string name)
        {

            XmlNode ret = null;
            if (MachineConfigFolder != null && Directory.Exists(MachineConfigFolder))
            {
                string[] machineFiles = Directory.GetFiles(MachineConfigFolder, "machine_*.xml");

                foreach (string machineFile in machineFiles)
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(machineFile);
                    XmlNode machineNode = xml.SelectSingleNode("/Machine");
                    string n = machineNode.Attributes["name"].Value;
                    if(n.Equals(name))
                    {
                        ret = machineNode;
                        break;
                    }
                }
            }

            return ret;
        }

        public List<ParamValue> ParameterList = new List<ParamValue>();

        private void cmb_machines_SelectedIndexChanged(object sender, EventArgs e)
        {
            ParameterList.Clear();
            this.dgv_params.Rows.Clear();
            if(this.cmb_machines.SelectedItem != null)
            {
                string name = this.cmb_machines.SelectedItem.ToString();
                XmlNode machineNode = GetMachineByName(name);
                if(machineNode != null)
                {
                    XmlNodeList paramList = machineNode.SelectNodes("Parameters/Parameter");
                    foreach(XmlNode paramNode in paramList)
                    {
                        ParamValue pv = new ParamValue();
                        pv.Id = paramNode.Attributes["id"].Value;
                        pv.Desc = paramNode.Attributes["desc"].Value;
                        pv.Value  = paramNode.Attributes["value"].Value;
                        ParameterList.Add(pv);
                        this.dgv_params.Rows.Add(new object[] {pv.Id, pv.Desc, pv.Value });
                    }
                    this.lbl_name.Text = name;
                }
            }
        }

        private void btn_load_Click(object sender, EventArgs e)
        {
            this.Ok = true;
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Ok = false;
            this.Close();
        }

        private void dgv_params_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 2)
            {
                DataGridViewCell cell = dgv_params[e.ColumnIndex, e.RowIndex];
                dgv_params.CurrentCell = cell;
                dgv_params.BeginEdit(false);
                this.btn_load.Enabled = false;
            }
            else
            {
                dgv_params.EndEdit();
                
            }
        }
    }

    public struct ParamValue
    {
        public string Id;
        public string Desc;
        public string Value;
    }
}
