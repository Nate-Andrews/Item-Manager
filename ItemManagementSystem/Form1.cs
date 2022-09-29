using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ItemManagementSystem
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\andre\\Documents\\Coding\\C#\\ItemManagementSystem\\ItemManagementSystem\\Database1.mdf;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }

        private void itemsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.itemsBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.database1DataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'database1DataSet.Items' table. You can move, or remove it, as needed.
            this.itemsTableAdapter.Fill(this.database1DataSet.Items);

            clear();
            
            con.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Items", con);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            itemsDataGridView.DataSource = dt;
            con.Close();
        }

        private void buttonc_Click(object sender, EventArgs e)
        { 
            con.Open();
            SqlCommand command = new SqlCommand("Insert INTO Items(itemId, name, price, Department) VALUES (@itemId, @name, @price, @Department)", con);
            command.Parameters.AddWithValue("@itemID", itemIdTextBox.Text);
            command.Parameters.AddWithValue("@name", nameTextBox.Text);
            command.Parameters.AddWithValue("@price", priceTextBox.Text);
            command.Parameters.AddWithValue("@Department", departmentTextBox.Text);
            command.ExecuteNonQuery();
            con.Close();

            clear();

            MessageBox.Show("Created Item");
        }
        
        private void buttonu_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand("UPDATE Items SET name=@name, price=@price, Department=@Department WHERE itemId=@itemId", con);
            command.Parameters.AddWithValue("@itemID", itemIdTextBox.Text);
            command.Parameters.AddWithValue("@name", nameTextBox.Text);
            command.Parameters.AddWithValue("@price", priceTextBox.Text);
            command.Parameters.AddWithValue("@Department", departmentTextBox.Text);
            command.ExecuteNonQuery();
            con.Close();

            clear();

            MessageBox.Show("Item Updated");
        }

        private void buttond_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand("Delete Items WHERE itemId=@itemId", con);
            command.Parameters.AddWithValue("@itemID", itemIdTextBox.Text);
            command.ExecuteNonQuery();
            con.Close();

            clear();

            MessageBox.Show("Item Deleted");
        }

        private void buttonf_Click(object sender, EventArgs e)
        { 
            con.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Items WHERE itemId=@itemId", con);
            command.Parameters.AddWithValue("@itemID", itemIdTextBox.Text);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            itemsDataGridView.DataSource = dt;
            con.Close();

            clear();
        }

        private void buttonr_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Items", con);
            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            itemsDataGridView.DataSource = dt;
            con.Close();
        }

        void clear()
        {
            itemIdTextBox.Text = "";
            nameTextBox.Text = "";
            priceTextBox.Text = "";
            departmentTextBox.Text = "";
        }
    }
}
