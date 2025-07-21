using UnityEngine;

public class PlayerSpotCleaner : MonoBehaviour
{
    private SpotTarget currentSpot = null;
    private Spot2Target currentSpot2 = null;
    private Spot3Target currentSpot3 = null;
    private Spot4Target currentSpot4 = null;

    void Update()
    {
        if (currentSpot != null && Input.GetKeyDown(KeyCode.Space))
        {
            currentSpot.Clean();
            currentSpot = null;
        }

        if (currentSpot2 != null && Input.GetKeyDown(KeyCode.Space))
        {
            currentSpot2.Clean(); // 닦는 중이라 null 처리 안 함
        }

        if (currentSpot3 != null && Input.GetKeyDown(KeyCode.Space))
        {
            currentSpot3.Clean();
            currentSpot3 = null;
        }

        if (currentSpot4 != null && Input.GetKeyDown(KeyCode.Space))
        {
            currentSpot4.Clean();
            currentSpot4 = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spot"))
        {
            currentSpot = other.GetComponent<SpotTarget>();
        }
        if (other.CompareTag("Spot2"))
        {
            currentSpot2 = other.GetComponent<Spot2Target>();
        }
        if (other.CompareTag("Spot3"))
        {
            currentSpot3 = other.GetComponent<Spot3Target>();
        }
        if (other.CompareTag("Spot4"))
        {
            currentSpot4 = other.GetComponent<Spot4Target>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Spot") && currentSpot != null && other.gameObject == currentSpot.gameObject)
        {
            currentSpot = null;
        }
        if (other.CompareTag("Spot2") && currentSpot2 != null && other.gameObject == currentSpot2.gameObject)
        {
            currentSpot2 = null;
        }
        if (other.CompareTag("Spot3") && currentSpot3 != null && other.gameObject == currentSpot3.gameObject)
        {
            currentSpot3 = null;
        }
        if (other.CompareTag("Spot4") && currentSpot4 != null && other.gameObject == currentSpot4.gameObject)
        {
            currentSpot4 = null;
        }
    }
}
