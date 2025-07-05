using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Abstractions.Paragraphs;
using DiffCode.DOM.Rendering.Models;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.Shapes;
using MigraDocCore.DocumentObjectModel.Tables;
using MigraDocCore.Rendering;

namespace DiffCode.DOM.Rendering.Services;

/// <summary>
/// Сервис для рендеринга документов в PDF.
/// </summary>
public class MigraDocRenderingService
{
  //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly MigraDocStyles _st;

  //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Func<Document> _docFn;

  //[DebuggerBrowsable(DebuggerBrowsableState.Never)]
  private readonly Func<ParaTypeEnum, Style> _styleMapper;





  public MigraDocRenderingService(MigraDocStyles styles, Func<Document> func, Func<ParaTypeEnum, Style> styleMapper)
  {
    _st = styles;
    _docFn = func;
    _styleMapper = styleMapper;
  }


  public Task<Document> MakePDF(string fileName, params IDocum[] docums) => Task.Run(() =>
  {
    var doc = ConvertDocument(docums);

    PdfDocumentRenderer renderer = new(true);

    renderer.Document = doc;
    renderer.RenderDocument();

    using (var str = new MemoryStream())
    {
      renderer.PdfDocument.Save(str);
      File.WriteAllBytes(fileName, str.ToArray());
    }

    return Task.FromResult(doc);
  });


  public Document ConvertDocument(IEnumerable<IDocum> documents)
  {
    //var doc = _docFn();

    var ret = new Document();
    //var styles = sp.GetService<MigraDocStyles>();

    var styles = _st;

    ret.Styles.Add(styles.MainHeader);
    ret.Styles.Add(styles.NumberedHeader1);
    ret.Styles.Add(styles.NumberedHeader2);
    ret.Styles.Add(styles.NumberedHeader3);
    ret.Styles.Add(styles.NumberedHeader4);
    ret.Styles.Add(styles.Norm);
    ret.Styles.Add(styles.NumberedNormal1);
    ret.Styles.Add(styles.NumberedNormal2);
    ret.Styles.Add(styles.Level2);
    ret.Styles.Add(styles.Level3);
    ret.Styles.Add(styles.BulletedList1);
    ret.Styles.Add(styles.BulletedList2);
    ret.Styles.Add(styles.BulletedList3);
    ret.Styles.Add(styles.Header1);
    ret.Styles.Add(styles.Header2);
    ret.Styles.Add(styles.Header3);


    foreach (var d in documents)
    {
      var pageSetup = ret.DefaultPageSetup.Clone();
      pageSetup.TopMargin = new(10, UnitType.Millimeter);
      pageSetup.BottomMargin = new(10, UnitType.Millimeter);
      pageSetup.LeftMargin = new(10, UnitType.Millimeter);
      pageSetup.RightMargin = new(10, UnitType.Millimeter);

      Section section = ret.AddSection();
      section.PageSetup = pageSetup;
      section.AddPageBreak();
      ConvertParagraph(section, d.Content);
    }

    return ret;
  }


  public Section ConvertParagraph(Section sect, IPara para)
  {
    if (para.IsActive)
    {
      var ret = para.ParaType switch
      {
        ParaTypeEnum p when p.HasFlag(PLAIN) || p.HasFlag(TITLE) || p.HasFlag(NUM) || p.HasFlag(LIST) => ConvertListOrNumberedOrPlainOrTitlePara(sect, para),
        ParaTypeEnum p when p.HasFlag(TABLE) => ConvertTablePara(sect, para),

        _ => ConvertListOrNumberedOrPlainOrTitlePara(sect, para)
      };

      return ret;
    }

    return sect;
  }




  public Section ConvertImageParagraph(Section sect, IPara para)
  {
    var ret = sect.AddParagraph();
    //ret.AddImage(para.AsText);
    //sect.AddImage(para.AsText);
    return sect;
  }


  public Section ConvertListOrNumberedOrPlainOrTitlePara(Section sect, IPara para)
  {
    Paragraph lastParagraph = null;

    if (sect.Elements.Count > 0)
      lastParagraph = sect.LastParagraph;


    var ret = sect.AddParagraph();

    ret.Style = _styleMapper(para.ParaType).Name;

    AppendLinesBefore(ret, para);
    ret.Format.Alignment = ConvertAlignment(para.Align);
    AppendFragments(ret, para);

    AppendSpacingBefore(ret, para);
    AppendSpacingAfter(ret, para);

    foreach (var pp in para.Paragraphs.Where(w => w.IsActive).ToList())
      ConvertParagraph(sect, pp);

    AppendLinesAfter(ret, para);

    return sect;
  }


  public Section ConvertTablePara(Section sect, IPara para)
  {
    var pageEffectiveWidth = sect.PageSetup.PageWidth.Millimeter - sect.PageSetup.LeftMargin.Millimeter - sect.PageSetup.RightMargin.Millimeter;

    Table table;

    var lastParagraph = sect.LastParagraph;

    if (para.Lines?.Before > 0)
    {
      var pbefore = sect.AddParagraph();
      AppendLinesBefore(pbefore, para);
      AppendSpacingBefore(pbefore, para);


    }

    if (lastParagraph.Style == _st.NumberedNormal1.Name)
    {
      var tf = sect.AddTextFrame();
      tf.Left = ShapePosition.Left;
      tf.RelativeHorizontal = RelativeHorizontal.Page;
      tf.MarginLeft = new(10, UnitType.Millimeter);

      var gridPara = sect.AddParagraph();
      gridPara.Style = _styleMapper(para.ParaType).Name;
      gridPara.Format.Alignment = ConvertAlignment(para.Align);
      AppendFragments(gridPara, para);
      tf.Add(gridPara);

      table = tf.AddTable();
    }
    else
    {
      var gridPara = sect.AddParagraph();
      gridPara.Style = _styleMapper(para.ParaType).Name;
      gridPara.Format.Alignment = ConvertAlignment(para.Align);
      AppendFragments(gridPara, para);

      table = sect.AddTable();
    }



    var widths = ((BaseGrid)para).Widths.Select(s => new Unit((double)pageEffectiveWidth * (double)s / 100, UnitType.Millimeter)).ToList();

    table.BottomPadding = new(((BaseGrid)para).Margins.B, UnitType.Point);
    table.LeftPadding = new(((BaseGrid)para).Margins.L, UnitType.Point);
    table.RightPadding = new(((BaseGrid)para).Margins.R, UnitType.Point);
    table.TopPadding = new(((BaseGrid)para).Margins.T, UnitType.Point);

    if (((BaseGrid)para).Border)
    {
      Func<Border> new_Border = () => new Border() { Style = BorderStyle.Single, Width = new(0.5, UnitType.Point), Color = new(180, 180, 180) };

      table.Borders.Bottom = new_Border();
      table.Borders.Top = new_Border();
      table.Borders.Left = new_Border();
      table.Borders.Right = new_Border();
    }

    var colsCount = Enumerable.Range(1, ((BaseGrid)para).Cols);
    var rowsCount = Enumerable.Range(1, ((BaseGrid)para).Rows);

    foreach (var c in colsCount)
      table.AddColumn(widths[c - 1]);
    foreach (var r in rowsCount)
      table.AddRow();


    foreach (var c in colsCount)
    {
      foreach (var r in rowsCount)
      {
        var cell = table.Rows[r - 1].Cells[c - 1];
        var pp = ((BaseGrid)para).Paragraphs.FirstOrDefault(f => ((BaseCell)f).Row == r && ((BaseCell)f).Col == c);

        if (pp != null && pp.IsActive)
        {
          if (((BaseCell)pp).ColSpan > 1)
            cell.MergeRight = ((BaseCell)pp).ColSpan - 1;

          if (((BaseCell)pp).RowSpan > 1)
            cell.MergeDown = ((BaseCell)pp).RowSpan - 1;

          cell.VerticalAlignment = ConvertVerticalAlignment(pp.Align);

          if (true || pp.Texts.Count > 0)
          {
            var pcell = cell.AddParagraph();
            pcell.Format.Alignment = ConvertAlignment(pp.Align);
            pcell.Style = _styleMapper(pp.ParaType).Name;
            AppendFragments(pcell, pp);
          }

          foreach (var a in pp.Paragraphs.Where(w => w.IsActive))
            ConvertCellPara(cell, a);

        }
      }
    }

    if (((BaseGrid)para).HasHeader)
    {
      table.Rows[0].HeadingFormat = true;
      table.Rows[0].Shading.Color = Colors.LightGray;
    }

    if (para.Lines?.After > 0)
    {
      var pafter = sect.AddParagraph();
      AppendLinesAfter(pafter, para);
      AppendSpacingAfter(pafter, para);
    }


    foreach (var c in colsCount)
    {
      foreach (var r in rowsCount)
      {
        var cell = table.Rows[r - 1].Cells[c - 1];
        var imgs = cell.Elements.OfType<Paragraph>().SelectMany(s => s.Elements.OfType<MigraDocCore.DocumentObjectModel.Shapes.Image>());
        foreach (var im in imgs)
        {
          im.LockAspectRatio = true;
          im.Width = cell.Column.Width;
        }
      }
    }

    return sect;
  }


  public Paragraph ConvertCellPara(MigraDocCore.DocumentObjectModel.Tables.Cell cell, IPara para)
  {
    if (true || para.Texts.Count > 0)
    {
      var p = cell.AddParagraph();
      p.Format.Alignment = ConvertAlignment(para.Align);
      p.Style = _styleMapper(para.ParaType).Name;

      AppendLinesBefore(p, para);
      AppendFragments(p, para);
      AppendSpacingBefore(p, para);
      AppendSpacingAfter(p, para);

      foreach (var pp in para.Paragraphs.Where(w => w.IsActive))
        ConvertCellPara(cell, pp);

      AppendLinesAfter(p, para);

      return p;
    }

    return null;
  }



  public Paragraph AppendFragments(Paragraph p, IPara para)
  {
    if (para.ParaType.HasFlag(NUM) && para.Numbering != null)
    {
      p.AddFormattedText(para.Numbering.Trim());
      p.AddTab();
    }

    if (para.ParaType.HasFlag(LIST))
    {
      p.AddFormattedText("•");
      p.AddTab();
    }
    var count = 0;

    foreach (var f in para.Texts)
    {
      var ftext = p.AddFormattedText(count == 0 ? f.ToString().TrimStart('\r', '\n', '\t', ' ') : f.ToString().TrimStart('\r', '\n', '\t'));
      ftext.Bold = f.Format.HasFlag(Common.Enums.TextFormat.Bold);
      ftext.Italic = f.Format.HasFlag(Common.Enums.TextFormat.Italic);
      ftext.Underline = f.Format.HasFlag(Common.Enums.TextFormat.Underline) ? Underline.Words : Underline.None;

      if (f is IText exprText)
      {
        if (exprText.IsComputed)
        {
          ftext.Color = new(68, 130, 236);
        }
        ;

        if (exprText.IsFromParam)
        {
          ftext.Color = new(48, 180, 196);
        }
        ;

        //ftext.Color = new(0, 0, 0);
      }

      //else if (f is ISwitchText switchText)
      //{
      //  ftext.Underline = Underline.Dash;
      //  ftext.Color = new(18, 160, 36);
      //}

      count++;
    }

    return p;
  }


  public Paragraph AppendLinesBefore(Paragraph p, IPara para)
  {
    if (para.Lines != null)
    {
      foreach (var i in Enumerable.Range(0, para.Lines.Before))
        p.AddLineBreak();
    }

    return p;
  }


  public Paragraph AppendLinesAfter(Paragraph p, IPara para)
  {
    if (para.Lines != null)
    {
      foreach (var i in Enumerable.Range(0, para.Lines.After))
        p.AddLineBreak();
    }

    return p;
  }


  public Paragraph AppendSpacingBefore(Paragraph p, IPara para)
  {
    if (para.Spacing?.Before > 0)
    {
      p.Format.SpaceBefore = new(para.Spacing.Before, UnitType.Point);
    }

    return p;
  }


  public Paragraph AppendSpacingAfter(Paragraph p, IPara para)
  {
    if (para.Spacing?.After > 0)
    {
      p.Format.SpaceAfter = new(para.Spacing.After, UnitType.Point);
    }

    return p;
  }



  public ParagraphAlignment ConvertAlignment(AlignEnum align) => align switch
  {
    AlignEnum a when a.HasFlag(LEFT) => ParagraphAlignment.Left,
    AlignEnum a when a.HasFlag(BOTH) => ParagraphAlignment.Justify,
    AlignEnum a when a.HasFlag(CENTER) => ParagraphAlignment.Center,
    AlignEnum a when a.HasFlag(RIGHT) => ParagraphAlignment.Right,

    _ => ParagraphAlignment.Justify
  };


  public VerticalAlignment ConvertVerticalAlignment(AlignEnum align) => align switch
  {
    AlignEnum a when a.HasFlag(BOTTOM) => VerticalAlignment.Bottom,
    AlignEnum a when a.HasFlag(MID) => VerticalAlignment.Center,
    AlignEnum a when a.HasFlag(TOP) => VerticalAlignment.Top,

    _ => VerticalAlignment.Top
  };

}
