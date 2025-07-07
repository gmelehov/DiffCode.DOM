using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Models.Paragraphs;

/// <summary>
/// Нумерованный абзац 1-го уровня иерархии, текст которого является его заголовком.
/// </summary>
public class NumHeader1 : BaseNumHeader1
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public NumHeader1(params IText[] fragments) : base(fragments)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  public NumHeader1(params string[] strings) : base(strings)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="objects"></param>
  public NumHeader1(params object[] objects) : base(objects)
  {

  }
}
