using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace ProjectYZ
{
    public class Combat
    {

        public class LineOfNpcName
        {
            public int XStart { get; set; }
            public int Y { get; set; }
            public int XEnd { get; set; }
            public int RedCount { get; set; }
            public int Length => XEnd - XStart + 1;
            public int X => XStart + ((XEnd - XStart) / 2);
            public int YReal => Y + 15;

            public LineOfNpcName(int xStart, int xend, int y, int redcount)
            {
                XStart = xStart;
                Y = y;
                XEnd = xend;
                RedCount = redcount;
            }
        }

        public static List<LineOfNpcName> DetectNpcs()
        {
            Bitmap Screenshot = WowProcess.GetWowScreenshot();
            List<LineOfNpcName> npcNameLine = new List<LineOfNpcName>();
            Color pixel;
            Color pixel1;
            Debug.AddDebugRecord("Seek for mobs...", false);
            for (int y = ProfileSettings.Prof.Combats.TargetRectangle.Y; y < ProfileSettings.Prof.Combats.TargetRectangle.Y + ProfileSettings.Prof.Combats.TargetRectangle.H; y += 2)
            {
                for (int x = ProfileSettings.Prof.Combats.TargetRectangle.X; x < ProfileSettings.Prof.Combats.TargetRectangle.X + ProfileSettings.Prof.Combats.TargetRectangle.W; x += 2)
                {
                    pixel = Screenshot.GetPixel(x, y);
                    bool isRedPixel = pixel.R > 140 && pixel.G < 30 && pixel.B < 30;
                    bool isYellowPixel = pixel.R > 190 && pixel.G > 190 && pixel.B < 60;

                    if (isRedPixel || (ProfileSettings.Prof.GlobalSettings.YellowMobsTarget.Enabled && isYellowPixel))
                    {
                        int countofred = 0;

                        var lengthStart = -1;
                        var lengthEnd = -1;

                        for (int subY = y - 6; subY > 0 && subY < Screenshot.Height && subY < y + 6; subY++)
                        {
                            for (int subX = x - 20; subX > 0 && subX < Screenshot.Width && subX < x + 100; subX++)
                            {
                                pixel1 = Screenshot.GetPixel(subX, subY);
                                isRedPixel = pixel1.R > 100 && pixel1.G < 60 && pixel1.B < 60;
                                isYellowPixel = pixel1.R > 190 && pixel1.G > 190 && pixel1.B < 60;
                                if (isRedPixel || (ProfileSettings.Prof.GlobalSettings.YellowMobsTarget.Enabled && isYellowPixel))
                                {
                                    countofred++;
                                    lengthStart = lengthStart == -1 ? subX : lengthStart;
                                    lengthEnd = subX;
                                }
                            }
                        }
                        if (countofred > 6) npcNameLine.Add(new LineOfNpcName(lengthStart, lengthEnd, y, countofred));
                        y += 5;
                        break;
                    }
                    else
                    {
                        x += 2;
                    }
                }

            }
            Screenshot.Dispose();
            Debug.AddDebugRecord("End seek mobs...", false);
            return npcNameLine;
        }

        public static bool TargetNearestNpc()
        {
            Debug.AddDebugRecord("Comes to target npc ...", false);
            int xr = ProfileSettings.Prof.Combats.TargetRectangle.X;
            int yr = ProfileSettings.Prof.Combats.TargetRectangle.Y;
            int wr = ProfileSettings.Prof.Combats.TargetRectangle.W;
            int hr = ProfileSettings.Prof.Combats.TargetRectangle.H;
            List<LineOfNpcName> list = DetectNpcs();
            if (list.Count <= 0)
            {
                Debug.AddDebugRecord("Out from target npc false ...", false);
                return false;
            }
            List<LineOfNpcName> SortedList = list.OrderByDescending(o => o.RedCount).ToList();
            Debug.AddDebugRecord("Found" + SortedList.Count + " npc ...", false);
            for (int i = 0; i < SortedList.Count && i < 2; i++)
            {
                int X = SortedList[i].X;
                int Y = SortedList[i].YReal;
                WowProcess.ClickInWow(X, Y + ProfileSettings.Prof.Combats.TargetHeight.Height, "LEFT", ProfileSettings.Prof.Combats.TargetMouseSpeed.Value);
                GameInfo.Refresh();
                Console.WriteLine("Distance: {0}", GameInfo.Distance);
                Console.WriteLine("Mob: {0}", GameInfo.TargetName);
                if (GameInfo.Distance <= ProfileSettings.Prof.Combats.AtackRange.Trigger && !GameInfo.TargetGrey && !GameInfo.TargetDead && MobwhiteListed(GameInfo.TargetName))
                {
                    Debug.AddDebugRecord("Out from target npc true...", false);
                    return true;
                }
                if (GameInfo.TargetHp > 0 || GameInfo.TargetDead) ClearTarget();
            }
            Debug.AddDebugRecord("Out from target npc false...", false);
            return false;
        }


        public static bool MobwhiteListed(int mobid)
        {
            foreach (int id in ProfileSettings.Prof.WhiteMobList.WhiteMob.Select(x => x.MobId).ToList())
            {
                if (id == mobid)
                {
                    Console.WriteLine("Mob White!");
                    return true;
                }

            }
            Console.WriteLine("Mob Black!");
            return false;
        }

        public static void Mount()
        {
            Debug.AddDebugRecord("Comes to Mount ...", true);
            WowProcess.PressButton(ProfileSettings.Prof.GlobalSettings.Mount.Key);
            Thread.Sleep(new Random().Next(3100, 3300));
        }

        public static void Dismount()
        {
            Debug.AddDebugRecord("Mount ...", true);
            WowProcess.PressButton(ProfileSettings.Prof.GlobalSettings.Mount.Key);
            Thread.Sleep(new Random().Next(100, 300));
        }

        public static void TargetLastTarget()
        {
            Debug.AddDebugRecord("TargetLastTarget ..", true);
            WowProcess.PressButton(ProfileSettings.Prof.GlobalSettings.TargetLastTarget.Key);
            Thread.Sleep(new Random().Next(100, 200));
        }


        public static void Jump()
        {
            Debug.AddDebugRecord("Jump ..", true);
            Thread.Sleep(new Random().Next(150, 200));
            WowProcess.PressButton(ConfigSettings.Cfg.Commonsettings.Jump.Key);
        }

        public static void FaceTarget()
        {
            Debug.AddDebugRecord("Try Facing ..", true);
            Thread.Sleep(new Random().Next(150, 200));
            WowProcess.PressButton(ProfileSettings.Prof.GlobalSettings.Facing.Key);
        }

        public static void ClearTarget()
        {
            Debug.AddDebugRecord("ClearTarget ..", true);
            Thread.Sleep(new Random().Next(150, 200));
            WowProcess.PressButton(ProfileSettings.Prof.GlobalSettings.ClearTarget.Key);
        }

        public static void Backward()
        {
            Debug.AddDebugRecord("Backward ..", true);
            Navigation.PauseNav = true;
            WowProcess.PressButton(ConfigSettings.Cfg.Commonsettings.Backward.Key);
            Thread.Sleep(new Random().Next(100, 200));
        }

        public static void StopAutoMovement()
        {
            Debug.AddDebugRecord("StopAutoMovement ..", true);
            Navigation.PauseNav = true;
            WowProcess.PressButton(ConfigSettings.Cfg.Commonsettings.StopAutoMovement.Key);
            Thread.Sleep(new Random().Next(200, 350));
        }

        public static void StartMovement()
        {
            Debug.AddDebugRecord("StartMovement ..", true);
            Navigation.PauseNav = false;
            WowProcess.PressButton(ConfigSettings.Cfg.Commonsettings.StartMovement.Key);
            Thread.Sleep(new Random().Next(100, 150));
        }

        public static void AtackTarget()
        {
            Debug.AddDebugRecord("AtackTarget ..", true);
            Thread.Sleep(new Random().Next(150, 200));
            WowProcess.PressButton(ProfileSettings.Prof.Combats.MainSpell.Key);
        }

        public static void HookMob()
        {
            Debug.AddDebugRecord("AtackTarget ..", true);
            Thread.Sleep(new Random().Next(50, 100));
            WowProcess.PressButton(ProfileSettings.Prof.Combats.HookSpell.Key);
            Thread.Sleep(new Random().Next(50, 100));
        }

        public static bool ReadyToCombat()
        {
            int countoftriedaggrobyrange = 0;
            Debug.AddDebugRecord("ReadyToCombat ..", false);
            do
            {
                if (countoftriedaggrobyrange % 6 == 0)
                {
                    FaceTarget();
                }

                Thread.Sleep(new Random().Next(150, 200));
                GameInfo.Refresh();
            }
            while (GameInfo.Distance < 60 && !GameInfo.HookSpell && !GameInfo.TargetGrey && countoftriedaggrobyrange++ < 25);

            if (countoftriedaggrobyrange > 25)
            {
                Debug.AddDebugRecord("Out from ReadyToCombat false...", false);
                return false;
            }
            Debug.AddDebugRecord("Out from ReadyToCombat true...", false);
            return true;
        }

        public static bool StopAndHookMob()
        {
            Thread.Sleep(new Random().Next(100, 150));
            Debug.AddDebugRecord("Comes to StopAndHookMob ...", false);
            int countoftriedaggromob = 0;
            do
            {
                if (countoftriedaggromob % 6 == 0)
                {
                    HookMob();
                }
                if (GameInfo.NeedToFace)
                {
                    FaceTarget();
                    continue;
                }
                if (!GameInfo.HookSpell) return false;
                Thread.Sleep(new Random().Next(70, 150));
                GameInfo.Refresh();
            }
            while (!GameInfo.Aggro && !GameInfo.TargetGrey && !GameInfo.TargetDead && GameInfo.TargetHp > 0 && countoftriedaggromob++ < 20);

            if (countoftriedaggromob > 30)
            {
                Debug.AddDebugRecord("Out from StopAndHookMob false...", false);
                return false;
            }
            Debug.AddDebugRecord("Out from StopAndHookMob true...", false);
            return true;
        }

        public static bool Cmbt()
        {
            int tries = 0;
            int oldHp = GameInfo.TargetHp;
            Debug.AddDebugRecord("Comes to combat ...", false);
            do
            {
                GameInfo.Refresh();
                CheckHpAndMakeAny();
                CheckMpAndMakeAny();
                if (GameInfo.Aggro) AtackTarget();
                Thread.Sleep(100);
                GameInfo.Refresh();
                if (GameInfo.NeedToFace || oldHp <= GameInfo.TargetHp + 2)
                {
                    FaceTarget();
                    continue;
                }
                GameInfo.Refresh();

                if (!GameInfo.CastStart) continue;
                GameInfo.Refresh();
                oldHp = GameInfo.TargetHp;
                while (!GameInfo.CastReady && !GameInfo.CastFailed && GameInfo.Aggro)
                {
                    if (GameInfo.NeedToFace || oldHp <= GameInfo.TargetHp + 2)
                    {
                        FaceTarget();
                        break;
                    }
                    GameInfo.Refresh();
                    Thread.Sleep(25);
                }
                
            }
            while (GameInfo.Aggro && !GameInfo.TargetGrey && tries++ < 30 && !GameInfo.Dead);

            if (tries > 29 || GameInfo.TargetGrey)
            {
                Debug.AddDebugRecord("Out from combat false ...", false);
                return false;
            }
            Debug.AddDebugRecord("Out from combat true...", false);
            return true;
        }

        public static void ActivateBuffsBeforeCombat()
        {
            Debug.AddDebugRecord("Activating buffs before combat...", true);
            Debug.AddDebugRecord(ProfileSettings.Prof.Buffs.Buff.Count.ToString(), false);
            
            foreach (Buff b in ProfileSettings.Prof.Buffs.Buff)
            {
                //Console.WriteLine($"Buff key: {b.Key} , buff delay {(DateTime.Now - b.lastUse).TotalSeconds}");
                if (b.BeforeCombat && (DateTime.Now - b.lastUse).TotalSeconds > b.CastDelay)
                {
                    WowProcess.PressButton(b.Key);
                    b.lastUse = DateTime.Now;
                    Thread.Sleep(new Random().Next(1510, 1550));
                }
            }
            Debug.AddDebugRecord("Out from activating buffs before combat...", false);
        }

        public static void ResetGlobalBuffs()
        {
            Debug.AddDebugRecord("Reseting buffs..", true);
            foreach (Buff b in ProfileSettings.Prof.Buffs.Buff)
            {
                if (!b.BeforeCombat)
                {
                    b.lastUse = DateTime.Now.AddHours(-1);
                }
            }
            Debug.AddDebugRecord("Out from reseting buffs before combat...", false);
        }

        public static void ActivateGlobalBuffs()
        {
            Debug.AddDebugRecord("ActivateGlobalBuffs buffs...", true);
            Debug.AddDebugRecord(ProfileSettings.Prof.Buffs.Buff.Count.ToString(), false);
            
            foreach (Buff b in ProfileSettings.Prof.Buffs.Buff)
            {
                //Console.WriteLine($"Buff key: {b.Key} , buff delay {(DateTime.Now - b.lastUse).TotalSeconds}");
                if (!b.BeforeCombat && (DateTime.Now - b.lastUse).TotalSeconds > b.CastDelay)
                {
                    WowProcess.PressButton(b.Key);
                    b.lastUse = DateTime.Now;
                    Thread.Sleep(new Random().Next(1500, 1550));
                }
            }
            Debug.AddDebugRecord("Out from activating buffs...", false);
        }

        public static void CheckHpAndMakeAny()
        {
           // Debug.AddDebugRecord("Target Hp: " + GameInfo.TargetHp + " My curr HP: " + GameInfo.Hp, true);
            
            foreach (HpBuff buff in ProfileSettings.Prof.HpBuffs.HpBuff)
            {
                GameInfo.Refresh();
                if (!GameInfo.Aggro) break;
                //Console.WriteLine($"Buff key: {buff.Key} , buff delay {(DateTime.Now - buff.lastUse).TotalSeconds}");
                //Debug.AddDebugRecord("Seconds: " + (DateTime.Now - buff.lastUse).TotalSeconds, true);

                if (!buff.IsTarget && buff.Trigger >= GameInfo.Hp && buff.Trigger <= GameInfo.Hp + 45 && (DateTime.Now - buff.lastUse).TotalSeconds > buff.CastDelay)
                {
                    Debug.AddDebugRecord("Buff at:" + buff.Trigger, false);
                    WowProcess.PressButton(buff.Key);
                    buff.lastUse = DateTime.Now;
                    Thread.Sleep(new Random().Next(1500, 1510));
                }
                if (buff.IsTarget && buff.Trigger >= GameInfo.TargetHp && buff.Trigger <= GameInfo.TargetHp + 45 && (DateTime.Now - buff.lastUse).TotalSeconds > buff.CastDelay)
                {
                    Debug.AddDebugRecord("Buff at:" + buff.Trigger, false);
                    WowProcess.PressButton(buff.Key);
                    buff.lastUse = DateTime.Now;
                    Thread.Sleep(new Random().Next(1500, 1510));
                }
            }
        }

        public static void CheckMpAndMakeAny()
        {
            Debug.AddDebugRecord("My curr MP: " + GameInfo.Mp, false);
            
            foreach (MpBuff buff in ProfileSettings.Prof.MpBuffs.MpBuff)
            {
                GameInfo.Refresh();
                if (!GameInfo.Aggro) break;
                //Console.WriteLine($"Buff key: {buff.Key} , buff delay {(DateTime.Now - buff.lastUse).TotalSeconds}");
                if (buff.Trigger >= GameInfo.Mp && buff.Trigger <= GameInfo.Mp + 45 && (DateTime.Now - buff.lastUse).TotalSeconds > buff.CastDelay)
                {
                    Thread.Sleep(250);
                    Debug.AddDebugRecord("Buff at:" + buff.Trigger, false);
                    WowProcess.PressButton(buff.Key);
                    buff.lastUse = DateTime.Now;
                    Thread.Sleep(new Random().Next(1500, 1510));
                }
            }
        }

        public static bool LootingProcess()
        {
            Debug.AddDebugRecord("Looting...", true);
            int count = 0;
            do
            {
                TargetLastTarget();
                Thread.Sleep(200);
                GameInfo.Refresh();
            }
            while (!GameInfo.Aggro && !GameInfo.TargetDead && count++ < 5);
            if (count > 5) return false;
            count = 0;
            int countofchecks = 0;
            while (!GameInfo.Aggro && !GameInfo.TargetDead && countofchecks++ < 5)
            {
                GameInfo.Refresh();
                Thread.Sleep(500);
            }
            if (countofchecks < 5) FaceTarget();
            else
            {
                Debug.AddDebugRecord("Looted bad isDead...", true);
                return false;
            }
            GameInfo.Refresh();
            while (!GameInfo.LootReady && !GameInfo.Aggro && count++ < 40)
            {
                GameInfo.Refresh();
                Thread.Sleep(100);
            }
            Thread.Sleep(500);
            if (count > 40)
            {
                Debug.AddDebugRecord("Looted bad...", true);
                return false;
            }
            Debug.AddDebugRecord("Looted good...", true);
            return true;
        }

        public static bool SkiningProcess()
        {
            Debug.AddDebugRecord("Skinning...", true);
            int count = 0;
            do
            {
                TargetLastTarget();
                Thread.Sleep(200);
                GameInfo.Refresh();
            }
            while (!GameInfo.Aggro && !GameInfo.TargetDead && count++ < 5);
            if (count > 5) return false;
            count = 0;
            int countofchecks = 0;
            while (!GameInfo.Aggro && !GameInfo.TargetDead && countofchecks++ < 5)
            {
                GameInfo.Refresh();
                Thread.Sleep(500);
            }
            if (countofchecks < 5) FaceTarget();
            else
            {
                Debug.AddDebugRecord("Skinned bad isDead...", true);
                return false;
            }
            GameInfo.Refresh();
            //Console.WriteLine("Aboba!!!");
            while (!GameInfo.LootReady && !GameInfo.Aggro && count++ < 9)
            {
                Thread.Sleep(200);
                GameInfo.Refresh();
            }
            if (count >= 9)
            {
                Debug.AddDebugRecord("Skinned bad...", true);
                return false;
            }
            Debug.AddDebugRecord("Skinned good...", true);
            Thread.Sleep(500);
            TelegramBot.GatherCount++;
            return true;
        }

        public static bool IsHeal()
        {
            Debug.AddDebugRecord("Healing...", true);
            GameInfo.Refresh();
            if (GameInfo.FoodCont == 0 && !GameInfo.Aggro)
            {
                Debug.AddDebugRecord("Food null...", true);
                Navigation.PauseNavigation();
                WowProcess.PressButton(ProfileSettings.Prof.GlobalSettings.ConjureFood.Key);
                Thread.Sleep(new Random().Next(3600, 3700));
            }

            GameInfo.Refresh();

            if (GameInfo.WaterCont == 0 && !GameInfo.Aggro)
            {
                Debug.AddDebugRecord("Water null...", true);
                Navigation.PauseNavigation();
                WowProcess.PressButton(ProfileSettings.Prof.GlobalSettings.ConjureWater.Key);
                Thread.Sleep(new Random().Next(3600, 3700));
            }

            GameInfo.Refresh();

            bool used = false;

            if (GameInfo.Hp < ProfileSettings.Prof.GlobalSettings.UseFood.Trigger || GameInfo.Mp < ProfileSettings.Prof.GlobalSettings.UseWater.Trigger && !GameInfo.Aggro)
            {
                Debug.AddDebugRecord("Use food...", true);
                Navigation.PauseNavigation();
                WowProcess.PressButton(ProfileSettings.Prof.GlobalSettings.UseFood.Key);
                Thread.Sleep(new Random().Next(400, 500));
                Debug.AddDebugRecord("Use water...", true);
                Navigation.PauseNavigation();
                WowProcess.PressButton(ProfileSettings.Prof.GlobalSettings.UseWater.Key);
                used = true;
            }

            GameInfo.Refresh();

            if (!used)
            {
                Debug.AddDebugRecord("Not used heal...", true);
                if (Navigation.PauseNav) StartMovement();
                return false;
            }

            int hpTrigger = ProfileSettings.Prof.GlobalSettings.UseFood.Trigger, mpTrigger = ProfileSettings.Prof.GlobalSettings.UseWater.Trigger;
            int oldHp, oldMp, count = 0;
            do
            {
                oldHp = GameInfo.Hp; oldMp = GameInfo.Mp;
                int waitCount = 0;
                while (!GameInfo.Aggro && ++waitCount < 20)
                {
                    GameInfo.Refresh();
                    Thread.Sleep(100);
                }
            }
            while ((GameInfo.Hp < hpTrigger | GameInfo.Mp < mpTrigger) && (oldHp + 2 < GameInfo.Hp | oldMp + 2 < GameInfo.Mp) && !GameInfo.Dead && !GameInfo.Aggro && count++ < 10 && used);
           
            if (GameInfo.Hp > hpTrigger && GameInfo.Mp > mpTrigger)
            {
                Debug.AddDebugRecord("Heal ok...", true);
                if (Navigation.PauseNav) StartMovement();
                return true;
            }

            if (Navigation.PauseNav) StartMovement();
            Debug.AddDebugRecord("Heal not ok...", true);
            return false;
        }

    }
}
