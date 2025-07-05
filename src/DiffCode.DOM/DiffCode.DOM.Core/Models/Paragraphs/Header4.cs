using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Models.Paragraphs;

/// <summary>
/// Ненумерованный абзац-заголовок 4-го уровня иерархии.
/// </summary>
public class Header4 : BaseHeader4
{
  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public Header4(params IText[] fragments) : base(fragments)
  {

  }


}
