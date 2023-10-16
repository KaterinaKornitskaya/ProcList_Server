using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace server
{
    public partial class Form1 : Form
    {
        MyList myList;
        MyConnection conn;
        public Form1()
        {
            InitializeComponent();
            
            myList = new MyList();
            conn = new MyConnection();
        }

        private void button_ShowList(object sender, EventArgs e)
        {
            listBox1.DataSource = myList.CreateList().ToList();
        }

        private void button_ConnectWithCl(object sender, EventArgs e)
        {
            conn.ConnectWithClient();
        }
    }
}
