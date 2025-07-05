namespace DiffCode.DOM.Interfaces;

/// <summary>
/// Интерфейс типизированного идентификатора.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ITypedId<T> where T : IComparable<T>, IEquatable<T>
{

  /// <summary>
  /// <inheritdoc cref="IEquatable{T}"/>
  /// </summary>
  /// <returns></returns>
  public int GetHashCode() => Value.GetHashCode();

  /// <summary>
  /// <inheritdoc cref="ValueType.ToString()"/>
  /// </summary>
  /// <returns></returns>
  public string ToString() => Value.ToString();



  /// <summary>
  /// Значение идентификатора.
  /// </summary>
  T Value { get; }

}
