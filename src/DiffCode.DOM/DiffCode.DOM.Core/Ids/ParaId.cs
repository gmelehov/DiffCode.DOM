using DiffCode.DOM.Interfaces;
using System.Collections.Immutable;
using System.Diagnostics;


namespace DiffCode.DOM.Core.Ids;

/// <summary>
/// Типизированный идентификатор для абзаца шаблона.
/// </summary>
[DebuggerDisplay("{AsText}")]
public readonly record struct ParaId : ITypedId<string>, IComparable<ParaId>, IEquatable<ParaId>
{
  public ParaId(params ushort[] ushorts)
  {
    Segments = ushorts.ToImmutableArray();
  }





  public int CompareTo(ParaId other)
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


  public bool Equals(ParaId other) => Segments.Equals(other.Segments);


  public override int GetHashCode() => Segments.ToString().GetHashCode();


  public override string ToString() => string.Join(":", Segments.Select(s => ((int)Math.Pow(10, 2) + s).ToString()[1..]));



  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public string Value => ToString();





  /// <summary>
  /// Значение идентификатора.
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public readonly ImmutableArray<ushort> Segments { get; }




  public static readonly ParaId Empty = new(0);


  public static ParaId New() => new(0);


  public static ParaId New(params ushort[] segments) => new(segments);




  public static implicit operator ushort[](ParaId paraId) => paraId.Segments.ToArray();


  public static implicit operator ParaId(ushort[] values) => New(values);


  public static implicit operator ParaId(List<ushort> values) => New(values.ToArray());

}
