namespace DiffCode.DOM.Interfaces;

/// <summary>
/// Интерфейс объекта, имеющего признак активного.
/// </summary>
public interface IActiveState
{


  /// <summary>
  /// Устанавливает новое условие включения элемента.
  /// </summary>
  /// <typeparam name="T">Тип элемента.</typeparam>
  /// <param name="expr">Условие включения.</param>
  /// <returns></returns>
  T SetActiveOn<T>(IExpr expr) where T : IActiveState;


  /// <summary>
  /// Признак активного элемента.
  /// </summary>
  bool IsActive { get; }

  /// <summary>
  /// Признак закрытого от изменений условия включения элемента.
  /// </summary>
  bool IsActiveOnLocked { get; }

  /// <summary>
  /// Условие включения элемента.
  /// </summary>
  IExpr IsActiveOn { get; }

  /// <summary>
  /// Запрещает изменение условия включения элемента.
  /// </summary>
  void LockActiveOn();

}
