using DotNetTranstor;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPFLauncher.Code;
using WPFLauncher.Network.Launcher;

namespace xmaoyeHook
{
    public class Class1
    {
        public static int Start(string mas)
        {
            
            
            MethodHook.Install(null);
            
            
            return 0;
        }
        
        
    }
}
