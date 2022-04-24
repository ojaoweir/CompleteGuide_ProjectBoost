using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineParticlesController : MonoBehaviour
{
    private ParticleSystem[] particleSystems;
    private bool emitting;
    private void Start()
    {
        emitting = false;
        particleSystems = GetComponentsInChildren<ParticleSystem>();
    }
    public void PlayParticles()
    {
        if (emitting) return;
        foreach(ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }
        emitting = true;
    }
    public void StopParticles()
    {
        if (!emitting) return;
        foreach(ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
        emitting = false;
    }
}
