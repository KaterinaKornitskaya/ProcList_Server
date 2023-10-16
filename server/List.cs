using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace server
{
    internal class MyList
    {
        public Dictionary<int, string> dict { get; set; }   
        public Process[] pr = Process.GetProcesses();
       
        public MyList() { }
        public Dictionary<int, string> CreateList()
        {
            dict = new Dictionary<int, string>();
            int n = 1;
            try
            {
                foreach (var item in pr)
                {
                    dict.Add(n, item.ProcessName.ToString());
                    n++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dict;
        }

        public List<string> FromDictToList()
        {
            CreateList();
            List<string> list = new List<string>();
            foreach(var item in CreateList())
            {
                list.Add(item.Value.ToString());
            }
            return list;
        }

        public Dictionary<int, string> KillProc(int n)
        {
            dict = new Dictionary<int, string>();
            Process[] pr = Process.GetProcesses();
            pr[n].Kill();
            Process[] proc = Process.GetProcesses();
            int x = 1;
            foreach (var item in proc)
            {
                dict.Add(x, item.ProcessName.ToString());
                x++;
            }
            return dict;
        }


    }
}
