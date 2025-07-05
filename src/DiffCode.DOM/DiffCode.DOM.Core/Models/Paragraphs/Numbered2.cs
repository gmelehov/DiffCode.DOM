using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Models.Paragraphs;

/// <summary>
/// Нумерованный абзац 2-го уровня иерархии.
/// </summary>
public class Numbered2 : BaseNumbered2
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public Numbered2(params IText[] fragments) : base(fragments)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  public Numbered2(params string[] strings) : base(strings)
  {

  }
}
