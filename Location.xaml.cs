using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Configuration;
using 天气.cn.com.webxml.www;

namespace 天气
{
    /// <summary>
    /// Location.xaml 的交互逻辑
    /// </summary>
    public partial class Location : Window
    {
        public Location()
        {
            InitializeComponent();
        }
        WeatherWS WS = new WeatherWS();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] Region = WS.getRegionProvince();
            cb_r.ItemsSource = Region;
            cb_r.SelectionChanged += Cb_r_SelectionChanged;
        }

        private void Cb_r_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] RegionInfo = cb_r.SelectedValue.ToString().Split(',');
            string[] City = WS.getSupportCityString(RegionInfo[1]);
            cb_c.ItemsSource = City;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void DiSet_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Set_Click(object sender, RoutedEventArgs e)
        {
            if (cb_c.SelectedIndex != -1)
            {
                Configuration CFG = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                CFG.AppSettings.Settings["City"].Value = cb_c.SelectedValue.ToString().Split(',')[0];
                CFG.Save();
                ConfigurationManager.RefreshSection("appSettings");
                Close();
            }
            else
            { }
        }
    }
}
