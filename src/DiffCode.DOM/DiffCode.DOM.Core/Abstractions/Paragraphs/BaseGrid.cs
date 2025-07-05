using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Interfaces;
using DiffCode.Validating.Interfaces.Extensions;

namespace DiffCode.DOM.Core.Abstractions.Paragraphs;

/// <summary>
/// Базовая модель абзаца-ячейки таблицы.
/// </summary>
public abstract class BaseGrid : BasePara, IGrid
{
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="fragments"></param>
  protected BaseGrid(params IText[] fragments) : base(fragments)
  {
    Margins = new Box<int>();
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="items"></param>
  protected BaseGrid(params IPara[] items) : base(items)
  {
    Margins = new Box<int>();
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="expr"></param>
  protected BaseGrid(Expr expr) : base(expr)
  {

  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  protected BaseGrid(params string[] strings) : base(strings)
  {

  }



  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="cols"></param>
  /// <param name="rows"></param>
  /// <param name="border"></param>
  /// <param name="hasHeader"></param>
  /// <returns></returns>
  public BaseGrid SetInnerGrid(int cols, int rows, bool border = false, bool hasHeader = false) => this.FluentAction(() =>
  {
    Cols = cols;
    Rows = rows;
    Border = border;
    HasHeader = hasHeader;
  });

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="heights"></param>
  /// <returns></returns>
  public BaseGrid SetInnerGridHeights(params decimal[] heights) => this.FluentAction(() => Heights = heights);

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="margins"></param>
  /// <returns></returns>
  /// <exception cref="ArgumentNullException"></exception>
  public BaseGrid SetInnerGridMargins(IBox<int> margins) => this.FluentAction(() =>
  {
    if (margins == null)
      throw new ArgumentNullException(nameof(margins));

    Margins = new Box<int>(margins.L, margins.T, margins.R, margins.B);
  });

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="marg"></param>
  /// <returns></returns>
  public BaseGrid SetInnerGridMargins(int marg) => SetInnerGridMargins(new Box<int>(marg));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="widths"></param>
  /// <returns></returns>
  public BaseGrid SetInnerGridWidths(params decimal[] widths) => this.FluentAction(() => Widths = widths);


  IGrid IGrid.SetInnerGrid(int cols, int rows, bool border, bool hasHeader) => SetInnerGrid(cols, rows, border, hasHeader);

  IGrid IGrid.SetInnerGridMargins(IBox<int> margins) => SetInnerGridMargins(margins);

  IGrid IGrid.SetInnerGridWidths(params decimal[] widths) => SetInnerGridWidths(widths);

  IGrid IGrid.SetInnerGridHeights(params decimal[] heights) => SetInnerGridHeights(heights);






  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public int Cols { get; protected set; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public int Rows { get; protected set; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public IBox<int> Margins { get; protected set; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public bool Border { get; protected set; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public bool HasHeader { get; protected set; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public decimal[] Widths { get; protected set; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public decimal[] Heights { get; protected set; }



  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public sealed override ParaTypeEnum ParaType => TABLE | NORMAL;

}
