namespace DiffCode.DOM.Interfaces;

/// <summary>
/// Интерфейс пары значений объекта указанного типа.
/// </summary>
/// <typeparam name="T">Тип значения.</typeparam>
public interface IBeforeAfter<out T>
{

  /// <summary>
  /// Значение "ДО".
  /// </summary>
  T Before { get; }

  /// <summary>
  /// Значение "ПОСЛЕ".
  /// </summary>
  T After { get; }

}
