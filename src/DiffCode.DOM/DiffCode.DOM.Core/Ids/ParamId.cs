using DiffCode.DOM.Interfaces;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace DiffCode.DOM.Core.Ids;

/// <summary>
/// Типизированный идентификатор для параметра шаблона.
/// </summary>
public readonly record struct ParamId : ITypedId<string>, IComparable<ParamId>, IEquatable<ParamId>
{
  public ParamId([CallerMemberName] string name = "")
  {
    Value = name;
  }





  public int CompareTo(ParamId other) => Value.CompareTo(other.Value);


  public bool Equals(ParamId other) => Value.Equals(other.Value);


  public override int GetHashCode() => Value.GetHashCode();


  public override string ToString() => Value;





  /// <summary>
  /// Значение идентификатора.
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public readonly string Value { get; }





  public static ParamId New([CallerMemberName] string name = "") => new(name);


  public static implicit operator string(ParamId id) => id.Value;

}
