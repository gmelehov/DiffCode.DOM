using System.Runtime.CompilerServices;

namespace DiffCode.DOM.Core.Models.Params;

/// <summary>
/// Параметр, содержащий дату.
/// </summary>
public class DateOnlyParam : Param<DateOnly>
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="memberName"></param>
  public DateOnlyParam([CallerMemberName] string memberName = "") : base(memberName)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="val"></param>
  /// <param name="memberName"></param>
  public DateOnlyParam(DateOnly val, [CallerMemberName] string memberName = "") : base(val, memberName)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="exprWrapper"></param>
  /// <param name="memberName"></param>
  public DateOnlyParam(Expr exprWrapper, [CallerMemberName] string memberName = "") : base(exprWrapper, memberName)
  {

  }

}
