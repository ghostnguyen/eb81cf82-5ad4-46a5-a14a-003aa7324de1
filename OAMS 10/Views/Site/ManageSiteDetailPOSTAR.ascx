﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.Site>" %>
<script type="text/javascript" language="javascript">
    function AddSiteDetailMore(siteDetailID) {
        $.ajax({
            url: '<%= Url.Content("~/SiteDetailMore/Add") %>', type: "GET",
            data: { siteDetailID: siteDetailID },
            success: function (data) {
                $("#divManageSiteDetailMoreList" + siteDetailID).append(data);
            }
        });
    }
</script>
<div id="divManageSiteDetailMore" style="overflow:auto;width:550px;" >
    <table>
        <thead>
            <tr>
                <th>
                    Name
                </th>
                <th>
                    Type
                </th>
                <th>
                    Format
                </th>
                <th>
                    Height
                </th>
                <th>
                    Width
                </th>
                <th>
                    Size
                </th>
                <th>
                </th>
            </tr>
        </thead>
        <tbody id="divManageSiteDetailList">
            <% 
                Session["SiteDetailEditTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.SiteDetailController>("Edit", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxEdit(siteDetailID,'divSiteDetail_siteDetailID','{0}');", Url.Content("~/SiteDetail/Edit")) } }, false);
                Session["SiteDetailDeleteTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.SiteDetailController>("Delete", r => r.Delete(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxDelete2(siteDetailID,'divSiteDetail_siteDetailID','{0}','divSubSiteDetail_siteDetailID');", Url.Content("~/SiteDetail/Delete")) } }, true);
                Session["SiteDetailSaveTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.SiteDetailController>("Save", r => r.Edit(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxSave('divSiteDetail_siteDetailID','{0}');", Url.Content("~/SiteDetail/Edit")) } }, true);
                Session["SiteDetailCancelTemplate"] = Html.ActionLinkWithRoles<OAMS.Controllers.SiteDetailController>("Cancel", r => r.View2(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AjaxView(siteDetailID,'divSiteDetail_siteDetailID','{0}');", Url.Content("~/SiteDetail/View2")) } }, false);
            %>
            <% foreach (var item in Model.SiteDetails)
               { %>
            <% Html.RenderPartial("~/Views/SiteDetail/View.ascx", item); %>
            <% } %>
        </tbody>
    </table>
    <div>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.SiteDetailController>("Add", r => r.Add(0), null, new Dictionary<string, object>() { { "href", string.Format("javascript:AddSiteDetail({0})", Model.ID) } }, false)%>
        <script type="text/javascript" language="javascript">
            function AddSiteDetail(siteID) {
                $.ajax({
                    url: '<%= Url.Content("~/SiteDetail/Add") %>', type: "GET",
                    data: { siteID: siteID },
                    success: function (data) {
                        $("#divManageSiteDetailList").append(data);
                    }
                })
            }
        </script>
    </div>
</div>
