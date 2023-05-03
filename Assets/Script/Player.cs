using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �̵� �ӵ�
    public float speed;

    // �ӵ��� �����ϱ� ���� h, v
    float hAxis;
    float vAxis;

    // �ȱ� ����
    bool wDown;

    // �����̱� ���� ����
    Vector3 moveVec;

    // �ִϸ��̼� �����ϱ� ����
    Animator anim;

    void Awake()
    {
        // �ʱ�ȭ
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

        // normalized : ���� ���� 1�� ������ ����
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        // ���� ���� �� �� �����ϱ� (���׿����ڷ� ����)
        // ���׿����� : bool ���� ���� ? true �� �� �� : false �� �� ��
        transform.position += moveVec * speed * (wDown ? 0.4f : 1f) * Time.deltaTime;

        // SetBool() �Լ��� �Ķ���� ���� �����ϱ�
        // moveVec�� 0�� �ƴϸ� true
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);

        // LookAt() : ������ ���͸� ���ؼ� ȸ�������ִ� �Լ�
        // �÷��̾ ���� ������ �ٷκ����� ����
        transform.LookAt(transform.position + moveVec);
    }
}
