using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Ids;
using DiffCode.DOM.Core.Models;
using DiffCode.DOM.Core.Models.Paragraphs;
using DiffCode.DOM.Core.Models.Params;
using DiffCode.DOM.Core.Models.Texts;
using DiffCode.DOM.Interfaces;
using DiffCode.Validating.Interfaces.Extensions;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace DiffCode.DOM.Core.Abstractions.Paragraphs;


/// <summary>
/// Базовая модель абзаца.
/// </summary>
[DebuggerDisplay("{DisplayAs}")]
public abstract class BasePara : BaseValidatable<IPara>, IPara
{
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected List<IText> _texts;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected List<IPara> _paragraphs;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected IPara _parent;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected Expr _isActiveOn;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  protected bool _isActiveOnLocked = false;



  /// <summary>
  /// Создает новый абзац.
  /// </summary>
  protected BasePara() : base()
  {
    _texts ??= [];
    _paragraphs ??= [];
    Lines = new BeforeAfter<int>(0, 0);
    Spacing = new BeforeAfter<int>(0, 0);
  }
  /// <summary>
  /// Создает новый абзац и устанавливает для него условие видимости/активности.
  /// </summary>
  /// <param name="expr"></param>
  protected BasePara(Expr expr) : base()
  {
    _texts ??= [];
    _paragraphs ??= [];
    Lines = new BeforeAfter<int>(0, 0);
    Spacing = new BeforeAfter<int>(0, 0);
    SetActiveOn<BasePara>(expr);
  }
  /// <summary>
  /// Создает новый абзац с указанным текстовым содержимым.
  /// </summary>
  /// <param name="texts"></param>
  protected BasePara(params IText[] texts)
  {
    foreach (var text in texts)
    {
      text.Parent = this;
    }

    _texts ??= new List<IText>(texts);
    _paragraphs ??= [];
    Lines = new BeforeAfter<int>(0, 0);
    Spacing = new BeforeAfter<int>(0, 0);
  }
  /// <summary>
  /// Создает новый абзац с указанными дочерними абзацами.
  /// </summary>
  /// <param name="items"></param>
  protected BasePara(params IPara[] items)
  {
    _paragraphs ??= [];
    AddRange(items);
    _texts ??= [];
    Lines = new BeforeAfter<int>(0, 0);
    Spacing = new BeforeAfter<int>(0, 0);
  }
  /// <summary>
  /// Создает новый абзац с текстовыми фрагментами, сформированными
  /// из указанного списка строк.
  /// </summary>
  /// <param name="strings"></param>
  protected BasePara(params string[] strings)
  {
    _texts ??= [];
    _paragraphs ??= [];
    Lines = new BeforeAfter<int>(0, 0);
    Spacing = new BeforeAfter<int>(0, 0);
    AddRange(strings);
  }

  protected BasePara(params object[] objects)
  {
    _texts ??= [];
    _paragraphs ??= [];
    Lines = new BeforeAfter<int>(0, 0);
    Spacing = new BeforeAfter<int>(0, 0);
    foreach (var obj in objects)
    {
      if (obj is IPara pp)
      {
        AddRange(pp);
      }
      else if (obj is IText tt)
      {
        AddRange(tt);
      }
      else if (obj is string ss)
      {
        AddRange(new Text(() => ss));
      }
      else if (obj is DateOnlyParam prmDate)
      {
        AddRange(new Text(prmDate));
      }
      else if (obj is IParam prm)
      {
        AddRange(new Text(prm));
      }
      else if(obj is Func<string> fn)
      {
        AddRange(new Text(fn));
      }
      else if (obj is Expr expr)
      {
        SetActiveOn<BasePara>(expr);
      }
    }
  }






  /// <summary>
  /// <inheritdoc />
  /// </summary>
  public void LockActiveOn() => _isActiveOnLocked = true;

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="expr"></param>
  /// <returns></returns>
  public T SetActiveOn<T>(IExpr expr) where T : IActiveState
  {
    if (!_isActiveOnLocked)
    {
      _isActiveOn = (Expr)expr;
    }

    return (T)(IActiveState)this;
  }

  /// <summary>
  /// 
  /// </summary>
  public void SetNumberings()
  {
    if (HasAnyParaTypes(HEADER1, NEWPAGE, TITLE))
    {
      var sections = new IPara[] { };

      _paragraphs.ForEach(p =>
      {
        Func<IPara, bool> hdr1 = para => para.IsActive && para.HasAnyParaTypes(HEADER1, NUM);
        var hh = hdr1(p);
        if (hdr1(p))
        {
          sections = sections.Concat([p]).ToArray();
        }
        else
        {
          sections = sections.Concat(p.FindMany(hdr1)).ToArray();
        }
      });

      int order = 1;
      this.FluentForEachAction(sections, p1 =>
      {
        p1.Numbering = $"{order}.";
        var points = p1.FindMany(w => w.IsActive && w.HasAllParaTypes(NUM, HEADER2));
        int suborder = 1;

        p1.FluentForEachAction(points, p2 =>
        {
          p2.Numbering = $"{p1.Numbering}{suborder}.";
          var subpoints = p2.FindMany(w => w.IsActive && w.HasAllParaTypes(NUM, HEADER3));
          int subsuborder = 1;

          p2.FluentForEachAction(subpoints, p3 =>
          {
            p3.Numbering = $"{p2.Numbering}{subsuborder}.";
            var subsubpoints = p3.FindMany(w => w.IsActive && w.HasAllParaTypes(NUM, HEADER4));
            int subsubsuborder = 1;

            p3.FluentForEachAction(subsubpoints, p4 =>
            {
              p4.Numbering = $"{p3.Numbering}{subsubsuborder}.";
              subsubsuborder++;
            });
            subsuborder++;
          });
          suborder++;
        });
        order++;
      });
    }
  }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="parent"></param>
  /// <returns></returns>
  public IPara SetParent(IPara parent) => this.FluentAction(parent, p =>
  {
    _parent = p;
    _paragraphs.ForEach(f => f.SetParent(this));
  });

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public IEnumerable<IPara> GetParentsAxis()
  {
    var ret = new List<IPara>(10);
    var curr = this;

    while (curr != null && curr.HasParent())
    {
      ret.Add(curr);
      curr = (BasePara)curr.Parent;
    }

    return ret;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected int FindMaxChildHeight()
  {
    int max = 0;
    _paragraphs.ForEach(f =>
    {
      max = Math.Max(max, f.GetHeight());
    });

    return max;
  }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public ushort GetOrder() => (ushort)(Parent?.Paragraphs.IndexOf(this) + 1 ?? 0);

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public int GetHeight() => Paragraphs.Count == 0 ? 0 : 1 + FindMaxChildHeight();

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public bool HasParent() => Parent != null && Parent.HasChildren();

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public bool HasChildren() => Paragraphs.Count > 0;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="items"></param>
  /// <returns></returns>
  public IPara AddRange(params IPara[] items) => this.FluentForEachAction(items, it =>
  {
    it.SetParent(this);
    Paragraphs.Add(it);
  });

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="items"></param>
  /// <returns></returns>
  public IPara AddRange(params string[] items) => this.FluentForEachAction(items, it =>
  {
    var text = new Models.Texts.Text(it)
    {
      Parent = this
    };
    _texts.Add(text);
  });

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="predicate"></param>
  /// <param name="items"></param>
  /// <returns></returns>
  public IPara InsertAfter(Func<IPara, bool> predicate, params IPara[] items) => this.FluentAction(items, it =>
  {
    var found = FindOne(predicate);
    if (found != null && found.Parent != null)
    {
      foreach (var i in it)
        i.SetParent(found.Parent);

      var index = found.Parent.Paragraphs.IndexOf(found);

      foreach (var i in it)
      {
        found.Parent.Paragraphs.Insert(index + 1, i);
        index++;
      }
    }
  });

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="texts"></param>
  /// <returns></returns>
  public IPara AddRange(params IText[] texts) => this.FluentAction(texts, el =>
  {
    foreach (var t in texts)
      t.Parent = this;

    _texts.AddRange(texts);
  });

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public virtual string GetFragmentsText()
  {
    var ret = new StringBuilder();
    if (IsActive)
    {
      if (Texts.Count > 0)
      {
        foreach (var it in Texts)
        {
          ret.Append(Regex.Replace(it.ToString(), @"[\s]{2,}", " "));
        }
      }
    }

    return ret.ToString();
  }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public virtual string GetChildrenText()
  {
    var ret = new StringBuilder();
    _paragraphs.ForEach(c => ret.Append(c.GetFullText()));
    return ret.ToString();
  }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public virtual string GetFullText()
  {
    if (IsActive)
    {
      var ret = new StringBuilder();

      if (Lines.Before > 0)
      {
        foreach (var line in Enumerable.Range(0, Lines.Before))
          ret.AppendLine();
      }

      ret.Append($"{Numbering}{(!string.IsNullOrWhiteSpace(Numbering) ? " " : "")}{GetFragmentsText()}");

      if (!string.IsNullOrWhiteSpace(GetFragmentsText()))
        ret.AppendLine();

      ret.Append(GetChildrenText());

      if (Lines.After > 0)
      {
        foreach (var line in Enumerable.Range(0, Lines.After))
          ret.AppendLine();
      }

      return ret.ToString().TrimStart(' ');
    }
    else
    {
      return string.Empty;
    }
  }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="predicate"></param>
  /// <returns></returns>
  public IEnumerable<IPara> FindMany(Func<IPara, bool> predicate) => Paragraphs.Where(predicate).Concat(Paragraphs.SelectMany(s => s.FindMany(predicate)));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="predicate"></param>
  /// <returns></returns>
  public IPara FindOne(Func<IPara, bool> predicate) => FindMany(predicate).FirstOrDefault();

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public T FindOne<T>() where T : IPara => (T)FindOne(p => p is T);

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <returns></returns>
  public IEnumerable<IPara> GetAll() => FindMany(t => true).Prepend(this);

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="predicate"></param>
  /// <returns></returns>
  public IEnumerable<IPara> GetAll(Func<IPara, bool> predicate) => predicate(this) ? FindMany(predicate).Prepend(this) : FindMany(predicate);

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <returns></returns>
  public IEnumerable<T> FindText<T>() where T : IText => Texts.OfType<T>().Concat(Paragraphs.SelectMany(s => s.FindText<T>())).Distinct();

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="predicate"></param>
  /// <returns></returns>
  public IEnumerable<T> FindText<T>(Func<T, bool> predicate) where T : IText => FindText<T>().Where(predicate);

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="enums"></param>
  /// <returns></returns>
  public bool HasAllParaTypes(params ParaTypeEnum[] enums) => enums.All(a => ParaType.HasFlag(a));

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  /// <param name="enums"></param>
  /// <returns></returns>
  public bool HasAnyParaTypes(params ParaTypeEnum[] enums) => enums.Any(a => ParaType.HasFlag(a));





  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public ParaId Id => GetParentsAxis().Count() == 0 ? ParaId.New(0) : ParaId.New(new ushort[] { 0 }.Concat(GetParentsAxis().Reverse()?.Select(s => s.GetOrder())).ToArray());

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  ITypedId<string> IPara.Id => Id;


  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public IPara Parent => _parent;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public List<IText> Texts => _texts;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public List<IPara> Paragraphs => _paragraphs;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public IBeforeAfter<int> Lines { get; set; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public IBeforeAfter<int> Spacing { get; set; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public string Numbering { get; set; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public abstract ParaTypeEnum ParaType { get; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public AlignEnum Align { get; set; }

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public virtual string AsText => ToString();

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public virtual bool IsActive => IsActiveOn.Result ?? true;

  /// <summary>
  /// <inheritdoc/>
  /// </summary>
  public bool IsActiveOnLocked { get; }

  /// <summary>
  /// <inheritdoc />
  /// </summary>
  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  public Expr IsActiveOn => _isActiveOn;

  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  IExpr IActiveState.IsActiveOn => IsActiveOn;




  [DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private string DisplayAs => $"{GetType().Name}, {Id} --- {AsText}";


  public override string ToString() => $"{GetFullText()}";









  public static Para Para(params object[] objects) => new(objects);


  public static Bulleted1 Bulleted1(params object[] objects) => new(objects);


  public static Bulleted2 Bulleted2(params object[] objects) => new(objects);


  public static Bulleted3 Bulleted3(params object[] objects) => new(objects);


  public static Cell Cell(params object[] objects) => new(objects);


  public static Grid Grid(params object[] objects) => new(objects);


  public static Header1 Header1(params object[] objects) => new(objects);


  public static Header2 Header2(params object[] objects) => new(objects);


  public static Header3 Header3(params object[] objects) => new(objects);


  public static Header4 Header4(params object[] objects) => new(objects);


  public static Header5 Header5(params object[] objects) => new(objects);


  public static Indented2 Indented2(params object[] objects) => new(objects);


  public static Indented3 Indented3(params object[] objects) => new(objects);


  public static Numbered1 Numbered1(params object[] objects) => new(objects);


  public static Numbered2 Numbered2(params object[] objects) => new(objects);


  public static Numbered3 Numbered3(params object[] objects) => new(objects);


  public static Numbered4 Numbered4(params object[] objects) => new(objects);


  public static NumHeader1 NumHeader1(params object[] objects) => new(objects);


  public static TitleHeader TitleHeader(params object[] objects) => new(objects);

}
