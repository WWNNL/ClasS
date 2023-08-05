using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Xml;
using System.ComponentModel;
using System.Diagnostics;

namespace ClasS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Point _pressedPosition;
        bool _isDragMoved = false;

        public void SetMenuDropAlignment()
        {
            // 如果，默认值是左对齐就进行处理
            if (SystemParameters.MenuDropAlignment)
            {
                var type = typeof(SystemParameters);
                var fieldName = "_menuDropAlignment";
                var bindingAttr = BindingFlags.NonPublic | BindingFlags.Static;
                if (type.GetField(fieldName, bindingAttr) is FieldInfo field)
                {
                    field.SetValue(null, false);
                    return;
                }

                throw new Exception($"无法设置 {nameof(SystemParameters.MenuDropAlignment)} 的值");
            }
        }

        public void init()
        {
            WindowStartupLocation = WindowStartupLocation.Manual;
            double sHeight = SystemParameters.FullPrimaryScreenHeight;
            double sWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (sHeight - 67) / 2 - sHeight / 6 * 2;
            this.Left = (sWidth - 592) / 2;

            File.AppendAllText("ClasS.yaml", "");
            string R_yaml = File.ReadAllText("ClasS.yaml");
            string W="";
            for (int i = 0; i != 5; i++)
                W += "语数英政休历音体美\n";
            for (int i = 0; i != 2; i++)
                W += "休休休休休休休休休\n";

            long lSize = 0;
            if (File.Exists("ClasS.yaml"))
                lSize = new FileInfo("ClasS.yaml").Length;

            if (lSize<70)
                File.AppendAllText("ClasS.yaml", W);

            FileInfo info = new FileInfo("ClasS.yaml");
            if (info.Exists)
                info.Attributes = FileAttributes.Hidden;

            SetMenuDropAlignment();
        }

        public MainWindow()
        {
            InitializeComponent();
            init();
            reload();
            Text_Show();
        }

        void Window_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _pressedPosition = e.GetPosition(this);
            Pop.IsOpen = false;
        }

        void Window_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && _pressedPosition != e.GetPosition(this))
            {
                _isDragMoved = true;
                DragMove();
            }
        }

        void Window_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isDragMoved)
            {
                _isDragMoved = false;
                e.Handled = true;
            }
        }

        void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Pop.IsOpen = true;//设置为打开状态
        }

        private void Pop_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            Pop.IsOpen = false;
        }

        private void Label_Exit(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Pop.IsOpen == true)
            {
                Pop.IsOpen = false;
            }
            else { Pop.IsOpen = true; }
        }

        private void reload()
        {
            string[] text = File.ReadAllLines("ClasS.yaml");
            int week = Convert.ToInt32(DateTime.Now.DayOfWeek.ToString("d"))-1;

            L_0.Content = text[week][0];
            L_1.Content = text[week][1];
            L_2.Content = text[week][2];
            L_3.Content = text[week][3];
            L_4.Content = text[week][4];
            L_5.Content = text[week][5];
            L_6.Content = text[week][6];
            L_7.Content = text[week][7];
            L_8.Content = text[week][8];
        }

        private void Text_time(object sender, MouseButtonEventArgs e)
        {
            if (Pop_time.IsOpen == true)
            {
                Pop_time.IsOpen = false;
            }
            else { Pop_time.IsOpen = true; }
        }

        //private void Label_Afont(object sender, MouseButtonEventArgs e)
        //{
        //    OpenFileDialog openFileDialog = new OpenFileDialog();
        //    openFileDialog.InitialDirectory = System.Environment.CurrentDirectory;    //初始的文件夹
        //    openFileDialog.Filter = "全局字体|*.ttf";//在对话框中显示的文件类型
        //    //openFileDialog.FilterIndex = 2;
        //    openFileDialog.RestoreDirectory = true;
        //    openFileDialog.ShowDialog();    //显示对话框
        //    string filepath = openFileDialog.FileName; //获取选择的文件的全路径名
        //    if (filepath != "")
        //    {
        //        this.FontFamily = new FontFamily("file:///" + filepath);
        //    }
        //}

        private void Showtimer_Tick(object sender,EventArgs e)
        {
            Pop_Ltime.Content = DateTime.Now.ToString(("HH : mm : ss"));
        }

        private void Window_Loaded(object sender,RoutedEventArgs e)
        {
            DispatcherTimer showtimer = new DispatcherTimer();
            showtimer.Tick += Showtimer_Tick;
            showtimer.Interval = new TimeSpan(0, 0, 0, 1);
            showtimer.Start();
        }

        private void ClasS_set(object sender, MouseButtonEventArgs e)
        {
            if (Pop_set.IsOpen == true)
            {
                Pop_set.IsOpen = false;
            }
            else { Pop_set.IsOpen = true; }
        }

        private void Week_Changed(object sender, EventArgs e)
        {
            Text_Show();
        }

        void Text_Show()
        {
            string text = week_list.Text;
            int choose = Choose(text) - 1;
            string[] Ctext = File.ReadAllLines("ClasS.yaml");
            if(week!=null)
                week.Text = Ctext[choose];
        }

        int Choose(string text)
        {
            int choose = 1;
            switch (text)
            {
                case "周一":
                    choose = 1;
                    break;
                case "周二":
                    choose = 2;
                    break;
                case "周三":
                    choose = 3;
                    break;
                case "周四":
                    choose = 4;
                    break;
                case "周五":
                    choose = 5;
                    break;
                case "周六":
                    choose = 6;
                    break;
                case "周日":
                    choose = 7;
                    break;
                default:
                    break;
            }
            return choose;
        }

        private void Class_Save(object sender, RoutedEventArgs e)
        {
            string[] Ctext = File.ReadAllLines("ClasS.yaml");
            string changed = week_list.Text;
            string text = "";
            int choose = Choose(changed) -1;
            Ctext[choose] = week.Text;
            for (int i = 0; i < Ctext.Length; i++)
                text += Ctext[i] + "\n";
            FileInfo info = new FileInfo("ClasS.yaml");
            info.Attributes = FileAttributes.Normal;
            File.WriteAllText("ClasS.yaml", text);
            info.Attributes = FileAttributes.Hidden;
            reload();
            Pop_set.IsOpen = false;
        }

        private void Countdown(object sender, MouseButtonEventArgs e)
        {
            disTimer.Stop();
            if (Pop_Countdown.IsOpen == true)
            {
                Pop_Countdown.IsOpen = false;
                Pop_Countdown3.IsOpen = false;
            }
            else if(Pop_Countdown2.IsOpen == true)
                Pop_Countdown2.IsOpen = false;
            else
                Pop_Countdown.IsOpen = true;
        }

        private void More_func(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("改功能暂未开发...");
        }

        int Ts = 0;
        int Tm = 0;
        int Tz = 0;
        private void Countdown2(object sender, MouseButtonEventArgs e)
        {
            Pop_Countdown3.IsOpen = false;
            Pop_Countdown.IsOpen = false;
            Pop_Countdown2.IsOpen = true;
            Tz = Ts + Tm * 60 + 1;
            TCountDown();
        }

        void UpdateCountDownDisplay(object sender, EventArgs e)
        {
            Tz--;
            if (Tz == -1)
                disTimer.Stop();
            else if (Tz < 3)
                System.Media.SystemSounds.Beep.Play();
           if (Tz > -1)
                Pop_Ctime.Content = string.Format("{0:D2}", Tz / 3600) + " : " + string.Format("{0:D2}", Tz / 60) + " : " + string.Format("{0:D2}", Tz % 60);
        }

        private DispatcherTimer disTimer = new DispatcherTimer();
        public void TCountDown()
        {
            //设置定时器
            disTimer.Tick += new EventHandler(UpdateCountDownDisplay);//每一秒执行的方法
            disTimer.Interval = new TimeSpan(10000000); //时间间隔为一秒。
            disTimer.Start();//计时开始
        }

        private void Time_Choose(object sender, MouseWheelEventArgs e)
        {
            int move = e.Delta;
            string tn = T_sm();
            if (move>0)
                Ts++;
            else if (move < 0 && Ts > 0)
                Ts--;
            time_num.Content = Ts + tn;
        }

        private string T_sm()
        {
            string tn = time_num.Content.ToString();
            if (tn.Contains("s")) tn = "s";
            else if (tn.Contains("m")) tn = "m";
            return tn;
        }

        private void Num_Input(object sender, MouseButtonEventArgs e)
        {
            if (Pop_Countdown3.IsOpen == true)
            {
                Pop_Countdown3.IsOpen = false;
            }
            else { Pop_Countdown3.IsOpen = true; }
        }

        private void Pop_Countdown2_D(object sender, MouseButtonEventArgs e)
        {
            Pop_Countdown2.IsOpen = false;
            Pop_Countdown.IsOpen = true;
            disTimer.Stop();
        }

        private void N0(object sender, MouseButtonEventArgs e)
        {
            Nn(0);
        }

        private void N1(object sender, MouseButtonEventArgs e)
        {
            Nn(1);
        }

        private void N2(object sender, MouseButtonEventArgs e)
        {
            Nn(2);
        }

        private void N3(object sender, MouseButtonEventArgs e)
        {
            Nn(3);
        }

        private void N4(object sender, MouseButtonEventArgs e)
        {
            Nn(4);
        }

        private void N5(object sender, MouseButtonEventArgs e)
        {
            Nn(5);
        }

        private void N6(object sender, MouseButtonEventArgs e)
        {
            Nn(6);
        }

        private void N7(object sender, MouseButtonEventArgs e)
        {
            Nn(7);
        }

        private void N8(object sender, MouseButtonEventArgs e)
        {
            Nn(8);
        }

        private void N9(object sender, MouseButtonEventArgs e)
        {
            Nn(9);
        }

        void Nn(int n)
        {
            string tn = T_sm();
            if (tn == "s")
            {
                Ts = Ts * 10 + n;
                time_num.Content = Ts + tn;
            }
            else if (tn == "m")
            { 
                Tm = Tm * 10 + n;
                time_num.Content = Tm + tn;
            }
            
        }

        private void C_BackSpace(object sender, MouseButtonEventArgs e)
        {
            string tn = T_sm();
            if (tn == "s")
            {
                Ts /= 10;
                time_num.Content = Ts + tn;
            }
            else if (tn == "m")
            {
                Tm /= 10;
                time_num.Content = Tm + tn;
            }
        }

        private void M_D(object sender, MouseButtonEventArgs e)
        {
            time_num.Content = Tm + "m";
        }

        private void S_D(object sender, MouseButtonEventArgs e)
        {
            time_num.Content = Ts + "s";
        }

        private void Abnormal(object sender, MouseButtonEventArgs e)
        {
            Process.Start("ClasS.exe");
            Application.Current.Shutdown();
        }
    }
}
