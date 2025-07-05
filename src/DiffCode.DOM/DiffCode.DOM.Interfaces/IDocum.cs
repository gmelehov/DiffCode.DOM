using DiffCode.Validating.Interfaces;

namespace DiffCode.DOM.Interfaces;

/// <summary>
/// Интерфейс документа.
/// </summary>
public interface IDocum : IActiveState, IWithFluentAction, IValidatable<IDocum>
{


  /// <summary>
  /// Добавляет указанные абзацы к корневому абзацу документа.
  /// </summary>
  /// <param name="elements"></param>
  /// <returns></returns>
  IDocum AddRange(params IPara[] elements);


  IDocum AddRange(params IDocum[] docums);

  /// <summary>
  /// Устанавливает родительский документ для текущего документа.
  /// </summary>
  /// <param name="parent">Документ, который необходимо установить в качестве родительского.</param>
  /// <returns></returns>
  IDocum SetParent(IDocum parent);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="predicate"></param>
  /// <returns></returns>
  IEnumerable<IDocum> FindMany(Func<IDocum, bool> predicate);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  IEnumerable<IDocum> GetAll();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="predicate"></param>
  /// <returns></returns>
  IEnumerable<IDocum> GetAll(Func<IDocum, bool> predicate);


  bool HasParent();


  bool HasChildren();


  int GetHeight();


  ushort GetOrder();





  /// <summary>
  /// Идентификатор документа.
  /// </summary>
  ITypedId<string> Id { get; }

  /// <summary>
  /// Ссылка на родительский документ.
  /// </summary>
  IDocum Parent { get; }

  /// <summary>
  /// Ссылка на дочерние документы.
  /// </summary>
  List<IDocum> Documents { get; }

  /// <summary>
  /// Корневой абзац документа.
  /// </summary>
  IPara Content { get; }

  /// <summary>
  /// Полное текстовое представление этого документа.
  /// </summary>
  string AsText { get; }

  /// <summary>
  /// Текстовое представление нумерации.
  /// </summary>
  string Numbering { get; }

}
