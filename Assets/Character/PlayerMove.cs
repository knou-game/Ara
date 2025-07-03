using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Animator anim;
    private float lastDirection = 1f; // 마지막 이동 방향 (처음엔 오른쪽)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        // 실제 이동 처리
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        // 방향 정보 기억 (입력 있을 때만 갱신)
        if (moveX != 0)
        {
            lastDirection = moveX;
            anim.SetFloat("Direction", moveX); // 걷기 애니메이션
        }
        else
        {
            // 키 안 누르면 마지막 방향 기준 Idle 애니메이션으로 유지
            anim.SetFloat("Direction", lastDirection * 0.01f); // -0.01 또는 0.01
        }
    }
}
