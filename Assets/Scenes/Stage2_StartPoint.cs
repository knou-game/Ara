using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        // Player가 이미 존재하는지 확인 (재시작 시 중복 방지용)
        if (GameObject.FindWithTag("Player") == null)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity);
        }
    }
}
