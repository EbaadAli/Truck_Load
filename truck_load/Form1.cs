using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MySql.Data.MySqlClient;


namespace truck_load
{
    public partial class Form1 : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;



        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            server = "localhost";
            database = "testDB";
            uid = "root";
            password = "P@ssw0rd";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
            panel2.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            groupBox2.Visible = false;
            truck_panel.Visible = false;


        }
        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public MySqlDataReader ExecuteReader(string sql)
        {
            try
            {
                MySqlDataReader reader;
                MySqlCommand cmd = new MySqlCommand(sql, connection);
                reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (this.OpenConnection() == true)
            {
                MessageBox.Show("Database Connected");
                CloseConnection();
                GetTruckList(); 
                GetItemList();
                panel2.Visible = false;
                truck_panel.Visible = true;
                panel5.Visible = true;
            };
            button1.Text = "Database Connected";
            button1.Enabled = false;

        }

        private void GetTruckList()
        {
            try
            {
                connection.Open();
                string query = "SELECT truckID,name,number,status FROM trucks";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        DataGridViewRow newRow = new DataGridViewRow();

                        newRow.CreateCells(dataGridView1);
                        newRow.Cells[0].Value = dataReader["truckID"].ToString();
                        newRow.Cells[1].Value = dataReader["name"].ToString();
                        dataGridView1.Rows.Add(newRow);
                    }
                }
                
                connection.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }
        string selected_truck_id = "";
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //GetSpecificTruck(dataGridView1.CurrentRow.Cells["Column1"].Value.ToString());
            //selected_truck_id = dataGridView1.CurrentRow.Cells["Column1"].Value.ToString();
        }

        private void GetSpecificTruck(string truck_id)
        {
            try
            {
                connection.Open();
                string query = "SELECT truckID,name,number,status FROM trucks where truckID="+truck_id;
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                int flag = 0;
                if (dataReader.HasRows)
                {

                    flag = 1;
                    while (dataReader.Read())
                    {
                        textBox1.Text = dataReader["name"].ToString();
                        textBox2.Text = dataReader["number"].ToString();
                        switch (dataReader["status"].ToString())
                        {
                            case "O":
                                textBox3.Text = "Open";
                                button4.Enabled = true;
                                break;
                            case "C":
                                textBox3.Text = "Closed";
                                button4.Enabled = false;
                                break;
                        }
                    }
                }
                connection.Close();

                if(flag == 1)
                {
                    groupBox2.Visible = true;
                    GetSpecificTruckItem(truck_id);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        private void GetSpecificTruckItem(string truck_id)
        {
            try
            {
                connection.Open();
                string query = "SELECT boxID,PLU,number,weight,status,truckID FROM Boxes WHERE truckID="+truck_id;
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataGridView2.Rows.Clear();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        DataGridViewRow newRow = new DataGridViewRow();
                        newRow.CreateCells(dataGridView2);
                        newRow.Cells[0].Value = dataReader["PLU"].ToString();
                        dataGridView2.Rows.Add(newRow);
                    }

                }
                else
                {

                    dataGridView2.Rows.Clear();
                }
                

                connection.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox3.Text == "Closed")
            {
                MessageBox.Show("Truck already closed");
            }
            else
            {
                check_truck_item(selected_truck_id);
            }
        }

        private void check_truck_item(string truck_id)
        {
            connection.Open();
            string query = "SELECT boxID,PLU,number,weight,status,truckID FROM Boxes WHERE truckID=" + truck_id;
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            int flag = 0;
            if (dataReader.HasRows)
            {
                flag = 1;
                
            }
            connection.Close();
            if(flag == 1)
            {
                MessageBox.Show("Closing Truck");
                update_truck_status(selected_truck_id, "C");
            }
            else
            {
                MessageBox.Show("Unable To Close Truck. Item need to be loaded before closing trucks");
            }
        }

        private void update_truck_status(string truck_id,string truck_status)
        {
            string query = "UPDATE trucks SET status='"+ truck_status + "' WHERE truckID='"+truck_id+"'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
                GetSpecificTruck(selected_truck_id);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "Open")
            {
                MessageBox.Show("Truck already Opened");
            }
            else
            {
                update_truck_status(selected_truck_id, "O");
                MessageBox.Show("Truck Opened");
            }
        }

        private void GetAvailableItemToBeLoaded_List(string truck_id)
        {
            comboBox1.Items.Clear();

            connection.Open();
            string query = "SELECT boxID,PLU,number,weight,status,truckID FROM Boxes WHERE status='I'";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            if (dataReader.HasRows)
            {
                //Iterate through the rows and add it to the combobox's items
                while (dataReader.Read())
                {
                    comboBox1.Items.Add(new ListItem(dataReader.GetString("PLU"), dataReader.GetString("boxID")));
                }

            }
            else
            {
                comboBox1.Items.Add("No item available");
            }
            comboBox1.SelectedIndex = 0;
            connection.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
            GetAvailableItemToBeLoaded_List(selected_truck_id);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int selectedIndex = comboBox1.SelectedIndex;
            comboBox1.SelectedItem.ToString();
            string selectedValue = comboBox1.Items[selectedIndex].ToString();
            
            if(selectedValue == "No item available")
            {
                MessageBox.Show("Oops, no item available to be loaded");
            }
            else
            {
                add_item_to_truck(selected_truck_id, selectedValue);
                MessageBox.Show("Item successfully loaded to trucks");

            }
           
        }

        private void add_item_to_truck(string truck_id, string item_name)
        {
            string query = "UPDATE Boxes SET status='S',truckID='"+truck_id+"' WHERE PLU='" + item_name + "'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                GetSpecificTruck(selected_truck_id);
                GetItemList();
                panel2.Visible = false;
                panel1.Visible = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            panel4.Visible = true;
            groupBox2.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
            panel3.Visible = true;
            groupBox2.Visible = true;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(textBox4.Text != "")
            {
                add_new_truck(textBox4.Text);
            }
            else
            {
                MessageBox.Show("Key in trucks name");
            }
        }

        private void add_new_truck(string truck_name)
        {
            string query = "INSERT INTO trucks (name,status) VALUES ('" + truck_name + "','O')";

            //Open connection
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                this.CloseConnection();
                GetTruckList();
                panel4.Visible = false;
                panel3.Visible = true;
                groupBox2.Visible = true;
                MessageBox.Show("Truck Successfully added");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try {

                GetSpecificTruck(dataGridView1.CurrentRow.Cells["Column1"].Value.ToString());
                selected_truck_id = dataGridView1.CurrentRow.Cells["Column1"].Value.ToString();
            } catch (Exception err) {
               // MessageBox.Show("x");
            }
        }


        private void GetItemList()
        {
            try
            {
                connection.Open();
                string query = "SELECT A.PLU,A.status,B.name as truckName FROM Boxes A LEFT JOIN trucks B ON B.truckID = A.truckID ";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                dataGridView3.Rows.Clear();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {


                        DataGridViewRow newRow = new DataGridViewRow();

                        newRow.CreateCells(dataGridView3);
                        newRow.Cells[0].Value = dataReader["PLU"].ToString();
                        newRow.Cells[1].Value = dataReader["status"].ToString();
                        newRow.Cells[2].Value = dataReader["truckName"].ToString();
                        dataGridView3.Rows.Add(newRow);
                    }
                }

                connection.Close();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }

        }


    }
}
