using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class EnumCollectionConverter<T> : ValueConverter<ICollection<T>, string> where T : Enum
{
    public EnumCollectionConverter() : base(
      v => string.Join(";", v),
      v => !string.IsNullOrEmpty(v) ? v.Split(new[] { ';' }).Select(x => (T)Enum.Parse(typeof(T), x)).ToArray() : null)
    {
    }
}

public class CollectionValueComparer<T> : ValueComparer<ICollection<T>>
{
    public CollectionValueComparer() : base(
        (c1, c2) => c1.SequenceEqual(c2),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => (ICollection<T>)c.ToHashSet())
    {
    }
}

public class ListStringValueComparer : ValueComparer<IList<string>>
{
    public ListStringValueComparer() : base(
        (c1, c2) => c1.SequenceEqual(c2),
        c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), c => (IList<string>)c.ToList())
    {
    }
}