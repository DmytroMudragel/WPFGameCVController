using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectYZ
{
    /// <summary>
    /// Interaction logic for MeshCreator.xaml
    /// </summary>
    public partial class MeshCreator : Window
    {
        public static bool AutoCoord { get; set; }
        public static double XStart { get; set; }
        public static double YStart { get; set; }
        public static double XEnd { get; set; }
        public static double YEnd { get; set; }
        public static string MeshPath { get; set; }
        public static string PicturePath { get; set; }
        public static double XScaleCanvas { get; set; }
        public static double YScaleCanvas { get; set; }


        public MeshCreator()
        {
            InitializeComponent();
            Application.Current.MainWindow.Closing += new CancelEventHandler(MeshCreator_Closing);
            BitmapImage theImage = new BitmapImage(new Uri(PicturePath, UriKind.Relative));
            CanvasBuilder.Background = new ImageBrush(theImage);
            XScaleCanvas = (XEnd - XStart) / CanvasBuilder.Width;
            YScaleCanvas = (YEnd - YStart) / CanvasBuilder.Height;
            if (!AutoCoord)
                PointFromWow.Visibility = Visibility.Hidden;
            DrawEdgesCanvas();
            DrawPointsCanvas();
        }

        private void CreateEdge_Click(object sender, RoutedEventArgs e)
        {
            if (!MeshHandler.PointNameAvailability(FirstEdgePointName.Text) || !MeshHandler.PointNameAvailability(SecondEdgePointName.Text))
            {
                MessageBox.Show("Both or one points name not available!!!");
                return;
            }
            if (FirstEdgePointName.Text == SecondEdgePointName.Text)
            {
                MessageBox.Show("Same point!!!");
                return;
            }
            if (MeshHandler.EdgeAvailability(FirstEdgePointName.Text, SecondEdgePointName.Text))
            {
                MessageBox.Show("Edge was created before!!!");
                return;
            }

            MeshHandler.AppendEdge(FirstEdgePointName.Text, SecondEdgePointName.Text);
            RebuildCanvas();
        }

        private void DeleteEdge_Click(object sender, RoutedEventArgs e)
        {
            if (!MeshHandler.PointNameAvailability(FirstEdgePointName.Text) || !MeshHandler.PointNameAvailability(SecondEdgePointName.Text))
            {
                MessageBox.Show("Both or one points name not available!!!");
                return;
            }
            if (FirstEdgePointName.Text == SecondEdgePointName.Text)
            {
                MessageBox.Show("Same point!!!");
                return;
            }

            if (!MeshHandler.EdgeAvailability(FirstEdgePointName.Text, SecondEdgePointName.Text))
            {
                MessageBox.Show("Edge not exists!!!");
                return;
            }

            MeshHandler.DeleteEdge(FirstEdgePointName.Text, SecondEdgePointName.Text);
            RebuildCanvas();
        }

        private void PointManual_Click(object sender, RoutedEventArgs e)
        {
            if (SpotCheckBox.IsChecked is false && VendorCheckBox.IsChecked is false && GhostCheckBox.IsChecked is false)
            {
                MessageBox.Show("Pls select type in checkbox!");
                return;
            }

            if (MeshHandler.PointNameAvailability(PointCreateName.Text))
            {
                MessageBox.Show("Point name not available!!!");
                return;
            }
            if (!double.TryParse(PointCreateX.Text, out double xcoord) | !double.TryParse(PointCreateY.Text, out double ycoord))
            {
                MessageBox.Show("Point coords not double!!!");
                return;

            }

            if (MeshHandler.PointCoordsAvailability(xcoord, ycoord))
            {
                MessageBox.Show("Point coords not available, other coordinate have same!!!");
                return;
            }
            if (!double.TryParse(PointCreateRadius.Text, out double r) | !double.TryParse(PointCreateAccuracy.Text, out double a) | !int.TryParse(PointCreateScreenshotTimeout.Text, out int scr))
            {
                MessageBox.Show("Arguments unvalid!!!");
                return;
            }

            bool mountcheck = PointCreateMount.IsChecked != null && PointCreateMount.IsChecked != false;

            if (SpotCheckBox.IsChecked == true)
            {
                MeshHandler.AppendDot("spot", PointCreateName.Text, xcoord, ycoord, r, a, scr, mountcheck);
            }
            if (VendorCheckBox.IsChecked == true)
            {
                MeshHandler.AppendDot("vendor", PointCreateName.Text, xcoord, ycoord, r, a, scr, mountcheck);
            }
            if (GhostCheckBox.IsChecked == true)
            {
                MeshHandler.AppendDot("ghost", PointCreateName.Text, xcoord, ycoord, r, a, scr, mountcheck);
            }

            RebuildCanvas();
        }

        private void PointFromWow_Click(object sender, RoutedEventArgs e)
        {
            if (SpotCheckBox.IsChecked is false && VendorCheckBox.IsChecked is false && GhostCheckBox.IsChecked is false)
            {
                MessageBox.Show("Pls select type in checkbox!");
                return;
            }

            GameInfo.Refresh();

            PointCreateX.Text = GameInfo.X.ToString();
            PointCreateY.Text = GameInfo.Y.ToString();

            if (MeshHandler.PointNameAvailability(PointCreateName.Text))
            {
                MessageBox.Show("Point name not available!!!");
                return;
            }
            if (!double.TryParse(PointCreateX.Text, out double xcoord) | !double.TryParse(PointCreateY.Text, out double ycoord))
            {
                MessageBox.Show("Point coords not double!!!");
                return;
            }

            if (MeshHandler.PointCoordsAvailability(xcoord, ycoord))
            {
                MessageBox.Show("Point coords not available, other coordinate have same!!!");
                return;
            }
            if (!double.TryParse(PointCreateRadius.Text, out double r) | !double.TryParse(PointCreateAccuracy.Text, out double a) | !int.TryParse(PointCreateScreenshotTimeout.Text, out int scr))
            {
                MessageBox.Show("Arguments unvalid!!!");
                return;
            }

            bool mountcheck = PointCreateMount.IsChecked != null && PointCreateMount.IsChecked != false;

            if (SpotCheckBox.IsChecked == true)
            {
                MeshHandler.AppendDot("spot", PointCreateName.Text, xcoord, ycoord, r, a, scr, mountcheck);
            }
            if (VendorCheckBox.IsChecked == true)
            {
                MeshHandler.AppendDot("vendor", PointCreateName.Text, xcoord, ycoord, r, a, scr, mountcheck);
            }
            if (GhostCheckBox.IsChecked == true)
            {
                MeshHandler.AppendDot("ghost", PointCreateName.Text, xcoord, ycoord, r, a, scr, mountcheck);
            }

            RebuildCanvas();
            PointCreateName.Text = Utils.GenerateName(5).ToLower();
        }

        private void CanvasBuilder_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(CanvasBuilder);
            double XCanvasConverted = Math.Round(XStart + (XScaleCanvas * point.X), 2);
            double YCanvasConverted = Math.Round(YStart + (YScaleCanvas * point.Y), 2);

            PointCreateX.Text = XCanvasConverted.ToString();
            PointCreateY.Text = YCanvasConverted.ToString();
            if (string.IsNullOrEmpty(PointCreateRadius.Text) && string.IsNullOrEmpty(PointCreateAccuracy.Text) && string.IsNullOrEmpty(PointCreateScreenshotTimeout.Text))
            {
                PointCreateRadius.Text = 0.1.ToString();
                PointCreateAccuracy.Text = 1.ToString();
                PointCreateScreenshotTimeout.Text = 1000.ToString();
            }

            PointCreateName.Text = Utils.GenerateName(5).ToLower();
        }

        private void Grid_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter: break;
                case Key.Escape: Close(); break;
                default: break;
            }
        }

        private void DrawPointsCanvas()
        {
            DrawPointsCanvasA(MeshHandler.MeshDots.SpotDots.Dot, Brushes.Green);
            DrawPointsCanvasA(MeshHandler.MeshDots.VendorDots.Dot, Brushes.Yellow);
            DrawPointsCanvasA(MeshHandler.MeshDots.GhostDots.Dot, Brushes.Red);
        }


        private void DrawPointsCanvasA(List<Dot> l, Brush b)
        {
            foreach (Dot p in l)
            {
                if (!(p.X > XStart & p.X < XEnd & p.Y > YStart & p.Y < YEnd)) continue;



                Ellipse circle = new Ellipse()
                {
                    Width = p.Radius * 2 * (1 / XScaleCanvas),
                    Height = p.Radius * 2 * (1 / YScaleCanvas),
                    Stroke = b,
                    StrokeThickness = 2,
                };

                Label label = new Label()
                {
                    Content = p.Name,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    Width = 100,
                    Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
                    FontSize = 16
                };

                circle.SetValue(Canvas.LeftProperty, (p.X - XStart) * (1 / XScaleCanvas) - p.Radius * (1 / XScaleCanvas));
                circle.SetValue(Canvas.TopProperty, (p.Y - YStart) * (1 / YScaleCanvas) - p.Radius * (1 / YScaleCanvas));

                label.SetValue(Canvas.LeftProperty, (p.X - XStart) * (1 / XScaleCanvas) - 50);
                label.SetValue(Canvas.TopProperty, (p.Y - YStart) * (1 / YScaleCanvas) + 25);

                CanvasBuilder.Children.Add(circle);
                CanvasBuilder.Children.Add(label);
            }
        }

        private void DrawEdgesCanvas()
        {
            foreach (Edge edg in MeshHandler.MeshDots.EdgeList.Edge)
            {
                if (MeshHandler.PointNameAvailability(edg.Dot1) && MeshHandler.PointNameAvailability(edg.Dot2))
                {
                    Dot mp1 = MeshHandler.GetPointByName(edg.Dot1);
                    Dot mp2 = MeshHandler.GetPointByName(edg.Dot2);

                    if (mp1.X > XStart && mp1.X < XEnd && mp2.X > XStart && mp2.X < XEnd && mp1.Y > YStart && mp1.Y < YEnd && mp2.Y > YStart && mp2.Y < YEnd)
                    {
                        Line redLine = new Line()
                        {
                            X1 = (int)((mp1.X - XStart) * (1 / XScaleCanvas)),
                            Y1 = (int)((mp1.Y - YStart) * (1 / YScaleCanvas)),
                            X2 = (int)((mp2.X - XStart) * (1 / XScaleCanvas)),
                            Y2 = (int)((mp2.Y - YStart) * (1 / YScaleCanvas))
                        };

                        SolidColorBrush redBrush = new SolidColorBrush()
                        {
                            Color = Colors.Red
                        };

                        redLine.StrokeThickness = 2;
                        redLine.Stroke = redBrush;

                        CanvasBuilder.Children.Add(redLine);
                    }
                }
            }
        }


        private void MeshCreator_Closing(object sender, CancelEventArgs e)
        {
            MeshHandler.WriteMesh();
        }

        private void ClearPointNameButton_Click(object sender, RoutedEventArgs e)
        {
            if (!MeshHandler.PointNameAvailability(ClearPointName.Text))
            {
                MessageBox.Show("Wrong dot Name!!!");
                return;
            }
            MeshHandler.DeleteDot(ClearPointName.Text);
            MeshHandler.DeleteEdgesConnectedToDot(ClearPointName.Text);
            ClearPointName.Text = "";
            RebuildCanvas();
        }

        private void RebuildCanvas()
        {
            CanvasBuilder.Children.Clear();
            DrawEdgesCanvas();
            DrawPointsCanvas();
        }

        private void FormMain_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
                "Save Meshes ?",
                "Warning",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Information);
            switch (result)
            {
                case MessageBoxResult.Yes: MeshHandler.WriteMesh(); e.Cancel = false; break;
                case MessageBoxResult.Cancel: e.Cancel = true; break;
                case MessageBoxResult.No: e.Cancel = false; break;
                default: break;
            }
        }

        private void SpotCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (SpotCheckBox.IsChecked == true && (VendorCheckBox.IsChecked == true || GhostCheckBox.IsChecked == true))
            {
                SpotCheckBox.IsChecked = false;
            }
        }

        private void VendorCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (VendorCheckBox.IsChecked == true && (SpotCheckBox.IsChecked == true || GhostCheckBox.IsChecked == true))
            {
                VendorCheckBox.IsChecked = false;
            }
        }

        private void GhostCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (GhostCheckBox.IsChecked == true && (SpotCheckBox.IsChecked == true || VendorCheckBox.IsChecked == true))
            {
                GhostCheckBox.IsChecked = false;
            }
        }
    }
}
