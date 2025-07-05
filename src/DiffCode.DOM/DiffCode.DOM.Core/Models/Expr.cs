using DiffCode.DOM.Core.Extensions;
using DiffCode.DOM.Core.Ids;
using DiffCode.DOM.Interfaces;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;


namespace DiffCode.DOM.Core.Models;

/// <summary>
/// Логическое условие.
/// </summary>
public readonly record struct Expr : IExpr
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Expression<Func<bool>> expr;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Func<bool> compiled;




  public Expr([CallerMemberName] string name = "")
  {
    Id = ExprId.New(name);
  }
  public Expr(Expression<Func<bool>> expression, [CallerMemberName] string name = "")
  {
    Id = ExprId.New(name);
    expr = expression;
    compiled = expr?.Compile();
  }





  /// <summary>
  /// Идентификатор.
  /// </summary>
  public readonly ExprId Id { get; }

  /// <summary>
  /// Результат оценки скомпилированного выражения для логического условия.
  /// </summary>
  public readonly bool? Result => compiled?.Invoke();


  ITypedId<string> IExpr.Id => Id;




  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public override string ToString() => $"{Id} --- {Result}";




  public static Expr IsTrue = new(() => true, nameof(IsTrue));


  public static Expr New(Expression<Func<bool>> expression, [CallerMemberName] string name = "") => new(expression, name);





  /// <summary>
  /// Позволяет комбинировать логические условия по схеме УСЛОВИЕ1 И УСЛОВИЕ2
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static Expr operator &(Expr left, Expr right) => new(left.expr.And(right.expr), $"{left.Id} AND {right.Id}");

  /// <summary>
  /// Позволяет комбинировать логические условия по схеме УСЛОВИЕ1 ИЛИ УСЛОВИЕ2
  /// </summary>
  /// <param name="left"></param>
  /// <param name="right"></param>
  /// <returns></returns>
  public static Expr operator |(Expr left, Expr right) => new(left.expr.Or(right.expr), $"{left.Id} OR {right.Id}");

  /// <summary>
  /// Позволяет составлять логическое условие по схеме НЕ УСЛОВИЕ1
  /// </summary>
  /// <param name="left"></param>
  /// <returns></returns>
  public static Expr operator !(Expr left) => new((left.expr ?? IsTrue).Not(), $"NOT {left.Id}");




  public static implicit operator Expression<Func<bool>>(Expr wrapper) => wrapper.expr;

}
