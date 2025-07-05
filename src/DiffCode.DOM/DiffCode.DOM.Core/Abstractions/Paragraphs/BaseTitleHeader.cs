using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Abstractions.Paragraphs;

/// <summary>
/// Базовая модель абзаца-заголовка документа.
/// </summary>
public abstract class BaseTitleHeader : BasePara, ITitleHeader
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  protected BaseTitleHeader() : base()
  {
    Align = AlignEnum.CENTER;
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="fragments"></param>
  protected BaseTitleHeader(params IText[] fragments) : base(fragments)
  {
    Align = AlignEnum.CENTER;
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="items"></param>
  protected BaseTitleHeader(params IPara[] items) : base(items)
  {
    Align = AlignEnum.CENTER;
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="expr"></param>
  protected BaseTitleHeader(Expr expr) : base(expr)
  {
    Align = AlignEnum.CENTER;
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  protected BaseTitleHeader(params string[] strings) : base(strings)
  {
    Align = AlignEnum.CENTER;
  }



  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public sealed override ParaTypeEnum ParaType => TITLE | HEADER1;
}