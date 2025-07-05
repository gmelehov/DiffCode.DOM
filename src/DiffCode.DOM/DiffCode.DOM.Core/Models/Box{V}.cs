using DiffCode.DOM.Interfaces;
using System.Numerics;

namespace DiffCode.DOM.Core.Models;


/// <summary>
/// Блочная модель.
/// </summary>
/// <typeparam name="V">Тип значения.</typeparam>
public readonly record struct Box<V> : IBox<V> where V : INumber<V>, IComparable<V>, IEquatable<V>
{
  public Box() => L = T = R = B = default;

  public Box(V val) => L = T = R = B = val;

  public Box(V horiz, V vert)
  {
    L = R = horiz;
    T = B = vert;
  }

  public Box(V l, V t, V r, V b)
  {
    L = l;
    T = t;
    R = r;
    B = b;
  }





  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public readonly V L { get; }

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public readonly V T { get; }

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public readonly V R { get; }

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public readonly V B { get; }

}
