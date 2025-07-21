using UnityEngine;

public class Spot4Target : MonoBehaviour
{
    public Vector3 closeOffset = new Vector3(-1f, 0, 0); // 왼쪽으로 1칸
    public float moveSpeed = 2f;

    private Vector3 targetPos;
    private bool isClosing = false;

    private void Start()
    {
        targetPos = transform.position + closeOffset;
    }

    public void Clean()
    {
        Debug.Log("📦 서랍장 닫힘 시작!");
        isClosing = true;
    }

    private void Update()
    {
        if (isClosing)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                Debug.Log("✅ 서랍장 닫힘 완료!");
                Destroy(gameObject);
            }
        }
    }
}
