using System;
using System.Collections;
using System.IO;
using System.Text;

namespace ZPL.Tools
{

  /// <summary>
  /// A data-reader style interface for reading CSV files.
  /// </summary>
  public class CsvReader : IDisposable
  {

    #region Private variables

    private readonly Stream _stream;
    private readonly StreamReader _reader;
    private char _separator = ',';

    #endregion

    public char Separator
    {
      get
      {
        return _separator;
      }
      set
      {
        _separator = value;
      }
    }

    /// <summary>
    /// Create a new reader for the given stream.
    /// </summary>
    /// <param name="s">The stream to read the CSV from.</param>
    public CsvReader(Stream s) : this(s, null) { }

    /// <summary>
    /// Create a new reader for the given stream and encoding.
    /// </summary>
    /// <param name="s">The stream to read the CSV from.</param>
    /// <param name="enc">The encoding used.</param>
    public CsvReader(Stream s, Encoding enc)
    {

      _stream = s;
      if (!s.CanRead)
      {
        throw new CsvReaderException("Could not read the given CSV stream!");
      }
      _reader = (enc != null) ? new StreamReader(s, enc) : new StreamReader(s);
    }

    /// <summary>
    /// Creates a new reader for the given text file path.
    /// </summary>
    /// <param name="filename">The name of the file to be read.</param>
    public CsvReader(string filename) : this(filename, null) { }

    /// <summary>
    /// Creates a new reader for the given text file path and encoding.
    /// </summary>
    /// <param name="filename">The name of the file to be read.</param>
    /// <param name="enc">The encoding used.</param>
    public CsvReader(string filename, Encoding enc)
      : this(new FileStream(filename, FileMode.Open), enc) { }

    /// <summary>
    /// Returns the fields for the next row of CSV data (or null if at eof)
    /// </summary>
    /// <returns>A string array of fields or null if at the end of file.</returns>
    public string[] GetCsvLine()
    {

      string data = _reader.ReadLine();

      // REPLACE REPLACE REPLACE
      //data = data.Replace("\"", "");

      if (data == null) return null;
      if (data.Length == 0) return new string[0];

      var result = new ArrayList();

      ParseCsvFields(result, data);

      return (string[])result.ToArray(typeof(string));
    }

    // Parses the CSV fields and pushes the fields into the result arraylist
    private void ParseCsvFields(ArrayList result, string data)
    {

      int pos = -1;
      while (pos < data.Length)
        result.Add(ParseCsvField(data, ref pos));
    }

    // Parses the field at the given position of the data, modified pos to match
    // the first unparsed position and returns the parsed field
    private string ParseCsvField(string data, ref int startSeparatorPosition)
    {

      if (startSeparatorPosition == data.Length - 1)
      {
        startSeparatorPosition++;
        // The last field is empty
        return "";
      }

      var fromPos = startSeparatorPosition + 1;

      // Determine if this is a quoted field
      if (data[fromPos] == '"')
      {
        // If we're at the end of the string, let's consider this a field that
        // only contains the quote
        if (fromPos == data.Length - 1)
        {
//          fromPos++;
          return "\"";
        }

        // Otherwise, return a string of appropriate length with double quotes collapsed
        // FSQ returns data.Length if no single quote was found
        var nextSingleQuote = FindSingleQuote(data, fromPos + 1);
        startSeparatorPosition = nextSingleQuote + 1;
        return data.Substring(fromPos + 1, nextSingleQuote - fromPos - 1).Replace("\"\"", "\"");
      }

      // The field ends in the next comma or EOL
      var nextComma = data.IndexOf(';', fromPos);
      if (nextComma == -1)
      {
        startSeparatorPosition = data.Length;
        return data.Substring(fromPos);
      }
      startSeparatorPosition = nextComma;
      return data.Substring(fromPos, nextComma - fromPos);
    }

    // Returns the index of the next single quote mark in the string 
    // (starting from startFrom)
    private static int FindSingleQuote(string data, int startFrom)
    {

      int i = startFrom - 1;
      while (++i < data.Length)
        if (data[i] == '"')
        {
          // If this is a double quote, bypass the chars
          if (i < data.Length - 1 && data[i + 1] == '"')
          {
            i++;
            continue;
          }
          return i;
        }
      // If no quote found, return the end value of i (data.Length)
      return i;
    }

    /// <summary>
    /// Disposes the CSVReader. The underlying stream is closed.
    /// </summary>
    public void Dispose()
    {
      // Closing the reader closes the underlying stream, too
      if (_reader != null) _reader.Close();
      else if (_stream != null)
        _stream.Close(); // In case we failed before the reader was constructed
      GC.SuppressFinalize(this);
    }
  }


  /// <summary>
  /// Exception class for CSVReader exceptions.
  /// </summary>
  public class CsvReaderException : ApplicationException
  {

    /// <summary>
    /// Constructs a new exception object with the given message.
    /// </summary>
    /// <param name="message">The exception message.</param>
    public CsvReaderException(string message) : base(message) { }
  }

}
