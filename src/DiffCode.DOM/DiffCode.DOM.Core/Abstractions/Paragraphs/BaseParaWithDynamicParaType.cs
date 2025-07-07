using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Interfaces;
using System.Diagnostics;

namespace DiffCode.DOM.Core.Abstractions.Paragraphs;

/// <summary>
/// Базовая модель абзаца, комплексный тип которого должен быть переопределен при создании.
/// </summary>
public abstract class BaseParaWithDynamicParaType : BasePara
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected ParaTypeEnum? _paraType;



  /// <summary>
  /// Создает новый абзац с указанным комплексным типом.
  /// </summary>
  /// <param name="paraType"></param>
  protected BaseParaWithDynamicParaType(ParaTypeEnum paraType) : base()
  {
    _paraType = paraType;
  }
  /// <summary>
  /// Создает новый абзац с указанным комплексным типом.
  /// </summary>
  /// <param name="paraType"></param>
  protected BaseParaWithDynamicParaType(Expr expr, ParaTypeEnum paraType) : base(expr)
  {
    _paraType = paraType;
  }
  /// <summary>
  /// Создает новый абзац с указанным комплексным типом и текстовым содержимым.
  /// </summary>
  /// <param name="paraType"></param>
  /// <param name="texts"></param>
  protected BaseParaWithDynamicParaType(ParaTypeEnum paraType, params IText[] texts) : this(paraType)
  {
    AddRange(texts);
  }
  /// <summary>
  /// Создает новый абзац с указанным комплексным типом и дочерними абзацами.
  /// </summary>
  /// <param name="paraType"></param>
  /// <param name="items"></param>
  protected BaseParaWithDynamicParaType(ParaTypeEnum paraType, params IPara[] items) : this(paraType)
  {
    AddRange(items);
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="strings"></param>
  protected BaseParaWithDynamicParaType(ParaTypeEnum paraType, params string[] strings) : this(paraType)
  {
    AddRange(strings);
  }
  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="objects"></param>
  protected BaseParaWithDynamicParaType(ParaTypeEnum paraType, params object[] objects) : base(objects)
  {
    _paraType = paraType;
  }





  /// <summary>
  /// Возвращает комплексный тип дочернего нумерованного абзаца.
  /// </summary>
  /// <returns></returns>
  public ParaTypeEnum GetNumberedChild() => _paraType switch
  {
    NUM | HEADER1 => NUM | HEADER2,
    NUM | HEADER2 => NUM | HEADER3,
    NUM | HEADER3 => NUM | HEADER4,
    NUM | HEADER1 | NORMAL => NUM | HEADER2,

    _ => PLAIN | NORMAL,
  };

  /// <summary>
  /// Возвращает комплексный тип дочернего ненумерованного абзаца.
  /// </summary>
  /// <returns></returns>
  public ParaTypeEnum GetIndentedChild() => _paraType switch
  {
    NUM | HEADER1 | NORMAL => PLAIN | IND2,
    NUM | HEADER2 => PLAIN | IND2,
    NUM | HEADER3 => PLAIN | IND3,
    NUM | HEADER4 => PLAIN | IND4,

    _ => PLAIN | NORMAL,
  };

  /// <summary>
  /// Возвращает комплексный тип дочернего абзаца-элемента списка.
  /// </summary>
  /// <returns></returns>
  public ParaTypeEnum GetBulletedChild() => _paraType switch
  {
    NUM | HEADER1 | NORMAL => LIST | BUL1,
    NUM | HEADER2 => LIST | BUL2,
    NUM | HEADER3 => LIST | BUL3,

    _ => PLAIN | NORMAL,
  };





  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public sealed override ParaTypeEnum ParaType => _paraType ??= PLAIN | NORMAL;
}
