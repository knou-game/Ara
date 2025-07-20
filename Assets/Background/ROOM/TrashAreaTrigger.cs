using UnityEngine;

public class TrashAreaTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var handler = other.GetComponent<PlayerTrashHandler>();
            if (handler != null)
            {
                handler.SetCanUseTrashArea(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var handler = other.GetComponent<PlayerTrashHandler>();
            if (handler != null)
            {
                handler.SetCanUseTrashArea(false);
            }
        }
    }
}
