using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class ChatList : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnChatUpdate))]
    public string chat = "";

    public event Action<string> ChatUpdated;

    public void OnChatUpdate(string oldChat, string newChat)
    {
        chat = newChat;
        ChatUpdated?.Invoke(chat);
    }

    [Server]
    public void AddMessage(string playerName, string message)
    {
        string newMessage = "[" + playerName + "]: " + message + "\n";
        chat = chat + newMessage;
    }
}
