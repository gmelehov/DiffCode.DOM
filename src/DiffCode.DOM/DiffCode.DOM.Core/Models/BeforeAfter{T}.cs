using DiffCode.DOM.Interfaces;


namespace DiffCode.DOM.Core.Models;

/// <summary>
/// Модель пары значений указанного типа.
/// </summary>
/// <typeparam name="T"></typeparam>
public readonly record struct BeforeAfter<T> : IBeforeAfter<T>
{
  public BeforeAfter()
  {
    Before = default;
    After = default;
  }

  public BeforeAfter(T before, T after)
  {
    Before = before;
    After = after;
  }




  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public readonly T Before { get; }

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public readonly T After { get; }

}
