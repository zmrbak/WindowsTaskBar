using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
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
//using System.Windows.Shapes;
using System.Windows.Shell;

namespace WindowsTaskBar05
{
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

            JumpTask jumpTask2 = new JumpTask();
            jumpTask2.ApplicationPath = @"C:\Windows\notepad.exe";
            jumpTask2.Arguments = "readme.txt";
            jumpTask2.CustomCategory = "User Added Tasks";
            jumpTask2.Description = "Open readme.txt in Notepad.";
            jumpTask2.IconResourcePath = @"C:\Windows\System32\imageres.dll";
            jumpTask2.IconResourceIndex = 14;
            jumpTask2.WorkingDirectory = @"C:\Users\Public\Documents";
            jumpTask2.Title = "Open Notepad" + index;
            jumpList.JumpItems.Add(jumpTask2);

            jumpList.Apply();
            jumpList.JumpListSet();
        }
    }

    public static class JumpListExtention
    {
        [Serializable]
        public class MyJumpItem
        {
            public bool IsJumpPath { get; set; }

            //JumpItem
            public string CustomCategory { get; set; }

            //JumpPath
            public string Path { get; set; }

            //JumpTask
            public string Title { get; set; }
            public string Description { get; set; }
            public string ApplicationPath { get; set; }
            public string Arguments { get; set; }
            public string WorkingDirectory { get; set; }
            public string IconResourcePath { get; set; }
            public int IconResourceIndex { get; set; }
        }

        public static string AppDataPath { get; }
        static JumpListExtention()
        {
            //应用程序的数据保存路径
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyProductAttribute assemblyProductAttribute = assembly.GetCustomAttribute<AssemblyProductAttribute>();
            AppDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Zmrbak",
                assemblyProductAttribute.Product);

            //如果路径不存在，则创建路径
            if (Directory.Exists(AppDataPath) == false)
            {
                Directory.CreateDirectory(AppDataPath);
            }
        }

        public static void JumpListSet(this JumpList jumpList)
        {
            JumpList.SetJumpList(Application.Current, jumpList);

            var jumItemsFileName = Path.Combine(AppDataPath, "JumpItems.bin");
            using (FileStream fileStream = new FileStream(jumItemsFileName, FileMode.Create))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                List<MyJumpItem> myJumpItems = new List<MyJumpItem>();
                foreach (var item in jumpList.JumpItems)
                {
                    if (item is JumpPath)
                    {
                        myJumpItems.Add(new MyJumpItem()
                        {
                            IsJumpPath = true,
                            Path = ((JumpPath)item).Path,

                            CustomCategory = item.CustomCategory
                        });
                    }
                    else
                    {
                        myJumpItems.Add(new MyJumpItem()
                        {
                            IsJumpPath = false,
                            CustomCategory = item.CustomCategory,

                            Title = ((JumpTask)item).Title,
                            Description = ((JumpTask)item).Description,
                            ApplicationPath = ((JumpTask)item).ApplicationPath,
                            Arguments = ((JumpTask)item).Arguments,
                            WorkingDirectory = ((JumpTask)item).WorkingDirectory,
                            IconResourcePath = ((JumpTask)item).IconResourcePath,
                            IconResourceIndex = ((JumpTask)item).IconResourceIndex
                        });
                    }
                }

                binaryFormatter.Serialize(fileStream, myJumpItems);
            }
        }

        public static void JumpListGet(this JumpList jumpList)
        {
            var jumItemsFileName = Path.Combine(AppDataPath, "JumpItems.bin");
            if (File.Exists(jumItemsFileName) == true)
            {
                using (FileStream fileStream = new FileStream(jumItemsFileName, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    List<MyJumpItem> myJumpItems = binaryFormatter.Deserialize(fileStream) as List<MyJumpItem>;
                    if (myJumpItems != null)
                    {
                        foreach (var item in myJumpItems)
                        {
                            if (item.IsJumpPath)
                            {
                                jumpList.JumpItems.Add(
                                    new JumpPath
                                    {
                                        CustomCategory = item.CustomCategory,
                                        Path = item.Path
                                    });
                            }
                            else
                            {
                                jumpList.JumpItems.Add(
                                    new JumpTask
                                    {
                                        CustomCategory = item.CustomCategory,

                                        Title = item.Title,
                                        Description = item.Description,
                                        ApplicationPath = item.ApplicationPath,
                                        Arguments = item.Arguments,
                                        WorkingDirectory = item.WorkingDirectory,
                                        IconResourcePath = item.IconResourcePath,
                                        IconResourceIndex = item.IconResourceIndex
                                    });
                            }
                        }
                    }
                }
            }
        }
    }
}
