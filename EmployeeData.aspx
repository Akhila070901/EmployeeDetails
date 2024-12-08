<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeData.aspx.cs" Inherits="WebAppCrudReports.EmployeeData" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Info</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .form-table {
            border: 1px solid #ddd;
            border-collapse: collapse;
            width: 100%;
            margin-bottom: 20px;
        }

            .form-table td, .form-table th {
                border: 1px solid #ddd;
                padding: 10px;
            }

            .form-table th {
                background-color: #f8f9fa;
                text-align: left;
            }

        .action-buttons {
            text-align: center;
        }
        .text-danger {
            font-size: 0.9em;
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server"  style="background-color:aliceblue">
        <div class="container mt-4">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            <h3 class="mb-4 text-center">Employee Details</h3>
            

            <table class="form-table w-50 align-content-center">
                  <tr>
                    <th class="text-end">Name</th>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                        <span class="text-danger" id="nameError"></span>
                    </td>
                </tr>
                <tr>
                    <th class="text-end">Designation</th>
                    <td>
                        <asp:TextBox ID="txtDesignation" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                        <span class="text-danger" id="designationError"></span>
                    </td>
                </tr>
               <tr>
    <th class="text-end">Date of Joining</th>
    <td>
        <asp:TextBox ID="txtDOJ" runat="server" CssClass="form-control" Placeholder="dd/MM/yyyy" ClientIDMode="Static"></asp:TextBox>
        <span class="text-danger" id="dojError"></span>
    </td>
</tr>


                    <th class="text-end">Salary</th>
                    <td>
                        <asp:TextBox ID="txtSalary" runat="server" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                        <span class="text-danger" id="salaryError"></span>
                    </td>
                </tr>
                <tr>
                    <th class="text-end">Gender</th>
                    <td>
                        <asp:RadioButton ID="rbMale" runat="server" GroupName="Gender" Text="Male" CssClass="form-check-input me-1" ClientIDMode="Static" />
                        <asp:RadioButton ID="rbFemale" runat="server" GroupName="Gender" Text="Female" CssClass="form-check-input me-1" ClientIDMode="Static" />
                        <span class="text-danger" id="genderError"></span>
                    </td>
                </tr>
                <tr>
                    <th class="text-end">State</th>
                    <td>
                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-select" ClientIDMode="Static">
                            <asp:ListItem Text="Select State" Value="" />
                            <asp:ListItem Text="Andhra Pradesh" Value="Andhra Pradesh" />
                            <asp:ListItem Text="Karnataka" Value="Karnataka" />
                            <asp:ListItem Text="Tamil Nadu" Value="Tamil Nadu" />
                        </asp:DropDownList>
                        <span class="text-danger" id="stateError"></span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="text-center pt-3">
                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="SaveGroup" CssClass="btn btn-primary me-2" OnClick="btnSave_Click" />
                        <asp:Button ID="btnNew" runat="server" Text="New Employee" CssClass="btn btn-secondary" OnClick="btnNew_Click" />
                    </td>
                </tr>
            </table>


            <hr class="my-4" />

            <h3 class="mb-4 text-center">List of Employees</h3>

            <asp:GridView
                ID="gvEmployees"
                runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="EmployeeID"
                CssClass="table table-bordered table-striped"
                OnRowDeleting="gvEmployees_RowDeleting"
                OnRowCommand="gvEmployees_RowCommand"
                ShowFooter="True">
                <Columns>
                    <asp:TemplateField HeaderText="S.No">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:LinkButton
                                ID="lnkEdit"
                                runat="server"
                                CommandName="EditRec"
                                CommandArgument='<%# Eval("EmployeeID") %>'
                                Text='<%# Eval("Name") %>'
                                CssClass="text-primary">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                    <asp:BoundField DataField="DOJ" HeaderText="DOJ" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Salary" HeaderText="Salary" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" />
                    <asp:BoundField DataField="State" HeaderText="State" />
                    <asp:ButtonField CommandName="Delete" Text="Delete" ButtonType="Button" />
                </Columns>
                <FooterStyle CssClass="text-end" />
            </asp:GridView>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

     <script src="https://cdn.jsdelivr.net/npm/jquery-validation@1.19.5/dist/jquery.validate.min.js"></script>
 <script>
     $(document).ready(function () {
         // Custom validation method for dd/MM/yyyy format
         $.validator.addMethod("dateDDMMYYYY", function (value, element) {
             // Regular expression to validate the format
             var regex = /^\d{2}\/\d{2}\/\d{4}$/;
             if (!regex.test(value)) {
                 return false;
             }
             // Split the input and validate date components
             var parts = value.split("/");
             var day = parseInt(parts[0], 10);
             var month = parseInt(parts[1], 10);
             var year = parseInt(parts[2], 10);
             var date = new Date(year, month - 1, day);
             return date.getDate() === day && date.getMonth() === month - 1 && date.getFullYear() === year;
         }, "Please enter a valid date in the format dd/MM/yyyy.");

         // Initialize form validation
         $("#form1").validate({
             rules: {
                 txtName: { required: true },
                 txtDesignation: { required: true },
                 txtDOJ: {
                     required: true,
                     dateDDMMYYYY: true // Custom validation method
                 },
                 txtSalary: { required: true, number: true, min: 1, max: 1000000 },
                 ddlState: { required: true },
                 Gender: { required: true }
             },
             messages: {
                 txtName: "Please enter your name.",
                 txtDesignation: "Please enter your designation.",
                 txtDOJ: {
                     required: "Please enter your date of joining.",
                     dateDDMMYYYY: "Please enter a valid date in the format dd/MM/yyyy."
                 },
                 txtSalary: {
                     required: "Please enter your salary.",
                     number: "Salary must be a number.",
                     min: "Salary must be at least 1.",
                     max: "Salary cannot exceed 1,000,000."
                 },
                 ddlState: "Please select a state.",
                 Gender: "Please select your gender."
             },
             errorPlacement: function (error, element) {
                 error.addClass("text-danger");
                 if (element.attr("name") === "Gender") {
                     $("#genderError").html(error);
                 } else {
                     error.insertAfter(element);
                 }
             }
         });
     });
                    </script>


</body>
</html>
