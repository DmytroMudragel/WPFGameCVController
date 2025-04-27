using AutoIt;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows;
using System.Text.RegularExpressions;
using IronOcr;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace ProjectYZ
{
    public class WowProcess
    {
        private static readonly object balanceLock = new object();
        private static readonly object scrLock = new object();
        private static readonly Random random = new Random();

        #region For Wow 

        public static Rectangle wowRect;

        /// <summary>
        /// Provides random delay: between delay time and delay time +7%
        /// </summary>
        /// <param name="delayTime"></param>
        public static void Delay(int delayTime)
        {
            Thread.Sleep(random.Next(delayTime, delayTime + (7 * (delayTime / 100))));
        }

        /// <summary>
        /// Provides random time for game sessions from Time to Time + 20%
        /// </summary>
        /// <param name="delayTime"></param>
        public static int GameSessionMinutesTime(int Time)
        {
            return random.Next(Time, Time + (20 * (Time / 100)));
        }

        /// <summary>
        /// Uses for getting wow Rectangle
        /// </summary>
        /// <returns></returns>
        public static bool GetWowRect() 
        {
            if (!DetectWowWindow())
            {
                return false;
            }
            if (AutoItX.WinGetTitle("[ACTIVE]") != "World of Warcraft")
            {
                AutoItX.WinActivate("[CLASS:GxWindowClass]");
            }

            wowRect = AutoItX.WinGetPos("[CLASS:GxWindowClass]");

            return true;
        }

        /// <summary>
        /// Uses for getting wow Handle
        /// </summary>
        /// <returns></returns>
        public static bool DetectWowWindow()
        {
            return AutoItX.WinExists("[CLASS:GxWindowClass]") == 1;
        }

        /// <summary>
        /// Uses for getting wow screenshot
        /// </summary>
        /// <returns></returns>
        public static Bitmap GetWowScreenshot()
        { 
            lock (scrLock)
            {
                if (!GetWowRect())
                {
                    Console.WriteLine("Lost wow Rect!");
                    return null;
                }
                Rectangle bounds = new Rectangle(wowRect.Left, wowRect.Top, wowRect.Right - wowRect.Left, wowRect.Bottom - wowRect.Top);
                Bitmap B = new Bitmap(bounds.Width, bounds.Height);
                using (Graphics graphics = Graphics.FromImage(B))
                {
                    graphics.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
                }
                return B;
            }
        }

        /// <summary>
        /// Uses for click in wow window
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="button"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static bool ClickInWow(int x, int y, string button = "RIGHT", int speed = 1) 
        {
            EmulateMouseMove.EmulateMoveMouse(wowRect.Left + x, wowRect.Top + y, speed);
            AutoItX.MouseMove(wowRect.Left + x, wowRect.Top + y, 6);
            Thread.Sleep(new Random().Next(10, 40));
            Debug.AddDebugRecord(wowRect.Left + x + " " + (wowRect.Top + y), true);
            AutoItX.MouseDown(button);
            Thread.Sleep(new Random().Next(10, 40));
            AutoItX.MouseUp(button);
            return true;
        }

        /// <summary>
        /// Realistic text entering (char by char)
        /// </summary>
        /// <param name="text"></param>
        public static void EnterText(string text)
        {
            PressButton("ENTER");
            Delay(170);
            for (int i = 0; i < text.Length; i++)
            {
                AutoItX.Send(text[i].ToString());
                Delay(20);
            }
            Delay(140);
            PressButton("ENTER");
        }

        /// <summary>
        /// Uses for mouse moving in wow
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="speed"></param>
        public static void MouseMoveInMow(int x, int y, int speed = 2)
        {
            EmulateMouseMove.EmulateMoveMouse(wowRect.Left + x, wowRect.Top + y, speed);
        }

        /// <summary>
        /// Uses for pressbutton (with write to debug log)
        /// </summary>
        /// <param name="button"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static bool PressButton(string button, int time = -1) 
        {
            if (time == -1)
            {
                time = new Random().Next(50, 110);
            }
            lock (balanceLock)
            {
                StackTrace stackTrace = new StackTrace();
                Console.WriteLine(stackTrace.GetFrame(1).GetMethod().Name);
                Debug.AddDebugRecord("| Press Button : " + button, true);
                DownButton(button);
                Thread.Sleep(time);
                Debug.AddDebugRecord(DateTime.Now + " " + DateTime.Now.Millisecond + " | UnPress Button : " + button, false);
                UnButton(button);
            }
            return true;

        }

        /// <summary>
        /// Uses for press button while not unpress it
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool DownButton(string button)
        {
            AutoItX.Send("{" + button + " down}");
            return true;
        }

        /// <summary>
        /// unpress button from DownButton
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool UnButton(string button)
        {
            AutoItX.Send("{" + button + " up}");
            return true;
        }

        /// <summary>
        /// Uses for generating random application title
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2;
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }

        /// <summary>
        /// Clicks "Enter world" button and checks wheather the distance to the nearest route point is < 40  
        /// </summary>
        /// <returns></returns>
        public static bool PrepareForStartMainWowBotLoop()
        {
            if (Equals(AutoItX.WinExists("World of Warcraft"), 1))
            {
                AutoItX.WinActivate("[CLASS:GxWindowClass]", "");
                Delay(1200);
                PressButton("ENTER");
                Delay(12000);
                GameInfo.Refresh(false);
                if (GameInfo.Main == true)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Open Battle.net application with update (if needed) and login
        /// </summary>
        /// <returns>1 - Wow was successfully opened; 0 - fail to open Wow; </returns>
        public static int OpenWow()
        {
            try
            {
                Bitmap imgToFind = new Bitmap(@"BtlNet.png");
                Bitmap tempScreen = GetScreenShot();
                Rectangle res = FindImageOnScreen(imgToFind, tempScreen, false);
                //SendMessage($"Location = {res.Location}, Size = {res.Size}");
                EmulateMouseMove.EmulateMoveMouse(res.X, res.Y);//20
                AutoItX.MouseClick("LEFT", res.X, res.Y, 2, 30);
                Delay(12000);
                if (Equals(AutoItX.WinExists("Battle.net Login"), 1))
                {
                    AutoItX.WinActivate("[CLASS:Qt5QWindowIcon]", "");
                    int LoginXPos = AutoItX.WinGetPos("Battle.net Login").X;
                    int LoginYPos = AutoItX.WinGetPos("Battle.net Login").Y;
                    EmulateMouseMove.EmulateMoveMouse(LoginXPos + 220, LoginYPos + 220, 30);
                    Delay(70);
                    AutoItX.MouseDown("LEFT");
                    Delay(70);
                    EmulateMouseMove.EmulateMoveMouse(LoginXPos + 34, LoginYPos + 221);
                    AutoItX.MouseUp("LEFT");
                    Delay(170);
                    for (int i = 0; i < ConfigSettings.Cfg.Commonsettings.Login.LoginString.Length; i++)
                    {
                        AutoItX.Send(ConfigSettings.Cfg.Commonsettings.Login.LoginString[i].ToString());
                        Delay(200);
                    }
                    Delay(140);
                    EmulateMouseMove.EmulateMoveMouse(LoginXPos + 40, LoginYPos + 280);
                    Delay(140);
                    AutoItX.MouseClick("LEFT", LoginXPos + 40, LoginYPos + 280, 1, 30);
                    Delay(170);
                    for (int i = 0; i < ConfigSettings.Cfg.Commonsettings.Password.PasswordString.Length; i++)
                    {
                        AutoItX.Send(ConfigSettings.Cfg.Commonsettings.Password.PasswordString[i].ToString());
                        Delay(200);
                    }
                    Delay(140);
                    PressButton("ENTER");
                    Delay(10000);
                }
            }
            catch (Exception exeption)
            {
                Debug.AddDebugRecord(exeption.Message.ToString(), true);
                TelegramBot.SendMessage("Fail to login in Wow", exeption);
                return 0;
            }

            if (Equals(AutoItX.WinExists("Battle.net"), 1))
            {
                AutoItX.WinActivate("[CLASS:Chrome_WidgetWin_0]", "");
                int BattleNetXPos = AutoItX.WinGetPos("Battle.net").X;
                int BattleNetYPos = AutoItX.WinGetPos("Battle.net").Y;
                Delay(80);
                EmulateMouseMove.EmulateMoveMouse(BattleNetXPos + 160, BattleNetYPos + 530);
                int count = 0;
                while (count <= 100 && !Equals(AutoItX.WinExists("World of Warcraft"), 1))
                {
                    AutoItX.MouseClick("LEFT", BattleNetXPos + 160, BattleNetYPos + 530, 1, 30);
                    Delay(20000);
                }
                if (count >= 100)
                {
                    TelegramBot.SendMessage("Wow might been updating too long.. you shall check it");
                    return 0;
                }
                else if (Equals(AutoItX.WinExists("World of Warcraft"), 1))
                {
                    return 1;
                }
            }
            return 0;
        }

        /// <summary>
        /// Closes Wow and Battle.net app 
        /// </summary>
        /// <returns> 0 - fail to close Wow and battle.net app ; 1 - Wow and battle.net were successfully closed </returns>
        public static int CloseWowAndBattleNet()
        {
            try
            {
                if (Equals(AutoItX.WinExists("World of Warcraft"), 1))
                {
                    AutoItX.WinActivate("[CLASS:GxWindowClass]", "");
                    Process[] wowProcess = Process.GetProcessesByName("WowClassic");
                    if (Equals(AutoItX.ProcessClose(wowProcess[0].Id.ToString()), 1) && !Equals(AutoItX.WinExists("World of Warcraft"), 1))
                    {
                        //SendMessage("Wow was successfully closed");
                    }
                    else
                    {
                        AutoItX.WinActivate("[CLASS:GxWindowClass]", "");
                        int WowXPos = AutoItX.WinGetPos("World of Warcraft").X;
                        int WowYPos = AutoItX.WinGetPos("World of Warcraft").Y;
                        Delay(700);
                        EmulateMouseMove.EmulateMoveMouse(WowXPos + 1580, WowYPos + 20, 30);
                        Delay(70);
                        AutoItX.MouseClick("LEFT");
                        Delay(700);
                        if (!Equals(AutoItX.WinExists("World of Warcraft"), 1))
                        {
                            //SendMessage("Wow was successfully closed");
                        }
                        else
                        {
                            //SendMessage("Some error, fail to close Wow");
                        }
                    }
                }


                //if (Equals(AutoItX.WinExists("World of Warcraft"), 1))
                //{
                //    AutoItX.WinActivate("[CLASS:GxWindowClass]", "");
                //    Process[] wowProcess = Process.GetProcessesByName("WowClassic");
                //    int WowXPos = AutoItX.WinGetPos("World of Warcraft").X;
                //    int WowYPos = AutoItX.WinGetPos("World of Warcraft").Y;
                //    Delay(700);
                //    EmulateMouseMove.EmulateMoveMouse(WowXPos + 1580, WowYPos + 20, 30);
                //    Delay(70);
                //    AutoItX.MouseClick("LEFT");
                //    if (!Equals(AutoItX.WinExists("World of Warcraft"), 1))
                //    {
                //        //TelegramBot.SendMessage("Wow was successfully closed");
                //    }
                //    else
                //    {
                //        Delay(150);
                //        if (Equals(AutoItX.ProcessClose(wowProcess[0].Id.ToString()), 1) && !Equals(AutoItX.WinExists("World of Warcraft"), 1))
                //        {
                //            // TelegramBot.SendMessage("Wow was successfully closed");
                //        }
                //        else
                //        {
                //            //TelegramBot.SendMessage("Some error, fail to close Wow");
                //            return 0;
                //        }
                //    }
                //}



                if (Equals(AutoItX.WinExists("Battle.net"), 1))
                {
                    AutoItX.WinActivate("[CLASS:Chrome_WidgetWin_0]", "");
                    int BattleNetXPos = AutoItX.WinGetPos("Battle.net").X;
                    int BattleNetYPos = AutoItX.WinGetPos("Battle.net").Y;
                    Delay(700);
                    EmulateMouseMove.EmulateMoveMouse(BattleNetXPos + 980, BattleNetYPos + 15, 30);
                    Delay(70);
                    AutoItX.MouseClick("LEFT");
                    Delay(70);
                    AutoItX.MouseClick("LEFT");
                    Delay(1000);
                    if (!Equals(AutoItX.WinExists("Battle.net"), 1))
                    {
                        //TelegramBot.SendMessage("BattleNet was successfully closed");
                        return 1;
                    }
                    else
                    {
                        AutoItX.WinActivate("[CLASS:Chrome_WidgetWin_0]", "");
                        Process[] BattleNetProcess = Process.GetProcessesByName("Battle.net");
                        if (Equals(AutoItX.ProcessClose(BattleNetProcess[0].Id.ToString()), 1) && !Equals(AutoItX.WinExists("Battle.net"), 1))
                        {
                            //TelegramBot.SendMessage("BattleNet was successfully closed");
                            return 1;
                        }
                        else
                        {
                            //TelegramBot.SendMessage("Some error, fail to close BattleNet");
                            return 0;
                        }
                    }
                }

                if (!Equals(AutoItX.WinExists("Battle.net"), 1) && !Equals(AutoItX.WinExists("World of Warcraft"), 1))
                {
                    return 1;
                }

                return 0;
            }
            catch (Exception exeption)
            {
                Debug.AddDebugRecord(exeption.Message.ToString(), true);
                return 0;
            }
        }

        #endregion

        #region Base functions for any window

        /// <summary>
        /// Parse text from image
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static string ParseBitmap(Bitmap image)
        {
            var Ocr = new IronTesseract();
            using (var Input = new OcrInput(image))
            {
                Input.Contrast();
                Input.Invert();
                var Result = Ocr.Read(Input);
                return Result.Text;
            }
        }

        /// <summary>
        /// Crop bitmap image to certain size 
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Bitmap GetCroppedBitmap(Bitmap bitmap, int x, int y, int width, int height)
        {
            Bitmap retBitmap = null;
            using (var currentTile = new Bitmap(width, height))
            {
                using (var currentTileGraphics = Graphics.FromImage(currentTile))
                {
                    currentTileGraphics.Clear(System.Drawing.Color.Black);
                    var absentRectangleArea = new System.Drawing.Rectangle(x, y, width, height);
                    currentTileGraphics.DrawImage(bitmap, 0, 0, absentRectangleArea, GraphicsUnit.Pixel);
                }
                retBitmap = new Bitmap(currentTile);
            }
            return retBitmap;
        }

        /// <summary>
        /// Returns window rectangle or empty rectangle if there no window
        /// </summary>
        /// <param name="winToFindName"></param>
        /// <returns></returns>
        public static Rectangle GetWindowRect(string winToFindName, string windowToFindClass, bool needed)
        {
            lock (scrLock)
            {
                if (!IsTheWindowsExist(windowToFindClass))
                {
                    return Rectangle.Empty;
                }
                if (needed && (AutoItX.WinGetTitle("[ACTIVE]") != winToFindName))
                {
                    AutoItX.WinActivate(windowToFindClass);
                    AutoItX.WinGetTitle();
                }
                return AutoItX.WinGetPos(windowToFindClass);
            }
        }

        /// <summary>
        /// Check is there windows exists 
        /// </summary>
        /// <returns></returns>
        public static bool IsTheWindowsExist(string windowToFindClass)
        {
            lock (scrLock)
            {
                return AutoItX.WinExists(windowToFindClass) == 1;
            }
        }

        /// <summary>
        /// Return certain window screenshot as bitmap or null 
        /// </summary>
        /// <param name="windowToActivateName"></param>
        /// <param name="windowToFindClass"></param>
        /// <param name="screenshotLock"></param>
        /// <returns></returns>
        public static Bitmap GetWinScreenshot(string windowToActivateName, string windowToFindClass)
        {
            lock (scrLock)
            {
                Rectangle currWinRect = GetWindowRect(windowToActivateName, windowToFindClass, true);
                if (currWinRect == Rectangle.Empty)
                {
                    return null;
                }
                Bitmap bitmap = new Bitmap(currWinRect.Width, currWinRect.Height);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(new System.Drawing.Point(currWinRect.Left, currWinRect.Top), System.Drawing.Point.Empty, currWinRect.Size);
                }
                return bitmap;
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        /// <summary>
        /// Return certain window screenshot even if it is not active as bitmap or null
        /// </summary>
        /// <param name="windowToActivateName"></param>
        /// <param name="windowToFindClass"></param>
        /// <param name="screenshotLock"></param>
        /// <returns></returns>
        /// 
        public static Bitmap GetWinScreenshotNotActive(string windowToActivateName, string windowToFindClass)
        {
            lock (scrLock)
            {
                Rectangle currWinRect = GetWindowRect(windowToActivateName, windowToFindClass, false);
                if (currWinRect == Rectangle.Empty)
                {
                    return null;
                }
                Bitmap B = new Bitmap(currWinRect.Width, currWinRect.Height);
                using (Graphics graphics = Graphics.FromImage(B))
                {
                    Bitmap bmp = new Bitmap(currWinRect.Size.Width, currWinRect.Size.Height, graphics);
                    Graphics memoryGraphics = Graphics.FromImage(bmp);
                    IntPtr dc = memoryGraphics.GetHdc();
                    var handle = AutoItX.WinGetHandle(windowToFindClass);
                    bool success = PrintWindow(handle, dc, 0);
                    memoryGraphics.ReleaseHdc(dc);
                    return bmp;
                }
            }
        }

        /// <summary>
        /// Gets whole screen screenshot
        /// </summary>
        /// <returns></returns>
        public static Bitmap GetScreenShot()
        {
           
            Rectangle bounds = new Rectangle(0, 0, (int)SystemParameters.VirtualScreenWidth, (int)SystemParameters.VirtualScreenHeight);
            Bitmap Screenshot = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics graphics = Graphics.FromImage(Screenshot))
            {
                graphics.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
            }
            return Screenshot;  
        }

        /// <summary>Finds a matching image on the screen.</summary>
        ///     ''' <param name="bmpMatch">The image to find on the screen.</param>
        ///     ''' <param name="ExactMatch">True finds an exact match (slowerer on large images). False finds a close match (faster on large images).</param>
        ///     ''' <param name="bmpWhereFind">Picture where to find subimage.</param>
        ///     ''' <returns>Returns a Rectangle of the found image in sceen coordinates.</returns>
        public static Rectangle FindImageOnScreen(Bitmap bmpMatch, Bitmap bmpWhereFind, bool ExactMatch)
        {
            BitmapData ImgBmd = bmpMatch.LockBits(new Rectangle(0, 0, bmpMatch.Width, bmpMatch.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            BitmapData ScreenBmd = bmpWhereFind.LockBits(new Rectangle(0, 0, bmpWhereFind.Width, bmpWhereFind.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

            byte[] ImgByts = new byte[(Math.Abs(ImgBmd.Stride) * bmpMatch.Height) - 1 + 1];
            byte[] ScreenByts = new byte[(Math.Abs(ScreenBmd.Stride) * bmpWhereFind.Height) - 1 + 1];

            Marshal.Copy(ImgBmd.Scan0, ImgByts, 0, ImgByts.Length);
            Marshal.Copy(ScreenBmd.Scan0, ScreenByts, 0, ScreenByts.Length);

            bool FoundMatch = false;
            Rectangle rct = Rectangle.Empty;
            int sindx, iindx;
            int spc, ipc;
            int skpx = Convert.ToInt32((bmpMatch.Width - 1) / (double)10);
            if (skpx < 1 | ExactMatch)
                skpx = 1;
            int skpy = Convert.ToInt32((bmpMatch.Height - 1) / (double)10);
            if (skpy < 1 | ExactMatch)
                skpy = 1;
            for (int si = 0; si <= ScreenByts.Length - 1; si += 3)
            {
                FoundMatch = true;
                for (int iy = 0; iy <= ImgBmd.Height - 1; iy += skpy)
                {
                    for (int ix = 0; ix <= ImgBmd.Width - 1; ix += skpx)
                    {
                        sindx = (iy * ScreenBmd.Stride) + (ix * 3) + si;
                        iindx = (iy * ImgBmd.Stride) + (ix * 3);
                        spc = Color.FromArgb(ScreenByts[sindx + 2], ScreenByts[sindx + 1], ScreenByts[sindx]).ToArgb();
                        ipc = Color.FromArgb(ImgByts[iindx + 2], ImgByts[iindx + 1], ImgByts[iindx]).ToArgb();
                        if (spc != ipc)
                        {
                            FoundMatch = false;
                            iy = ImgBmd.Height - 1;
                            ix = ImgBmd.Width - 1;
                        }
                    }
                }
                if (FoundMatch)
                {
                    double r = si / (double)(bmpWhereFind.Width * 3);
                    double c = bmpWhereFind.Width * (r % 1);
                    if (r % 1 >= 0.5)
                        r -= 1;
                    rct.X = Convert.ToInt32(c);
                    rct.Y = Convert.ToInt32(r);
                    rct.Width = bmpMatch.Width;
                    rct.Height = bmpMatch.Height;
                    break;
                }
            }
            bmpMatch.UnlockBits(ImgBmd);
            bmpWhereFind.UnlockBits(ScreenBmd);
            return rct;
        }

        #endregion
    }
}
