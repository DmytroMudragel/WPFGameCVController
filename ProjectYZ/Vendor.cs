using System;
using System.Threading;

namespace ProjectYZ
{
    class VendorH
    {
        public static bool ToVendor { get; set; }
        public static bool FromVendor { get; set; }
        public static bool MountEnabled { get; set; }
        public static bool Vendor { get; set; }

        public static void Start()
        {
            Debug.AddDebugRecord("Start Vendor!", true);
            ToVendor = true;
            FromVendor = false;
            MountEnabled = true;
            Vendor = true;
            Navigation.PauseNav = false;
            VendorHandler();
        }

        public static void VendorHandler()
        {
            Debug.AddDebugRecord("Comes to Vendor Handler!", false);
            while (!GlobalBotHandler.BotStop && Vendor)
            {
                GameInfo.Refresh();
                if (GameInfo.Dead)
                {
                    Debug.AddDebugRecord("Died!", true);
                    Debug.AddDebugRecord("Tg Died!", true);
                    Thread.Sleep(1000);
                    Navigation.StopNavigation();
                    Debug.AddDebugRecord("Nav Stopped!", true);
                    Ghost.GhostHandler();
                    Combat.ResetGlobalBuffs();
                    Combat.ActivateGlobalBuffs();
                }
                if (Navigation.EndPointName == ProfileSettings.Prof.Vendor.MountUnmountDot && ToVendor) MountEnabled = false;
                if (Navigation.EndPointName == ProfileSettings.Prof.Vendor.MountUnmountDot && FromVendor) MountEnabled = true;
                if (ProfileSettings.Prof.GlobalSettings.Mount.SpotEnabled && MountEnabled && !GameInfo.Mount && !GameInfo.Aggro)
                {
                    Combat.StopAutoMovement();
                    Navigation.PauseNavigation();
                    Combat.Mount();
                    Combat.StartMovement();

                }
                if (ProfileSettings.Prof.GlobalSettings.Mount.SpotEnabled && !MountEnabled && GameInfo.Mount) Combat.Dismount();
                if (Navigation.NavEnded)
                {
                    GameInfo.Refresh();
                    Dot returnDot = MeshHandler.GetPointByName(ProfileSettings.Prof.Vendor.ReturnDot);
                    Dot vendorDot = MeshHandler.GetPointByName(ProfileSettings.Prof.Vendor.VendorDot);
                    if (Math.Pow(returnDot.X - GameInfo.X, 2) + Math.Pow(returnDot.Y - GameInfo.Y, 2) < Math.Pow(returnDot.Radius, 2) && FromVendor)
                    {
                        Debug.AddDebugRecord("Comes to ReturnDot", true);
                        Vendor = false;
                    }
                    else if (Math.Pow(vendorDot.X - GameInfo.X, 2) + Math.Pow(vendorDot.Y - GameInfo.Y, 2) < Math.Pow(vendorDot.Radius, 2) && ToVendor)
                    {
                        Debug.AddDebugRecord("Comes to VendorDot", true);
                        ToVendor = false;
                        FromVendor = true;
                        VendorInteractor();
                    }
                    else if (ToVendor)
                    {
                        Debug.AddDebugRecord("Comes to Vendor build route", true);
                        Debug.AddDebugRecord("Calculate path", true);
                        TransportClass tr = new TransportClass(ProfileSettings.Prof.Vendor.VendorDot, MeshHandler.MergedSpotVendor());
                        Thread myThread = new Thread(new ParameterizedThreadStart(Navigation.RunRoute));
                        myThread.Start(tr);
                    }
                    else if (FromVendor)
                    {
                        Debug.AddDebugRecord("Comes from Vendor build route", true);
                        Debug.AddDebugRecord("Calculate path", true);
                        TransportClass tr = new TransportClass(ProfileSettings.Prof.Vendor.ReturnDot, MeshHandler.MergedSpotVendor());
                        Thread myThread = new Thread(new ParameterizedThreadStart(Navigation.RunRoute));
                        myThread.Start(tr);
                    }
                    Thread.Sleep(100);
                }

                //if (Whishper.IsWhishper(wowscr)) Telegram.SendMessage("Alert! New Whishper!", true);
                if (!GameInfo.Main)
                {
                    GlobalBotHandler.BotStop = true;
                    //Telegram.SendMessage("Alert! Bot Disconnected!", false);
                }
                Thread.Sleep(400);
            }

            GameInfo.Refresh();
            if (GameInfo.Bag)
            {
                GlobalBotHandler.BotStop = true;
                TelegramBot.SendMessage("Bag Fulled , pls help. Cant sell anymore.", false);
            }
        }

        public static void VendorInteractor()
        {
            Debug.AddDebugRecord("Comes to Vendor Interactor", true);
            Thread.Sleep(500);
            WowProcess.EnterText($"/tar {ProfileSettings.Prof.Vendor.Name}");
            WowProcess.PressButton(ProfileSettings.Prof.GlobalSettings.Facing.Key);
            Thread.Sleep(5000);
        }
    }
}
