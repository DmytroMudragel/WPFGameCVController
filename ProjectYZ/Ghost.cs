using System;
using System.Threading;
using System.Drawing;

namespace ProjectYZ
{
    class Ghost
    {
        public static Dot deathPoint { get; set; }
        public static DateTime deathTime { get; set; }

        public static bool IsButtonRed(Bitmap b)
        {
            int count = 0;
            for (int i = b.Width / 2 - 100; i < b.Width / 2 + 100; i++)
                for (int j = (int)(b.Height * 0.22); j < (int)(b.Height * 0.34); j++)
                {
                    Color color = b.GetPixel(i, j);
                    if (color.R > 90 && color.G < 10 && color.B < 10) count++;
                }
            Console.WriteLine($"Red: {count}");
            if (count > 200) return true;
            return false;
        }
        
        
        [STAThread]
        public static void GhostHandler()
        {
            GameInfo.Refresh();
            //if (!GameInfo.Dead)
            //{
            //    ConfigSettings.Cfg.Commonsettings.LastX.lastX = GameInfo.X.ToString();
            //    ConfigSettings.Cfg.Commonsettings.LastY.lastY = GameInfo.Y.ToString();
            //    ConfigSettings.ConfigWriter();
            //}
            deathPoint = new Dot(float.Parse(ConfigSettings.Cfg.Commonsettings.LastX.lastX), float.Parse(ConfigSettings.Cfg.Commonsettings.LastY.lastY));
            int tries = 0;
            do
            { 
                
                WowProcess.EnterText("/script RepopMe();");
                Thread.Sleep(4000);
                GameInfo.Refresh();
            }
            while (deathPoint.X == GameInfo.X && deathPoint.Y == GameInfo.Y && tries++ < 5);
            Navigation.ResetFlags();
            MeshHandler.AppendDotAndConnectWithNearestExist("spot", "DeathPoint", deathPoint.X, deathPoint.Y);
            TransportClass tr = new TransportClass("DeathPoint", MeshHandler.MergedSpotVendorGhost());
            Thread myThread = new Thread(new ParameterizedThreadStart(Navigation.RunRoute));
            myThread.Start(tr);
            tries = 0;
            int checks = 0;
            while (GameInfo.Dead && tries < 5 && checks++ < 2000)
            {
                Navigation.PauseNav = false;
                Thread.Sleep(150);
                Bitmap b = WowProcess.GetWowScreenshot();
                GameInfo.Refresh();
                if (Math.Pow(deathPoint.X - GameInfo.X, 2) + Math.Pow(deathPoint.Y - GameInfo.Y, 2) < Math.Pow(0.7, 2) && IsButtonRed(b))
                {
                    Combat.StopAutoMovement();
                    Thread.Sleep(300);
                    WowProcess.EnterText("/script RetrieveCorpse();");
                    Thread.Sleep(3000);
                    tries++;
                    GameInfo.Refresh();
                }
                b.Dispose();
            }
            if (checks > 1999) WowProcess.EnterText("/script RetrieveCorpse();");
            Navigation.PauseNav = false;
            MeshHandler.DeleteEdgesConnectedToDot("DeathPoint");
            MeshHandler.DeleteDot("DeathPoint");
        }
    }
}
