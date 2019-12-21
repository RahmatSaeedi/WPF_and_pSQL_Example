using System;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace WPF_and_pSQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private string connectionString = String.Format(
            "Server={0};Port={1};User Id={2};Password={3};Database={4};","localhost",5433,"postgres","Password","Demo"
        );
        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;

        public MainWindow()
        {
            InitializeComponent();
            conn = new NpgsqlConnection(connectionString);
        }

        private void Exit(object source, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Select(object source, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from _select()";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                conn.Close();
                dataGridData.DataContext = dt;

            } catch (Exception exp)
            {
                conn.Close();
                MessageBox.Show("Could not connect to the server." + exp.Message);
            }
        }

        private void Insert(object source, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from _create('" + tbFName.Text +"','"+ tbLName.Text+"',"+ tbDoB.Text + ")";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
                Select(null, null);
            }
            catch (Exception exp)
            {
                conn.Close();
                MessageBox.Show("Could not connect to the server." + exp.Message);
            }
        }

        private void Delete(object source, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"delete from demo where id = " + tbId.Text;
                cmd = new NpgsqlCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
                Select(null, null);
            }
            catch (Exception exp)
            {
                conn.Close();
                MessageBox.Show("Could not connect to the server." + exp.Message);
            }
        }

        private void Update(object source, RoutedEventArgs e)
        {
            try
            {
                conn.Open();
                sql = @"select * from _update("+ tbId.Text + ",'" + tbFName.Text + "','" + tbLName.Text + "'," + tbDoB.Text + ")";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
                Select(null, null);
            }
            catch (Exception exp)
            {
                conn.Close();
                MessageBox.Show("Could not connect to the server." + exp.Message);
            }
        }
    }
}
