using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Interfaces;
using System.Diagnostics;

namespace DiffCode.DOM.Core.Models.Paragraphs;

/// <summary>
/// Обычный абзац.
/// </summary>
public class Para : BasePara, IPara, ISimplePara
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected ParaTypeEnum? _paraType;




  public Para()
  {
    Align = AlignEnum.BOTH;
  }
  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public Para(Expr exprWrapper, params IText[] fragments) : base(fragments)
  {
    _isActiveOn = exprWrapper;
    Align = AlignEnum.BOTH;
  }
  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public Para(ParaTypeEnum paraType, params IText[] fragments) : base(fragments)
  {
    _paraType = paraType;
    Align = AlignEnum.BOTH;
  }
  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public Para(params IText[] fragments) : base(fragments)
  {
    Align = AlignEnum.BOTH;
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="items"></param>
  public Para(params IPara[] items) : base(items)
  {
    Align = AlignEnum.BOTH;
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  public Para(params string[] strings) : base(strings)
  {
    Align = AlignEnum.BOTH;
  }





  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public override ParaTypeEnum ParaType => _paraType ??= PLAIN | NORMAL;

}
