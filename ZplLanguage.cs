
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZPL.Tools
{
  public class ZplLanguage
  {
    public static Dictionary<string, string> Commands;

    public ZplLanguage()
    {
      //ZPL II Basic
      Commands.Add("^XA","");
      Commands.Add("^XZ", "");
      Commands.Add("^LH", "");
      Commands.Add("^LL", "");
      Commands.Add("^ML", "");
      Commands.Add("^LS", "");
      Commands.Add("^P", "");
      Commands.Add("^FW", "");
      Commands.Add("^PO", "");
      Commands.Add("^FO", "");
      Commands.Add("^FT", "");
      Commands.Add("^FP", "");
      Commands.Add("^FD", "");
      Commands.Add("^FS", "");
      Commands.Add("^FH", "");
      // Printer Configuration
      Commands.Add("^MM", "");
      Commands.Add("^MN", "");
      Commands.Add("^MT", "");
      Commands.Add("^MU", "");
      Commands.Add("^MD", "");
      Commands.Add("~SD", "");
      // Text for Labels
      Commands.Add("^CI", "");
      Commands.Add("^CF", "");
      Commands.Add("^A", "");
      Commands.Add("^A0", "");
      Commands.Add("^A@", "");
      Commands.Add("~DB", "");
      Commands.Add("~DU", "");
      Commands.Add("^CW", "");
      // Bar Codes
      Commands.Add("^B2", "");
      Commands.Add("^BK", "");
      Commands.Add("^BP", "");
      Commands.Add("^BZ", "");
      Commands.Add("^B8", "");
      Commands.Add("^B9", "");
      Commands.Add("^BE", "");
      Commands.Add("^BS", "");
      Commands.Add("^BU", "");
      Commands.Add("^B3", "");
      Commands.Add("^BA", "");
      Commands.Add("^BC", "");
      // Two-Dimensional Bar Code
      Commands.Add("^B7", "");
      Commands.Add("^BD", "");
      Commands.Add("^BX", "");
      // Bar Codes others
      Commands.Add("^BY", "");
      // Graphic Instruction
      Commands.Add("^GB", "");
      Commands.Add("^GC", "");
      Commands.Add("^GD", "");
      Commands.Add("^DD", "");
      Commands.Add("~DG", "");
      Commands.Add("^GF", "");
      Commands.Add("~DY", "");
      Commands.Add("^XG", "");
      Commands.Add("^IM", "");
      Commands.Add("^IS", "");
      Commands.Add("^IL", "");
      Commands.Add("^ID", "");
      Commands.Add("^EG", "");
      Commands.Add("~EG", "");
      // Advanced Techniques
      Commands.Add("^FX", "");
      Commands.Add("^FR", "");
      Commands.Add("^LR", "");
      Commands.Add("^PM", "");
      Commands.Add("^SN", "");
      Commands.Add("~TA", "");
      Commands.Add("^EF", "");
      Commands.Add("~EF", "");
      Commands.Add("^DF", "");
      Commands.Add("^XF", "");
      Commands.Add("^ST", "");
      Commands.Add("~JR", "");
      Commands.Add("~JC", "");
      Commands.Add("~JA", "");
      Commands.Add("~JP", "");
      Commands.Add("~JX", "");
      Commands.Add("~PH", "");
      Commands.Add("^PH", "");
      Commands.Add("~PP", "");
      Commands.Add("^PP", "");
      Commands.Add("~PS", "");
      Commands.Add("^PQ", "");
      Commands.Add("^PR", "");
      Commands.Add("~PR", "");
      Commands.Add("^JM", "");
      Commands.Add("^CC", "");
      Commands.Add("~CC", "");
      Commands.Add("^CD", "");
      Commands.Add("~CD", "");
      Commands.Add("^CT", "");
      Commands.Add("~CT", "");
      Commands.Add("^CM", "");
      Commands.Add("~JD", "");
      Commands.Add("~WC", "");
    }

    public long Count
    {
      get
      {
        return Commands.Count;
      }
    }

  }
}