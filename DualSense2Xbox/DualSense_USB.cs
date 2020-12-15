using Device.Net;
using System;


namespace DualSense2Xbox
{
    class DualSense_USB : DualSense_Base
    {
        public DualSense_USB(IDevice _device)
            :base(_device)
        {
            if (ReadBufferSize != 0x40)
                throw new Exception("Unknown Read Buffer!");
        }
        public override void RefreshButtonStatus()
        {
            var DeviceInputByteArray = ReadDeviceInput();
            UpdateButtonStatus(DeviceInputByteArray[1], DeviceInputByteArray[2], DeviceInputByteArray[3], DeviceInputByteArray[4], DeviceInputByteArray[5], DeviceInputByteArray[6], DeviceInputByteArray[8], DeviceInputByteArray[9]);
        }
        //Ref:https://www.reddit.com/r/gamedev/comments/jumvi5/dualsense_haptics_leds_and_more_hid_output_report/
        public override void SetVibration(byte Left, byte Right)
        {
            LeftMotor = Left;
            RightMotor = Right;
            SendHaptics();
        }
        public override void SetAdaptiveTrigger(bool IsLeftTrigger, Int64 Parameter)
        {
            if (IsLeftTrigger)
                LeftAdaptiveTrigger = Parameter;
            else
                RightAdaptiveTrigger = Parameter;
            SendHaptics();
        }
        private void SendHaptics()
        {
            var LeftTriggerParameter = BitConverter.GetBytes(LeftAdaptiveTrigger);
            var RightTriggerParameter = BitConverter.GetBytes(RightAdaptiveTrigger);

            var outputReport = new byte[WriteBufferSize];
            outputReport[0] = 0x02; // report type
            outputReport[1] = 0x1 | 0x2 | 0x4 | 0x8; // flags determiing what changes this packet will perform
            outputReport[3] = LeftMotor;
            outputReport[4] = RightMotor;
            for (int i = 0; i < 7; ++i)
            {
                outputReport[11 + i] = RightTriggerParameter[i];
                outputReport[22 + i] = LeftTriggerParameter[i];
            }
            outputReport[20] = RightTriggerParameter[7];
            outputReport[31] = LeftTriggerParameter[7];
            SendData(outputReport);
        }

    }
}
