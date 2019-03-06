using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenCNC.STEAM.grbl
{
    public class GrblResponseRouter
    {
        private ActionFlowType _flowType = ActionFlowType.Default;
        public GrblResponseRouter(ActionFlowType flowType)
        {
            _flowType = flowType;
        }
        public void Route(string response)
        {
            ResponseMessageType respType = GetMessageType(response);
            switch (respType)
            {
                case ResponseMessageType.OK:
                    break;
                case ResponseMessageType.PRAMETER:
                    break;
                case ResponseMessageType.STATUS:
                    Process_Status(response);
                    break;
                default:
                    break;
            }
        }

        public void Process_Ok()
        {

        }
        public void Process_Parameter(string response)
        {

        }
        public void Process_Status(string response)
        {
            string content = response.Substring(1, response.Length - 2);
            string[] flds = content.Split('|');
            int cnt = 0;
            string status = "unknown";
            string mpos = "";
            string wpos = "";
            string wco = "";
            string fs = "";

            foreach (string fld in flds)
            {
                if (cnt == 0)
                {
                    status = fld;
                }
                else if (fld.ToUpper().StartsWith("MPOS:"))
                {
                    mpos = fld.Substring(5);
                }
                else if (fld.ToUpper().StartsWith("WPOS:"))
                {
                    wpos = fld.Substring(5);
                }
                else if (fld.ToUpper().StartsWith("WCO:"))
                {
                    wco = fld.Substring(4);
                }
                else if (fld.ToUpper().StartsWith("FS:"))
                {
                    fs = fld.Substring(3);
                }
                cnt++;
            }
            //logger.Debug($"Status:{status} mpos={mpos}, wpos={wpos}, wco={wco}, fs={fs}");
        }

        public static ResponseMessageType GetMessageType(string response)
        {
            if (response.Trim().ToUpper().Equals("OK"))
            {
                return ResponseMessageType.OK;
            }
            else if (response.StartsWith("<") && response.EndsWith(">"))
            {
                return ResponseMessageType.STATUS;
            }
            else if (response.StartsWith("$") && response.IndexOf("=") > 0)
            {
                return ResponseMessageType.PRAMETER;
            }
            return ResponseMessageType.Unkown;
        }
    }

    public enum ActionFlowType
    {
        Default,
        Single
    }

    public enum ResponseMessageType
    {
        [Description("Ok")]
        OK,
        [Description("Status")]
        STATUS,
        [Description("Parameter")]
        PRAMETER,
        [Description("Unkown")]
        Unkown
    }
}
