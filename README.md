# DualSense2Xbox

A lightweight tool to use DualSense on Windows based on ViGEm.
Currently it works perfectly to emulate an Xbox 360 controller.

## Features
With DualSense2Xbox, your DualSense can support (almost) all games based on XInput.

The Adaptive Trigger can be modified by calling the function ``dualSense.SetLeftAdaptiveTrigger()`` and ``dualSense.SetRightAdaptiveTrigger()``.

Here are some examples:
```
dualSense.SetLeftAdaptiveTrigger(DualSense_Base.RigidTrigger);
dualSense.SetRightAdaptiveTrigger(DualSense_Base.HardTrigger);
dualSense.SetLeftAdaptiveTrigger(DualSense_Base.VibrateTrigger_10Hz);    //Vibrate 10Hz if presses
dualSense.SetRightAdaptiveTrigger(DualSense_Base.VibrateTrigger(3));    //Vibrate 3Hz if presses
```

My friends and I are working on fitting games' events by black-box approaches.
In the future, we might be able to experience the Adaptive Trigger on PC platforms.

![Adaptive Trigger on GTA5 with 5Hz firing](5HzFiring.gif)


## Buliding Environment
  - Windows 10
  - Visual Studio 2019
  - .Net Framework 4.8
  - Nefarius.ViGEm.Client 1.16.150
  - Device.Net 3.1.0
  - Hid.Net 3.1.0
  - Usb.Net 3.1.0

## Installation

Please preinstall the ViGEm  driver first.
* [ViGEm Bus](https://github.com/ViGEm/ViGEmBus)

All C# libraries should be automaticly downloaded by NuGet Package Manager.
If not, please download these libraries manually.
* [How to Use Nuget Packages](https://www.syncfusion.com/blogs/post/how-to-use-nuget-packages.aspx)

## Acknowledgement and Reference

* [Dualsense, Haptics, Leds, and More (Hid Output Report)](https://www.reddit.com/r/gamedev/comments/jumvi5/dualsense_haptics_leds_and_more_hid_output_report/)
* [BLE Inputs](https://gist.github.com/Ryochan7/91a9759deb5dff3096fc5afd50ba19e2)
* [DualSense-Windows](https://github.com/Ohjurot/DualSense-Windows)
* [DS4Windows](https://github.com/Ryochan7/DS4Windows)

# Implemented Game(s)

**Please switch to the corresponding branch and build the code. You can also choose to download the released files.**

* [GTA V Branch](https://github.com/Solla/DualSense2Xbox/tree/GTAV_AdaptiveTrigger)
* [GTA V Release](https://github.com/Solla/DualSense2Xbox/releases/tag/v0.1_Alpha_Version)