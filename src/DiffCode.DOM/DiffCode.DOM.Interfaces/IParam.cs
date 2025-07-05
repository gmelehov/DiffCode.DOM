using DiffCode.Validating.Interfaces;

namespace DiffCode.DOM.Interfaces;

/// <summary>
/// Интерфейс нетипизированного параметра.
/// </summary>
public interface IParam : IActiveState, IWithFluentAction
{

  /// <summary>
  /// Запрещает изменение значения параметра <see cref="Value"/>.
  /// </summary>
  IParam LockValue();


  IParam Set(object val);


  /// <summary>
  /// Признак закрытого от изменений значения параметра.
  /// </summary>
  bool IsValueLocked { get; }





  /// <summary>
  /// Идентификатор параметра.
  /// </summary>
  ITypedId<string> Id { get; }

  /// <summary>
  /// Значение параметра.
  /// </summary>
  dynamic Value { get; }

}
