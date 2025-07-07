using DiffCode.DOM.Core.Ids;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Interfaces;
using DiffCode.Validating.Interfaces.Extensions;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DiffCode.DOM.Core.Abstractions.Params;


/// <summary>
/// Базовая модель нетипизированного параметра.
/// </summary>
[DebuggerDisplay("{Id}: {Value}")]
public abstract class BaseParam : IParam
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected bool _isValueLocked = false;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected Expr _isActiveOn;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected bool _isActiveOnLocked = false;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private dynamic _val;




  protected BaseParam([CallerMemberName] string memberName = "")
  {
    Id = ParamId.New(memberName);
  }

  protected BaseParam(dynamic val, [CallerMemberName] string memberName = "")
  {
    Id = ParamId.New(memberName);
    _val = val;
  }

  protected BaseParam(Expr exprWrapper, [CallerMemberName] string memberName = "")
  {
    Id = ParamId.New(memberName);
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
  /// <exception cref="NotImplementedException"></exception>
  public IParam Set(object val) => throw new NotImplementedException();

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
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public virtual dynamic Value => _val;

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

}
