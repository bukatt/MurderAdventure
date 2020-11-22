using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableNotLocal : MonoBehaviour
{
    public GamePlayer gamePlayer;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!gamePlayer.isLocalPlayer)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
