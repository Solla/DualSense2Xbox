using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DualSense2Xbox
{
    class GTA_AdaptiveTrigger
    {
        enum WeaponTypeEnum
        {
            Rifles = 0,
            MachineGuns = 1,
            Handguns = 2,
            Thrown = 3,
            HeavyWeapons = 4,
            Shotguns = 5,
            Melee = 6,
            SniperRifles = 7
        }
        private static WeaponTypeEnum WeaponType;
        public static void UpdateWeapons(DualSense_Base Controller, int ThumbX, int ThumbY)
        {
            if (Math.Abs(ThumbX) < 10 * 255 && ThumbY < Math.Abs(10 * 255))
            {
                //Selecting different weapons but the same type
                PredictShootHaptic(Controller);   //Reset Weapon Feedback
                return;
            }
            double sqr = Math.Sqrt(ThumbX * ThumbX + ThumbY * ThumbY);
            double x = ThumbX / sqr, y = ThumbY / sqr;
            double Angle = (Math.Atan2(y, (double)x) * 180 / Math.PI + 360) % 360;
            int Stage = (int)((Angle + 22.5) % 360) / 45;
            WeaponType = (WeaponTypeEnum)Stage;
            Console.WriteLine(WeaponType.ToString());
            PredictShootHaptic(Controller);
        }

        public static void PredictShootHaptic(DualSense_Base Controller)
        {
            Controller.SetLeftAdaptiveTrigger(DualSense_Base.VerySoftTrigger);
            switch (WeaponType)
            {
                case WeaponTypeEnum.Rifles:
                    Controller.SetRightAdaptiveTrigger(DualSense_Base.VibrateTrigger(6));
                    break;
                case WeaponTypeEnum.MachineGuns:
                    Controller.SetRightAdaptiveTrigger(DualSense_Base.VibrateTrigger(6));
                    break;
                case WeaponTypeEnum.Handguns:
                    Controller.SetRightAdaptiveTrigger(DualSense_Base.VerySoftTrigger);
                    break;
                case WeaponTypeEnum.Thrown:
                    Controller.SetRightAdaptiveTrigger(DualSense_Base.VeryHardTrigger);
                    break;
                case WeaponTypeEnum.HeavyWeapons:
                    Controller.SetRightAdaptiveTrigger(DualSense_Base.RigidTrigger);
                    break;
                case WeaponTypeEnum.Shotguns:
                    Controller.SetRightAdaptiveTrigger(DualSense_Base.HardTrigger);
                    break;
                case WeaponTypeEnum.Melee:
                    Controller.SetLeftAdaptiveTrigger(DualSense_Base.NormalTrigger);
                    Controller.SetRightAdaptiveTrigger(DualSense_Base.SoftTrigger);
                    break;
                case WeaponTypeEnum.SniperRifles:
                    Controller.SetRightAdaptiveTrigger(DualSense_Base.VeryHardTrigger);
                    break;
                default:
                    Controller.SetRightAdaptiveTrigger(DualSense_Base.NormalTrigger);
                    break;
            }
        }
        public static void UpdateShootHaptic(DualSense_Base Controller, byte LeftShootingVibration, byte RightShootingVibration)
        {
            if (LeftShootingVibration > 0)
                return; //GTAV doesn't use Left Motor when shooting, so the event must be different haptic events.
            switch (WeaponType)
            {
                case WeaponTypeEnum.Rifles:
                    if (RightShootingVibration == 92)
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.VibrateTrigger(8));
                    if (RightShootingVibration == 200 || RightShootingVibration == 182)
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.VibrateTrigger(6));
                    break;
                case WeaponTypeEnum.MachineGuns:
                    if (RightShootingVibration == 95 || RightShootingVibration == 160)
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.VibrateTrigger(8));
                    if (RightShootingVibration == 92)
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.VibrateTrigger(7));
                    if (RightShootingVibration == 139)
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.VibrateTrigger(6));
                    if (RightShootingVibration == 119)
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.VibrateTrigger(5));
                    break;
                case WeaponTypeEnum.Handguns:
                    if (RightShootingVibration == 95)
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.VibrateTrigger(5));  //穿甲
                    else
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.VerySoftTrigger);
                    break;
                case WeaponTypeEnum.Thrown:
                    Controller.SetRightAdaptiveTrigger(DualSense_Base.VeryHardTrigger);
                    break;
                case WeaponTypeEnum.HeavyWeapons:
                    if (RightShootingVibration == 92)
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.VibrateTrigger(15));  //火神
                    else
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.RigidTrigger);
                    break;
                case WeaponTypeEnum.Shotguns:
                    if (RightShootingVibration == 164 || RightShootingVibration == 209)
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.VibrateTrigger(3));   //突擊
                    else
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.HardTrigger);
                    break;
                case WeaponTypeEnum.Melee:
                    Controller.SetRightAdaptiveTrigger(DualSense_Base.SoftTrigger);
                    break;
                case WeaponTypeEnum.SniperRifles:
                    if (RightShootingVibration == 200)
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.VibrateTrigger(3));   //突擊
                    else
                        Controller.SetRightAdaptiveTrigger(DualSense_Base.VeryHardTrigger);
                    break;
                default:
                    Controller.SetRightAdaptiveTrigger(DualSense_Base.NormalTrigger);
                    break;
            }
            //WeaponType
        }
    }
}
