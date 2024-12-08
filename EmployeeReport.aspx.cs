using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebAppCrudReport
{
    public partial class EmployeeReport : System.Web.UI.Page
    {
        private string connectionString = "Data Source=AKHILA\\AKHILA;Initial Catalog=EmployeeData;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Default report to show Employee Wise Report
                ShowEmployeeWiseReport();
                LoadEmployees();
                LoadDesignations();
            }
        }

        // Method to load employees into the employee dropdown
        private void LoadEmployees()
        {
            //var employees = new[] {
            //    new { EmployeeID = 1, EmployeeName = "John Doe" },
            //    new { EmployeeID = 2, EmployeeName = "Jane Smith" },
            //    new { EmployeeID = 3, EmployeeName = "Michael Johnson" },
            //    new { EmployeeID = 4, EmployeeName = "Emily Davis" },
            //    new { EmployeeID = 5, EmployeeName = "David Wilson" },
            //    new { EmployeeID = 6, EmployeeName = "Sarah Brown" },
            //    new { EmployeeID = 7, EmployeeName = "James Miller" },
            //    new { EmployeeID = 8, EmployeeName = "Olivia Garcia" },
            //    new { EmployeeID = 9, EmployeeName = "William Martinez" },
            //    new { EmployeeID = 10, EmployeeName = "Sophia Lee" }
            //};

            //ddlEmployee.DataSource = employees;
            //ddlEmployee.DataTextField = "EmployeeName";  // Display the employee name
            //ddlEmployee.DataValueField = "EmployeeID";  // Use the EmployeeID as the value
            //ddlEmployee.DataBind();
            //ddlEmployee.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Employee", "0"));


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT EmployeeID, Name FROM Employees"; // Adjust table/column names
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlEmployee.DataSource = dt;
                ddlEmployee.DataTextField = "Name"; // Column to display
                ddlEmployee.DataValueField = "EmployeeID";  // Column as value
                ddlEmployee.DataBind();
                ddlEmployee.Items.Insert(0, new ListItem("Select Employee", "0"));
            }


        }

        // Method to load designations into the designation dropdown
        private void LoadDesignations()
        {
            //var designations = new[] {
            //    new { Designation = "Software Engineer" },
            //    new { Designation = "Project Manager" },
            //    new { Designation = "HR Manager" },
            //    new { Designation = "Data Analyst" },
            //    new { Designation = "Software Developer" },
            //    new { Designation = "Quality Assurance" },
            //    new { Designation = "Business Analyst" },
            //    new { Designation = "UI/UX Designer" },
            //    new { Designation = "System Administrator" },
            //    new { Designation = "Product Owner" }
            //};

            //ddlDesignation.DataSource = designations;
            //ddlDesignation.DataTextField = "Designation";  // Display the designation
            //ddlDesignation.DataValueField = "Designation"; // Use the designation as the value
            //ddlDesignation.DataBind();
            //ddlDesignation.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select Designation", "0"));

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT DISTINCT Designation FROM Employees"; // Adjust table/column names
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                ddlDesignation.DataSource = dt;
                ddlDesignation.DataTextField = "Designation"; // Column to display
                ddlDesignation.DataValueField = "Designation"; // Column as value
                ddlDesignation.DataBind();
                ddlDesignation.Items.Insert(0, new ListItem("Select Designation", "0"));
            }
        }
        


        //protected void btnEmployeeWise_Click(object sender, EventArgs e)
        //{
        //    ShowEmployeeWiseReport();
        //}

        //protected void btnDesignationWise_Click(object sender, EventArgs e)
        //{
        //    ShowDesignationWiseReport();
        //}

        protected void btnCombinationWise_Click(object sender, EventArgs e)
        {
            ShowCombinationWiseReport();
        }

        protected void btnHierarchyWise_Click(object sender, EventArgs e)
        {
            ShowHierarchyWiseReport();
        }
        private void ShowEmployeeWiseReport()
        {
            // Create SQL connection and adapter
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter("GetEmployeeWiseReport", conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                
            // Create and fill DataSet
            DataSet ds = new DataSet();
            adapter.Fill(ds, "EmployeeWiseReport");

            // Bind to ReportViewer
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables["EmployeeWiseReport"]);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report1.rdlc");

            // Refresh ReportViewer
            ReportViewer1.LocalReport.Refresh();
        }

        //private void ShowDesignationWiseReport()
        //{
        //    // Create SQL connection and adapter
        //    SqlConnection conn = new SqlConnection(connectionString);
        //    SqlDataAdapter adapter = new SqlDataAdapter("GetDesignationWiseReport", conn);
        //    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

        //    // Create and fill DataSet
        //    DataSet ds = new DataSet();
        //    adapter.Fill(ds, "DesignationWiseReport");

        //    // Bind to ReportViewer
        //    ReportViewer1.LocalReport.DataSources.Clear();
        //    ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables["DesignationWiseReport"]);
        //    ReportViewer1.LocalReport.DataSources.Add(rds);
        //    ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report1.rdlc");

        //    // Refresh ReportViewer
        //    ReportViewer1.LocalReport.Refresh();
        //}

        private void ShowCombinationWiseReport()
        {
            // Create SQL connection and adapter
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter("GetCombinationWiseReport", conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Create and fill DataSet
            DataSet ds = new DataSet();
            adapter.Fill(ds, "CombinationWiseReport");

            // Bind to ReportViewer
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables["CombinationWiseReport"]);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report1.rdlc");

            // Refresh ReportViewer
            ReportViewer1.LocalReport.Refresh();
        }

        private void ShowHierarchyWiseReport()
        {
            // Create SQL connection and adapter
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataAdapter adapter = new SqlDataAdapter("GetDesignationHierarchy", conn);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            // Create and fill DataSet
            DataSet ds = new DataSet();
            adapter.Fill(ds, "HierarchyWiseReport");

            // Bind to ReportViewer
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rds = new ReportDataSource("DataSet1", ds.Tables["HierarchyWiseReport"]);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report1.rdlc");

            // Refresh ReportViewer
            ReportViewer1.LocalReport.Refresh();
        }

        protected void ddlEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEmployee.SelectedValue != "0")
            {
                int selectedEmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
                DataTable employeeData = GetEmployeeData(selectedEmployeeId);

                // Pass the employee data to the ReportViewer
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", employeeData);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report1.rdlc");
                ReportViewer1.LocalReport.Refresh();
            }
        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDesignation.SelectedValue != "0")
            {
                string selectedDesignation = ddlDesignation.SelectedValue;
                DataTable designationData = GetDesignationData(selectedDesignation);

                // Pass the designation data to the ReportViewer
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rds = new ReportDataSource("DataSet1", designationData);
                ReportViewer1.LocalReport.DataSources.Add(rds);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report1.rdlc");
                ReportViewer1.LocalReport.Refresh();
            }
        }

        // Method to get employee data based on the selected employee ID
        private DataTable GetEmployeeData(int employeeId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees WHERE EmployeeID = @EmployeeID"; // Adjust this query as needed
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@EmployeeID", employeeId);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Method to get designation data based on the selected designation
        private DataTable GetDesignationData(string designation)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employees WHERE Designation = @Designation"; // Adjust this query as needed
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@Designation", designation);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
    }
}