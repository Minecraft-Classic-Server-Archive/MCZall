/*
	Copyright 2010 MCZall Team Licensed under the
	Educational Community License, Version 2.0 (the "License"); you may
	not use this file except in compliance with the License. You may
	obtain a copy of the License at
	
	http://www.osedu.org/licenses/ECL-2.0
	
	Unless required by applicable law or agreed to in writing,
	software distributed under the License is distributed on an "AS IS"
	BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express
	or implied. See the License for the specific language governing
	permissions and limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MCZall
{
    class CmdRules : Command
    {
        public override string name { get { return "rules"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "information"; } }

        public override void Use(Player p, string message)
        {
            List<string> rules = new List<string>();
            if (!File.Exists("text/rules.txt")) {
                File.WriteAllText("text/rules.txt", "No rules entered yet!");
            }
            StreamReader r = File.OpenText("text/rules.txt");
            while (!r.EndOfStream)
                rules.Add(r.ReadLine());

            r.Close();
            r.Dispose();

            Player who = null;
            if (message != "") {
                if (p.group == Group.Find("guest") | p.group == Group.Find("banned"))
                { p.SendMessage("You cant send /rules to another player!"); return; }
                who = Player.Find(message);
            } else {
                who = p;
            }

            if (who != null) {
                if (who.level == Server.mainLevel && Server.mainLevel.permissionbuild == LevelPermission.Guest) { who.SendMessage("You are currently on the guest map where anyone can build"); }
                who.SendMessage("Server Rules:");
                foreach (string s in rules)
                    who.SendMessage(s);
            } else {
                p.SendMessage("There is no player \"" + message + "\"!");
            }
        }

        public override void Help(Player p) {
            if (p.group.name != "operator" && p.group.name != "superop") {
                p.SendMessage("/rules - Displays server rules");
            } else {
                p.SendMessage("/rules [player]- Displays server rules to a player");
            }
        }
    }
}
