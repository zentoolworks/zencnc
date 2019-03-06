using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using static ZenCNC.STEAM.grbl.GrblClient;

namespace ZenCNC.STEAM.grbl
{
    public static class USBUtil
    {
        static ManagementEventWatcher watchingObect = null;
        static WqlEventQuery watcherQuery;
        static ManagementScope scope;

        public static void init()
        {
            scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;
            AddInsetUSBHandler();
            AddRemoveUSBHandler();
        }

        public static void AddRemoveUSBHandler()
        {
            try
            {
                USBWatcherSetUp("__InstanceDeletionEvent");
                watchingObect.EventArrived += new EventArrivedEventHandler(USBRemoved);
                watchingObect.Start();

            }

            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                if (watchingObect != null)
                    watchingObect.Stop();

            }

        }

        static void AddInsetUSBHandler()
        {

            try
            {
                USBWatcherSetUp("__InstanceCreationEvent");
                watchingObect.EventArrived += new EventArrivedEventHandler(USBAdded);
                watchingObect.Start();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                if (watchingObect != null)
                    watchingObect.Stop();

            }

        }

        private static void USBWatcherSetUp(string eventType)
        {

            watcherQuery = new WqlEventQuery();
            watcherQuery.EventClassName = eventType;
            watcherQuery.WithinInterval = new TimeSpan(0, 0, 2);
            watcherQuery.Condition = @"TargetInstance ISA 'Win32_USBControllerdevice'";
            watchingObect = new ManagementEventWatcher(scope, watcherQuery);
        }

        public static void USBAdded(object sender, EventArgs e)
        {
            PortStateChangeArg args = new PortStateChangeArg();
            args.State = "ADDED";
            OnThresholdReached(args);
            

        }

        public static void USBRemoved(object sender, EventArgs e)
        {
            PortStateChangeArg args = new PortStateChangeArg();
            args.State = "REMOVED";
            OnThresholdReached(args);

        }
        public static event EventHandler USBPortChangeEvent;

        private static void OnThresholdReached(PortStateChangeArg e)
        {
            EventHandler handler = USBPortChangeEvent;
            if (handler != null)
            {
                handler(null, e);
            }
        }


    }

    public class PortStateChangeArg : EventArgs
    {
        public string State { get; set; }
    }
}
