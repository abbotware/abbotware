// -----------------------------------------------------------------------
// <copyright file="IStateMachineManager.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.ExtensionPoints
{
    public interface IStateMachineManager : IStateMachine
    {
        void AddState(IState state);

        IState AddState(string state);

        IAction AddAction(string action, IState source, IState target);

        IAction AddAction(string action, string source, string target);

        void AddAction(IAction action);
    }
}