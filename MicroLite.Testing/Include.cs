﻿// -----------------------------------------------------------------------
// <copyright file="Include.cs" company="Project Contributors">
// Copyright Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace MicroLite.Testing
{
    /// <summary>
    /// A class which contains helper methods to create IIncludes for unit tests.
    /// </summary>
    public static class Include
    {
        /// <summary>
        /// Creates an <see cref="IIncludeMany&lt;T&gt;"/> where the HasResults is false and Results is empty.
        /// </summary>
        /// <typeparam name="T">The type of included value.</typeparam>
        /// <returns>An <see cref="IIncludeMany&lt;T&gt;"/> where the HasResults is false and Results is empty.</returns>
        public static IIncludeMany<T> Many<T>() => new IncludeMany<T>(new T[0]);

        /// <summary>
        /// Creates an <see cref="IIncludeMany&lt;T&gt;"/> where the HasResults is true and Results returns the specified values.
        /// </summary>
        /// <typeparam name="T">The type of included values.</typeparam>
        /// <param name="values">The included values.</param>
        /// <returns>An <see cref="IIncludeMany&lt;T&gt;"/> where the HasResults is true and Results returns the specified values.</returns>
        public static IIncludeMany<T> Many<T>(IList<T> values) => new IncludeMany<T>(values);

        /// <summary>
        /// Creates an <see cref="IInclude&lt;T&gt;"/> where the HasResults is false and Result returns the default value of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of included value.</typeparam>
        /// <returns>An <see cref="IInclude&lt;T&gt;"/> where the HasResults is false and Result returns the default value of the specified type.</returns>
#pragma warning disable CA1720 // Identifier contains type name

        public static IInclude<T> Single<T>() => new IncludeSingle<T>(default, hasValue: false);

#pragma warning restore CA1720 // Identifier contains type name

        /// <summary>
        /// Creates an <see cref="IInclude&lt;T&gt;"/> where the HasResults is true and Result returns the specified single value.
        /// </summary>
        /// <typeparam name="T">The type of included value.</typeparam>
        /// <param name="value">The included value.</param>
        /// <returns>An <see cref="IInclude&lt;T&gt;"/> where the HasResults is true and Result returns the specified single value.</returns>
#pragma warning disable CA1720 // Identifier contains type name

        public static IInclude<T> Single<T>(T value) => new IncludeSingle<T>(value, hasValue: true);

#pragma warning restore CA1720 // Identifier contains type name

        private sealed class IncludeMany<T> : IIncludeMany<T>
        {
            internal IncludeMany(IList<T> values) => Values = values;

            public bool HasValue => Values.Count > 0;

            public IList<T> Values { get; }

            public void OnLoad(Action<IIncludeMany<T>> action) => action?.Invoke(this);
        }

        private class IncludeSingle<T> : IInclude<T>
        {
            internal IncludeSingle(T value, bool hasValue)
            {
                Value = value;
                HasValue = hasValue;
            }

            public bool HasValue { get; }

            public T Value { get; }

            public void OnLoad(Action<IInclude<T>> action) => action?.Invoke(this);
        }
    }
}
