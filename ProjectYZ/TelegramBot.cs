using System;
using AutoIt;
using System.Drawing;
using Telegram.Bot;
using System.IO;
using Telegram.Bot.Types.InputFiles;
using System.Windows;
using System.Threading;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace ProjectYZ
{
    public class TelegramBot
    {
        public static bool BotСondition = false;
        public static bool SessionIsRunning = true;
        public static bool IsBotStacked = false;
        public static bool IsReconectWasSuccessful = true;
        public static int localSessionCount = 0;
        public static int CombatCount = 0;
        public static int LootCount = 0;
        public static int GatherCount = 0;
        public static int DeathCount = 0;

        private const string GetWowScreenButton = "Get Wow screenshot";
        private const string GetScreenButton = "Get screenshot";
        private const string StartWowBotButton = "Start Wow Bot";
        private const string StopWowBotButton = "Stop Wow Bot";
        private const string SendTextToWowButton = "Send message";
        private const string RunWowButton = "Reconect";
        private const string StatsButton = "Get info";
        private const string TurnWowBotButton = "Turn";
        private const string ExitButton = "Back";
        private const string SureCloseWowButton = "Close Wow/BattleNet";
        private const string GetIpButton = "Get IP";
        private const string OpenWowGameButton = "Open Wow";
        private const string CloseWowGameButton = "Close Wow";

        public static ITelegramBotClient botClient;
        public static Stopwatch stopwatch = new Stopwatch();
        public static Stopwatch stopwatchForTheSessions = new Stopwatch();
        internal enum State { None, InWhisper, Turn, Closewow }
        internal class UserState { public object State { get; set; } }
        private static Dictionary<long, UserState> ClientStates = new Dictionary<long, UserState>();

        [Obsolete]
        public static void Init()
        {
            try
            {
                botClient = new TelegramBotClient(ConfigSettings.Cfg.Commonsettings.Telegram.ApiKey) { Timeout = TimeSpan.FromSeconds(10) };
                var Me = botClient.GetMeAsync().Result;
                if (Me != null && !string.IsNullOrEmpty(Me.FirstName))
                {
                    Debug.AddDebugRecord("Telegram bot was successfully loaded", true);
                    SendMessage("Bot was successfully loaded", false);
                }
                GetUpdates();
            }
            catch (Exception ex)
            {
                Debug.AddDebugRecord(ex.Message.ToString(), true);
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public static void SendMessage(string text, bool photo = false)
        {
            try
            {
                if (photo)
                {
                    WowProcess.GetWowScreenshot().Save("tg.png");
                    FileStream fs = File.OpenRead("tg.png");
                    InputOnlineFile inputOnlineFile = new InputOnlineFile(fs, ConfigSettings.Cfg.Commonsettings.Bot.PcId + " - " + text + ".png");
                    botClient.SendDocumentAsync(ConfigSettings.Cfg.Commonsettings.Telegram.UserId, inputOnlineFile).Wait();
                }
                else botClient.SendTextMessageAsync(chatId: ConfigSettings.Cfg.Commonsettings.Telegram.UserId, text: ConfigSettings.Cfg.Commonsettings.Bot.PcId + " | " + text, replyMarkup: BotDobuttons());
            }
            catch (Exception e)
            {
                Debug.AddDebugRecord(e.Message.ToString(), false);
            }

        }

        public static void SendMessage(string text, IReplyMarkup buttons, bool photo = false)
        {
            try
            {
                if (photo)
                {
                    WowProcess.GetWowScreenshot().Save("tg.png");
                    FileStream fs = File.OpenRead("tg.png");
                    InputOnlineFile inputOnlineFile = new InputOnlineFile(fs, ConfigSettings.Cfg.Commonsettings.Bot.PcId + " | " + text + ".png");
                    botClient.SendDocumentAsync(ConfigSettings.Cfg.Commonsettings.Telegram.UserId, inputOnlineFile).Wait();
                }
                else botClient.SendTextMessageAsync(chatId: ConfigSettings.Cfg.Commonsettings.Telegram.UserId, text: ConfigSettings.Cfg.Commonsettings.Bot.PcId + " | " + text, replyMarkup: buttons);
            }
            catch (Exception e)
            {
                Debug.AddDebugRecord(e.Message.ToString(), false);
            }

        }

        public static void SendMessage(string text, IReplyMarkup buttons,Exception exeption)
        {
            botClient.SendTextMessageAsync(chatId: ConfigSettings.Cfg.Commonsettings.Telegram.UserId, text: ConfigSettings.Cfg.Commonsettings.Bot.PcId + " | Something happend: " + exeption.Message.ToString() + text, replyMarkup: buttons);
        }

        public static void SendMessage(string text, Exception exeption)
        {
            botClient.SendTextMessageAsync(chatId: ConfigSettings.Cfg.Commonsettings.Telegram.UserId, text: ConfigSettings.Cfg.Commonsettings.Bot.PcId + " | Something happend: " + exeption.Message.ToString() + text, replyMarkup: BotDobuttons());
        }

        public static void SendSesionReport()
        {
            if (Equals(AutoItX.WinExists("World of Warcraft"), 1))
            {
                GameInfo.Refresh();
                TimeSpan timeSpan = stopwatch.Elapsed;
                TimeSpan currentSessionTimeSpan = stopwatchForTheSessions.Elapsed;
                if (Equals(AutoItX.WinExists("World of Warcraft"), 1))
                {
                    SendMessage($"{localSessionCount} Session finished", true);
                }
                else
                {
                    SendMessage($"{localSessionCount} Session finished");
                }
                string summary = "";
                try
                {
                    string ipResult = Utils.GetPublicIP();
                    if (ipResult != "")
                    {
                        summary += $"IP: {ipResult}";
                    }
                }
                catch (Exception exeption)
                {
                    Debug.AddDebugRecord(exeption.Message.ToString(), true);
                    summary += "cannot get IP";
                }
                summary += $"Total running time: " + timeSpan.ToString(@"hh\:mm\:ss") + $"\nBot has finished {localSessionCount} session in: " + currentSessionTimeSpan.ToString(@"hh\:mm\:ss") + $"\nTotal gold: {GameInfo.Gold} \nTotal combats: {CombatCount} \nTotal loots: {LootCount} \nTotal gathers: {GatherCount} \nTotal deaths: {DeathCount}";
                if (GameInfo.Bag) { summary += $"\nBags: Are full"; }
                else { summary += $"\nBags: Aren`t full"; }
                if (GameInfo.Repair) { summary += $"\nRepair: Needed"; }
                else { summary += $"\nRepair: Not needed"; }
                if (GameInfo.Dead) { summary += $"\nState: Dead"; }
                else { summary += $"\nState: Alive"; }
                if (GameInfo.Main) { summary += $"\nConnection: In Game"; }
                else { summary += $"\nConnection: Disconected"; }
                SendMessage(summary);
            }
            else { SendMessage("There is no wow window"); }
        }

        /// <summary>
        /// Closes Wow Bot and sends message to telegram if some unhandled exception appears
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void BotWasClosedDueOfExeption(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.AddDebugRecord((e.ExceptionObject as Exception).Message.ToString(), false);
            if (botClient != null)
            {
                SendMessage("Bot was closed",new ReplyKeyboardRemove(), e.ExceptionObject as Exception);
            }
        }

        /// <summary>
        /// Closes Wow Bot and sends message to telegram if some unhandled exception appears
        /// </summary>
        public static void BotWasClosed()
        {
            if (botClient != null)
            {
                SendMessage("Bot was closed", new ReplyKeyboardRemove());          
            }
        }

        /// <summary>
        /// Yes or No keyboard for telegram
        /// </summary>
        /// <returns></returns>
        private static IReplyMarkup YNbuttons()
        {
            var YNKeyboard = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton> {new KeyboardButton(SureCloseWowButton), new KeyboardButton(ExitButton)}
            };
            return new ReplyKeyboardMarkup(YNKeyboard);
        }

        /// <summary>
        /// Just "Back" button keyboard for telegram
        /// </summary>
        /// <returns></returns>
        private static IReplyMarkup Exitbuttons()
        {
            var ExitKeyboard = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton> {new KeyboardButton(ExitButton)}
            };
            return new ReplyKeyboardMarkup(ExitKeyboard);
        }

        /// <summary>
        /// Main keyboard for telegram
        /// </summary>
        /// <returns></returns>
        private static IReplyMarkup BotDobuttons()
        {
            IEnumerable<List<KeyboardButton>> BotDoKeyboard = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton> {new KeyboardButton(GetScreenButton), new KeyboardButton(GetWowScreenButton), new KeyboardButton(StatsButton), new KeyboardButton(GetIpButton)},
                new List<KeyboardButton> { new KeyboardButton(OpenWowGameButton), new KeyboardButton(StartWowBotButton), new KeyboardButton(SendTextToWowButton), new KeyboardButton(TurnWowBotButton)},
                new List<KeyboardButton> { new KeyboardButton(CloseWowGameButton), new KeyboardButton(StopWowBotButton), new KeyboardButton(RunWowButton)}
            };
            return new ReplyKeyboardMarkup(BotDoKeyboard);
        }

        /// <summary>
        /// Just gets updates from telegram user
        /// </summary>
        [Obsolete]
        public static void GetUpdates()
        {
            int offset = 0;
            while (true)
            {
                try
                {
                    var Updates = botClient.GetUpdatesAsync(offset).Result;
                    if (Updates != null && Updates.Length > 0)
                    {
                        foreach (var update in Updates)
                        {
                            ProcessUpdate(update);
                            offset = update.Id + 1;
                        }
                    }
                }
                catch (Exception ex) { Debug.AddDebugRecord(ex.Message.ToString(), false); }
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Receives and handles all types of a new messages from telegram user
        /// </summary>
        /// <param name="update"></param>
        [Obsolete]
        private static void ProcessUpdate(global::Telegram.Bot.Types.Update update)
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    {
                        var state = ClientStates.ContainsKey(update.Message.Chat.Id) ? ClientStates[update.Message.Chat.Id] : null;
                        string text = update.Message.Text;
                        if (state != null)
                        {
                            switch (state.State)
                            {
                                case State.InWhisper:
                                    {
                                        if (text.Equals(ExitButton))
                                        {
                                            ClientStates[update.Message.Chat.Id] = null;
                                            SendMessage("Сanceled");
                                        }
                                        else
                                        {
                                            string UserWhispMessage = text;
                                            AutoItX.WinActivate("[CLASS:GxWindowClass]", "");
                                            WowProcess.PressButton("ENTER");
                                            WowProcess.Delay(170);
                                            for (int i = 0; i < UserWhispMessage.Length; i++)
                                            {
                                                AutoItX.Send(text[i].ToString());
                                                WowProcess.Delay(70);
                                            }
                                            WowProcess.Delay(140);
                                            WowProcess.PressButton("ENTER");
                                            SendMessage("Re-whisp sended");
                                            ClientStates[update.Message.Chat.Id] = null;
                                        }
                                    }
                                    break;
                                case State.Turn:
                                    {
                                        if (text.Equals(ExitButton))
                                        {
                                            ClientStates[update.Message.Chat.Id] = null;
                                            SendMessage("Сanceled");
                                        }
                                        else
                                        {
                                            string Angle = text;
                                            AutoItX.WinActivate("[CLASS:GxWindowClass]", "");
                                            WowProcess.DownButton(ConfigSettings.Cfg.Commonsettings.TurnRight.Key);
                                            WowProcess.Delay(2000 / 360 * Convert.ToInt32(text));
                                            WowProcess.UnButton(ConfigSettings.Cfg.Commonsettings.TurnRight.Key);
                                            SendMessage("Turned");
                                            ClientStates[update.Message.Chat.Id] = null;
                                        }
                                    }
                                    break;
                                case State.Closewow:
                                    {
                                        if (text.Equals(ExitButton))
                                        {
                                            ClientStates[update.Message.Chat.Id] = null;
                                            SendMessage("Сanceled");
                                        }
                                        if (text.Equals(SureCloseWowButton))
                                        {
                                            if (Equals(AutoItX.WinExists("World of Warcraft"), 1))
                                            {
                                                AutoItX.WinActivate("[CLASS:GxWindowClass]", "");
                                                Process[] wowProcess = Process.GetProcessesByName("WowClassic");
                                                if (Equals(AutoItX.ProcessClose(wowProcess[0].Id.ToString()), 1) && !Equals(AutoItX.WinExists("World of Warcraft"), 1))
                                                {
                                                    SendMessage("Wow was successfully closed");
                                                }
                                                else
                                                {
                                                    AutoItX.WinActivate("[CLASS:GxWindowClass]", "");
                                                    int WowXPos = AutoItX.WinGetPos("World of Warcraft").X;
                                                    int WowYPos = AutoItX.WinGetPos("World of Warcraft").Y;
                                                    WowProcess.Delay(700);
                                                    EmulateMouseMove.EmulateMoveMouse(WowXPos + 1580, WowYPos + 20, 30);
                                                    WowProcess.Delay(70);
                                                    AutoItX.MouseClick("LEFT");
                                                    WowProcess.Delay(700);
                                                    if (!Equals(AutoItX.WinExists("World of Warcraft"), 1))
                                                    {
                                                        SendMessage("Wow was successfully closed");
                                                    } else
                                                    {
                                                        SendMessage("Some error, fail to close Wow");
                                                    }
                                                }
                                            }


                                            if (Equals(AutoItX.WinExists("Battle.net"), 1))
                                            {
                                                AutoItX.WinActivate("[CLASS:Chrome_WidgetWin_0]", "");
                                                int BattleNetXPos = AutoItX.WinGetPos("Battle.net").X;
                                                int BattleNetYPos = AutoItX.WinGetPos("Battle.net").Y;
                                                WowProcess.Delay(700);
                                                EmulateMouseMove.EmulateMoveMouse(BattleNetXPos + 980, BattleNetYPos + 15, 30);
                                                WowProcess.Delay(70);
                                                AutoItX.MouseClick("LEFT");
                                                WowProcess.Delay(70);
                                                AutoItX.MouseClick("LEFT");
                                                WowProcess.Delay(1000);
                                                if (!Equals(AutoItX.WinExists("Battle.net"), 1))
                                                {
                                                    SendMessage("BattleNet was successfully closed");
                                                }
                                                else
                                                {
                                                    AutoItX.WinActivate("[CLASS:Chrome_WidgetWin_0]", "");
                                                    Process[] BattleNetProcess = Process.GetProcessesByName("Battle.net");
                                                    if (Equals(AutoItX.ProcessClose(BattleNetProcess[0].Id.ToString()), 1) && !Equals(AutoItX.WinExists("Battle.net"), 1))
                                                    {
                                                        SendMessage("BattleNet was successfully closed");
                                                    }
                                                    else
                                                    {
                                                        SendMessage("Some error, fail to close BattleNet");
                                                    }
                                                }
                                            }
                                            GlobalBotHandler.BotStop = true;
                                            ClientStates[update.Message.Chat.Id] = null;
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (text)
                            {
                                case GetScreenButton:
                                    {
                                        try
                                        {
                                            Rectangle bounds = new Rectangle(0, 0, (int)SystemParameters.VirtualScreenWidth, (int)SystemParameters.VirtualScreenHeight);
                                            Bitmap Screenshot = new Bitmap(bounds.Width, bounds.Height);
                                            using (Graphics graphics = Graphics.FromImage(Screenshot))
                                            {
                                                graphics.CopyFromScreen(new System.Drawing.Point(bounds.Left, bounds.Top), System.Drawing.Point.Empty, bounds.Size);
                                            }
                                            Screenshot.Save("tg.png");
                                            InputOnlineFile inputOnlineFile = new InputOnlineFile(File.OpenRead("tg.png"), ConfigSettings.Cfg.Commonsettings.Bot.PcId + " Screenshot.png");
                                            botClient.SendDocumentAsync(ConfigSettings.Cfg.Commonsettings.Telegram.UserId, inputOnlineFile).Wait();
                                            Screenshot.Dispose();
                                        }
                                        catch (Exception exeption)
                                        {
                                            Debug.AddDebugRecord(exeption.Message.ToString(), true);
                                            SendMessage("cannot take screen",exeption);
                                        }
                                    }
                                    break;
                                case GetWowScreenButton:
                                    {
                                        try
                                        {
                                            Bitmap WowScreenshot = WowProcess.GetWowScreenshot();
                                            if (WowScreenshot == null)
                                            { SendMessage("There is no wow window", false); }
                                            else
                                            {
                                                WowScreenshot.Save("tg.png");
                                                InputOnlineFile inputOnlineFile = new InputOnlineFile(File.OpenRead("tg.png"), ConfigSettings.Cfg.Commonsettings.Bot.PcId + " Wow screenshot.png");
                                                botClient.SendDocumentAsync(ConfigSettings.Cfg.Commonsettings.Telegram.UserId, inputOnlineFile).Wait();
                                                WowScreenshot.Dispose();
                                            }
                                        }
                                        catch (Exception exeption)
                                        {
                                            Debug.AddDebugRecord(exeption.Message.ToString(), true);
                                            SendMessage("cannot take wow screen",exeption);
                                        }
                                    }
                                    break;
                                case StatsButton:
                                    {
                                        if (Equals(AutoItX.WinExists("World of Warcraft"), 1))
                                        {
                                            GameInfo.Refresh();
                                            TimeSpan timeSpan = stopwatch.Elapsed;
                                            TimeSpan currentSessionTimeSpan = stopwatchForTheSessions.Elapsed;
                                            string summary = "";
                                            try
                                            {
                                                string ipResult = Utils.GetPublicIP();
                                                if (ipResult != "")
                                                {
                                                    summary += $"IP: {ipResult}";
                                                }
                                            }
                                            catch (Exception exeption)
                                            {
                                                Debug.AddDebugRecord(exeption.Message.ToString(), true);
                                                summary += "cannot get IP";
                                            }
                                            summary += $"Total Time: " + timeSpan.ToString(@"hh\:mm\:ss") + "\nCurrent session: " + currentSessionTimeSpan.ToString(@"hh\:mm\:ss") +  $"\nGold: {GameInfo.Gold} \nCombats: {CombatCount} \nLoots: {LootCount} \nGathers: {GatherCount} \nDeaths: {DeathCount}";
                                            if (GameInfo.Bag) { summary += $"\nBags: Are full"; }
                                            else { summary += $"\nBags: Aren`t full"; }
                                            if (GameInfo.Repair) { summary += $"\nRepair: Needed"; }
                                            else { summary += $"\nRepair: Not needed"; }
                                            if (GameInfo.Dead) { summary += $"\nState: Dead"; }
                                            else { summary += $"\nState: Alive"; }
                                            if (GameInfo.Main) { summary += $"\nConnection: In Game"; }
                                            else { summary += $"\nConnection: Disconected"; }
                                            SendMessage(summary, false);
                                        }
                                        else { SendMessage("There is no wow window"); }
                                    }
                                    break;
                                case GetIpButton:
                                    {
                                        try
                                        {
                                            string ipResult = Utils.GetPublicIP();
                                            if (ipResult != "")
                                            {
                                                SendMessage($"Current IP: {ipResult}");
                                            }
                                        }
                                        catch (Exception exeption)
                                        {
                                            Debug.AddDebugRecord(exeption.Message.ToString(), true);
                                            SendMessage("cannot get IP", exeption);
                                        }
                                    }
                                    break;
                                case OpenWowGameButton:
                                    {
                                        try
                                        {
                                            if (WowProcess.OpenWow() == 1)
                                            {
                                                if (WowProcess.PrepareForStartMainWowBotLoop())
                                                {
                                                    SendMessage("Wow was successfully opened, character is in game");
                                                }
                                                else
                                                {
                                                    SendMessage("Failed to enter the game world");
                                                }
                                            }
                                            else
                                            {
                                                SendMessage("Failed to open Wow");
                                            }
                                        }
                                        catch (Exception exeption)
                                        {
                                            Debug.AddDebugRecord(exeption.Message.ToString(), true);
                                            SendMessage("cannot open Wow", exeption);
                                        }
                                    }
                                    break;
                                case CloseWowGameButton:
                                    {
                                        if (Equals(AutoItX.WinExists("World of Warcraft"), 1) || Equals(AutoItX.WinExists("Battle.net"), 1))
                                        {
                                            ClientStates[update.Message.Chat.Id] = new UserState { State = State.Closewow };
                                            SendMessage("Close Wow/BattleNet?", YNbuttons());
                                        }
                                        else { SendMessage("There is no wow window"); }
                                    }
                                    break;
                                case SendTextToWowButton:
                                    {
                                        if (Equals(AutoItX.WinExists("World of Warcraft"), 1))
                                        {
                                            ClientStates[update.Message.Chat.Id] = new UserState { State = State.InWhisper };
                                            SendMessage("Re - Whisper:", Exitbuttons());
                                        }
                                        else { SendMessage("There is no wow window"); }
                                    }
                                    break;
                                case StartWowBotButton:
                                    {
                                        //if (Equals(AutoItX.WinExists("World of Warcraft"), 1))
                                        //{
                                            if (BotСondition == true)
                                            { SendMessage("Wow bot is already running", false); }
                                            else
                                            { BotСondition = true; }
                                       // }
                                        //else {
                                        //    SendMessage("There is no wow window", false); }
                                    }
                                    break;
                                case StopWowBotButton:
                                    {
                                        if (Equals(AutoItX.WinExists("World of Warcraft"), 1))
                                        {
                                            if (BotСondition == false)
                                            { SendMessage("Wow bot has already been stopped", false); }
                                            else
                                            { BotСondition = false; }
                                        }
                                        else { SendMessage("There is no wow window", false); }
                                    }
                                    break;
                                case TurnWowBotButton:
                                    {
                                        if (Equals(AutoItX.WinExists("World of Warcraft"), 1))
                                        {
                                            ClientStates[update.Message.Chat.Id] = new UserState { State = State.Turn };
                                            botClient.SendTextMessageAsync(chatId: ConfigSettings.Cfg.Commonsettings.Telegram.UserId, text: "Turn angle:", replyMarkup: Exitbuttons());
                                        }
                                        else { SendMessage("There is no wow window", false); }
                                    }
                                    break;
                                case RunWowButton:
                                    {
                                        if (Equals(AutoItX.WinExists("World of Warcraft"), 1))
                                        {
                                            AutoItX.WinActivate("[CLASS:GxWindowClass]", "");
                                            Utils.ReConnect();
                                            if (GameInfo.Main) SendMessage("Bot сonnected back", false);
                                            else SendMessage("Fail to reconnect!", false);
                                        }
                                        else { SendMessage("There is no wow window", false); }
                                    }
                                    break;
                                default:
                                    SendMessage("Choose another action");
                                    break;
                            }
                            break;
                        }
                    }
                    break;
                default:
                    SendMessage("This type of message is not supported");
                    break;
            }
        }
    }
}

