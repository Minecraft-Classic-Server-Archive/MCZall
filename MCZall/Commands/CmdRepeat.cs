using System;

namespace MCZall {
	public class CmdRepeat : Command {
        public override string name { get { return "repeat"; } }
        public override string shortcut { get { return "m"; } }
        public override string type { get { return "other"; } }
        public CmdRepeat() { }
		public override void Use(Player p,string message)  {
            if (p.lastCMD == "") { p.SendMessage("No commands used yet."); return; }

            p.SendMessage("Using &b/" + p.lastCMD);

            if (p.lastCMD.IndexOf(' ') == -1) {
                Command.all.Find(p.lastCMD).Use(p, "");
            } else {
                Command.all.Find(p.lastCMD.Substring(0, p.lastCMD.IndexOf(' '))).Use(p, p.lastCMD.Substring(p.lastCMD.IndexOf(' ') + 1));
            }
		}
        public override void Help(Player p) {
            p.SendMessage("/repeat - Repeats the last used command");
		}
	}
}