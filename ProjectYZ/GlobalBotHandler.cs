using AutoIt;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace ProjectYZ
{
    public class GlobalBotHandler
    {
        public static bool BotStop { get; set; }
        public static string CurrentProfilePath { get; set; }
        public static string CurrentRoutePath { get; set; }

        private static int MountCheckCount = 0;

        public static int CurrentNumDot = 0;

        public static void RunBot()
        {
            if (!Equals(AutoItX.WinExists("World of Warcraft"), 1))
            {
                TelegramBot.SendMessage("Trying to open Wow...");
                if (WowProcess.OpenWow() == 1 && WowProcess.PrepareForStartMainWowBotLoop()) //Open Wow and check character in game 
                {
                    PrepareAndRunMainCycle();
                }
                else
                {
                    TelegramBot.SendMessage("Couldn't open Wow, maybe bad internet connection");
                    TelegramBot.IsReconectWasSuccessful = false;
                    if (WowProcess.CloseWowAndBattleNet() == 1)
                    {
                        TelegramBot.SendMessage("Wow/Battlenet were closed");
                    }
                }
            }
            else
            {
                if (WowProcess.PrepareForStartMainWowBotLoop()) 
                {
                    PrepareAndRunMainCycle();
                }
            }
        }

        public static void PrepareAndRunMainCycle()
        {
            Thread shiftControlThread = new Thread(new ThreadStart(ShiftControlAndRefreshCoord));///////////////
            shiftControlThread.SetApartmentState(ApartmentState.STA);
            shiftControlThread.Start();
            Navigation.ResetFlags();
            Console.WriteLine("Set nav ended true");
            Navigation.NavEnded = true;
            BotStop = false;
            foreach (MainDot d in ProfileSettings.Prof.MainDots.MainDot)
            {
                if (!MeshHandler.PointNameAvailability(d.Name))
                {
                    MessageBox.Show("Error maindots in profile, fix it, and reload bot pls");
                    return;
                }
            }
            GameInfo.Refresh(false);
            if (!GameInfo.Main)
            {
                MessageBox.Show("Dont see pixels , fix it, and reload bot pls");
                return;
            }
            if (WowProcess.wowRect.Width <= ProfileSettings.Prof.Combats.TargetRectangle.X + ProfileSettings.Prof.Combats.TargetRectangle.W &&
                WowProcess.wowRect.Height <= ProfileSettings.Prof.Combats.TargetRectangle.Y + ProfileSettings.Prof.Combats.TargetRectangle.H)
            {
                MessageBox.Show("Wrong target rectangle, fix it, and reload bot pls");
                return;
            }
            if (!MeshHandler.PointNameAvailability(ProfileSettings.Prof.Vendor.VendorDot) ||
                !MeshHandler.PointNameAvailability(ProfileSettings.Prof.Vendor.ReturnDot) ||
                (ProfileSettings.Prof.GlobalSettings.Mount.SpotEnabled && !MeshHandler.PointNameAvailability(ProfileSettings.Prof.Vendor.MountUnmountDot)))
            {
                MessageBox.Show("Bad dot name, fix it, and reload bot pls");
                return;
            }
            ConfigSettings.ConfigWriter();
            TelegramBot.SendMessage("Character is in game, start a new farm session");
            TelegramBot.SessionIsRunning = true;//////////////////////
            TelegramBot.localSessionCount++;
            TelegramBot.stopwatch.Start();
            TelegramBot.stopwatchForTheSessions.Start();/////////////////
            MainCycle();
        }

            //public static void RunBot()
            //{
            //    /////////
            //    if (!Equals(AutoItX.WinExists("World of Warcraft"), 1))
            //    {
            //        TelegramBot.SendMessage("Trying to open Wow...");
            //        if (WowProcess.OpenWow() == 1 && WowProcess.PrepareForStartMainWowBotLoop()) //Open Wow and check character in game 
            //        {

            //            // AntiShift run
            //            Thread shiftControlThread = new Thread(new ThreadStart(ShiftControlAndRefreshCoord));///////////////
            //            shiftControlThread.SetApartmentState(ApartmentState.STA);
            //            shiftControlThread.Start();////////////
            //            Navigation.ResetFlags();
            //            Console.WriteLine("Set nav ended true");
            //            Navigation.NavEnded = true;
            //            BotStop = false;
            //            foreach (MainDot d in ProfileSettings.Prof.MainDots.MainDot)
            //            {
            //                if (!MeshHandler.PointNameAvailability(d.Name))
            //                {
            //                    MessageBox.Show("Error maindots in profile, fix it, and reload bot pls");
            //                    return;
            //                }
            //            }
            //            GameInfo.Refresh(false);
            //            if (!GameInfo.Main)
            //            {
            //                MessageBox.Show("Dont see pixels , fix it, and reload bot pls");
            //                return;
            //            }
            //            if (WowProcess.wowRect.Width <= ProfileSettings.Prof.Combats.TargetRectangle.X + ProfileSettings.Prof.Combats.TargetRectangle.W &&
            //                WowProcess.wowRect.Height <= ProfileSettings.Prof.Combats.TargetRectangle.Y + ProfileSettings.Prof.Combats.TargetRectangle.H)
            //            {
            //                MessageBox.Show("Wrong target rectangle, fix it, and reload bot pls");
            //                return;
            //            }
            //            if (!MeshHandler.PointNameAvailability(ProfileSettings.Prof.Vendor.VendorDot) ||
            //                !MeshHandler.PointNameAvailability(ProfileSettings.Prof.Vendor.ReturnDot) ||
            //                (ProfileSettings.Prof.GlobalSettings.Mount.SpotEnabled && !MeshHandler.PointNameAvailability(ProfileSettings.Prof.Vendor.MountUnmountDot)))
            //            {
            //                MessageBox.Show("Bad dot name, fix it, and reload bot pls");
            //                return;
            //            }
            //            ConfigSettings.ConfigWriter();
            //            TelegramBot.SendMessage("Character is in game, start a new farm session");
            //            MainCycle();
            //        }
            //        else
            //        {
            //            TelegramBot.SendMessage("Couldn't open Wow, maybe bad internet connection");
            //            TelegramBot.BotСondition = false;
            //        }
            //    }
            //}

        public static void MainCycle()
        {
            try
            {
                
                if (AutoItX.WinActive("[CLASS:GxWindowClass]", "") != 1) AutoItX.WinActivate("[CLASS:GxWindowClass]", "");
                Combat.ClearTarget();
                Utils.UIDisabler();
                Thread.Sleep(new Random().Next(300, 400));
                while (!BotStop)
                {
                    GameInfo.Refresh();
                    if (GameInfo.Dead)
                    {
                        TelegramBot.DeathCount++;
                        Debug.AddDebugRecord("Died!", true);
                        TelegramBot.SendMessage("Died", true);
                        Debug.AddDebugRecord("Tg Died!", false);
                        Thread.Sleep(1000);
                        Navigation.StopNavigation();
                        Debug.AddDebugRecord("Nav Stopped!", true);
                        Thread.Sleep(300);
                        Ghost.GhostHandler();
                        Navigation.StopNavigation();
                        Combat.ResetGlobalBuffs();
                        Combat.ActivateGlobalBuffs();
                    }

                    if (!GameInfo.Aggro && GameInfo.Repair || GameInfo.Bag)
                    {
                        Debug.AddDebugRecord("Comes to Vendor!", true);
                        Navigation.StopNavigation();
                        TelegramBot.SendMessage("Vendor!", false);
                        Debug.AddDebugRecord("Stopped before Vendor", true);
                        VendorH.Start();
                        Debug.AddDebugRecord("Comes after Vendor", true);
                        Navigation.StopNavigation();
                    }
                    GameInfo.Refresh();
                    if (!GameInfo.Aggro && !GameInfo.Dead) Combat.ActivateGlobalBuffs();
                    GameInfo.Refresh();
                    if (!GameInfo.Aggro && !GameInfo.Dead) Combat.IsHeal();
                    if (Navigation.NavEnded)
                    {
                        Debug.AddDebugRecord("Calculate path", true);
                        TransportClass tr = new TransportClass(GetNewPoint(), MeshHandler.MeshDots.SpotDots.Dot);
                        Thread myThread = new Thread(new ParameterizedThreadStart(Navigation.RunRoute));
                        myThread.Start(tr);
                        Thread.Sleep(100);
                    }
                    GameInfo.Refresh();
                    //RefreshLastChatacterCoordinates();/////////////////////////////////////////////////
                    if (GameInfo.Aggro || Combat.TargetNearestNpc())
                    {
                        Navigation.PauseNavigationWithoutStop();
                        if (Combat.ReadyToCombat())
                        {
                            Debug.AddDebugRecord("Can hook mob", true);
                            if (Combat.StopAndHookMob())
                            {
                                Combat.Backward();
                                GameInfo.Refresh();
                                if (!GameInfo.TargetGrey)
                                {
                                    GameInfo.Refresh();
                                    Combat.ActivateBuffsBeforeCombat();
                                    if (!GameInfo.TargetGrey)
                                    {
                                        bool cmt = Combat.Cmbt();
                                        if (cmt) TelegramBot.CombatCount++;
                                        bool IsLooted = false;
                                        if (!GameInfo.Aggro && ProfileSettings.Prof.GlobalSettings.Loot.Enabled && cmt && !GameInfo.Dead) IsLooted = Combat.LootingProcess();
                                        if (IsLooted) TelegramBot.LootCount++;
                                        if (!GameInfo.Aggro && IsLooted && ProfileSettings.Prof.GlobalSettings.Skin.Enabled && cmt && !GameInfo.Dead)
                                        {
                                            int count = 0;
                                            Thread.Sleep(600);
                                            while (!Combat.SkiningProcess() && !GameInfo.Dead && !GameInfo.Aggro && count++ < ProfileSettings.Prof.GlobalSettings.Skin.Tries) ;
                                        }
                                    }
                                }
                            }
                        }
                        GameInfo.Refresh();
                        if (!GameInfo.Aggro && !GameInfo.Dead) Combat.IsHeal();
                        Combat.StartMovement();
                        Combat.ClearTarget();
                    }
                    else
                    {
                        if (ProfileSettings.Prof.GlobalSettings.Mount.SpotEnabled && !GameInfo.Mount && MountCheckCount > 1)
                        {
                            Combat.StopAutoMovement();
                            MountCheckCount = 0;
                            Combat.Mount();
                            Combat.StartMovement();
                        }
                        else if (!GameInfo.Mount) MountCheckCount++;

                    }
                }
                Navigation.StopNavigation();
            }
            catch
            {
                TelegramBot.SendMessage("Unexpected exaption Moo!", true);
            }
        }

        public static string GetNewPoint()
        {
            if (CurrentNumDot == ProfileSettings.Prof.MainDots.MainDot.Count) CurrentNumDot = 0;
            return ProfileSettings.Prof.MainDots.MainDot[CurrentNumDot++].Name;
        }

        public static void RefreshLastChatacterCoordinates()
        {
            GameInfo.Refresh();
            ConfigSettings.Cfg.Commonsettings.LastX.lastX = GameInfo.X.ToString();
            ConfigSettings.Cfg.Commonsettings.LastY.lastY = GameInfo.Y.ToString();
            ConfigSettings.ConfigWriter();
        }

        public static void ShiftControlAndRefreshCoord()////////////////////
        {
            try
            {
                GameInfo.Refresh(false);/////////////////
                if (!GameInfo.Dead && GameInfo.Main) RefreshLastChatacterCoordinates();///////////////////////////////////////

                if (Keyboard.IsKeyDown(Key.LeftShift))
                {
                    Thread.Sleep(100);
                    if (Keyboard.IsKeyDown(Key.LeftShift))
                    {
                        WowProcess.UnButton("LSHIFT");
                    }
                }

                if (Keyboard.IsKeyDown(Key.RightShift))
                {
                    Thread.Sleep(100);
                    if (Keyboard.IsKeyDown(Key.RightShift))
                    {
                        WowProcess.UnButton("RSHIFT");
                    }
                }
            }
            catch (Exception exeption)
            {
                Debug.AddDebugRecord(exeption.Message.ToString(), true);
            }
        }
    }
}