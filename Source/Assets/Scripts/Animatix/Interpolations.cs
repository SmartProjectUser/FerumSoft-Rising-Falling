using UnityEngine;

using System;

namespace Animatix
{
  public abstract class Interpolate<TValue> : ActionBase
  {
    protected readonly Easing _easing;

    protected readonly TValue _target;

    protected Interpolate(TimeSpan duration, TValue target, Easing easing) : base(duration)
    {
      _easing = easing;
      _target = target;
    }

    protected float GetValue(Easing easing, float origin, float target, float percent)
    {
      switch (_easing)
      {
        case Easing.EaseInBack:
          return EasingFunction.EaseInBack(origin, target, percent);
        case Easing.EaseInBounce:
          return EasingFunction.EaseInBounce(origin, target, percent);
        case Easing.EaseInCirc:
          return EasingFunction.EaseInCirc(origin, target, percent);
        case Easing.EaseInCubic:
          return EasingFunction.EaseInCubic(origin, target, percent);
        case Easing.EaseInElastic:
          return EasingFunction.EaseInElastic(origin, target, percent);
        case Easing.EaseInExpo:
          return EasingFunction.EaseInExpo(origin, target, percent);
        case Easing.EaseInOutBack:
          return EasingFunction.EaseInOutBack(origin, target, percent);
        case Easing.EaseInOutBounce:
          return EasingFunction.EaseInOutBounce(origin, target, percent);
        case Easing.EaseInOutCirc:
          return EasingFunction.EaseInOutCirc(origin, target, percent);
        case Easing.EaseInOutCubic:
          return EasingFunction.EaseInOutCubic(origin, target, percent);
        case Easing.EaseInOutElastic:
          return EasingFunction.EaseInOutElastic(origin, target, percent);
        case Easing.EaseInOutExpo:
          return EasingFunction.EaseInOutExpo(origin, target, percent);
        case Easing.EaseInOutQuad:
          return EasingFunction.EaseInOutQuad(origin, target, percent);
        case Easing.EaseInOutQuart:
          return EasingFunction.EaseInOutQuart(origin, target, percent);
        case Easing.EaseInOutQuint:
          return EasingFunction.EaseInOutQuint(origin, target, percent);
        case Easing.EaseInOutSine:
          return EasingFunction.EaseInOutSine(origin, target, percent);
        case Easing.EaseInQuad:
          return EasingFunction.EaseInQuad(origin, target, percent);
        case Easing.EaseInQuart:
          return EasingFunction.EaseInQuart(origin, target, percent);
        case Easing.EaseInQuint:
          return EasingFunction.EaseInQuint(origin, target, percent);
        case Easing.EaseInSine:
          return EasingFunction.EaseInSine(origin, target, percent);
        case Easing.EaseOutBack:
          return EasingFunction.EaseOutBack(origin, target, percent);
        case Easing.EaseOutBounce:
          return EasingFunction.EaseOutBounce(origin, target, percent);
        case Easing.EaseOutCirc:
          return EasingFunction.EaseOutCirc(origin, target, percent);
        case Easing.EaseOutCubic:
          return EasingFunction.EaseOutCubic(origin, target, percent);
        case Easing.EaseOutElastic:
          return EasingFunction.EaseOutElastic(origin, target, percent);
        case Easing.EaseOutExpo:
          return EasingFunction.EaseOutExpo(origin, target, percent);
        case Easing.EaseOutQuad:
          return EasingFunction.EaseOutQuad(origin, target, percent);
        case Easing.EaseOutQuart:
          return EasingFunction.EaseOutQuart(origin, target, percent);
        case Easing.EaseOutQuint:
          return EasingFunction.EaseOutQuint(origin, target, percent);
        case Easing.EaseOutSine:
          return EasingFunction.EaseOutSine(origin, target, percent);
        case Easing.Linear:
          return EasingFunction.Linear(origin, target, percent);
        case Easing.Spring:
          return EasingFunction.Spring(origin, target, percent);
      }

      throw new NotSupportedException();
    }

    protected abstract TValue GetValue(TValue origin, float percent);

    protected abstract void SetValue(GameObject go, TValue value);

    internal override void Update(GameObject go, float deltaTime, float journey, object[] arguments)
    {
      var origin = (TValue) arguments[0];
      var percent = Mathf.Clamp01(journey / Convert.ToSingle(_duration.TotalSeconds));
      var value = GetValue(origin, percent);

      SetValue(go, value);
    }
  }

  public abstract class InterpolateVector3 : Interpolate<Vector3>
  {
    protected InterpolateVector3(TimeSpan duration, Vector3 target, Easing easing) : base(duration, target, easing)
    {
    }

    protected override Vector3 GetValue(Vector3 origin, float percent)
    {
      var value = new float[3];

      for (var i = 0; i < 3; i++)
      {
        var origin_
          = i == 0
            ? origin.x
            : i == 1
              ? origin.y
              : origin.z;

        var target_
          = i == 0
            ? _target.x
            : i == 1
              ? _target.y
              : _target.z;

        value[i] = GetValue(_easing, origin_, target_, percent);
      }

      return new Vector3(value[0], value[1], value[2]);
    }
  }

  public abstract class InterpolateVector4 : Interpolate<Vector4>
  {
    protected InterpolateVector4(TimeSpan duration, Vector3 target, Easing easing) : base(duration, target, easing)
    {
    }

    protected override Vector4 GetValue(Vector4 origin, float percent)
    {
      var value = new float[4];

      for (var i = 0; i < 4; i++)
      {
        var origin_
          = i == 0
            ? origin.w
            : i == 1
              ? origin.x
              : i == 2
                ? origin.y
                : origin.z;

        var target_
          = i == 0
            ? _target.w
            : i == 1
              ? _target.x
              : i == 2
                ? _target.y
                : _target.z;

        value[i] = GetValue(_easing, origin_, target_, percent);
      }

      return new Vector4(value[0], value[1], value[2], value[3]);
    }
  }

  public abstract class InterpolateQuaternion : Interpolate<Quaternion>
  {
    protected InterpolateQuaternion(TimeSpan duration, Quaternion target, Easing easing) : base(duration, target,
      easing)
    {
    }

    protected override Quaternion GetValue(Quaternion origin, float percent)
    {
      switch (_easing)
      {
        case Easing.Linear:
          return Quaternion.Lerp(origin, _target, percent);
      }

      throw new NotSupportedException();
    }
  }
}