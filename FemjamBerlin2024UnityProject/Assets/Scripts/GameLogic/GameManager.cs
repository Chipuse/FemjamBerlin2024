using System.Collections.Generic;
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
    public List<ItemSlot> SlotList = new List<ItemSlot>();
    public GameObject cancelTargetMenu;
    public GameObject nextMenu;

    public void OpenBattleMenu()
    {
        battleMenu.gameObject.SetActive(true);
        cancelTargetMenu.gameObject.SetActive(false);
        nextMenu.gameObject.SetActive(false);
        itemMenu.gameObject.SetActive(false);
        disableTargeting();
    }

    public void OpenItemMenu()
    {
        battleMenu.gameObject.SetActive(false);
        cancelTargetMenu.gameObject.SetActive(false);
        nextMenu.gameObject.SetActive(false);

        itemMenu.gameObject.SetActive(true);
        disableTargeting();

        if (SlotList.Count <= 0)
        {
            //fetch slotscripts
            foreach (var item in itemMenu.GetComponentsInChildren<ItemSlot>())
            {
                SlotList.Add(item);
            }
        }
        for (int i = 0; i < SlotList.Count; i++)
        {

            SlotList[i].gameObject.SetActive(false);
            if (i < hero.inventory.Count)
            {
                SlotList[i].itemType = hero.inventory[i];
                SlotList[i].gameObject.SetActive(true);
            }
        }
    }

    public BaseItem chosenItem;
    public void OpenCancelMenu()
    {
        chosenItem = null;
        battleMenu.gameObject.SetActive(false);
        cancelTargetMenu.gameObject.SetActive(true);
        nextMenu.gameObject.SetActive(false);
        itemMenu.gameObject.SetActive(false);

        enableTargeting();

        if(chosenItem == null)
        {
            //basic attack was chosen
        }
    }

    public delegate void SimpleGameEvent();
    public SimpleGameEvent enableTargeting;
    public SimpleGameEvent disableTargeting;

    public void OnTargetChosen(ITargetable _target)
    {
        if(chosenItem != null)
        {
            chosenItem.OnUse(_target);
        }
        else
        {
            //basic attack of hero was selected
            hero.OnAttack(_target);
        }
        OpenNextMenu();
        disableTargeting();
    }

    public void OpenNextMenu()
    {
        disableTargeting();
        chosenItem = null;
        battleMenu.gameObject.SetActive(false);
        cancelTargetMenu.gameObject.SetActive(false);
        nextMenu.gameObject.SetActive(true);
        itemMenu.gameObject.SetActive(false);

        if (chosenItem == null)
        {
            //basic attack was chosen
        }
    }

    public void OnNextButton()
    {
        //ToDo Have to figure out how to navigate through events with animations and stuff....


        if (onNextButtonQueue != null)
        {
            onNextButtonQueue();
            onNextButtonQueue = null;
        }
    }
    public SimpleGameEvent onNextButtonQueue;

    public Inputmode inputMode = Inputmode.menu;


    void Update()
    {
        
    }

    public TextboxText OnHeroDeathText;
    public void OnHeroDeath()
    {
        //display Herodeath Text and potential to reload "savestate"/scene
        Debug.Log("Hero died");
    }
}
