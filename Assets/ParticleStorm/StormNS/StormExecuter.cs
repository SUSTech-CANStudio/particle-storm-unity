﻿using JetBrains.Annotations;
using ParticleStorm.ParticleNS;
using ParticleStorm.StormNS.Behavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ParticleStorm.StormNS
{
	/// <summary>
	/// Executes storm behaviors.<para/>
	/// To generate a storm at given time and position,
	/// get an executer from it.
	/// </summary>
	internal class StormExecuter
	{
		private readonly SortedList<StormBehavior, ParticleSystemController> behaviors;
		private readonly StormGenerator generator;

		public StormExecuter(
			[NotNull] SortedList<StormBehavior, ParticleSystemController> behaviors,
			[NotNull] StormGenerator generator)
		{
			this.behaviors = behaviors;
			this.generator = generator;
		}

		/// <summary>
		/// Start to execute storm behaviors.
		/// </summary>
		/// <returns></returns>
		public IEnumerator Start()
		{
			float startTime = Time.time;
			int i = 0;
			// Generate.
			while (i < behaviors.Count)
			{
				if (Time.time - startTime >= behaviors.Keys[i].StartTime)
				{
					generator.StartCoroutine(
						behaviors.Keys[i].Execute(behaviors.Values[i], generator.transform));
					i++;
				}
				else { yield return new WaitForFixedUpdate(); }
			}
		}
	}
}
