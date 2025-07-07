using DiffCode.DOM.Core.Ids;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Interfaces;
using DiffCode.Validating.Interfaces.Extensions;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DiffCode.DOM.Core.Abstractions.Params;


/// <summary>
/// Базовая модель типизированного параметра.
/// </summary>
/// <typeparam name="T"></typeparam>
[DebuggerDisplay("{Id}: {Value}")]
public abstract class BaseParam<T> : BaseValidatable<IParam<T>>, IParam<T>
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected bool _isValueLocked = false;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected Expr _isActiveOn;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected bool _isActiveOnLocked = false;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private T _val;




  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="memberName"></param>
  protected BaseParam([CallerMemberName] string memberName = "")
  {
    Id = new ParamId(memberName);
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="val"></param>
  /// <param name="memberName"></param>
  protected BaseParam(T val, [CallerMemberName] string memberName = "")
  {
    _val = val;
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="exprWrapper"></param>
  /// <param name="memberName"></param>
  protected BaseParam(Expr exprWrapper, [CallerMemberName] string memberName = "")
  {
    _isActiveOn = exprWrapper;
  }




  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public IParam LockValue() => this.FluentAction(() => _isValueLocked = true);

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="val"></param>
  /// <returns></returns>
  public virtual IParam<T> Set(T val)
  {
    if (!_isValueLocked)
    {
      _val = val;
    }

    return this;
  }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="val"></param>
  /// <returns></returns>
  /// <exception cref="NotImplementedException"></exception>
  public IParam Set(object val) => Set((T)val);

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="expr"></param>
  /// <returns></returns>
  public T SetActiveOn<T>(IExpr expr) where T : IActiveState
  {
    if (!_isActiveOnLocked)
    {
      _isActiveOn = (Expr)expr;
    }

    return (T)(IActiveState)this;
  }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public void LockActiveOn() => _isActiveOnLocked = true;




  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public bool IsValueLocked => _isValueLocked;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public ParamId Id { get; }

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  ITypedId<string> IParam.Id => Id;


  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public bool IsActive => IsActiveOn.Result ?? true;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public bool IsActiveOnLocked => _isActiveOnLocked;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public IExpr IsActiveOn => _isActiveOn;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
  public virtual T Value => _val;


  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  dynamic IParam.Value => Value;

}
