using System.Runtime.CompilerServices;

namespace DiffCode.DOM.Core.Models.Params;

/// <summary>
/// Параметр, содержащий булево значение.
/// </summary>
public class BooleanParam : Param<bool>
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="memberName"></param>
  public BooleanParam([CallerMemberName] string memberName = "") : base(memberName)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="val"></param>
  /// <param name="memberName"></param>
  public BooleanParam(bool val, [CallerMemberName] string memberName = "") : base(val, memberName)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="exprWrapper"></param>
  /// <param name="memberName"></param>
  public BooleanParam(Expr exprWrapper, [CallerMemberName] string memberName = "") : base(exprWrapper, memberName)
  {

  }

}
