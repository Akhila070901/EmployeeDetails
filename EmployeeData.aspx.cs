using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WebGrease.Activities;

namespace WebAppCrudReports
{
    public partial class EmployeeData : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["EmployeeDBConnection"].ConnectionString;


            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    LoadEmployees();
                    ClearFields();
                }

            }

            private void LoadEmployees()
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Employees";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Add Total Salary calculation
                    decimal totalSalary = dt.AsEnumerable().Sum(row => row.Field<decimal>("Salary"));

                    gvEmployees.DataSource = dt;
                    gvEmployees.DataBind();

                    // Set the Footer for Total Salary
                    if (gvEmployees.FooterRow != null)
                    {
                        gvEmployees.FooterRow.Cells[3].Text = "Total Salary:";
                        gvEmployees.FooterRow.Cells[4].Text = totalSalary.ToString("C");
                    }
                }
            }

            protected void btnSave_Click(object sender, EventArgs e)
            {
                if (!Page.IsValid)
                    return;

                string gender = rbMale.Checked ? "M" : "F";


            using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = ViewState["EmployeeID"] != null
                        ? "UPDATE Employees SET Name=@Name, Designation=@Designation, DOJ=@DOJ, Salary=@Salary, Gender=@Gender, State=@State WHERE EmployeeID=@EmployeeID"
                        : "INSERT INTO Employees (Name, Designation, DOJ, Salary, Gender, State) VALUES (@Name, @Designation, @DOJ, @Salary, @Gender, @State)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
                    cmd.Parameters.AddWithValue("@DOJ", DateTime.Parse(txtDOJ.Text));
                    cmd.Parameters.AddWithValue("@Salary", decimal.Parse(txtSalary.Text));
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@State", ddlState.SelectedValue);

                    if (ViewState["EmployeeID"] != null)
                        cmd.Parameters.AddWithValue("@EmployeeID", ViewState["EmployeeID"]);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    // Clear the ViewState after saving
                    ViewState["EmployeeID"] = null;

                    // Clear fields and reload employees
                    ClearFields();
                    LoadEmployees();
                }
            }

            private void ClearFields()
            {
                txtName.Text = string.Empty;
                txtDesignation.Text = string.Empty;
                txtDOJ.Text = string.Empty;
                txtSalary.Text = string.Empty;

                rbMale.Checked = false;
                rbFemale.Checked = false;

                ddlState.SelectedIndex = 0;

                // Also clear the ViewState for the editing state
                ViewState["EmployeeID"] = null;
            }

            protected void ValidateGender(object source, ServerValidateEventArgs args)
            {
                args.IsValid = rbMale.Checked || rbFemale.Checked;
            }

            protected void btnNew_Click(object sender, EventArgs e)
            {
                ClearFields();
                ViewState["EmployeeID"] = null;
            }
            protected void gvEmployees_RowCommand(object sender, GridViewCommandEventArgs e)
            {


                if (e.CommandName == "EditRec")
                {
                    int employeeId = Convert.ToInt32(e.CommandArgument);


                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtName.Text = reader["Name"].ToString();
                            txtDesignation.Text = reader["Designation"].ToString();
                            txtDOJ.Text = Convert.ToDateTime(reader["DOJ"]).ToString("yyyy-MM-dd");
                            txtSalary.Text = reader["Salary"].ToString();

                            if (reader["Gender"].ToString() == "M")
                            {
                                rbMale.Checked = true;
                                rbFemale.Checked = false;
                            }
                            else
                            {
                                rbMale.Checked = false;
                                rbFemale.Checked = true;
                            }

                            ddlState.SelectedValue = reader["State"].ToString();

                            // Save EmployeeID to ViewState for updating
                            ViewState["EmployeeID"] = employeeId;
                        }
                        conn.Close();
                    }
                }
            }

            protected void gvEmployees_RowDeleting(object sender, GridViewDeleteEventArgs e)
            {
                int employeeId = Convert.ToInt32(gvEmployees.DataKeys[e.RowIndex].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    // Reload the updated employee list
                    LoadEmployees();
                }
            }

            //protected void gvEmployees_RowDataBound(object sender, GridViewRowEventArgs e)
            //{
            //    if (e.Row.RowType == DataControlRowType.DataRow)
            //    {
            //        // Calculate total salary
            //        if (ViewState["TotalSalary"] == null)
            //        {
            //            ViewState["TotalSalary"] = 0M;
            //        }
            //        decimal salary = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Salary"));
            //        ViewState["TotalSalary"] = (decimal)ViewState["TotalSalary"] + salary;
            //    }
            //    else if (e.Row.RowType == DataControlRowType.Footer)
            //    {
            //        // Display total salary in the footer row
            //        e.Row.Cells[3].Text = "Total Salary:";
            //        e.Row.Cells[4].Text = ViewState["TotalSalary"].ToString();
            //    }
            //}

        }
    }
