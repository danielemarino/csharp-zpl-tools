
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZPL.Tools
{
  class CommandEntry
  {
    public string Description;
    public string Parameters;
  }

  public class ZplDictionary
  {
    private readonly Dictionary<string, CommandEntry> _zplDictionary;

    public ZplDictionary(string dictionaryFile)
    {
      _zplDictionary = new Dictionary<string, CommandEntry>();
      DictionaryLoad(dictionaryFile);
    }

    private int DictionaryLoad(string dictionaryFile)
    {
      if (!File.Exists(dictionaryFile)) return -1;

      string[] dataRow;
      var csvInput = new CsvReader(dictionaryFile) {Separator = ';'};

      while ((dataRow= csvInput.GetCsvLine()) != null) CommandLoad(dataRow);
      return 0;
    }

    private int CommandLoad(IList<string> newCommand)
    {
      _zplDictionary.Add(newCommand[0], new CommandEntry {Description = newCommand[1], Parameters = newCommand[2]});
      return 0;
    }

    public long Count
    {
      get
      {
        return _zplDictionary.Count;
      }
    }

  }
}