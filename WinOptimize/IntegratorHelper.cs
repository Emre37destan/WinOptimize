﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinOptimize
{
    public static class IntegratorHelper
    {
        internal static string FolderDefaultIcon = @"%systemroot%\system32\imageres.dll,-112";

        internal static void CreateCustomCommand(string file, string keyword)
        {
            if (!keyword.EndsWith(".exe"))
            {
                keyword = keyword + ".exe";
            }

            string key = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\" + keyword;

            Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\" + keyword);
            Registry.SetValue(key, "", file);
            Registry.SetValue(key, "Path", file.Substring(0, file.LastIndexOf("\\")));
        }

        internal static List<string> GetCustomCommands()
        {
            List<string> items = new List<string>();

            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\"))
            {
                foreach (string command in key.GetSubKeyNames())
                {
                    items.Add(command);
                }
            }

            return items;
        }

        internal static void DeleteCustomCommand(string command)
        {
            Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\" + command, false);
        }

        private static void CreateDefaultCommand(string itemName)
        {
            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"DesktopBackground\Shell\" + itemName, true))
            {
                key.CreateSubKey("command", RegistryKeyPermissionCheck.Default);
            }
        }

        internal static List<string> GetDesktopItems()
        {
            List<string> items = new List<string>();

            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"DesktopBackground\Shell", false))
            {
                foreach (string item in key.GetSubKeyNames())
                {
                    // filter the list, so the default items will not be visible
                    if (item.Contains("Gadgets")) continue;
                    if (item.Contains("Display")) continue;
                    if (item.Contains("Personalize")) continue;

                    items.Add(item);
                }
            }

            return items;
        }

        internal static void RemoveItem(string name)
        {
            try
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"DesktopBackground\Shell", true))
                {
                    try
                    {
                        key.DeleteSubKeyTree(name, false);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Integrator.RemoveItem", ex.Message, ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Integrator.RemoveItem", ex.Message, ex.StackTrace);
            }
        }

        internal static bool DesktopItemExists(string name)
        {
            try
            {
                return Registry.ClassesRoot.OpenSubKey(@"DesktopBackground\Shell\" + name, false) != null;
            }
            catch (Exception ex)
            {
                Logger.LogError("Integrator.ItemExists", ex.Message, ex.StackTrace);
                return false;
            }
        }

        internal static bool TakeOwnershipExists()
        {
            try
            {
                return Registry.ClassesRoot.OpenSubKey(@"*\shell\runas", false).GetValue("").ToString() == "Take Ownership";
            }
            catch (Exception ex)
            {
                Logger.LogError("Integrator.TakeOwnershipExists", ex.Message, ex.StackTrace);
                return false;
            }
        }

        internal static bool OpenWithCMDExists()
        {
            try
            {
                return Registry.ClassesRoot.OpenSubKey(@"Directory\shell\OpenWithCMD", false) != null;
            }
            catch (Exception ex)
            {
                Logger.LogError("Integrator.OpenWithCMDExists", ex.Message, ex.StackTrace);
                return false;
            }
        }

        internal static void RemoveAllItems(List<string> items)
        {
            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"DesktopBackground\Shell", true))
            {
                foreach (string item in items)
                {
                    try
                    {
                        key.DeleteSubKeyTree(item, false);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Integrator.RemoveAllItems", ex.Message, ex.StackTrace);
                    }
                }
            }
        }

        internal static string ExtractIconFromExecutable(string itemName, string fileName)
        {
            string iconPath = string.Empty;

            if (File.Exists(fileName))
            {
                Icon ico = Icon.ExtractAssociatedIcon(fileName);

                Clipboard.SetImage(ico.ToBitmap());
                Clipboard.GetImage().Save(CoreHelper.ExtractedIconsFolder + itemName + ".ico", ImageFormat.Bmp);
                Clipboard.Clear();

                iconPath = CoreHelper.ExtractedIconsFolder + itemName + ".ico";
            }

            return iconPath;
        }

        internal static string DownloadFavicon(string link, string name)
        {
            string favicon = string.Empty;

            try
            {
                Uri url = new Uri(link);
                if (url.HostNameType == UriHostNameType.Dns)
                {
                    Image.FromStream(((HttpWebResponse)WebRequest.Create("http://" + url.Host + "/favicon.ico").GetResponse()).GetResponseStream()).Save(CoreHelper.FavIconsFolder + name + ".ico", ImageFormat.Bmp);

                    favicon = CoreHelper.FavIconsFolder + name + ".ico";
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Integrator.DownloadFavicon", ex.Message, ex.StackTrace);
            }

            return favicon;
        }

        internal static void AddItem(string name, string item, string icon, DesktopTypePosition position, bool shift, DesktopItemType type)
        {
            using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"DesktopBackground\Shell", true))
            {
                key.CreateSubKey(name, RegistryKeyPermissionCheck.Default);
            }

            CreateDefaultCommand(name);

            if (shift)
            {
                Registry.SetValue(@"HKEY_CLASSES_ROOT\DesktopBackground\Shell\" + name, "Extended", "");
            }
            else
            {
                using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"DesktopBackground\Shell\" + name, true))
                {
                    key.CreateSubKey(name, RegistryKeyPermissionCheck.Default);
                }
            }

            Registry.SetValue(@"HKEY_CLASSES_ROOT\DesktopBackground\Shell\" + name, "Icon", icon);
            Registry.SetValue(@"HKEY_CLASSES_ROOT\DesktopBackground\Shell\" + name, "Position", position.ToString());

            switch (type)
            {
                case DesktopItemType.Program:
                    Registry.SetValue(@"HKEY_CLASSES_ROOT\DesktopBackground\Shell\" + name + "\\command", "", item);
                    break;
                case DesktopItemType.Folder:
                    Registry.SetValue(@"HKEY_CLASSES_ROOT\DesktopBackground\Shell\" + name + "\\command", "", "explorer " + item);
                    break;
                case DesktopItemType.Link:
                    Registry.SetValue(@"HKEY_CLASSES_ROOT\DesktopBackground\Shell\" + name + "\\command", "", "explorer " + item);
                    break;
                case DesktopItemType.File:
                    string tmp = @"""";
                    string tmp2 = "explorer.exe";

                    Registry.SetValue(@"HKEY_CLASSES_ROOT\DesktopBackground\Shell\" + name + "\\command", "", tmp2 + " " + tmp + item + tmp);
                    break;
                case DesktopItemType.Command:
                    Registry.SetValue(@"HKEY_CLASSES_ROOT\DesktopBackground\Shell\" + name + "\\command", "", item);
                    break;
            }
        }

        internal static void InstallOpenWithCMD()
        {
            Utilities.ImportRegistryScript(CoreHelper.ScriptsFolder + "AddOpenWithCMD.reg");
        }

        internal static void DeleteOpenWithCMD()
        {
            Registry.ClassesRoot.DeleteSubKeyTree(@"Directory\shell\OpenWithCMD", false);
            Registry.ClassesRoot.DeleteSubKeyTree(@"Directory\Background\shell\OpenWithCMD", false);
            Registry.ClassesRoot.DeleteSubKeyTree(@"Drive\shell\OpenWithCMD", false);
        }

        internal static void InstallTakeOwnership(bool remove)
        {
            if (!File.Exists(CoreHelper.ReadyMadeMenusFolder + "InstallTakeOwnership.reg"))
            {
                try
                {
                    File.WriteAllText(CoreHelper.ReadyMadeMenusFolder + "InstallTakeOwnership.reg", Properties.Resources.InstallTakeOwnership);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Integrator.TakeOwnership", ex.Message, ex.StackTrace);
                }
            }
            if (!File.Exists(CoreHelper.ReadyMadeMenusFolder + "RemoveTakeOwnership.reg"))
            {
                try
                {
                    File.WriteAllText(CoreHelper.ReadyMadeMenusFolder + "RemoveTakeOwnership.reg", Properties.Resources.RemoveTakeOwnership);
                }
                catch (Exception ex)
                {
                    Logger.LogError("Integrator.TakeOwnership", ex.Message, ex.StackTrace);
                }
            }

            if (!remove)
            {
                Utilities.ImportRegistryScript(CoreHelper.ReadyMadeMenusFolder + "InstallTakeOwnership.reg");
            }
            else
            {
                Utilities.ImportRegistryScript(CoreHelper.ReadyMadeMenusFolder + "RemoveTakeOwnership.reg");
            }
        }

        /// <summary>
        /// PATH System Variables functions
        /// </summary>

        const int HWND_BROADCAST = 0xffff;
        const uint WM_SETTINGCHANGE = 0x001a;

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool SendNotifyMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, string lParam);

        internal static string[] GetPathSystemVariables()
        {
            try
            {
                string basePathKey = @"SYSTEM\CurrentControlSet\Control\Session Manager\Environment";
                using (var key = Registry.LocalMachine.OpenSubKey(basePathKey, false))
                {
                    string result = key.GetValue("Path", new string[] { }).ToString();
                    return result.Split(';');
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Integrator.GetPathSystemVariables", ex.Message, ex.StackTrace);
                return new string[] { };
            }
        }

        internal static void UpdatePathSystemVariables(string[] newValues)
        {
            if (newValues == null || newValues.Length <= 0)
            {
                return;
            }

            try
            {
                string basePathKey = @"SYSTEM\CurrentControlSet\Control\Session Manager\Environment";
                using (var key = Registry.LocalMachine.OpenSubKey(basePathKey, true))
                {
                    string updatedSystemVariables = string.Join(";", newValues);
                    key.SetValue("Path", updatedSystemVariables, RegistryValueKind.ExpandString);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Integrator.UpdatePathSystemVariables", ex.Message, ex.StackTrace);
            }
        }

        // Notifies the shell that System variables have been changed
        // Otherwise, a restart is needed
        internal static void ApplyPathSystemVariables()
        {
            SendNotifyMessage((IntPtr)HWND_BROADCAST, WM_SETTINGCHANGE, (UIntPtr)0, "Environment");
        }
    }
}
