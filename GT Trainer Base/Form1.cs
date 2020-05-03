using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Memory;

namespace GT_Trainer_Base
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        //Starts a new Memory session
        public Mem Memory = new Mem();

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //infinite loop
            while(true)
            {
            //gettting growtopias process id (PID)
            int PID = Memory.GetProcIdFromName("Growtopia");
            //boolean to check if the process is attached
            bool isAttached = false;
            //if the process exists then attach to the process
            if (PID > 0)
            {
                isAttached = Memory.OpenProcess(PID);
            }
                //checks if the process is attached
                if (isAttached == true)
                {
                    if (checkBox1.Checked)
                    {
                        //code to enable banbypass
                        Memory.WriteMemory("base+0x1F4983", "bytes", "74 08");
                    }
                    else
                    {
                        //code to disable banbypass
                        Memory.WriteMemory("base+0x1F4983", "bytes", "75 08");
                    }
                    if (checkBox2.Checked)
                    {
                        //code to enable modfly
                        Memory.WriteMemory("base+0x32DEE7", "bytes", "90 90 90 90 90 90");
                    }
                    else
                    {
                        //code to disable modfly
                        Memory.WriteMemory("base+0x32DEE7", "bytes", "0F 84 95 00 00 00");
                    }
                    if (checkBox3.Checked)
                    {
                        //code to enable antibounce
                        Memory.WriteMemory("base+0x32E5B3", "bytes", "90 90");
                    }
                    else
                    {
                        //code to disable antibounce
                        Memory.WriteMemory("base+0x32E5B3", "bytes", "75 10");
                    }
                    if (checkBox4.Checked)
                    {
                        //code to enable growz
                        Memory.WriteMemory("base+0x2D7ADE", "bytes", "90 90 90 90");
                    }
                    else
                    {
                        //code to disable growz
                        Memory.WriteMemory("base+0x2D7ADE", "bytes", "F3 0F 5C D1");
                    }
                }
                //sleep for 25 milliseconds to lower cpu usage
                Thread.Sleep(25);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //if the backgroundworker is not busy
            if (!backgroundWorker1.IsBusy)
            {
                //then run it
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //change pants pointer value to whatever value the textbox is in. An example for using pointers.
            Memory.WriteMemory("base+0x005766E0,0x8,0x1738,0x1F0,0x80,0x1E0,0x8,0x360", "int", textBox1.Text);
        }
    }
}
