using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Models.Paragraphs;

/// <summary>
/// Нумерованный абзац 4-го уровня иерархии.
/// </summary>
public class Numbered4 : BaseNumbered4
{
  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public Numbered4(params IText[] fragments) : base(fragments)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="objects"></param>
  public Numbered4(params object[] objects) : base(objects)
  {

  }

}
