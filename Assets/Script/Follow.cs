using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Player�� ���󰡱� ���ؼ� Ÿ�� ����
    public Transform target;

    // ī�޶� ������ �����ϱ� ���Ͽ� x, y, z�� ������ offset
    public Vector3 offSet; 
    void Update()
    {
        transform.position = target.position + offSet;
    }
}
