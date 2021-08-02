// -----------------------------------------------------------------------
// <copyright file="IWorkflowPartsInformation.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.ExtensionPoints
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface for WorklflowParts Information
    /// </summary>
    public interface IWorkflowPartsInformation
    {
        /// <summary>
        /// Get WorkflowParts for OnEnter state
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <returns>list of WorkflowParts</returns>
        IEnumerable<BaseWorkflowPart> GetPartsOnEnterState(string stateName);

        /// <summary>
        /// Get WorkflowParts for OnExit state
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <returns>list of WorkflowParts</returns>
        IEnumerable<BaseWorkflowPart> GetPartsOnExitState(string stateName);

        /// <summary>
        /// Get WorkflowParts for OnAction
        /// </summary>
        /// <param name="stateName">state name</param>
        /// <returns>list of WorkflowParts</returns>
        IEnumerable<BaseWorkflowPart> GetPartsOnAction(string stateName);

        /// <summary>
        /// Get WorkflowParts of type T for OnEnter state
        /// </summary>
        /// <typeparam name="TPartType">part type</typeparam>
        /// <param name="stateName">state name</param>
        /// <param name="searchBuckets">search buckets for parts</param>
        /// <returns>list of WorkflowParts of type T</returns>
        IEnumerable<TPartType> GetPartsOnEnterState<TPartType>(string stateName, bool searchBuckets)
            where TPartType : BaseWorkflowPart;

        /// <summary>
        /// Get WorkflowParts of type T for OnExit state
        /// </summary>
        /// <typeparam name="TPartType">part type</typeparam>
        /// <param name="stateName">state name</param>
        /// <param name="searchBuckets">search buckets for parts</param>
        /// <returns>list of WorkflowParts of type T</returns>
        IEnumerable<TPartType> GetPartsOnExitState<TPartType>(string stateName, bool searchBuckets)
            where TPartType : BaseWorkflowPart;

        /// <summary>
        /// Get WorkflowParts of type T for OnAction
        /// </summary>
        /// <typeparam name="TPartType">part type</typeparam>
        /// <param name="stateName">state name</param>
        /// <param name="searchBuckets">search buckets for parts</param>
        /// <returns>list of WorkflowParts of type T</returns>
        IEnumerable<TPartType> GetPartsOnAction<TPartType>(string stateName, bool searchBuckets)
            where TPartType : BaseWorkflowPart;
    }
}