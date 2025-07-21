using UnityEngine;

public class Spot2Target : MonoBehaviour
{
    private int cleanCount = 0;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Clean()
    {
        cleanCount++;

        // 알파값 줄이기
        float alpha = Mathf.Clamp01(1f - (cleanCount / 3f));
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }

        Debug.Log($"🧼 얼룩별로 닦은 횟수: {cleanCount}/3");

        if (cleanCount >= 3)
        {
            Destroy(gameObject);
        }
    }
}
