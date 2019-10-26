using UnityEngine;
using UnityEngine.Events;

using System;
using System.Collections.Generic;

/*myGameObject.AddComponent<Animate>()
  .Begin(Repeat.Forever,
new MoveTo(TimeSpan.FromSeconds(5), myGameObject.transform.position + new Vector3(15, 0, 0), Easing.EaseInBounce, TransformOrigin.World),
new DelayTime(TimeSpan.FromSeconds(3)),
new MoveTo(TimeSpan.FromSeconds(5), myGameObject.transform.position, Easing.EaseOutQuint, TransformOrigin.World),
new DelayTime(TimeSpan.FromSeconds(3)),
new MoveTo(TimeSpan.FromSeconds(5), myGameObject.transform.position - new Vector3(15, 0, 0), Easing.EaseInBack, TransformOrigin.World),
new DelayTime(TimeSpan.FromSeconds(3)),
new MoveTo(TimeSpan.FromSeconds(5), myGameObject.transform.position, Easing.Spring, TransformOrigin.World),
new DelayTime(TimeSpan.FromSeconds(3)));*/

/*myGameObject.AddComponent<Animate>()
  .Begin(Repeat.Forever, new Parallel(
new MoveTo(TimeSpan.FromSeconds(5), myGameObject.transform.position - new Vector3(0, 5, 0)),
new ScaleTo(TimeSpan.FromSeconds(5), Vector3.zero)));*/

namespace Animatix
{
  public class AnimateComponent: MonoBehaviour
  {
    // public const Repeat DefaultRepeat = Repeat.Once;

    public UnityEvent onFinish = new UnityEvent();

    List<ActionSequence> _sequences = new List<ActionSequence>();

    void Update()
    {
      var count = _sequences.Count;

      if (count == 0)
      {
        return;
      }

      var deltaTime = Time.deltaTime;

      for (int i = 0; i < count; i++)
      {
        var sequence = _sequences[i];

        if (sequence.Current.Duration <= TimeSpan.Zero)
        {
          goto Done;
        }

        if (!sequence.CurrentStarted)
        {
          sequence.CurrentArguments = sequence.Current.GetArguments(gameObject);
          sequence.CurrentStarted = true;
        }

        if (sequence.CurrentJourney >= sequence.Current.Duration.TotalSeconds)
        {
          goto Done;
        }

        sequence.CurrentJourney += deltaTime;
        sequence.Current.Update(gameObject, deltaTime, sequence.CurrentJourney, sequence.CurrentArguments);

        continue;

        //Is the action done?
        Done:
        {
          //If so, get the next
          sequence.Next();

          if (sequence.Repeat == Repeat.Forever)
          {
            //If done, prepare for next iteration
            if (sequence.CurrentIndex == sequence.Length)
            {
              sequence.CurrentIndex = 0;

              onFinish.Invoke();
            }

            //Get next action now so we don't have to on subsequent updates
            sequence.Current = sequence[sequence.CurrentIndex];
          }
          else if (sequence.Repeat == Repeat.Once)
          {
            //If done, remove current sequence
            if (sequence.CurrentIndex == sequence.Length)
            {
              _sequences.RemoveAt(i);
              count--;
              i--;

              onFinish.Invoke();
            }
            //Otherwise, get next action now so we don't have to on subsequent updates
            else
            {
              sequence.Current = sequence[sequence.CurrentIndex];
            }
          }
        }
      }
    }

    public void Begin(params ActionBase[] actions)
    {
      Begin(Repeat.Once, actions);
    }

    public void Begin(Repeat repeat, params ActionBase[] actions)
    {
      var sequence = new ActionSequence(repeat, actions);
      _sequences.Add(sequence);
    }

    public void Begin(Parallel parallel)
    {
      Begin(Repeat.Once, parallel);
    }

    public void Begin(Repeat repeat, Parallel parallel)
    {
      for (uint i = 0; i < parallel.Count; i++)
      {
        Begin(repeat, parallel[i]);
      }
    }
  }
}