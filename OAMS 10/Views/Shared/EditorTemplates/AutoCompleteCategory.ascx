﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%= Html.TextBox("", ViewData.TemplateInfo.FormattedModelValue, new { @class = "text-box single-line" }) %>
<style type="text/css">
    .ui-autocomplete
    {
       
    }
</style>
<script type="text/javascript" language="javascript">
    $(function () {
        $("#<%= ViewData.ModelMetadata.PropertyName %>").autocomplete({
            select: function (event, ui) {
                if (typeof showCat2 == 'function') {
                    showCat2(ui.item.value, true, '');
                }
            },
            source: function (request, response) {
                $.ajax({
                    url: '<%= Url.Content("~/Listing/ListCats") %>', type: "POST", dataType: "json",
                    data: { searchText: request.term, level: '<%= ViewData["level"]%>' },
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
