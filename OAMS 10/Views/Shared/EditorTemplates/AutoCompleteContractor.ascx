<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%= Html.TextBox("", ViewData.TemplateInfo.FormattedModelValue, new { @class = "text-box single-line" }) %>
<script type="text/javascript" language="javascript">
    $(function () {
        $("#<%= ViewData.ModelMetadata.PropertyName %>").autocomplete({
            select: function (event, ui) { $("#ContractorID").val(ui.item.id); },
            source: function (request, response) {
                $.ajax({
                    url: '<%= Url.Content("~/Listing/ListContractor") %>', type: "POST", dataType: "json",
                    data: { searchText: request.term, maxResults: 10, type: "<%= ViewData.ModelMetadata.PropertyName %>" },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return { label: item.Name, value: item.Name, id: item.ID }
                        }))
                    }
                })
            }
        });
    });

    //    $(function () {
    //        var c = "<%= ViewData.ModelMetadata.PropertyName %>";
    //        
    //        SetAutoCompleteForContractor(c, 'ContractorID');
    //    });

</script>
