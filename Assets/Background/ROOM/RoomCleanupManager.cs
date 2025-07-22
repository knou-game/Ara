using UnityEngine;

public class RoomCleanupManager : MonoBehaviour
{
    public static bool isRoomCleaned = false;

    void Update()
    {
        if (isRoomCleaned) return;

        bool noTrash = GameObject.FindGameObjectsWithTag("Trash").Length == 0;

        bool noSpots =
            GameObject.FindGameObjectsWithTag("Spot").Length == 0 &&
            GameObject.FindGameObjectsWithTag("Spot2").Length == 0 &&
            GameObject.FindGameObjectsWithTag("Spot3").Length == 0 &&
            GameObject.FindGameObjectsWithTag("Spot4").Length == 0;

        if (noTrash && noSpots)
        {
            isRoomCleaned = true;
            Debug.Log("🎉 방이 완전히 치워졌습니다!");
        }
    }
}
