
using DotNetTranstor;
using System;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using WPFLauncher.Util;

namespace xmaoyeHook
{
    
    public class SocketUtils
    {
        
        public static bool PropetyTCP()
        {
            bool flag = SocketUtils.socket == null;
            if (flag)
            {
                Thread thread = new Thread(new ThreadStart(SocketUtils.Receive_ToServer));
                thread.Start();
            }
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect("127.0.0.1", 64521);
            return socket.Connected;
        }

        
        public static void send(string send)
        {
            
            byte[] bytes = Encoding.Default.GetBytes(send);
            bool flag = SocketUtils.PropetyTCP() && SocketUtils.socket != null;
            if (flag)
            {
                socket.Send(bytes, bytes.Length, SocketFlags.None);
                socket.Close();
            }
        }

        
        public static void Receive_ToServer()
        {
            Buffersocket.Connect("127.0.0.1", 64521);
            Thread thread = new Thread(new ThreadStart(SocketUtils.targett));
            thread.Start();
        }

        
        private static void targett()
        {
            int count = SocketUtils.Buffersocket.Receive(SocketUtils.buffers);
            string @string = Encoding.UTF8.GetString(SocketUtils.buffers, 0, count);
            string[] array = Regex.Split(@string, "&");
            if(@string == "GameStart")
            {
                if(Class2.SocketGameStart == true)
                {
                    Class2.GameStart = true;
                }
            }
            if (array[0] == "MessageBox")
            {
                uk.n(array[1]);
            }
            


                try
            {
                SocketUtils.buffers = new byte[2097152];
                Thread thread = new Thread(new ThreadStart(SocketUtils.targett));
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Source);
                throw;
            }
        }
        public static bool cookieLogin = false;

        public static Socket socket = null;

       
        public static Socket Buffersocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

       
        public static byte[] buffers = new byte[2097152];
    }
}
