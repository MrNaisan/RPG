using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Default;
    public List<Image> CDImages;
    public List<Text> CDTexts;
    public Image HP;
    [HideInInspector]
    public float maxHP;
    public Image EnemyHP;
    public Image end;
    public Text EnemyName;
    public GameObject EnemyUI;
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
        for(int i = 0; i < CDImages.Count; i++)
        {
            CDImages[i].fillAmount = 0;
            CDTexts[i].gameObject.SetActive(false);
        }    
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
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            Pause.SetActive(true);
        }
    }

    public void ChangeHP(float hp)
    {
        HP.fillAmount = hp / maxHP;
    }

    public void ChangeEnemyHP(float hp, float maxHP, string name)
    {
        EnemyUI.SetActive(true);
        EnemyHP.fillAmount = hp / maxHP;
        EnemyName.text = name;
    }

    public void SetCDImage(int skillNum)
    {
        CDImages[skillNum-1].fillAmount = 1;
    }

    public void SkillCD(int skillNum, float time)
    {
        StartCoroutine(CD(skillNum-1, time));
    }

    IEnumerator CD(int skillNum, float time)
    {
        CDTexts[skillNum].gameObject.SetActive(true);
        CDTexts[skillNum].text = $"{time}s";
        CDImages[skillNum].fillAmount = 1;

        var step = 1f / time;
        float timePassed = time;

        while(timePassed > 0)
        {
            yield return new WaitForSeconds(1f);
            CDImages[skillNum].fillAmount -= step;
            timePassed -= 1f;
            CDTexts[skillNum].text = $"{timePassed}s";
        }

        CDTexts[skillNum].text = $"0s";
        CDTexts[skillNum].gameObject.SetActive(false);
        CDImages[skillNum].fillAmount = 0;
    }
    
    public void Continue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Pause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
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

    public void End()
    {
        StartCoroutine(EndCour());
    }
    
    IEnumerator EndCour()
    {
        while(end.color.a < 1)
        {
            end.color = new Color(0, 0, 0, end.color.a + 0.05f);
            yield return new WaitForSeconds(0.1f);
        }
        
        SceneManager.LoadScene("Menu");
    }
}
