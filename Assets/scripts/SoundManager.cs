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
    public AudioClip[] PlayerAttack;
    public AudioClip[] PlayerDeath;
    public AudioClip[] PlayerHit;
    public AudioClip[] PlayerJump;
    public AudioClip[] PlayerGiggle;

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

    public void PlayPlayerAttack(Vector3 soundPos)
    {
        if (PlayerAttack.Length <= 0)
            return;

        AudioSource.PlayClipAtPoint(PlayerAttack.GetRandom(), soundPos); 
    }

    public void PlayPlayerDeath(Vector3 soundPos)
    {
        if (PlayerDeath.Length <= 0)
            return;

        AudioSource.PlayClipAtPoint(PlayerDeath.GetRandom(), soundPos); 
    }

    public void PlayPlayerHit(Vector3 soundPos)
    {
        if (PlayerHit.Length <= 0)
            return;

        AudioSource.PlayClipAtPoint(PlayerHit.GetRandom(), soundPos); 
    }

    public void PlayPlayerJump(Vector3 soundPos)
    {
        if (PlayerJump.Length <= 0)
            return;

        AudioSource.PlayClipAtPoint(PlayerJump.GetRandom(), soundPos); 
    }

    public void PlayPlayerGiggle(Vector3 soundPos)
    {
        if (PlayerGiggle.Length <= 0)
            return;

        AudioSource.PlayClipAtPoint(PlayerGiggle.GetRandom(), soundPos);
    }
}
