using System;

namespace MCZall
{
    public class CmdMove : Command
    {
        public override string name { get { return "move"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "other"; } }
        public CmdMove() { }
        public override void Use(Player p, string message) {
            if (message.Split(' ').Length < 2 || message.Split(' ').Length > 4) { Help(p); return; }
            int pos = message.IndexOf(' '); Player who = Player.Find(message.Substring(0, pos));
            if (who == null) { who = p; }
            
            if (who.name != p.name && !(Server.operators.Contains(p.name) || Server.superOps.Contains(p.name))) { p.SendMessage("You cannot move other players."); return; }

            if (message.Split(' ').Length == 2) { //Change level
                string storedLevel = who.level.name;
                string newMap = message.Substring(pos + 1);
                Command.all.Find("goto").Use(who, newMap);
                if (who.level.name != storedLevel) { p.SendMessage(who.name + " was moved to " + newMap); } else { p.SendMessage(newMap + " is not loaded."); }
                return;
            }

            if (message.Split(' ').Length == 3) { pos = -1; }
            try {
                int pos2 = message.IndexOf(' ', pos + 1); int pos3 = message.IndexOf(' ', pos2 + 1);
                ushort x = System.Convert.ToUInt16(message.Substring(pos + 1, pos2 - pos));
                ushort y = System.Convert.ToUInt16(message.Substring(pos2 + 1, pos3 - pos2));
                ushort z = System.Convert.ToUInt16(message.Substring(pos3));
                pos = (int)x * 32 + 16; x = (ushort)pos;
                pos = (int)y * 32 + 32; y = (ushort)pos;
                pos = (int)z * 32 + 16; z = (ushort)pos;
                unchecked { who.SendPos((byte)-1, x, y, z, p.rot[0], p.rot[1]); }
            } catch { p.SendMessage("Invalid co-ordinates"); return; }
        }
        public override void Help(Player p) {
            p.SendMessage("/move <player> <map> <x> <y> <z> - Move <player>");
            p.SendMessage("<map> must be blank if x, y or z is used and vice versa");
        }
    }
}