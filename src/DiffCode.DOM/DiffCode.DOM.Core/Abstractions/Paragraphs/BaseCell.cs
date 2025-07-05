using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Interfaces;
using DiffCode.Validating.Interfaces.Extensions;

namespace DiffCode.DOM.Core.Abstractions.Paragraphs;

/// <summary>
/// Базовая модель абзаца-ячейки таблицы.
/// </summary>
public abstract class BaseCell : BasePara, ICell
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  protected BaseCell() : base()
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="fragments"></param>
  protected BaseCell(params IText[] fragments) : base(fragments)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="items"></param>
  protected BaseCell(params IPara[] items) : base(items)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="expr"></param>
  protected BaseCell(Expr expr) : base(expr)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  protected BaseCell(params string[] strings) : base(strings)
  {

  }





  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="col"></param>
  /// <param name="row"></param>
  /// <param name="colspan"></param>
  /// <param name="rowspan"></param>
  /// <returns></returns>
  public BaseCell SetInnerCell(int col = 1, int row = 1, int colspan = 1, int rowspan = 1) => this.FluentAction(() =>
  {
    Col = col;
    Row = row;
    ColSpan = colspan;
    RowSpan = rowspan;
  });

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="alignEnum"></param>
  /// <returns></returns>
  public BaseCell SetAlign(AlignEnum alignEnum) => this.FluentAction(() => Align = alignEnum);


  ICell ICell.SetInnerCell(int col, int row, int colspan, int rowspan) => SetInnerCell(col, row, colspan, rowspan);

  ICell ICell.SetAlign(AlignEnum alignEnum) => SetAlign(alignEnum);





  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public int Col { get; protected set; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public int Row { get; protected set; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public int ColSpan { get; protected set; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public int RowSpan { get; protected set; }



  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public sealed override ParaTypeEnum ParaType => PLAIN | NORMAL;
}
