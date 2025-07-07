using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Abstractions.Paragraphs;

/// <summary>
/// Базовая модель нумерованного абзаца 3-го уровня иерархии.
/// </summary>
public abstract class BaseNumbered3 : BasePara, INumbered3
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="fragments"></param>
  protected BaseNumbered3(params IText[] fragments) : base(fragments)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="items"></param>
  protected BaseNumbered3(params IPara[] items) : base(items)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="expr"></param>
  protected BaseNumbered3(Expr expr) : base(expr)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  protected BaseNumbered3(params string[] strings) : base(strings)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="objects"></param>
  protected BaseNumbered3(params object[] objects) : base(objects)
  {

  }



  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public sealed override ParaTypeEnum ParaType => NUM | HEADER3;
}
