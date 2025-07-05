using DiffCode.DOM.Common.Enums;


namespace DiffCode.DOM.Interfaces;

/// <summary>
/// Интерфейс абзаца-ячейки таблицы.
/// </summary>
public interface ICell : IPara
{


  ICell SetInnerCell(int col = 1, int row = 1, int colspan = 1, int rowspan = 1);


  ICell SetAlign(AlignEnum alignEnum);




  /// <summary>
  /// Номер столбца.
  /// </summary>
  int Col { get; }

  /// <summary>
  /// Номер строки.
  /// </summary>
  int Row { get; }

  /// <summary>
  /// Кол-во столбцов, занимаемое этой ячейкой.
  /// </summary>
  int ColSpan { get; }

  /// <summary>
  /// Кол-во строк, занимаемое этой ячейкой.
  /// </summary>
  int RowSpan { get; }

}
