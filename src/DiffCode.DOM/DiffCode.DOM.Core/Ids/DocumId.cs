using DiffCode.DOM.Interfaces;
using System.Collections.Immutable;
using System.Diagnostics;


namespace DiffCode.DOM.Core.Ids;

/// <summary>
/// Типизированный идентификатор для документа.
/// </summary>
public readonly record struct DocumId : ITypedId<string>, IComparable<DocumId>, IEquatable<DocumId>
{
  public DocumId(params ushort[] ushorts)
  {
    Segments = ushorts.ToImmutableArray();
  }
  public DocumId(ImmutableArray<ushort> ushorts)
  {
    Segments = ushorts;
  }





  public int CompareTo(DocumId other)
  {
    var thisLen = Segments.Length;
    var otherLen = other.Segments.Length;

    if (thisLen == otherLen)
    {
      if (Segments.SequenceEqual(other.Segments))
        return 0;

      for (var i = 0; i < thisLen; i++)
      {
        if (Segments[i] == other.Segments[i])
          continue;
        else
          return Segments[i].CompareTo(other.Segments[i]);
      }
    }
    else if (thisLen > otherLen)
    {
      for (var i = 0; i < otherLen; i++)
      {
        if (Segments[i] == other.Segments[i])
          continue;
        else
          return Segments[i].CompareTo(other.Segments[i]);
      }
    }
    else
    {
      for (var i = 0; i < thisLen; i++)
      {
        if (Segments[i] == other.Segments[i])
          continue;
        else
          return Segments[i].CompareTo(other.Segments[i]);
      }
    }

    return 0;
  }


  public bool Equals(DocumId other) => Segments.Equals(other.Segments);


  public override int GetHashCode() => Segments.ToString().GetHashCode();


  public override string ToString() => string.Join(":", Segments.Select(s => ((int)Math.Pow(10, 2) + s).ToString()[1..]));



  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public string Value => ToString();





  /// <summary>
  /// Значение идентификатора.
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public readonly ImmutableArray<ushort> Segments { get; }
  

  public static readonly DocumId Empty = new(0);


  public static DocumId New() => new(0);


  public static DocumId New(params ushort[] segments) => new(segments);



  public static implicit operator ushort[](DocumId documId) => documId.Segments.ToArray();


  public static implicit operator DocumId(ushort[] values) => New(values);


  public static implicit operator DocumId(List<ushort> values) => New(values.ToArray());

}
