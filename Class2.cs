using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xmaoyeHook;
using WPFLauncher.Util;
using WPFLauncher.Manager;
using System.IO;
using System.Windows.Controls;
using System.Threading;
using WPFLauncher.Network.Launcher;
using WPFLauncher.Code;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using WPFLauncher;
using System.Runtime.CompilerServices;
using WPFLauncher.Network.TransService;
using System.Windows;
using WPFLauncher.Common;
using WPFLauncher.Manager.PCChannel;
using WPFLauncher.Messaging;
using WPFLauncher.Network.Service;
using WPFLauncher.Model;
using WPFLauncher.Network;
using WPFLauncher.View;

namespace DotNetTranstor
{
    internal class Class2 : IMethodHook
    {
        [OriginalMethod]
        public static void f(string hnb, Action<EntityResponse<abu.Resposne>, Exception> hnc)
        {
        }

        
        
        
        [OriginalMethod]
        private bool e_Original()
        {
            return false;
        }
        [HookMethod("WPFLauncher.Manager.Login.asq")]
        private bool e()
        {
            SocketUtils.send("LoginSuccess");
            return this.e_Original();
        }
        
        [HookMethod("WPFLauncher.Manager.Configuration.awp")]
        public bool get_PlayCG()
        {
            SocketUtils.send("PlayCG");
            return false;
        }
        
        [HookMethod("WPFLauncher.Manager.Log.Util.asd")]
        public static string b()
        {
            string text = "";
            string result;
            try
            {
                bool flag = text == null || text.Length != 12;
                bool flag2 = flag;
                bool flag3 = flag2;
                if (flag3)
                {
                    text = getMac(b_Original());
                }
                SocketUtils.send("MACAddress|" + text);
                result = text;
            }
            catch
            {
                result = text;
            }
            return result;
            ;
            
        }
        [OriginalMethod]
        public static string b_Original()
        {
            return null;
        }
        public static string getMac(string source)
        {
            string text = null;
            bool flag = text == null || text.Length != 12;
            bool flag2 = flag;
            bool flag3 = flag2;
            if (flag3)
            {
                text = Tool.randomMac(source).ToUpper();
                bool flag4 = text.Length > 12;
                bool flag5 = flag4;
                bool flag6 = flag5;
                if (flag6)
                {
                    text = text.Substring(0, 12);
                }
            }
            return text;
        }
        [HookMethod("WPFLauncher.Util.vl")]
        public static aqa a(string gsg, string gsh, EventHandler gsi, apy gsj, string gsk = null, bool gsl = false, Action<string> gsm = null)
        {
            SocketUtils.send("GameArgs|" + gsh);
            SocketGameStart = true;
            while (true)
            {
                if(GameStart == true)
                {
                    SocketGameStart = false;
                    GameStart = false;
                    break;
                } 
                    Thread.Sleep(1000);

            }
            Game = a_Original(gsg,gsh,gsi,gsj,gsk,gsl,gsm);
            while (true)
            {
                if (Game.MainWindowHandle != IntPtr.Zero)
                {
                    SocketUtils.send("GameStartOK");
                    break;
                }
                Thread.Sleep(1000);

            }
            while (true)
            {
                if (Game.HasExited)
                {
                    SocketUtils.send("GameStop");
                    break;
                }
                Thread.Sleep(1000);

            }
            

            return Game;
            
        }
        [OriginalMethod]
        public static aqa a_Original(string gro, string grp, EventHandler grq, apy grr, string grs = null, bool grt = false, Action<string> gru = null)
        {
            return Game;
        }
        public static aqa Game;
        public static bool GameStart = false;
        public static bool SocketGameStart = false;
        [HookMethod("WPFLauncher.Network.abk", null, null)]
        public void a(ushort hoc, Action<byte[]> hod)
        {
            try
            {
                this.a_Original(hoc, hod);
                SocketUtils.send("Network.abg.a|" + hoc.ToString() + "|" + hod.Method.ToString() + " " + hod.Target.ToString());
            }
            catch (Exception)
            {
            }
        }
        [HookMethod("WPFLauncher.Network.abk", null, null)]
        public void b(ushort hoe, byte[] hof)
        {
            
                SocketUtils.send("ClientCheck");
                this.b_Original(hoe, hof);
         
        }
        
        [OriginalMethod]
        public void a_Original(ushort hni, Action<byte[]> hnj)
        {
        }

        [OriginalMethod]

        public void b_Original(ushort hnk, byte[] hnl)
        {
        }
        private static ushort[] blokArray = new ushort[19];

        [HookMethod("WPFLauncher.Manager.apv", null, null)]
        public void f(string mfi)
        {
            BanUserEntity banUserEntity = null;
            try
            {
                banUserEntity = JsonConvert.DeserializeObject<BanUserEntity>(mfi);
            }
            catch (Exception frg)
            {
                sp.Default.l(frg, "BanUser");
            }
            if (banUserEntity != null && !banUserEntity.show_tips)
            {
                ayk<aow>.Instance.Status = aqk.f;
                
                nt.Default.aq<nn>(new nn("STOP_PIPELINE", null));
                nt.Default.aq<no>(new no("LOGOUT", null));
                ayk<aow>.Instance.ab(0);
            }
            else
            {
                string mfn = (banUserEntity == null) ? string.Empty : apv.h(banUserEntity.kick_type, banUserEntity.reason, banUserEntity.ban_to_ts);
                SocketUtils.send(mfn);
            }
            SocketUtils.send(mfi);
        }
    }
}
