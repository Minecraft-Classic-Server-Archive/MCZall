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

namespace MCZall {
	public class CmdHelp : Command {
        public override string name { get { return "help"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "information"; } }
		public CmdHelp() {  }
		public override void Use(Player p,string message) {
			message.ToLower();
            switch (message) {
                case "":
                    if (Server.oldHelp) {
                        goto case "old";
                    } else {
                        p.SendMessage("Use &b/help ranks" + Server.DefaultColor + " for a list of ranks.");
                        p.SendMessage("Use &b/help build" + Server.DefaultColor + " for a list of building commands.");
                        p.SendMessage("Use &b/help mod" + Server.DefaultColor + " for a list of moderation commands.");
                        p.SendMessage("Use &b/help information" + Server.DefaultColor + " for a list of information commands.");
                        p.SendMessage("Use &b/help other" + Server.DefaultColor + " for a list of other commands.");
                        p.SendMessage("Use &b/help short" + Server.DefaultColor + " for a list of shortcuts.");
                        p.SendMessage("Use &b/help old" + Server.DefaultColor + " to view the Old help menu.");
                        p.SendMessage("Use &b/help [command] or /help [block] " + Server.DefaultColor + "to view more info.");
                    } break;
                case "ranks":
                    message = "";
                    Group.GroupList.ForEach(delegate(Group grp) {
                        p.SendMessage(grp.color + grp.name + Server.DefaultColor + " - &bCommand limit: " + grp.maxBlocks);
                    });
                    break;
                case "build":
                    message = "";
                    p.group.commands.All().ForEach(delegate(Command comm) {
                        if (comm.type.ToLower() == "build") message += ", " + getColor(comm.name) + comm.name;
                    });

                    if (message == "") { p.SendMessage("No commands of this type are available to you."); break; }
                    p.SendMessage("Building commands you may use:");
                    p.SendMessage(message.Remove(0, 2) + ".");
                    break;
                case "mod":
                    message = "";
                    p.group.commands.All().ForEach(delegate(Command comm) {
                        if (comm.type.ToLower() == "mod") message += ", " + getColor(comm.name) + comm.name;
                    });

                    if (message == "") { p.SendMessage("No commands of this type are available to you."); break; }
                    p.SendMessage("Moderation commands you may use:");
                    p.SendMessage(message.Remove(0, 2) + ".");
                    break;
                case "information":
                    message = "";
                    p.group.commands.All().ForEach(delegate(Command comm) {
                        if (comm.type.ToLower() == "information") message += ", " + getColor(comm.name) + comm.name;
                    });

                    if (message == "") { p.SendMessage("No commands of this type are available to you."); break; }
                    p.SendMessage("Information commands you may use:");
                    p.SendMessage(message.Remove(0, 2) + ".");
                    break;
                case "other":
                    message = "";
                    p.group.commands.All().ForEach(delegate(Command comm) {
                        if (comm.type.ToLower() != "information" && comm.type.ToLower() != "mod" && comm.type.ToLower() != "build") message += ", " + getColor(comm.name) + comm.name;
                    });

                    if (message == "") { p.SendMessage("No commands of this type are available to you."); break; }
                    p.SendMessage("Other commands you may use:");
                    p.SendMessage(message.Remove(0, 2) + ".");
                    break;
                case "short":
                    message = "";
                    p.group.commands.All().ForEach(delegate(Command comm) {
                        if (comm.shortcut != "") message += ", &b" + comm.shortcut + " " + Server.DefaultColor + "[" + comm.name + "]";
                    });
                    p.SendMessage("Available shortcuts:");
                    p.SendMessage(message.Remove(0, 2) + ".");
                    break;
                case "old":
                    p.group.commands.All().ForEach(delegate(Command comm) {
					    try { message += ", " + comm.name; } catch { }
				    });
				    p.SendMessage("Available commands:");
                    p.SendMessage(message.Remove(0,2) + ".");
                    p.SendMessage("Type \"/help <command>\" for more help.");
                    p.SendMessage("Type \"/help shortcuts\" for shortcuts.");
                    break;
                default:
                    Command cmd = Command.all.Find(message);
                    if (cmd != null) { 
                        cmd.Help(p);
                        string foundRank = Level.PermissionToName(GrpCommands.allowedCommands.Find(grpComm => grpComm.commandName == cmd.name).rank);
                        p.SendMessage("Rank needed: " + Group.Find(foundRank).color + foundRank);
                        return; 
                    }
                    byte b = Block.Byte(message);
                    if (b != Block.Zero) {
                        p.SendMessage("Block \"" + message + "\" appears as &b" + Block.Name(Block.Convert(b)));
                        p.SendMessage("Rank needed to place it: " + Group.Find(Level.PermissionToName(Block.allowPlace(b))).color + Level.PermissionToName(Block.allowPlace(b)));
                        return;
                    }
                    p.SendMessage("Could not find command or block specified.");
                    break;
            }
		} 

        private string getColor(string commName) {
            string foundColor = "";

            GrpCommands.allowedCommands.ForEach(delegate(GrpCommands.rankAllowance aV) {
                if (aV.commandName == commName) {
                    if (aV.rank == LevelPermission.Admin) foundColor = Group.Find("superop").color;
                    if (aV.rank == LevelPermission.Operator) foundColor = Group.Find("operator").color;
                    if (aV.rank == LevelPermission.AdvBuilder) foundColor = Group.Find("advbuilder").color;
                    if (aV.rank == LevelPermission.Builder) foundColor = Group.Find("builder").color;
                    if (aV.rank == LevelPermission.Guest) foundColor = Group.Find("guest").color;
                    if (aV.rank == LevelPermission.Banned) foundColor = Group.Find("banned").color;
                }
            });

            return foundColor;
        }

        public override void Help(Player p)  
        {
			p.SendMessage("...really? Wow. Just...wow.");
		}
	}
}