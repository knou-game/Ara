using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrashHandler : MonoBehaviour
{
    public float followSpeed = 5f;
    public float followDistance = 0.5f;

    public bool canCollectTrash = false;
    private bool isInTrashArea = false;
    private bool canDump = false;

    private List<Transform> collectedTrash = new List<Transform>();

    public Transform trashCanTargetPoint; // 쓰레기통 안쪽 목표 위치

    void Update()
    {
        if (isInTrashArea && Input.GetKeyDown(KeyCode.Space))
        {
            canCollectTrash = true;
            Debug.Log("✅ 쓰레기 줍기 모드 활성화됨!");
        }

        if (canDump && Input.GetKeyDown(KeyCode.Space))
        {
            DumpTrash();
            Debug.Log("🗑️ 쓰레기 버렸음!");
        }

        FollowTrash();
    }

    void FollowTrash()
    {
        Vector3 targetPos = transform.position;

        foreach (Transform trash in collectedTrash)
        {
            if (trash == null) continue;

            float dist = Vector3.Distance(trash.position, targetPos);
            if (dist > followDistance)
            {
                trash.position = Vector3.MoveTowards(trash.position, targetPos, followSpeed * Time.deltaTime);
            }

            targetPos = trash.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canCollectTrash && other.CompareTag("Trash"))
        {
            if (!collectedTrash.Contains(other.transform))
            {
                collectedTrash.Add(other.transform);
                other.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    public void DumpTrash()
    {
        foreach (Transform trash in collectedTrash)
        {
            if (trash != null)
            {
                StartCoroutine(MoveAndDestroy(trash));
            }
        }

        collectedTrash.Clear();
    }

    private IEnumerator MoveAndDestroy(Transform trash)
    {
        if (trash == null) yield break;

        float duration = 0.5f;
        float elapsed = 0f;
        Vector3 startPos = trash.position;
        Vector3 endPos = trashCanTargetPoint.position;

        while (elapsed < duration)
        {
            if (trash == null) yield break;

            trash.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        if (trash != null)
        {
            trash.position = endPos;
            Destroy(trash.gameObject);
        }
    }

    public void SetCanUseTrashArea(bool inArea)
    {
        isInTrashArea = inArea;
    }

    public void SetCanDump(bool value)
    {
        canDump = value;
    }
}
