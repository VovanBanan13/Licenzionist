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
    public partial class FormMain : Form
    {
        public string ConnectionString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = ..\\..\\DataBase.mdb";
        public string CommandText = "SELECT " +
                "[Main].[id_main] AS [id номер], " +
                "[User].[F_name] AS [Фамилия], " +
                "[User].[I_name] AS [Имя], " +
                "[User].[O_name] AS [Отчество], " +
                "[PO].[PO_name] AS [ПО], " +
                "[Main].[License] AS [Срок лицензии] " +
               "FROM " +
                "[Main], " +
                "[User], " +
                "[PO] " +
               "WHERE " +
                "([Main].[User_id]=[User].[id_User]) AND " +
                "([Main].[PO_id]=[PO].[id_PO])";
        public int act_table = 1; // активная таблица (1-пользователи, 2-ПО, 3-производители, 4-ПК)
        public FormMain()
        {
            InitializeComponent();
            OleDbDataAdapter dA = new OleDbDataAdapter(CommandText, ConnectionString);
            DataSet ds = new DataSet();
            dA.Fill(ds, "[Main]");
            dataGridView1.DataSource = ds.Tables["[Main]"].DefaultView;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string CommandText = "SELECT " +
                "[Main].[id_main] AS [id номер], " +
                "[User].[F_name] AS [Фамилия], " +
                "[User].[I_name] AS [Имя], " +
                "[User].[O_name] AS [Отчество], " +
                "[PO].[PO_name] AS [ПО], " +
                "[Main].[License] AS [Срок лицензии] " +
               "FROM " +
                "[Main], " +
                "[User], " +
                "[PO] " +
               "WHERE " +
                "([Main].[User_id]=[User].[id_User]) AND " +
                "([Main].[PO_id]=[PO].[id_PO])";

            if (textBox1.Text != "")  // если набран текст в поле фильтра
            {
                if (comboBox1.SelectedIndex == 0) // № Фамилия
                    CommandText = CommandText + " AND ([User].[F_name] LIKE '" + textBox1.Text + "%') ";
                if (comboBox1.SelectedIndex == 1) // № Имя
                    CommandText = CommandText + " AND ([User].[I_name] LIKE '" + textBox1.Text + "%') ";
                if (comboBox1.SelectedIndex == 2) // Отчество
                    CommandText = CommandText + " AND ([User].[O_name] LIKE '" + textBox1.Text + "%') ";
                if (comboBox1.SelectedIndex == 3) // ПО
                    CommandText = CommandText + " AND ([PO].[PO_name] LIKE '" + textBox1.Text + "%') ";
                if (comboBox1.SelectedIndex == 4) // Срок лицензии
                    CommandText = CommandText + " AND ([Main].[License] LIKE '" + textBox1.Text + "%') ";
            }

            OleDbDataAdapter dA = new OleDbDataAdapter(CommandText, ConnectionString);
            DataSet ds = new DataSet();
            dA.Fill(ds, "[Main]");
            dataGridView1.DataSource = ds.Tables["[Main]"].DefaultView;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1; // Очистка комбобокса
            comboBox1.Text = "";
            textBox1.Clear();
            OleDbDataAdapter dA = new OleDbDataAdapter(CommandText, ConnectionString);
            DataSet ds = new DataSet();
            dA.Fill(ds, "[Main]");
            dataGridView1.DataSource = ds.Tables["[Main]"].DefaultView;
        }


        public void Get_User()
        {
            FormView V = new FormView();
            V.Show();
            V.label1.Text = "Пользователи";
            V.Text = "Просмотр таблицы \"Пользователи ПО\"";
            V.act_table = 1;
            string CommandText = "SELECT " +
                "DISTINCT [User].[id_User], " +
                "[User].[F_name] AS [Фамилия], " +
                "[User].[I_name] AS [Имя], " +
                "[User].[O_name] AS [Отчество], " +
                "[User].[Tel] AS [Телефон], " +
                "[User].[E-mail], " +
                "[User].[BirthDay] AS [Дата рождения], " +
                // "[User].[PC_count] AS [Компьютеры (штук)], " +
                // "[User].[PO_count] AS [Программы (штук)], " +
                // "COUNT ([PC].[id_PC]), " + 
                // "COUNT ([Main].[id_main]) " +
                "(SELECT COUNT(*) FROM [PC] WHERE [PC].[User_id]=[User].[id_User]) AS [Компьютеры (штук)], " +
                "(SELECT COUNT(*) FROM [Main] WHERE [Main].[User_id]=[User].[id_User]) AS [Программы (штук)] " +
               "FROM " +
                "[User], [PC], [Main]";
              //  "WHERE " +
              //  "([User].[id_User]=[PC].[User_id]) OR " +
              //  "([User].[id_User]=[Main].[User_id])"; 
              // "LEFT JOIN [PC], [Main] " + 
              //  "ON ([User].[id_User] = [PC].[User_id]) AND " + 
              //  "([User].[id_User] = [Main].[User_id])";

            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(CommandText, ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "[User]");
            V.dataGridView1.DataSource = ds.Tables[0].DefaultView;
            V.dataGridView1.Columns[0].Visible = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Get_User();
            act_table = 1;
        }

        public void Get_PO()
        {
            FormView V = new FormView();
            V.Show();
            V.label1.Text = "Программное обеспечение";
            V.Text = "Просмотр таблицы \"Программное обеспечение\"";
            V.act_table = 2;
            string CommandText = "SELECT " +
                "[PO].[id_PO], " +
                "[PO].[PO_name] AS [Название], " +
                "[PO].[Info] AS [Информация], " +
                "[Proizvod].[Prod_name] AS [Производитель] " +
               "FROM " +
                "[PO], " +
                "[Proizvod] " +
               "WHERE " +
                "([Proizvod].[id_Prod]=[PO].[Prod_id]) AND " +
                "([PO].[Prod_id]=[Proizvod].[id_Prod])";
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(CommandText, ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "[PO]");
            V.dataGridView1.DataSource = ds.Tables[0].DefaultView;
            V.dataGridView1.Columns[0].Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Get_PO();
            act_table = 2;
        }

        public void Get_Proizvod()
        {
            FormView V = new FormView();
            V.Show();
            V.label1.Text = "Производители ПО";
            V.Text = "Просмотр таблицы \"Производители ПО\"";
            V.act_table = 3;
            string CommandText = "SELECT " +
                "[Proizvod].[id_Prod], " +
                "[Proizvod].[Prod_name] AS [Название], " +
                "[Proizvod].[Info] AS [Информация] " +
               "FROM " +
                "[Proizvod]";
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(CommandText, ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "[Proizvod]");
            V.dataGridView1.DataSource = ds.Tables[0].DefaultView;
            V.dataGridView1.Columns[0].Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Get_Proizvod();
            act_table = 3;
        }

        public void Get_PC()
        {
            FormView V = new FormView();
            V.Show();
            V.label1.Text = "Компьютеры";
            V.Text = "Просмотр таблицы \"Конфигурации компьютеров\"";
            V.act_table = 4;
            string CommandText = "SELECT " +
                "[PC].[id_PC], " +
                "[PC].[OS] AS [ОС], " +
                "[PC].[CPU], " +
                "[PC].[GPU], " +
                "[PC].[RAM], " +
                "[PC].[ROM], " +
                "[User].[F_name] & \" \" & " +
                "LEFT ([User].[I_name], 1) & \". \" & " +
                "LEFT ([User].[O_name], 1) & \".\" AS [ФИО]" +
               "FROM " +
                "[PC], " +
                "[User] " +
               "WHERE " +
                "([PC].[User_id]=[User].[id_User]) AND " +
                "([User].[id_User]=[PC].[User_id])";
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(CommandText, ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "[PC]");
            V.dataGridView1.DataSource = ds.Tables[0].DefaultView;
            V.dataGridView1.Columns[0].Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Get_PC();
            act_table = 4;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FormAdd A = new FormAdd();
            string CommandText;
            string ID_M, ID_U, ID_PO, ID_PC, Lic;
            int row;
            //FormAdd f = new FormAdd(); // создали новую форму

            if (A.ShowDialog() == DialogResult.OK)
            {
                // добавляем данные
                // Номер 
                if (A.textBox1.Text == "") ID_M = "0";
                else ID_M = A.textBox1.Text;
                // добавляем id_User
                row = A.dataGridView1.CurrentCell.RowIndex; // взяли строку с dataGridView1
                ID_U = Convert.ToString(A.dataGridView1[0, row].Value);
                // добавляем id_PO
                row = A.dataGridView2.CurrentCell.RowIndex; // взяли строку с dataGridView2
                ID_PO = Convert.ToString(A.dataGridView2[0, row].Value);
                // добавляем id_PC
                row = A.dataGridView4.CurrentCell.RowIndex; // взяли строку с dataGridView4
                ID_PC = Convert.ToString(A.dataGridView4[0, row].Value);
                Lic = Convert.ToString(A.dateTimePicker1.Text); // переводим время в строку
                Lic = Lic.Substring(0, Lic.LastIndexOf('.') + 5);  // удаляем время, оставляем только дату
                // формируем CommandText
                CommandText = "INSERT INTO [Main] (id_main, User_id, PO_id, License) " +
                "VALUES (" + ID_M + ", " + ID_U + ", " + ID_PO + ", \"" + Lic + "\")";

                // выполняем SQL-команду
                My_Execute_Non_Query(CommandText);
                // обновляем таблицу и форму
                this.Refresh();
                button1_Click(sender, e);
            }
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

        private void button8_Click(object sender, EventArgs e)
        {
            FormDelete f = new FormDelete();

            if (f.ShowDialog() == DialogResult.OK)
            {
                int row, row_old;
                string ID;
                string CommandText = "DELETE FROM ";

                row = dataGridView1.CurrentCell.RowIndex;  // берём номер строки
                row_old = row;                              // сохраняем номер в память
                ID = Convert.ToString(dataGridView1[0, row].Value);

                // Формируем строку CommandText
                CommandText = "DELETE FROM [Main] WHERE [Main].[id_main] = " + ID;

                // выполняем SQL-запрос
                My_Execute_Non_Query(CommandText);

                // обновить форму
                this.Refresh();
                button1_Click(sender, e);

                if (row_old >= 0)
                {
                    dataGridView1.ClearSelection();         // делаем клик на другую строку
                    dataGridView1[0, row_old].Selected = true;
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBox1.Text = "";
            button6_Click(sender, e);
        }


        // добавление Пользователя через ConnectionString и запрос ExecuteNonQuery()
        private void Add_User(string f_name, string i_name, string o_name, string tel, string email, DateTime date)
        {
            string CommandText;
            string birthday;
            birthday = Convert.ToString(date); // переводим время в строку
        

            CommandText = "INSERT INTO [User] (F_name, I_name, O_name, Tel, E-mail, BirthDay) " 
                + "VALUES ('" + f_name + "', " + i_name + ", '" + o_name + "', '" +
             tel + "', '" + email + "', " + birthday + ")";
            My_Execute_Non_Query(CommandText);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            closeAll();
        }

        public static void closeAll()
        {
            FormCollection fc = Application.OpenForms;
            if (fc.Count > 1)
            {
                for (int i = (fc.Count); i > 1; i--)
                {
                    Form selectedForm = Application.OpenForms[i - 1];
                    selectedForm.Close();
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Закрыть приложение?", this.Text,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }
        }

        private void закрытьАИСToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbout F = new FormAbout();
            F.ShowDialog();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int row, row_old;
            string ID;

            row = dataGridView1.CurrentCell.RowIndex;  // берём номер строки
            row_old = row;                              // сохраняем номер в память
            ID = Convert.ToString(dataGridView1[0, row].Value);

            FormUpL U = new FormUpL();
            string CommandText;
            string Lic;
            U.label3.Text = label3.Text;
            U.label1.Text = "Пользователь: " + Convert.ToString(dataGridView1[1, row].Value) +
                " " + String.Join(" ", Convert.ToString(dataGridView1[2, row].Value).Split(' ').Select(v => v.First())) +
                ". " + String.Join(" ", Convert.ToString(dataGridView1[3, row].Value).Split(' ').Select(v => v.First())) + ". ";
            U.label2.Text = "Программное обеспечение: " + Convert.ToString(dataGridView1[4, row].Value);
            U.dateTimePicker1.Text = Convert.ToString(dataGridView1[5, row].Value);

            if (U.ShowDialog() == DialogResult.OK)
            {
                Lic = Convert.ToString(U.dateTimePicker1.Text); // переводим время в строку
                Lic = Lic.Substring(0, Lic.LastIndexOf('.') + 5);  // удаляем время, оставляем только дату
                // формируем CommandText
                CommandText = "UPDATE [Main] SET " +
                    "[Main].[License] = '" + Lic + "' WHERE [Main].[id_main] = " + ID;

                // выполняем SQL-команду
                My_Execute_Non_Query(CommandText);
                // обновляем таблицу и форму
                this.Refresh();
                button1_Click(sender, e);
            }
        }

        public void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            FormUpL U = new FormUpL();
            int row;
            row = dataGridView1.CurrentCell.RowIndex;
            label3.Text = Convert.ToString(dataGridView1[0, row].Value);
            U.label3.Text = label3.Text;
        }
    }    
}

