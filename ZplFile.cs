
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZPL.Tools;

namespace ZPL.Tools
{
  public class ZplFile
  {
    #region Private

    private static readonly string[] CmdChar = new[] { "^", "~" };
    //private HashSet<string> _zplCommands = new HashSet<string>(); 

    #endregion

    #region Properties

    /// <summary>
    /// 
    /// </summary>
    public long Length 
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

    public long CommandCount(string commandTag)
    {
      return new System.Text.RegularExpressions.Regex(commandTag).Matches(Text).Count;
    }

    public long CommandCount(string[] commandTags)
    {
      long totalCount = 0;
      for (var i = 0; i < commandTags.Length; i++)
      {
        totalCount += new System.Text.RegularExpressions.Regex(commandTags[i]).Matches(Text).Count;
      }
      return totalCount;
    }

    public int CommandIndexOf(int charIndex)
    {
      if (charIndex < 0 || charIndex >= Text.Length) return -1;
      var myIndex = new int[CmdChar.Length];

      for (var i = 0; i < CmdChar.Length; i++)
      {
        if (Text.Substring(charIndex, 1) == CmdChar[i]) return charIndex;
        myIndex[i] = Text.Substring(0, charIndex - 1).LastIndexOf(CmdChar[i]);
      }

      if (CmdChar.Length == 1) return myIndex[0];

      var myMax = 0;
      for (var i = 1; i < CmdChar.Length; i++)
      {
        myMax = Math.Max(myIndex[i], myIndex[i - 1]);
      }
      return myMax;
    }

    public int CommandIndexOf(int charIndex, string commandTag)
    {
      if (charIndex < 0 || charIndex >= Text.Length) return -1;
      var myIndex = new int[CmdChar.Length];

      for (var i = 0; i < CmdChar.Length; i++)
      {
        if (Text.Substring(charIndex, 1) == CmdChar[i]) return charIndex;
        myIndex[i] = Text.Substring(0, charIndex - 1).LastIndexOf(CmdChar[i]);
      }

      if (CmdChar.Length == 1) return myIndex[0];

      var myMax = 0;
      for (var i = 1; i < CmdChar.Length; i++)
      {
        myMax = Math.Max(myIndex[i], myIndex[i - 1]);
      }
      return myMax;
    }

    public int CommandLastIndexOf(int charIndex)
    {
      if (charIndex <= 0 || charIndex >= Text.Length) return -1;
      var currentCommandIndex = CommandIndexOf(charIndex);
      return CommandIndexOf(currentCommandIndex - 1);
    }

    public int CommandLastIndexOf(int charIndex, string commandTag)
    {
      if (charIndex <= 0 || charIndex >= Text.Length) return -1;
      var currentCommandIndex = CommandIndexOf(charIndex);
      return CommandIndexOf(currentCommandIndex - 1);
    }

    public string CommandExtract(int commandIndex)
    {
      if (commandIndex < 0 || commandIndex >= (Text.Length - 1)) return "";
      if (!(ZplLanguage.Commands.ContainsKey(Text.Substring(commandIndex, 2)) || ZplLanguage.Commands.ContainsKey(Text.Substring(commandIndex, 3)))) return "";

      var nextCommandIndex = Text.IndexOf('^', commandIndex + 1);
      return nextCommandIndex > 0 ? Text.Substring(commandIndex, nextCommandIndex - commandIndex) : Text.Substring(commandIndex);
    }

    public int CommandStrip(int commandIndex)
    {
      var commandTag = GetCommandTag(commandIndex);
      if (commandIndex < 0 || commandIndex >= (Text.Length - commandTag.Length)) return 2;
      if (!ZplLanguage.Commands.ContainsKey(Text.Substring(commandIndex, 3))) return 1;

      var nextCommandIndex = Text.IndexOf('^', commandIndex + 1);

      if (nextCommandIndex > 0)
        Text = Text.Substring(0, commandIndex - 1) + "\n" + Text.Substring(nextCommandIndex);
      else
        Text = Text.Substring(0, commandIndex - 1);

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



