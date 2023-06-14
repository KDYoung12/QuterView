using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �̵� �ӵ�
    public float speed;

    // ���� �Ŀ�
    public float jumpPower;

    // �ӵ��� �����ϱ� ���� h, v
    float hAxis;
    float vAxis;

    // �ȱ� ����
    bool wDown;

    // ���� ����
    bool jDown;

    // ���� ����
    bool isJump;

    // ȸ�� ����
    bool isDodge;

    // �����̱� ���� ����
    Vector3 moveVec;

    // �����̱� ���� ����
    Vector3 DodgeVec;

    // �ִϸ��̼� �����ϱ� ����
    Animator anim;

    // RigidBody
    Rigidbody rigid;
    void Awake()
    {
        // �ʱ�ȭ
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
        // normalized : ���� ���� 1�� ������ ����
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;

        if (isDodge)
            moveVec = DodgeVec;

        // ���� ���� �� �� �����ϱ� (���׿����ڷ� ����)
        // ���׿����� : bool ���� ���� ? true �� �� �� : false �� �� ��
        transform.position += moveVec * speed * (wDown ? 0.4f : 1f) * Time.deltaTime;

        // SetBool() �Լ��� �Ķ���� ���� �����ϱ�
        // moveVec�� 0�� �ƴϸ� true
        anim.SetBool("isRun", moveVec != Vector3.zero);
        anim.SetBool("isWalk", wDown);
    }

    void Turn()
    {
        // LookAt() : ������ ���͸� ���ؼ� ȸ�������ִ� �Լ�
        // �÷��̾ ���� ������ �ٷκ����� ����
        transform.LookAt(transform.position + moveVec);
    }

    void Jump()
    {
        if (jDown && moveVec == Vector3.zero && !isJump && !isDodge)
        {
            // AddForce() : �������� ���� ����
            // ForceMode.Impulse : ������� ��
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

            // ���� �ִϸ��̼�
            anim.SetBool("isJump", true);
            anim.SetTrigger("doJump");

            // ������ �Ǹ� true
            isJump = true;
        }
    }

    void Dodge()
    {
        if (jDown && moveVec != Vector3.zero && !isJump && !isDodge)
        {
            DodgeVec = moveVec;

            // ȸ�Ǵ� �̵��ӵ��� 2��� ����ϵ��� ����
            speed *= 2;

            // ȸ�� �ִϸ��̼�
            anim.SetTrigger("doDodge");

            // ȸ�ǰ� �Ǹ� true
            isDodge = true;

            // Invoke() : �ð����� �̿��� �Լ� ȣ��
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
