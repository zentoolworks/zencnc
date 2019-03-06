using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZenCNC.STEAM.grbl
{
    public class CNCMachine
    {
        static XmlDocument machinesXml = new XmlDocument();
        public static List<CNCMachine> Machines;

        public List<MachineParam> MachineParams;

        public string Id { get; set; }

        public string Name { get; set; }

        public static CNCMachine GetMachineById(string id)
        {
            CNCMachine result = null;
            foreach (CNCMachine machine in Machines)
            {
                if (machine.Id.Equals(id))
                {
                    result = machine;
                    break;
                }
            }
            return result;
        }
        public CNCMachine(XmlNode machineNode)
        {
            Name = machineNode.Attributes["name"].Value;

            Id = machineNode.Attributes["id"].Value;

            MachineParams = new List<MachineParam>();

            foreach (XmlNode paramNode in machineNode.SelectNodes("Parameters/Parameter"))
            {
                MachineParams.Add(new MachineParam(paramNode));
            }
        }

        public static void AddMachines(string fileName)
        {
            if (Machines == null)
            {
                Machines = new List<CNCMachine>();
            }
            XmlDocument machineDoc = new XmlDocument();
            machineDoc.Load(fileName);
            foreach (XmlNode machineNode in machineDoc.SelectNodes("Machines/Machine"))
            {
                Machines.Add(new CNCMachine(machineNode));
            }
        }

    }

    public class MachineParam
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public string Value { get; set; }

        public MachineParam(XmlNode node)
        {
            Id = node.Attributes["id"].Value;
            Desc = node.Attributes["desc"].Value;
            Value = node.Attributes["value"].Value;
        }
    }
}

