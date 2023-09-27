using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Default;
    public List<Image> CDImages;
    public List<Text> CDTexts;
    public Image HP;
    [HideInInspector]
    public float maxHP;
    public Image EnemyHP;
    public Text EnemyName;
    public GameObject EnemyUI;
    
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
    

}
