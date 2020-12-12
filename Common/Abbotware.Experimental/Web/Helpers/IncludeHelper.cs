//-----------------------------------------------------------------------
// <copyright file="IncludeHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// <author>Anthony Abate</author>
//-----------------------------------------------------------------------

namespace Abbotware.Core.Web.Helpers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using Abbotware.Core.Cryptography;
    using Abbotware.Core.Extensions;
    using Abbotware.Core.Helpers;

    /// <summary>
    /// Help class for ASP MVC Includes
    /// </summary>
    public static class IncludeHelper
    {
        /// <summary>
        /// Prepares an HTML element that's a JavaScript resource on disk using a path
        /// </summary>
        /// <param name="viewPage">The view being generated in the .cshtml file</param>
        /// <param name="resourcePath">The path and name of the style sheet file</param>
        /// <returns>The raw html element</returns>
        public static IHtmlString JavaScript(WebViewPage viewPage, string resourcePath)
        {
            Contract.Requires<ArgumentException>(!StringExtensions.IsNullOrWhiteSpace(resourcePath));

            string path = VirtualPathUtility.ToAbsolute(resourcePath);
            string html = $"<script src='{path}' type='text/javascript'></script>";
            return viewPage.Html.Raw(html);
        }

        /// <summary>
        /// Prepares an HTML element that's designed to retrieve a Javascript file from an assembly's embedded resources
        /// </summary>
        /// <param name="viewPage">The view being generated in the .cshtml file</param>
        /// <param name="assemblyName">the name of the assembly module</param>
        /// <param name="resourcePath">The name of the javascript file</param>
        /// <returns>The raw html element</returns>
        public static IHtmlString JavaScript(WebViewPage viewPage, string assemblyName, string resourcePath)
        {
            Guid guid = IncludeHelper.ExtractResourceInfo(assemblyName, resourcePath, out string assembly, out string resource, out string encoding);

            if (guid.Equals(Guid.Empty))
            {
                string error = $"<link data-type='javascript' data-module-error='Unknown module name: {assembly}' />";
                return viewPage.Html.Raw(error);
            }

            //-------------------------------------------------------------------------------------
            //  Example of usage in a .cshtml file:
            //      @this.JavaScript( "Nexlend.Platform.DecisionLogic", "Scripts/ValidationFormFields.js" )
            //
            //  Here is an example of a processed uri:
            //      "/Resource/JavaScript/Nexlend.Platform.DecisionLogic/Scripts.ValidationFormFields.js/"
            //
            //  Here is what the final HTML tag will look like:
            //      <script data-file='Scripts.ValidationFormFields.js' src='/Resource/JavaScript/{guid}/Nexlend.Platform.DecisionLogic/encoded_string/' type='text/javascript' ></script>
            //-------------------------------------------------------------------------------------
            var html = FormattableString.Invariant($"<script data-file='{resource}' src='/Resource/JavaScript/{guid}/{assembly}/{encoding}/' type='text/javascript'></script>");

            return viewPage.Html.Raw(html);
        }

        /// <summary>
        /// Prepares an HTML element that's a StyleSheet resource on disk using a path
        /// </summary>
        /// <param name="viewPage">The view being generated in the .cshtml file</param>
        /// <param name="resourcePath">The path and name of the style sheet file</param>
        /// <returns>The raw html element</returns>
        public static IHtmlString StyleSheet(WebViewPage viewPage, string resourcePath)
        {
            Contract.Requires<ArgumentException>(!StringExtensions.IsNullOrWhiteSpace(resourcePath));

            string path = VirtualPathUtility.ToAbsolute(resourcePath);
            string html = $"<link href='{path}' rel='stylesheet' type='text/css' />";
            return viewPage.Html.Raw(html);
        }

        /// <summary>
        /// Prepares an HTML element that's designed to retrieve a StyleSheet file from an assembly's embedded resources
        /// </summary>
        /// <param name="viewPage">The view being generated in the .cshtml file</param>
        /// <param name="assemblyName">the name of the assembly module</param>
        /// <param name="resourcePath">The name of the style sheets file</param>
        /// <returns>The raw html element</returns>
        public static IHtmlString StyleSheet(WebViewPage viewPage, string assemblyName, string resourcePath)
        {
            Guid guid = IncludeHelper.ExtractResourceInfo(assemblyName, resourcePath, out string assembly, out string resource, out string encoding);

            if (guid.Equals(Guid.Empty))
            {
                string error = $"<link data-type='stylesheet' data-module-error='Unknown module name: {assembly}' />";
                return viewPage.Html.Raw(error);
            }

            //-------------------------------------------------------------------------------------
            //  Example of usage in a .cshtml file:
            //      @this.StyleSheet( "Nexlend.Platform.DecisionLogic", "Styles/CssClasses.css" )
            //
            //  Here is an example of a processed uri:
            //      "/Resource/StyleSheet/Nexlend.Platform.DecisionLogic/Styles.CssClasses.css/"
            //
            //  Here is what the final HTML tag will look like:
            //      <link data-file='Styles.CssClasses.css' href='/Resource/StyleSheet/{guid}/Nexlend.Platform.DecisionLogic/encoded_string/' rel='stylesheet' type='text/css' />
            //-------------------------------------------------------------------------------------
            string html = FormattableString.Invariant($"<link data-file='{resource}' href='/Resource/StyleSheet/{guid}/{assembly}/{encoding}/' rel='stylesheet' type='text/css' />");

            return viewPage.Html.Raw(html);
        }

        /// <summary>
        /// Gets Resource info
        /// </summary>
        /// <param name="assemblyName">name of assembly</param>
        /// <param name="resourcePath">resoruce path</param>
        /// <param name="assembly">output assembly name</param>
        /// <param name="resource">output resource path</param>
        /// <param name="encoding">output resource encoding</param>
        /// <returns>assembly module guid</returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "reviewed")]
        private static Guid ExtractResourceInfo(string assemblyName, string resourcePath, out string assembly, out string resource, out string encoding)
        {
            Contract.Requires<ArgumentException>(!StringExtensions.IsNullOrWhiteSpace(assemblyName));
            Contract.Requires<ArgumentException>(!StringExtensions.IsNullOrWhiteSpace(resourcePath));

            assembly = TrimPath(assemblyName);
            resource = TrimPath(resourcePath);
            encoding = UrlCrypto.ToUrlEncodedString(resource);

            try
            {
                //---------------------------------------------------------------------------------
                //  The guid's are used to ensure that the client's browser does not use stale
                //  files from their cache. This is because whenever a new module's assembly is
                //  generated, the compiler assigns a new guid for it and new links are created
                //---------------------------------------------------------------------------------
                Assembly module = EmbeddedResourceHelper.GetResourceModule(assembly);
                Guid guid = module.ManifestModule.ModuleVersionId;
                return guid;
            }
            catch (Exception)
            {
                return Guid.Empty;  // couldn't retrieve the file info, probably a wrong file name
            }
        }

        /// <summary>
        /// Remove characters from assembly name
        /// </summary>
        /// <param name="assembly">assembly name</param>
        /// <returns>trimmed assembly name</returns>
        private static string TrimPath(string assembly)
        {
            Contract.Requires<ArgumentException>(!StringExtensions.IsNullOrWhiteSpace(assembly));

            assembly = assembly.Trim(' ', '.', '/', '\\');
            return assembly;
        }
    }
}
