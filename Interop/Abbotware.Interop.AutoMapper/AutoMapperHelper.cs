// -----------------------------------------------------------------------
// <copyright file="AutoMapperHelper.cs" company="Abbotware, LLC">
// Copyright © Abbotware, LLC 2012-2020. All rights reserved
// </copyright>
// -----------------------------------------------------------------------
// <author>Anthony Abate</author>

namespace Abbotware.Interop.AutoMapper
{
    using Abbotware.Core;
    using global::AutoMapper;
    using global::AutoMapper.Extensions.ExpressionMapping;

    /// <summary>
    ///     Helper class that provides ease of use for AutoMapper
    /// </summary>
    public static class AutoMapperHelper
    {
        /// <summary>
        /// Creates a mapper with a provided profile
        /// </summary>
        /// <typeparam name="TProfile">mapping profile</typeparam>
        /// <param name="useExpressionMapping">flag to indicate expression mapper support</param>
        /// <returns>mapper </returns>
        public static IMapper Create<TProfile>(bool useExpressionMapping = false)
            where TProfile : Profile, new()
        {
            return Create(useExpressionMapping, new TProfile());
        }

        /// <summary>
        /// Creates a mapper with provided profiles
        /// </summary>
        /// <typeparam name="TProfile1">mapping profile 1</typeparam>
        /// <typeparam name="TProfile2">mapping profile 2</typeparam>
        /// <param name="useExpressionMapping">flag to indicate expression mapper support</param>
        /// <returns>mapper </returns>
        public static IMapper Create<TProfile1, TProfile2>(bool useExpressionMapping = false)
            where TProfile1 : Profile, new()
            where TProfile2 : Profile, new()
        {
            return Create(useExpressionMapping, new TProfile1(), new TProfile2());
        }

        /// <summary>
        /// Creates a mapper with provided profiles
        /// </summary>
        /// <param name="useExpressionMapping">flag to indicate expression mapper support</param>
        /// <param name="profiles">profiles</param>
        /// <returns>mapper </returns>
        public static IMapper Create(bool useExpressionMapping, params Profile[] profiles)
        {
            profiles = Arguments.EnsureNotNull(profiles, nameof(profiles));

            var config = new MapperConfiguration(cfg =>
           {
               foreach (var p in profiles)
               {
                   if (useExpressionMapping)
                   {
                       cfg.AddExpressionMapping();
                   }

                   cfg.AddProfile(p);
               }
           });

            foreach (var p in profiles)
            {
                config.AssertConfigurationIsValid(p.ProfileName);
            }

            return config.CreateMapper();
        }
    }
}