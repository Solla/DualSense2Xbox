using System;

namespace DualSense2Xbox
{
    abstract partial class DualSense_Base
    {
        public static Int64 NormalTrigger = 0x0000000000000000;
        public static Int64 VerySoftTrigger = 0x00000000FFA08002;
        public static Int64 SoftTrigger = 0x00000000FFA04502;
        public static Int64 HardTrigger = 0x00000000FFA02002;
        public static Int64 VeryHardTrigger = 0x00000000FFA01002;
        public static Int64 HardestTrigger = 0x00000000FFFF0002;
        public static Int64 RigidTrigger = 0x0000000000FF0002;
        public static Int64 CalibrateTrigger = 0x00000000FFFF00FC;

        public static Int64 VibrateTrigger_10Hz = 0x0AFF000000FF0026;
        public static Int64 VibrateTrigger(byte Freq)
        {
            return ((Int64)Freq << 56) | 0x00FF000000FF0026;
        }
    }
}
