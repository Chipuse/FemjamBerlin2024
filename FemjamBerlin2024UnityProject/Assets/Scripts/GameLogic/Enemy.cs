using UnityEngine;

public class Enemy : MonoBehaviour
{
    //we want to start with 9999
    public int totalMaxHealth = 100;
    public int currentHealth = 100;

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
