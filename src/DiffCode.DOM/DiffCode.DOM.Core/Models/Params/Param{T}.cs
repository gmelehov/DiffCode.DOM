using DiffCode.DOM.Core.Abstractions.Params;
using System.Runtime.CompilerServices;

namespace DiffCode.DOM.Core.Models.Params;


/// <summary>
/// Типизированный параметр.
/// </summary>
/// <typeparam name="T">Тип значения параметра.</typeparam>
public class Param<T> : BaseParam<T>
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="memberName"></param>
  public Param([CallerMemberName] string memberName = "") : base(memberName)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="val"></param>
  /// <param name="memberName"></param>
  public Param(T val, [CallerMemberName] string memberName = "") : base(val, memberName)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="exprWrapper"></param>
  /// <param name="memberName"></param>
  public Param(Expr exprWrapper, [CallerMemberName] string memberName = "") : base(exprWrapper, memberName)
  {
    
  }

}
