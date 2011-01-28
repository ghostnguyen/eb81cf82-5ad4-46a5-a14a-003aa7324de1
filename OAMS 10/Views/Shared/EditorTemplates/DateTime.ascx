<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%= Html.TextBox("", String.Format("{0:d}", ViewData.TemplateInfo.FormattedModelValue), new { @class = "text-box single-line" }) %>

<%--<%= Html.TextBox("", ViewData.TemplateInfo.FormattedModelValue, new { @class = "text-box single-line" }) %>--%>

<script type="text/javascript">
    $(function () {
        $("#<%= ViewData.ModelMetadata.PropertyName %>").datepicker({ showAnim: '' });
    });
</script>
