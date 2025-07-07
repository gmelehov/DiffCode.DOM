using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Core.Models.Params;
using DiffCode.DOM.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace DiffCode.DOM.Core.Abstractions.Texts;


/// <summary>
/// Базовая модель текстового фрагмента.
/// </summary>
public abstract class BaseText : BaseValidatable<IText>, IText
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected Func<string> _fn;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected Expr _isActiveOn;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected bool _isActiveOnLocked = false;




  protected BaseText(string val)
  {
    _fn = () => val;
    _isActiveOn = Expr.IsTrue;
  }

  protected BaseText(Func<string> fn)
  {
    _fn = fn;
    _isActiveOn = Expr.IsTrue;
    IsComputed = true;
  }

  protected BaseText(string val, Expr expr)
  {
    _fn = () => val;
    _isActiveOn = expr;
  }

  protected BaseText(Func<string> fn, Expr expr)
  {
    _fn = fn;
    _isActiveOn = expr;
    IsComputed = true;
  }

  protected BaseText(DateOnlyParam prm, Expr? expr = null)
  {
    _fn = () => prm.Value.ToLongDateString();
    _isActiveOn = expr ?? Expr.IsTrue;
    IsComputed = true;
    IsFromParam = true;
  }

  protected BaseText(IParam prm, LambdaExpression expression = null, Expr? expr = null)
  {
    _fn = expression == null ? () => prm.Value.ToString() : () => expression.Compile().DynamicInvoke(prm).ToString();
    _isActiveOn = expr ?? Expr.IsTrue;
    IsComputed = true;
    IsFromParam = true;
  }




  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public void LockActiveOn() => _isActiveOnLocked = true;

  /// <summary>
  /// <inheritdoc />
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
  /// <inheritdoc />
  /// </summary>
  public IPara Parent { get; set; }

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public IBeforeAfter<int> Lines { get; set; }

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public TextFormat Format { get; set; }

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public virtual string Content => _fn?.DynamicInvoke()?.ToString();


  public bool IsComputed { get; }


  public bool IsFromParam { get; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public virtual bool IsActive => IsActiveOn.Result ?? true;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public bool IsActiveOnLocked => _isActiveOnLocked;

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public Expr IsActiveOn => _isActiveOn;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  IExpr IActiveState.IsActiveOn => IsActiveOn;




  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public override string ToString()
  {
    var ret = new StringBuilder();

    if (IsActive)
    {
      if (Lines != null)
      {
        foreach (var i in Enumerable.Range(0, Lines?.Before ?? 0))
        {
          ret.AppendLine();
        }
      }

      ret.Append(_fn?.DynamicInvoke()?.ToString());

      if (Lines != null)
      {
        foreach (var i in Enumerable.Range(0, Lines?.After ?? 0))
        {
          ret.AppendLine();
        }
      }
    }

    return ret.ToString();
  }

  
  
}
