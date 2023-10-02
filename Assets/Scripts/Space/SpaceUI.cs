using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceUI : MonoBehaviour
{
    public static SpaceUI Default;
    public Image hp;
    public Image speed;
    public Image end;
    public Transform player;
    public AsteroidSpawner spawner;
    public GameObject Tutorial;
    public GameObject Pause;
    public GameObject Death;

    bool isStart = false;
    bool isEnd = false;
    bool isDie = false;

    private void Awake() 
    {
        Default = this;    
    }

    private void Start() 
    {
        hp.fillAmount = 1f;
        speed.fillAmount = 1f;    
        Time.timeScale = 0f;
    }

    private void Update() 
    {
        if(!isStart && Input.anyKey)
        {
            Tutorial.SetActive(false);
            Time.timeScale = 1f;
            isStart = true;
        }

        if(!isStart) return;

        if(!isDie && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Pause.SetActive(true);
            Time.timeScale = 0f;
        }

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

    public void Continue()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Pause.SetActive(false);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("FlyScene");
    }

    public void Die()
    {
        isDie = true;
        StartCoroutine(DieCour());
    }

    IEnumerator DieCour()
    {
        while(end.color.a < 1)
        {
            end.color = new Color(0, 0, 0, end.color.a + 0.05f);
            yield return new WaitForSeconds(0.1f);
        }

        Cursor.lockState = CursorLockMode.Confined;
        Death.SetActive(true);
    }
    
    IEnumerator End()
    {
        while(end.color.a < 1)
        {
            end.color = new Color(0, 0, 0, end.color.a + 0.05f);
            yield return new WaitForSeconds(0.1f);
        }

        player.gameObject.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene("FightScene");
    }
}
