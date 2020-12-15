using System;
using Device.Net;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;
using System.Threading;

namespace DualSense2Xbox
{
    abstract partial class DualSense_Base
    {
        /// <summary>
        /// The interval between each button status updates. In millisecond. 
        /// </summary>
        private const int RefreshInterval = 2;
        private uint _VirtualXboxId;
        public uint VirtualXboxId
        {
            get
            {
                return _VirtualXboxId;
            }
        }
        private IDevice device;
        protected int WriteBufferSize;
        protected int ReadBufferSize;
        protected ViGEmClient client = new ViGEmClient();
        protected IXbox360Controller controller;
        protected byte LeftMotor, RightMotor;
        protected Int64 LeftAdaptiveTrigger, RightAdaptiveTrigger;
        
        protected short LeftThumbX, LeftThumbY, RightThumbX, RightThumbY;
        protected byte LeftTrigger, RightTrigger;
        protected bool Y, B, A, X, Up, Right, Down, Left;
        protected bool LeftThumb, RightThumb, Start, Back, LeftShoulder, RightShoulder;

        public DualSense_Base(IDevice _device)
        {
            device = _device;
            if (!device.IsInitialized)
                device.InitializeAsync().Wait();
            if (!device.ConnectedDeviceDefinition.WriteBufferSize.HasValue)
                throw new AccessViolationException("Write Buffer Size is null");
            if (!device.ConnectedDeviceDefinition.ReadBufferSize.HasValue)
                throw new AccessViolationException("Read Buffer Size is null");
            WriteBufferSize = device.ConnectedDeviceDefinition.WriteBufferSize.Value;
            ReadBufferSize = device.ConnectedDeviceDefinition.ReadBufferSize.Value;
            ++_VirtualXboxId;

            controller = client.CreateXbox360Controller();
            controller.Connect();
            controller.FeedbackReceived += Controller_FeedbackReceived;
            new Thread(() => 
            {
                try
                {
                    while (true)
                    {
                        RefreshButtonStatus();
                        Thread.Sleep(RefreshInterval);
                    }
                }catch(Exception e)
                {
                    Console.WriteLine($"An error occurs.\n{e.ToString()}\n{e.StackTrace}\n");
                }
            }).Start();
        }
        private void Controller_FeedbackReceived(object sender, Xbox360FeedbackReceivedEventArgs e)
        {
            SetVibration(e.SmallMotor, e.LargeMotor);
        }
        public void SendData(byte[] SendArray)
        {
            if (SendArray.Length != WriteBufferSize)
                throw new AccessViolationException($"Trying to send invalid data to DualSense (Device ID = {device.DeviceId})");
            device.WriteAsync(SendArray).Wait();
        }
        public byte[] ReadDeviceInput()
        {
            var ReadTask = device.ReadAsync();
            var ReadResult = ReadTask.Result;
            if (ReadResult.BytesRead != ReadBufferSize)
                throw new AccessViolationException($"Read input from device: received {ReadResult.BytesRead} bytes, Expected {ReadBufferSize}");
            return ReadResult.Data;
        }
        public void UpdateButtonStatus(byte LX, byte LY, byte RX, byte RY, byte L2, byte R2, byte Button_DPad_State, byte Button2_Status)
        {
            controller.SetAxisValue(Xbox360Axis.LeftThumbX, LeftThumbX = (short)((LX << 8) - 32768));
            controller.SetAxisValue(Xbox360Axis.LeftThumbY, LeftThumbY = (short)~((LY << 8) - 32768));
            controller.SetAxisValue(Xbox360Axis.RightThumbX, RightThumbX = (short)((RX << 8) - 32768));
            controller.SetAxisValue(Xbox360Axis.RightThumbY, RightThumbY = (short)~((RY << 8) - 32768));
            controller.SetSliderValue(Xbox360Slider.LeftTrigger, LeftTrigger = L2);
            controller.SetSliderValue(Xbox360Slider.RightTrigger, RightTrigger = R2);
            //
            controller.SetButtonState(Xbox360Button.Y, Y = (Button_DPad_State & (1 << 7)) > 0);
            controller.SetButtonState(Xbox360Button.B, B = (Button_DPad_State & (1 << 6)) > 0);
            controller.SetButtonState(Xbox360Button.A, A = (Button_DPad_State & (1 << 5)) > 0);
            controller.SetButtonState(Xbox360Button.X, X = (Button_DPad_State & (1 << 4)) > 0);
            int dPad = Button_DPad_State & 0x0F;
            controller.SetButtonState(Xbox360Button.Up, Up = (dPad == 0 || dPad == 1 || dPad == 7));
            controller.SetButtonState(Xbox360Button.Right, Right = (dPad == 1 || dPad == 2 || dPad == 3));
            controller.SetButtonState(Xbox360Button.Down, Down = (dPad == 3 || dPad == 4 || dPad == 5));
            controller.SetButtonState(Xbox360Button.Left, Left = (dPad == 5 || dPad == 6 || dPad == 7));
            //
            controller.SetButtonState(Xbox360Button.RightThumb, RightThumb= (Button2_Status & (1 << 7)) > 0);
            controller.SetButtonState(Xbox360Button.LeftThumb, LeftThumb = (Button2_Status & (1 << 6)) > 0);
            controller.SetButtonState(Xbox360Button.Start, Start = (Button2_Status & (1 << 5)) > 0);    //Option
            controller.SetButtonState(Xbox360Button.Back, Back = (Button2_Status & (1 << 4)) > 0);    //Share
            controller.SetButtonState(Xbox360Button.RightShoulder, RightShoulder = (Button2_Status & (1 << 1)) > 0);
            controller.SetButtonState(Xbox360Button.LeftShoulder, LeftShoulder = (Button2_Status & (1 << 0)) > 0);
            controller.SubmitReport();
        }
        public void SetLeftAdaptiveTrigger(Int64 Parameter)
        {
            SetAdaptiveTrigger(true, Parameter);
        }
        public void SetRightAdaptiveTrigger(Int64 Parameter)
        {
            SetAdaptiveTrigger(false, Parameter);
        }
        //public abstract void SetLightBrightness(int value);
        //public abstract void SetPlayerNumber(int value);
        //public abstract void SetColor(Color color);
        public abstract void RefreshButtonStatus();
        public abstract void SetVibration(byte Left, byte Right);
        public abstract void SetAdaptiveTrigger(bool IsLeftTrigger, Int64 Parameter);
    }
}
