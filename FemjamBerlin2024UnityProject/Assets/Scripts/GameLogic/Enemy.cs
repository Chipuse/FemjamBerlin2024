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

    public void ChargeWingAttack()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.wing, AttackTextContext.whenCharging));
    }

    void DoWingAttack()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterWingAttack));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.wing, AttackTextContext.beforeAttack));
    }

    public void AfterWingAttack()
    {
        if (Wing.ailmentState == Ailment.banded)
        {
            MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.wing, AttackTextContext.afterAttack));
            GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(ChargeMouthAttack));
        }
        else
        {
            MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.wing, AttackTextContext.afterAttack));
            //Death
        }
    }

    void ChargeMouthAttack()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.mouth, AttackTextContext.whenCharging));
    }

    void DoMouthAttack()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterMouthAttack));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.mouth, AttackTextContext.beforeAttack));
    }

    public void AfterMouthAttack()
    {
        if (GameManager.gameManager.hero.ailment == Ailment.frozen)
        {

        }
        else
        {
            //death
        }

    }


    void ChargeTailAttack()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.tail, AttackTextContext.whenCharging));
    }


    void DoTailAttack()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterTailAttack));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.tail, AttackTextContext.beforeAttack));
    }

    public void AfterTailAttack()
    {
        if (GameManager.gameManager.hero.ailment == Ailment.petrified)
        {
            GameManager.gameManager.hero.inventory.Add(Items.stinger);
        }
        else
        {
            //death
        }

    }

    void ChargeArmAttack()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.arm, AttackTextContext.whenCharging));
    }

    void DoArmAttack()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterArmAttack));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.arm, AttackTextContext.beforeAttack));
    }

    public void AfterArmAttack()
    {
        if (GameManager.gameManager.hero.inventory.Contains(Items.holyWater))
        {

        }
        else
        {
            //Death
        }

    }

    void ChargeEyeAttack()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.eye, AttackTextContext.whenCharging));
    }


    void DoEyeAttack()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(AfterEyeAttack));
        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.eye, AttackTextContext.beforeAttack));
    }

    public void AfterEyeAttack()
    {
        if (Eye.ailmentState == Ailment.blind)
        {
            //stuned
        }
        else
        {
            //hero will get killed

        }

    }

}
