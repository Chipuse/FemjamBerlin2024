using UnityEngine;
public class ItemSlot : MonoBehaviour
{
    public Items itemType;
    public BaseItem item;

    public void OnItemUsed()
    {
        GameManager.gameManager.OpenCancelMenu();
        GameManager.gameManager.chosenItem = item;
    }

    public void OnEnable()
    {
        //Set Images and Set 
        switch (itemType)
        {
            case Items.water:
                item = new WaterItem();
                break;
            case Items.holyWater:
                item = new HolyWaterItem();
                break;
            case Items.medEye:
                item = new MedEyeItem();
                break;
            case Items.iceGem:
                item = new IceGemItem();
                break;
            case Items.bandaid:
                item = new BandaidItem();
                break;
            case Items.stinger:
                item = new StingerItem();
                break;
            default:
                break;
        }
    }
}
