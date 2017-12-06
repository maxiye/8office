using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace WpfApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Clickme_Click(object sender, RoutedEventArgs e)
        {
            string mobile = mobile_txt.Text;
            string pwd = pwd_txt.Password;
            string url = "http://app.8office.cn/site/check-password?username=" + mobile + "&password=" + pwd;
            string retJson = this.HttpGet(url, "");
            JObject jo = JObject.Parse(retJson);
            string[] values = jo.Properties().Select(item => item.Value.ToString()).ToArray();
            if (values.Length > 0)
            {
                if (values[0] == "200")
                {
                    System.Diagnostics.Process.Start("http://web.8office.cn/#/desktop_login?uuid=" + values[2]);
                }
                else
                {
                    MessageBox.Show(values[1], "Maxiye");
                }                
            } else
            {
                MessageBox.Show("网络异常", "Maxiye");
            }

        }
        public string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "application/json;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        private void Pwd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Console.WriteLine("111");
                this.Clickme_Click(sender, e);
            }
        }
    }
}
