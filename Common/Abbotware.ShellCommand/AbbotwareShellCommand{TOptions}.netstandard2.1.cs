// -----------------------------------------------------------------------
// <copyright file="AbbotwareShellCommand{TOptions}.netstandard2.1.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.ShellCommand
{
    using System;
    using System.Linq;

#pragma warning disable CS1710 // XML comment has a duplicate typeparam tag
    /// <summary>
    /// class the can run a shell command
    /// </summary>
    /// <typeparam name="TOptions">options type</typeparam>
    public partial class AbbotwareShellCommand<TOptions>
#pragma warning restore CS1710 // XML comment has a duplicate typeparam tag
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