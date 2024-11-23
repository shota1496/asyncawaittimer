using HtmlAgilityPack;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace MyTimer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        CancellationTokenSource? cts;
        CancellationToken token;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            cts = new CancellationTokenSource();
            token = cts.Token;

            StartTimer(token);
        }

        
        public async void StartTimer(CancellationToken token)
        {
            try
            {
                //1秒ごとに時間を更新する
                while (true)
                {
                    //キャンセルされていたら例外を発生させる
                    token.ThrowIfCancellationRequested();
                    string time = DateTime.Now.ToString("yyyy年MM月dd日 HH時mm分ss秒");
                    TxtTime.Dispatcher.Invoke(() => { TxtTime.Text = time; });
                    Debug.WriteLine(time);
                    await Task.Delay(1000);
                }

            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.ToString());
                MessageBox.Show("タスクがキャンセルされました");
  
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                MessageBox.Show(ex.Message.ToString());
            }

        }

        /// <summary>
        /// ボタンクリックでキャンセル処理を発行する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }
    }
}