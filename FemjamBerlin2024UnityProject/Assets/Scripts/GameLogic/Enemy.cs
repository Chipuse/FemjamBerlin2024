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
    private BodyPart Wing;
    public GameObject mouth;
    private BodyPart Mouth;
    public GameObject tail;
    private BodyPart Tail;
    public GameObject arm;
    private BodyPart Arm;
    public GameObject eye;
    private BodyPart Eye;

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
        Wing = wing.GetComponent<BodyPart>();
        Mouth = mouth.GetComponent<BodyPart>();
        Tail = tail.GetComponent<BodyPart>();
        Arm = arm.GetComponent<BodyPart>();
        Eye = eye.GetComponent<BodyPart>();
    }

    void Update()
    {
        
    }
    public void OnEnemyTurn()
    {
        switch (currentBodyPart)
        {
            case BodyPartEnum.wing:
                DoWingAttack();
                break;
            case BodyPartEnum.mouth:
                DoMouthAttack();
                break;
            case BodyPartEnum.tail:
                DoTailAttack();
                break;
            case BodyPartEnum.arm:
                DoArmAttack();
                break;
            case BodyPartEnum.eye:
                DoEyeAttack();
                currentBodyPart--;
                //new flag for open eye
                break;
            default:
                break;
        }

        currentBodyPart++;
        //check if all body parts dead, if not set back to = wing; else set to eye

    }

    void DoWingAttack()
    {
        if(Wing.ailmentState == Ailment.banded)
        {

        }
        else
        {
            //Death
        }
    }

    void DoMouthAttack()
    {
        if(GameManager.gameManager.hero.ailment == Ailment.frozen)
        {

        }
        else
        {
            //death
        }
    }

    void DoTailAttack()
    {
        if(GameManager.gameManager.hero.ailment == Ailment.petrified)
        {
            GameManager.gameManager.hero.inventory.Add(Items.stinger);
        }
        else
        {
            //death
        }
    }

    void DoArmAttack()
    {
        if (GameManager.gameManager.hero.inventory.Contains(Items.holyWater))
        {

        }
        else
        {
            //Death
        }
    }

    void DoEyeAttack()
    {
        if(Eye.ailmentState == Ailment.blind)
        {
            //stuned
        }
        else
        {
            //hero will get killed

        }
    }

}
