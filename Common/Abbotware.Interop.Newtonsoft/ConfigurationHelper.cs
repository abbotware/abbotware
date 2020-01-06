// -----------------------------------------------------------------------
// <copyright file="ConfigurationHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------

namespace Abbotware.Interop.Newtonsoft
{
    using System.IO;
    using Abbotware.Core.Exceptions;

    /// <summary>
    ///     helper to load config files
    /// </summary>
    public static class ConfigurationHelper
    {
        /// <summary>
        ///     Loads a config file
        /// </summary>
        /// <typeparam name="TConfig">Type of the config file</typeparam>
        /// <returns>config file</returns>
        public static TConfig LoadJson<TConfig>()
            where TConfig : new()
        {
            return LoadJson(false, new TConfig());
        }

        /// <summary>
        ///     Loads a config file
        /// </summary>
        /// <typeparam name="TConfig">Type of the config file</typeparam>
        /// <param name="create">create file</param>
        /// <returns>config file</returns>
        public static TConfig LoadJson<TConfig>(bool create)
            where TConfig : new()
        {
            return LoadJson(create, new TConfig());
        }

        /// <summary>
        ///     Loads a config file
        /// </summary>
        /// <typeparam name="TConfig">Type of the config file</typeparam>
        /// <param name="create">create file</param>
        /// <param name="defaultValue">default value to use for creating</param>
        /// <returns>config file</returns>
        public static TConfig LoadJson<TConfig>(bool create, TConfig defaultValue)
            where TConfig : new()
        {
            {
                var typeInfo = typeof(TConfig);

                var fileName = Path.Combine(typeInfo.Name + ".json");

                var filePath = fileName;

                if (create)
                {
                    var output = JsonHelper.ToString(defaultValue);

                    File.WriteAllText(filePath, output);
                }

                if (File.Exists(filePath))
                {
                    var text = File.ReadAllText(filePath);

                    return JsonHelper.FromString<TConfig>(text);
                }

                throw new ConfigurationNotFoundException($"{fileName} not found");
            }
        }
    }
}