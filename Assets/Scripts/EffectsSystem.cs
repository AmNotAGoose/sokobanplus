using System.Collections.Generic;
using UnityEngine;

public class EffectsSystem : MonoBehaviour
{
    public List<ParticleSystem> particleSystems;

    public void PlayEffect(int index)
    {
        particleSystems[index].Play();
    }
    
    public void StopEffect(int index) 
    {
        particleSystems[index].Stop();
    }
}
