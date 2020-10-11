using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            string ext = ".a002";
            string progId = "WindowsTaskBar10.Data.a002";
            string desc = "WindowsTaskBar10 DataFile A002";
            string showExt = "1";
            string exePath = "\"C:\\Users\\Zmrbak\\source\\repos\\WindowsTaskBar10\\WindowsTaskBar10\\bin\\Debug\\WindowsTaskBar10.exe\"";

            //[HKEY_CLASSES_ROOT\.a00]
            RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey(ext);

            //[HKEY_CLASSES_ROOT\.a00\OpenWithProgids]
            registryKey = registryKey.CreateSubKey("OpenWithProgids");
            //"WindowsTaskBar10.Data"=""
            registryKey.SetValue(progId, "");

            //[HKEY_CLASSES_ROOT\WindowsTaskBar10.Data]
            registryKey = Registry.ClassesRoot.CreateSubKey(progId);
            //@="WindowsTaskBar10 DataFile"
            registryKey.SetValue("", desc);
            //"AlwaysShowExt"="1"
            registryKey.SetValue("AlwaysShowExt", showExt);

            //[HKEY_CLASSES_ROOT\WindowsTaskBar10.Data\DefaultIcon]
            registryKey = Registry.ClassesRoot.CreateSubKey(progId).CreateSubKey("DefaultIcon");
            //@="\"C:\\Users\\Zmrbak\\source\\repos\\WindowsTaskBar10\\WindowsTaskBar10\\bin\\Debug\\WindowsTaskBar10.exe\""
            registryKey.SetValue("", exePath);

            //[HKEY_CLASSES_ROOT\WindowsTaskBar10.Data\shell]
            registryKey = Registry.ClassesRoot.CreateSubKey(progId).CreateSubKey("shell");

            //[HKEY_CLASSES_ROOT\WindowsTaskBar10.Data\shell\Open]
            registryKey = Registry.ClassesRoot.CreateSubKey(progId).CreateSubKey("shell").CreateSubKey("Open");

            //[HKEY_CLASSES_ROOT\WindowsTaskBar10.Data\shell\Open\Command]
            registryKey = Registry.ClassesRoot.CreateSubKey(progId).CreateSubKey("shell").CreateSubKey("Open").CreateSubKey("Command");

            //@="\"C:\\Users\\Zmrbak\\source\\repos\\WindowsTaskBar10\\WindowsTaskBar10\\bin\\Debug\\WindowsTaskBar10.exe\" \"%1\""
            registryKey.SetValue("", exePath +" \"%1\"");

            registryKey.Close();
        }
    }
}
