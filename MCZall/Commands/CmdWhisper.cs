using System;

namespace MCZall {
	public class CmdWhisper : Command {
        public override string name { get { return "whisper"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public CmdWhisper() { }
		public override void Use(Player p,string message)  {
            if (message == "") {
                p.whisper = !p.whisper; p.whisperTo = ""; 
                if (p.whisper) p.SendMessage("All messages sent will now auto-whisper");
                else p.SendMessage("Whisper chat turned off");
            } else {
                Player who = Player.Find(message);
                if (who == null) { p.whisperTo = ""; p.whisper = false; p.SendMessage("Could not find player."); return; }

                p.whisper = true;
                p.whisperTo = who.name;
            }


		} public override void Help(Player p)  {
			p.SendMessage("/whisper <name> - Makes all messages act like whispers");
		}
	}
}