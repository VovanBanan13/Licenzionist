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
    public partial class FormAPO : Form
    {
        public FormAPO()
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

        private void FormAPO_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataBaseDataSet.Proizvod' table. You can move, or remove it, as needed.
            this.proizvodTableAdapter.Fill(this.dataBaseDataSet.Proizvod);

        }

        private void Add_PO(string PO_name, string info, int Prod_id)
        {
            int ID;
            string CommandText;
            if (label4.Text != "")
            {
                ID = Convert.ToInt32(label4.Text);
                CommandText = "UPDATE [PO] SET "
                + "[PO].[PO_name] = '" + PO_name + "', [PO].[Info] = '" + info + "', " +
             "[PO].[Prod_id] = '" + Prod_id + "' WHERE [PO].[id_PO] = " + ID;
                My_Execute_Non_Query(CommandText);
                this.Close();
            }
            else
                if (label4.Text == "")
            {
                CommandText = "INSERT INTO [PO] ([PO_name], [Info], [Prod_id]) "
                + "VALUES ('" + PO_name + "', '" + info + "', '" + Prod_id + "')";
                My_Execute_Non_Query(CommandText);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Refresh();
            this.Close();
            Refresh();
            V.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_PO(textBox1.Text, textBox2.Text, Convert.ToInt32(comboBox1.SelectedValue));
            this.Refresh();
            V.Refresh();
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
