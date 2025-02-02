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
using System.IO;

namespace MCZall {
	public class CmdSetspawn : Command {
        public override string name { get { return "setspawn"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
		public CmdSetspawn() {  }
		public override void Use(Player p,string message)  {
			if (message != "") { Help(p); return; }
			p.SendMessage("Spawn location changed.");
			p.level.spawnx = (ushort)(p.pos[0]/32);
            p.level.spawny = (ushort)(p.pos[1] / 32);
            p.level.spawnz = (ushort)(p.pos[2] / 32);
            p.level.rotx = p.rot[0];
            p.level.roty = 0;
		} public override void Help(Player p)  {
			p.SendMessage("/setspawn - Set the default spawn location.");
		}
	}
}