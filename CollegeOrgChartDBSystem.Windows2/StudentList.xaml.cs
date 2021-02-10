using System;
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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace CollegeOrgChartDBSystem.Windows2
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    /// <summary>
    /// Interaction logic for StudentList.xaml
    /// </summary>
    public partial class StudentList : Window
    {
        private string course;
        public StudentList()
        {
            InitializeComponent();
            cb_course.ItemsSource = new List<string>() { "MIS", "BSIS", "BSA" };
            ShowAllStudentS();
            btnDelete.IsEnabled = false;
        }




        private void ShowAllStudentS()
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost; Initial Catalog=CollegeDB; Integrated Security=True;");
            try
            {
                String query = "";
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                query = "SELECT * FROM T_STUD";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.ExecuteNonQuery();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable("T_STUD");
                dataAdapter.Fill(dt);
                StudentsDataGrid.ItemsSource = dt.DefaultView;
                dataAdapter.Update(dt);
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ShowSpecificStudentS(string var)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost; Initial Catalog=CollegeDB; Integrated Security=True;");
            try
            {
                String query = "";
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                query = "SELECT * FROM T_STUD where stud_course='" + var + "'";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.ExecuteNonQuery();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable("T_STUD");
                dataAdapter.Fill(dt);
                StudentsDataGrid.ItemsSource = dt.DefaultView;
                dataAdapter.Update(dt);
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost; Initial Catalog=CollegeDB; Integrated Security=True;");
            try
            {
                String query = "";
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                if (tb_search.Text == "")
                {
                    query = "SELECT * FROM T_STUD";
                }
                else
                {
                    query = "SELECT * FROM T_STUD where stud_firstname like'" + this.tb_search.Text + "%'";
                }
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.ExecuteNonQuery();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataTable dt = new DataTable("T_STUD");
                dataAdapter.Fill(dt);
                StudentsDataGrid.ItemsSource = dt.DefaultView;
                dataAdapter.Update(dt);
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddStudent addstud = new AddStudent();
            addstud.Show();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Update update = new Update();
            update.Show();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection sqlCon = new SqlConnection(@"Data Source=localhost; Initial Catalog=CollegeDB; Integrated Security=True;");
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                String query = "DELETE FROM T_STUD WHERE STUD_ID='" + tb_search.Text + "' ";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("student removed");
                sqlCon.Close();
                ShowAllStudentS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void StudentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDelete.IsEnabled = true;
            DataGrid dg = sender as DataGrid;
            DataRowView dr = dg.SelectedItem as DataRowView;
            if (dr != null)
            {
                tb_search.Text = dr["STUD_ID"].ToString();

            }


        }

        private void cb_course_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            course = cb_course.SelectedValue.ToString();
            ShowSpecificStudentS(course);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            ShowAllStudentS();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            LoginScreen loginScreen = new LoginScreen();
            loginScreen.Show();
            this.Close();
        }
    }
}
