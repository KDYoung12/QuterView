using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Player를 따라가기 위해서 타겟 설정
    public Transform target;

    // 카메라 각도를 조절하기 위하여 x, y, z를 설정할 offset
    public Vector3 offSet; 
    void Update()
    {
        transform.position = target.position + offSet;
    }
}
