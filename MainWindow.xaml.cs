using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using 天气.cn.com.webxml.www;
using System.Configuration;
using System.Net.Http;

namespace 天气
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TiteText_MouseMove(object sender, MouseEventArgs e)
        {
            WindowMove(e);
        }

        private void Title_Base_MouseMove(object sender, MouseEventArgs e)
        {
            WindowMove(e);
        }

        private void WindowMove(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
        private void ThisClose()
        {
            Process.GetCurrentProcess().Kill();
        }
        private void btn_Close_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush brush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            btn_Close.Background = brush;
        }

        private void btn_Close_MouseLeave(object sender, MouseEventArgs e)
        {
            Brush brush = new SolidColorBrush(Color.FromArgb(0, 255, 0, 0));
            btn_Close.Background = brush;
        }

        private void btn_Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ThisClose();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ResetText();
            await LoadWeatherAsync();

        }
        WeatherWS WS = new WeatherWS();
        public async Task LoadWeatherAsync()
        {
            WS.getWeatherCompleted += WS_getWeatherCompleted;
            await Task.Run(() => WS.getWeatherAsync(ConfigurationManager.AppSettings["City"], null));
            
        }
        public void ResetText()
        {
            Time_Last.Text = null;
            Local.Text = null;
            Temp.Text = null;
            MoreInfo.Text = null;
            Day1.Text = null;
            Day1Temp.Text = null;
            Day2.Text = null;
            Day2Temp.Text = null;
            Day3.Text = null;
            Day3Temp.Text = null;
        }

        private void WS_getWeatherCompleted(object sender, getWeatherCompletedEventArgs e)
        {
            string[] WeatherInfo = e.Result;
            string[] Weathericonmsg = WeatherInfo[7].Split(' ');
            Dispatcher.Invoke(() => {
                switch (Weathericonmsg[1])
                {
                    case "晴":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/好天气，太阳.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "多云":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/多云转晴，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "阴":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/多云天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "阵雨":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雨天，下雨，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "雷阵雨":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雷电，暴雨，天气，下雨，雨天.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "雷阵雨并伴有冰雹":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雷电，暴雨，天气，下雨，雨天.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "雨夹雪":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/下雪，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "小雨":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雨天，下雨，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "中雨":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雨天，下雨，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "大雨":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雨天，下雨，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "暴雨":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雨天，下雨，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "大暴雨":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雨天，下雨，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "特大暴雨":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雨天，下雨，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "阵雪":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雪花，冬天，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "小雪":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雪花，冬天，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "中雪":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雪花，冬天，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "大雪":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雪花，冬天，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    case "暴雪":
                        iconW.ImageSource = new BitmapImage(new Uri("icon/雪花，冬天，天气.png", UriKind.RelativeOrAbsolute));
                        break;
                    default:
                        iconW.ImageSource = new BitmapImage(new Uri("icon/iconfont-icon.png", UriKind.RelativeOrAbsolute));
                        break;
                }
                Time_Last.Text = WeatherInfo[7];
                Local.Text = WeatherInfo[0];
                Temp.Text = WeatherInfo[8];
                MoreInfo.Text = WeatherInfo[6];
                Day1.Text = WeatherInfo[12];
                Day1Temp.Text = WeatherInfo[13];
                Day2.Text = WeatherInfo[17];
                Day2Temp.Text = WeatherInfo[18];
                Day3.Text = WeatherInfo[22];
                Day3Temp.Text = WeatherInfo[23];
            });

        }

        private void OtherLocal_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Location location = new Location();
            location.Show();
        }

        private void OtherLocal_MouseEnter(object sender, MouseEventArgs e)
        {
            Brush brush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255));
            OtherLocal.Foreground = brush;
        }

        private void OtherLocal_MouseLeave(object sender, MouseEventArgs e)
        {
            Brush brush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            OtherLocal.Foreground = brush;
        }
    }
}
