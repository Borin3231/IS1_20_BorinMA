using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace IS1_20_BorinMA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string loginUser = login.Text;
            string PassUser = Pass.Text;

            

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

         
            MySqlCommand command = new MySqlCommand("SELECT * FROM `` WHERE `login` = @uL AND `pass` = @uP");
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value =loginUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = PassUser;
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
                MessageBox.Show("Авторизация прошла успешно");
            else
                MessageBox.Show("Авторизация не удалась");
        }
    }
}
