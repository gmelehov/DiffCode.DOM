using DiffCode.DOM.Common.Enums;
using DiffCode.DOM.Rendering.Models;
using DiffCode.DOM.Rendering.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MigraDocCore.DocumentObjectModel;

namespace DiffCode.DOM.Rendering.Extensions;


public static class IServiceExtensions
{



  private static IServiceCollection AddMigraDocStyles(this IServiceCollection scoll) =>
    scoll
    .Configure<MigraDocStyles>(p =>
    {
      p.StandardFontName = "Roboto Mono";


      p.MainHeader = new(nameof(MigraDocStyles.MainHeader), "Normal");
      p.MainHeader.Font.Name = p.StandardFontName;
      p.MainHeader.Font.Size = new(15, UnitType.Point);
      p.MainHeader.Font.Color = new(68, 114, 166);
      p.MainHeader.ParagraphFormat.Alignment = ParagraphAlignment.Center;
      p.MainHeader.ParagraphFormat.KeepWithNext = true;
      p.MainHeader.ParagraphFormat.SpaceAfter = new(10, UnitType.Point);
      p.MainHeader.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;


      p.NumberedHeader1 = new(nameof(MigraDocStyles.NumberedHeader1), "Normal");
      p.NumberedHeader1.Font.Name = p.StandardFontName;
      p.NumberedHeader1.Font.Size = new(11, UnitType.Point);
      p.NumberedHeader1.Font.Color = new(68, 114, 166);
      p.NumberedHeader1.ParagraphFormat.Alignment = ParagraphAlignment.Left;
      p.NumberedHeader1.ParagraphFormat.KeepWithNext = true;
      p.NumberedHeader1.ParagraphFormat.SpaceBefore = new(12, UnitType.Point);
      p.NumberedHeader1.ParagraphFormat.SpaceAfter = new(6, UnitType.Point);
      p.NumberedHeader1.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
      p.NumberedHeader1.ParagraphFormat.OutlineLevel = OutlineLevel.Level2;


      p.Header1 = new(nameof(MigraDocStyles.Header1), "Normal");
      p.Header1.Font.Name = p.StandardFontName;
      p.Header1.Font.Size = new(13.5, UnitType.Point);
      p.Header1.Font.Color = new(68, 114, 166);
      p.Header1.ParagraphFormat.Alignment = ParagraphAlignment.Left;
      p.Header1.ParagraphFormat.KeepWithNext = true;
      p.Header1.ParagraphFormat.SpaceBefore = new(12, UnitType.Point);
      p.Header1.ParagraphFormat.SpaceAfter = new(6, UnitType.Point);
      p.Header1.ParagraphFormat.OutlineLevel = OutlineLevel.Level2;


      p.Header2 = new(nameof(MigraDocStyles.Header2), "Normal");
      p.Header2.Font.Name = p.StandardFontName;
      p.Header2.Font.Size = new(12.5, UnitType.Point);
      p.Header2.Font.Color = new(68, 114, 166);
      p.Header2.ParagraphFormat.Alignment = ParagraphAlignment.Left;
      p.Header2.ParagraphFormat.KeepWithNext = true;
      p.Header2.ParagraphFormat.SpaceBefore = new(12, UnitType.Point);
      p.Header2.ParagraphFormat.SpaceAfter = new(6, UnitType.Point);
      p.Header2.ParagraphFormat.OutlineLevel = OutlineLevel.Level3;


      p.Header3 = new(nameof(MigraDocStyles.Header3), "Normal");
      p.Header3.Font.Name = p.StandardFontName;
      p.Header3.Font.Size = new(11.5, UnitType.Point);
      p.Header3.Font.Color = new(68, 114, 166);
      p.Header3.ParagraphFormat.Alignment = ParagraphAlignment.Left;
      p.Header3.ParagraphFormat.KeepWithNext = true;
      p.Header3.ParagraphFormat.SpaceBefore = new(12, UnitType.Point);
      p.Header3.ParagraphFormat.SpaceAfter = new(6, UnitType.Point);
      p.Header3.ParagraphFormat.OutlineLevel = OutlineLevel.Level4;


      p.NumberedHeader2 = new(nameof(MigraDocStyles.NumberedHeader2), "Normal");
      p.NumberedHeader2.Font.Name = p.StandardFontName;
      p.NumberedHeader2.Font.Size = new(8, UnitType.Point);
      p.NumberedHeader2.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
      p.NumberedHeader2.ParagraphFormat.LineSpacing = new(11, UnitType.Point);
      p.NumberedHeader2.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
      p.NumberedHeader2.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
      p.NumberedHeader2.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
      p.NumberedHeader2.ParagraphFormat.FirstLineIndent = new(-10, UnitType.Millimeter);
      p.NumberedHeader2.ParagraphFormat.LeftIndent = new(10, UnitType.Millimeter);
      p.NumberedHeader2.ParagraphFormat.OutlineLevel = OutlineLevel.Level3;


      p.NumberedHeader3 = new(nameof(MigraDocStyles.NumberedHeader3), "Normal");
      p.NumberedHeader3.Font.Name = p.StandardFontName;
      p.NumberedHeader3.Font.Size = new(8, UnitType.Point);
      p.NumberedHeader3.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
      p.NumberedHeader3.ParagraphFormat.LineSpacing = new(11, UnitType.Point);
      p.NumberedHeader3.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
      p.NumberedHeader3.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
      p.NumberedHeader3.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
      p.NumberedHeader3.ParagraphFormat.FirstLineIndent = new(-15, UnitType.Millimeter);
      p.NumberedHeader3.ParagraphFormat.LeftIndent = new(25, UnitType.Millimeter);
      p.NumberedHeader3.ParagraphFormat.OutlineLevel = OutlineLevel.Level4;


      p.NumberedHeader4 = new(nameof(MigraDocStyles.NumberedHeader4), "Normal");
      p.NumberedHeader4.Font.Name = p.StandardFontName;
      p.NumberedHeader4.Font.Size = new(8, UnitType.Point);
      p.NumberedHeader4.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
      p.NumberedHeader4.ParagraphFormat.LineSpacing = new(11, UnitType.Point);
      p.NumberedHeader4.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
      p.NumberedHeader4.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
      p.NumberedHeader4.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
      p.NumberedHeader4.ParagraphFormat.FirstLineIndent = new(-10, UnitType.Millimeter);
      p.NumberedHeader4.ParagraphFormat.LeftIndent = new(30, UnitType.Millimeter);
      p.NumberedHeader4.ParagraphFormat.OutlineLevel = OutlineLevel.Level5;


      p.NumberedNormal1 = new(nameof(MigraDocStyles.NumberedNormal1), "Normal");
      p.NumberedNormal1.Font.Name = p.StandardFontName;
      p.NumberedNormal1.Font.Size = new(8, UnitType.Point);
      p.NumberedNormal1.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
      p.NumberedNormal1.ParagraphFormat.LineSpacing = new(11, UnitType.Point);
      p.NumberedNormal1.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
      p.NumberedNormal1.ParagraphFormat.KeepWithNext = true;
      p.NumberedNormal1.ParagraphFormat.SpaceBefore = new(6, UnitType.Point);
      p.NumberedNormal1.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
      p.NumberedNormal1.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
      p.NumberedNormal1.ParagraphFormat.FirstLineIndent = new(-10, UnitType.Millimeter);
      p.NumberedNormal1.ParagraphFormat.LeftIndent = new(10, UnitType.Millimeter);
      p.NumberedNormal1.ParagraphFormat.OutlineLevel = OutlineLevel.Level2;


      p.NumberedNormal2 = new(nameof(MigraDocStyles.NumberedNormal2), "Normal");
      p.NumberedNormal2.Font.Name = p.StandardFontName;
      p.NumberedNormal2.Font.Size = new(8, UnitType.Point);
      p.NumberedNormal2.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
      p.NumberedNormal2.ParagraphFormat.LineSpacing = new(11, UnitType.Point);
      p.NumberedNormal2.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
      p.NumberedNormal2.ParagraphFormat.KeepWithNext = true;
      p.NumberedNormal2.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
      p.NumberedNormal2.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
      p.NumberedNormal2.ParagraphFormat.FirstLineIndent = new(-10, UnitType.Millimeter);
      p.NumberedNormal2.ParagraphFormat.LeftIndent = new(20, UnitType.Millimeter);
      p.NumberedNormal2.ParagraphFormat.OutlineLevel = OutlineLevel.Level3;


      p.Norm = new(nameof(MigraDocStyles.Norm), "Normal");
      p.Norm.Font.Name = p.StandardFontName;
      p.Norm.Font.Size = new(8, UnitType.Point);
      p.Norm.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
      p.Norm.ParagraphFormat.LineSpacing = new(11, UnitType.Point);
      p.Norm.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
      p.Norm.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
      p.Norm.ParagraphFormat.OutlineLevel = OutlineLevel.BodyText;


      p.BulletedList1 = new(nameof(MigraDocStyles.BulletedList1), "Normal");
      p.BulletedList1.Font.Name = p.StandardFontName;
      p.BulletedList1.Font.Size = new(8, UnitType.Point);
      p.BulletedList1.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
      p.BulletedList1.ParagraphFormat.LineSpacing = new(11, UnitType.Point);
      p.BulletedList1.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
      p.BulletedList1.ParagraphFormat.SpaceAfter = new(2, UnitType.Point);
      p.BulletedList1.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
      p.BulletedList1.ParagraphFormat.FirstLineIndent = new(-4, UnitType.Millimeter);
      p.BulletedList1.ParagraphFormat.LeftIndent = new(4, UnitType.Millimeter);
      p.BulletedList1.ParagraphFormat.OutlineLevel = OutlineLevel.Level3;


      p.BulletedList2 = new(nameof(MigraDocStyles.BulletedList2), "Normal");
      p.BulletedList2.Font.Name = p.StandardFontName;
      p.BulletedList2.Font.Size = new(8, UnitType.Point);
      p.BulletedList2.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
      p.BulletedList2.ParagraphFormat.LineSpacing = new(11, UnitType.Point);
      p.BulletedList2.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
      p.BulletedList2.ParagraphFormat.SpaceAfter = new(2, UnitType.Point);
      p.BulletedList2.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
      p.BulletedList2.ParagraphFormat.FirstLineIndent = new(-4, UnitType.Millimeter);
      p.BulletedList2.ParagraphFormat.LeftIndent = new(14, UnitType.Millimeter);
      p.BulletedList2.ParagraphFormat.OutlineLevel = OutlineLevel.Level4;


      p.BulletedList3 = new(nameof(MigraDocStyles.BulletedList3), "Normal");
      p.BulletedList3.Font.Name = p.StandardFontName;
      p.BulletedList3.Font.Size = new(8, UnitType.Point);
      p.BulletedList3.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
      p.BulletedList3.ParagraphFormat.LineSpacing = new(11, UnitType.Point);
      p.BulletedList3.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
      p.BulletedList3.ParagraphFormat.SpaceAfter = new(2, UnitType.Point);
      p.BulletedList3.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
      p.BulletedList3.ParagraphFormat.FirstLineIndent = new(-4, UnitType.Millimeter);
      p.BulletedList3.ParagraphFormat.LeftIndent = new(24, UnitType.Millimeter);
      p.BulletedList3.ParagraphFormat.OutlineLevel = OutlineLevel.Level5;


      p.Level2 = new(nameof(MigraDocStyles.Level2), "Normal");
      p.Level2.Font.Name = p.StandardFontName;
      p.Level2.Font.Size = new(8, UnitType.Point);
      p.Level2.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
      p.Level2.ParagraphFormat.LineSpacing = new(11, UnitType.Point);
      p.Level2.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
      p.Level2.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
      p.Level2.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
      p.Level2.ParagraphFormat.LeftIndent = new(10, UnitType.Millimeter);
      p.Level2.ParagraphFormat.OutlineLevel = OutlineLevel.Level3;


      p.Level3 = new(nameof(MigraDocStyles.Level3), "Normal");
      p.Level3.Font.Name = p.StandardFontName;
      p.Level3.Font.Size = new(8, UnitType.Point);
      p.Level3.ParagraphFormat.LineSpacingRule = LineSpacingRule.Exactly;
      p.Level3.ParagraphFormat.LineSpacing = new(11, UnitType.Point);
      p.Level3.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
      p.Level3.ParagraphFormat.SpaceAfter = new(4, UnitType.Point);
      p.Level3.ParagraphFormat.AddTabStop(new(10, UnitType.Millimeter), TabAlignment.Left, TabLeader.Spaces);
      p.Level3.ParagraphFormat.LeftIndent = new(20, UnitType.Millimeter);
      p.Level3.ParagraphFormat.OutlineLevel = OutlineLevel.Level4;


    })
    .AddScoped(cfg => cfg.GetService<IOptions<MigraDocStyles>>().Value)
    .AddScoped<Func<ParaTypeEnum, Style>>(sp => str =>
    {
      var p = sp.GetService<MigraDocStyles>();
      return str switch
      {
        LIST | BUL1 => p.BulletedList1,
        LIST | BUL2 => p.BulletedList2,
        LIST | BUL3 => p.BulletedList3,
        PLAIN | NORMAL or TABLE | NORMAL => p.Norm,
        TITLE | HEADER1 => p.MainHeader,
        PLAIN | IND2 => p.Level2,
        PLAIN | IND3 => p.Level3,
        NUM | HEADER1 | NORMAL => p.NumberedNormal1,
        NUM | HEADER2 | NORMAL => p.NumberedNormal2,
        NUM | HEADER1 => p.NumberedHeader1,
        NUM | HEADER2 => p.NumberedHeader2,
        NUM | HEADER3 => p.NumberedHeader3,
        NUM | HEADER4 => p.NumberedHeader4,
        PLAIN | HEADER1 => p.Header1,
        PLAIN | HEADER2 => p.Header2,
        PLAIN | HEADER3 => p.Header3,

        _ => throw new NotImplementedException(),
      };
    })
    ;



  public static IServiceCollection AddMigraDocRendering(this IServiceCollection scoll) =>
    scoll
    .AddMigraDocStyles()
    .AddScoped<Func<Document>>(sp => () =>
    {
      var ret = new Document();
      var styles = sp.GetService<MigraDocStyles>();

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

      return ret;
    })
    .AddScoped<MigraDocRenderingService>()
    ;




}
