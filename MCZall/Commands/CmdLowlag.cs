﻿using System;
using System.IO;
using System.Collections.Generic;

namespace MCZall
{
    public class CmdLowlag : Command
    {
        public override string name { get { return "lowlag"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public CmdLowlag() { }
        public override void Use(Player p, string message) { 
            if (message != "") { Help(p); return; }

            if (Server.updateTimer.Interval > 1000) {
                Server.updateTimer.Interval = 100;
                Player.GlobalChat(null, "&dLow lag " + Server.DefaultColor + "mode was turned &cOFF" + Server.DefaultColor + ".", false);
            } else {
                Server.updateTimer.Interval = 10000;
                Player.GlobalChat(null, "&dLow lag " + Server.DefaultColor + "mode was turned &aON" + Server.DefaultColor + ".", false);
            }
        }
        public override void Help(Player p) {
            p.SendMessage("/lowlag - Turns lowlag mode on or off");
        }
    }
}