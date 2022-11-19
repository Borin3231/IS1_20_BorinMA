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
    public partial class Form1 : MetroFramework.Forms.MetroForm
     
    {
        string connStr = "server= caseum.ru;port = 33333; user = st_1_20_4; database = 123;password=32006333;";

        MySqlConnection conn;

        

        static string sha256(string randomString)
        {
            // Смысл данного метода заключается в том, что строка залетает в метод
            var crypt = new System.Security.Cryptography.SHA256Managed();
            var hash = new System.Text.StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }
        public void GetUserInfo(string login_user)
        {
            //Объявлем переменную для запроса в БД
            string selected_id_stud = login.Text;
            // устанавливаем соединение с БД
            conn.Open();
            // запрос
            string sql = $"SELECT * FROM t_user WHERE loginUser='{login_user}'";
            // объект для выполнения SQL-запроса
            MySqlCommand command = new MySqlCommand(sql, conn);
            // объект для чтения ответа сервера
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                // элементы массива [] - это значения столбцов из запроса SELECT
                Auth.auth_id = reader[0].ToString();
                Auth.auth_fio = reader[1].ToString();
                Auth.auth_role = Convert.ToInt32(reader[4].ToString());
            }
            reader.Close(); // закрываем reader
            // закрываем соединение с БД
            conn.Close();
        }
        static class Auth
        {
            //Статичное поле, которое хранит значение статуса авторизации
            public static bool auth = false;
            //Статичное поле, которое хранит значения ID пользователя
            public static string auth_id = null;
            //Статичное поле, которое хранит значения ФИО пользователя
            public static string auth_fio = null;
            //Статичное поле, которое хранит количество привелегий пользователя
            public static int auth_role = 0;
        }
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connStr);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string loginUser = login.Text;
            string PassUser = Pass.Text;
            
            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

         
            MySqlCommand command = new MySqlCommand("SELECT * FROM `t_user` WHERE `login` = @uL AND `pass` = @uP");
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value =loginUser;
            command.Parameters.Add("@uP", MySqlDbType.VarChar).Value = PassUser;
            command.Parameters["@un"].Value = login.Text;
            command.Parameters["@up"].Value = sha256(Pass.Text);
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                //Присваеваем глобальный признак авторизации
                Auth.auth = true;
                //Достаем данные пользователя в случае успеха
                GetUserInfo(login.Text);
                MessageBox.Show("Complete");
                this.Hide();
                f2.ShowDialog();
                
                //Закрываем форму
                this.Close();
            }
            else
            {
                //Отобразить сообщение о том, что авторизаия неуспешна
                MessageBox.Show("Неверные данные авторизации!");
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        From2 f2 = new From2();

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void login_TextChanged(object sender, EventArgs e)
        {

        }

        private void Pass_TextChanged(object sender, EventArgs e)
        {
            
            
        }
    }
  }

