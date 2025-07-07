using DiffCode.DOM.Interfaces;
using DiffCode.Validating.Interfaces.Extensions;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DiffCode.DOM.Core.Abstractions.Params;

/// <summary>
/// Базовая модель типизированного параметра, значение которого устанавливается делегатом типа <typeparamref name="V"/>.
/// </summary>
/// <typeparam name="T">Тип значения, хранящегося в параметре.</typeparam>
/// <typeparam name="V">Фабрика для установки значения параметра.</typeparam>
public abstract class BaseParamWithFactory<T, V> : BaseParam<T>, IParamWithFactory<T, V> where V : Delegate
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected V _valFactory;




  protected BaseParamWithFactory([CallerMemberName] string memberName = "") : base(memberName)
  {

  }




  /// <summary>
  /// <inheritdoc cref="IParamWithFactory{T, V}.SetValueFactory(V)"/>
  /// </summary>
  /// <param name="factory"></param>
  /// <returns></returns>
  public BaseParamWithFactory<T, V> SetValueFactory(V factory) => this.FluentAction(() => _valFactory = factory);

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  /// <param name="factory"></param>
  /// <returns></returns>
  IParamWithFactory<T, V> IParamWithFactory<T, V>.SetValueFactory(V factory) => SetValueFactory(factory);


  /// <summary>
  /// <inheritdoc cref="IParamWithFactory{T, V}.Set(object[])"/>
  /// </summary>
  /// <param name="objects"><inheritdoc cref="IParamWithFactory{T, V}.Set(object[])"/></param>
  /// <returns></returns>
  public BaseParamWithFactory<T, V> Set(params object[] objects) => 
    (BaseParamWithFactory<T, V>)(_valFactory != null ? base.Set(_valFactory.DynamicInvoke(objects)) : this);


  /// <summary>
  /// <inheritdoc />
  /// </summary>
  /// <param name="objects"><inheritdoc /></param>
  /// <returns></returns>
  IParamWithFactory<T, V> IParamWithFactory<T, V>.Set(params object[] objects) => Set(objects);
  
}