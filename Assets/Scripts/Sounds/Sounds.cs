using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    private static Sounds _default;
    public static Sounds Default { get => _default; }
    public Sounds() => _default = this;
    public int LocationNum;
    AudioSource shipMove;
    AudioSource accelerator;
    Coroutine moveCour;
    Coroutine acceleratorCour;

    private void Start() 
    {
        if(PlayerPrefs.GetInt("BackGroundSound") == 1)
            SoundHolder.Default.PlayFromSoundPack($"BackGround", true, LocationNum);    
        if(LocationNum == 0)
        {
            if(PlayerPrefs.GetInt("SoundEffects") != 1) return;

            shipMove = SoundHolder.Default.PlayFromSoundPack("ShipMove", true);
            shipMove.volume = 0f;
            accelerator = SoundHolder.Default.PlayFromSoundPack("Accelerator", true);
            accelerator.volume = 0f;
        }
    }

    public void Accelerator(bool isOn = true)
    {
        if(PlayerPrefs.GetInt("SoundEffects") != 1) return;

        if(acceleratorCour != null)
            StopCoroutine(acceleratorCour);
        if(isOn)
            acceleratorCour = StartCoroutine(OffOnSounds(accelerator, 1f, acceleratorCour));
        else
            acceleratorCour = StartCoroutine(OffOnSounds(accelerator, 0f, acceleratorCour));
    }

    public void ShipDamage()
    {
        Play("ShipDamage");
    }

    public void ShipMove(bool isOn = true)
    {
        if(PlayerPrefs.GetInt("SoundEffects") != 1) return;

        if(moveCour != null)
            StopCoroutine(moveCour);
        if(isOn)
            moveCour = StartCoroutine(OffOnSounds(shipMove, 1f, moveCour));
        else
            moveCour = StartCoroutine(OffOnSounds(shipMove, 0f, moveCour));
    }

    IEnumerator OffOnSounds(AudioSource source, float endValue, Coroutine cour)
    {
        if(endValue > 0)
        {
            while(source.volume < endValue)
            {
                yield return new WaitForSeconds(0.1f);
                source.volume += endValue / 5;
            }
        }
        else
        {
            while(source.volume > 0)
            {
                yield return new WaitForSeconds(0.1f);
                source.volume -= .2f;
            }
        }
        
        cour = null;
    }

    public void PlayerAttack()
    {
        Play("SwordAttack");
    }

    public void PlayerDamage()
    {
        Play("PlayerDamage");
    }

    public void GolemDamage()
    {
        Play("GolemDamage");
    }

    public void DemonDamage()
    {
        Play("DemonDamage");
    }

    public void Buff()
    {
        Play("Buff");
    }

    public void Bullet()
    {
        Play("Bullet");
    }

    public void Shield()
    {
        Play("Shield");
    }

    public void GolemRoar()
    {
        Play("GolemRoar");
    }

    public void GolemRun()
    {
        Play("GolemRun");
    }

    public void DemonsSpawn()
    {
        Play("DemonsSpawn");
    }

    public void GroundSlash()
    {
        Play("Ground");
        Play("GroundSlash");
    }

    void Play(string name)
    {
        if(PlayerPrefs.GetInt("SoundEffects") == 1)
            SoundHolder.Default.PlayFromSoundPack(name, false);
    }
}