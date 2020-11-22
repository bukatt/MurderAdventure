using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using System;

public class Chat : NetworkBehaviour
{
    public TMP_InputField chatInput;

    public TMP_Text chatText;

    public KeyCode chatHotKey;

    public KeyCode sendMessage;

    public string playerName;

    public bool chatting = false;

    private ChatList chatList;

    

    // Start is called before the first frame update
    void Start()
    {
        //Chat_UI chatUI = GameObject.FindGameObjectWithTag(Constants.Tags.chatUIManager).GetComponent<Chat_UI>();
        // chatText = chatUI.chatText;
        //chatInput = chatUI.chatInput;
        chatList = GameObject.FindGameObjectWithTag(Constants.Tags.chatManager).GetComponent<ChatList>();
        chatList.ChatUpdated += OnChatUpdate;
        playerName = GetComponent<GamePlayer>().playerName;
    }

    void Update()
    {
        if (Input.GetKeyDown(chatHotKey))
        {
            ToggleChatInput();
        }

        if (Input.GetKeyDown(sendMessage))
        {
            CmdSendMessage(chatInput.text, playerName);
            chatInput.text = null;
            chatInput.ActivateInputField();
        }
    }

    private void ToggleChatInput()
    {
        if (chatInput.IsInteractable())
        {
            chatInput.interactable = false;
            chatting = false;
        } else
        {
            chatting = true;
            chatInput.interactable = true;
            chatInput.ActivateInputField();
        }
    }

    [Command]
    private void CmdSendMessage(string message, string playerName)
    {
        chatList.AddMessage(playerName, message);
    }

    private void OnChatUpdate(string chatString)
    {
        Debug.Log("Updateing chate");
        chatText.text = chatString;
    }

    //private void OnDestroy()
    //{
    //    chatList.ChatUpdated -= OnChatUpdate;
    //}
}
