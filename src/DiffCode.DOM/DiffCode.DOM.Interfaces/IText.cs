using DiffCode.DOM.Common.Enums;
using DiffCode.Validating.Interfaces;
using System.Diagnostics;


namespace DiffCode.DOM.Interfaces;

/// <summary>
/// Интерфейс текстового фрагмента.
/// </summary>
public interface IText : IActiveState, IWithFluentAction, IValidatable<IText>
{

  /// <summary>
  /// Ссылка на родительский абзац.
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  IPara Parent { get; set; }

  /// <summary>
  /// Кол-во пустых строк, выводимых до и после текстового фрагмента.
  /// </summary>
  IBeforeAfter<int> Lines { get; set; }

  /// <summary>
  /// Форматирование текстового фрагмента.
  /// </summary>
  TextFormat Format { get; set; }

  /// <summary>
  /// Обычное текстовое представление фрагмента.
  /// </summary>
  string Content { get; }



  bool IsComputed { get; }


  bool IsFromParam { get; }

}
