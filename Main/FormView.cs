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
    public partial class FormView : Form
    {
        public string ConnectionString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = ..\\..\\DataBase.mdb";
        public string CommandText = "";
        public int act_table;   // активная таблица (1-пользователи, 2-ПО, 3-производители, 4-ПК)

        public FormView()
        {
            InitializeComponent();
        }

        // выполнение SQL-запроса для команд INSERT, UPDATE, DELETE
        public void My_Execute_Non_Query(string CommandText)
        {
            OleDbConnection conn = new OleDbConnection(ConnectionString);
            conn.Open();
            OleDbCommand myCommand = conn.CreateCommand();
            myCommand.CommandText = CommandText;
            myCommand.ExecuteNonQuery();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (act_table)
            {
                case 1:
                    {
                        FormAU U = new FormAU();
                        U.Show();
                    }
                    break;
                case 2:
                    {
                        FormAPO PO = new FormAPO();
                        PO.Show();
                    }
                    break;
                case 3:
                    {
                        FormAPr Pr = new FormAPr();
                        Pr.Show();
                    }
                    break;
                case 4:
                    {
                        FormAPC PC = new FormAPC();
                        PC.Show();
                    }
                    break;
                default:
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Refresh();
            FormMain F = new FormMain();
            switch (act_table)
            {
                case 1:
                    {
                        this.Close();
                        F.Get_User();
                        act_table = 1;
                    }
                    break;
                case 2:
                    {
                        this.Close();
                        F.Get_PO();
                        act_table = 2;
                    }
                    break;
                case 3:
                    {
                        this.Close();
                        F.Get_Proizvod();
                        act_table = 3;
                    }
                    break;
                case 4:
                    {
                        this.Close();
                        F.Get_PC();
                        act_table = 4;
                    }
                    break;
                default:
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormDelete f = new FormDelete();

            if (f.ShowDialog() == DialogResult.OK)
            {
                int row = 1, row_old;
                string ID;
                string CommandText = "DELETE FROM ";

                row = dataGridView1.CurrentCell.RowIndex;  // берём номер строки
                row_old = row;                              // сохраняем номер в память
                ID = Convert.ToString(dataGridView1[0, row].Value);

                // Формируем строку CommandText
                switch (act_table)
                {
                    case 1:
                        {
                            CommandText = "DELETE FROM [User] WHERE [User].[id_User] = " + ID;
                        }
                        break;
                    case 2:
                        {
                            CommandText = "DELETE FROM [PO] WHERE [PO].[id_PO] = " + ID;
                        }
                        break;
                    case 3:
                        {
                            CommandText = "DELETE FROM [Proizvod] WHERE [Proizvod].[id_Prod] = " + ID;
                        }
                        break;
                    case 4:
                        {
                            CommandText = "DELETE FROM [PC] WHERE [PC].[id_PC] = " + ID;
                        }
                        break;
                    default:
                        break;
                }

                // выполняем SQL-запрос
                My_Execute_Non_Query(CommandText);

                // обновить форму
                this.Refresh();

                if (row_old >= 0)
                {
                    dataGridView1.ClearSelection();         // делаем клик на другую строку
                    dataGridView1[0, row_old].Selected = true;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int row = 1, row_old;
            string ID;

            row = dataGridView1.CurrentCell.RowIndex;  // берём номер строки
            row_old = row;                              // сохраняем номер в память
            ID = Convert.ToString(dataGridView1[0, row].Value);
            switch (act_table)
            {
                case 1:
                    {
                        FormAU U = new FormAU();
                        U.textBox1.Text = Convert.ToString(dataGridView1[1, row].Value);
                        U.textBox2.Text = Convert.ToString(dataGridView1[2, row].Value);
                        U.textBox3.Text = Convert.ToString(dataGridView1[3, row].Value);
                        U.textBox4.Text = Convert.ToString(dataGridView1[4, row].Value);
                        U.textBox5.Text = Convert.ToString(dataGridView1[5, row].Value);
                        U.dateTimePicker1.Text = Convert.ToString(dataGridView1[6, row].Value);
                        U.Text = "Редактировать";
                        U.label7.Text = ID;
                        U.button1.Text = "Применить";
                        U.Show();
                    }
                    break;
                case 2:
                    {
                        FormAPO PO = new FormAPO();
                        PO.textBox1.Text = Convert.ToString(dataGridView1[1, row].Value);
                        PO.textBox2.Text = Convert.ToString(dataGridView1[2, row].Value);
                        PO.comboBox1.Text = Convert.ToString(dataGridView1[3, row].Value);
                        PO.Text = "Редактировать";
                        PO.label4.Text = ID;
                        PO.button1.Text = "Применить";
                        PO.Show();
                    }
                    break;
                case 3:
                    {
                        FormAPr Pr = new FormAPr();
                        Pr.textBox1.Text = Convert.ToString(dataGridView1[1, row].Value);
                        Pr.textBox2.Text = Convert.ToString(dataGridView1[2, row].Value);
                        Pr.Text = "Редактировать";
                        Pr.label3.Text = ID;
                        Pr.button1.Text = "Применить";
                        Pr.Show();
                    }
                    break;
                case 4:
                    {
                        FormAPC PC = new FormAPC();
                        PC.textBox1.Text = Convert.ToString(dataGridView1[1, row].Value);
                        PC.textBox2.Text = Convert.ToString(dataGridView1[2, row].Value);
                        PC.textBox3.Text = Convert.ToString(dataGridView1[3, row].Value);
                        PC.textBox4.Text = Convert.ToString(dataGridView1[4, row].Value);
                        PC.textBox5.Text = Convert.ToString(dataGridView1[5, row].Value);
                        PC.comboBox1.Text = Convert.ToString(dataGridView1[6, row].Value);
                        PC.Text = "Редактировать";
                        PC.label7.Text = ID;
                        PC.button1.Text = "Применить";
                        PC.Show();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
