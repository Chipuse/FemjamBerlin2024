using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 9999;
    public int currentHealth = 9999;

    public GameObject healthbar;
    public void UpdateHealthBar()
    {
        healthbar.transform.localScale = new Vector3((float)currentHealth / (float)maxHealth, 1f, 1f);

    }


    public GameObject wing;
    public GameObject mouth;
    public GameObject tail;
    public GameObject arm;
    public GameObject eye;

    public BodyPartEnum currentBodyPart = BodyPartEnum.wing;

    void Start()
    {
        
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
