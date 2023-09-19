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
            // �ʱ� myDir�� �����մϴ�.
            myDir = transform.position - followTarget.position;
        }
    }

    Vector3 myDir = Vector3.zero;

    void Update()
    {
        if (followTarget != null)
        {
            // ī�޶��� ��ġ�� ������Ʈ�մϴ�.
            transform.position = followTarget.position + myDir;

            // ī�޶� �׻� followTarget�� �ٶ��� �ʵ��� �ּ� ó���մϴ�.
            // transform.LookAt(followTarget);
        }
    }
}
