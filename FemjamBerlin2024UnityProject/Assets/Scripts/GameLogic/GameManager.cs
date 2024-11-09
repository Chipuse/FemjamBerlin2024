using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    private void Awake()
    {
        if(gameManager == null || gameManager != this)
        {
            gameManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    //BattleManagment

    public Enemy enemy;
    public Hero hero;


    //Menumanagement

    public GameObject battleMenu;
    public GameObject itemMenu;
    public GameObject cancelTargetMenu;

    public Inputmode inputMode = Inputmode.menu;


    void Update()
    {
        
    }
}
