# DualSense2Xbox: DualSense Adaptive Trigger for GTA V on PC

A lightweight tool to use DualSense on Windows based on ViGEm.

Currently, it works perfectly to emulate an Xbox 360 controller.

In this branch, I implemented a program to support Adaptive Trigger for GTA V through Black-box Approaches.

It means that the program will not modify any code in GTA V.

However, I still don't suggest that anyone use this program while playing GTA Online.

Due to the Black-box approaches' limitations, the program doesn't know how many bullets in your magazine.

The haptic feedback will not stop when reloading.

The patterns are not defined carefully, so you might feel that the frequencies of feedback do not match the frequencies of firing.

This version is still not supporting Bluetooth Connection. Please use a USB-C cable to connect DualSenses to the PC.

The program is still not perfect. There are several bugs.

I used the joystick's positions to guess what type of weapons users choose, and the approach is not precise.

If you feel the feedback is awkward, please select the weapons once again.

The program doesn't handle the error when the cable is loose, and GTA V will be freeze for a while.

Make sure your cable is secure before using this program.

You can download the pre-compiled version here:

* [GTA V Release](https://github.com/Solla/DualSense2Xbox/releases/tag/v0.1_Alpha_Version)

Don't forget to install the ViGEm driver first.

## Installation

Please preinstall the ViGEm driver first.

* [ViGEm Bus](https://github.com/ViGEm/ViGEmBus)

All C# libraries should be automaticly downloaded by NuGet Package Manager.
If not, please download these libraries manually.
* [How to Use Nuget Packages](https://www.syncfusion.com/blogs/post/how-to-use-nuget-packages.aspx)

## Buliding Environment
  - Windows 10
  - Visual Studio 2019
  - .Net Framework 4.8
  - Nefarius.ViGEm.Client 1.16.150
  - Device.Net 3.1.0
  - Hid.Net 3.1.0
  - Usb.Net 3.1.0

## Acknowledgement and Reference

* [Dualsense, Haptics, Leds, and More (Hid Output Report)](https://www.reddit.com/r/gamedev/comments/jumvi5/dualsense_haptics_leds_and_more_hid_output_report/)
* [DS4Windows](https://github.com/Ryochan7/DS4Windows)