using UnityEngine;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Project
{
  public static class AbbrevationUtility
  {
    private static readonly SortedDictionary<ulong, string> abbrevations = new SortedDictionary<ulong, string>
    {
      {1000,"K"},
      {1000000, "M" },
      {1000000000, "B" },
      {1000000000000, "T" },
    };

    public static string AbbreviateNumber(ulong number)
    {
      for (int i = abbrevations.Count - 1; i >= 0; i--)
      {
        KeyValuePair<ulong, string> pair = abbrevations.ElementAt(i);

        if (Mathf.Abs(number) >= pair.Key)
        {
          int roundedNumber = Mathf.FloorToInt(number / pair.Key);

          return roundedNumber.ToString() + pair.Value;
        }
      }

      return number.ToString();
    }
  }
}