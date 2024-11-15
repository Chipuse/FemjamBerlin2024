using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseItem : MonoBehaviour
{
    public string itemName;
    public Items itemType;

    public Image image;
    public Sprite sprite;
    //displayed while hovering in Item menu
    public TextboxText textboxText;

    public bool applyAilment;
    public Ailment ailment;
    public int damage;
    public int healPlayerAmount;

    public string descriptionText;
    public string usedOnPlayerText;
    public string effectOnPlayerText;
    public string usedOnBossText;
    public string effectOnBossText;


    void Start()
    {
        gameObject.GetComponent<Image>().sprite = sprite;
    }
    public void OnHover()
    {

    }
    public void OnSelected()
    {
        GameManager.gameManager.OpenCancelMenu();
        GameManager.gameManager.SaveLastText();
        MostTexts.mostTexts.FillTextBox(descriptionText);
        GameManager.gameManager.chosenItem = this;
    }
    public void OnTargetSelect()
    {

    }

    public virtual void OnUse()
    {
        GameManager.gameManager.hero.RemoveItem(itemType);

        if (GameManager.gameManager.latestTarget == (ITargetable) GameManager.gameManager.hero)
        {
            OnUseHero();
        }
        else
        {
            OnUseBoss();
        }
    }

    public virtual void OnUseHero()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(ItemEffectHero));
        MostTexts.mostTexts.FillTextBox(usedOnPlayerText);
        if (damage > 0)
            GameManager.gameManager.latestTarget.AffectHealth(-damage);
        if (healPlayerAmount > 0)
            GameManager.gameManager.latestTarget.AffectHealth(healPlayerAmount);

    }

    public virtual void OnUseBoss()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(ItemEffectBoss));
        MostTexts.mostTexts.FillTextBox(usedOnBossText);
        if (damage > 0)
            GameManager.gameManager.latestTarget.AffectHealth(-damage);

    }

    public virtual void ItemEffectHero()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(effectOnPlayerText);
        if (applyAilment)
            GameManager.gameManager.latestTarget.OnEnterNewAilment(ailment);
    }
    public virtual void ItemEffectBoss()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(GameManager.gameManager.enemy.OnEnemyTurn));
        MostTexts.mostTexts.FillTextBox(effectOnBossText);
        if (applyAilment)
            GameManager.gameManager.latestTarget.OnEnterNewAilment(ailment);
    }
}
