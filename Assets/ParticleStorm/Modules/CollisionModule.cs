﻿#pragma warning disable 0649

using ParticleStorm.Core;
using System;
using ParticleStorm.Script;
using UnityEngine;

namespace ParticleStorm.Modules
{
	[Serializable]
	internal struct CollisionModule : IParticleModule
	{
		[Tooltip("Enable particle collision with game object")]
		public bool enabled;
		[Tooltip("Kill the particle after collision")]
		public bool kill;
		[Tooltip("Occation to call the script for particle")]
		public ParticleSystemTriggerEventType triggerType;
		[Tooltip("Name of the collision event for this particle")]
		public string collisionEvent;
		[Tooltip("The collision quality of this particle")]
		public ParticleSystemCollisionQuality quality;

		internal CollisionEvent onCollision;

		public void ApplicateOn(ParticleGenerator ps)
		{
			var module = ps.GetComponent<ParticleSystem>().collision;
			module.enabled = enabled;
			if (enabled)
			{
				module.mode = ParticleSystemCollisionMode.Collision3D;
				module.type = ParticleSystemCollisionType.World;
				module.sendCollisionMessages = true;
				module.enableDynamicColliders = true;
				module.quality = quality;
				if (kill)
				{
					module.maxKillSpeed = 0;
				}
				onCollision = CollisionEvent.Find(collisionEvent);
				if (onCollision == null)
				{
					Debug.LogWarning("No collision event named " + collisionEvent);
				}
				ps.collisionModule = this;
			}
		}
	}
}
