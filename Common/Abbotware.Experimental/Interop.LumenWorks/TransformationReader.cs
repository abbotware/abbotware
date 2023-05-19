// -----------------------------------------------------------------------
// <copyright file="TransformationReader.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2023. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.LumenWorks
{
    using System;
    using System.IO;
    using Abbotware.Core;

    /// <summary>
    /// specialized stream reader that transform data on the fly
    /// </summary>
    public class TransformationReader : StreamReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransformationReader"/> class.
        /// </summary>
        /// <param name="path">The complete file path to be read</param>
        public TransformationReader(string path)
            : base(path)
        {
        }

        /// <inheritdoc/>
        public override int Read(char[] buffer, int index, int count)
        {
            Arguments.NotNull(buffer, nameof(buffer));

            var chars = base.Read(buffer, index, count);

            var delemeter = false;

            for (var i = 0; i < chars; ++i)
            {
                if (!delemeter)
                {
                    if (buffer[i] == '|')
                    {
                        delemeter = true;
                    }
                }
                else
                {
                    if (buffer[i] == '|')
                    {
                        buffer[i - 1] = ' ';
                    }

                    delemeter = false;
                }
            }

            if (delemeter)
            {
                buffer[chars - 1] = ' ';
            }

            return chars;
        }
    }
}