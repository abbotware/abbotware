//-----------------------------------------------------------------------
// <copyright file="PrivateEyeExtensions.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
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
    /// Helper for Private Eye Html extensions
    /// </summary>
    public static class PrivateEyeExtensions
    {
        /// <summary>
        /// Html Template for private eye
        /// </summary>
        private static string privateEyeHtml = @"
            <style type='text/css'>
                .private-eye {
                    position: relative;
                    z-index: 3;
                    height: 0;
                    width: 35px;
                    top: 3px;
                    left: calc(100% - 35px);
                    font-size: 165%;
                }
                .private-eye:hover {
                    cursor: pointer;
                }
            </style>

            <script type='text/javascript'>
                document.private_eye_clicked = document.private_eye_clicked || function( elem )
                {
                    var id = $( elem ).attr( 'data-for-id' );
                    var input_elem = $( id );

                    var new_type = ( input_elem.attr( 'type' ) === 'password' ) ? 'text' : 'password';
                    input_elem.attr( 'type', new_type );

                    if ( new_type !== 'password' )
                    {
                        var time = new Date().getTime().toString();
                        input_elem.attr( 'data-private-eye-time', time );

                        setTimeout( function()
                        {
                            if ( input_elem.attr( 'data-private-eye-time' ) === time )
                            {
                                input_elem.attr( 'type', 'password' );
                            }
                        }, 6000 );
                    }
                };
            </script>
            ";

        /// <summary>
        /// Help method to create a string box that can hide sensitive information
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <param name="htmlHelper">html helper</param>
        /// <returns>mvc rendered string</returns>
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "htmlHelper", Justification = "reviewed")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString UsingPrivateEyeFor<TModel>(this HtmlHelper<TModel> htmlHelper)
        {
            return new MvcHtmlString(privateEyeHtml);
        }

        /// <summary>
        /// Help method to create a string box that can hide sensitive information
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Propery Type</typeparam>
        /// <param name="htmlHelper">html helper</param>
        /// <param name="expression">property expression</param>
        /// <returns>mvc rendered string</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString PrivateEyeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return PrivateEyeFor(htmlHelper, expression, (IDictionary<string, object>)null);
        }

        /// <summary>
        /// Help method to create a string box that can hide sensitive information
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Propery Type</typeparam>
        /// <param name="htmlHelper">html helper</param>
        /// <param name="expression">property expression</param>
        /// <param name="htmlAttributes">html attributes</param>
        /// <returns>mvc rendered string</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString PrivateEyeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return PrivateEyeFor(htmlHelper, expression, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
        }

        /// <summary>
        /// Help method to create a string box that can hide sensitive information
        /// </summary>
        /// <typeparam name="TModel">Model Class</typeparam>
        /// <typeparam name="TProperty">Propery Type</typeparam>
        /// <param name="htmlHelper">html helper</param>
        /// <param name="expression">property expression</param>
        /// <param name="htmlAttributes">html attributes</param>
        /// <returns>mvc rendered string</returns>
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "reviewed")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        public static MvcHtmlString PrivateEyeFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IReadOnlyDictionary<string, object> htmlAttributes)
        {
            const string eye_type = "&#x1f441;";   // 👁
//          const string eye_type = "&#x1F453;";   // 👓
//          const string eye_type = "&#x1F440;";   // 👀
//          const string eye_type = "&#x25C9;";    // ◉
//          const string eye_type = "&#x25CE;";    // ◎

            //-------------------------------------------------------------------------------
            //  These next few lines of code get the HTML 'id' and 'name' strings generated
            //  by the ASP MVC engine.
            //
            //  For example, the next uses the expression ( model => model.Ssn ) as in:
            //      @Html.PrivateEyeFor( model => model.Ssn );
            //
            //  and we get the 'id' and 'name' strings conversions using:
            //      GetFullHtmlFieldId(field_name)    --->  "Owners_0__Contact_Ssn"
            //      GetFullHtmlFieldName(field_name)  --->  "Owners[0].Contact.Ssn"
            //-------------------------------------------------------------------------------
            string field_name = ExpressionHelper.GetExpressionText(expression);

            string id = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(field_name);
            string name = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(field_name);

            string attr = StringExtensions.HtmlAttributesToString(htmlAttributes);
            string fors = $"data-for-id='#{id}' data-for-name='{name}'";
            string html = $"<div class='private-eye' onclick='document.private_eye_clicked( this )' {fors} {attr}>{eye_type}</div>";

            return new MvcHtmlString(html);
        }
    }
}
