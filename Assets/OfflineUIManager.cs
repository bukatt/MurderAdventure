using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class OfflineUIManager : MonoBehaviour
{
    private NetworkManagerCustom networkManager;
    public TMP_InputField ipInput;
    // Start is called before the first frame update
    void Start()
    {
        networkManager = NetworkManager.singleton as NetworkManagerCustom;
    }

    public void JoinClicked()
    {
        string ip = ipInput.text;
        if(ip != null)
        {
            networkManager.networkAddress = "localhost";
            networkManager.StartClient();
        }
    }

    public void HostClicked()
    {
        networkManager.StartHost();
    }
}
