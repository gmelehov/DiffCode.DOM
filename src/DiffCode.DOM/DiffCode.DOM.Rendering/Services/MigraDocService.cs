using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Core.Abstractions.Paragraphs;
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.Shapes;
using MigraDocCore.DocumentObjectModel.Tables;

namespace DiffCode.DOM.Rendering.Services;

public class MigraDocService
{
  private Dictionary<string, Style> _styles;
  private Dictionary<ParaTypeEnum, string> _styleMapper;

  private const string MainHeader = nameof(MainHeader);
  private const string NumberedHeader1 = nameof(NumberedHeader1);
  private const string NumberedHeader2 = nameof(NumberedHeader2);
  private const string NumberedHeader3 = nameof(NumberedHeader3);
  private const string NumberedHeader4 = nameof(NumberedHeader4);
  private const string Norm = nameof(Norm);
  private const string Level2 = nameof(Level2);
  private const string Level3 = nameof(Level3);
  private const string Level4 = nameof(Level4);
  private const string NumberedNormal1 = nameof(NumberedNormal1);
  private const string NumberedNormal2 = nameof(NumberedNormal2);
  private const string BulletedList1 = nameof(BulletedList1);
  private const string BulletedList2 = nameof(BulletedList2);
  private const string BulletedList3 = nameof(BulletedList3);
  private const string Placeholder = nameof(Placeholder);





  public Document ConvertDocument(IEnumerable<IDocum> documents)
  {
    var doc = new Document();
    var styles = GetStyles();

    doc.Styles.Add(styles[MainHeader]);
    doc.Styles.Add(styles[NumberedHeader1]);
    doc.Styles.Add(styles[NumberedHeader2]);
    doc.Styles.Add(styles[NumberedHeader3]);
    doc.Styles.Add(styles[NumberedHeader4]);
    doc.Styles.Add(styles[Norm]);
    doc.Styles.Add(styles[Level2]);
    doc.Styles.Add(styles[Level3]);
    doc.Styles.Add(styles[NumberedNormal1]);
    doc.Styles.Add(styles[NumberedNormal2]);
    doc.Styles.Add(styles[BulletedList1]);
    doc.Styles.Add(styles[BulletedList2]);
    doc.Styles.Add(styles[BulletedList3]);

    foreach (var d in documents)
    {
      var pageSetup = doc.DefaultPageSetup.Clone();
      pageSetup.TopMargin = new(10, UnitType.Millimeter);
      pageSetup.BottomMargin = new(10, UnitType.Millimeter);
      pageSetup.LeftMargin = new(10, UnitType.Millimeter);
      pageSetup.RightMargin = new(10, UnitType.Millimeter);

      Section section = doc.AddSection();
      section.PageSetup = pageSetup;
      section.AddPageBreak();
      ConvertParagraph(section, d.Content);
    }

    return doc;
  }







  public Section ConvertParagraph(Section sect, IPara para)
  {
    if (para.IsActive)
    {
      return para.ParaType switch
      {
        ParaTypeEnum p when p.HasFlag(PLAIN) || p.HasFlag(TITLE) || p.HasFlag(NUM) || p.HasFlag(LIST) => ConvertListOrNumberedOrPlainOrTitlePara(sect, para),
        ParaTypeEnum p when p.HasFlag(TABLE) => ConvertTablePara(sect, para),

        _ => ConvertListOrNumberedOrPlainOrTitlePara(sect, para)
      };
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

    ret.Style = GetStyleMapper()[para.ParaType];

    AppendLinesBefore(ret, para);
    ret.Format.Alignment = ConvertAlignment(para.Align);
    AppendFragments(ret, para);

    AppendSpacingBefore(ret, para);
    AppendSpacingAfter(ret, para);

    foreach (var pp in para.Paragraphs.Where(w => w.IsActive))
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

    if (lastParagraph.Style == NumberedNormal1)
    {
      var tf = sect.AddTextFrame();
      tf.Left = ShapePosition.Left;
      tf.RelativeHorizontal = RelativeHorizontal.Page;
      tf.MarginLeft = new(10, UnitType.Millimeter);

      table = tf.AddTable();
    }
    else
    {
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
            pcell.Style = GetStyleMapper()[pp.ParaType];
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
      p.Style = GetStyleMapper()[para.ParaType];

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
          //exprText.ApplyTestCases();
          if (false)
          {
            //ftext.Underline = Underline.DotDash;
            ftext.Color = new(198, 10, 10);
          }
          else
          {
            //ftext.Underline = Underline.Dash;
            ftext.Color = new(68, 130, 236);
          }
        }
        ;
        if (true)
        {
          if (false)
          {
            //ftext.Underline = Underline.DotDash;
            ftext.Color = new(178, 5, 5);
          }
          else
          {
            //ftext.Underline = Underline.Dash;
            ftext.Color = new(48, 180, 196);
          }
        }
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














  public Dictionary<string, Style> GetStyles() => _styles ??= new Dictionary<string, Style>
  {
    [MainHeader] = MainHeaderStyle(),
    [NumberedHeader1] = NumberedHeader1Style(),
    [NumberedHeader2] = NumberedHeader2Style(),
    [NumberedHeader3] = NumberedHeader3Style(),
    [NumberedHeader4] = NumberedHeader4Style(),
    [Norm] = NormalStyle(),
    [Level2] = Level2Style(),
    [Level3] = Level3Style(),
    [NumberedNormal1] = NumberedNormal1Style(),
    [NumberedNormal2] = NumberedNormal2Style(),
    [BulletedList1] = BulletedList1Style(),
    [BulletedList2] = BulletedList2Style(),
    [BulletedList3] = BulletedList3Style(),
    [Placeholder] = PlaceholderStyle()
  };



  public Dictionary<ParaTypeEnum, string> GetStyleMapper() => _styleMapper ??= new Dictionary<ParaTypeEnum, string>
  {
    [PLAIN | NORMAL] = Norm,
    [TITLE | HEADER1] = MainHeader,
    [NUM | HEADER1] = NumberedHeader1,
    [NUM | HEADER2] = NumberedHeader2,
    [NUM | HEADER3] = NumberedHeader3,
    [NUM | HEADER4] = NumberedHeader4,
    [PLAIN | IND2] = Level2,
    [PLAIN | IND3] = Level3,
    [PLAIN | IND4] = Level4,
    [PLAIN | NORMAL | NEWPAGE] = Norm,
    [NUM | NORMAL | HEADER1] = NumberedNormal1,
    [LIST | BUL1] = BulletedList1,
    [LIST | BUL2] = BulletedList2,
    [LIST | BUL3] = BulletedList3,
  };



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







  public static Style MainHeaderStyle()
  {
    var style = new Style(MainHeader, "Normal");

    style.Font.Name = "Roboto Mono";
    style.Font.Size = new(13, UnitType.Point);
    style.Font.Color = new(68, 114, 166);
    style.ParagraphFormat.Alignment = ParagraphAlignment.Center;
    style.ParagraphFormat.KeepWithNext = true;
    style.ParagraphFormat.SpaceAfter = new(0, UnitType.Point);
    style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;

    return style;
  }


  public static Style PlaceholderStyle()
  {
    var style = new Style(Placeholder, "Normal");

    style.Font.Name = "Roboto Mono";
    style.Font.Size = new(8, UnitType.Point);
    style.Font.Color = new(98, 114, 196);

    return style;
  }


  public static Style NumberedHeader1Style()
  {
    var style = new Style(NumberedHeader1, "Normal");

    style.Font.Name = "Roboto Mono";
    style.Font.Size = new(11, UnitType.Point);
    style.Font.Color = new(68, 114, 166);
    style.ParagraphFormat.Alignment = ParagraphAlignment.Left;
    style.ParagraphFormat.KeepWithNext = true;
    style.ParagraphFormat.SpaceBefore = new(12, UnitType.Point);
    style.ParagraphFormat.SpaceAfter = new(6, UnitType.Point);
    style.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
    style.ParagraphFormat.OutlineLevel = OutlineLevel.Level2;

    return style;
  }


  public static Style NumberedNormal1Style()
  {
    var style = new Style(NumberedNormal1, "Normal");

    style.Font.Name = "Roboto Mono";

    style.Font.Size = new(8, UnitType.Point);
    style.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
    style.ParagraphFormat.LineSpacing = new(11, UnitType.Point);

    style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
    style.ParagraphFormat.KeepWithNext = true;
    style.ParagraphFormat.SpaceBefore = new(6, UnitType.Point);
    style.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
    style.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
    style.ParagraphFormat.FirstLineIndent = new(-10, UnitType.Millimeter);
    style.ParagraphFormat.LeftIndent = new(10, UnitType.Millimeter);
    style.ParagraphFormat.OutlineLevel = OutlineLevel.Level2;

    return style;
  }


  public static Style NumberedNormal2Style()
  {
    var style = new Style(NumberedNormal2, "Normal");

    style.Font.Name = "Roboto Mono";

    style.Font.Size = new(8, UnitType.Point);
    style.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
    style.ParagraphFormat.LineSpacing = new(11, UnitType.Point);

    style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
    style.ParagraphFormat.KeepWithNext = true;
    style.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
    style.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
    style.ParagraphFormat.FirstLineIndent = new(-10, UnitType.Millimeter);
    style.ParagraphFormat.LeftIndent = new(20, UnitType.Millimeter);
    style.ParagraphFormat.OutlineLevel = OutlineLevel.Level3;
    return style;
  }


  public static Style NumberedHeader2Style()
  {
    var style = new Style(NumberedHeader2, "Normal");

    style.Font.Name = "Roboto Mono";

    style.Font.Size = new(8, UnitType.Point);
    style.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
    style.ParagraphFormat.LineSpacing = new(11, UnitType.Point);

    style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
    style.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
    style.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
    style.ParagraphFormat.FirstLineIndent = new(-10, UnitType.Millimeter);
    style.ParagraphFormat.LeftIndent = new(10, UnitType.Millimeter);
    style.ParagraphFormat.OutlineLevel = OutlineLevel.Level3;
    return style;
  }


  public static Style NumberedHeader3Style()
  {
    var style = new Style(NumberedHeader3, "Normal");

    style.Font.Name = "Roboto Mono";

    style.Font.Size = new(8, UnitType.Point);
    style.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
    style.ParagraphFormat.LineSpacing = new(11, UnitType.Point);

    style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
    style.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
    style.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
    style.ParagraphFormat.FirstLineIndent = new(-15, UnitType.Millimeter);
    style.ParagraphFormat.LeftIndent = new(25, UnitType.Millimeter);
    style.ParagraphFormat.OutlineLevel = OutlineLevel.Level4;
    return style;
  }


  public static Style NumberedHeader4Style()
  {
    var style = new Style(NumberedHeader4, "Normal");

    style.Font.Name = "Roboto Mono";

    style.Font.Size = new(8, UnitType.Point);
    style.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
    style.ParagraphFormat.LineSpacing = new(11, UnitType.Point);

    style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
    style.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
    style.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
    style.ParagraphFormat.FirstLineIndent = new(-10, UnitType.Millimeter);
    style.ParagraphFormat.LeftIndent = new(30, UnitType.Millimeter);
    style.ParagraphFormat.OutlineLevel = OutlineLevel.Level5;

    return style;
  }


  public static Style NormalStyle()
  {
    var style = new Style(Norm, "Normal");

    style.Font.Name = "Roboto Mono";

    style.Font.Size = new(8, UnitType.Point);
    style.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
    style.ParagraphFormat.LineSpacing = new(11, UnitType.Point);

    style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
    style.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
    style.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;
    return style;
  }


  public static Style BulletedList1Style()
  {
    var style = new Style(BulletedList1, "Normal");

    style.Font.Name = "Roboto Mono";

    style.Font.Size = new(8, UnitType.Point);
    style.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
    style.ParagraphFormat.LineSpacing = new(11, UnitType.Point);

    style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
    style.ParagraphFormat.SpaceAfter = new(2, UnitType.Point);
    style.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
    style.ParagraphFormat.FirstLineIndent = new(-4, UnitType.Millimeter);
    style.ParagraphFormat.LeftIndent = new(4, UnitType.Millimeter);
    style.ParagraphFormat.OutlineLevel = OutlineLevel.Level3;
    return style;
  }


  public static Style BulletedList2Style()
  {
    var style = new Style(BulletedList2, "Normal");

    style.Font.Name = "Roboto Mono";

    style.Font.Size = new(8, UnitType.Point);
    style.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
    style.ParagraphFormat.LineSpacing = new(11, UnitType.Point);

    style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
    style.ParagraphFormat.SpaceAfter = new(2, UnitType.Point);
    style.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
    style.ParagraphFormat.FirstLineIndent = new(-4, UnitType.Millimeter);
    style.ParagraphFormat.LeftIndent = new(14, UnitType.Millimeter);
    style.ParagraphFormat.OutlineLevel = OutlineLevel.Level4;
    return style;
  }


  public static Style BulletedList3Style()
  {
    var style = new Style(BulletedList3, "Normal");

    style.Font.Name = "Roboto Mono";

    style.Font.Size = new(8, UnitType.Point);
    style.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
    style.ParagraphFormat.LineSpacing = new(11, UnitType.Point);

    style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
    style.ParagraphFormat.SpaceAfter = new(2, UnitType.Point);
    style.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
    style.ParagraphFormat.FirstLineIndent = new(-4, UnitType.Millimeter);
    style.ParagraphFormat.LeftIndent = new(24, UnitType.Millimeter);
    style.ParagraphFormat.OutlineLevel = OutlineLevel.Level5;
    return style;
  }


  public static Style Level2Style()
  {
    var style = new Style(Level2, "Normal");

    style.Font.Name = "Roboto Mono";

    style.Font.Size = new(8, UnitType.Point);
    style.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
    style.ParagraphFormat.LineSpacing = new(11, UnitType.Point);

    style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
    style.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
    style.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
    style.ParagraphFormat.LeftIndent = new(10, UnitType.Millimeter);
    style.ParagraphFormat.OutlineLevel = OutlineLevel.Level3;
    return style;
  }


  public static Style Level3Style()
  {
    var style = new Style(Level3, "Normal");

    style.Font.Name = "Roboto Mono";

    style.Font.Size = new(8, UnitType.Point);
    style.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
    style.ParagraphFormat.LineSpacing = new(11, UnitType.Point);

    style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
    style.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
    style.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
    style.ParagraphFormat.LeftIndent = new(20, UnitType.Millimeter);
    style.ParagraphFormat.OutlineLevel = OutlineLevel.Level4;

    return style;
  }

}
