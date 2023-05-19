//-----------------------------------------------------------------------
// <copyright file="InfoButtonExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.Core.Web.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web.Mvc;

    /// <summary>
    /// Helper for Info Button Html extensions
    /// </summary>
    public static class InfoButtonExtensions
    {
        /// <summary>
        /// Html Template for info button
        /// </summary>
        private static string infoButtonHtml = @"
            <style type='text/css'>
                .info-button {
                    position: relative;
                    z-index: 3;
                    height: 0;
                    width: 35px;
                    top: 4px;
                    left: calc(100% - 30px);
                    font-size: 150%;
                }
                .info-button:hover {
                    cursor: pointer;
                }
            </style>

            <div id='info-button-dialog' title='Top Business Search'> </div>

            <script type='text/javascript'>
                document.info_button_clicked = document.info_button_clicked || function( elem )
                {
                    var iden = $( elem ).attr( 'data-for-id' );
                    var info = $( elem ).attr( 'data-for-info' );
                    var text = $( 'label[for=""' + iden + '""]' ).text();

                    $( '#info-button-dialog' ).html( info );
                    $( '#info-button-dialog' ).dialog( 'option', 'title', text );
                    $( '#info-button-dialog' ).dialog( 'open' );
                };

                $( '#info-button-dialog' ).dialog( { autoOpen: false } );
            </script>
            ";

        /// <summary>
        /// Help method to create pop up information box
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <param name="htmlHelper">html helper</param>
        /// <returns>mvc rendered string</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlHelper", Justification = "reviewed")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString UsingInfoFor<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return new MvcHtmlString(infoButtonHtml);
        }

        /// <summary>
        /// Help method to create pop up information box
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Propery Type</typeparam>
        /// <param name="htmlHelper">html helper</param>
        /// <param name="expression">property expression</param>
        /// <param name="information">information text</param>
        /// <returns>mvc rendered string</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString InfoFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string information)
        {
            return InfoFor(htmlHelper, expression, information, (IDictionary<string, object>)null);
        }

        /// <summary>
        /// Help method to create pop up information box
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Propery Type</typeparam>
        /// <param name="htmlHelper">html helper</param>
        /// <param name="expression">property expression</param>
        /// <param name="information">information text</param>
        /// <param name="htmlAttributes">html attributes</param>
        /// <returns>mvc rendered string</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString InfoFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string information, object htmlAttributes)
        {
            return InfoFor(htmlHelper, expression, information, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Help method to create pop up information box
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Propery Type</typeparam>
        /// <param name="htmlHelper">html helper</param>
        /// <param name="expression">property expression</param>
        /// <param name="information">information text</param>
        /// <param name="htmlAttributes">html attributes</param>
        /// <returns>mvc rendered string</returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "reviewed")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString InfoFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string information, IReadOnlyDictionary<string, object> htmlAttributes)
        {
            const string info_type = "&#x1F6C8;";  // 🛈
//          const string info_type = "&#x2139;";   // ℹ

            //-------------------------------------------------------------------------------
            //  These next few lines of code get the HTML 'id' and 'name' strings generated
            //  by the ASP MVC engine.
            //
            //  For example, the next uses the expression ( model => model.Ssn ) as in:
            //      @Html.InfoFor( model => model.Ssn );
            //
            //  and we get the 'id' and 'name' strings conversions using:
            //      GetFullHtmlFieldId(field_name)    --->  "Owners_0__Contact_Ssn"
            //      GetFullHtmlFieldName(field_name)  --->  "Owners[0].Contact.Ssn"
            //-------------------------------------------------------------------------------
            string field_name = ExpressionHelper.GetExpressionText(expression);

            string id = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(field_name);

            string attr = StringExtensions.HtmlAttributesToString(htmlAttributes);
            string fors = $"data-for-id='{id}' data-for-info='{information}'";
            string html = $"<div class='info-button' onclick='document.info_button_clicked( this )' {fors} {attr}>{info_type}</div>";

            return new MvcHtmlString(html);
        }
    }
}
