using Microsoft.Win32;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Telegram.Bot;
using AutoIt;

namespace ProjectYZ
{
    public partial class MainWindow : Window
    {

        #region Hotkey

        [DllImport("User32.dll")]
        private static extern bool RegisterHotKey([In] IntPtr hWnd, [In] int id, [In] uint fsModifiers, [In] uint vk);

        [DllImport("User32.dll")]
        private static extern bool UnregisterHotKey([In] IntPtr hWnd, [In] int id);

        private HwndSource _source;
        private const int HOTKEY_ID = 9000;

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var helper = new WindowInteropHelper(this);
            _source = HwndSource.FromHwnd(helper.Handle);
            _source.AddHook(HwndHook);
            RegisterHotKey();
        }

        protected override void OnClosed(EventArgs e)
        {
            _source.RemoveHook(HwndHook);
            _source = null;
            UnregisterHotKey();
            base.OnClosed(e);
        }

        private void RegisterHotKey()
        {
            var helper = new WindowInteropHelper(this);
            const uint VK_F10 = 0x79;
            const uint MOD_CTRL = 0x0002;
            if (!RegisterHotKey(helper.Handle, HOTKEY_ID, MOD_CTRL, VK_F10))
            {
                // handle error
            }
        }

        private void UnregisterHotKey()
        {
            var helper = new WindowInteropHelper(this);
            UnregisterHotKey(helper.Handle, HOTKEY_ID);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case HOTKEY_ID:
                            OnHotKeyPressed();
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void OnHotKeyPressed()
        {
            TelegramBot.BotСondition = false;
        }

        #endregion

        #region Main UI
        [Obsolete]
        public MainWindow()
        {
            Debug.ClearLog();
            ConfigSettings.ConfigInitDefault();
            if (!ConfigSettings.ConfigReader())
            {
                MessageBox.Show("Error while read config, fix it, and reload bot pls");
                return;
            }
            InitializeComponent();
            Title = GenerateName(8);
            Console.SetWindowSize(32, 26);
            Console.Title = GenerateName(8);
            Console.ForegroundColor = ConsoleColor.Yellow;
            GlobalBotHandler.CurrentProfilePath = ConfigSettings.Cfg.Commonsettings.LastProfile.Path;
            GlobalBotHandler.CurrentRoutePath = ConfigSettings.Cfg.Commonsettings.LastNav.Path;
            CurrentCombatProfileTextBox.Text = ConfigSettings.Cfg.Commonsettings.LastProfile.Path.Split('\\').Last();
            CurrentRouteProfileTextBox.Text = ConfigSettings.Cfg.Commonsettings.LastNav.Path.Split('\\').Last();
            Thread telegramThread = new Thread(new ThreadStart(TelegramBot.Init));
            telegramThread.Start();
            Thread startPausebuttonThread = new Thread(new ThreadStart(ButtonHandler));
            startPausebuttonThread.Start();
            Console.WriteLine(ConfigSettings.Cfg.Commonsettings.LastProfile.Path.Split('\\').Last());
            Console.WriteLine(ConfigSettings.Cfg.Commonsettings.LastNav.Path.Split('\\').Last());
            Closed += new EventHandler(MainWindow_Closed);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(TelegramBot.BotWasClosedDueOfExeption);
        }

        [STAThread]
        private void ButtonHandler()
        {
            bool prev = TelegramBot.BotСondition;
            int sessionTimeMinutes = WowProcess.GameSessionMinutesTime(ConfigSettings.Cfg.Commonsettings.OneSessionTime.oneSessionTime);
            while (true)
            {
                TimeSpan currentSessionTimeSpan = TelegramBot.stopwatchForTheSessions.Elapsed;
                if ((currentSessionTimeSpan.Days * 24 * 60 + currentSessionTimeSpan.Hours * 60 + currentSessionTimeSpan.Minutes == sessionTimeMinutes && sessionTimeMinutes != 0) || TelegramBot.IsBotStacked || !TelegramBot.IsReconectWasSuccessful)
                {
                    Task.Run(() => Application.Current.Dispatcher.Invoke(new Action(() => { textNotifierbox.Text = $"Finishing {TelegramBot.localSessionCount} session..."; })));
                    TelegramBot.SessionIsRunning = false;
                    TelegramBot.BotСondition = false;
                    //TelegramBot.SendMessage($"Bot has finished session in { currentSessionTimeSpan.ToString("hh\\:mm\\:ss")}");
                    //TelegramBot.stopwatchForTheSessions.Reset();
                    //sessionTimeMinutes = WowProcess.GameSessionMinutesTime(ConfigSettings.Cfg.Commonsettings.OneSessionTime.oneSessionTime);
                }

                // Start bot "handler"
                if (TelegramBot.BotСondition && TelegramBot.BotСondition != prev)
                {
                    if (!File.Exists(GlobalBotHandler.CurrentProfilePath) || !File.Exists(GlobalBotHandler.CurrentRoutePath))
                    {
                        MessageBox.Show("Route or profile file not exist, fix it, and reload bot pls");
                        return;
                    }
                    ProfileSettings.ProfileLocation = GlobalBotHandler.CurrentProfilePath;
                    MeshHandler.MeshPath = GlobalBotHandler.CurrentRoutePath;
                    if (!ProfileSettings.ConfigReader() & !MeshHandler.ReadMesh())
                    {
                        MessageBox.Show("Error while reading files, fix it, and reload bot pls");
                        return;
                    }
                    Task.Run(() => Application.Current.Dispatcher.Invoke(new Action(() => { Runbutton.Content = "Pause"; })));
                    Task.Run(() => Application.Current.Dispatcher.Invoke(new Action(() => { textNotifierbox.Text = "Bot is about to run..."; })));
                    TelegramBot.botClient.SendTextMessageAsync(chatId: ConfigSettings.Cfg.Commonsettings.Telegram.UserId, text: ConfigSettings.Cfg.Commonsettings.Bot.PcId + " | Wow Bot starts ");
                    TelegramBot.SessionIsRunning = true;
                    Thread myThread = new Thread(new ThreadStart(GlobalBotHandler.RunBot));
                    myThread.Start();
                    Console.WriteLine("Profiles was successfully loaded");
                    SaveProfileSettings();
                    prev = TelegramBot.BotСondition;
                }

                // Stop bot "handler"
                if (!TelegramBot.BotСondition && TelegramBot.BotСondition != prev)
                {
                    int localAttemptCount = 0;
                    GlobalBotHandler.BotStop = true;
                    Task.Run(() => Application.Current.Dispatcher.Invoke(new Action(() => { textNotifierbox.Text = "Begin stoping process..."; })));
                    if (TelegramBot.IsReconectWasSuccessful)
                    {
                        while (Navigation.NavEnded == false && localAttemptCount <= 50)
                        {
                            WowProcess.Delay(3000);
                            localAttemptCount++;
                        }
                    }
                    TelegramBot.stopwatchForTheSessions.Stop();/////////////////
                    Task.Run(() => Application.Current.Dispatcher.Invoke(new Action(() => { Runbutton.Content = "Run"; })));
                    Task.Run(() => Application.Current.Dispatcher.Invoke(new Action(() => { textNotifierbox.Text = "Bot stopped"; })));
                    prev = TelegramBot.BotСondition;
                    TelegramBot.botClient.SendTextMessageAsync(chatId: ConfigSettings.Cfg.Commonsettings.Telegram.UserId, text: ConfigSettings.Cfg.Commonsettings.Bot.PcId + " | Wow Bot Stoped");
                } 

                #region Panel + telegram data update 

                Task.Run(() => Application.Current.Dispatcher.Invoke(new Action(() => { Combatcounter.Content = TelegramBot.CombatCount; })));
                Task.Run(() => Application.Current.Dispatcher.Invoke(new Action(() => { Lootcounter.Content = TelegramBot.LootCount; })));
                Task.Run(() => Application.Current.Dispatcher.Invoke(new Action(() => { Deathcounter.Content = TelegramBot.DeathCount; })));
                Task.Run(() => Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    TimeSpan timeSpan = TelegramBot.stopwatch.Elapsed;
                    SessionTime.Content = timeSpan.ToString(@"hh\:mm\:ss");
                })));

                Task.Run(() => Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    TimeSpan currTimeSpan = TelegramBot.stopwatchForTheSessions.Elapsed;
                    if (TelegramBot.stopwatchForTheSessions.IsRunning && currTimeSpan != null)
                    {
                        textNotifierbox.Text = $"Running {TelegramBot.localSessionCount} session";
                    }
                })));

                #endregion

                //todo Check sessions
                WowProcess.Delay(250);
                if (TelegramBot.SessionIsRunning == false)
                {
                    if (TelegramBot.IsReconectWasSuccessful == false)
                    {
                        WowProcess.Delay(1000);
                        TelegramBot.SendSesionReport();
                        TelegramBot.stopwatchForTheSessions.Reset();
                        sessionTimeMinutes = WowProcess.GameSessionMinutesTime(ConfigSettings.Cfg.Commonsettings.OneSessionTime.oneSessionTime);
                        if (WowProcess.CloseWowAndBattleNet() == 1)
                        {
                            TelegramBot.SendMessage("Wow/battlenet was closed");
                        }
                        else
                        {
                            WowProcess.Delay(1000);
                            if (WowProcess.CloseWowAndBattleNet() == 1)
                            {
                                TelegramBot.SendMessage("Wow/battlenet was closed");
                            }
                        }
                        WowProcess.Delay(1000);
                        Task.Run(() => Application.Current.Dispatcher.Invoke(new Action(() => { textNotifierbox.Text = $"Waiting {ConfigSettings.Cfg.Commonsettings.betweenSessionTime.betweenSessionTime} min..."; })));
                        TelegramBot.SendMessage($"Waiting approximately {ConfigSettings.Cfg.Commonsettings.betweenSessionTime.betweenSessionTime} min before next session");
                        WowProcess.Delay(ConfigSettings.Cfg.Commonsettings.betweenSessionTime.betweenSessionTime * 60000);
                        TelegramBot.IsReconectWasSuccessful = true;
                        TelegramBot.BotСondition = true;

                    }
                    else
                    {
                        int localAttemptCount = 0;
                        while (Navigation.NavEnded == false && localAttemptCount <= 50)
                        {
                            WowProcess.Delay(3000);
                            localAttemptCount++;
                        }
                        WowProcess.PressButton(ConfigSettings.Cfg.Commonsettings.StopAutoMovement.Key);
                        WowProcess.Delay(2000);
                        TelegramBot.SendSesionReport();
                        TelegramBot.stopwatchForTheSessions.Reset();
                        sessionTimeMinutes = WowProcess.GameSessionMinutesTime(ConfigSettings.Cfg.Commonsettings.OneSessionTime.oneSessionTime);
                        if (Navigation.NavEnded == true && WowProcess.CloseWowAndBattleNet() == 1)
                        {
                            TelegramBot.SendMessage("Wow/battlenet was closed, navigation ended");
                        }
                        else
                        {
                            if (WowProcess.CloseWowAndBattleNet() == 1)
                            {
                                TelegramBot.SendMessage("Wow/battlenet was closed");
                            }
                        }
                        WowProcess.Delay(1000);
                        Task.Run(() => Application.Current.Dispatcher.Invoke(new Action(() => { textNotifierbox.Text = $"Waiting {ConfigSettings.Cfg.Commonsettings.betweenSessionTime.betweenSessionTime} min..."; })));
                        TelegramBot.SendMessage($"Waiting approximately {ConfigSettings.Cfg.Commonsettings.betweenSessionTime.betweenSessionTime} min before next session");
                        WowProcess.Delay(ConfigSettings.Cfg.Commonsettings.betweenSessionTime.betweenSessionTime * 60000);
                        if (!TelegramBot.IsBotStacked)
                        {
                            TelegramBot.BotСondition = true;
                        }
                        else
                        {
                            TelegramBot.SendMessage("Bot is still stacked, can`t run a new session, close the bot");
                            Application.Current.Shutdown();
                            ///ToDo tp to the heartstone place
                        }
                    }
                }
                //

                WowProcess.Delay(250);
            }
        }

        public static void SaveProfileSettings()
        {
            ConfigSettings.Cfg.Commonsettings.LastProfile.Path = GlobalBotHandler.CurrentProfilePath;
            ConfigSettings.Cfg.Commonsettings.LastNav.Path = GlobalBotHandler.CurrentRoutePath;
            ConfigSettings.ConfigWriter();
        }

        private static void MainWindow_Closed(object sender, EventArgs e)
        {
            GameInfo.Refresh();
            TimeSpan timeSpan = TelegramBot.stopwatch.Elapsed;
            string summary = "";
            try
            {
                string ipResult = Utils.GetPublicIP();
                if (ipResult != "")
                {
                    summary += $"IP:{ipResult}\n";
                }
            }
            catch (Exception exeption)
            {
                Debug.AddDebugRecord(exeption.Message.ToString(), true);
                summary += "cannot get IP\n";
            }
            summary += $"Time: " + timeSpan.ToString(@"hh\:mm\:ss") + $"\n Gold: {GameInfo.Gold} \n Combats: {TelegramBot.CombatCount} \n Loots: {TelegramBot.LootCount} \n Gathers: {TelegramBot.GatherCount} \n Deaths: {TelegramBot.DeathCount}";
            if (GameInfo.Bag) { summary += $"\n Bags: Are full"; }
            else { summary += $"\n Bags: Aren`t full"; }
            if (GameInfo.Repair) { summary += $"\n Repair: Needed"; }
            else { summary += $"\n Repair: Not needed"; }
            if (GameInfo.Dead) { summary += $"\n State: Dead"; }
            else { summary += $"\n State: Alive"; }
            if (GameInfo.Main) { summary += $"\n Connection: In Game"; }
            else { summary += $"\n Connection: Disconected"; }
            TelegramBot.SendMessage(summary, false);
            TelegramBot.botClient = null;
            TelegramBot.BotWasClosed();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Debug.AddDebugRecord((e.ExceptionObject as Exception).Message.ToString(), false);
        }

        private static string GenerateName(int len)
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

        private void MenuItemMain_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            Routes.Visibility = Visibility.Hidden;
        }

        private void MenuItemRoutes_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Hidden;
            Routes.Visibility = Visibility.Visible;
        }

        private void MenuItemLog_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Hidden;
            Routes.Visibility = Visibility.Hidden;
        }

        private void ScreenShot_Click(object sender, RoutedEventArgs e)
        {
            WowProcess.GetWowScreenshot().Save("TestScr.png");
        }

        private void CurrentCombatProfileTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Please select a combat rotation file",
                Filter = "Xml files (*.xml)|*.xml",
                InitialDirectory = Directory.GetCurrentDirectory()
            };
            if (openFileDialog.ShowDialog() == true)
            {
                CurrentCombatProfileTextBox.Text = openFileDialog.SafeFileName;
                GlobalBotHandler.CurrentProfilePath = openFileDialog.FileName;
            }
        }

        private void CurrentRouteProfileTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Please select a route file",
                Filter = "Xml files (*.xml)|*.xml",
                InitialDirectory = Directory.GetCurrentDirectory()
            };
            if (openFileDialog.ShowDialog() == true)
            {
                CurrentRouteProfileTextBox.Text = openFileDialog.SafeFileName;
                GlobalBotHandler.CurrentRoutePath = openFileDialog.FileName;
            }
        }

        private void RunbuttonClick(object sender, RoutedEventArgs e)
        {
            if (Runbutton.Content is "Run")
            {
                TelegramBot.BotСondition = true;
            }
            else
            {
                TelegramBot.BotСondition = false;
            }
        }

        private void GetMobIdButton_Click(object sender, RoutedEventArgs e)
        {
            GameInfo.Refresh(false);
            if (!GameInfo.Main)
            {
                MessageBox.Show("Dont see pixels , pls fix it!");
                return;
            }
            Clipboard.SetText(GameInfo.TargetName.ToString());
        }

        #endregion

        #region Mesh Creator UI
        private void Constructor_Click(object sender, RoutedEventArgs e)
        {
            double xstart;
            double ystart;
            double xend;
            double yend;

            if (double.TryParse(XStartEditBox.Text, out xstart)) MeshCreator.XStart = xstart;
            else
            {
                MessageBox.Show("Wrong XStart");
                return;
            }

            if (double.TryParse(YStartEditBox.Text, out ystart)) MeshCreator.YStart = ystart;
            else
            {
                MessageBox.Show("Wrong YStart");
                return;
            }

            if (double.TryParse(XEndEditBox.Text, out xend)) MeshCreator.XEnd = xend;
            else
            {
                MessageBox.Show("Wrong XEnd");
                return;
            }

            if (double.TryParse(YEndEditBox.Text, out yend)) MeshCreator.YEnd = yend;
            else
            {
                MessageBox.Show("Wrong YEnd");
                return;
            }

            if (!string.IsNullOrWhiteSpace(MeshEditBox.Text) && !string.IsNullOrEmpty(MeshEditBox.Text)) MeshCreator.MeshPath = MeshEditBox.Text;
            else
            {
                MessageBox.Show("Wrong MeshPath");
                return;
            }

            if (!string.IsNullOrWhiteSpace(PictureEditBox.Text) && !string.IsNullOrEmpty(PictureEditBox.Text) && File.Exists(PictureEditBox.Text)) MeshCreator.PicturePath = PictureEditBox.Text;
            else
            {
                MessageBox.Show("Wrong PictureBox");
                return;
            }

            MeshCreator.AutoCoord = AutoCoordSlider.IsChecked == false || AutoCoordSlider.IsChecked == null ? false : true;


            MeshHandler.MeshPath = MeshEditBox.Text;

            if (MeshCreator.AutoCoord && !WowProcess.DetectWowWindow())
            {
                MessageBox.Show("No wow window!!!");
                return;
            }
            if (!MeshHandler.ReadMesh())
                return;

            MeshCreator window = new MeshCreator();
            window.ShowDialog();
        }

        private void MeshEditBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Choose mesh...",
                Filter = "Xml files (*.xml)|*.xml",
                InitialDirectory = Directory.GetCurrentDirectory()
            };
            if (openFileDialog.ShowDialog() == true)
            {
                MeshEditBox.Text = openFileDialog.FileName;
            }
        }

        private void PictureEditBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Title = "Choose picture...",
                    Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png",
                    InitialDirectory = Directory.GetCurrentDirectory()
                };
                if (openFileDialog.ShowDialog() == true)
                {
                    PictureEditBox.Text = openFileDialog.FileName;
                }
            }
        }

        private void TesterSlider_Click(object sender, RoutedEventArgs e)
        {
            if (TesterSlider.IsChecked is false)
            {
                TesterNameOrX.Text = "Name";
                TesterNameOrY.Visibility = Visibility.Hidden;
                TesterNameorYField.Visibility = Visibility.Hidden;

            }
            if (TesterSlider.IsChecked == true)
            {
                TesterNameOrX.Text = "X";
                TesterNameOrY.Text = "Y";
                TesterNameorXField.Text = TesterNameorYField.Text = "";
                TesterNameOrY.Visibility = Visibility.Visible;
                TesterNameorYField.Visibility = Visibility.Visible;
            }
        }

        private void TesterMeshEditBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Choose mesh...",
                Filter = "Xml files (*.xml)|*.xml",
                InitialDirectory = Directory.GetCurrentDirectory()
            };
            if (openFileDialog.ShowDialog() == true)
            {
                TesterMeshEditBox.Text = openFileDialog.FileName;
            }
        }

        private void TesterRun_Click(object sender, RoutedEventArgs e)
        {
            if (TesterRun.Content is "Run")
            {
                if (!File.Exists(TesterMeshEditBox.Text))
                {
                    MessageBox.Show("Route file not exist!");
                    return;
                }
                MeshHandler.MeshPath = TesterMeshEditBox.Text;
                MeshHandler.ReadMesh();
                if (!MeshHandler.PointNameAvailability(TesterNameorXField.Text))
                {
                    MessageBox.Show("Bad dot name!");
                    return;
                }
                GameInfo.Refresh(false);
                if (!GameInfo.Main)
                {
                    MessageBox.Show("Dont see pixels!");
                    return;
                }
                TransportClass tr = new TransportClass(TesterNameorXField.Text, MeshHandler.MergedSpotVendorGhost());
                Thread myThread = new Thread(new ParameterizedThreadStart(Navigation.RunRoute));
                myThread.Start(tr);
                TesterRun.Content = "Pause";
            }
            else
            {
                TesterRun.Content = "Pausing...";
                Navigation.StopNavigation();
                TesterRun.Content = "Run";
            }
        }

        #endregion
    }
}
