using System.Runtime.CompilerServices;

namespace DiffCode.DOM.Core.Models.Params;

/// <summary>
/// Строковый параметр.
/// </summary>
public class StringParam : Param<string>
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="memberName"></param>
  public StringParam([CallerMemberName] string memberName = "") : base(memberName)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="exprWrapper"></param>
  /// <param name="memberName"></param>
  public StringParam(Expr exprWrapper, [CallerMemberName] string memberName = "") : base(exprWrapper, memberName)
  {

  }

}
