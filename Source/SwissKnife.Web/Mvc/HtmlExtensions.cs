using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using SwissKnife.Diagnostics.Contracts;

namespace SwissKnife.Web.Mvc // TODO-IG: All types in this namespace are added because of an urgent need. Review and refactoring is needed. Originally developed by Marin Roncevic.
{
    public static class HtmlExtensions
    {
        public static IHtmlString Or(this HtmlHelper htmlHelper, Func<string> firstHtmlFunction, Func<string> secondHtmlFunction, bool condition)
        {
            var result = condition ? (firstHtmlFunction != null ? firstHtmlFunction() : null) : (secondHtmlFunction != null ? secondHtmlFunction() : null);

            return new HtmlString(result);
        }

        public static IHtmlString Or(this HtmlHelper htmlHelper, Func<IHtmlString> firstHtmlFunction, Func<IHtmlString> secondHtmlFunction, bool condition)
        {
            return condition ? (firstHtmlFunction != null ? firstHtmlFunction() : null) : (secondHtmlFunction != null ? secondHtmlFunction() : null);
        }

        public static string JavaScriptEncode(this HtmlHelper helper, string value)
        {
            return HttpUtility.JavaScriptStringEncode(value);
        }

        public static MvcForm BeginForm(this HtmlHelper htmlHelper, params string[] attributes)
        {
            return htmlHelper.BeginForm(FormMethod.Post, attributes);
        }

        public static MvcForm BeginForm(this HtmlHelper htmlHelper, FormMethod formMethod, params string[] attributes)
        {
            var dictionary = new RouteValueDictionary();

            var queryString = htmlHelper.ViewContext.HttpContext.Request.QueryString;

            foreach (var key in queryString.AllKeys)
            {
                dictionary[key] = queryString[key];
            }

            return htmlHelper.BeginForm(null, null, dictionary, formMethod, CreateAttributesDictionaryFromArray(attributes));
        }

        public static MvcHtmlString TextBoxFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression)
        {
            return htmlHelper.TextBoxFor(expression, null);
        }

        public static MvcHtmlString TextBoxFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression, object value)
        {
            return htmlHelper.TextBoxFor(expression, value, new string[] { });
        }

        public static MvcHtmlString TextBoxFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression, object value, params string[] attributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.TextBox(name, value, attributes);
        }

        public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name, object value, params string[] attributes)
        {
            return htmlHelper.TextBox(name, value, CreateAttributesDictionaryFromArray(attributes));
        }

        public static MvcHtmlString TextAreaFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression)
        {
            return htmlHelper.TextAreaFor(expression, null);
        }

        public static MvcHtmlString TextAreaFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression, string value)
        {
            return htmlHelper.TextAreaFor(expression, value, new string[] { });
        }

        public static MvcHtmlString TextAreaFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression, string value, params string[] attributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.TextArea(name, value, CreateAttributesDictionaryFromArray(attributes));
        }

        public static MvcHtmlString TextAreaFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression, string value, int rows, int columns, params string[] attributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.TextArea(name, value, rows, columns, attributes);
        }

        public static MvcHtmlString TextArea(this HtmlHelper htmlHelper, string name, string value, int rows, int columns, params string[] attributes)
        {
            return htmlHelper.TextArea(name, value, rows, columns, CreateAttributesDictionaryFromArray(attributes));
        }

        public static MvcHtmlString PasswordFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression)
        {
            return htmlHelper.PasswordFor(expression, null);
        }

        public static MvcHtmlString PasswordFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression, object value)
        {
            return htmlHelper.PasswordFor(expression, value, new string[] { });
        }

        public static MvcHtmlString PasswordFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression, object value, params string[] attributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.Password(name, value, attributes);
        }

        public static MvcHtmlString Password(this HtmlHelper htmlHelper, string name, object value, params string[] attributes)
        {
            return htmlHelper.Password(name, value, CreateAttributesDictionaryFromArray(attributes));
        }

        public static MvcHtmlString CheckBoxFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression, bool isChecked, params string[] attributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.CheckBox(name, isChecked, attributes);
        }

        public static MvcHtmlString CheckBox(this HtmlHelper htmlHelper, string name, bool isChecked, params string[] attributes)
        {
            return htmlHelper.CheckBox(name, isChecked, CreateAttributesDictionaryFromArray(attributes));
        }

        public static MvcHtmlString RadioButtonFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression, object value, bool isChecked, params string[] attributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.RadioButton(name, value, isChecked, attributes);
        }

        public static MvcHtmlString RadioButton(this HtmlHelper htmlHelper, string name, object value, bool isChecked, params string[] attributes)
        {
            return htmlHelper.RadioButton(name, value, isChecked, CreateAttributesDictionaryFromArray(attributes));
        }

        public static MvcHtmlString HiddenFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression, object value, params string[] attributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.Hidden(name, value, attributes);
        }

        public static MvcHtmlString Hidden(this HtmlHelper htmlHelper, string name, object value, params string[] attributes)
        {
            return htmlHelper.Hidden(name, value, CreateAttributesDictionaryFromArray(attributes));
        }

        public static MvcHtmlString DropDownListFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, params string[] attributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.DropDownList(name, selectList, optionLabel, attributes);
        }

        public static MvcHtmlString DropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, params string[] attributes)
        {
            return htmlHelper.DropDownList(name, selectList, optionLabel, CreateAttributesDictionaryFromArray(attributes));
        }

        public static MvcHtmlString ListBoxFor<T>(this HtmlHelper htmlHelper, Expression<Func<T, object>> expression, IEnumerable<SelectListItem> selectList, params string[] attributes)
        {
            var name = ExpressionHelper.GetExpressionText(expression);

            return htmlHelper.ListBox(name, selectList, attributes);
        }

        public static MvcHtmlString ListBox(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, params string[] attributes)
        {
            return htmlHelper.ListBox(name, selectList, CreateAttributesDictionaryFromArray(attributes));
        }

        private static IDictionary<string, object> CreateAttributesDictionaryFromArray(params string[] attributes)
        {
            if (attributes == null || !attributes.Any()) return new Dictionary<string, object>();

            Argument.IsValid(attributes.Length % 2 == 0, "Attributes array does not contain even number of elements.", "attributes");
            Argument.IsValid(!attributes.Where((item, index) => string.IsNullOrWhiteSpace(item) && index % 2 == 0).Any(), @"CreateAttributesDictionaryFromArray() attribute names must not be null or whitespace.", "attributes");

            var dictionary = new Dictionary<string, object>();

            for (var i = 0; i < attributes.Length - 1; i += 2)
            {
                dictionary.Add(attributes[i], attributes[i + 1]);
            }

            return dictionary;
        }
    }
}