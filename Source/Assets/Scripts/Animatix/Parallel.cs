using System;

namespace Animatix
{
  public class Parallel
  {
    readonly ActionBase[] _actions;

    public uint Count
    {
      get => Convert.ToUInt32(_actions.Length);
    }

    public Parallel(params ActionBase[] actions)
    {
      _actions = actions;
    }

    public ActionBase this[uint index]
    {
      get => _actions[index];
    }
  }
}