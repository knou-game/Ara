using UnityEngine;

public class Spot3Target : MonoBehaviour
{
    public GameObject objectToActivate;

    public void Clean()
    {
        Debug.Log("✨ 정리됨!");

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        Destroy(gameObject);
    }
}
