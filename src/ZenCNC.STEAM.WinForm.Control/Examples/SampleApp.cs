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
using ZenCNC.STEAM.WinForm.Control;
using ZenCNC.STEAM.WinForm.Control.Examples;
using ZenCNC.STEAM.WinForm.Examples;

namespace ZenCNC.STEAM.WinForm.Examples
{
    public partial class SampleAppForm : Form
    {
        //Grbl client used across UI components
        private GrblClient grbl = new GrblClient();

        //Grbl parameter form
        private ParamForm paramForm;

        //Indicator whether grbl setting completed
        private bool initialized = false;

        delegate void ZeroArgReturningVoidDelegate();

        public void StopGrbl()
        {
            if (this.cmb_ports.InvokeRequired)
            {
                ZeroArgReturningVoidDelegate d = new ZeroArgReturningVoidDelegate(StopGrbl);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.joggingControl1.Disable();
                grbl.IsConnected = false;
                initialized = false;
                grbl.portDesc = null;
                this.cmb_ports.Items.Clear();
                this.cmb_ports.Text = string.Empty;
            }
        }

        public void DisableUI()
        {
            if (this.joggingControl1.InvokeRequired)
            {
                ZeroArgReturningVoidDelegate d = new ZeroArgReturningVoidDelegate(DisableUI);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.joggingControl1.Disable();
            }
        }
        public void StartGrbl()
        {
            if (this.cmb_ports.InvokeRequired)
            {
                ZeroArgReturningVoidDelegate d = new ZeroArgReturningVoidDelegate(StartGrbl);
                this.Invoke(d, new object[] { });
            }
            else
            {
                List<PortDesc> ports = GrblClient.GetSerialPorts();
                cmb_ports.Items.Clear();
                foreach (var port in ports)
                {
                    cmb_ports.Items.Add(port.Caption);
                }
                if (ports.Count == 1)
                {
                    cmb_ports.SelectedIndex = 0;
                    grbl.Open(ports[0]);

                }
            }
        }


        public SampleAppForm()
        {
            InitializeComponent();

            //Add event listener to response received event
            grbl.ResponseReceived += Grbl_ResponseReceived;

            //Add event listener to gcode status update event
            grbl.GCodeStatusUpdate += Grbl_GCodeStatusUpdate;

            USBUtil.USBPortChangeEvent += USBUtil_USBPortChangeEvent;
            USBUtil.init();

            StopGrbl();

            LoadMachines();

            //If ports found and only one availeble, open at the beginning

            StartGrbl();
        }

        private void USBUtil_USBPortChangeEvent(object sender, EventArgs e)
        {
            PortStateChangeArg args = (PortStateChangeArg)e;

            if(args.State.Equals("ADDED"))
            {
                if(!grbl.IsConnected)
                {
                    StartGrbl();
                }
            }
            else if(args.State.Equals("REMOVED"))
            {
                List<PortDesc> ports = GrblClient.GetSerialPorts();
                bool isCurrentConnectionOk = false;
                foreach(var port in ports)
                {
                    if(port.DeviceId == grbl.portDesc.DeviceId)
                    {
                        isCurrentConnectionOk = true;
                        break;
                    }
                }

                if(!isCurrentConnectionOk)
                {
                    StopGrbl();
                }

            }

        }

        public void LoadMachines()
        {
            List<string> machines = grbl.GetAllMachines();
            foreach(string machine in machines)
            {
                string[] flds = machine.Split('|');
                int cnt = 0;

            //    foreach(string fld in flds)
            //    {
            //        if(cnt == 0)
            //        {
            //            level1 = null;
            //            foreach(ToolStripMenuItem submenu in machinesToolStripMenuItem.DropDownItems)
            //            {
            //                if(submenu.Text.Equals(fld))
            //                {
            //                    level1 = submenu;
            //                }
            //            }
            //            if(level1 == null)
            //            {
            //                level1 = new ToolStripMenuItem(fld);
            //                machinesToolStripMenuItem.DropDownItems.Add(level1);
            //            }
            //            //Find first level parent
            //        }
            //        cnt++;
            //    }
            }
        }

        /// <summary>
        /// G Code file status update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grbl_GCodeStatusUpdate(object sender, EventArgs e)
        {
            GrblGCodeStatusEventArgs args = (GrblGCodeStatusEventArgs)e;
            this.droSample1.UpdateGCodeStatus(args.FileStatus.ToString(), args.CurrentLine, args.TotalLine);
         
        }

        /// <summary>
        /// Grbl response update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grbl_ResponseReceived(object sender, EventArgs e)
        {
            GrblResponseEventArgs args = (GrblResponseEventArgs)e;


            if(args.State == MachineState.ALARM)
            {
                if (!initialized)
                {
                    this.joggingControl1.Enable();
                    grbl.CMD_UNLOCK();
                    System.Threading.Thread.Sleep(1000);
                    grbl.CMD_ZEROALL();
                    initialized = true;
                }
            }

            if(args.State == MachineState.IDLE)
            {
                if(!initialized)
                {
                    this.joggingControl1.Enable();
                    grbl.CMD_ZEROALL();
                    initialized = true;
                }
            }

            

            if (args.EventType != null && args.EventType.Equals("ParameterUpdate"))
            {
                if (paramForm != null)
                    paramForm.RefreshParameters();
                    
            }
            this.droSample1.UpdateGrblStatus(args.State.ToString());
            this.droSample1.Update(args.WPos.X, args.WPos.Y, args.WPos.Z);
        }

        private void gCodeFileControl1_RunGCode(object sender, EventArgs e)
        {
            RunGCodeEventArgs args = (RunGCodeEventArgs)e;
            grbl.OpenGCode(args.FilePath);
            grbl.StartFile();
        }

        /// <summary>
        /// Jogging control command handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void joggingControl1_RunCommand(object sender, EventArgs e)
        {
            CommandEventArgs args = (CommandEventArgs)e;
            string feedrate = joggingControl1.JogFeedrate;
            string xystepsize = joggingControl1.XYStepSize;
            string zstepsize = joggingControl1.ZStepSize;
            string xval = joggingControl1.XValue;
            string yval = joggingControl1.YValue;
            string zval = joggingControl1.ZValue;
            grbl.JogFeedRate = float.Parse(feedrate);
            switch (args.Command)
            {
                case "JOGX+":
                    grbl.CMD_JOG("X", true);
                    break;
                case "JOGX-":
                    grbl.CMD_JOG("X", false);
                    break;
                case "JOGY+":
                    grbl.CMD_JOG("Y", true);
                    break;
                case "JOGY-":
                    grbl.CMD_JOG("Y", false);
                    break;
                case "JOGZ+":
                    grbl.CMD_JOG("Z", true);
                    break;
                case "JOGZ-":
                    grbl.CMD_JOG("Z", false);
                    break;
                case "STOPJOG":
                    grbl.CMD_JC();
                    break;
                case "HOME":
                    grbl.CMD_HOME();
                    break;
                case "ZEROALL":
                    grbl.CMD_ZEROALL();
                    break;
                case "UNLOCK":
                    grbl.CMD_UNLOCK();
                    break;
                case "STEPX+":
                    grbl.CMD_MOVERELATIVE("x", float.Parse(xystepsize), float.Parse(feedrate));
                    break;
                case "STEPX-":
                    grbl.CMD_MOVERELATIVE("x", -float.Parse(xystepsize), float.Parse(feedrate));
                    break;
                case "STEPY+":
                    grbl.CMD_MOVERELATIVE("y", float.Parse(xystepsize), float.Parse(feedrate));
                    break;
                case "STEPY-":
                    grbl.CMD_MOVERELATIVE("y", -float.Parse(xystepsize), float.Parse(feedrate));
                    break;
                case "STEPZ+":
                    grbl.CMD_MOVERELATIVE("z", float.Parse(zstepsize), float.Parse(feedrate));
                    break;
                case "STEPZ-":
                    grbl.CMD_MOVERELATIVE("z", -float.Parse(zstepsize), float.Parse(feedrate));
                    break;
                case "ABSMOVE":
                    grbl.CMD_MOVETO(float.Parse(xval), float.Parse(yval), float.Parse(zval), float.Parse(feedrate));
                    break;
                case "RELMOVE":
                    grbl.CMD_MOVERELATIVE(float.Parse(xval), float.Parse(yval), float.Parse(zval), float.Parse(feedrate));
                    break;
                default:
                    break;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(paramForm == null)
               paramForm = new ParamForm(this.grbl);
            paramForm.ShowDialog();
        }

        private void btn_stop_MouseDown(object sender, MouseEventArgs e)
        {
            grbl.CMD_RESET();
        }

        private void exltToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (grbl != null)
                grbl.Close();
            grbl = null;

            this.Close();
        }

        private void machinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MachineLoadForm mform = new MachineLoadForm("config\\machines");
            mform.ShowDialog();
            if(mform.Ok)
            {
                foreach(var param in mform.ParameterList)
                {
                    if(grbl.IsConnected)
                    {
                        grbl.UpdateParameter(param.Id, param.Value);
                    }
                }
            }
        }

        private void saveMachineAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsMachineForm machineform = new SaveAsMachineForm();
            machineform.ShowDialog();
            if(machineform.Ok)
            {
                string name = machineform.Name;
                string model = machineform.Model;
                string id = machineform.Id;
                grbl.SaveMachineAs(name, id, model);
            }

        }
    }
}
