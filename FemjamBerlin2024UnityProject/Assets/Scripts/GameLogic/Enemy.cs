using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 9999;
    public int currentHealth = 9999;

    public GameObject healthbar;
    public void UpdateHealthBar()
    {
        float newScale = (float)currentHealth / (float)maxHealth;
        if (newScale < 0)
            newScale = 0;
        healthbar.transform.localScale = new Vector3(newScale, 1f, 1f);

    }


    public GameObject wing;
    public GameObject mouth;
    public GameObject tail;
    public GameObject arm;
    public GameObject eye;

    public int bodyPartCount = 5;
    public void CheckBodyParts()
    {
        StartCoroutine(CoroutineCheckBodyparts());
    }
    public IEnumerator CoroutineCheckBodyparts()
    {
        if(bodyPartCount == 1)
        {
            //eye opens!
            eye.SetActive(true);
            //health has to get to 31
            while (currentHealth != 31)
            {
                yield return new WaitForEndOfFrame();
                currentHealth--;
                UpdateHealthBar();
            }
        }
        else
        {
            //other text trigger
        }
    }

    public BodyPartEnum currentBodyPart = BodyPartEnum.wing;

    void Start()
    {
        eye.SetActive(false);
        UpdateHealthBar();
    }

    void Update()
    {
        
    }
    public void OnEnemyTurn()
    {
        switch (currentBodyPart)
        {
            case BodyPartEnum.wing:

                break;
            case BodyPartEnum.mouth:
                break;
            case BodyPartEnum.tail:
                break;
            case BodyPartEnum.arm:
                break;
            case BodyPartEnum.eye:
                
                currentBodyPart--;
                //new flag for open eye
                break;
            default:
                break;
        }

        currentBodyPart++;
        //check if all body parts dead, if not set back to = wing; else set to eye

    }

}
