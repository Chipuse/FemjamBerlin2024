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

    public void CheckAilments()
    {
        if (Wing.ailmentState == Ailment.frozen || Wing.ailmentState == Ailment.petrified)
        {
            Wing.OnEnterNewAilment(Ailment.neutral);
        }
        if (Mouth.ailmentState == Ailment.frozen || Mouth.ailmentState == Ailment.petrified)
        {
            Mouth.OnEnterNewAilment(Ailment.neutral);
        }
        if (Tail.ailmentState == Ailment.frozen || Tail.ailmentState == Ailment.petrified)
        {
            Tail.OnEnterNewAilment(Ailment.neutral);
        }
        if (Arm.ailmentState == Ailment.frozen || Arm.ailmentState == Ailment.petrified)
        {
            Arm.OnEnterNewAilment(Ailment.neutral);
        }
        if (Eye.ailmentState == Ailment.frozen || Eye.ailmentState == Ailment.petrified)
        {
            Eye.OnEnterNewAilment(Ailment.neutral);
        }
    }

    public void CheckBodyParts()
    {
        CoroutineCheckBodyparts();
    }
    public void CoroutineCheckBodyparts()
    {

        if(bodyPartCount == 1)
        {
            //eye opens!
            Eye.isOpenEye = false;
            eye.SetActive(true); 
            GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(ChargeEyeAttack));
            MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.eye, AttackTextContext.whenCharging));
            //MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.eye, AttackTextContext.whenDead));
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
        GameManager.gameManager.latestAttacker=Wing;
    }

    void Update()
    {
        
    }

    public void checkForNextBodyPart()
    {
        if(
            !wing.activeInHierarchy &&
            !mouth.activeInHierarchy &&
            !arm.activeInHierarchy &&
            !tail.activeInHierarchy &&
            !eye.activeInHierarchy)
        {
            //enter victory scene
            return;
        }
        switch (currentBodyPart)
        {
            case BodyPartEnum.wing:
                if (wing.activeInHierarchy)
                    return;
                currentBodyPart = BodyPartEnum.mouth;
                GameManager.gameManager.latestAttacker=Mouth;
                checkForNextBodyPart();
                break;
            case BodyPartEnum.mouth:
                if (mouth.activeInHierarchy)
                    return;
                currentBodyPart = BodyPartEnum.arm;
                GameManager.gameManager.latestAttacker=Arm;
                checkForNextBodyPart();
                break;
            case BodyPartEnum.arm:
                if (arm.activeInHierarchy)
                    return;
                currentBodyPart = BodyPartEnum.tail;
                GameManager.gameManager.latestAttacker=Tail;
                checkForNextBodyPart();
                break;
            case BodyPartEnum.tail:
                if (tail.activeInHierarchy)
                    return;
                currentBodyPart = BodyPartEnum.eye;
                GameManager.gameManager.latestAttacker=Eye;
                checkForNextBodyPart();
                break;
            case BodyPartEnum.eye:
                if (eye.activeInHierarchy)
                    return;
                currentBodyPart = BodyPartEnum.wing;
                GameManager.gameManager.latestAttacker=Wing;
                checkForNextBodyPart();
                break;
            default:
                break;
        }
    }

    public void OnEnemyTurn()
    {
        switch (currentBodyPart)
        {
            case BodyPartEnum.wing:
                if (wing.activeInHierarchy)
                    DoWingAttack();
                currentBodyPart++;
                break;
            case BodyPartEnum.mouth:
                if (mouth.activeInHierarchy)
                    DoMouthAttack();
                currentBodyPart++;
                break;
            case BodyPartEnum.tail:
                if (mouth.activeInHierarchy)
                    DoTailAttack();
                currentBodyPart++;
                break;
            case BodyPartEnum.arm:
                if (mouth.activeInHierarchy)
                    DoArmAttack();
                currentBodyPart++;
                break;
            case BodyPartEnum.eye:
                if (eye.activeInHierarchy)
                {
                    DoEyeAttack();
                }
                else
                    currentBodyPart = BodyPartEnum.wing;
                break;
            default:
                break;
        }

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
            MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.wing, AttackTextContext.specialCase));
            GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(ChargeMouthAttack));
        }
        else
        {
            switch (Wing.ailmentState)
            {
                case Ailment.banded:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.wing, AttackTextContext.whenBanded));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                case Ailment.petrified:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.wing, AttackTextContext.whenPetrified));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                case Ailment.frozen:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.wing, AttackTextContext.whenFrozen));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                default:
                    
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.wing, AttackTextContext.afterAttack));

                    //hero will get killed
                    if (GameManager.gameManager.hero.ailment == Ailment.petrified)
                    {
                        GameManager.gameManager.hero.AffectHealth(-1); 
                        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(ChargeMouthAttack));
                    }
                    else
                        GameManager.gameManager.hero.AffectHealth(-9999);
                    break;
            }
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
            GameManager.gameManager.hero.OnEnterNewAilment(Ailment.wet);
            MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.mouth, AttackTextContext.specialCase));
            GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(ChargeTailAttack));
            if (Eye.ailmentState == Ailment.banded)
            {
                Eye.KillThisBodyPart();
            }
            if (Wing.ailmentState == Ailment.banded)
            {
                Wing.KillThisBodyPart();
            }
            if (Mouth.ailmentState == Ailment.banded)
            {
                Mouth.KillThisBodyPart();
            }
            if (Arm.ailmentState == Ailment.banded)
            {
                Arm.KillThisBodyPart();
            }
            if (Tail.ailmentState == Ailment.banded)
            {
                Tail.KillThisBodyPart();
            }
        }
        else
        {
            switch (Mouth.ailmentState)
            {
                case Ailment.banded:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.mouth, AttackTextContext.whenBanded));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                case Ailment.petrified:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.mouth, AttackTextContext.whenPetrified));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                case Ailment.frozen:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.mouth, AttackTextContext.whenFrozen));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                default:
                    if (Eye.ailmentState == Ailment.banded)
                    {
                        Eye.KillThisBodyPart();
                    }
                    if (Wing.ailmentState == Ailment.banded)
                    {
                        Wing.KillThisBodyPart();
                    }
                    if (Mouth.ailmentState == Ailment.banded)
                    {
                        Mouth.KillThisBodyPart();
                    }
                    if (Arm.ailmentState == Ailment.banded)
                    {
                        Arm.KillThisBodyPart();
                    }
                    if (Tail.ailmentState == Ailment.banded)
                    {
                        Tail.KillThisBodyPart();
                    }
                    switch (GameManager.gameManager.hero.ailment)
                    {
                        case Ailment.petrified:
                            MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.mouth, AttackTextContext.afterAttack));
                            GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(ChargeTailAttack));
                            //hero will get killed
                            GameManager.gameManager.hero.AffectHealth(-1);
                            break;
                        case Ailment.frozen:
                            break;
                        default:
                            MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.mouth, AttackTextContext.afterAttack));

                            //hero will get killed
                            GameManager.gameManager.hero.AffectHealth(-9999);
                            break;
                    }
                    break;
            }
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
            MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.tail, AttackTextContext.specialCase));
            Tail.KillThisBodyPart();
            GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(ChargeArmAttack));
            GameManager.gameManager.hero.inventory.Add(Items.stinger);
        }
        else
        {

            //death
            switch(Tail.ailmentState)
            {
                case Ailment.banded:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.tail, AttackTextContext.whenBanded));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                case Ailment.petrified:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.tail, AttackTextContext.whenPetrified));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                case Ailment.frozen:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.tail, AttackTextContext.whenFrozen));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                default:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.tail, AttackTextContext.afterAttack));

                    //hero will get killed
                    if (GameManager.gameManager.hero.ailment == Ailment.petrified)
                    {
                        GameManager.gameManager.hero.AffectHealth(-1);
                        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(ChargeArmAttack));
                    }
                    else
                        GameManager.gameManager.hero.AffectHealth(-9999);
                    break;
            }
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
        if (GameManager.gameManager.hero.holyWet)
        {
            Arm.KillThisBodyPart();
            MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.arm, AttackTextContext.specialCase));
            GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(ChargeEyeAttack));
        }
        else
        {
            switch (Arm.ailmentState)
            {
                case Ailment.banded:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.arm, AttackTextContext.whenBanded));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                case Ailment.petrified:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.arm, AttackTextContext.whenPetrified));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                case Ailment.frozen:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.arm, AttackTextContext.whenFrozen));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                default:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.arm, AttackTextContext.afterAttack));

                    //hero will get killed
                    if (GameManager.gameManager.hero.ailment == Ailment.petrified)
                    {
                        GameManager.gameManager.hero.AffectHealth(-1);
                        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    }
                    else
                        GameManager.gameManager.hero.AffectHealth(-9999);
                    break;
            }
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
        if (Eye.isOpenEye == false)
        {
            Eye.isOpenEye = true;
            GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.ChargeEyeAttack));
            MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.eye, AttackTextContext.eyeJustOpened));

        }
        else if (Eye.ailmentState == Ailment.blind)
        {
            //stuned
            GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
            MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.eye, AttackTextContext.specialCase));
        }
        else
        {
            switch (Eye.ailmentState)
            {                
                case Ailment.banded:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.eye, AttackTextContext.whenBanded));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                case Ailment.petrified:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.eye, AttackTextContext.whenPetrified));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                case Ailment.frozen:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.eye, AttackTextContext.whenFrozen));
                    GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.OpenBattleMenu));
                    break;
                default:
                    MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.eye, AttackTextContext.afterAttack));

                    //hero will get killed
                    if (GameManager.gameManager.hero.ailment == Ailment.petrified)
                    {
                        GameManager.gameManager.hero.AffectHealth(-1);
                        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(ChargeEyeAttack));
                        MostTexts.mostTexts.FillTextBox(MostTexts.mostTexts.FindText(BodyPartEnum.eye, AttackTextContext.whenCharging));
                    }
                    else
                        GameManager.gameManager.hero.AffectHealth(-9999);
                    break;
            }
            
        }

    }

}
