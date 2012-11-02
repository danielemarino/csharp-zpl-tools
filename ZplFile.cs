
using System;
using System.Collections.Generic;
using System.Linq;

namespace ZPL.Tools
{
  public static class ZplLanguage
  {
    public static Dictionary<string, string> Commands = new Dictionary<string, string>();

    static ZplLanguage()
    {
      //ZPL II Basic
      Commands.Add("^XA", "");
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

    public static long Count
    {
      get
      {
        return Commands.Count;
      }
    }

  }

  public class ZplFile
  {
    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public int Length 
    {
      get { return Text.Length; }
    }

    /// <summary>
    /// 
    /// </summary>
    public string Text { get; private set; }

    #endregion  

    #region Constructors

    /// <summary>
    /// 
    /// </summary>
    /// <param name="myText"></param>
    public ZplFile(string myText)
    {
      Text = myText;
    }

    /// <summary>
    /// 
    /// </summary>
    public ZplFile()
    {
      Text = "";
    }

    #endregion

    #region Standard Methods

    public bool IsValidZpl()
    {
      // TODO
      return true;
    }

    public bool IsValidZpl(int commandIndex)
    {
      // TODO
      return true;
    }

    public string GetCommandTag(int commandIndex)
    {
      for (var i = 1; i <= 3; i++)
      {
        if (ZplLanguage.Commands.ContainsKey(Text.Substring(commandIndex, i))) return Text.Substring(commandIndex, i);
      }
      return "";
    }

    public string GetCommandParams(int commandIndex)
    {
      var commandTag = "";
      for (var i = 1; i <= 3; i++)
      {
        if (!ZplLanguage.Commands.ContainsKey(Text.Substring(commandIndex, i))) continue;
        commandTag= Text.Substring(commandIndex, i);
        break;
      }
      return CommandExtract(commandIndex).Replace(commandTag, "");
    }

    public bool IsValidCommandTag(int charIndex)
    {
      try
      {
        for (var i = 1; i <= 3; i++)
        {
          if (ZplLanguage.Commands.ContainsKey(Text.Substring(charIndex, i))) return true;
        }
        return false;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public int CommandCount(string commandTag)
    {
      return new System.Text.RegularExpressions.Regex(commandTag).Matches(Text).Count;
    }

    public int CommandCount(string[] commandTags)
    {
      return commandTags.Sum(t => new System.Text.RegularExpressions.Regex(t).Matches(Text).Count);
    }

    public int CommandPosition(int charIndex)
    {
      if (charIndex < 0 || charIndex >= Text.Length) return -1; // Wrong Request

      for (var i = charIndex; i >=0; i--)
      {
        if (IsValidCommandTag(i)) return i;
      }

      return -2; // No Command Found
    }

    public int NextCommandPosition(int charIndex)
    {
      return CommandIndexOf(CommandPosition(charIndex) + 1);
    }
    
    public int PrevCommandPosition (int charIndex)
    {
      return CommandPosition(CommandPosition(charIndex) - 1);
    }
    
    public int CommandIndexOf(int charIndex)
    {
      if (charIndex < 0 || charIndex >= Text.Length) return -1; // Wrong Request

      for (var i = charIndex; i < Length; i++)
      {
        if (IsValidCommandTag(i)) return i;
      }

      return -2; // No Command Found
    }

    public int CommandIndexOf(string commandTag, int charIndex)
    {
      return Text.IndexOf(commandTag, charIndex);
    }

    public int CommandIndexOf(string commandTag)
    {
      return Text.IndexOf(commandTag, 0);
    }

    public int CommandLastIndexOf()
    {
      for (var i = Length; i > 0; i--)
      {
        if (IsValidCommandTag(i)) return i;
      }

      return -2; // No Command Found
    }

    public int CommandLastIndexOf(string commandTag)
    {
      return Text.LastIndexOf(commandTag);
    }

    public string CommandExtract(int commandIndex)
    {
      if (commandIndex < 0 || commandIndex >= (Text.Length)) return "";
      if (!IsValidCommandTag(commandIndex)) return "";

      var nextCommandIndex = CommandIndexOf(commandIndex + 1);
      return nextCommandIndex > 0
               ? Text.Substring(commandIndex, nextCommandIndex - commandIndex)
               : Text.Substring(commandIndex);
    }

    public int CommandStrip(int commandIndex)
    {
      if (commandIndex < 0 || commandIndex >= Text.Length) return -1;
      if (!IsValidCommandTag(commandIndex)) return -2;

      var nextCommandIndex = CommandIndexOf(commandIndex + 1);

      if (nextCommandIndex > 0)
        Text = Text.Substring(0, commandIndex - 1) + "\n" + Text.Substring(nextCommandIndex);
      else
        Text = Text.Substring(0, commandIndex - 1);

      return 0;
    }

    public int CommandReplace(int commandIndex, string newCommand)
    {
      if (commandIndex < 0 || commandIndex >= Text.Length) return -1;
      if (!IsValidCommandTag(commandIndex)) return -2;

      var nextCommandIndex = CommandIndexOf(commandIndex + 1);

      if (nextCommandIndex > 0)
        Text = Text.Substring(0, commandIndex - 1) + newCommand + Text.Substring(nextCommandIndex);
      else
        Text = Text.Substring(0, commandIndex - 1) + newCommand;

      return 0;
    }

    public string CommandExtractAndStrip(int commandIndex)
    {
      var commandTag = GetCommandTag(commandIndex);
      if (commandIndex < 0 || commandIndex >= (Text.Length - commandTag.Length)) return "";
      var commandCode = CommandExtract(commandIndex);
      CommandStrip(commandIndex);

      return commandCode;
    }

    #endregion

  }
}



