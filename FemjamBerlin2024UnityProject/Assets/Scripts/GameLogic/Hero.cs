using UnityEngine;

public class Hero : MonoBehaviour, ITargetable
{
    public int maxHealth = 100;
    public int currentHealth = 100;
    public Ailment ailment = Ailment.neutral;

    public 
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPray()
    {

    }

    public void OnAttack()
    {

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
}
