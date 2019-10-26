using UnityEngine;

using System;

namespace Animatix
{
	public abstract class ActionBase
	{
		protected readonly TimeSpan _duration;
		public TimeSpan Duration
		{
			get => _duration;
		}

		protected ActionBase(TimeSpan duration)
		{
			_duration = duration;
		}

		internal abstract object[] GetArguments(GameObject go);

		internal abstract void Update(GameObject go, float deltaTime, float journey, object[] arguments);
	}
}
