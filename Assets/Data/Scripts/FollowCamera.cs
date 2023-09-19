using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform followTarget = null;

    void Start()
    {
        if (followTarget != null)
        {
            // 초기 myDir를 설정합니다.
            myDir = transform.position - followTarget.position;
        }
    }

    Vector3 myDir = Vector3.zero;

    void Update()
    {
        if (followTarget != null)
        {
            // 카메라의 위치를 업데이트합니다.
            transform.position = followTarget.position + myDir;

            // 카메라가 항상 followTarget을 바라보지 않도록 주석 처리합니다.
            // transform.LookAt(followTarget);
        }
    }
}
