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

namespace MCZall
{
    public class CmdKickban : Command
    {
        public override string name { get { return "kickban"; } }
        public override string shortcut { get { return "kb"; } }
        public override string type { get { return "mod"; } }
        public CmdKickban() { }
        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            string who = message.Split(' ')[0];

            Command.all.Find("ban").Use(p, message);
            Command.all.Find("kick").Use(p, message);
        }
        public override void Help(Player p)
        {
            p.SendMessage("/kickban <player> [message] - Kicks and bans a player with an optional message.");
        }
    }
}