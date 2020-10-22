using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using System.Windows.Shell;

namespace WindowsTaskBar10
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private BackgroundWorker backgroundWorker = new BackgroundWorker();
        public MainWindow()
        {
            InitializeComponent();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
        }

        private void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
            this.textBlock.Text = e.ProgressPercentage.ToString();
            taskbarItemInfo.ProgressValue = e.ProgressPercentage/100F;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            //MessageBox.Show(e.Cancelled ? "取消" : "完成");
            if(e.Cancelled==true)
            {
                taskbarItemInfo.ProgressState = TaskbarItemProgressState.Paused;
                taskbarItemInfo.Overlay = (DrawingImage)this.FindResource("ImageStop");
            }
            else
            {
                taskbarItemInfo.ProgressState = TaskbarItemProgressState.None;
                taskbarItemInfo.Overlay = null;
            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;
            if (backgroundWorker == null) return;

            for (int i = 0; i <= 100; i++)
            {
                if (backgroundWorker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                Thread.Sleep(25);
                backgroundWorker.ReportProgress(i);
            }
        }

        int index = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            index++;
            JumpList jumpList = JumpList.GetJumpList(Application.Current);

            //JumpTask jumpTask2 = new JumpTask();
            //jumpTask2.ApplicationPath = @"C:\Windows\notepad.exe";
            //jumpTask2.Arguments = "readme.txt";
            //jumpTask2.CustomCategory = "User Added Tasks";
            //jumpTask2.Description = "Open readme.txt in Notepad.";
            //jumpTask2.IconResourcePath = @"C:\Windows\System32\imageres.dll";
            //jumpTask2.IconResourceIndex = 14;
            //jumpTask2.WorkingDirectory = @"C:\Users\Public\Documents";
            //jumpTask2.Title = "Open Notepad" + index;
            //jumpList.JumpItems.Add(jumpTask2);

            JumpPath jumpPath = new JumpPath();
            jumpPath.CustomCategory = "JUMP_PATH";
            jumpPath.Path = "aaa.a00";
            jumpList.JumpItems.Add(jumpPath);

            jumpList.Apply();
            jumpList.JumpListSet();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.textBlock.Text = Environment.CommandLine;
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            index++;
            JumpTask jumpTask1 = new JumpTask();
            jumpTask1.ApplicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "calc.exe");
            jumpTask1.IconResourcePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "calc.exe");
            jumpTask1.Title = "计算器 " + index;
            jumpTask1.Description = "打开计算器 " + index;
            jumpTask1.CustomCategory = "计算器类";

            JumpList jumpList = JumpList.GetJumpList(Application.Current);
            jumpList.JumpItems.Add(jumpTask1);
            jumpList.Apply();
            jumpList.JumpListSet();
        }

        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            index = 0;
            JumpList jumpList = JumpList.GetJumpList(Application.Current);
            jumpList.JumpItems.Clear();
            //jumpList.JumpItems.RemoveAt(0);
            jumpList.Apply();
            jumpList.JumpListSet();
        }

        private void Button_Reset(object sender, RoutedEventArgs e)
        {
            index = 0;

            index++;
            JumpTask jumpTask1 = new JumpTask();
            jumpTask1.ApplicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "write.exe");
            jumpTask1.IconResourcePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "write.exe");
            jumpTask1.Title = "写字板 " + index;
            jumpTask1.Description = "打开写字板 " + index;
            jumpTask1.CustomCategory = "默认项";

            index++;
            JumpTask jumpTask2 = new JumpTask();
            jumpTask2.ApplicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "notepad.exe");
            jumpTask2.IconResourcePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "notepad.exe");
            jumpTask2.Title = "记事本 " + index;
            jumpTask2.Description = "打开记事本 " + index;
            jumpTask2.CustomCategory = "默认项";

            //JumpList jumpList = JumpList.GetJumpList(Application.Current);
            //jumpList.JumpItems.Clear();
            JumpList jumpList = new JumpList();

            jumpList.JumpItems.Add(jumpTask1);
            jumpList.JumpItems.Add(jumpTask2);
            jumpList.Apply();
            jumpList.JumpListSet();
        }

        private void Button_Play(object sender, RoutedEventArgs e)
        {
            Play();
        }

        private void Play()
        {
            if (backgroundWorker.IsBusy == true) return;

            backgroundWorker.RunWorkerAsync();

            taskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
            //taskbarItemInfo.ProgressState = TaskbarItemProgressState.Error;
            //taskbarItemInfo.ProgressState = TaskbarItemProgressState.Indeterminate;
            //taskbarItemInfo.ProgressState = TaskbarItemProgressState.None;
            //taskbarItemInfo.ProgressState = TaskbarItemProgressState.Paused;

            taskbarItemInfo.Overlay = (DrawingImage)this.FindResource("ImagePlay");
        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            backgroundWorker.CancelAsync();
        }

        private void thumbButtonPlay_Click(object sender, EventArgs e)
        {
            Play();
        }

        private void thumbButtonStop_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
        }
    }
}
