<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.Contract>" %>
<div id="divManageContractTimeline">
    <div id="divManageContractTimelineList">
        <% 
            Session["ContractTimelineEditTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ContractTimelineController>("Edit", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxEdit(contractTimelineID,'divContractTimeline_contractTimelineID','{0}');", Url.Content("~/ContractTimeline/Edit")) } }, false);
            Session["ContractTimelineSaveTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ContractTimelineController>("Save", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxSave('divContractTimeline_contractTimelineID','{0}');", Url.Content("~/ContractTimeline/Edit")) } }, true);
            Session["ContractTimelineCancelTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.ContractTimelineController>("Cancel", r => r.View(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxView(contractTimelineID,'divContractTimeline_contractTimelineID','{0}');", Url.Content("~/ContractTimeline/View")) } }, false);
        %>
        <% foreach (var item in Model.ContractTimelines)
           { %>
        <% Html.RenderPartial("~/Views/ContractTimeline/View.ascx", item); %>
        <% } %>
    </div>
    <div>
        <input id="txtManageContractTimeline_day" type="text" style="width: 50px;" />
        day(s)
        <%--<%: Html.ActionLink("Add", "Add", "ContractTimeline", new { href = string.Format("javascript:AddContractTimeline({0},'{1}')",Model.ID,"day")  })%>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractTimelineController>("Add", r => r.Add(0, 0, ""), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AddContractTimeline({0},'{1}')", Model.ID, "day") } }, true)%>
        <br />
        <input id="txtManageContractTimeline_month" type="text" style="width: 50px;" />
        month(s)
        <%--<%: Html.ActionLink("Add", "Add", "ContractTimeline", new { href = string.Format("javascript:AddContractTimeline({0},'{1}')",Model.ID,"month")  })%>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractTimelineController>("Add", r => r.Add(0, 0, ""), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AddContractTimeline({0},'{1}')", Model.ID, "month") } }, true)%>
        <br />
        <%--<%: Html.ActionLink("Delete last timeline", "DeleteTheLastTimeline", "ContractTimeline", new { href = "javascript:DeleteTheLastTimeline(" + Model.ID + ");" })%>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.ContractTimelineController>("Delete last timeline", r => r.DeleteTheLastTimeline(0), null, new Dictionary<string, object>() { { "href", "javascript:DeleteTheLastTimeline(" + Model.ID + ");" } }, true)%>
        <script type="text/javascript" language="javascript">

            function AddContractTimeline(contractID, timeUnit) {

                var timeCount = 0;
                if (timeUnit == 'day') {
                    timeCount = $('#txtManageContractTimeline_day').val();
                }
                if (timeUnit == 'month') {
                    timeCount = $('#txtManageContractTimeline_month').val();
                }

                $.ajax({
                    url: '<%= Url.Content("~/ContractTimeLine/Add") %>', type: "POST",
                    data: { contractID: contractID, timeCount: timeCount, timeUnit: timeUnit },
                    success: function (data) {

                        $("#divManageContractTimelineList").append(data);
                    }

                })

            }

            function DeleteTheLastTimeline(contractID) {

                $.ajax({
                    url: '<%= Url.Content("~/ContractTimeline/DeleteTheLastTimeline") %>', type: "POST",
                    data: { contractID: contractID },
                    success: function (data) {

                        $("#divManageContractTimeline").empty().append(data);
                    }

                })

            }
        </script>
    </div>
</div>
