using UnityEngine;

public class SpotTarget : MonoBehaviour
{
    public void Clean()
    {
        Debug.Log("🧽 정리됨!");
        Destroy(gameObject);
    }
}
