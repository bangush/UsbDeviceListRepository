using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace UsbDeviceListSampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GetDeviceInfos();
            GetDeviceDescriptions();

            Console.ReadKey();
        }

        private static void GetDeviceDescriptions()
        {
            ManagementScope scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;
            string Win32_USBControlerDevice = "Select * From Win32_USBControllerDevice";
            ObjectQuery query = new ObjectQuery(Win32_USBControlerDevice);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            foreach (ManagementObject mgmtObj in searcher.Get())
            {
                string strDeviceName = mgmtObj["Dependent"].ToString();
                string[] arrDeviceName = strDeviceName.Split('=');
                strDeviceName = arrDeviceName[1];
                string Win32_PnPEntity = "Select * From Win32_PnPEntity Where DeviceID = " + strDeviceName;
                ManagementObjectSearcher mySearcher = new ManagementObjectSearcher(Win32_PnPEntity);
                foreach (ManagementObject mobj in mySearcher.Get())
                {
                    string desc = "";
                    try
                    {
                        desc = mobj["Description"].ToString().Trim();
                        Console.WriteLine(desc);
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message + "---" + ex.StackTrace); }
                }
            }
        }

        private static void GetDeviceInfos()
        {
            ManagementScope scope = new ManagementScope("root\\CIMV2");
            scope.Options.EnablePrivileges = true;
            string Win32_USBControlerDevice = "Select * From Win32_USBControllerDevice";
            ObjectQuery query = new ObjectQuery(Win32_USBControlerDevice);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
            foreach (ManagementObject mgmtObj in searcher.Get())
            {
                string strDeviceName = mgmtObj["Dependent"].ToString();
                string[] arrDeviceName = strDeviceName.Split('=');
                strDeviceName = arrDeviceName[1];
                string Win32_PnPEntity = "Select * From Win32_PnPEntity Where DeviceID = " + strDeviceName;
                ManagementObjectSearcher mySearcher = new ManagementObjectSearcher(Win32_PnPEntity);
                foreach (ManagementObject mobj in mySearcher.Get())
                {
                    var enm = mobj.Properties.GetEnumerator();
                    while (enm.MoveNext())
                    {
                        var arr = enm.Current.Value as string[];
                        if (arr != null)
                        {
                            Console.WriteLine(string.Format("{0} --> {1}", enm.Current.Name, string.Join("**", arr)));
                        }
                        else
                        {
                            Console.WriteLine(string.Format("{0} --> {1}", enm.Current.Name, enm.Current.Value));
                        }
                    }
                    Console.WriteLine("------------------------------------------------");
                    //string strDeviceID = mobj["DeviceID"].ToString();
                    //string[] hwIDler = (string[])mobj["HardwareID"];
                    //string desc = "";
                    //try
                    //{
                    //    desc = mobj["Description"].ToString().Trim();
                    //    Console.WriteLine(desc);
                    //}
                    //catch (Exception ex) { Console.WriteLine(ex.Message + "---" + ex.StackTrace); }
                }
            }
        }
    }
}
