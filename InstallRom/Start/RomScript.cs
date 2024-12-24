﻿using ADBSevices;
using InstallRom.Data;
using InstallRom.Logic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstallRom.Start
{
    public delegate void UpdateProgressOnUI(DevicesCustomData device, string column, string value);

    public class RomScript
    {
        private UpdateProgressOnUI progress;
        private LogicStartScript startScript;
        private ADBService ADBService;
        private ADBSever sever;
        public RomScript(UpdateProgressOnUI progressMethod)
        {
            progress = progressMethod;
            startScript = new LogicStartScript();
            ADBService = new ADBService();
            sever = new ADBSever();
        }

        public void StartRomScript(DevicesCustomData device)
        {
            progress?.Invoke(device, "Progress", $"Start {device.Serial}");
            Console.WriteLine(device.Serial);

            // install rom 
            // import information device by device.serial

            DevicesCustomData result = startScript.callGetDataDeviceById(device.Serial);
            progress?.Invoke(device, "Progress", $"get file rom and boot device");
            device = new DevicesCustomData
            {
                Serial = device.Serial,
                boot = result.boot,
                rom = result.rom,
            };
            installRom(device);
        }
        public async void installRom(DevicesCustomData device)
        {
            bool result;
            string textResult;
            progress(device, "Progress", $"Start install rom");
            ADBService.StartBootloader(device.Serial);
            await Task.Delay(2000);
            while (true)
            {
                if (sever.FlasbootIsDeviceOnline(device.Serial))
                {
                    progress(device, "Progress", $"Flasboot");
                    await Task.Delay(10000);
                    //Thread.Sleep(1000);
                    progress(device, "Status", $"Online");
                    // install boot img
                    await Task.Delay(6000);
                    ADBService.UnlockBootloader(device.Serial);
                    await Task.Delay(10000);
                    progress(device, "Progress", $"Install {device.boot}");
                    (result,textResult) = await ADBService.FlashBootImageAsync(device.boot, device.Serial);
                    if (result)
                    {
                        progress(device, "Progress", $"Install {device.boot} {textResult}!");
                        ADBService.FastBootReboot(device.Serial);
                        // install rom
                        /*progress(device, "Progress", $"Check home!");
                        await Task.Delay(30000);
                        while (true)
                        {
                            bool isBootCompleted = ADBService.CheckDeviceBootCompleted(device.Serial);

                            // Hiển thị kết quả
                            if (isBootCompleted)
                            {
                                progress(device, "Progress", $"Reboot success!");
                                break;
                            }
                            else
                            {
                                await Task.Delay(10000);
                            }
                        }*/
                        progress(device, "Progress", $"Recovery!");
                        /* await Task.Delay(10000);
                         bool isSuccess = ADBService.RebootToRecovery(device.Serial);
                         if (isSuccess)
                         {
                             await Task.Delay(10000);
                             while (true)
                             {
                                 bool isDeviceConnected = ADBService.CheckDevice(device.Serial);
                                 if (isDeviceConnected)
                                 {
                                     break;
                                 }
                                 else
                                 {
                                     await Task.Delay(10000);
                                 }
                             }*/

                        // success recovery
                        progress(device, "Progress", $"Install {device.rom}!");
                        await Task.Delay(10000);
                        while (true)
                        {
                            bool isSideloadSuccessful = ADBService.RunSideload(device.Serial, device.rom);

                            if (isSideloadSuccessful)
                            {
                                Console.WriteLine("install rom completed.");
                                progress(device, "Progress", $"Rom {device.rom} install completed!");
                                break; // Thoát vòng lặp nếu thành công
                            }
                            else
                            {
                                await Task.Delay(5000);
                            }
                        }



                    }
                    else { progress(device, "Progress", $"Install boot.img Error! {textResult}"); break; }
                    break;
                }
                else
                {
                    progress(device, "Progress", $"Error");
                }
            }
        }
    }
}
