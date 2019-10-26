using UnityEngine;

using System;

namespace Animatix
{
  public enum TransformOrigin
  {
    Local,
    World
  }

  public class DelayTime : ActionBase
  {
    public DelayTime(TimeSpan duration) : base(duration) { }

    internal override object[] GetArguments(GameObject go)
    {
      return default(object[]);
    }

    internal override void Update(GameObject go, float deltaTime, float journey, object[] arguments) { }
  }

  public class MoveTo : InterpolateVector3
  {
    protected readonly TransformOrigin _transformOrigin;

    public MoveTo(TimeSpan duration, Vector3 target, Easing easing = Easing.Linear, TransformOrigin transformOrigin = TransformOrigin.Local) : base(duration, target, easing)
    {
      _transformOrigin = transformOrigin;
    }

    internal override object[] GetArguments(GameObject go)
    {
      return new object[]
      {
        _transformOrigin == TransformOrigin.Local
          ? go.transform.localPosition
          : go.transform.position
      };
    }

    protected override void SetValue(GameObject go, Vector3 value)
    {
      switch (_transformOrigin)
      {
        case TransformOrigin.Local:
          go.transform.localPosition = value;
          break;
        case TransformOrigin.World:
          go.transform.position = value;
          break;
      }
    }
  }

  public class RotateTo : InterpolateQuaternion
  {
    protected readonly TransformOrigin _transformOrigin;

    public RotateTo(TimeSpan duration, Quaternion target, Easing easing = Easing.Linear, TransformOrigin transformOrigin = TransformOrigin.Local) : base(duration, target, easing)
    {
      _transformOrigin = transformOrigin;
    }

    internal override object[] GetArguments(GameObject go)
    {
      return new object[]
      {
        _transformOrigin == TransformOrigin.Local
          ? go.transform.localRotation
          : go.transform.rotation
      };
    }

    protected override void SetValue(GameObject go, Quaternion value)
    {
      switch (_transformOrigin)
      {
        case TransformOrigin.Local:
          go.transform.localRotation = value;
          break;
        case TransformOrigin.World:
          go.transform.rotation = value;
          break;
      }
    }
  }

  public class ScaleTo : InterpolateVector3
  {
    public ScaleTo(TimeSpan duration, Vector3 target, Easing easing = Easing.Linear) : base(duration, target, easing) { }

    internal override object[] GetArguments(GameObject go)
    {
      return new object[]
      {
        go.transform.localScale
      };
    }

    protected override void SetValue(GameObject go, Vector3 value)
    {
      go.transform.localScale = value;
    }
  }
}