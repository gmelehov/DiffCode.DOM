using DiffCode.Validating.Interfaces;

namespace DiffCode.DOM.Interfaces;


/// <summary>
/// Интерфейс типизированного параметра.
/// </summary>
/// <typeparam name="T">Тип значения, хранящегося в параметре.</typeparam>
public interface IParam<T> : IParam, IValidatable<IParam<T>>
{


  /// <summary>
  /// Устанавливает новое значение этого параметра.
  /// </summary>
  /// <typeparam name="TVal">Тип значения параметра.</typeparam>
  /// <param name="val">Новое значение параметра.</param>
  /// <returns></returns>
  IParam<T> Set(T val);




  /// <summary>
  /// Значение параметра.
  /// </summary>
  new T Value { get; }

}