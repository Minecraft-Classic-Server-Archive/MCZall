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
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace MCZall
{
    public class CmdServerReport : Command
    {
        public CmdServerReport() { }
        public override string name { get { return "serverreport"; } }
        public override string shortcut { get { return ""; } }
        public override string type { get { return "information"; } }
        public override void Use(Player p, string message) {
            TimeSpan tp =  Process.GetCurrentProcess().TotalProcessorTime;
            TimeSpan up = (DateTime.Now - Process.GetCurrentProcess().StartTime);
            
            //To get actual CPU% is OS dependant
            string ProcessorUsage = "CPU Usage (Processes : All Processes):" + Server.ProcessCounter.NextValue() + " : " + Server.PCCounter.NextValue();
            //Alternative Average?
            //string ProcessorUsage = "CPU Usage is Not Implemented: So here is ProcessUsageTime/ProcessTotalTime:"+String.Format("00.00",(((tp.Ticks/up.Ticks))*100))+"%";
            //reports Private Bytes because it is what the process has reserved for itself and is unsharable
            string MemoryUsage = "Memory Usage: "+Math.Round((double)Process.GetCurrentProcess().PrivateMemorySize64/1048576).ToString()+" Megabytes";
            string Uptime =  "Uptime: "+up.Days+" Days "+up.Hours+" Hours "+up.Minutes+" Minutes "+up.Seconds+" Seconds";
            string Threads = "Threads: " + Process.GetCurrentProcess().Threads.Count;
            p.SendMessage(Uptime);
            p.SendMessage(MemoryUsage);
            p.SendMessage(ProcessorUsage);
            p.SendMessage(Threads);
            
        }
        public override void Help(Player p)
        {
            p.SendMessage("/serverreport - Get server CPU%, RAM usage, and uptime.");
        }
    }
}
