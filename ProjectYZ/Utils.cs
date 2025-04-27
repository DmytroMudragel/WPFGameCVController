using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;

namespace ProjectYZ
{
    class Utils
    {
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

        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CURSORINFO
        {
            public int cbSize;
            public int flags;
            public IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINTAPI
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

        public const Int32 CURSOR_SHOWING = 0x0001;
        public const Int32 DI_NORMAL = 0x0003;

        public static void ReConnect()
        {
            WowProcess.Delay(5000);
            WowProcess.PressButton("ENTER");
            WowProcess.Delay(3000);
            WowProcess.PressButton("ENTER");
            WowProcess.Delay(15000);
            WowProcess.PressButton("ENTER");
            WowProcess.Delay(25000);
        }

        public static void UIDisabler()
        {
            WowProcess.Delay(1000);
            WowProcess.EnterText("/run PlayerFrame:Hide();  MinimapCluster:Hide(); MultiBarBottomLeft:Hide();   MultiBarBottomRight:Hide();   ChatFrame1:Hide();   MainMenuExpBar:Hide(); MainMenuBarRightEndCap:Hide(); MainMenuBarLeftEndCap:Hide(); MultiBarRight:Hide();");
            WowProcess.Delay(1000);
            WowProcess.EnterText("/run _CHATHIDE=not _CHATHIDE for i=1,NUM_CHAT_WINDOWS do for _,v in pairs{\"\",\"Tab\"}do local f=_G[\"ChatFrame\"..i..v] f:Hide() end end");
            WowProcess.Delay(1000);
        }

        public static string GetPublicIP()
        {
            using (WebClient client = new WebClient())
            {

                try
                {
                    return client.DownloadString("http://wtfismyip.com/text");
                }
                catch (WebException)
                {
                    try
                    {
                        return client.DownloadString("https://api.ipify.org/");
                    }
                    catch (WebException)
                    {
                        return "";// offline ...
                    } 
                }
            }
        }
    }

    public class EmulateMouseMove
    {

        static readonly Random random = new Random();

        public static void EmulateMoveMouse(int x, int y, int Speed = 4)
        {
            GetCursorPos(out Point c);
            WindMouse(c.X, c.Y, x, y, 9.0, 3.0, 5.0 / Speed, 7.5 / Speed, 5.0 * Speed, 10f * Speed);
        }

        static void WindMouse(double xs, double ys, double xe, double ye,
            double gravity, double wind, double minWait, double maxWait,
            double maxStep, double targetArea)
        {

            double dist, windX = 0, windY = 0, veloX = 0, veloY = 0, randomDist, veloMag;
            int oldX, oldY, newX = (int)Math.Round(xs), newY = (int)Math.Round(ys);

            double sqrt2 = Math.Sqrt(2.0);
            double sqrt3 = Math.Sqrt(3.0);
            double sqrt5 = Math.Sqrt(5.0);

            dist = Hypot(xe - xs, ye - ys);

            while (dist > 1.0)
            {

                wind = Math.Min(wind, dist);

                if (dist >= targetArea)
                {
                    int w = random.Next((int)Math.Round(wind) * 2 + 1);
                    windX = windX / sqrt3 + (w - wind) / sqrt5;
                    windY = windY / sqrt3 + (w - wind) / sqrt5;
                }
                else
                {
                    windX /= sqrt2;
                    windY /= sqrt2;
                    if (maxStep < 3)
                        maxStep = random.Next(3) + 3.0;
                    else
                        maxStep /= sqrt5;
                }

                veloX += windX;
                veloY += windY;
                veloX += gravity * (xe - xs) / dist;
                veloY += gravity * (ye - ys) / dist;

                if (Hypot(veloX, veloY) > maxStep)
                {
                    randomDist = maxStep / 2.0 + random.Next((int)Math.Round(maxStep) / 2);
                    veloMag = Hypot(veloX, veloY);
                    veloX = veloX / veloMag * randomDist;
                    veloY = veloY / veloMag * randomDist;
                }

                oldX = (int)Math.Round(xs);
                oldY = (int)Math.Round(ys);
                xs += veloX;
                ys += veloY;
                dist = Hypot(xe - xs, ye - ys);
                newX = (int)Math.Round(xs);
                newY = (int)Math.Round(ys);

                if (oldX != newX || oldY != newY)
                    SetCursorPos(newX, newY);

                int wait = (int)Math.Round(/*waitDiff  (step / maxStep)*/ + minWait);
                Thread.Sleep(wait);
            }

            int endX = (int)Math.Round(xe);
            int endY = (int)Math.Round(ye);
            if (endX != newX || endY != newY)
                SetCursorPos(endX, endY);
        }

        static double Hypot(double dx, double dy)
        {
            return Math.Sqrt(dx * dx + dy * dy);
        }

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point p);
    }

    public class ValidateObjectAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(value, null, null);

            Validator.TryValidateObject(value, context, results, true);

            if (results.Count != 0)
            {
                var compositeResults = new CompositeValidationResult(string.Format("Validation for {0} failed!", validationContext.DisplayName));
                results.ForEach(compositeResults.AddResult);

                return compositeResults;
            }

            return ValidationResult.Success;
        }

        public static void PrintResults(string fileName, IEnumerable<ValidationResult> results)
        {
            foreach (var validationResult in results)
            {

                Debug.AddDebugRecord(fileName + " : " + validationResult.ErrorMessage, true);

                if (validationResult is CompositeValidationResult)
                {
                    PrintResults(fileName, ((CompositeValidationResult)validationResult).Results);
                }
            }
        }
    }

    public class CompositeValidationResult : ValidationResult
    {
        private readonly List<ValidationResult> _results = new List<ValidationResult>();

        public IEnumerable<ValidationResult> Results
        {
            get
            {
                return _results;
            }
        }

        public CompositeValidationResult(string errorMessage) : base(errorMessage) { }
        public CompositeValidationResult(string errorMessage, IEnumerable<string> memberNames) : base(errorMessage, memberNames) { }
        protected CompositeValidationResult(ValidationResult validationResult) : base(validationResult) { }

        public void AddResult(ValidationResult validationResult)
        {
            _results.Add(validationResult);
        }
    }
}
