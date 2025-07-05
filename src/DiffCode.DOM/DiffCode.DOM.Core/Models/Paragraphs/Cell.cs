using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Core.Extensions;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Models.Paragraphs;

/// <summary>
/// Абзац-ячейка таблицы.
/// </summary>
public class Cell : BaseCell
{
  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public Cell(Expr exprWrapper, params IText[] fragments) : base(fragments)
  {
    _isActiveOn = exprWrapper;
  }
  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public Cell(params IText[] fragments) : base(fragments)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="items"></param>
  public Cell(params IPara[] items) : base(items)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  public Cell(params object[] strings) : base()
  {
    this.With(strings);
  }


}
