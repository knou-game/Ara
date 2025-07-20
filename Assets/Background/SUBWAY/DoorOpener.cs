using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Transform door;              // 문 오브젝트
    public Vector3 openOffset;          // 얼마나 이동할지 (상대적 위치)
    public float openSpeed = 2f;        // 열리는 속도

    private bool isOpen = false;
    private Vector3 closedPos;
    private Vector3 targetPos;

    void Start()
    {
        closedPos = door.position;
        targetPos = closedPos + openOffset;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isOpen && other.CompareTag("Player"))
        {
            isOpen = true;
            StartCoroutine(OpenDoor());
        }
    }

    System.Collections.IEnumerator OpenDoor()
    {
        while (Vector3.Distance(door.position, targetPos) > 0.01f)
        {
            door.position = Vector3.MoveTowards(door.position, targetPos, openSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
