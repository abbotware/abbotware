// -----------------------------------------------------------------------
// <copyright file="BaseWorkflowComponent.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Core.Workflow.ExtensionPoints
{
    using System;
    using Abbotware.Core.Extensions;

    public abstract class BaseWorkflowComponent : IWorkflowComponent, IEquatable<BaseWorkflowComponent>
    {
        protected BaseWorkflowComponent(string name, long id)
        {
            this.Id = id;
            this.Name = name;
        }

        public long Id
        {
            get;
        }

        public string Name
        {
            get;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!this.ClassPossiblyEquals<BaseWorkflowComponent>(obj, out var other))
            {
                return false;
            }

            return this.Equals(other);
        }

        /// <inheritdoc />
        public virtual bool Equals(BaseWorkflowComponent other)
        {
            if (!other.Id.Equals(this.Id))
            {
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
