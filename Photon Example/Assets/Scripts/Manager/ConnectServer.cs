using Photon.Pun;
using Photon.Realtime;
using PlayFab.MultiplayerModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectServer : MonoBehaviourPunCallbacks      //SDK���� ���� �ϱ� ������ ��� �ٲ�� ��
{
    [SerializeField] Canvas canvasRoom;
    [SerializeField] Canvas canvasLobby;
    [SerializeField] TMP_Dropdown server;

    private void Awake()
    {
        server.options[0].text = "Asia";
        server.options[1].text = "EUROPE";
        server.options[2].text = "America";

        if(PhotonNetwork.IsConnected)
        {
            //�κ���� ���´ٸ� true������ ��ȯ��
            //������ ������ ���̱� ����
            canvasLobby.gameObject.SetActive(false);

        }
    }

    public void SelectServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //�κ� ���� ���� ��
    public override void OnJoinedLobby()    
    {
        canvasRoom.sortingOrder = 1;
    }

    public override void OnConnectedToMaster()
    {
        //JoinLobby: Ư�� �κ� �����Ͽ� �����ϴ� ���
        //����Ʈ�� �ϸ� Ʈ������ ���� ��Ƹ���
        //�κ� �̸����� ������ (server.options)
        PhotonNetwork.JoinLobby(new TypedLobby(server.options[server.value].text, LobbyType.Default));
    }

}
