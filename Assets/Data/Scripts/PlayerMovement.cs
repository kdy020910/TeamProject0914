using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : SystemProPerty
{
    public float rotSpeed = 10f;
    public float speed = 4f;

    protected Vector3 moveVec;
    PlayerTrigger playerTriggr;

    private void Start()
    {
        playerTriggr = GetComponentInChildren<PlayerTrigger>();
    }
    void Update()
    {
        if (!myAnim.GetBool("IsDontMove") && !playerTriggr.DiyUI.activeSelf) // �ִϸ��̼� ���� �� �������� ����
        {
            // �̵�
            float hAxis = Input.GetAxisRaw("Horizontal");
            float vAxis = Input.GetAxisRaw("Vertical");

            moveVec = new Vector3(hAxis, 0, vAxis).normalized;

            transform.position += moveVec * speed * Time.deltaTime;
            
            // ȸ��
            if (moveVec != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveVec, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
            }

            myAnim.SetBool("IsMoving", moveVec != Vector3.zero);
        }
    }  
}