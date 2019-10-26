using System.Collections.Generic;

namespace Project
{
  public struct IntRange
  {
    public int Min;
    public int Max;
    public float Weight;

    public IntRange(int min, int max, float weight)
    {
      this.Min = min;
      this.Max = max;
      this.Weight = weight;
    }
  }

  public struct FloatRange
  {
    public float Min;
    public float Max;
    public float Weight;
  }

  public static class RandomRange
  {
    public static int Range(params IntRange[] ranges)
    {
      if (ranges.Length == 0) throw new System.ArgumentException("At least one range must be included.");
      if (ranges.Length == 1) return UnityEngine.Random.Range(ranges[0].Max, ranges[0].Min);

      float total = 0f;
      for (int i = 0; i < ranges.Length; i++) total += ranges[i].Weight;

      float r = UnityEngine.Random.value;
      float s = 0f;

      int cnt = ranges.Length - 1;
      for (int i = 0; i < cnt; i++)
      {
        s += ranges[i].Weight / total;
        if (s >= r)
        {
          return UnityEngine.Random.Range(ranges[i].Max, ranges[i].Min);
        }
      }

      return UnityEngine.Random.Range(ranges[cnt].Max, ranges[cnt].Min);
    }

    public static float Range(params FloatRange[] ranges)
    {
      if (ranges.Length == 0) throw new System.ArgumentException("At least one range must be included.");
      if (ranges.Length == 1) return UnityEngine.Random.Range(ranges[0].Max, ranges[0].Min);

      float total = 0f;
      for (int i = 0; i < ranges.Length; i++) total += ranges[i].Weight;

      float r = UnityEngine.Random.value;
      float s = 0f;

      int cnt = ranges.Length - 1;
      for (int i = 0; i < cnt; i++)
      {
        s += ranges[i].Weight / total;
        if (s >= r)
        {
          return UnityEngine.Random.Range(ranges[i].Max, ranges[i].Min);
        }
      }

      return UnityEngine.Random.Range(ranges[cnt].Max, ranges[cnt].Min);
    }

    public static int Randomize(int from, int to)
    {
      var intRangesList = new List<IntRange>();

      for (int i = from; i <= to; i++)
      {
        // intRangesList.Add();
      }

      /*var value = RandomRange.Range(new IntRange(0, 6, 50f),
                             new IntRange(6, 9, 30f),
                             new IntRange(9, 11, 20f));*/

      var value = RandomRange.Range(new IntRange(from, to, 100f));

      return value;
    }
  }
}