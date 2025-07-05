using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Models.Paragraphs;

/// <summary>
/// Ненумерованный абзац-заголовок 5-го уровня иерархии.
/// </summary>
public class Header5 : BaseHeader5
{
  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public Header5(params IText[] fragments) : base(fragments)
  {

  }


}
