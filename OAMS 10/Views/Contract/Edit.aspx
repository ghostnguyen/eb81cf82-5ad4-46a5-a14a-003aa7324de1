<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OAMS.Models.Contract>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript" language="javascript">
        var oTable;
        $(document).ready(function () {
            oTable = $('#tblResult').dataTable({ "aaSorting": [[0, "desc"]], "iDisplayLength": 20 });
            ShowHideCols();
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
            <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("View detail", r => r.ViewReportDetail(0, null, null), null, new Dictionary<string, object>() { { "href", "javascript:btnViewDetail_Click();" } }, false)%>
        </div>
        <% string urlRptSum = Url.Action("ViewReport", "Contract", new { id = Model.ID });
           string urlRptDetail = Url.Action("ViewReportDetail", "Contract", new { id = Model.ID });%>
        <script type="text/javascript">
            function btnView_Click() {
                var url = '<%: urlRptSum %>' + "?" + $('#divSummary input').serialize();
                window.open(url);
            }
            function btnViewDetail_Click() {
                var url = '<%: urlRptDetail %>' + "?" + $('#divSummary input').serialize();
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
                            <%: Html.LabelFor(model => model.ContractName2) %>
                        </div>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(model => model.ContractName2) %>
                            <%: Html.ValidationMessageFor(model => model.ContractName2) %>
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
                            <%--<%: Html.ActionLink("Overwrite timelines to detail", "OverwriteTimelineForDetail", "Contract", new { href = string.Format("javascript:OverwriteTimelineForDetail({0})", Model.ID) })%>--%>
                            <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("Overwrite timelines to detail", r => r.OverwriteTimelineForDetail(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:OverwriteTimelineForDetail({0})", Model.ID) } }, true)%>
                        </div>
                        <p>
                            <%--<input type="submit" value="Save" />--%>
                            <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("Save", r => r.Edit(0), null, null, true)%>
                        </p>
                    </fieldset>
                </td>
                <td>
                    <%--<%: Html.ActionLink("Add Sites", "Find4Contract", "FindSite", new { ContractID = Model.ID},null)%>--%>
                    <%: Html.ActionLinkWithRoles<OAMS.Controllers.FindSiteController>("Add Sites", r => r.Find4Contract(0), new RouteValueDictionary(new { ContractID = Model.ID }), null, false)%>
                    <div id="divCol" style="overflow: auto;">
                        Display columns:
                        <input type="checkbox" id="chkColID" checked="checked" />
                        ID
                        <input type="checkbox" id="chkColPrice" />
                        Price
                        <input type="checkbox" id="chkColProductionPrice" />
                        Production Price
                        <input type="checkbox" id="chkColEffectiveDate" />
                        Effective Date
                        <input type="checkbox" id="chkColTermDate" />
                        Term Date
                        <br />
                        <input type="checkbox" id="chkColGeoFullName" checked="checked" />
                        Geo Full Name
                        <input type="checkbox" id="chkColAddressLine1" checked="checked" />
                        Address Line 1
                        <input type="checkbox" id="chkColAddressLine2" checked="checked" />
                        Address Line 2
                        <input type="checkbox" id="chkColType" checked="checked" />
                        Type
                        <input type="checkbox" id="chkColFormat" checked="checked" />
                        Format
                        <br />
                        <input type="checkbox" id="chkColCurrentClient" checked="checked" />
                        Current Client
                        <input type="checkbox" id="chkColCurrentProduct" checked="checked" />
                        Current Product
                        <input type="checkbox" id="chkColContractor" checked="checked" />
                        Contractor
                        <input type="checkbox" id="chkColPhotoCount" checked="checked" />
                        PhotoCount
                        <input type="button" id="hell" value="Ok" onclick='ShowHideCols();' />
                    </div>
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
                                        <%--<%: Html.ActionLink("Edit", "Edit", "ContractDetail", new { id=item.ID }, null) %>
                                        <br />
                                        <br />
                                        <%: Html.ActionLink("Remove", "Delete", "ContractDetail", new { id=item.ID }, new { onclick="return confirm('Delete?');" }) %>--%>
                                        <%: MvcHtmlString.Create(editTemplate.ToString().Replace("contractDetailID", item.ID.ToString()))%>
                                        <br />
                                        <br />
                                        <%: MvcHtmlString.Create(removeTemplate.ToString().Replace("contractDetailID", item.ID.ToString()))%>
                                    </td>
                                    <td>
                                        <div style='float: left;'>
                                            <%--<%: Html.ActionLink("New", "Create", "SiteMonitoring",  new { ContractDetailID=item.ID },null) %>--%>
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
                                        <%: item.Site.Type %>
                                    </td>
                                    <td>
                                        <%: item.Format %>
                                    </td>
                                    <td>
                                        <%: Model.ClientName %>
                                    </td>
                                    <td>
                                        <%: item.Product %>
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
        <%--<%: Html.ActionLink("Back to List", "Index") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractController>("Back to List", r => r.Index(), null, null, false)%>
    </div>
    <script type="text/javascript" language="javascript">
        function ShowHideCols() {
            oTable.fnSetColumnVis(3, $('#chkColID').attr('checked'));
            oTable.fnSetColumnVis(4, $('#chkColPrice').attr('checked'));
            oTable.fnSetColumnVis(5, $('#chkColProductionPrice').attr('checked'));
            oTable.fnSetColumnVis(6, $('#chkColEffectiveDate').attr('checked'));
            oTable.fnSetColumnVis(7, $('#chkColTermDate').attr('checked'));
            oTable.fnSetColumnVis(8, $('#chkColGeoFullName').attr('checked'));
            oTable.fnSetColumnVis(9, $('#chkColAddressLine1').attr('checked'));
            oTable.fnSetColumnVis(10, $('#chkColAddressLine2').attr('checked'));
            oTable.fnSetColumnVis(11, $('#chkColType').attr('checked'));
            oTable.fnSetColumnVis(12, $('#chkColFormat').attr('checked'));
            oTable.fnSetColumnVis(13, $('#chkColCurrentClient').attr('checked'));
            oTable.fnSetColumnVis(14, $('#chkColCurrentProduct').attr('checked'));
            oTable.fnSetColumnVis(15, $('#chkColContractor').attr('checked'));
            oTable.fnSetColumnVis(16, $('#chkColPhotoCount').attr('checked'));
        }

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
