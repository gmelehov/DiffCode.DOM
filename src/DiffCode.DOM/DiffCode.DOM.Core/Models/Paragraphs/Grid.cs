using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Models.Paragraphs;

/// <summary>
/// Абзац-таблица.
/// </summary>
public class Grid : BaseGrid
{
  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public Grid(params IText[] fragments) : base(fragments)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="objects"></param>
  public Grid(params object[] objects) : base(objects)
  {

  }

}
