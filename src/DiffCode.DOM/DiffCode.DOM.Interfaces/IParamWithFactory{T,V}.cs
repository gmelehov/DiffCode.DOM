namespace DiffCode.DOM.Interfaces;

/// <summary>
/// Интерфейс типизированного параметра, значение которого устанавливается делегатом типа <typeparamref name="V"/>.
/// </summary>
/// <typeparam name="T">Тип значения, хранящегося в параметре.</typeparam>
/// <typeparam name="V">Фабрика для установки значения параметра.</typeparam>
public interface IParamWithFactory<T, V> : IParam<T> where V : Delegate
{


  /// <summary>
  /// Задает фабрику для установки значения параметра.
  /// </summary>
  /// <param name="factory">Делегат-фабрика.</param>
  /// <returns></returns>
  IParamWithFactory<T, V> SetValueFactory(V factory);


  /// <summary>
  /// Устанавливает новое значение этого параметра, используя указанный набор аргументов при вызове фабрики.
  /// </summary>
  /// <param name="objects">Набор аргументов, передаваемый делегату-фабрике.</param>
  /// <returns></returns>
  IParamWithFactory<T, V> Set(params object[] objects);


}