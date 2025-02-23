using UnityEngine;
using System.Collections;

// Drag/Drop this script on a Particle System (or an object having Particle System objects as children) to prevent a Shuriken bug
// where a system would emit at its original instantiated position before being translated, resulting in particles in-between
// the two positions.
// Possibly a threading bug from Unity (as of 3.5.4)

public class CFX_ShurikenThreadFix : MonoBehaviour
{
	private ParticleSystem[] systems;

    [System.Obsolete]
    void Awake()
	{
		systems = GetComponentsInChildren<ParticleSystem>();
		
		foreach(ParticleSystem ps in systems)
			ps.enableEmission = false;

        StartCoroutine("WaitFrame");
	}

    [System.Obsolete]
    IEnumerator WaitFrame()
	{
		yield return null;
		
		foreach(ParticleSystem ps in systems)
		{
			ps.enableEmission = true;
			ps.Play(true);
		}
	}
}