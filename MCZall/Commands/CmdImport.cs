using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Compression;

namespace MCZall
{
    public class CmdImport : Command
    {
        public override string name { get { return "import"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "mod"; } }
        public CmdImport() { }
        public override void Use(Player p, string message) {
        }
        public override void Help(Player p) {
            p.SendMessage("/import [mapname] - Imports a .dat map.");
        }
    }
}