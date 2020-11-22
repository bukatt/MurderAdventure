using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;
using System;

public class PlayerControllerCustom : NetworkBehaviour
{
    #region Movement/Rotation
    private Vector2 moveDirection;

    private float lookDirection;

    private float lookDirection_p;

    private Vector2 moveDirection_p;
    #endregion

    [SerializeField]
    private Rigidbody2D myRB;

    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float sensitivity = 3f;

    [SerializeField]
    private float lookSensitivity = 3f;


    #region Controls
    [SerializeField]
    private KeyCode attackButton;

    [SerializeField]
    private KeyCode inspectButton;

    [SerializeField]
    private KeyCode changeViewButton;

    [SerializeField]
    private KeyCode toggleLantern;

    [SerializeField]
    private KeyCode toggleDoor;
    #endregion

    private GameManager gameManager;

    private PlayerWeapon playerWeapon;

    private GamePlayer gamePlayer;

    private ChangeCameraZoom changeCameraZoom;

    private ClueManager clueManager;

    private DoorOpenManager doorOpenManager;

    [SerializeField]
    private LayerMask mask;

    public GameObject weaponObject;

    private bool isDead = false;

    private float attackCounter = 0;

    private PlayerDeath playerDeath;

    public event Action<NetworkIdentity> InspectPressed;

    public GameObject lantern;

    public LightManager lanternLight;

    private bool enableControls = false;

    public Chat chat;

    public Transform leftHandTarget;

    public Transform rightHandTarget;

    private void OnDestroy()
    {
        playerDeath.playerKilled -= OnDeath;
        gameManager.GameStateChanged -= OnGameStateChanged;
    }

    public override void OnStartClient()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerWeapon = GetComponent<PlayerWeapon>();
        playerDeath = GetComponent<PlayerDeath>();
        playerDeath.playerKilled += OnDeath;
        gamePlayer = GetComponent<GamePlayer>();
        clueManager = GetComponent<ClueManager>();
        changeCameraZoom = GetComponent<ChangeCameraZoom>();
        doorOpenManager = GetComponent<DoorOpenManager>();
        base.OnStartClient();
        if (!isLocalPlayer) { return; }
        GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>().m_Follow = transform;
        GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>().m_LookAt = transform;
        gameManager.GameStateChanged += OnGameStateChanged;
    }

    [ClientCallback]
    void Update()
    {
        if (!hasAuthority || isDead || !enableControls || chat.chatting) { return; }
        lookDirection_p = lookDirection;
        moveDirection_p = moveDirection;
        //movement
        moveDirection.x = Input.GetAxisRaw("Horizontal");
        moveDirection.y = Input.GetAxisRaw("Vertical");
        moveDirection = moveDirection.normalized;
        //rotation
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        //attack
        if (attackCounter > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        else
        {
            attackCounter = 0;
        }

        if (Input.GetKeyDown(attackButton) && attackCounter <= 0)
        {
            CmdAttack();
            attackCounter = Constants.WeaponObjects.uiWeaponsDict[playerWeapon.equippedWeapon].attackRate;
        }

        if (Input.GetKeyDown(inspectButton))
        {
            InspectPressed?.Invoke(base.netIdentity);
            clueManager.CmdCheckClue();
        }

        if (Input.GetKeyDown(changeViewButton) && gamePlayer.role == Constants.Roles.murderer)
        {
            changeCameraZoom.toggleCameraZoom();
        }

        if (Input.GetKeyDown(toggleLantern))
        {
            CmdToggleLantern();
        }
       
        if (Input.GetKeyDown(toggleDoor))
        {
            CmdToggleDoor();
        }
        //RaycastHit2D _hit;
        //_hit = Physics2D.Raycast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position, 10, mask);
        ////var points = new Vector3[2];
        //lineOfSight.SetPosition(0,transform.position);
        //lineOfSight.SetPosition(1, _hit.transform.position);
        
    }

    #region lantern
    [Command]
    public void CmdToggleLantern()
    {
        lanternLight.ToggleLight();
    }

    [ClientRpc]
    private void RpcToggleLantern(bool lanternStatus)
    {
        lantern.SetActive(lanternStatus);
    }
    #endregion

    public void OnDeath(bool isDead)
    {
        if (isDead)
        {
            this.isDead = true;
        }
    }

    [Command]
    private void CmdToggleDoor()
    {
        doorOpenManager.ToggleDoor();
    }

    [ClientCallback]
    private void FixedUpdate()
    {

        if (!isLocalPlayer) { return; }

        float lookDirectionDifference = lookDirection - lookDirection_p;
        lookDirectionDifference = Mathf.Abs(lookDirectionDifference);
        //Debug.Log(lookDirection);
        if (moveDirection != moveDirection_p || lookDirectionDifference != 0)
        {
            CmdMove(Mathf.Lerp(lookDirection_p, lookDirection, sensitivity), moveDirection);
            //if (lookDirectionDifference >= lookSensitivity)
            //{
            //    CmdMove(Mathf.Lerp(lookDirection_p, lookDirection, sensitivity), moveDirection);
            //} else
            //{
            //    CmdMove(lookDirection_p, moveDirection);
            //}
            

            //if (lookDirectionDifference > .1)
            //{
            //    CmdMove(lookDirection, moveDirection);
            //} else
            //{
            //    CmdMove(lookDirection_p, moveDirection);
            //}
        }
    }

    [Command]
    private void CmdMove(float lookDirection, Vector2 moveDirection)
    {
        //myRB.MoveRotation(Mathf.Lerp(myRB.rotation, lookDirection, sensitivity * Time.deltaTime));

        // myRB.MoveRotation(lookDirection);
        //myRB.velocity = moveDirection * speed;
        //myRB.MovePosition(myRB.position + moveDirection * speed * Time.deltaTime);

        RpcMove(lookDirection, moveDirection);
    }

    [ClientRpc]
    public void RpcMove(float lookDirection, Vector2 moveDirection)
    {
        myRB.MoveRotation(lookDirection);
        myRB.MovePosition(myRB.position + moveDirection * speed * Time.deltaTime);
        //leftHandTarget.localPosition = new Vector2(.15f * Mathf.Cos(lookDirection), .15f * Mathf.Sin(lookDirection));
    }

    public void OnGameStateChanged(string gameState)
    {
        Debug.Log("going to enable controls");
        Debug.Log(netId);
        if (gameState == Constants.GameStates.inGame && isLocalPlayer)
        {
            enableControls = true;
        } else
        {
            enableControls = false;
        }
    }

    [Command]
    private void CmdAttack()
    {
        playerWeapon.RpcAttack();
        RaycastHit2D _hit;
        WeaponObject equippedWeapon = Constants.WeaponObjects.uiWeaponsDict[playerWeapon.equippedWeapon];
        _hit = Physics2D.Raycast(transform.position,transform.up, equippedWeapon.damageDistance, mask);

        if (_hit)
        {
           
            if (_hit.collider.tag == Constants.Tags.player)
            {
                _hit.collider.gameObject.GetComponent<PlayerDeath>().isDead = true;
                gameManager.CheckPlayerDeath();
                ///CmdPlayerHit(_hit.collider.gameObject.GetComponent<PlayerDeath>(), transform.name);
            }

            // We hit something, call the OnHit method on the server
            //RpcDoHitEffect(_hit.point, _hit.normal);
        }
    }

    [Command]
    void CmdOnHit(Vector3 _pos, Vector2 _normal)
    {
        RpcDoHitEffect(_pos, _normal);
    }

    [ClientRpc]
    void RpcDoHitEffect(Vector3 _pos, Vector2 _normal)
    {
        GameObject _hitEffect = (GameObject)Instantiate(Constants.WeaponObjects.uiWeaponsDict[playerWeapon.equippedWeapon].hitEffect,
            _pos, Quaternion.LookRotation(_normal));
    }

    //[Command]
    //void CmdPlayerHit(PlayerDeath gamePlayer, string _sourceID)
    //{
    //    //Debug.Log(_playerID + " has been shot.");

    //    gamePlayer.isDead = true;
    //}
}
