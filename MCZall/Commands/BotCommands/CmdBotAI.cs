using System;
using System.IO;

namespace MCZall {
    public class CmdBotAI : Command {
        public override string name { get { return "botai"; } }
        public override string shortcut { get { return "bai"; } }
        public override string type { get { return "other"; } }
        public CmdBotAI() { }
        public override void Use(Player p, string message) {
            if (message.Split(' ').Length < 2) { Help(p); return; }

            string foundPath = message.Split(' ')[1].ToLower();

            if (!Player.ValidName(foundPath)) { p.SendMessage("Invalid AI name!"); return; }
            if (foundPath == "hunt" || foundPath == "kill") { p.SendMessage("Reserved for special AI."); return; }

            try {
                switch (message.Split(' ')[0]) {
                    case "add":
                        if (message.Split(' ').Length == 2) addPoint(p, foundPath);
                        else if (message.Split(' ').Length == 3) addPoint(p, foundPath, message.Split(' ')[2]);
                        else if (message.Split(' ').Length == 4) addPoint(p, foundPath, message.Split(' ')[2], message.Split(' ')[3]);
                        else addPoint(p, foundPath, message.Split(' ')[2], message.Split(' ')[3], message.Split(' ')[4]);
                        break;
                    case "del":
                        if (!Directory.Exists("bots/deleted")) Directory.CreateDirectory("bots/deleted");

                        int currentTry = 0;
                        if (File.Exists("bots/" + foundPath)) {
            retry:          try {
                                if (message.Split(' ').Length == 2) { 
                                    if (currentTry == 0)
                                        File.Move("bots/" + foundPath, "bots/deleted/" + foundPath);
                                    else
                                        File.Move("bots/" + foundPath, "bots/deleted/" + foundPath + currentTry);
                                } else {
                                    if (message.Split(' ')[2].ToLower() == "last") {
                                        string[] Lines = File.ReadAllLines("bots/" + foundPath);
                                        string[] outLines = new string[Lines.Length - 1];
                                        for (int i = 0; i < Lines.Length - 1; i++) {
                                            outLines[i] = Lines[i];
                                        }

                                        File.WriteAllLines("bots/" + foundPath, outLines);
                                        p.SendMessage("Deleted the last waypoint from " + foundPath);
                                        return;
                                    } else {
                                        Help(p); return;
                                    }
                                }
                            } catch (IOException) { currentTry++; goto retry; }
                            p.SendMessage("Deleted &b" + foundPath);
                        } else {
                            p.SendMessage("Could not find specified AI.");
                        }
                        break;
                    default: Help(p); return;
                }
            } catch (Exception e) { Server.ErrorLog(e); }
        }
        public override void Help(Player p) {
            p.SendMessage("/botai <add/del> [AI name] <extra> - Adds or deletes [AI name]");
            p.SendMessage("Extras: walk, teleport, wait, nod, speed, spin, reset, remove, reverse, linkscript, jump");
            p.SendMessage("wait, nod and spin can have an extra '0.1 seconds' parameter");
            p.SendMessage("nod and spin can also take a 'third' speed parameter");
            p.SendMessage("speed sets a percentage of normal speed");
            p.SendMessage("linkscript takes a script name as parameter");
        }

        public void addPoint(Player p, string foundPath, string additional = "", string extra = "10", string more = "2") {
            string[] allLines;
            try { allLines = File.ReadAllLines("bots/" + foundPath); } catch { allLines = new string[1]; }

            StreamWriter SW;
            try {
                if (!File.Exists("bots/" + foundPath)) {
                    p.SendMessage("Created new bot AI: &b" + foundPath);
                    SW = new StreamWriter(File.Create("bots/" + foundPath));
                    SW.WriteLine("#Version 2");
                } else if (allLines[0] != "#Version 2") {
                    p.SendMessage("File found is out-of-date. Overwriting");
                    SW = new StreamWriter(File.Create("bots/" + foundPath));
                    SW.WriteLine("#Version 2");
                } else {
                    p.SendMessage("Appended to bot AI: &b" + foundPath);
                    SW = new StreamWriter("bots/" + foundPath, true);
                }
            } catch { p.SendMessage("An error occurred when accessing the files. You may need to delete it."); return; }
            
            try {
                switch (additional.ToLower()) {
                    case "": case "walk":
                        SW.WriteLine("walk " + p.pos[0] + " " + p.pos[1] + " " + p.pos[2] + " " + p.rot[0] + " " + p.rot[1]);
                        break;
                    case "teleport": case "tp":
                        SW.WriteLine("teleport " + p.pos[0] + " " + p.pos[1] + " " + p.pos[2] + " " + p.rot[0] + " " + p.rot[1]);
                        break;
                    case "wait":
                        SW.WriteLine("wait " + int.Parse(extra)); break;
                    case "nod":
                        SW.WriteLine("nod " + int.Parse(extra) + " " + int.Parse(more)); break;
                    case "speed":
                        SW.WriteLine("speed " + int.Parse(extra)); break;
                    case "remove":
                        SW.WriteLine("remove"); break;
                    case "reset":
                        SW.WriteLine("reset"); break;
                    case "spin":
                        SW.WriteLine("spin " + int.Parse(extra) + " " + int.Parse(more)); break;
                    case "reverse":
                        for (int i = allLines.Length - 1; i > 0; i--) if (allLines[i][0] != '#' && allLines[i] != "") SW.WriteLine(allLines[i]);
                        break;
                    case "linkscript":
                        if (extra != "10") SW.WriteLine("linkscript " + extra); else p.SendMessage("Linkscript requires a script as a parameter");
                        break;
                    case "jump":
                        SW.WriteLine("jump"); break;
                    default:
                        p.SendMessage("Could not find \"" + additional + "\""); break;
                }

                SW.Flush();
                SW.Close();
                SW.Dispose();
            } catch { p.SendMessage("Invalid parameter"); SW.Close(); }
        }
    }
}