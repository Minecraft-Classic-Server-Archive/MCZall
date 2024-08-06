using System;

namespace MCZall {
	public class CmdOpChat : Command {
        public override string name { get { return "opchat"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public CmdOpChat() { }
		public override void Use(Player p,string message)  {
            p.opchat = !p.opchat;
            if (p.opchat) p.SendMessage("All messages will now be sent to OPs only");
            else p.SendMessage("OP chat turned off");
		} public override void Help(Player p)  {
			p.SendMessage("/opchat - Makes all messages sent go to OPs by default");
		}
	}
}