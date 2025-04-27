using System.Drawing;
using System;
using AutoIt;

namespace ProjectYZ
{
    static class GameInfo
    {
        private static readonly object refreshlock = new object();
        private static DateTime lastWhihperReport { get; set; }

        public static float X;
        public static float Y;
        public static int Angle;
        public static bool Mount;
        public static bool Aggro;
        public static int Hp;
        public static int Mp;
        public static int TargetHp;
        public static bool Repair;
        public static bool Bag;
        public static int Distance;
        public static bool HookSpell;
        public static bool TargetGrey;
        public static bool TargetDead;
        public static bool Whishper;
        public static bool NeedToFace;
        public static bool CastReady;
        public static bool CastFailed;
        public static bool CastStart;
        public static bool LootReady;
        public static bool Interupted;
        public static bool Dead;
        public static int WaterCont;
        public static int FoodCont;
        public static int TargetName;
        public static bool Main;
        public static int Gold;

        private static int GetXlenByPos(int pos) => ConfigSettings.Cfg.Pixelsettings.Start + ConfigSettings.Cfg.Pixelsettings.Size * (pos - 1) + ConfigSettings.Cfg.Pixelsettings.Size / 2;

        private static float SetX(Bitmap b)
        {
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.X.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return color.R + color.G / 100f;
        }

        private static float SetY(Bitmap b)
        {
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Y.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return color.R + color.G / 100f;
        }

        private static int SetAngle(Bitmap b)
        {
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Angle.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return color.R * 100 + color.G;
        }

        private static bool SetMount(Bitmap b)
        {
            Color standart = ColorTranslator.FromHtml("#" + ConfigSettings.Cfg.Pixelsettings.Mounted.ColorHex);
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Mounted.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return standart == color;
        }

        private static bool SetAggro(Bitmap b)
        {
            Color standart = ColorTranslator.FromHtml("#" + ConfigSettings.Cfg.Pixelsettings.Aggro.ColorHex);
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Aggro.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return standart == color;
        }

        private static void SetHpMpTargetHp(Bitmap b, out int Hp1, out int Mp1, out int TargetHp1)
        {
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Hp.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            Hp1 = color.R;
            Mp1 = color.G;
            TargetHp1 = color.B;
        }

        private static bool SetRepair(Bitmap b)
        {
            Color standart = ColorTranslator.FromHtml("#" + ConfigSettings.Cfg.Pixelsettings.Repair.ColorHex);
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Repair.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return standart == color;
        }

        private static bool SetBag(Bitmap b)
        {
            Color standart = ColorTranslator.FromHtml("#" + ConfigSettings.Cfg.Pixelsettings.Bag.ColorHex);
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Bag.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return standart == color;
        }

        private static int SetDistance(Bitmap b)
        {
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Targetdistance.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return color.R == 60 ? int.MaxValue : color.R;
        }

        private static bool SetWhishper(Bitmap b)
        {
            Color standart = ColorTranslator.FromHtml("#" + ConfigSettings.Cfg.Pixelsettings.Whishper.ColorHex);
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Whishper.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return standart == color;
        }

        private static bool SetNeedToFace(Bitmap b)
        {
            Color standart = ColorTranslator.FromHtml("#" + ConfigSettings.Cfg.Pixelsettings.Needtoface.ColorHex);
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Needtoface.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return standart == color;
        }

        private static void SetCastStatus(Bitmap b, out bool CastStart, out bool CastInterrupted, out bool CastSuccess)
        {
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Spellstatus.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            CastStart = color.R == 0 && color.G == 0 && color.B == 0;
            CastInterrupted = color.R == 2 && color.G == 0 && color.B == 0;
            CastSuccess = color.R == 1 && color.G == 0 && color.B == 0;
        }

        private static bool SetLootReady(Bitmap b)
        {
            Color standart = ColorTranslator.FromHtml("#" + ConfigSettings.Cfg.Pixelsettings.Lootready.ColorHex);
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Lootready.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return standart == color;
        }

        private static bool SetDead(Bitmap b)
        {
            Color standart = ColorTranslator.FromHtml("#" + ConfigSettings.Cfg.Pixelsettings.Died.ColorHex);
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Died.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return color != standart;
        }

        private static void SetWaterFood(Bitmap b, out int FoodCount, out int WaterCount)
        {
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Water.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            FoodCount = color.G;
            WaterCount = color.B;
        }

        private static int SetMobName(Bitmap b)
        {
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Mobid.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return (color.R * 10000) + (color.G * 100) + color.B;
        }

        private static bool SetMain(Bitmap b)
        {
            Color standart = ColorTranslator.FromHtml("#" + ConfigSettings.Cfg.Pixelsettings.Isingame.ColorHex);
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Isingame.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return color == standart;
        }

        private static int SetGold(Bitmap b)
        {
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Moneycount.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return (color.R * 256 * 256) + (color.G * 256) + color.B;
        }

        private static bool SetFailedAttempt(Bitmap b)
        {
            Color standart = ColorTranslator.FromHtml("#" + ConfigSettings.Cfg.Pixelsettings.FailedAttempt.ColorHex);
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.FailedAttempt.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            return color == standart;
        }

        private static void SetTargetDeadGreyHookSpell(Bitmap b, out bool TargetGrey, out bool TargetDied, out bool HookSpell)
        {
            Color color = b.GetPixel(GetXlenByPos(ConfigSettings.Cfg.Pixelsettings.Targetgrey.Position), ConfigSettings.Cfg.Pixelsettings.Height);
            TargetDied = color.R == 1;
            TargetGrey = color.G == 1;
            HookSpell = color.B == 1;
        }


        public static void Refresh(bool checkInfo = true)
        {
            lock (refreshlock)
            {
                
                    Bitmap b = WowProcess.GetWowScreenshot();
                    X = SetX(b);
                    Y = SetY(b);
                    Angle = SetAngle(b);
                    Mount = SetMount(b);
                    Aggro = SetAggro(b);
                    SetHpMpTargetHp(b, out Hp, out Mp, out TargetHp);
                    Repair = SetRepair(b);
                    Bag = SetBag(b);
                    Distance = SetDistance(b);
                    SetTargetDeadGreyHookSpell(b, out TargetGrey, out TargetDead, out HookSpell);
                    Whishper = SetWhishper(b);
                    NeedToFace = SetNeedToFace(b);
                    SetCastStatus(b, out CastStart, out CastFailed, out CastReady);
                    LootReady = SetLootReady(b);
                    Interupted = SetFailedAttempt(b);
                    Dead = SetDead(b);
                    SetWaterFood(b, out FoodCont, out WaterCont);
                    TargetName = SetMobName(b);
                    Main = SetMain(b);
                    Gold = SetGold(b);
                    b.Dispose();
                    if (checkInfo) IfWhishperOrDisconnect();
                
            }
        }

        public static void IfWhishperOrDisconnect()
        {
            if (!Main)
            {
                Utils.ReConnect();
                Bitmap b = WowProcess.GetWowScreenshot();
                if (!SetMain(b))
                {
                    TelegramBot.SendMessage("Reconnect error, finishing current session..");
                    b.Dispose();
                    TelegramBot.IsReconectWasSuccessful = false;
                }
                Utils.UIDisabler();
            }
            if (Whishper && (DateTime.Now - lastWhihperReport).TotalSeconds > 6)
            {
                TelegramBot.SendMessage("|New whishper|", true);
                lastWhihperReport = DateTime.Now;
            }
        }
    }
}
