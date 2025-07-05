namespace DiffCode.DOM.Interfaces;

/// <summary>
/// Интерфейс абзаца-таблицы.
/// </summary>
public interface IGrid : IPara, IParentOfCell
{


  IGrid SetInnerGrid(int cols, int rows, bool border = false, bool hasHeader = false);


  IGrid SetInnerGridMargins(IBox<int> margins);


  IGrid SetInnerGridWidths(params decimal[] widths);


  IGrid SetInnerGridHeights(params decimal[] heights);





  /// <summary>
  /// Кол-во столбцов.
  /// </summary>
  int Cols { get; }

  /// <summary>
  /// Кол-во строк.
  /// </summary>
  int Rows { get; }

  /// <summary>
  /// Отступы внутри каждой ячейки таблицы.
  /// </summary>
  IBox<int> Margins { get; }

  /// <summary>
  /// Наличие границ ячеек.
  /// </summary>
  bool Border { get; }

  /// <summary>
  /// Наличие заголовка таблицы.
  /// </summary>
  bool HasHeader { get; }

  /// <summary>
  /// Массив относительных ширин столбцов.
  /// </summary>
  decimal[] Widths { get; }

  /// <summary>
  /// Массив относительных высот строк.
  /// </summary>
  decimal[] Heights { get; }

}
