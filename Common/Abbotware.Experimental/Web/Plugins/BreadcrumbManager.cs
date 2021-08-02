// -----------------------------------------------------------------------
// <copyright file="BreadcrumbManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Web.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Xml;
    using Abbotware.Core.Collections;
    using Abbotware.Core.Helpers;
    using Abbotware.Core.Web.ExtensionPoints;
    using Abbotware.Core.Web.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    /// <summary>
    /// class that manages all the breadcrumbs
    /// </summary>
    public class BreadcrumbManager : IBreadcrumbManager
    {
        /// <summary>
        /// injected service provider
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// all breadcrumbs in a tree structure
        /// </summary>
        private TreeCollection<Breadcrumb> root;

        /// <summary>
        /// Initializes a new instance of the <see cref="BreadcrumbManager"/> class.
        /// </summary>
        /// <param name="serviceProvider">injected service provider</param>
        public BreadcrumbManager(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

            var doc = new XmlDocument();
            var xml = EmbeddedResourceHelper.GetTextFile("Zyronet.Web.Dashboard", "BreadCrumbNodes.xml");

            doc.LoadXml(xml);
            doc.ChildNodes.Cast<XmlNode>()
                .ToList()
                .ForEach(node => this.ProcessChild(node, null));

            if (this.root == null)
            {
                throw new XmlException("BreadCrumb XML file could not be processed, verify the file is formatted properly");
            }
        }

        /// <summary>
        /// creates the breadcrumb view model for the current http context
        /// </summary>
        /// <param name="context">current http context</param>
        /// <returns>view model</returns>
        public IEnumerable<BreadcrumbViewModel> Build(HttpContext context)
        {
            var routeDictionary = context.GetRouteData().Values;

            var actionName = routeDictionary["action"].ToString();
            var controllerName = routeDictionary["controller"].ToString();

            var currentNode = this.root.FirstOrDefault(x => x.Action == actionName && x.Controller == controllerName);

            var list = new List<BreadcrumbViewModel>();

            for (; currentNode != null; currentNode = currentNode.Parent)
            {
                var vm = currentNode.Data.CreateViewModel(context);

                list.Insert(0, vm);
            }

            if (list.Any())
            {
                list.Last().IsLast = true;
            }

            return list;
        }

        /// <summary>
        /// recrusively processes the xml file for breadcrumb nodes
        /// </summary>
        /// <param name="child">xml child node</param>
        /// <param name="tree">collection of breadcrumbs</param>
        private void ProcessChild(XmlNode child, TreeCollection<Breadcrumb> tree)
        {
            if (child.NodeType != XmlNodeType.Element)
            {
                return;
            }

            var a = child.Attributes.GetNamedItem("Action");
            var c = child.Attributes.GetNamedItem("Controller");
            var f = child.Attributes.GetNamedItem("Fragment");
            var t = child.Attributes.GetNamedItem("Text");
            var type = child.Attributes.GetNamedItem("Type")?.Value;

            if (a == null || c == null || t == null)
            {
                throw new XmlException("BreadCrumb node must contain 'Controller', 'Action', and 'Text' attributes");
            }

            Breadcrumb node;

            if (string.IsNullOrWhiteSpace(type))
            {
                node = new Breadcrumb();
            }
            else
            {
                node = (Breadcrumb)this.serviceProvider.GetService(Type.GetType(type));
            }

            node.Action = a.Value;
            node.Controller = c.Value;
            node.Text = t.Value;
            node.Fragment = f?.Value;

            tree = tree == null ? (this.root = new TreeCollection<Breadcrumb>(node)) : tree.AddChild(node);

            child.ChildNodes.Cast<XmlNode>()
                .ToList()
                .ForEach(n => this.ProcessChild(n, tree));
        }
    }
}