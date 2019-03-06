using ArduinoUploader;
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

namespace ZenCNC.STEAM.WinForm.Control
{
    public partial class HexUploadForm : Form
    {
        public HexUploadForm()
        {
            InitializeComponent();

            List<PortDesc> ports = GrblClient.GetSerialPorts();

            cmb_models.Items.Add("UNO R3");
            foreach(var port in ports)
            {
                this.cmb_ports.Items.Add(port);
            }
        }

        private void btn_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "hex files (*.hex)|*.hex";
            

            open.ShowDialog();

            if(open.FileName.Length > 0)
            {
                this.lbl_file.Text = open.FileName;
            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            ArduinoSketchUploaderOptions options = new ArduinoSketchUploaderOptions();
            //options.ArduinoModel = ArduinoUploader.Hardware.ArduinoModel.UnoR3;
            options.ArduinoModel = ArduinoUploader.Hardware.ArduinoModel.NanoR3;
            options.FileName = this.lbl_file.Text;

            PortDesc selectedPort = (PortDesc)this.cmb_ports.SelectedItem;
            options.PortName = selectedPort.DeviceId;

            
            ArduinoSketchUploader uploader = new ArduinoSketchUploader(options, null, null);
            this.lbl_msg.Text = "Uploading ...";
            
            uploader.UploadSketch();

            this.lbl_msg.Text = "Upload Completed.";
        }
    }
}
