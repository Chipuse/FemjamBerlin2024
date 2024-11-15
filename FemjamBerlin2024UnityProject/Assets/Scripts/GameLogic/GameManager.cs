using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    private void Awake()
    {
        if (gameManager == null || gameManager != this)
        {
            gameManager = this;
        }
        else
        {
            Destroy(this);
        }
    }


    //BattleManagment

    public ITargetable latestTarget;

    public ITargetable latestAttacker;

    public Enemy enemy;
    public Hero hero;


    //Menumanagement

    public TMP_Text textBoxBox;
    public GameObject battleMenu;
    public GameObject itemMenu;
    public List<ItemSlot> SlotList = new List<ItemSlot>();
    public GameObject cancelTargetMenu;
    public GameObject nextMenu;

    private string lastText="";

    public List<BaseItem> itemList = new List<BaseItem>();
    public Dictionary<Items, BaseItem> itemDic = new Dictionary<Items, BaseItem>();

     private void ConvertItemsToDictionary(){
        foreach(BaseItem i in itemList){
            itemDic.Add( i.itemType,i);

        }
    }

    void Start(){
                ConvertItemsToDictionary();

    }

    public void OpenBattleMenu()
    {
        RecoverLastText();

        if (hero.ailment == Ailment.frozen || hero.ailment == Ailment.petrified)
        {
            hero.OnEnterNewAilment(Ailment.neutral);
        }
        enemy.CheckAilments();

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
        
        print("deactivated ");
        foreach(BaseItem i in itemList){
            i.gameObject.SetActive(false);
            

        }

        foreach(Items i in hero.inventory){
            BaseItem cItem;
            itemDic.TryGetValue(i,out cItem);
            cItem?.gameObject.SetActive(true);
        }
/*
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
        */
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

        if (chosenItem == null)
        {
            //basic attack was chosen
        }
    }

    public void SaveLastText(){
        lastText=textBoxBox.text;
    }
    public void RecoverLastText(){
        if(lastText!=""){
        textBoxBox.text=lastText;
        lastText="";
        }
    }


    public delegate void SimpleGameEvent();
    public SimpleGameEvent enableTargeting;
    public SimpleGameEvent disableTargeting;

    public void OnTargetChosen(ITargetable _target)
    {
        latestTarget = _target;
        if (chosenItem != null)
        {
            chosenItem.OnUse();
        }
        else
        {
            //basic attack of hero was selected
           // hero.OnAttack(_target);
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




        textBoxActive = false;
    }

    public bool textBoxActive = false;
    public IEnumerator TextBoxClickCallback(Action callbackFunc)
    {
        OpenNextMenu();
        textBoxActive = true;
        while (textBoxActive)
        {
            yield return new WaitForEndOfFrame();
        }
        if (callbackFunc != null)
        {
            callbackFunc();
        }
    }

    public Inputmode inputMode = Inputmode.menu;


    void Update()
    {

    }

    public TextboxText OnHeroDeathText;
    public void OnHeroDeath()
    {
        GameManager.gameManager.StartCoroutine(GameManager.gameManager.TextBoxClickCallback(RestartGame));
        //display Herodeath Text and potential to reload "savestate"/scene
    }

    public void NextTurn()
    {
        enemy.TurnOver();
        StartCoroutine(TextBoxClickCallback(OpenBattleMenu));

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Hero died");
    }

    public void WinGame()
    {
        SceneManager.LoadScene(2);

        Debug.Log("Hero won");
    }
}
