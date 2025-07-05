using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Abstractions.Paragraphs;

/// <summary>
/// Базовая модель нумерованного абзаца 1-го уровня иерархии,
/// текст которого является его заголовком.
/// </summary>
public abstract class BaseNumHeader1 : BasePara, INumHeader1
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="fragments"></param>
  protected BaseNumHeader1(params IText[] fragments) : base(fragments)
  {
    Lines = new BeforeAfter<int>(1, 1);
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="items"></param>
  protected BaseNumHeader1(params IPara[] items) : base(items)
  {
    Lines = new BeforeAfter<int>(1, 1);
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="expr"></param>
  protected BaseNumHeader1(Expr expr) : base(expr)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  protected BaseNumHeader1(params string[] strings) : base(strings)
  {

  }



  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public sealed override ParaTypeEnum ParaType => NUM | HEADER1;
}
