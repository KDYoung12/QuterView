using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 이동 속도
    public float speed;

    // 속도를 조절하기 위한 h, v
    float hAxis;
    float vAxis;

    // 걷기 구분
    bool wDown;

    // 움직이기 위한 벡터
    Vector3 moveVec;

    // 애니메이션 설정하기 위함
    Animator anim;

    void Awake()
    {
        // 초기화
        anim = GetComponentInChildren<Animator>();
    }
    void Start()
    {

    }
    void Update()
    {
        // Player move
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        wDown = Input.GetButton("Walk");

        // normalized : 방향 값이 1로 보정된 벡터
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        // 걸을 때와 뛸 때 구분하기 (삼항연산자로 구분)
        // 삼항연산자 : bool 형태 조건 ? true 일 때 값 : false 일 때 값
        transform.position += moveVec * speed * (wDown ? 0.4f : 1f) * Time.deltaTime;

        // SetBool() 함수로 파라메터 값을 설정하기
        // moveVec가 0이 아니면 true
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

        // LookAt() : 지정된 벡터를 향해서 회전시켜주는 함수
        // 플레이어가 가는 방향을 바로보도록 설정
        transform.LookAt(transform.position + moveVec);
    }
}
