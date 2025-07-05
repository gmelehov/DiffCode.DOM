using DiffCode.DOM.Core.Abstractions.Docums;
using DiffCode.DOM.Core.Models.Paragraphs;
using System.Diagnostics;

namespace DiffCode.DOM.Core.Models.Docums;

/// <summary>
/// Документ, не имеющий ни родителя, ни дочерних документов.
/// </summary>
[DebuggerDisplay("{Id}: {Content}")]
public class Docum : BaseDocum
{
  public Docum() : base()
  {
    Content = new TitleHeader();
  }



  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Collapsed)]
  public override TitleHeader Content { get; }

}
