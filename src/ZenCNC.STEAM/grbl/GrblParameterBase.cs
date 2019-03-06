using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZenCNC.STEAM.grbl
{
    public class GrblParameterBase
    {
        /// <summary>
        /// Grbl parameter ID, 1, 2, 3
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// Grbl parameter value
        /// </summary>
        public string Val { get; set; }
        /// <summary>
        /// Grbl parameter description, from predefined config file
        /// </summary>
        private string _desc = string.Empty;

        private static string lastParameter = "";

        public static bool IsLastParameter(string id)
        {
            if (id.Trim().Equals(lastParameter))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string Desc
        {
            get
            {
                return _desc;
            }
            set
            {
                string[] flds = value.Split(',');
                if (flds.Length == 2)
                {
                    _desc = flds[0];
                    Unit = flds[1].Trim();
                }
                else
                {
                    _desc = value;
                }
            }
        }
        public string ValueType { get; set; }
        public string Unit { get; set; }

        private static Hashtable descHash = new Hashtable();
        private static Hashtable typeHash = new Hashtable();

        public override string ToString()
        {
            
            return StringUtil.Padding(this.ID, 4, ' ', true) + " = " + StringUtil.Padding(Val, 10, ' ', true) + " (" + Desc + ")";
        }

        public static void Init()
        {
            descHash = new Hashtable();
            typeHash = new Hashtable();
            //Open grbl configuration file
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("config\\grbl_v1.1f.xml");
            //For each parameter defined in config file
            foreach (XmlNode paramNode in xmlDoc.SelectNodes("/grbl/Parameters/Parameter"))
            {
                //Parameter id
                string id = paramNode.Attributes["id"].Value;
                //Parameter description
                string desc = paramNode.Attributes["desc"].Value;
                //Parameter type
                string typ = paramNode.Attributes["type"].Value;

                if (paramNode.Attributes["isTheLast"] != null)
                {
                    lastParameter = id;
                }

                //Add to description hash, key is the id
                descHash.Add(id, desc);
                //Add to type hash, key is the id
                typeHash.Add(id, typ);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="str"></param>
        public GrblParameterBase(string id, string str)
        {
            ID = id;
            Val = str;
            Desc = descHash[ID].ToString();
            ValueType = typeHash[ID].ToString();
        }

        /// <summary>
        /// Create a paramter from Grbl Parameter line $1=0, for ex.
        /// </summary>
        /// <param name="paramStr"></param>
        /// <returns></returns>
        public static GrblParameterBase CreateParameter(string paramStr)
        {
            //Get rid of the starting $ sign
            paramStr = paramStr.Trim().Substring(1);
            //Split the string by =
            string[] flds = paramStr.Split('=');

            //flds[0] is the id, and flds[1] is the current value
            return new GrblParameterBase(flds[0], flds[1]);
        }
    }
}
