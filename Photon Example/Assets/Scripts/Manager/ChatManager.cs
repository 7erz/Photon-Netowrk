using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public InputField inputField;
    public Transform chatContentTrans;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SendChat();
    }

    public void SendChat()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inputField.text.Length == 0)
            {
                return;
            }
            string chatting = PhotonNetwork.NickName + " : " + inputField.text;

            //photonView.RPC
            //�Ű����� 1 : ���� ��ų �Լ��� �̸�
            //�Ű����� 2 : ȣ���� �Լ��� ���� �� �ִ� ��� ����
            //�Ű����� 3 : ������ �Լ��� �Ű�����
            photonView.RPC("Chatting", RpcTarget.All, chatting);
            //photonView.RPC("ChattingTMPro", RpcTarget.All, chatting);
        }
    }

    public void SendChattButton()
    {
        if (inputField.text.Length == 0)
        {
            return;
        }
        string chatting = PhotonNetwork.NickName + " : " + inputField.text;

        //photonView.RPC("Chatting", RpcTarget.All, chatting);
        photonView.RPC("ChattingTMPro", RpcTarget.All, chatting);
    }

    [PunRPC]
    public void Chatting(string message)
    {
        GameObject content = Instantiate(Resources.Load<GameObject>("String"));

        content.GetComponent<Text>().text = message;

        content.transform.SetParent(chatContentTrans);

        inputField.ActivateInputField();

        inputField.text = "";
    }




    [PunRPC]
    public void ChattingTMPro(string message)
    {
        GameObject content = Instantiate(Resources.Load<GameObject>("String"));

        content.GetComponent<TMP_Text>().text = message;

        content.transform.SetParent(chatContentTrans);

        inputField.ActivateInputField();

        inputField.text = "";
    }
}
