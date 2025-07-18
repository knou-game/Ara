using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 moveInput;
    private Vector2 lastMoveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // 1. 방향 입력 받기
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(moveX, moveY).normalized;

        // 2. 애니메이터 파라미터 업데이트
        anim.SetFloat("MoveX", moveInput.x);
        anim.SetFloat("MoveY", moveInput.y);

        bool isMoving = moveInput != Vector2.zero;
        anim.SetBool("IsMoving", isMoving);

        // 3. 마지막 이동 방향 기억해서 Idle 방향 유지
        if (isMoving)
            lastMoveDir = moveInput;
        else
        {
            // 멈췄을 때 Blend Tree가 idle 쪽으로 가도록 마지막 방향으로 살짝 유지
            anim.SetFloat("MoveX", lastMoveDir.x * 0.5f);
            anim.SetFloat("MoveY", lastMoveDir.y * 0.5f);
        }

    }

    void FixedUpdate()
    {
        // 4. 물리 이동 처리
        rb.velocity = moveInput * moveSpeed;
    }
}
