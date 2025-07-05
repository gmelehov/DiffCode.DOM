namespace DiffCode.DOM.Common.Enums;

/// <summary>
/// Типы абзацев.
/// </summary>
[Flags]
public enum ParaTypeEnum
{

  NONE = 0,

  TITLE = 1,

  NUM = 2,

  PLAIN = 4,

  LIST = 8,

  TABLE = 16,

  HEADER1 = 32,

  HEADER2 = 64,

  HEADER3 = 128,

  HEADER4 = 256,

  HEADER5 = 512,

  NORMAL = 1024,

  IND2 = 2048,

  IND3 = 4096,

  IND4 = 8192,

  BUL1 = 16384,

  BUL2 = 32768,

  BUL3 = 65536,

  NEWPAGE = 131072,

}
