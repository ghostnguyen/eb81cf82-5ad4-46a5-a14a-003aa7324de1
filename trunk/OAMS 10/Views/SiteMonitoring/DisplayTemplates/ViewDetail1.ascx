<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<OAMS.Models.SiteMonitoring>" %>
<%@ Import Namespace="OAMS.Models" %>
<table class="notable">
    <tr>
        <td>
            <h1>
                Inspection Report</h1>
        </td>
        <td align="right">
            <img src="<%= Url.Content("~/Content/Image/" + AppSetting.Logo)%>" alt="Ambient" />
            <img src="<%= Url.Content("~/Content/Image/bni_logo.jpg")%>" style="float: right;
                width: 150px;" />
        </td>
    </tr>
    <tr>
        <td valign="top">
            <table class="notable">
                <tr>
                    <td>
                        <table class="notable" style="vertical-align: top;">
                            <tr>
                                <td style="width: 120px;">
                                    Site Monitoring ID:
                                </td>
                                <td style="width: 90px;">
                                    <%: Model.ID %>
                                </td>
                                <td style="width: 140px;">
                                    Site location:
                                </td>
                                <td style="width: 230px;">
                                    <%: Model.ContractDetail.Site.AddressLine1 + " " + Model.ContractDetail.Site.AddressLine2  %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Score
                                </td>
                                <td>
                                    <span style="font-size: large;">
                                        <%: Model.ContractDetail.Site.Score %></span>
                                </td>
                                <td>
                                    Province:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.Geo1.Name%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    District:
                                </td>
                                <td>
                                    <%: (Model.ContractDetail.Site.Geo3 == null ? "" : Model.ContractDetail.Site.Geo3.Name + ", ") + (Model.ContractDetail.Site.Geo2 == null ? "" : Model.ContractDetail.Site.Geo2.Name)%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    In/Outbound to CBD:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.CBDViewed %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                                <td>
                                    Size:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Height %>m x
                                    <%: Model.ContractDetail.Width %>m
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="notable">
                            <tr>
                                <td>
                                    <img alt="Map" src="http://maps.google.com/maps/api/staticmap?center=<%: Model.ContractDetail.Site.Lat%>,<%: Model.ContractDetail.Site.Lng%>&zoom=17&size=300x225&maptype=hybrid&markers=color:red|<%: Model.ContractDetail.Site.Lat%>,<%: Model.ContractDetail.Site.Lng%>&sensor=false" />
                                </td>
                                <%  int i = 0;
                                    bool hasCloseTr = false;
                                    foreach (var item in Model.SiteMonitoringPhotoes)
                                    { %>
                                <%
                                        if (i % 2 == 0)
                                        { 
                                %>
                                <td valign="top">
                                    <img src='<%= item.Url.ToUrlPicasaPhotoResize() %>' alt="" width="300" height="225" />
                                </td>
                            </tr>
                            <%
                                            hasCloseTr = true;
                                        }
                                        else
                                        {
                            %>
                            <tr>
                                <td valign="top">
                                    <img src='<%= item.Url.ToUrlPicasaPhotoResize() %>' alt="" width="300" height="225" />
                                </td>
                                <% 
                                            hasCloseTr = false;
                                        }
                                        i++;
                                %>
                                <% } %>
                                <%
                                    if (!hasCloseTr)
                                    { 
                                %>
                                <td>
                                </td>
                            </tr>
                            <%
                                    }
                            %>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
        <td valign="top">
            <table class="notable">
                <tr>
                    <td>
                        <table class="notable">
                            <tr>
                                <td>
                                    Site Format:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Width >= Model.ContractDetail.Height ? "Horizontal" : "Vertical"%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Current Client:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Contract.ClientName%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Current Creative:
                                </td>
                                <td>
                                    <%: Model.CurrentCreative  %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Media Type:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Type %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Format Execution:
                                </td>
                                <td>
                                    <%--<%: Model.ContractDetail.Site.Format %>--%>
                                    <%: Model.ContractDetail.Format %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Latest Dates</b>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Latest Site Inspection:
                                </td>
                                <td>
                                    <% OAMS.Models.SiteMonitoringRepository repo = new OAMS.Models.SiteMonitoringRepository();
                                       var pre = repo.GetPrevious(Model.ID);%>
                                    <%: pre != null ? pre.LastUpdatedDate.ToString() : "" %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Latest Photo Taken:
                                </td>
                                <td>
                                    <%
                                        DateTime? lastDate = null;
                                        foreach (OAMS.Models.SiteMonitoringPhoto item in Model.SiteMonitoringPhotoes)
                                        {
                                            if (item.TakenDate != null && (lastDate == null || lastDate < item.TakenDate))
                                            {
                                                lastDate = item.TakenDate;
                                            }
                                        }
                                        if (lastDate != null)
                                        { 
                                    %>
                                    <%: lastDate.ToShortDateString() %>
                                    <%
                                        }
                                    %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Lighting</b>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Backlit/Frontlit Vendor:
                                </td>
                                <td>
                                    <%: Model.ContractDetail.Site.FrontlitNumerOfLamps>0?"Frontlit":"Backlit" %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Working:
                                </td>
                                <td>
                                    <%: Model.Working.ToCustomeString() %>
                                </td>
                            </tr>
                            <% if (Model.ContractDetail.Site.FrontlitNumerOfLamps.HasValue
                           && Model.ContractDetail.Site.FrontlitNumerOfLamps > 0)
                               {%>
                            <tr>
                                <td>
                                    No. of bulbs:
                                </td>
                                <td>
                                    <%: Model.NoOfBullbs%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Bulbs working:
                                </td>
                                <td>
                                    <%: Model.BullsWorking%>
                                </td>
                            </tr>
                            <%} %>
                            <tr>
                                <td>
                                    Issues:
                                </td>
                                <td>
                                    <%: Model.Issues%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Status of Site</b>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Clean:
                                </td>
                                <td>
                                    <%: Model.Clean.ToCustomeString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Creative Good Condition:
                                </td>
                                <td>
                                    <%: Model.CreativeGoodConditon.ToCustomeString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    External Interference:
                                </td>
                                <td>
                                    <%: Model.ExternalInterference.ToCustomeString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Vandalism:
                                </td>
                                <td>
                                    <%: Model.Vandalism.ToCustomeString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Comments:
                                </td>
                                <td>
                                    <%: Model.Comments%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Follow up (if any)</b>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Required:
                                </td>
                                <td>
                                    <%: Model.RequiredFollowUp.ToCustomeString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Action:
                                </td>
                                <td>
                                    <%: Model.Action%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date of problem:
                                </td>
                                <td>
                                    <%: Model.DateOfProblem.ToShortDateString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Now fixed:
                                </td>
                                <td>
                                    <%: Model.NowFixed.ToCustomeString() %>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Date fixed:
                                </td>
                                <td>
                                    <%: Model.DateFixed.ToShortDateString() %>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div class="pageBreak">
</div>
