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

namespace MCZall
{
    class CmdPlayers : Command
    {

        public override string name { get { return "players"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "information"; } }
        public override void Use(Player p, string message)
        {
            string superops = "";
            string ops = "";
            string advbuilders = "";
            string builders = "";
            string guests = "";
            string banned = "";
            int totalPlayers = 0;
            foreach (Player pl in Player.players)
            {
                if (!pl.hidden || p.group.Permission > LevelPermission.AdvBuilder)
                {
                    totalPlayers++;
                    string foundName = pl.name;

                    if (Server.afkset.Contains(pl.name)) {
                        foundName = pl.name + "-afk";
                    }

                    switch (pl.group.name.ToLower()) {
                        case "superop":
                            superops += " " + foundName + " (" + pl.level.name + ")" + ",";
                            break;
                        case "operator":
                            ops += " " + foundName + " (" + pl.level.name + ")" + ",";
                            break;
                        case "advbuilder":
                            advbuilders += " " + foundName + " (" + pl.level.name + ")" + ",";
                            break;
                        case "builder":
                            builders += " " + foundName + " (" + pl.level.name + ")" + ",";
                            break;
                        case "guest":
                            guests += " " + foundName + " (" + pl.level.name + ")" + ",";
                            break;
                        case "banned":
                            banned += " " + foundName + " (" + pl.level.name + ")" + ",";
                            break;
                    }
                }
            }
            p.SendMessage("There are " + totalPlayers + " players online.");
            p.SendMessage(":" + Group.Find("superop").color + "SuperOPs:" + Server.DefaultColor + superops.Trim(','));
            p.SendMessage(":" + Group.Find("op").color + "OPs:" + Server.DefaultColor + ops.Trim(','));
            p.SendMessage(":" + Group.Find("adv").color + "AdvBuilders:" + Server.DefaultColor + advbuilders.Trim(',')); ;
            p.SendMessage(":" + Group.Find("builder").color + "Builders:" + Server.DefaultColor + builders.Trim(',')); ;
            p.SendMessage(":" + Group.Find("guest").color + "Guests:" + Server.DefaultColor + guests.Trim(','));
            p.SendMessage(":" + Group.Find("banned").color + "Banned:" + Server.DefaultColor + banned.Trim(','));
        }

        public override void Help(Player p)
        {
            p.SendMessage("/players - Shows name and general rank of all players");
        }
    }
}
