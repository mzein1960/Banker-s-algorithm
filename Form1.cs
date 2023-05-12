using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp7
{
    public partial class Form1 : Form
    {
        struct process
        {
            public int[] allocation;
            public int[] max;
            public int[] need;
            public int status;


        }

        int n, r;
        int flag = 0,i=0;
        int unsafestate = 0;
        int[] Seq = new int[10];

        process[] p = new process[10];


        int[] x = new int[10];
        int[] avail = new int[10];



        private DataTable table;
        private DataTable table2;
        

        public Form1()
        {
            InitializeComponent();

            table = new DataTable();
            table2 = new DataTable();

            table.Columns.Add("PROCESS", typeof(string));
            table.Columns.Add("ALLOCATION", typeof(string));
            table.Columns.Add("MAX", typeof(string));
            table.Columns.Add("NEED", typeof(string));
            
            
            dataGridView1.DataSource = table;

            label3.Hide();
            label5.Hide();
            label6.Hide();
            label9.Hide();
            label10.Hide();

            panel5.Hide();
            panel6.Hide();
            panel7.Hide();
            panel8.Hide();

            textBox3.Hide();
            textBox4.Hide();

            button3.Hide();

            
            dataGridView2.Hide();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            table2.Columns.Add("PROCESS", typeof(string));
            table2.Columns.Add("AVAILABLE", typeof(string));
            dataGridView2.DataSource = table2;



            int[] need = new int[3];
            string available = "";
            if (i == n && i!=0)
            {
                for (int i = 0; i < n; i++)
                {
                    flag = 0;
                    if (p[i].status == 0)
                    {
                        for (int j = 0; j < r; j++)
                        {
                            if (p[i].need[j] <= avail[j])
                            {
                                flag = 1;
                            }
                            else
                            {
                                flag = 0;
                                break;
                            }
                        }
                        if (flag == 1)
                        {
                            
                            for (int j = 0; j < r; j++)
                            {
                                avail[j] = avail[j] + p[i].allocation[j];
                                p[i].status = 1;
                                
                                

                            }
                            foreach (int num in avail)
                            {
                                available += num.ToString() + " ";
                            }
                            table2.Rows.Add($"p{i}", available);
                            available = "";
                            Seq[unsafestate] = i;
                            i = -1;
                            unsafestate++;
                            

                        }


                    }
                }
                if (unsafestate == n)
                {
                    MessageBox.Show("SYSTEM IN SAFE STATE ", "Information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label10.Show();
                    label9.Text = "";
                    for (int i = 0; i < n; i++)
                    {
                        label9.Text+=$"p{Seq[i] } " ;
                    }
                    label9.Show();
                    dataGridView2.Show();
                }
                else
                {
                    MessageBox.Show("SYSTEM IN UNSAFE STATE", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Missing Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

      

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Missing Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {


                n = int.Parse(textBox1.Text);
                r = int.Parse(textBox2.Text);
                
                
                string input = textBox5.Text;  // Get the text box input as a string
                string[] tokens = input.Split();  // Split the string by whitespace
                avail= tokens.Select(int.Parse).ToArray();
                

              /*  textBox1.Clear();
                textBox2.Clear();
                textBox5.Clear();*/

                label3.Show();
                label5.Show();
                label6.Show();

                panel5.Show();
                panel6.Show();
                panel7.Show();
                panel8.Show();

                textBox3.Show();
                textBox4.Show();

                button3.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string need="";
            
            if (textBox3.Text != "" || textBox4.Text != "" )
            {
                if (i < n)
                {
                    p[i].allocation = new int[3];
                    p[i].max = new int[3];
                    p[i].need = new int[3];

                    string input = textBox3.Text;  // Get the text box input as a string
                    string[] tokens = input.Split();  // Split the string by whitespace
                    p[i].allocation = tokens.Select(int.Parse).ToArray();

                    input = textBox4.Text;  // Get the text box input as a string
                    tokens = input.Split();  // Split the string by whitespace
                    p[i].max = tokens.Select(int.Parse).ToArray();

                    for (int j = 0; j < r; j++)
                    {
                        p[i].need[j] = p[i].max[j] - p[i].allocation[j];
                    }

                    foreach (int num in p[i].need)
                    {
                        need += num.ToString() + " ";
                    }
                    table.Rows.Add($"p{i}", textBox3.Text, textBox4.Text, need);

                    p[i].status = 0;
                    i++;
                    textBox3.Clear();
                    textBox4.Clear();
                }
                else
                {
                    MessageBox.Show("CANNOT ADD MORE", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Missing Information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            
         


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

       
    }
}
