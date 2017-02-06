using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace MarkCheck
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }



        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
                
            //    if (textBox1.Text.Length > 50)
            //    {
            //        string numberStr = getNumber(textBox1.Text);
            //        int number = Int32.Parse(getNumber(textBox1.Text));
            //        string oldNumStr = "";
            //        int oldNumber = 0;
            //        if (listBox1.Items.Count > 0)
            //        {
            //            oldNumStr = getNumber(listBox1.Items[(listBox1.Items.Count - 1)].ToString());
            //            oldNumber = Int32.Parse(getNumber((listBox1.Items[(listBox1.Items.Count - 1)].ToString())));
            //        }

            //        if (number != oldNumber)
            //        {
            //            listBox1.Items.Add(textBox1.Text);

            //            //listBox1.Items.Add(getNumber(number));
            //            listBox1.Refresh();

            //            if (radioButton1.Checked)
            //            {
            //                if (oldNumber != 0 && (number != oldNumber + 1))
            //                {
            //                    printAlert(oldNumStr, numberStr);

            //                }
            //            }
            //            else
            //            {
            //                if (oldNumber != 0 && (number != oldNumber - 1))
            //                {
            //                    printAlert(oldNumStr, numberStr);

            //                }
            //            }
            //        }



            //    }
            //    else if(checkBox1.Checked)
            //    {
            //        listBox1.Items.Add(textBox1.Text);
            //    }

            //    if (listBox1.Items.Count>2)
            //    {
            //       listBox1.SelectedIndex = listBox1.Items.Count - 1;
            //       listBox1.SelectedIndex = -1;
            //    }
            //    if (listBox2.Items.Count>2)
            //    {
            //      listBox2.SelectedIndex = listBox2.Items.Count - 1;
            //      listBox2.SelectedIndex = -1;
            //    }
                
            //    textBox1.SelectAll();
            //    label2.Text = listBox1.Items.Count.ToString();
            //}
        }

               
           
        

        public void printAlert(String s1, String s2)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(s1);
            sb.Append(" -- ");
            sb.Append(s2);
            listBox2.Items.Add(sb.ToString());

            //----------------------------------
            //int i1 = Int32.Parse(s1);
            //int i2 = Int32.Parse(s2);
            //for (int i = i1+1; i < i2; i++)
            //{

            //listBox2.Items.Add(i.ToString());
            //}

            listBox2.Refresh();
        }

        public string getNumber(String fullNumber)
        {
            return fullNumber.Substring(31, 6);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            if (button1.Text.Equals("Старт"))
            {
               //textBox1.Enabled = true;
               // textBox1.Focus();
               //textBox1.SelectAll();
               button1.Text = "Стоп";
                button3.Enabled = false;
                string pname = comboBox1.SelectedItem.ToString();
                serialPort1.PortName = pname;
                serialPort1.Open();
            }
            else
            {
                //textBox1.Text = "";
                //textBox1.Enabled = false;
                button1.Text = "Старт";
                button3.Enabled = true;
                serialPort1.Close();

            }
            

        }

       

        private void button2_Click(object sender, EventArgs e)
        {

            string curDate = DateTime.Now.Date.ToString().Substring(0,10).Replace(".","_");
            string curTimeShort = DateTime.Now.ToShortTimeString().Replace(":","_");
            string fileName = "d:\\"+curDate+"_"+curTimeShort+".txt";

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName))
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                    sw.WriteLine(listBox1.Items[i].ToString());
            }
            MessageBox.Show("Файл "+fileName+" создан","Успех", MessageBoxButtons.OK);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            label2.Text = "";
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            {
                this.Invoke(new EventHandler(DoUpdate));
            }
        }

        private void DoUpdate(object s, EventArgs e)
        {
           // String str = serialPort1.ReadLine();
            String str = serialPort1.ReadExisting();
            //listBox1.Items.Add(str);
            // textBox1.Text = str;
            // textBox1.Focus();
            // SendKeys.Send("{ENTER}");

            //textBox1_KeyPress(object sender, KeyPressEventArgs e);

            //textBox1.Text = serialPort1.ReadLine();
           // MessageBox.Show(str);
           // MessageBox.Show(str.Length.ToString());

            if (str.Length >= 68)
            {
                for (int i = 0; i < str.Length; i += 68)
                {
                    String temp1 = str.Substring(i, 67);
                   
                    string numberStr =getNumber(temp1);
                    int number = Int32.Parse(getNumber(temp1));
                    string oldNumStr = "";
                    int oldNumber = 0;
                    if (listBox1.Items.Count > 0)
                    {
                        oldNumStr = getNumber(listBox1.Items[(listBox1.Items.Count - 1)].ToString());
                        oldNumber = Int32.Parse(getNumber((listBox1.Items[(listBox1.Items.Count - 1)].ToString())));
                    }

                    if (number != oldNumber)
                    {
                        listBox1.Items.Add(temp1);

                        //listBox1.Items.Add(getNumber(number));
                        listBox1.Refresh();

                        if (radioButton1.Checked)
                        {
                            if (oldNumber != 0 && (number != oldNumber + 1))
                            {
                                printAlert(oldNumStr, numberStr);

                            }
                        }
                        else
                        {
                            if (oldNumber != 0 && (number != oldNumber - 1))
                            {
                                printAlert(oldNumStr, numberStr);

                            }
                        }
                    }

                }
            }
            else if (checkBox1.Checked)
            {
                listBox1.Items.Add(str);
                listBox1.Items.Add(str.Length);
            }
            if (listBox1.Items.Count > 2)
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.SelectedIndex = -1;
            }
            if (listBox2.Items.Count > 2)
            {
                listBox2.SelectedIndex = listBox2.Items.Count - 1;
                listBox2.SelectedIndex = -1;
            }

           
            label2.Text = listBox1.Items.Count.ToString();
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            comboBox1.Items.AddRange(ports);
            
            
        }
    }
}

    
