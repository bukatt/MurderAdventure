using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class PlayerControllerCustom : NetworkBehaviour
{
    private Vector2 moveDirection;

    private float lookDirection;

    private float lookDirection_p;

    private Vector2 moveDirection_p;

    [SerializeField]
    private Rigidbody2D myRB;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float sensitivity = 3f;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!hasAuthority) { return; }
        GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>().m_Follow = transform;
        GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>().m_LookAt = transform;
        gameManager.GameStateChanged += OnGameStateChanged;
    }

    [ClientCallback]
    void Update()
    { 
        if (!hasAuthority) { return; }
        lookDirection_p = lookDirection;
        moveDirection_p = moveDirection;
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        moveDirection = moveDirection.normalized;
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
    }

    [ClientCallback]
    private void FixedUpdate()
    {
        if (!hasAuthority) { return; }
        if(moveDirection != moveDirection_p || lookDirection != lookDirection_p)
            CmdMove(lookDirection, moveDirection); 
    }

    [Command]
    private void CmdMove(float lookDirection, Vector2 moveDirection)
    {
        //myRB.MoveRotation(Mathf.Lerp(myRB.rotation, lookDirection, sensitivity * Time.deltaTime));
        
        myRB.MoveRotation(lookDirection);
        //myRB.velocity = moveDirection * speed;
        myRB.MovePosition(myRB.position + moveDirection * speed * Time.deltaTime);
        if(moveDirection.x != 0)
        {
            Debug.Log(moveDirection);
        }
        
    }

    public void OnGameStateChanged(string gameState)
    {
        Debug.Log("going to enable controls");
        if (gameState == Constants.GameStates.inGame)
        {

        }
    }
}
