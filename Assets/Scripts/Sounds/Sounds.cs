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
        SoundHolder.Default.PlayFromSoundPack($"BackGround", true, LocationNum);    
        if(LocationNum == 0)
        {
            shipMove = SoundHolder.Default.PlayFromSoundPack("ShipMove", true);
            shipMove.volume = 0f;
            accelerator = SoundHolder.Default.PlayFromSoundPack("Accelerator", true);
            accelerator.volume = 0f;
        }
    }

    public void Accelerator(bool isOn = true)
    {
        if(acceleratorCour != null)
            StopCoroutine(acceleratorCour);
        if(isOn)
            acceleratorCour = StartCoroutine(OffOnSounds(accelerator, 1f, acceleratorCour));
        else
            acceleratorCour = StartCoroutine(OffOnSounds(accelerator, 0f, acceleratorCour));
    }

    public void ShipDamage()
    {
        SoundHolder.Default.PlayFromSoundPack("ShipDamage", false);
    }

    public void ShipMove(bool isOn = true)
    {
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
        SoundHolder.Default.PlayFromSoundPack("SwordAttack", false);
    }

    public void PlayerDamage()
    {
        SoundHolder.Default.PlayFromSoundPack("PlayerDamage", false);
    }

    public void GolemDamage()
    {
        SoundHolder.Default.PlayFromSoundPack("GolemDamage", false);
    }

    public void DemonDamage()
    {
        SoundHolder.Default.PlayFromSoundPack("DemonDamage", false);
    }

    public void Buff()
    {
        SoundHolder.Default.PlayFromSoundPack("Buff", false);
    }

    public void Bullet()
    {
        SoundHolder.Default.PlayFromSoundPack("Bullet", false);
    }

    public void Shield()
    {
        SoundHolder.Default.PlayFromSoundPack("Shield", false);
    }

    public void GolemRoar()
    {
        SoundHolder.Default.PlayFromSoundPack("GolemRoar", false);
    }

    public void GolemRun()
    {
        SoundHolder.Default.PlayFromSoundPack("GolemRun", false);
    }

    public void DemonsSpawn()
    {
        SoundHolder.Default.PlayFromSoundPack("DemonsSpawn", false);
    }

    public void GroundSlash()
    {
        SoundHolder.Default.PlayFromSoundPack("Ground", false);
        SoundHolder.Default.PlayFromSoundPack("GroundSlash", false);
    }
}