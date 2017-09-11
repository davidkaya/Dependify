// Copyright 2017 Dávid Kaya. All rights reserved.
// Use of this source code is governed by the MIT license,
// as found in the LICENSE file.

using System;
using System.Collections.Generic;

namespace Dependify.Extensions {
    // ReSharper disable once InconsistentNaming
    internal static class IEnumerableExtensions {
        internal static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
            foreach (var item in source)
                action(item);
        }
    }
}