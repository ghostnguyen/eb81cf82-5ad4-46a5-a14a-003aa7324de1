<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.Rpt01_Model_SiteMonitoring>" %>
<table class="notable" width="100%">
    <tr>
        <td>
            <h1>
                Inspection Report
            </h1>
            <table class="notable">
                <tr>
                    <td>
                        Client:
                        <%: Model.ClientName %>
                    </td>
                    <td>
                        Campaign Date:
                        <%: Model.From.ToShortDateString() %>
                        -
                        <%: Model.To.ToShortDateString() %>
                    </td>
                </tr>
                <tr>
                    <td>
                        Campaign:
                    </td>
                    <td>
                        Reporting Period
                    </td>
                </tr>
            </table>
        </td>
        <td>
            <div style="text-align: right;">
                <img src="<%= Url.Content("~/Content/Image/ambient.png")%>" alt="Ambient" />
                <h2>
                    MONITORING</h2>
            </div>
        </td>
    </tr>
</table>
<table>
    <thead>
        <tr style="background-color: #0000FF; color: #FFFFFF; text-align: center;">
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td colspan="2">
                Latest Dates
            </td>
            <td colspan="5">
                Lighting
            </td>
            <td colspan="4">
                Status of Creative
            </td>
            <td>
            </td>
            <td colspan="6">
                Follow Up
            </td>
            <td>
            </td>
        </tr>
        <tr style="background-color: #0000FF; color: #FFFFFF; text-align: center;">
            <td>
                #
            </td>
            <td>
                Site Code
            </td>
            <td>
                Supplier
            </td>
            <td>
                Province
            </td>
            <td>
                District
            </td>
            <td>
                Street
            </td>
            <td>
                Size(W x H)
            </td>
            <td>
                Client
            </td>
            <td>
                Product
            </td>
            <td>
                Creative
            </td>
            <td>
                Advertising Type
            </td>
            <td>
                Format Execution
            </td>
            <td>
                Inspect
            </td>
            <td>
                Photo
            </td>
            <td>
                Back/Front
            </td>
            <td>
                Working
            </td>
            <td>
                No. of Bulbs
            </td>
            <td>
                Bulbs Working
            </td>
            <td>
                Issue
            </td>
            <td>
                Clean
            </td>
            <td>
                Creative Good Condition
            </td>
            <td>
                External Interference
            </td>
            <td>
                Vandalism
            </td>
            <td>
                Additional Comments / Follow Up
            </td>
            <td>
                Required
            </td>
            <td>
                Action
            </td>
            <td>
                Date
            </td>
            <td>
                Now Fixed
            </td>
            <td>
                Date
            </td>
            <td>
                Photo
            </td>
            <td>
                Additional Comments / Follow Up
            </td>
        </tr>
    </thead>
    <% 
        foreach (var cd in Model.Details)
        {
    %>
    <tr>
        <td>
            <%: cd.ID%>
        </td>
        <td>
            <%: cd.SiteCode%>
        </td>
        <td>
            <%: cd.Supplier%>
        </td>
        <td>
            <%: cd.Province%>
        </td>
        <td>
            <%: cd.District%>
        </td>
        <td>
            <%: cd.Street%>
        </td>
        <td>
            <%: cd.Size%>
        </td>
        <td>
            <%: cd.Client%>
        </td>
        <td>
            <%: cd.Product%>
        </td>
        <td>
        </td>
        <td>
            <%: cd.SiteType%>
        </td>
        <td>
            <%: cd.SiteFormat%>
        </td>
        <td>
            <%: cd.LastestPhotoDate.ToShortDateString()%>
        </td>
        <td>
            <%: cd.LastestPhotoDate.ToShortDateString()%>
        </td>
        <td>
            <%: cd.BackFront != null ? "Backlit" : "Frontlit"%>
        </td>
        <td>
            <%: cd.Working.ToCustomeString()%>
        </td>
        <td>
            <%: cd.NumOfBulbs%>
        </td>
        <td>
            <%: cd.BulbsWorking%>
        </td>
        <td>
            <%: cd.Issue%>
        </td>
        <td>
            <%: cd.Clean.ToCustomeString()%>
        </td>
        <td>
            <%: cd.CreativeGoodConditon.ToCustomeString()%>
        </td>
        <td>
            <%: cd.ExternalInterference.ToCustomeString()%>
        </td>
        <td>
            <%: cd.Vandalism.ToCustomeString()%>
        </td>
        <td>
            <%: cd.Comments%>
        </td>
        <td>
            <%: cd.RequiredFollowUp.ToCustomeString()%>
        </td>
        <td>
            <%: cd.Action%>
        </td>
        <td>
            <%: cd.DateOfProblem.ToShortDateString()%>
        </td>
        <td>
            <%: cd.NowFixed.ToCustomeString()%>
        </td>
        <td>
            <%: cd.DateFixed.ToShortDateString() %>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <% } %>
    <tr style="background-color: #0000FF; color: #FFFFFF; text-align: center;">
        <td>
            Total
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
            <%: Model.TotalWorking %>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
            <%: Model.TotalIssues %>
        </td>
        <td>
            <%: Model.TotalClean %>
        </td>
        <td>
            <%: Model.TotalCreativeGoodCondition %>
        </td>
        <td>
            <%: Model.TotalExternnalInterference %>
        </td>
        <td>
            <%: Model.TotalVandalism %>
        </td>
        <td>
        </td>
        <td>
            <%: Model.TotalRequiredFollowUp %>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
            <%: Model.TotalNowFixed %>
        </td>
        <td>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>
