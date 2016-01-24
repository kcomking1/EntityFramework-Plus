﻿// Description: EF Bulk Operations & Utilities | Bulk Insert, Update, Delete, Merge from database.
// Website & Documentation: https://github.com/zzzprojects/Entity-Framework-Plus
// Forum: https://github.com/zzzprojects/EntityFramework-Plus/issues
// License: http://www.zzzprojects.com/license-agreement/
// More projects: http://www.zzzprojects.com/
// Copyright (c) 2015 ZZZ Projects. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Z.EntityFramework.Plus
{
    public static class QueryIncludeOptimizedExtensions
    {
        /// <summary>
        ///     An IQueryable&lt;T&gt; extension method that include and filter related entities with a optimized SQL.
        /// </summary>
        /// <typeparam name="T">The type of elements of the query.</typeparam>
        /// <typeparam name="TChild">The type of elements of the child query.</typeparam>
        /// <param name="query">The query to filter included related entities.</param>
        /// <param name="queryIncludeFilter">The query filter to apply on included related entities.</param>
        /// <returns>An IQueryable&lt;T&gt; that include and filter related entities.</returns>
        public static IQueryable<T> IncludeOptimized<T, TChild>(this IQueryable<T> query, Expression<Func<T, IEnumerable<TChild>>> queryIncludeFilter) where T : class where TChild : class
        {
            // GET query root
            var includeOrderedQueryable = query as QueryIncludeOptimizedParentQueryable<T> ?? new QueryIncludeOptimizedParentQueryable<T>(query);

            // ADD sub query
            includeOrderedQueryable.Childs.Add(new QueryIncludeOptimizedChild<T, TChild>(queryIncludeFilter));

            // RETURN root
            return includeOrderedQueryable;
        }
    }
}