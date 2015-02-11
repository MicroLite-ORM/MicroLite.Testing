// -----------------------------------------------------------------------
// <copyright file="Include.cs" company="MicroLite">
// Copyright 2012 - 2015 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace MicroLite.Testing
{
    using System;
    using System.Collections.Generic;

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
        public static IIncludeMany<T> Many<T>() where T : class, new()
        {
            return new IncludeMany<T>(new T[0]);
        }

        /// <summary>
        /// Creates an <see cref="IIncludeMany&lt;T&gt;"/> where the HasResults is true and Results returns the specified values.
        /// </summary>
        /// <typeparam name="T">The type of included values.</typeparam>
        /// <param name="values">The included values.</param>
        /// <returns>An <see cref="IIncludeMany&lt;T&gt;"/> where the HasResults is true and Results returns the specified values.</returns>
        public static IIncludeMany<T> Many<T>(IList<T> values) where T : class, new()
        {
            return new IncludeMany<T>(values);
        }

        /// <summary>
        /// Creates an <see cref="IInclude&lt;T&gt;"/> where the HasResults is false and Result returns the default value of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of included value.</typeparam>
        /// <returns>An <see cref="IInclude&lt;T&gt;"/> where the HasResults is false and Result returns the default value of the specified type.</returns>
        public static IInclude<T> Single<T>()
        {
            return new IncludeSingle<T>(default(T), hasValue: false);
        }

        /// <summary>
        /// Creates an <see cref="IInclude&lt;T&gt;"/> where the HasResults is true and Result returns the specified single value.
        /// </summary>
        /// <typeparam name="T">The type of included value.</typeparam>
        /// <param name="value">The included value.</param>
        /// <returns>An <see cref="IInclude&lt;T&gt;"/> where the HasResults is true and Result returns the specified single value.</returns>
        public static IInclude<T> Single<T>(T value)
        {
            return new IncludeSingle<T>(value, hasValue: true);
        }

        private sealed class IncludeMany<T> : IIncludeMany<T> where T : class, new()
        {
            private readonly IList<T> values;

            internal IncludeMany(IList<T> values)
            {
                this.values = values;
            }

            public bool HasValue
            {
                get
                {
                    return this.values.Count > 0;
                }
            }

            public IList<T> Values
            {
                get
                {
                    return this.values;
                }
            }

            public void OnLoad(Action<IIncludeMany<T>> action)
            {
                if (action != null)
                {
                    action(this);
                }
            }
        }

        private class IncludeSingle<T> : IInclude<T>
        {
            private readonly bool hasValue;
            private readonly T value;

            internal IncludeSingle(T value, bool hasValue)
            {
                this.value = value;
                this.hasValue = hasValue;
            }

            public bool HasValue
            {
                get
                {
                    return this.hasValue;
                }
            }

            public T Value
            {
                get
                {
                    return this.value;
                }
            }

            public void OnLoad(Action<IInclude<T>> action)
            {
                if (action != null)
                {
                    action(this);
                }
            }
        }
    }
}