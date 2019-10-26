using System;

namespace Animatix
{
	public enum Repeat
	{
		Once,
		Forever
	}

	class ActionSequence
	{
		/// <summary>
		/// The actions to perform.
		/// </summary>
		readonly ActionBase[] _actions;

		/// <summary>
		/// The current action.
		/// </summary>
		public ActionBase Current;

		/// <summary>
		/// The arguments to evaluate for the current action.
		/// </summary>
		public object[] CurrentArguments;

		/// <summary>
		/// The index of the current action.
		/// </summary>
		public uint CurrentIndex = 0;

		/// <summary>
		/// The "journey" of the current action.
		/// </summary>
		public float CurrentJourney = 0;

		/// <summary>
		/// Whether or not the current action has started.
		/// </summary>
		public bool CurrentStarted;

		/// <summary>
		/// The number of actions in the sequence.
		/// </summary>
		public readonly uint Length;

		/// <summary>
		/// The manner in which the sequence of actions should repeat.
		/// </summary>
		public Repeat Repeat;

		public ActionSequence(Repeat repeat, ActionBase[] actions)
		{
			_actions = actions;
			Current = _actions[0];
			Length = Convert.ToUInt32(_actions.Length);
			Repeat = repeat;
		}

		public ActionBase this[uint index]
		{
			get => _actions[index];
		}

		public void Next()
		{
			CurrentArguments = null;
			CurrentIndex++;
			CurrentJourney = 0;
			CurrentStarted = false;
		}
	}
}