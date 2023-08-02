using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

namespace ClasS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Point _pressedPosition;
        bool _isDragMoved = false;
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.Manual;
            double sHeight = SystemParameters.FullPrimaryScreenHeight;
            double sWidth = SystemParameters.FullPrimaryScreenWidth;
            this.Top = (sHeight - 67) / 2 - sHeight / 6 * 2;
            this.Left = (sWidth - 592) / 2;
            StreamWriter Csw = new StreamWriter("Class.txt",true);
            Csw.Close();
            FileInfo info = new FileInfo("Class.txt");
            if (info.Exists) { info.Attributes = FileAttributes.Hidden; }

            InitializeComponent();
        }

        void Window_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _pressedPosition = e.GetPosition(this);
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

        private void Label_add(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Label_add触发");
        }

        private void Text_reload(object sender, MouseButtonEventArgs e)
        {
            String[] line = new String[45];
            int line_num = 0;
            StreamReader Csr = new StreamReader("Class.txt");
            while (true)
            {
                line[line_num] = Csr.ReadLine();
                if (line[line_num] == null) { break; }
                line_num++;
            }
            //L_0.Content = Regex.Replace(line[0], @"[^0-9]+", "");
            if (line_num > 8) 
            {
                L_0.Content = line[0][0];
                L_1.Content = line[1][0];
                L_2.Content = line[2][0];
                L_3.Content = line[3][0];
                L_4.Content = line[4][0];
                L_5.Content = line[5][0];
                L_6.Content = line[6][0];
                L_7.Content = line[7][0];
                L_8.Content = line[8][0];
            }
            Csr.Close();
        }

        private void Text_time(object sender, MouseButtonEventArgs e)
        {
            if (Pop_time.IsOpen == true)
            {
                Pop_time.IsOpen = false;
            }
            else { Pop_time.IsOpen = true; }
        }

        private void Label_Cfont(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.Environment.CurrentDirectory;    //初始的文件夹
            openFileDialog.Filter = "课表字体|*.ttf";//在对话框中显示的文件类型
            //openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();    //显示对话框
            string filepath = openFileDialog.FileName; //获取选择的文件的全路径名
        }

        private void Label_Tfont(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.Environment.CurrentDirectory; ;    //初始的文件夹
            openFileDialog.Filter = "计时字体|*.ttf";//在对话框中显示的文件类型
            //openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();    //显示对话框
            string filepath = openFileDialog.FileName; //获取选择的文件的全路径名
        }

        private void Label_Afont(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = System.Environment.CurrentDirectory; ;    //初始的文件夹
            openFileDialog.Filter = "全局字体|*.ttf";//在对话框中显示的文件类型
            //openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.ShowDialog();    //显示对话框
            string filepath = openFileDialog.FileName; //获取选择的文件的全路径名
            this.FontFamily = new FontFamily("file:///"+ filepath);
            //StreamWriter Fsw = new StreamWriter("Font.txt", true);
            //Fsw.WriteLine("file:///" + filepath);
            //Fsw.Close();
            //FileInfo info = new FileInfo("Font.txt");
            //if (info.Exists) { info.Attributes = FileAttributes.Hidden; }
        }
    }
}
