using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shell;

namespace WindowsTaskBar10
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            JumpList jumpList = new JumpList(new List<JumpItem>(), true, false);
            //JumpList jumpList = new JumpList();
            jumpList.JumpListGet();
            jumpList.Apply();
            JumpList.SetJumpList(Application.Current, jumpList);
        }
    }
}
