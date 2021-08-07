// -----------------------------------------------------------------------
// <copyright file="AbbotwareShellCommand.netstandard2.1.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.ShellCommand
{
    using System;
    using System.Linq;
    using Abbotware.Core.Objects;
    using Abbotware.ShellCommand.Configuration;

    /// <summary>
    /// class the can run a shell command
    /// </summary>
    public partial class AbbotwareShellCommand : BaseCommand<IShellCommandOptions, IExitInfo>, IShellCommand
    {
        private string MaskData(string data)
        {
            if (!this.Configuration.SensitiveFragments.Any())
            {
                return data;
            }

            foreach (var f in this.Configuration.SensitiveFragments)
            {
                var mask = new string('*', f.Length);
                data = data.Replace(f, mask, StringComparison.InvariantCulture);
            }

            return data;
        }
    }
}