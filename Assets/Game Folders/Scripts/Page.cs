using UnityEngine;

public class Page : MonoBehaviour
{
    public PageName pageName;

    protected virtual void Start()
    {

    }

    public void ClosePage()
    {
        gameObject.SetActive(false);
    }
}
