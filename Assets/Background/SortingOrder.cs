using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortingOrderByY : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        int order = -(int)(transform.position.y * 100);
        sr.sortingOrder = order;

        // 디버깅 로그 (한 번만 보고 싶으면 주석 처리해도 돼)
        // Debug.Log($"{gameObject.name} - Y: {transform.position.y:F2} → Order: {order}");
    }
}
