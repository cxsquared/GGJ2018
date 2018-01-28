using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private static SoundManager _instance;

    public static SoundManager Instance { get { return _instance; } }

    public AudioClip[] BigEnemyAttack;
    public AudioClip[] BigEnemyDeath;
    public AudioClip[] BigEnemyHit;
    public AudioClip[] LittleEnemyAttack;
    public AudioClip[] LittleEnemyDeath;
    public AudioClip[] LittleEnemyHit;
    public AudioClip[] LittleEnemyJump;

    private void Awake()
    {
        if (_instance != null && _instance != gameObject)
        {
            Destroy(gameObject);
        }

        _instance = this;
    }

    public void PlayBigEnemyAttack(Vector3 soundPos)
    {
        if (BigEnemyAttack.Length <= 0)
            return;

        AudioSource.PlayClipAtPoint(BigEnemyAttack.GetRandom(), soundPos); 
    }

    public void PlayBigEnemyDeath(Vector3 soundPos)
    {
        if (BigEnemyDeath.Length <= 0)
            return;

        AudioSource.PlayClipAtPoint(BigEnemyDeath.GetRandom(), soundPos); 
    }

    public void PlayBigEnemyHit(Vector3 soundPos)
    {
        if (BigEnemyHit.Length <= 0)
            return;

        AudioSource.PlayClipAtPoint(BigEnemyHit.GetRandom(), soundPos); 
    }

    public void PlayLittleEnemyAttack(Vector3 soundPos)
    {
        if (LittleEnemyAttack.Length <= 0)
            return;

        AudioSource.PlayClipAtPoint(LittleEnemyAttack.GetRandom(), soundPos); 
    }

    public void PlayLittleEnemyDeath(Vector3 soundPos)
    {
        if (LittleEnemyDeath.Length <= 0)
            return;

        AudioSource.PlayClipAtPoint(LittleEnemyDeath.GetRandom(), soundPos); 
    }

    public void PlayLittleEnemyHit(Vector3 soundPos)
    {
        if (LittleEnemyHit.Length <= 0)
            return;

        AudioSource.PlayClipAtPoint(LittleEnemyHit.GetRandom(), soundPos); 
    }

    public void PlayLittleEnemyJump(Vector3 soundPos)
    {
        if (LittleEnemyJump.Length <= 0)
            return;

        AudioSource.PlayClipAtPoint(LittleEnemyJump.GetRandom(), soundPos); 
    }
}
