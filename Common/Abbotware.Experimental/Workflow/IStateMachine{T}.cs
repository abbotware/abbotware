// -----------------------------------------------------------------------
// <copyright file="IStateMachine{T}.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow
{
    using System.Collections.Generic;

    public interface IStateMachine<T> : IStateMachine
    {
        IJournal ApplyAction(T current, string actionName);

        IEnumerable<IAction> DetermineActions(T current);

        IState DetermineState(T current);
    }
}
