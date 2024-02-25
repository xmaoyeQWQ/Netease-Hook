using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using WPFLauncher.Manager.LanGame;

namespace DotNetTranstor
{

    //Virtualize all methods of a class
    [Obfuscation(Feature = "Apply to member * when method or constructor: virtualization", Exclude = false)]

    public class Tool
    {
        public static string randomText;

        static string HKEY_BASE = @"SOFTWARE\Netease\MCLauncher";

        private static string mainPath = Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        /**
         * @desc 取随机字符
         * @param len 长度
         */
        public static string randomStr(int len, String[] arr = null) {

            if (arr == null || arr.Length <= 1) {
                string[] arrb = {
                    "a",
                    "b",
                    "c",
                    "d",
                    "e",
                    "f",
                    "0",
                    "1",
                    "2",
                    "3",
                    "4",
                    "5",
                    "6",
                    "7",
                    "8",
                    "9"
                };
                arr = arrb;
            }

            String result = "";
            for (int i = 0; i < len; i++) {
                result += arr[new Random(new Random(Guid.NewGuid().GetHashCode()).Next(0, 100)).Next(arr.Length - 1)];
            }
            return result;
        }

        /*
         * @desc 取随机Unicode字符
         * @param len 长度
         */
        public static string randomStr_Unicode(int len) {
            string[] arr = { "😄","🆒","😭","🐷","🍟","👍","🚲","❌","🧔","🤑","😜","😋","😡","😀",
                "😘","👌","😄","😤","🐲","👻","👁","🔮","⚔","⚽","🚦","🍩","🍙","🥓","🍖","👱‍","🎠",
                "₯","𝕬","𝖜","𝖎","𝖘","𝖍","𝖋","𝖔","𝖒","𝖞","𝖕","𝖎","𝖓","𝖈","𝖊","𝖘",
                "𝕬","𝕭","𝕮","𝕯","𝕰","𝕱","𝕲","𝕳","𝕴","𝕵","𝕶","𝕷","𝕸","𝕹","𝕺","𝕻","𝕼","𝕽","𝕾","𝕿","𝖀","𝖁","𝖂","𝖃","𝖄","𝖅"
            };
            return randomStr(len, arr);
        }

        /*
         * @desc 取随机MAC地址
         */
        public static string randomMac(String source=null) {
            string result = "";
            if (source != null)
            {
                if (source.Length > 6)
                {
                    result = source.Substring(0, 6);
                }
            }
            else {
                string[] farr = {
                    "002272","00D0EF","086195","F4BD9E","5885E9","BC2392","405582","A4E31B","D89790","883A30","002206","002202","002227","0021ED","0021EB","002260",
                    "001437","001431","00147C","001481","ACED5C","34F64B","9061AE","00E18C","E442A6","00DBDF","98541B","84EF18","A098ED","001925","F0421C","3876CA",
                    "B8BBAF","60C5AD","30E37A","0000C9","0040AA","D0B0CD","7050AF","F4EF9E","84683E","E4B318"
                };
                source = farr[new Random().Next(farr.Length - 1)];
            }
            for (int i = 1; i <=  12; i++) {

                if (i < result.Length + 1) {
                    continue;
                }

                if (i % 2 == 0)
                {
                    if (i == 2)
                    {
                        result += randomStr(1, new string[] { "0", "2","4","6","8","A","C","E"});
                    }
                    else if (i == 12)
                    {
                        result += randomStr(1,new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E" });
                    }
                    else {
                        result += randomStr(1);
                    }
                }
                else {
                    result += randomStr(1);
                }

            }
            result = result.ToUpper();
            return result;
        }

        /**
         * @desc Base64编码
         */
        public static byte[] Base64Encrypt(byte[] bytes) {
            bytes = Encoding.GetEncoding("gbk").GetBytes(Convert.ToBase64String(bytes));
            return bytes;
        }

        /*
         * @desc 判断进程标题是否违规
         */
        public static bool isGoodTitle(String title) {

            if (title == null || title.Length == 0)
                return true;

            String[]
            blackTitles = {
                "destiny","技","流","client","cheat","eclipse","台","集","visual","idea","本","记","编","liquid","demarcia","sense","aquavit","bounce","挂","mix","zero","dnspy",
                "破","解","crack","黑","盒","sigma","jello","flux","remix","lunar","hax","hacker","minecraft","godie","管理员","dbg","语言","命令","system32",
                "弊","端","全","bili","运","power","器","远程","连接","server","jbyte","gui","ida","typora","studio","esle"," | ","windows","linux","admin","anakers","xmaoye"
            };

            title = title.ToLower();

            foreach (String s in blackTitles)
            {
                if (title.Contains(s))
                {
                    return false;
                }
            }
            return true;
        }

        /*
         * @desc 判断进程名是否违规
         */
        public static bool isGoodName(string name) {

            if (name == null || name.Length == 0)
                return false;

            name = name.ToLower();

            if (isSystemProcess(name))
                return true;

            if (name.StartsWith("java") || name.StartsWith("e") || name.StartsWith("qq") || name.StartsWith("tim"))
                return false;

            if (name.StartsWith("lsp") || name.EndsWith("tmp") || name.StartsWith("dev"))
                return false;

            String[]
            blackNames = {
                "LOW","cheat","调试","inject","hacker","edit","dnspy","xmind","wechat","chrome","2345","_","installer",
                "feiq","netscan","rundll32","v2ray","studio","|","esle","anakers","xmaoye","niupaijun","360","fastwin32","IQ","Clash"
            };

            foreach (String s in blackNames)
            {
                if (name.Contains(s))
                {
                    return false;
                }
            }
            return true;
        }

        /*
         *@desc 判断进程名是否为系统进程
         */
        public static bool isSystemProcess(string name) {
            string[] arr = {
                "ServiceHub.RoslynCodeAnalysisService32","dllhost","IAStorDataMgrSvc",
                "fontdrvhost","WmiPrvSE","svchost","crss.exe","SecurityHealthService","spoolsv","dwm","ctfmon","conhost",
                "explorer","loop","CefSharp.BrowserSubprocess.exe"
            };
            foreach (string str in arr) {
                if (name.Equals(str))
                    return true;
            }
            return false;
        }

        /*
         * @desc 获取当前程序目录
         */
        public static string getMainPath()
        {
            return (mainPath == null) || (!File.Exists(mainPath)) ? Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) : mainPath;
        }

        /*
         * @desc 获取游戏目录
         */
        public static string getGamePath() {
            RegistryKey key;
            string dir;
            try {
                key = Registry.CurrentUser.OpenSubKey(HKEY_BASE);
                dir = (string)key.GetValue("DownloadPath");
            }
            catch (Exception) {
                dir = @"C:\MCLDownload";
            }
            return dir;
        }
        public static string getLocalIP(string source)
        {
            string text = "";
            bool flag = text == null || text.Length < 7;
            bool flag2 = flag;
            if (flag2)
            {
                text = Tool.randomIP(source).ToUpper();
                bool flag3 = text.Length > 15;
                bool flag4 = flag3;
                if (flag4)
                {
                    text = text.Substring(0, 15);
                }
            }
            return text;
        }

        public static string getDiskCode()
        {
            string text = "";
            bool flag = text == null || text.Length != 8;
            bool flag2 = flag;
            if (flag2)
            {
                text = Tool.randomStr(8, null).ToUpper();
            }
            return text;
        }

        public static string getJrePatch()
        {
            return getGamePath() + "\\ext\\jre-v64-220420\\jre8\\bin";
        }
        public static bool createMK(String source,String dest) {

            if (Directory.Exists(dest) || File.Exists(dest))
                deleteMK(dest);

            String cmdline = "";

            if (Directory.Exists(dest))
            {
                cmdline = "mklink /d \"" + dest + "\" " + "\"" + source + "\"";
            }
            else {
                cmdline = "mklink \"" + dest + "\" " + "\"" + source + "\"";
            }

            String ret = runCMD(cmdline);
            
            return false;
        }

        /*
         * @desc 删除符号链接
         */
        public static bool deleteMK(String dest) {

            runCMD("rmdir \"" + dest + "\"");

            if (Directory.Exists(dest))
            {
                if (Directory.Exists(dest))
                    Directory.Delete(dest);
                return !Directory.Exists(dest);
            }
            else if (File.Exists(dest)){
                if (File.Exists(dest))
                    File.Delete(dest);
                return !File.Exists(dest);
            }
            return true;
        }

        /*
         * @desc 执行 cmd 指令
         */
        public static String runCMD(String cmdline,bool waiteForExit = true) {
            String result = "";
            Process p = new Process();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd.exe";
            info.Arguments = "/c " + cmdline;
            info.UseShellExecute = false;
            info.RedirectStandardInput = false;
            info.RedirectStandardOutput = true;
            info.CreateNoWindow = true;
            p.StartInfo = info;
            try
            {
                p.Start();
                if (waiteForExit)
                    p.WaitForExit();
                result = p.StandardOutput.ReadToEnd();
            }
            catch
            {

            }
            finally {
                p.Close();
            }
            return result;
        }

        public static String toString()
        {
            return "";
        }

        /*
         * @desc 通过进程PID获取命令行
         */
        public static String getProcessCmd(int pid)
        {
            String result = "",
                    cmd = "wmic process where ProcessId=" + pid + " get commandline";

            result = runCMD(cmd);
            //StaticClient.send("checkProcess", result);
            return result;
        }

        public static bool checkUnSafeMod()
        {
            string path = Tool.getGamePath() + "\\Game\\.minecraft\\mods";
            try
            {
                bool flag = Directory.Exists(path);
                if (flag)
                {
                    foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles())
                    {
                        try
                        {
                            string name = fileInfo.Name;
                            bool flag2 = !name.Contains("@");
                            if (flag2)
                            {
                                bool flag3 = name.EndsWith(".jar");
                                if (flag3)
                                {
                                    return false;
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
                return true;
            }
            return true;
        }

        public static bool deleteUnSafeMod()
        {
            string path = Tool.getGamePath() + "\\Game\\.minecraft\\mods";
            try
            {
                bool flag = Directory.Exists(path);
                if (flag)
                {
                    foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles())
                    {
                        string name = fileInfo.Name;
                        bool flag2 = !name.Contains("@");
                        if (flag2)
                        {
                            bool flag3 = name.EndsWith(".jar");
                            if (flag3)
                            {
                                try
                                {
                                    fileInfo.Delete();
                                    
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static string randomIP(string source)
        {
            Random random = new Random();
            string text = "";
            for (int i = 0; i <= 3; i++)
            {
                string text2 = random.Next(0, 255).ToString();
                text = ((i >= 3) ? (text + text2.ToString()) : (text + (text2 + ".").ToString()));
            }
            return Regex.IsMatch(text, "^((25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))\\.){3}(25[0-5]|2[0-4]\\d|((1\\d{2})|([1-9]?\\d)))$") ? text : "";
        }

        public static void killMe(int pid = -1)
        {
            if (pid <= 0)
            {
                pid = Process.GetCurrentProcess().Id;
            }
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            string text = "taskkill /PID " + pid;
            process.StandardInput.WriteLine(text + "&exit");
            process.StandardInput.AutoFlush = true;
            process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            Process.GetCurrentProcess().Kill();
        }

        public static string getText()
        {
            string randomText = randomStr(1, new string[] { "0", "2", "4", "6", "8", "A", "C", "E" });
            return randomText;
        }
    }
}
