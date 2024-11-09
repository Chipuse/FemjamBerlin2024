using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour, ITargetable
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public Ailment ailment = Ailment.neutral;

    public TextboxText OnAttackText;

    public GameObject healthbar;
    public void UpdateHealthBar()
    {
        healthbar.transform.localScale = new Vector3( (float)currentHealth/(float)maxHealth, 1f, 1f);
    }

    //inventory
    public List<Items> inventory = new List<Items>();

    void Start()
    {
        inventory.Add(Items.water);
        inventory.Add(Items.medEye);
        inventory.Add(Items.iceGem);
    }

    void Update()
    {
        
    }

    public void OnPray()
    {
        //Heal some damage, turn water into holy water
    }

    public void OnAttackButton()
    {
        GameManager.gameManager.OpenCancelMenu();
        GameManager.gameManager.chosenItem = null;
    }

    public void OnAttack(ITargetable _target)
    {
        // deal damage to target
        _target.AffectHealth(-1);
    }

    public void OnUseItem(BaseItem item)
    {

    }

    public GameObject spriteNeutral;
    public GameObject spritePetrified;
    public GameObject spriteWet;
    public GameObject spriteFrozen;
    public GameObject spriteBanded;
    public GameObject spriteDamaged;
    public GameObject spriteDead;
    public GameObject currentSprite;
    //potentially more stuff???

    public string NameOfTarget = "sillyHero";
    public string INameOfTarget { get { return NameOfTarget; } set { NameOfTarget = INameOfTarget; } }

    public void OnEnterNewAilment(Ailment _ailment)
    {
        spriteNeutral.SetActive(false);
        spritePetrified.SetActive(false);
        spriteWet.SetActive(false);
        spriteFrozen.SetActive(false);
        spriteBanded.SetActive(false);
        spriteDamaged.SetActive(false);
        spriteDead.SetActive(false);
        //activate vfx?
        switch (_ailment)
        {
            case Ailment.neutral:
                spriteNeutral.SetActive(true);
                currentSprite = spriteNeutral;
                break;
            case Ailment.wet:
                spriteWet.SetActive(true);
                currentSprite = spriteWet;
                break;
            case Ailment.banded:
                spriteBanded.SetActive(true);
                currentSprite = spriteBanded;
                break;
            case Ailment.petrified:
                spritePetrified.SetActive(true);
                currentSprite = spritePetrified;
                break;
            case Ailment.frozen:
                spriteFrozen.SetActive(true);
                currentSprite = spriteFrozen;
                break;
            case Ailment.dead:
                spriteDead.SetActive(true);
                currentSprite = spriteDead;
                break;
            default:
                break;
        }
    }

    void ITargetable.TargetedByWater()
    {
        throw new System.NotImplementedException();
    }

    void ITargetable.TargetedByHolyWater()
    {
        throw new System.NotImplementedException();
    }

    void ITargetable.TargetedByMedusasEye()
    {
        throw new System.NotImplementedException();
    }

    void ITargetable.TargetedByIceGem()
    {
        throw new System.NotImplementedException();
    }

    void ITargetable.TargetedByBandaid()
    {
        throw new System.NotImplementedException();
    }

    void ITargetable.TargetedByStinger()
    {
        throw new System.NotImplementedException();
    }

    void ITargetable.TargetedByBasicAttack()
    {
        throw new System.NotImplementedException();
    }

    void ITargetable.AffectHealth(int _deltaHealth)
    {
        StartCoroutine(AffectHealthCoroutine(_deltaHealth));        
    }

    private IEnumerator AffectHealthCoroutine(int _deltaHealth)
    {
        if(_deltaHealth < 0)
        {
            for (int i = 0; i < 10; i++)
            {
                currentSprite.SetActive(false);
                yield return new WaitForSeconds(GlobalVariables.blinkTime);
                currentSprite.SetActive(true);
                yield return new WaitForSeconds(GlobalVariables.blinkTime);
            }
        }
        else
        {
            //its a heal play animation! opr not idc
        }
        int targetHealth = currentHealth + _deltaHealth;
        while(currentHealth != targetHealth)
        {
            yield return new WaitForEndOfFrame();
            if (_deltaHealth < 0)
                currentHealth--;
            else
                currentHealth++;
        }
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            GameManager.gameManager.OnHeroDeath();
        }

    }

}
