using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Interfaces;

namespace DiffCode.DOM.Core.Models.Paragraphs;

/// <summary>
/// Абзац-заголовок документа.
/// </summary>
public class TitleHeader : BaseTitleHeader
{
  public TitleHeader() : base()
  {

  }

  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="fragments"></param>
  public TitleHeader(params IText[] fragments) : base(fragments)
  {

  }
  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  /// <param name="items"></param>
  public TitleHeader(params IPara[] items) : base(items)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  public TitleHeader(params string[] strings) : base(strings)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="objects"></param>
  public TitleHeader(params object[] objects) : base(objects)
  {

  }

}