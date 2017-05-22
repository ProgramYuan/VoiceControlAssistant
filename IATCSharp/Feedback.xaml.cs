using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfIATCSharp
{
    /// <summary>
    /// Feedback.xaml 的交互逻辑
    /// </summary>
    public partial class Feedback : Window
    {
        delegate void MyDelegate(string value);

        private void RefreshWindow(double width, double height)
        {
            Left = System.Windows.SystemParameters.PrimaryScreenWidth - (System.Windows.SystemParameters.PrimaryScreenWidth / 2) - width / 2;
            Top = System.Windows.SystemParameters.PrimaryScreenHeight - height - 40 - 2;
        }
        public Feedback()
        {
            InitializeComponent();

            RefreshWindow(500, 100);

            this.Topmost = true;
            this.MouseDown += new MouseButtonEventHandler(Window_MouseDown);
            //this.MouseMove += new MouseEventHandler(Window_MouseMove);
            //this.MouseLeave += new MouseEventHandler(Window_MouseLeave);
            this.txtContent.TextChanged += new TextChangedEventHandler(txtContent_changeHeight);

            ContextMenu mMenu = new ContextMenu();
            MenuItem closeMenu = new MenuItem();
            closeMenu.Header = "关闭";
            closeMenu.Click += closeMenu_Click;
            mMenu.Items.Add(closeMenu);
            txtContent.ContextMenu = mMenu;

            Thread receiveDataThread = new Thread(new ThreadStart(ReceiveDataFromClient));
            receiveDataThread.IsBackground = true;
            receiveDataThread.Start();
        }

        private void closeMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        void setValue(string value)
        {
            this.txtContent.Text = value;
        }
        private void ReceiveDataFromClient()
        {
            MyDelegate d = new MyDelegate(setValue);
            while (true)
            {
                try
                {
                    NamedPipeServerStream _pipeServer = new NamedPipeServerStream("closePipe", PipeDirection.InOut, 2);
                    _pipeServer.WaitForConnection();
                    StreamReader sr = new StreamReader(_pipeServer);
                    string recData = sr.ReadLine();

                    if (recData.Length > 1)
                    {
                        Debug.WriteLine(recData);
                        this.Dispatcher.Invoke(d, recData);
                    }
                    Thread.Sleep(1000);
                    sr.Close();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            var ColorBrush = new SolidColorBrush(Color.FromScRgb(0, 255, 255, 255));
            this.Background = ColorBrush;
        }

        //Window_MouseLeave
        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            var ColorBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            this.Background = ColorBrush;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void txtContent_changeHeight(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                if ((sender as TextBox).Text != string.Empty && (sender as TextBox).Text.Length / 22 > 3)
                {

                    int row = (sender as TextBox).Text.Length / 22 + 2;
                    this.Height = row * 30;

                    RefreshWindow(500, this.Height);
                }
                else
                {
                    this.Height = 100;
                    this.Width = 500;
                    RefreshWindow(500, 100);
                }
            }
        }
    }
}
