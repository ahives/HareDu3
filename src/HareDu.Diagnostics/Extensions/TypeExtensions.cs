using System.Collections.Concurrent;
using System.Linq;
using HareDu.Diagnostics.Scanners;

namespace HareDu.Diagnostics.Extensions;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Probes;

public static class TypeExtensions
{
    public static void ConfigureAll(this ConcurrentDictionary<string, object> cache, Action<string> action)
    {
        Span<string> memoryFrames = CollectionsMarshal.AsSpan(cache.Keys.ToList());
        ref var ptr = ref MemoryMarshal.GetReference(memoryFrames);

        for (int i = 0; i < memoryFrames.Length; i++)
        {
            var key = Unsafe.Add(ref ptr, i);

            action(key);
        }
    }
}