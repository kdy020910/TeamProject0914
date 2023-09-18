using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SystemProPerty
{
    public float h, v;
    public float Speed = 4.0f;
    public GameObject[] tools;
    public bool[] hasTools;     // 플레이어에서 나중에 도구 개수만큼 갯수 생성해야 오류없이 잘 됨
    public float rotSpeed = 10f;

    public int coin;
    public int maxCoin;

    bool isHorizonMove;
    bool iDown;     //iDown키는 키보드 e키로 지정되어있음 iDown = Input.GetButton("Interaction");
    bool sDown1;
    bool sDown2;
    bool sDown3;

    RaycastHit rayHit;
    Rigidbody rigid;

    public Vector3 dirVec;
    protected Vector3 moveVec;

    public GameManager manager;
    GameObject scanObject;

    GameObject equipTool;
    int equipToolIndex = -1;
    PlayerTrigger playerTrigger;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerTrigger = GetComponentInChildren<PlayerTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Swap();
        Interaction();

        Lay();
        Move();
    }

    void GetInput()
    {
        //상태 변수이용 이동 제한
        h = manager.isAction ? 0 : Input.GetAxis("Horizontal");
        v = manager.isAction ? 0 : Input.GetAxis("Vertical");

        iDown = Input.GetButton("Interaction");
        //sDown1 = Input.GetButton("Swap1");
        //sDown2 = Input.GetButton("Swap2");
        //sDown3 = Input.GetButton("Swap3");
    }

    void Move()
    {
        moveVec = new Vector3(h, 0, v).normalized;
        //transform.Translate(moveVec * Speed * Time.deltaTime);
        transform.position += moveVec * Speed * Time.deltaTime;

        if (!myAnim.GetBool("IsDontMove") && !playerTrigger.DiyUI.activeSelf)
        {
            if (moveVec != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveVec, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
            }
            if (SetIsMoving() && h == 0 && v == 0)
            {
                myAnim.SetBool("IsMoving", false);
            }
        }
        //transform.Translate(new Vector3(h, 0, v) * Speed * Time.deltaTime);
    }
    public bool SetIsMoving()
    {
        myAnim.SetBool("IsMoving", true);
        return true; // 성공적으로 설정되었을 때 true를 반환
    }

    void Lay()
    {
        if (h > 0 && h <= 1 && v == 0)
        {
            dirVec = new Vector3(1, 0, 0);
        }
        else if (h < 0 && h >= -1 && v == 0)
        {
            dirVec = new Vector3(-1, 0, 0);
        }
        else if (v < 0 && v >= -1 && h == 0)
        {
            dirVec = new Vector3(0, 0, -1);
        }
        else if (v > 0 && v <= 1 && h == 0)
        {
            dirVec = new Vector3(0, 0, 1);
        }
        else if (h > 0 && h <= 1 && v > 0 && v <= 1)
        {
            dirVec = new Vector3(1, 0, 1);
        }
        else if (h < 0 && h >= -1 && v > 0 && v <= 1)
        {
            dirVec = new Vector3(-1, 0, 1);
        }
        else if (h < 0 && h >= -1 && v < 0 && v >= -1)
        {
            dirVec = new Vector3(-1, 0, -1);
        }
        else if (h > 0 && h <= 1 && v < 0 && v >= -1)
        {
            dirVec = new Vector3(1, 0, -1);
        }
        /*else
        {
            dirVec = Vector3.zero;
        }*/

        //transform.position += moveVec * Speed * Time.deltaTime;
        //Vector3 movVec = isHorizonMove ? new Vector3(h, 0, 0) : new Vector3(0, 0, v);
        rigid.velocity = moveVec * Speed * Time.deltaTime;

        //방향벡터 전체 지정

        if (Physics.Raycast(rigid.position, dirVec, out rayHit, 1.0f, LayerMask.GetMask("Object")))
        {
            Debug.DrawRay(rigid.position, dirVec * 1.0f, new Color(0, 1, 1));

            Ray();
        }
        else if (Physics.Raycast(rigid.position, dirVec, out rayHit, 1.0f, LayerMask.GetMask("Shop")))
        {
            Debug.DrawRay(rigid.position, dirVec * 1.0f, new Color(0, 1, 0));

            Ray();
        }
        else
        {
            Debug.DrawRay(rigid.position, dirVec * 1.0f, new Color(1, 0, 1));
        }
    }

    void Ray()
    {
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        }
        else
        {
            scanObject = null;
        }
    }

    void Swap()
    {
        if(sDown1 && (!hasTools[0] || equipToolIndex == 0))
        {
            return;
        }
        if (sDown2 && (!hasTools[1] || equipToolIndex == 1))
        {
            return;
        }
        if (sDown3 && (!hasTools[2] || equipToolIndex == 2))
        {
            return;
        }

        int toolIndex = -1;
        if (sDown1) toolIndex = 0;
        if (sDown2) toolIndex = 1;
        if (sDown3) toolIndex = 2;

        if (sDown1 || sDown2 || sDown3)
        {
            if(equipTool != null)
            {
                equipTool.SetActive(false);
            }
            equipToolIndex = toolIndex;
            equipTool = tools[toolIndex];
            equipTool.SetActive(true);
        }
    }

    void Interaction()
    {
        //iDown && 
        if (iDown && scanObject != null)
        {
            if(scanObject.tag == "Shop")
            {
                /*Shop shop = scanObject.GetComponent<Shop>();
                shop.Enter(this);*/
                ActiveShop(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z) && scanObject != null)
        {
            if (scanObject.tag == "Tool")
            {
                ItemPickUp items = scanObject.GetComponent<ItemPickUp>();
               // int toolIndex = items.value;
               // hasTools[toolIndex] = true;

                Destroy(scanObject);
            }
            /*else if (scanObject.tag == "Farming")
            {
                ItemPickUp items = scanObject.GetComponent<ItemPickUp>();
                int toolIndex = items.value;
                hasTools[toolIndex] = true;

                Destroy(scanObject);
            }*/
        }
        else if (Input.GetKeyDown(KeyCode.Space) && scanObject != null)
        {
            manager.Action(scanObject);
            //Debug.Log("nope");
        }
        scanObject = null;
    }

    public void ActiveShop(bool isOpen)
    {
        Shop shop = scanObject.GetComponent<Shop>();
        shop.ShopPanel.SetActive(isOpen);
        //shop.InvenPanel.SetActive(isOpen);
        shop.Enter(this);
    }

    private void OnTriggerStay(Collider other)
    {
        scanObject = other.gameObject;
        if (other.tag == "Tool")
        {
            Debug.Log($"도구 {scanObject.name}");
        }
        else if(other.tag == "Shop")
        {
            Debug.Log($"샵 {scanObject.name}");
        }
        else if (other.tag == "Farming")
        {
            Debug.Log($"농사 {scanObject.name}");
        }
        else
        {
            //Debug.Log("아직 해당 ㄴㄴ");
        } 
    }

    private void OnTriggerExit(Collider other)
    {
        scanObject = other.gameObject;
        /*if(scanObject != null)
        { print("스캔 되고있음"); }*/
        if (other.tag == "Tool")
        {
            scanObject = null;
        }
        else if (other.tag == "Shop")
        {
            Shop shop = scanObject.GetComponent<Shop>();
            shop.Exit();
            scanObject = null;
        }
        else if (other.tag == "Npc")
        {
            scanObject = null;
        }
        scanObject = null;
    }
}
