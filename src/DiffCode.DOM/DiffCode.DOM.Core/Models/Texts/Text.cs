using DiffCode.DOM.Core.Abstractions.Texts;
using DiffCode.DOM.Core.Models.Params;
using DiffCode.DOM.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;

namespace DiffCode.DOM.Core.Models.Texts;


/// <summary>
/// Текстовый фрагмент.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class Text : BaseText
{
  public Text(string val) : base(val)
  {

  }

  public Text(Func<string> fn) : base(fn)
  {

  }

  public Text(string val, Expr expr) : base(val, expr)
  {

  }

  public Text(Func<string> fn, Expr expr) : base(fn, expr)
  {

  }

  public Text(DateOnlyParam prm, Expr? expr = null) : base(prm, expr)
  {

  }

  public Text(IParam prm, LambdaExpression expression = null, Expr? expr = null) : base(prm, expression, expr)
  {

  }


}
