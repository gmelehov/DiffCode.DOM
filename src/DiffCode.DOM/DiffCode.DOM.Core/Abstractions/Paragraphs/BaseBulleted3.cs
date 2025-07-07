using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Abstractions.Paragraphs;

/// <summary>
/// Базовая модель абзаца-элемента маркированного списка с отступом 3-го уровня иерархии.
/// </summary>
public abstract class BaseBulleted3 : BasePara, IBulleted3
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="fragments"></param>
  protected BaseBulleted3(params IText[] fragments) : base(fragments)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="items"></param>
  protected BaseBulleted3(params IPara[] items) : base(items)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="expr"></param>
  protected BaseBulleted3(Expr expr) : base(expr)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  protected BaseBulleted3(params string[] strings) : base(strings)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="objects"></param>
  protected BaseBulleted3(params object[] objects) : base(objects)
  {

  }




  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public sealed override ParaTypeEnum ParaType => LIST | BUL3;
}
