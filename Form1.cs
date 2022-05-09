using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace sql
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Baza"].ConnectionString);
            sqlConnection.Open();

            if (sqlConnection.State == ConnectionState.Open) MessageBox.Show("БД подключена");
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        void Add_table_H(int i)
        {
            SqlCommand command = new SqlCommand($"CREATE TABLE [dbo].[H{i}] ([Id] INT IDENTITY(1, 1) NOT NULL, [date] DATE NULL);", sqlConnection);
            command.ExecuteNonQuery();
        }

        void Del_table_H(int i)
        {
            SqlCommand command = new SqlCommand($"DROP TABLE H{i}", sqlConnection);
            command.ExecuteNonQuery();
        }

        void Page()
        {
            string title = "TabPage " + (tabControl1.TabCount + 1).ToString();
            TabPage myTabPage = new TabPage(title);
            tabControl1.TabPages.Add(myTabPage);
            ProgressBar[] pr = new ProgressBar[2];
            pr[1] = new ProgressBar();
            pr[1].Location = new Point(100, 20);
            this.Controls.Add(pr[1]);
        }

        void BD_add(string[] H_input)
        {
            SqlCommand command = new SqlCommand($"INSERT INTO Habbits (habbit, time_start, time_end) VALUES (@habbit, @time_start, @time_end)", sqlConnection);

            command.Parameters.AddWithValue("habbit", H_input[0]);
            command.Parameters.AddWithValue("time_start", H_input[1]);
            command.Parameters.AddWithValue("time_end", H_input[2]);
            MessageBox.Show(command.ExecuteNonQuery().ToString());
        }

        void BD_del(string Name) 
        {
            SqlCommand command = new SqlCommand($"DELETE FROM Habbits WHERE habbit = N'{Name}'", sqlConnection);
            command.ExecuteNonQuery();
            MessageBox.Show("Удаление успешно!");
        }

        void BD_change_progress(string Name)
        {
            SqlCommand command = new SqlCommand($"UPDATE Habbits SET progress = progress + 1 WHERE habbit = N'{Name}'", sqlConnection);
            command.ExecuteNonQuery();            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string[] H_in = new string[3];
            H_in[0] = textBox1.Text;
            H_in[1] = textBox2.Text;
            H_in[2] = textBox3.Text;
            BD_add(H_in);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Del_table_H(1);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
