using UnityEngine;

public class TargetSelectButton : MonoBehaviour
{
    public ITargetable targetable;
    private void Start()
    {
        GameManager.gameManager.enableTargeting += EnableTarget;
        GameManager.gameManager.disableTargeting += DisableTarget;
        DisableTarget();
    }
    public void OnButtonPress()
    {
        if(targetable == null)
        {
            targetable = transform.parent.GetComponent<BodyPart>();
        }
        if (targetable == null)
        {
            targetable = GameManager.gameManager.hero;
        }
        GameManager.gameManager.OnTargetChosen(targetable);
    }

    public void DisableTarget()
    {
        transform.gameObject.SetActive(false);
    }

    public void EnableTarget()
    {
        transform.gameObject.SetActive(true);

    }
}
