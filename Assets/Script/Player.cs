using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 이동 속도
    public float speed;

    // 점프 파워
    public float jumpPower;

    // 속도를 조절하기 위한 h, v
    float hAxis;
    float vAxis;

    // 걷기 구분
    bool wDown;

    // 점프 구분
    bool jDown;

    // 점프 제약
    bool isJump;

    // 회피 제약
    bool isDodge;

    // 움직이기 위한 벡터
    Vector3 moveVec;

    // 움직이기 위한 벡터
    Vector3 DodgeVec;

    // 애니메이션 설정하기 위함
    Animator anim;

    // RigidBody
    Rigidbody rigid;
    void Awake()
    {
        // 초기화
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
    }
    void Start()
    {

    }
    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Dodge();
    }

    void GetInput()
    {
        // Player move
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");
        jDown = Input.GetButtonDown("Jump");
    }

    void Move()
    {
        // normalized : 방향 값이 1로 보정된 벡터
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = DodgeVec;

        // 걸을 때와 뛸 때 구분하기 (삼항연산자로 구분)
        // 삼항연산자 : bool 형태 조건 ? true 일 때 값 : false 일 때 값
        transform.position += moveVec * speed * (wDown ? 0.4f : 1f) * Time.deltaTime;

        // SetBool() 함수로 파라메터 값을 설정하기
        // moveVec가 0이 아니면 true
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    void Turn()
    {
        // LookAt() : 지정된 벡터를 향해서 회전시켜주는 함수
        // 플레이어가 가는 방향을 바로보도록 설정
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isDodge)
        {
            // AddForce() : 물리적인 힘을 가함
            // ForceMode.Impulse : 즉발적인 힘
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            // 점프 애니메이션
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");

            // 점프가 되면 true
            isJump = true;
        }
    }

    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isJump && !isDodge)
        {
            DodgeVec = moveVec;

            // 회피는 이동속도만 2배로 상승하도록 설정
            speed *= 2;

            // 회피 애니메이션
            anim.SetTrigger("doDodge");

            // 회피가 되면 true
            isDodge = true;

            // Invoke() : 시간차를 이용해 함수 호출
            Invoke("DodgeOut", 0.6f);
        }
    }

    void DodgeOut()
    {
        speed *= 0.5f;
        isDodge = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            anim.SetBool("isJump", false);
            isJump = false;
        }
    }
}
