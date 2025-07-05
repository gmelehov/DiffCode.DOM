using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Models.Paragraphs;

/// <summary>
/// Абзац, вложенный в нумерованный пункт 3-го уровня, наследующий его выравнивание текста.
/// </summary>
public class Indented3 : BaseIndented3
{
  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public Indented3(params IText[] fragments) : base(fragments)
  {

  }

}
