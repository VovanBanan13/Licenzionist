using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Main
{
    public partial class FormAPr : Form
    {
        public FormAPr()
        {
            InitializeComponent();
        }

        FormMain M = new FormMain();
        FormView V = new FormView();
        public void My_Execute_Non_Query(string CommandText)
        {
            OleDbConnection conn = new OleDbConnection(M.ConnectionString);
            conn.Open();
            OleDbCommand myCommand = conn.CreateCommand();
            myCommand.CommandText = CommandText;
            myCommand.ExecuteNonQuery();
            conn.Close();
        }

        private void Add_Prod(string P_name, string info)
        {
            string CommandText;
            int ID;
            if (label3.Text != "")
            {
                ID = Convert.ToInt32(label3.Text);
                CommandText = "UPDATE [Proizvod] SET "
                + "[Proizvod].[Prod_name] = '" + P_name + "', [Proizvod].[Info] = '" + info + 
             "' WHERE [Proizvod].[id_Prod] = " + ID;
                My_Execute_Non_Query(CommandText);
                this.Close();
            }
            else
                if (label3.Text == "")
            {
                CommandText = "INSERT INTO [Proizvod] ([Prod_name], [Info]) "
                + "VALUES ('" + P_name + "', '" + info + "')";
                My_Execute_Non_Query(CommandText);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Prod(textBox1.Text, textBox2.Text);
            this.Refresh();
            V.Refresh();
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Refresh();
            this.Close();
            Refresh();
            V.Refresh();
        }
    }
}
