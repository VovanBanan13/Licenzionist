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
    public partial class FormAPC : Form
    {
        public FormAPC()
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

        private void FormAPC_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataBaseDataSet.User' table. You can move, or remove it, as needed.
            this.userTableAdapter.Fill(this.dataBaseDataSet.User);

        }

        private void Add_PC(string OS, string CPU, string GPU, string RAM, string ROM, int User_id)
        {
            string CommandText;
            int ID;
            if (label7.Text != "")
            {
                ID = Convert.ToInt32(label7.Text);
                CommandText = "UPDATE [PC] SET "
                + "[PC].[OS] = '" + OS + "', [PC].[CPU] = '" + CPU + "', [PC].[GPU] = '" + GPU + "', " +
             "[PC].[RAM] = '" + RAM + "', [PC].[ROM] = '" + ROM + "', [PC].[User_id] = '" + User_id + "' WHERE [PC].[id_PC] = " + ID;
                My_Execute_Non_Query(CommandText);
                this.Close();
            }
            else
                if (label7.Text == "")
            {
                CommandText = "INSERT INTO [PC] ([OS], [CPU], [GPU], [RAM], [ROM], [User_id]) "
                + "VALUES ('" + OS + "', '" + CPU + "', '" + GPU + "', '" + RAM + "', '" + ROM + "', '" + User_id + "')";
                My_Execute_Non_Query(CommandText);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_PC(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, Convert.ToInt32(comboBox1.SelectedValue));
            this.Refresh();
            V.Refresh();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
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
