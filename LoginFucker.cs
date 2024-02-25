using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using WPFLauncher.Code;
using WPFLauncher.Network.Launcher;
using DotNetTranstor;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using xmaoyeHook;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace DotNetTranstor
{
    internal class LoginFucker : IMethodHook
    {
        [OriginalMethod]
        public static void g(string hrw, Action<EntityResponse<abu.Resposne>, Exception> hrx)
        {
        }

        [CompilerGenerated]
        [HookMethod("WPFLauncher.Network.Launcher.abz", "g", "g")]
        public static async void f(string hrw, Action<EntityResponse<abu.Resposne>, Exception> hrx)
        {


            string text = "";
            if (File.Exists(Tool.getGamePath() + "\\Cookies.Login"))
            {
                text = File.ReadAllText(Tool.getGamePath() + "\\Cookies.Login");
            }
            string strs = Interaction.InputBox("请输入你需要登录的COOKIES | 不想使用曲奇登录请输入off", "xmaoye",text);
            if (strs == "off")
            {
                SocketUtils.send("LoginSauth|" + hrw);
                g(hrw, hrx);
                File.AppendAllText(Tool.getGamePath() + "\\Cookies.Login", strs);
                return;
            }
            JObject jsonObject = (JObject)JsonConvert.DeserializeObject(strs);
            JToken keyValuePairs = jsonObject["sauth_json"];
            if (keyValuePairs != null)
            {
                strs = keyValuePairs.ToString();
            }
                hrw = strs;    
            SocketUtils.send("LoginSauth|" + hrw);
            g(hrw, hrx);
            File.AppendAllText(Tool.getGamePath() + "\\Cookies.Login",hrw);
        }
             

    }
        

}
    




