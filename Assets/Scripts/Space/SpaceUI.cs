using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SpaceUI : MonoBehaviour
{
    public static SpaceUI Default;
    public Image hp;
    public Image speed;
    public Image end;
    public Transform player;
    public AsteroidSpawner spawner;
    bool isEnd = false;

    private void Awake() 
    {
        Default = this;    
    }

    private void Start() 
    {
        hp.fillAmount = 1f;
        speed.fillAmount = 1f;       
    }

    private void Update() 
    {
        if(player.position.z >= spawner.Size.z - 200f && !isEnd)
        {
            isEnd = true;
            StartCoroutine(End());
        }
    }

    public void ChangeHp(float hp, float maxHp)
    {
        this.hp.fillAmount = hp / maxHp;
    }

    public void ChangeSpeed(float speed, float maxSpeed)
    {
        this.speed.fillAmount = speed / maxSpeed;
    }
    
    IEnumerator End()
    {
        while(end.color.a < 1)
        {
            end.color = new Color(0, 0, 0, end.color.a + 0.05f);
            yield return new WaitForSeconds(0.1f);
        }

        player.gameObject.SetActive(false);
    }
}
