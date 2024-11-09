using UnityEngine;

public class TargetSelectButton : MonoBehaviour
{
    public BodyPart bodyPart;
    public void OnButtonPress()
    {
        GameManager.gameManager.OnTargetChosen(bodyPart);
    }
}
