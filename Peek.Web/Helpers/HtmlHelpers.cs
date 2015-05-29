namespace Peek.Web.Helpers
{
    using System;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;

    public static class HtmlHelpers
    {
        public static MvcHtmlString RawActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName,
            string controllerName = null, object routeValues = null, AjaxOptions ajaxOptions = null, object htmlAttributes = null)
        {
            var placeholder = Guid.NewGuid().ToString();
            var link = ajaxHelper.ActionLink(placeholder, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            var rawLink = MvcHtmlString.Create(link.ToString().Replace(placeholder, linkText));

            return rawLink;
        }  

        public static MvcHtmlString BootstrapSubmitButton(this HtmlHelper helper, string value, object htmlAttributes = null)
        {
            var submitButton = new TagBuilder("button");
            submitButton.AddCssClass("btn btn-primary");
            submitButton.Attributes.Add("type", "submit");
            submitButton.InnerHtml = value;
            submitButton.ApplyAttributes(htmlAttributes);

            return new MvcHtmlString(submitButton.ToString());
        }

        private static void ApplyAttributes(this TagBuilder tagBuilder, object htmlAttributes)
        {
            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                tagBuilder.MergeAttributes(attributes);
            }
        }
    }
}
