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
    public partial class FormAU : Form
    {
        public FormAU()
        {
            InitializeComponent();
        }
        FormMain M = new FormMain();
        public void My_Execute_Non_Query(string CommandText)
        {
            OleDbConnection conn = new OleDbConnection(M.ConnectionString);
            conn.Open();
            OleDbCommand myCommand = conn.CreateCommand();
            myCommand.CommandText = CommandText;
            myCommand.ExecuteNonQuery();
            conn.Close();
        }
        private void Add_User(string f_name, string i_name, string o_name, string tel, string email, string date)
        {
            FormView V = new FormView();
            string CommandText;
            string birthday;
            int ID;
            birthday = Convert.ToString(date); // переводим время в строку
            birthday = birthday.Substring(0, birthday.LastIndexOf('.') + 5);  // удаляем время, оставляем только дату

            if (label7.Text != "")
            {
                ID = Convert.ToInt32(label7.Text);
                CommandText = "UPDATE [User] SET "
                + "[User].[F_name] = '" + f_name + "', [User].[I_name] = '" + i_name + "', [User].[O_name] = '" + o_name + "', " +
             "[User].[Tel] = '" + tel + "', [User].[E-mail] = '" + email + "', [User].[BirthDay] = '" + birthday + "' WHERE [User].[id_User] = " + ID;
                My_Execute_Non_Query(CommandText);
                this.Close();
            }
            else
            if (label7.Text == "")
            {
                CommandText = "INSERT INTO [User] ([F_name], [I_name], [O_name], [Tel], [E-mail], [BirthDay]) "
                + "VALUES ('" + f_name + "', '" + i_name + "', '" + o_name + "', '" +
             tel + "', '" + email + "', '" + birthday + "')";
            My_Execute_Non_Query(CommandText);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.OK;
            Add_User(textBox1.Text, textBox2.Text, textBox3.Text,
                    textBox4.Text, textBox5.Text, dateTimePicker1.Text);
            this.Refresh();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            dateTimePicker1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
