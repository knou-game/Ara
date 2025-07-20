using UnityEngine;

public class TrashDumpZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var handler = other.GetComponent<PlayerTrashHandler>();
            if (handler != null)
            {
                handler.SetCanDump(true);
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
                handler.SetCanDump(false);
            }
        }
    }
}
