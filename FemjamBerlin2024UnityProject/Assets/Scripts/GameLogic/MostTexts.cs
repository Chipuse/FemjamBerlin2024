using UnityEngine;
using TMPro;

public class MostTexts : MonoBehaviour
{
    public static MostTexts mostTexts;

    private void Awake()
    {
        if (mostTexts == null)
        {
            mostTexts = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public itemText[] itemTexts;
    public attackText[] attackTexts;

    public TMP_Text textBox;


    public void FillTextBox(string _text)
    {
        if(textBox == null)
        {
            textBox = GameManager.gameManager.textBoxBox;
        }
        textBox.text = TextboxText.ParsedStatic(_text);
    }
    public void FillTextBox(TextboxText _text)
    {
        textBox.text = _text.Parsed();
    }

    public string FindText(Items _item, ItemTextContext _context)
    {
        for (int i = 0; i < itemTexts.Length; i++)
        {
            if (itemTexts[i].item == _item && itemTexts[i].context == _context)
                return itemTexts[i].text.Parsed();
        }

        return "placeholder";
    }

    public string FindText(BodyPartEnum _bodyPart, AttackTextContext _context)
    {
        for (int i = 0; i < attackTexts.Length; i++)
        {
            if (attackTexts[i].bodyPart == _bodyPart && attackTexts[i].context == _context)
                return attackTexts[i].text.Parsed();
        }

        return "placeholder";
    }

    //callbacks for different textboxes:

    //WaterItem Callbacks
/*    public void OnWaterUsedOnPlayer()
    {
        GameManager.gameManager.latestTarget.TargetedByHolyWater();
    }

    public void OnWaterEffectOnPlayer()
    {

    }

    public void OnWaterUsedOnBoss()
    {
        GameManager.gameManager.latestTarget.TargetedByHolyWater();
    }

    public void OnWaterEffectOnBoss()
    {

    }

    //HolyWaterItem Callbacks
    public void OnHolyWaterUsedOnPlayer()
    {

    }

    public void OnHolyWaterEffectOnPlayer()
    {

    }

    public void OnHolyWaterUsedOnBoss()
    {

    }

    public void OnHolyWaterEffectOnBoss()
    {

    }

    //MedEyeItem Callbacks
    public void OnMedEyeUsedOnPlayer()
    {

    }

    public void OnMedEyeEffectOnPlayer()
    {

    }

    public void OnMedEyeUsedOnBoss()
    {

    }

    public void OnMedEyeEffectOnBoss()
    {

    }

    //IceGemItem Callbacks
    public void OnIceGemUsedOnPlayer()
    {

    }

    public void OnIceGemEffectOnPlayer()
    {

    }

    public void OnIceGemUsedOnBoss()
    {

    }

    public void OnIceGemEffectOnBoss()
    {

    }

    //BandaidItem Callbacks
    public void OnBandaidUsedOnPlayer()
    {

    }

    public void OnBandaidEffectOnPlayer()
    {

    }

    public void OnBandaidUsedOnBoss()
    {

    }

    public void OnBandaidEffectOnBoss()
    {

    }

    //StingerItem Callbacks
    public void OnStingerUsedOnPlayer()
    {

    }

    public void OnStingerEffectOnPlayer()
    {

    }

    public void OnStingerUsedOnBoss()
    {

    }

    public void OnStingerEffectOnBoss()
    {

    }

    //Backpack Callbacks
    public void OnBackpackUsedOnPlayer()
    {

    }

    public void OnBackpackEffectOnPlayer()
    {

    }

    public void OnBackpackUsedOnBoss()
    {

    }

    public void OnBackpackEffectOnBoss()
    {

    }
    */
}
[System.Serializable]
public struct itemText
{
    public Items item;
    public ItemTextContext context;
    public TextboxText text;
}

[System.Serializable]
public struct attackText
{
    public BodyPartEnum bodyPart;
    public AttackTextContext context;
    public TextboxText text;
}
