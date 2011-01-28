<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<OAMS.Models.Site>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Index</h2>
    <p>
        <%--<%: Html.ActionLink("Create New", "Create") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.SiteController>("Create New", r => r.Create(null), null, null, false)%>
    </p>
    <table id="tblResult" class="display">
        <thead>
            <tr>
                <th>
                </th>
                <th>
                    ID
                </th>
                <th>
                    Code
                </th>
                <th>
                    Geo
                </th>
                <th>
                    Address Line 1
                </th>
                <th>
                    Address Line 2
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
                    Total
                </th>
                <th>
                    Photo Count
                </th>
            </tr>
        </thead>
        <tbody>
            <% 
                var editTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.SiteController>("Edit", r => r.Edit(0), new RouteValueDictionary(new { id = "siteID" }), null, false);
                var deleteTemplate = Html.ActionLinkWithRoles<OAMS.Controllers.SiteController>("Delete", r => r.Delete(0), new RouteValueDictionary(new { id = "siteID" }), new Dictionary<string,object> (){{"onclick","return confirm('Delete?');"}}, false);
            %>
            <% foreach (var item in Model)
               { %>
            <tr>
                <td>
                    <%--<%: Html.ActionLink("Edit", "Edit", new { id=item.ID }) %>
                    <br />
                    <br />
                    <%: Html.ActionLink("Delete", "Delete", new { id = item.ID }, new { onclick="return confirm('Delete?');" })%>--%>
                    <%: MvcHtmlString.Create(editTemplate.ToString().Replace("siteID", item.ID.ToString()))%>
                    <br />
                    <br />
                    <%: MvcHtmlString.Create(deleteTemplate.ToString().Replace("siteID", item.ID.ToString()))%>
                </td>
                <td>
                    <%: item.ID %>
                </td>
                <td>
                    <%: item.Code %>
                </td>
                <td>
                    <%: item.GeoFullName %>
                </td>
                <td>
                    <%: item.AddressLine1 %>
                </td>
                <td>
                    <%: item.AddressLine2 %>
                </td>
                <td>
                    <%: item.Type %>
                </td>
                <td>
                    <%--<%: item.Format %>--%>
                    <%: string.Join(", ", item.SiteDetails.Select(r => r.Format)) %>
                </td>
                <td>
                    <%--<%: item.CurrentClientName %>--%>
                    <%: string.Join(", ", item.SiteDetails.Where(r => r.Product != null).Select(r => r.Product.NewClientName)) %>
                </td>
                <td>
                    <%--<%: item.CurrentProduct %>--%>
                    <%: string.Join(", ", item.SiteDetails.Where(r => r.Product != null).Select(r => r.Product.Name)) %>
                </td>
                <td>
                    <%: item.Score %>
                </td>
                <td>
                    <%: item.SitePhotoes.Count() %>
                </td>
            </tr>
            <% } %>
        </tbody>
        <tfoot>
            <tr>
                <th>
                </th>
                <th>
                </th>
                <th>
                </th>
                <th>
                </th>
                <th>
                </th>
                <th>
                </th>
                <th>
                </th>
                <th>
                </th>
                <th>
                </th>
                <th>
                </th>
                <th>
                </th>
                <th>
                </th>
            </tr>
        </tfoot>
    </table>
    <br />
    <p>
        <%--<%: Html.ActionLink("Create New", "Create") %>--%>
        <%: Html.ActionLinkWithRoles<OAMS.Controllers.SiteController>("Create New", r => r.Create(null), null, null, false)%>
    </p>
    <script type="text/javascript">


        (function ($) {
            /*
            * Function: fnGetColumnData
            * Purpose:  Return an array of table values from a particular column.
            * Returns:  array string: 1d data array 
            * Inputs:   object:oSettings - dataTable settings object. This is always the last argument past to the function
            *           int:iColumn - the id of the column to extract the data from
            *           bool:bUnique - optional - if set to false duplicated values are not filtered out
            *           bool:bFiltered - optional - if set to false all the table data is used (not only the filtered)
            *           bool:bIgnoreEmpty - optional - if set to false empty values are not filtered from the result array
            * Author:   Benedikt Forchhammer <b.forchhammer /AT\ mind2.de>
            */
            $.fn.dataTableExt.oApi.fnGetColumnData = function (oSettings, iColumn, bUnique, bFiltered, bIgnoreEmpty) {
                // check that we have a column id
                if (typeof iColumn == "undefined") return new Array();

                // by default we only wany unique data
                if (typeof bUnique == "undefined") bUnique = true;

                // by default we do want to only look at filtered data
                if (typeof bFiltered == "undefined") bFiltered = true;

                // by default we do not wany to include empty values
                if (typeof bIgnoreEmpty == "undefined") bIgnoreEmpty = true;

                // list of rows which we're going to loop through
                var aiRows;

                // use only filtered rows
                if (bFiltered == true) aiRows = oSettings.aiDisplay;
                // use all rows
                else aiRows = oSettings.aiDisplayMaster; // all row numbers

                // set up data array	
                var asResultData = new Array();

                for (var i = 0, c = aiRows.length; i < c; i++) {
                    iRow = aiRows[i];
                    var aData = this.fnGetData(iRow);
                    var sValue = aData[iColumn];

                    // ignore empty values?

                    if (typeof sValue == "undefined") continue;

                    if (bIgnoreEmpty == true && sValue.length == 0) continue;
                    // ignore unique values?
                    else if (bUnique == true && jQuery.inArray(sValue, asResultData) > -1) continue;

                    // else push the value onto the result data array
                    else asResultData.push(sValue);
                }

                return asResultData;
            }
        } (jQuery));


        function fnCreateSelect(aData) {

            //alert(typeof (aData.length));

            var r = '<select style="width:55px;"><option value=""></option>', i, iLen = aData.length;
            for (i = 0; i < iLen; i++) {
                r += '<option value="' + aData[i] + '">' + aData[i] + '</option>';
            }
            return r + '</select>';
        }

        $(document).ready(function () {
            /* Initialise the DataTable */
            //            var oTable = $('#example').dataTable({
            //                "oLanguage": {
            //                    "sSearch": "Search all columns:"
            //                }
            //            });

            //var oTable = $('#tblResult').dataTable({ "aaSorting": [[1, "desc"]], "sScrollX": "100%" });
            var oTable = $('#tblResult').dataTable({ "aaSorting": [[1, "desc"]] });

            /* Add a select menu for each TH element in the table footer */
            $("tfoot th").each(function (i) {
                if (i > 5 || i == 3) {
                    this.innerHTML = fnCreateSelect(oTable.fnGetColumnData(i));
                    $('select', this).change(function () {
                        oTable.fnFilter($(this).val(), i);
                    });
                }
            });
        });

    </script>
</asp:Content>
