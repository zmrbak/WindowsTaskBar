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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace WindowsTaskBar10
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
            this.textBlock.Text = Environment.CommandLine;
        }
    }
}
