using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Abstractions.Paragraphs;

/// <summary>
/// Базовая модель абзаца-элемента маркированного списка с отступом 1-го уровня иерархии.
/// </summary>
public abstract class BaseBulleted1 : BasePara, IBulleted1
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="fragments"></param>
  protected BaseBulleted1(params IText[] fragments) : base(fragments)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="items"></param>
  protected BaseBulleted1(params IPara[] items) : base(items)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="expr"></param>
  protected BaseBulleted1(Expr expr) : base(expr)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  protected BaseBulleted1(params string[] strings) : base(strings)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="objects"></param>
  protected BaseBulleted1(params object[] objects) : base(objects)
  {

  }





  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public sealed override ParaTypeEnum ParaType => LIST | BUL1;
}
