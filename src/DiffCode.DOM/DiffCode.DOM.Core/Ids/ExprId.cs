using DiffCode.DOM.Interfaces;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace DiffCode.DOM.Core.Ids;

/// <summary>
/// Типизированный идентификатор для логического условия.
/// </summary>
public readonly record struct ExprId : ITypedId<string>, IComparable<ExprId>, IEquatable<ExprId>
{
  public ExprId([CallerMemberName] string name = "")
  {
    Value = name;
  }


  /// <summary>
  /// <inheritdoc />
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public int CompareTo(ExprId other) => Value.CompareTo(other.Value);

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="other"></param>
  /// <returns></returns>
  public bool Equals(ExprId other) => Value.Equals(other.Value);

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public override int GetHashCode() => Value.GetHashCode();


  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public override string ToString() => Value;



  /// <summary>
  /// Значение идентификатора.
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public readonly string Value { get; }





  public static ExprId New([CallerMemberName] string name = "") => new(name);

  public static implicit operator string(ExprId id) => id.Value;

  public static implicit operator ExprId(string id) => new(id);

}
