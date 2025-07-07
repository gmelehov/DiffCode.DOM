using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Abstractions.Paragraphs;

/// <summary>
/// Базовая модель ненумерованного абзаца-заголовка 1-го уровня иерархии.
/// </summary>
public abstract class BaseHeader1 : BasePara, IHeader1
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="fragments"></param>
  protected BaseHeader1(params IText[] fragments) : base(fragments)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="items"></param>
  protected BaseHeader1(params IPara[] items) : base(items)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="expr"></param>
  protected BaseHeader1(Expr expr) : base(expr)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  protected BaseHeader1(params string[] strings) : base(strings)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="objects"></param>
  protected BaseHeader1(params object[] objects) : base(objects)
  {

  }



  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public sealed override ParaTypeEnum ParaType => PLAIN | HEADER1;
}
