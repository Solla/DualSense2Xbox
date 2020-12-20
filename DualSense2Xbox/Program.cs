using System;
using System.Linq;
using System.Threading;
using Device.Net;
using Hid.Net.Windows;
using Usb.Net.Windows;

namespace DualSense2Xbox
{
    class Program
    {
        static void Main(string[] args)
        {
            //Register the factory for creating Usb devices. This only needs to be done once.
            WindowsUsbDeviceFactory.Register(null, null);

            //Register the factory for creating Usb devices. This only needs to be done once.
            WindowsHidDeviceFactory.Register(null, null);

            //Search DualSense Controller
            var DeviceSearcherTask = DeviceManager.Current.GetConnectedDeviceDefinitionsAsync(new FilterDeviceDefinition { DeviceType = DeviceType.Hid, VendorId = 1356, ProductId = 3302 });
            DeviceSearcherTask.Wait();
            var DualSenseList = DeviceSearcherTask.Result.ToArray();
            int DualSenseCount = DualSenseList.Length;
            if (DualSenseCount <= 0)
                throw new Exception("Cannot find DualSense. Check the cable again!");
            var DualSenseDevice = DeviceManager.Current.GetDevice(DualSenseList[0]);

            DualSense_Base dualSense;
            if (DualSenseList[0].ReadBufferSize == 64)
                dualSense = new DualSense_USB(DualSenseDevice);
            else if (DualSenseList[0].ReadBufferSize == 78)
                dualSense = new DualSense_Bluetooth(DualSenseDevice);
            else
                throw new NotImplementedException("Currently this application supports USB connection only.");

            //Set Adaptive Trigger as "Normal"
            dualSense.SetLeftAdaptiveTrigger(DualSense_Base.NormalTrigger);
            dualSense.SetRightAdaptiveTrigger(DualSense_Base.NormalTrigger);

            Thread.Sleep(-1);
        }
    }
}
