<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%= Html.TextBox("", ViewData.TemplateInfo.FormattedModelValue, new { @class = "text-box single-line" }) %>
<a href="javascript:RemoveContractor();">X</a>
<script type="text/javascript" language="javascript">
    function RemoveContractor() {
        if (confirm('Clear?')) {
            $("#<%= ViewData.ModelMetadata.PropertyName %>").val('');
            $("#ContractorID").val('');
        }
    }
    
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

    

</script>
