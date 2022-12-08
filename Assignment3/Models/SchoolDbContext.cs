using System;
using MySql.Data.MySqlClient;

namespace Assignment3.Models
{
    public class SchoolDbContext
    {
        private MySqlConnection connection;
        private static string Server { get { return "localhost"; } }
        private static string Database { get { return "schoodb"; } }
        private static string User { get { return "root"; } }
        private static string Password { get { return "root"; } }
        private static string Port { get { return "8889"; } }

        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server +
                    "; user = " + User +
                    "; database = " + Database +
                    "; port = " + Port +
                    "; password = " + Password;
            }
        }
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }

        /* use dis:

        private SchoolDbContext School = new SchoolDbContext();
        MySqlConnection Conn = School.AccessDataBase();

        */

       /* private void Initialize()
        {
            server = "localhost";
            database = "schoodb";
            uid = "username";
            password = "pword";
            string connectionString;
            connectionString = "SERVER= " + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            connection.Open();
            return true;
        }

        //Close connection
        private bool CloseConnection()
        {
            connection.Close();
            return true;
        }

        //Insert statement
        public void Insert()
        {
            string query = "Make your query bitch";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        //Update statement
        public void Update()
        {
            string query = "Fuck you update bitchass";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = query;

                cmd.Connection = connection;

                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }

        }

        //Delete statement
        public void Delete()
        {
            string query = "[Linda's voice] Get outta here";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

                this.CloseConnection();
            }
        }

        public List<string>[] Select()
        {

                string query = "SELECT * FROM tableinfo";

                //Create a list to store the result
                List<string>[] list = new List<string>[3];
                list[0] = new List<string>();
                list[1] = new List<string>();
                list[2] = new List<string>();

                //Open connection
                if (this.OpenConnection() == true)
                {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                        list[0].Add(dataReader["id"] + "");
                        list[1].Add(dataReader["name"] + "");
                        list[2].Add(dataReader["age"] + "");
                    }

                    //close Data Reader
                    dataReader.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return list;
                }
                else
                {
                    return list;
                }
            }

        public int Count()
        {
            string query = "SELECT Count(*) FROM tableinfo";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }*/
    }
}

