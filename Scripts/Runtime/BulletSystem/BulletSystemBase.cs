﻿using System;
using CANStudio.BulletStorm.BulletSystem.Modules;
using CANStudio.BulletStorm.Emission;
using NaughtyAttributes;
using UnityEngine;

#pragma warning disable 0649

namespace CANStudio.BulletStorm.BulletSystem
{
    /// <summary>
    /// A more convenient base class for <see cref="MonoBehaviour"/> based particle systems.
    /// </summary>
    public abstract class BulletSystemBase : MonoBehaviour, IBulletSystem, IBulletController
    {
        [Tooltip("Enable playing effect when emitting bullets."), Label("Enable")]
        [SerializeField, BoxGroup("Emission effect")]
        private bool enableEmissionEffect;
        
        [Tooltip("Play particle effect when emitting."), Label("Detail")]
        [SerializeField, EnableIf("enableEmissionEffect"), BoxGroup("Emission effect")]
        private EmissionEffectModule emissionEffect;
        
        [Tooltip("Enable bullets tracing some game object."), Label("Enable")]
        [SerializeField, BoxGroup("Tracing")]
        private bool enableTracing;
        
        [Tooltip("Bullets trace a target."), Label("Detail")]
        [SerializeField, EnableIf("enableTracing"), BoxGroup("Tracing")]
        private TracingModule tracing;
        
        public virtual string Name => name;
        public abstract void ChangePosition(Func<Vector3, Vector3, Vector3> operation);
        public abstract void ChangeVelocity(Func<Vector3, Vector3, Vector3> operation);
        public abstract void Emit(BulletEmitParam emitParam, Transform emitter);
        public abstract void Destroy();
        public virtual IBulletController GetController() => Instantiate(this);
        public virtual void SetParent(Transform parent) => transform.SetParent(parent, false);

        /// <summary>
        /// Plays the emission effect. Call this when emitting a bullet.
        /// </summary>
        protected void PlayEmissionEffect(BulletEmitParam emitParam, Transform emitter)
        {
            if (enableEmissionEffect) emissionEffect.OnEmit(emitParam, emitter);
        }

        /// <summary>
        /// Executes tracing module.
        /// </summary>
        protected void Start()
        {
            if (enableTracing) tracing.Init();
        }

        /// <summary>
        /// Executes tracing module.
        /// </summary>
        protected virtual void Update()
        {
            if (enableTracing) tracing.OnUpdate(this);
        }
    }
}