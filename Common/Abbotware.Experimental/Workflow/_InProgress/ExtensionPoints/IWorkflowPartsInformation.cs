// -----------------------------------------------------------------------
// <copyright file="IWorkflowPartsInformation.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.ExtensionPoints
{
    using System.Collections.Generic;

    public interface IWorkflowPartsInformation
    {
        IEnumerable<BaseWorkflowPart> GetPartsOnEnterState(string state);

        IEnumerable<BaseWorkflowPart> GetPartsOnExitState(string state);

        IEnumerable<BaseWorkflowPart> GetPartsOnAction(string state);

        IEnumerable<TPartType> GetPartsOnEnterState<TPartType>(string state, bool searchBuckets)
            where TPartType : BaseWorkflowPart;

        IEnumerable<TPartType> GetPartsOnExitState<TPartType>(string state, bool searchBuckets)
            where TPartType : BaseWorkflowPart;

        IEnumerable<TPartType> GetPartsOnAction<TPartType>(string state, bool searchBuckets)
            where TPartType : BaseWorkflowPart;
    }
}