using System.Numerics;


namespace DiffCode.DOM.Interfaces;

/// <summary>
/// Интерфейс блочной модели.
/// </summary>
/// <typeparam name="V">Тип значения, используемый в модели.</typeparam>
public interface IBox<out V> where V : INumber<V>, IComparable<V>, IEquatable<V>
{

  /// <summary>
  /// Левый край/левая сторона/слева.
  /// </summary>
  V L { get; }

  /// <summary>
  /// Верхний край/верхняя сторона/сверху.
  /// </summary>
  V T { get; }

  /// <summary>
  /// Правый край/правая сторона/справа.
  /// </summary>
  V R { get; }

  /// <summary>
  /// Нижний край/нижняя сторона/снизу.
  /// </summary>
  V B { get; }

}
