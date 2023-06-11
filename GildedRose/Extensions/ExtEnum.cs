using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRose.Extensions;

public static class ExtEnum
{
    public static IEnumerable<T> SelectMultipleByName<T>(string name) where T : Enum =>
        Enum.GetValues(typeof(T))
        .Cast<T>()
        .Where(x => x.ToString().Contains(name));
}
