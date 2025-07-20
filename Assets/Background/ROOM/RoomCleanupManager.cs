using UnityEngine;

public class RoomCleanupManager : MonoBehaviour
{
    private bool isRoomCleaned = false;

    void Update()
    {
        if (isRoomCleaned) return;

        // 조건 1: 남아 있는 쓰레기가 없음 (Tag로 확인)
        bool noTrash = GameObject.FindGameObjectsWithTag("Trash").Length == 0;

        // 조건 2: Spot 종류 모두 제거됨
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
