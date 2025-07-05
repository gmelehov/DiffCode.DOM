using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiffCode.DOM.Common.Enums;


/// <summary>
/// Варианты форматирования текстового фрагмента.
/// </summary>
[Flags]
public enum TextFormat
{

  None = 0,

  Bold = 1,

  Italic = 2,

  Underline = 4,

  Striked = 8,

}
