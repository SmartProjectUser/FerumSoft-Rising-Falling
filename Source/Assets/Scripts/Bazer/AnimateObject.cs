using UnityEngine;
using UnityEngine.Events;

using System;
using System.Collections;
using System.Collections.Generic;

using Animatix;

namespace Bazer
{
	public class AnimateObject : BaseObject
	{
		public AnimateComponent animateComponent;

		public AnimateObject()
		{
		}

		public void SetAnimationBoolParam(string paramName, bool paramValue)
		{
			Animator animatorComponent = gameObject.GetComponent<Animator>();

			if (animatorComponent != null)
			{
				animatorComponent.SetBool(paramName, paramValue);
			}
			else
			{
				throw new Exception("No Animator Component on object");
			}
		}

		public void Animate(ActionBase[] actions, UnityAction callback, Repeat repeat = Repeat.Once)
		{
			if (animateComponent == null)
			{
				animateComponent = gameObject.AddComponent<AnimateComponent>();

				animateComponent.onFinish.AddListener(callback);
			}

			animateComponent.Begin(repeat, actions);
		}

		public void Animate(Parallel parallel, UnityAction callback, Repeat repeat = Repeat.Once)
		{
			if (animateComponent == null)
			{
				animateComponent = gameObject.AddComponent<AnimateComponent>();

				animateComponent.onFinish.AddListener(callback);
			}

			animateComponent.Begin(repeat, parallel);
		}

		public void StopAnimate()
		{

		}

		public void MoveTo(Vector3 destPosition, float speed, UnityAction callback)
		{
			float startTime = Time.time;
			float journeyLength = Vector3.Distance(gameObject.transform.position, destPosition);
			Vector3 startPosition = gameObject.transform.position;

			UnityAction updateAction = () =>
			{
				float distCovered = (Time.time - startTime) * speed;
				float fracJourney = distCovered / journeyLength;

				gameObject.transform.position = Vector3.Lerp(startPosition, destPosition, fracJourney);

				if (gameObject.transform.position == destPosition)
				{
					gameComponent.onUpdate.RemoveAllListeners();

					callback();
				}
			};

			gameComponent.onUpdate.AddListener(updateAction);
		}
	}
}