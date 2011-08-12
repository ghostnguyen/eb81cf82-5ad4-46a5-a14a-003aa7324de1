<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OAMS.Models.Contract>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">
        var oTable;
        $(document).ready(function () {
            oTable = $('#tblResult').dataTable({ "aaSorting": [[0, "desc"]],
                "iDisplayLength": -1,
                "sDom": 'C<"clear">lfrtip',
                "aLengthMenu": [[25, 50, 100, -1], [25, 50, 100, "All"]]
            });

            var byName = $.getUrlVar('hl');
            //alert(byName);
            $("table.display tr:contains('" + byName + "')").addClass("selected");

        });
    </script>
    <h2>
        Edit</h2>
    <fieldset>
        <legend>View report summary</legend>
        <div id="divSummary">
            from
            <input class="text-box single-line" id="dFrom" name="dFrom" type="text" value="<%: DateTime.Now.ToShortDateString() %>" />
            <script type="text/javascript">
                $(function () {
                    $("#dFrom").datepicker({ showAnim: '' });
                });
            </script>
            to
            <input class="text-box single-line" id="dTo" name="dTo" type="text" value="<%: DateTime.Now.ToShortDateString() %>" />
            <script type="text/javascript">
                $(function () {
                    $("#dTo").datepicker({ showAnim: '' });
                });
            </script>
            of
            <%--<button id="btnView" onclick="btnView_Click()">
                View</button>
            |
            <button id="btnViewDetail" onclick="btnViewDetail_Click()">
                View detail</button>--%>
            <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("View", r => r.ViewReport(0, null, null), null, new Dictionary<string, object>() { { "href", "javascript:btnView_Click();" } }, false)%>
            |
            <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("View detail", r => r.ViewReportDetail(0, null, null,null), null, new Dictionary<string, object>() { { "href", "javascript:btnViewDetail_Click('true');" } }, false)%>
            |
            <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("View detail - BlueTrak", r => r.ViewReportDetail(0, null, null,null), null, new Dictionary<string, object>() { { "href", "javascript:btnViewDetail_Click('');" } }, false)%>
        </div>
        <% string urlRptSum = Url.Action("ViewReport", "Contract", new { id = Model.ID });
           string urlRptDetail = Url.Action("ViewReportDetail", "Contract", new { id = Model.ID });%>
        <script type="text/javascript">
            function btnView_Click() {
                var url = '<%: urlRptSum %>' + "?" + $('#divSummary input').serialize();
                window.open(url);
            }
            function btnViewDetail_Click(old) {
                var url = '<%: urlRptDetail %>' + "?old=" + old + "&" + $('#divSummary input').serialize();
                window.open(url);
            }
        </script>
    </fieldset>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <div style="overflow: auto;">
        <table>
            <tr valign="top">
                <td>
                    <fieldset>
                        <legend>Fields</legend>
                        <%: Html.HiddenFor(model => model.ID) %>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.Number) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(model => model.Number) %>
                            <%: Html.ValidationMessageFor(model => model.Number) %>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.ContractType) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.CodeMasterDropDownListFor(model => model.ContractType) %>
                            <%: Html.ValidationMessageFor(model => model.ContractType) %>
                        </div>
                        <div class="editor-label">
                            Supplier
                        </div>
                        <div class="editor-field">
                            <%: Html.EditorFor(model => model.ContractorName, "AutoCompleteContractor")%>
                            <%: Html.ValidationMessageFor(r => r.ContractorName)%>
                            <%: Html.TextBoxFor(model => model.ContractorID, new { @style = "visibility:collapse;" })%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.ClientName)%>
                        </div>
                        <div class="editor-field">
                            <%: Html.EditorFor(model => model.ClientName, "AutoCompleteClient")%>
                            <%: Html.ValidationMessageFor(r => r.ClientName)%>
                            <%: Html.TextBoxFor(model => model.ClientID, new { @style = "visibility:collapse;" })%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.SignedDate) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.EditorFor(model => model.SignedDate) %>
                            <%: Html.ValidationMessageFor(model => model.SignedDate) %>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.ExpiredDate) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.EditorFor(model => model.ExpiredDate)%>
                            <%: Html.ValidationMessageFor(model => model.ExpiredDate)%>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.InspectionFrequency) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(model => model.InspectionFrequency) %>
                            <%: Html.ValidationMessageFor(model => model.InspectionFrequency) %>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.ReportOn) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(model => model.ReportOn) %>
                            <%: Html.ValidationMessageFor(model => model.ReportOn) %>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.Value) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(model => model.Value) %>
                            <%: Html.ValidationMessageFor(model => model.Value) %>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.PaymentTerm) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(model => model.PaymentTerm) %>
                            <%: Html.ValidationMessageFor(model => model.PaymentTerm) %>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.ContactName1) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(model => model.ContactName1) %>
                            <%: Html.ValidationMessageFor(model => model.ContactName1) %>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.Phone1) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(model => model.Phone1) %>
                            <%: Html.ValidationMessageFor(model => model.Phone1) %>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.Email1) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(model => model.Email1) %>
                            <%: Html.ValidationMessageFor(model => model.Email1) %>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.ContactName2) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(model => model.ContactName2) %>
                            <%: Html.ValidationMessageFor(model => model.ContactName2) %>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.Phone2) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(model => model.Phone2) %>
                            <%: Html.ValidationMessageFor(model => model.Phone2) %>
                        </div>
                        <div class="editor-label">
                            <%: Html.LabelFor(model => model.Email2) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(model => model.Email2) %>
                            <%: Html.ValidationMessageFor(model => model.Email2) %>
                        </div>
                        <div>
                            <% Html.RenderPartial("ManageContractTimeline", Model); %>
                        </div>
                        <div>
                            <br />
                            <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("Overwrite timelines to detail", r => r.OverwriteTimelineForDetail(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:OverwriteTimelineForDetail({0})", Model.ID) } }, true)%>
                        </div>
                        <p>
                            <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("Save", r => r.Edit(0), null, null, true)%>
                        </p>
                    </fieldset>
                </td>
                <td>
                    <%: Html.ActionLinkWithRoles<OAMS.Controllers.FindSiteController>("Add Sites", r => r.Find4Contract(0), new RouteValueDictionary(new { ContractID = Model.ID }), null, false)%>
                    <br />
                    <div>
                        <table id="tblResult" class="display">
                            <thead>
                                <tr>
                                    <th>
                                        ID
                                    </th>
                                    <th>
                                    </th>
                                    <th>
                                        Monitoring
                                    </th>
                                    <th>
                                        Site ID
                                    </th>
                                    <th>
                                        Price
                                    </th>
                                    <th>
                                        Production Price
                                    </th>
                                    <th>
                                        Effective Date
                                    </th>
                                    <th>
                                        Term Date
                                    </th>
                                    <th>
                                        Geo Full Name
                                    </th>
                                    <th>
                                        Address Line1
                                    </th>
                                    <th>
                                        Address Line2
                                    </th>
                                    <th>
                                        Type
                                    </th>
                                    <th>
                                        Format
                                    </th>
                                    <th>
                                        Current Client
                                    </th>
                                    <th>
                                        Current Product
                                    </th>
                                    <th>
                                        Contractor
                                    </th>
                                    <th>
                                        Photo Count
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <% 
                               var editTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ContractDetailController>("Edit", r => r.Edit(0), new RouteValueDictionary(new { id = "contractDetailID" }), null, false);
                               var removeTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.ContractDetailController>("Remove", r => r.Delete(0), new RouteValueDictionary(new { id = "contractDetailID" }), new Dictionary<string, object>() { { "onclick", "return confirm('Delete?');" } }, false);
                                %>
                                <% 
           foreach (var item in Model.ContractDetails)
           { %>
                                <tr>
                                    <td>
                                        <%:item.ID %>
                                    </td>
                                    <td>
                                        <%: MvcHtmlString.Create(editTemplate.ToString().Replace("contractDetailID", item.ID.ToString()))%>
                                        <br />
                                        <br />
                                        <%: MvcHtmlString.Create(removeTemplate.ToString().Replace("contractDetailID", item.ID.ToString()))%>
                                    </td>
                                    <td>
                                        <div style='float: left;'>
                                            <%: Html.ActionLinkWithRoles<OAMS.Controllers.SiteMonitoringController>("New", r => r.Create(0), new RouteValueDictionary(new { ContractDetailID = item.ID }), null, false)%>
                                            <% 
               var smEditTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.SiteMonitoringController>("order", r => r.Edit(0), new RouteValueDictionary(new { id = "siteMonitoringID" }), null, false);
               var smRedEditTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.SiteMonitoringController>("order", r => r.Edit(0), new RouteValueDictionary(new { id = "siteMonitoringID" }), new Dictionary<string, object>() { { "style", "color:Red;" } }, false);
                                            %>
                                            <% 
               foreach (var sm in item.SiteMonitorings)
               {
                                            %>
                                            <%: "|" %>
                                            <% if (!string.IsNullOrEmpty(sm.Issues) || sm.IssuesCount.HasValue)
                                               {%>
                                            <%--<%: Html.ActionLink(sm.Order.ToStringOrDefault(), "Edit", "SiteMonitoring", new { id = sm.ID }, new { style="color:Red;" })%>--%>
                                            <%: MvcHtmlString.Create(smRedEditTemplate.ToString().Replace("siteMonitoringID", sm.ID.ToString()).Replace("order", sm.Order.ToStringOrDefault()))%>
                                            <% }
                                               else
                                               {%>
                                            <%--<%: Html.ActionLink(sm.Order.ToStringOrDefault(), "Edit", "SiteMonitoring", new { id = sm.ID }, null)%>--%>
                                            <%: MvcHtmlString.Create(smEditTemplate.ToString().Replace("siteMonitoringID", sm.ID.ToString()).Replace("order", sm.Order.ToStringOrDefault()))%>
                                            <%} %>
                                            <% if (sm.SiteMonitoringPhotoes.Count > 0)
                                               {%>
                                            <% if (sm.HasInvalidPhoto)
                                               { %>
                                            <span style="color: Red;">
                                                <%: "(" + sm.SiteMonitoringPhotoes.Count.ToString() + ")"%>
                                            </span>
                                            <% }
                                               else
                                               { %>
                                            <%: "(" + sm.SiteMonitoringPhotoes.Count.ToString() + ")"%>
                                            <% } %>
                                            <% } %>
                                            <% if (sm.RequiredFollowUp != null && sm.RequiredFollowUp.Value)
                                               { %>
                                            <img border='0' src='<%= Url.Content("~/Content/Image/exclamation.gif")%>' alt="RequiredFollowUp" />
                                            <% } %>
                                            <% } %>
                                        </div>
                                    </td>
                                    <td>
                                        <%: item.SiteID %>
                                    </td>
                                    <td>
                                        <%: String.Format("{0:c}", item.Price) %>
                                    </td>
                                    <td>
                                        <%: String.Format("{0:c}", item.ProductionPrice) %>
                                    </td>
                                    <td>
                                        <%: String.Format("{0:d}", item.EffectiveDate) %>
                                    </td>
                                    <td>
                                        <%: String.Format("{0:d}", item.TermDate) %>
                                    </td>
                                    <td>
                                        <%: item.Site.GeoFullName %>
                                    </td>
                                    <td>
                                        <%: item.Site.AddressLine1 %>
                                    </td>
                                    <td>
                                        <%: item.Site.AddressLine2 %>
                                    </td>
                                    <td>
                                        <%: item.Type %>
                                    </td>
                                    <td>
                                        <%: item.Format %>
                                    </td>
                                    <td>
                                        <%: Model.ClientName %>
                                    </td>
                                    <td>
                                        <%: item.Product == null ? "" : item.Product.Name%>
                                    </td>
                                    <td>
                                        <%: item.Site.ContractorName %>
                                    </td>
                                    <td>
                                        <%: item.Site.SitePhotoes.Count %>
                                    </td>
                                </tr>
                                <% } %>
                            </tbody>
                        </table>
                    </div>
                    <div id="divEditDetail">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <% } %>
    <div>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("Back to List", r => r.Index(), null, null, false)%>
    </div>
    <script type="text/javascript" language="javascript">

        function EditDetail(contractDetailID) {

            $.ajax({
                url: '<%= Url.Content("~/ContractDetail/Edit/") %>' + contractDetailID, type: "POST",
                success: function (data) {
                    $("#divEditDetail").html(data).dialog({ modal: true });

                }
            })

        };

        function OverwriteTimelineForDetail(contractID) {

            if (confirm('Overwrite all timeline details?')) {
                $.ajax({
                    url: '<%= Url.Content("~/Contract/OverwriteTimelineForDetail") %>', type: "POST",
                    data: { id: contractID },
                    success: function (data) {
                        //$("#divManageContractTimeline").empty().append(data);
                        alert('Overwrite done.');
                    }

                })
            }

        }

    </script>
</asp:Content>
