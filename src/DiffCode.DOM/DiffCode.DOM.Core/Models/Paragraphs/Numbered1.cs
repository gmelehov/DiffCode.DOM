using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Models.Paragraphs;

/// <summary>
/// Нумерованный абзац 1-го уровня иерархии.
/// </summary>
public class Numbered1 : BaseNumbered1
{
  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public Numbered1(params IText[] fragments) : base(fragments)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="objects"></param>
  public Numbered1(params object[] objects) : base(objects)
  {

  }

}
