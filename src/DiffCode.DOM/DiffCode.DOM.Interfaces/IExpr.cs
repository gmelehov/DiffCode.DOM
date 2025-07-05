namespace DiffCode.DOM.Interfaces;

/// <summary>
/// Интерфейс логического условия.
/// </summary>
public interface IExpr
{


  /// <summary>
  /// Идентификатор условия.
  /// </summary>
  ITypedId<string> Id { get; }

  /// <summary>
  /// Результат оценки выражения для логического условия.
  /// </summary>
  bool? Result { get; }


}