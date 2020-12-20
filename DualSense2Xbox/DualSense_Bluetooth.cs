using Device.Net;
using System;


namespace DualSense2Xbox
{
    class DualSense_Bluetooth : DualSense_Base
    {
        public DualSense_Bluetooth(IDevice _device)
            : base(_device)
        {
            if (ReadBufferSize != 78)
                throw new Exception("Unknown Read Buffer!");
        }
        public override void RefreshButtonStatus()
        {
            var DeviceInputByteArray = ReadDeviceInput();
            UpdateButtonStatus(DeviceInputByteArray[2], DeviceInputByteArray[3], DeviceInputByteArray[4], DeviceInputByteArray[5], DeviceInputByteArray[6], DeviceInputByteArray[7], DeviceInputByteArray[9], DeviceInputByteArray[10]);
        }
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
            outputReport[0] = 0x31;
            outputReport[1] = 0x02; // report type
            outputReport[2] = 0x1 | 0x2 | 0x4 | 0x8; // flags determiing what changes this packet will perform
            outputReport[4] = RightMotor;
            outputReport[5] = LeftMotor;
            for (int i = 0; i < 7; ++i)
            {
                outputReport[12 + i] = RightTriggerParameter[i];
                outputReport[23 + i] = LeftTriggerParameter[i];
            }
            outputReport[21] = RightTriggerParameter[7];
            outputReport[32] = LeftTriggerParameter[7];

            //Produce CRC32 and Copy to Send Buffer
            uint _CRC32 = ComputeCRC32(outputReport, 74);
            byte[] _CRC32_Bytes = BitConverter.GetBytes(_CRC32);
            Array.Copy(_CRC32_Bytes, 0, outputReport, 74, 4);

            SendData(outputReport);
        }
    }
}
