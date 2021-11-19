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
    public partial class FormAdd : Form
    {
        public FormAdd()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void Get_Table(string CommandText, int num_dG, string table_name)
        {
            FormMain f1 = new FormMain();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(CommandText, f1.ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, table_name);
            if (num_dG == 1)
            {
                dataGridView1.DataSource = ds.Tables[table_name].DefaultView;
                dataGridView1.Columns[0].Visible = false;
            }
            if (num_dG == 2)
            {
                dataGridView2.DataSource = ds.Tables[table_name].DefaultView;
                dataGridView2.Columns[0].Visible = false;
            }
            if (num_dG == 4)
            {
                dataGridView4.DataSource = ds.Tables[table_name].DefaultView;
                dataGridView4.Columns[0].Visible = false;
            }
         }

        private void FormAdd_Load(object sender, EventArgs e)
        {
            FormMain f1 = new FormMain();
            Get_Table("SELECT " +
                "DISTINCT [User].[id_User], " +
                "[User].[F_name] AS [Фамилия], " +
                "[User].[I_name] AS [Имя], " +
                "[User].[O_name] AS [Отчество], " +
                "[User].[Tel] AS [Телефон], " +
                "[User].[E-mail], " +
                "[User].[BirthDay] AS [Дата рождения], " +
                "(SELECT COUNT(*) FROM [PC] WHERE [PC].[User_id]=[User].[id_User]) AS [Компьютеры (штук)], " +
                "(SELECT COUNT(*) FROM [Main] WHERE [Main].[User_id]=[User].[id_User]) AS [Программы (штук)] " +
               "FROM " +
                "[User], [PC], [Main]", 1, "[User]"); // заполняем таблицу "Пользователи"
            Get_Table("SELECT " +
                "[PO].[id_PO], " +
                "[PO].[PO_name] AS [Название], " +
                "[PO].[Info] AS [Информация], " +
                "[Proizvod].[Prod_name] AS [Производитель] " +
               "FROM " +
                "[PO], " +
                "[Proizvod] " +
               "WHERE " +
                "([Proizvod].[id_Prod]=[PO].[Prod_id]) AND " +
                "([PO].[Prod_id]=[Proizvod].[id_Prod])", 2, "[PO]");   // заполняем таблицу "ПО"
            Get_Table("SELECT " +
                "[Proizvod].[id_Prod], " +
                "[Proizvod].[Prod_name] AS [Название], " +
                "[Proizvod].[Info] AS [Информация] " +
               "FROM " +
                "[Proizvod]", 3, "[Proizvod]");  // заполняем таблицу "Производители"
            Get_Table("SELECT " +
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
                "([User].[id_User]=[PC].[User_id])", 4, "[PC]");       // заполняем таблицу "ПК"
           // textBox1.Text = "";
           // textBox1.Text = f1.dataGridView1.Rows[0].Cells[0].ToString();
            textBox1.Text = f1.dataGridView1.RowCount.ToString();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row;
            // Берем данные из ячеек таблицы "Пользователи"
            row = dataGridView1.CurrentCell.RowIndex;
            label2.Text = "Пользователь: " + Convert.ToString(dataGridView1[1, row].Value) +
                " " + String.Join(" ", Convert.ToString(dataGridView1[2, row].Value).Split(' ').Select(v => v.First())) +
                ". " + String.Join(" ", Convert.ToString(dataGridView1[3, row].Value).Split(' ').Select(v => v.First())) + ". ";
            Get_Table("SELECT " +
                "[PC].[id_PC], " +                // отображение только собственных ПК
                "[PC].[OS] AS [ОС], " +          // отображение только собственных ПК
                "[PC].[CPU], " +                // отображение только собственных ПК
                "[PC].[GPU], " +               // отображение только собственных ПК
                "[PC].[RAM], " +              // отображение только собственных ПК
                "[PC].[ROM], " +
                "[User].[F_name] & \" \" & " +
                "LEFT ([User].[I_name], 1) & \". \" & " +
                "LEFT ([User].[O_name], 1) & \".\" AS [ФИО]" +
               "FROM " +
                "[PC], " +
                "[User] " +
               "WHERE " +
                "([PC].[User_id]=[User].[id_User]) AND " +
                "([User].[id_User]=[PC].[User_id]) AND ([PC].[User_id]=" + Convert.ToString(dataGridView1[0, row].Value) + ")", 4, "[PC]");
        }

        private void dataGridView2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row;
            // Берем данные из таблицы "ПО"
            row = dataGridView2.CurrentCell.RowIndex;
            label3.Text = "Программное обеспечение: " + 
               Convert.ToString(dataGridView2[1, row].Value);
            label4.Text = "Производитель: " + Convert.ToString(dataGridView2[3, row].Value);
        }

        private void dataGridView4_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int row;
            // Данные о ПК
            row = dataGridView4.CurrentCell.RowIndex;
            label5.Text = "ПК: " + Convert.ToString(dataGridView4[1, row].Value) +
               " / " + Convert.ToString(dataGridView4[2, row].Value) +
               " / " + Convert.ToString(dataGridView4[3, row].Value) +
               " / " + Convert.ToString(dataGridView4[4, row].Value) +
               " / " + Convert.ToString(dataGridView4[5, row].Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Фильтр к таблице "Пользователи"
            string CommandText = "SELECT " +
                "[User].[id_User], " +
                "[User].[F_name] AS [Фамилия], " +
                "[User].[I_name] AS [Имя], " +
                "[User].[O_name] AS [Отчество], " +
                "[User].[Tel] AS [Телефон], " +
                "[User].[E-mail], " +
                "[User].[BirthDay] AS [Дата рождения], " +
                "[User].[PC_count] AS [Компьютеры (штук)], " +
                "[User].[PO_count] AS [Программы (штук)] " +
               "FROM " +
                "[User]";
            // формируем переменную CommandText
            if (textBox2.Text == "")
                CommandText = "SELECT " +
                "[User].[id_User], " +
                "[User].[F_name] AS [Фамилия], " +
                "[User].[I_name] AS [Имя], " +
                "[User].[O_name] AS [Отчество], " +
                "[User].[Tel] AS [Телефон], " +
                "[User].[E-mail], " +
                "[User].[BirthDay] AS [Дата рождения], " +
                "[User].[PC_count] AS [Компьютеры (штук)], " +
                "[User].[PO_count] AS [Программы (штук)] " +
               "FROM " +
                "[User]";
            else
            if (comboBox1.SelectedIndex == 0) // Фамилия
                CommandText = CommandText + " WHERE [F_name] LIKE '" + textBox2.Text + "%'";
            else
            if (comboBox1.SelectedIndex == 1) // Имя
                CommandText = CommandText + " WHERE [I_name] LIKE '" + textBox2.Text + "%'";
            else
            if (comboBox1.SelectedIndex == 2) // Отчество
                CommandText = CommandText + " WHERE [O_name] LIKE '" + textBox2.Text + "%'";
            else
            if (comboBox1.SelectedIndex == 3) // Телефон
                CommandText = CommandText + " WHERE [Tel] LIKE '" + textBox2.Text + "%'";
            else
            if (comboBox1.SelectedIndex == 4) // E-mail
                CommandText = CommandText + " WHERE [E-mail] LIKE '" + textBox2.Text + "%'";
            else
            if (comboBox1.SelectedIndex == 5) // Дата рождения
                CommandText = CommandText + " WHERE [BirthDay] LIKE '" + textBox2.Text + "%'";

            FormMain f = new FormMain();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(CommandText, f.ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "[User]");
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
            dataGridView1.Columns[0].Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Фильтр к таблице "ПО"
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
            // формируем переменную CommandText
            if (textBox3.Text == "")
                CommandText = "SELECT " +
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
            else
            if (comboBox2.SelectedIndex == 0) // Наименование
                CommandText = CommandText + "AND ([PO].[PO_name] LIKE '" + textBox3.Text + "%') ";
            else
            if (comboBox2.SelectedIndex == 1) // Информация
                CommandText = CommandText + "AND ([PO].[Info] LIKE '" + textBox3.Text + "%') ";
            else
            if (comboBox2.SelectedIndex == 2) // Производитель
                CommandText = CommandText + "AND ([Proizvod].[Prod_name] LIKE '" + textBox3.Text + "%') ";
            // [Proizvod].[Prod_name]
            FormMain f = new FormMain();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(CommandText, f.ConnectionString);
            DataSet ds = new DataSet();
            dataAdapter.Fill(ds, "[PO]");
            dataGridView2.DataSource = ds.Tables[0].DefaultView;
            dataGridView2.Columns[0].Visible = false;
        }
    }
}
