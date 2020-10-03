using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shell;

namespace WindowsTaskBar03
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            JumpList jumpList = new JumpList();

            JumpTask jumpTask1 = new JumpTask();
            jumpTask1.ApplicationPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "calc.exe");
            jumpTask1.IconResourcePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.SystemX86), "calc.exe");
            jumpTask1.Title = "Calculator";
            jumpTask1.Description = "Open Calculator.";
            jumpTask1.CustomCategory = "User Added Tasks";
            jumpList.JumpItems.Add(jumpTask1);

            JumpTask jumpTask2 = new JumpTask();
            jumpTask2.ApplicationPath = @"C:\Windows\notepad.exe";
            jumpTask2.Arguments = "readme.txt";
            jumpTask2.CustomCategory = "User Added Tasks";
            jumpTask2.Description = "Open readme.txt in Notepad.";
            jumpTask2.IconResourcePath = @"C:\Windows\System32\imageres.dll";
            jumpTask2.IconResourceIndex = 14;
            jumpTask2.WorkingDirectory = @"C:\Users\Public\Documents";
            jumpTask2.Title = "Open Notepad";
            jumpList.JumpItems.Add(jumpTask2);

            jumpList.Apply();
        }
    }
}
