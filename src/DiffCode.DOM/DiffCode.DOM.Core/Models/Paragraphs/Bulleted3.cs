using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Models.Paragraphs;

/// <summary>
/// Абзац-элемент маркированного спискка с отступом 3-го уровня.
/// </summary>
public class Bulleted3 : BaseBulleted3
{
  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="fragments">Коллекция текстовых фрагментов для этого абзаца.</param>
  public Bulleted3(params IText[] fragments) : base(fragments)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="objects"></param>
  public Bulleted3(params object[] objects) : base(objects)
  {

  }

}
