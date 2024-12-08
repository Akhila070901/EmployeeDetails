<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeReport.aspx.cs" Inherits="WebAppCrudReport.EmployeeReport" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Reports</title>
    <style>
        body {
            font-family: Arial, sans-serif;
        }
        .buttons {
            margin-bottom: 20px;
        }
         legend {
            font-size: 24px;
            color: #003366; /* Dark blue color for the legend */
            font-weight: bold;
            padding: 0 10px;
        }
          fieldset {
            border: 2px solid #003366; /* Dark blue border */
            border-radius: 10px;
            padding: 20px;
            margin: 0 auto;
            width: 80%; /* Set the width of the fieldset */
        }
          .btn-dark-blue {
    background-color: #003366; /* Dark blue color */
    border-color: #003366; /* Dark blue border */
    color: white; /* White text */
}


    </style>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
        <legend>Employee Report Info</legend>
        <div>
       
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
        </div>
        <br />
        <div class="d-flex gap-10">
   <asp:DropDownList ID="ddlEmployee" runat="server" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged" AutoPostBack="true" CssClass="btn btn-dark-blue" >
                    <asp:ListItem Text="Select Employee" Value="0"  />
                </asp:DropDownList>
                <asp:DropDownList ID="ddlDesignation" runat="server" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged" AutoPostBack="true" CssClass="btn btn-dark-blue">
                    <asp:ListItem Text="Select Designation" Value="0" />
                </asp:DropDownList>
    <asp:Button ID="btnCombinationWise" runat="server" Text="Combination of Designations Wise" OnClick="btnCombinationWise_Click" CssClass="btn btn-dark-blue"  />
    <asp:Button ID="btnHierarchyWise" runat="server" Text="Employee List by Designation Hierarchy" OnClick="btnHierarchyWise_Click" CssClass="btn btn-dark-blue" />
</div>
            <br />
            <br />

        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1351px"></rsweb:ReportViewer>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
            </fieldset>
    </form>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.bundle.min.js"></script>
</body>
</html>