
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZPL.Tools
{
  public class ZplFile
  {
    #region Private

    private const string CmdDownloadGraphicImage = "~DG";
    private static readonly string[] CmdChar = new[] { "^", "~" };

    #endregion

    #region Properties

    public long Length 
    {
      get { return Text.Length; }
    }

    public string Text { get; private set; }

    #endregion  

    #region Constructors

    public ZplFile(string myText)
    {
      Text = myText;
    }

    public ZplFile()
    {
      Text = "";
    }

    #endregion

    #region Methods

    public int ImageStrip(int imageIndex)
    {
      if (imageIndex < 0 || imageIndex >= (Text.Length-CmdDownloadGraphicImage.Length)) return 2;
      if (Text.Substring(imageIndex, CmdDownloadGraphicImage.Length) != CmdDownloadGraphicImage) return 1;

      var nextCommandIndex = Text.IndexOf('^', imageIndex);

      if (nextCommandIndex > 0)
        Text = Text.Substring(0, imageIndex - 1) + "\n" + Text.Substring(nextCommandIndex);
      else
        Text = Text.Substring(0, imageIndex - 1);
      
      return 0;
    }

    public string ImageExtractAndStrip(int imageIndex)
    {
      if (imageIndex < 0 || imageIndex >= (Text.Length - CmdDownloadGraphicImage.Length)) return "";
      var imageCode = ImageExtract(imageIndex);
      ImageStrip(imageIndex);

      return imageCode;
    }

    public string ImageExtract(int imageIndex)
    {
      if (imageIndex < 0 || imageIndex >= (Text.Length - CmdDownloadGraphicImage.Length)) return "";
      if (Text.Substring(imageIndex, CmdDownloadGraphicImage.Length) != CmdDownloadGraphicImage) return "";
        
      var nextCommandIndex = Text.IndexOf('^', imageIndex);
      return nextCommandIndex>0 ? Text.Substring(imageIndex, nextCommandIndex - imageIndex) : Text.Substring(imageIndex);
    }

    public int ImageIndexOf()
    {
      return Text.IndexOf(CmdDownloadGraphicImage);
    }

    public int ImageLastIndexOf()
    {
      return Text.LastIndexOf(CmdDownloadGraphicImage);
    }

    public long ImagesCount()
    {
        return new System.Text.RegularExpressions.Regex(CmdDownloadGraphicImage).Matches(Text).Count;
    }

    public bool IsValidZpl()
    {
      return true;
    }

    public string CommandExtract(int commandIndex)
    {
      if (commandIndex < 0 || commandIndex >= (Text.Length - 1)) return "";
      if (Text.Substring(commandIndex, 1) != "^") return "";

      var nextCommandIndex = Text.IndexOf('^', commandIndex+1);
      return nextCommandIndex > 0 ? Text.Substring(commandIndex, nextCommandIndex - commandIndex) : Text.Substring(commandIndex);
    }

    public int CommandStrip(int commandIndex)
    {
      if (commandIndex < 0 || commandIndex >= (Text.Length - CmdDownloadGraphicImage.Length)) return 2;
      if (Text.Substring(commandIndex, 1) != "^") return 1;

      var nextCommandIndex = Text.IndexOf('^', commandIndex+1);

      if (nextCommandIndex > 0)
        Text = Text.Substring(0, commandIndex - 1) + "\n" + Text.Substring(nextCommandIndex);
      else
        Text = Text.Substring(0, commandIndex - 1);

      return 0;
    }

    public int CommandIndex(int charIndex)
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

    public int NextCommandIndex(int charIndex)
    {
      return charIndex;
    }

    public int PrevCommandIndex(int charIndex)
    {
      if (charIndex <= 0 || charIndex >= Text.Length) return -1;
      var currentCommandIndex = CommandIndex(charIndex);
      return CommandIndex(currentCommandIndex - 1);
    }

    #endregion
  
  }
}

// TODO: cambiare il "estrai immagine" e il "strip immagine" in "estrai comando" e il "strip comando".
// TODO: i caratteri di inizio comando sono due: ^ e tilde
// TODO: verificare Command, PreviousCommand e NextCommand. contando sempre tutti e due i separatori.


