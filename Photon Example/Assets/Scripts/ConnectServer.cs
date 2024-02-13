using Photon.Pun;
using Photon.Realtime;
using PlayFab.MultiplayerModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectServer : MonoBehaviourPunCallbacks      //SDK에서 지원 하기 때문에 상속 바꿔야 함
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
            //로비까지 들어온다면 true값으로 변환됨
            //룸으로 들어오는 것이기 때문
            canvasLobby.gameObject.SetActive(false);

        }
    }

    public void SelectServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //로비에 접속 했을 때
    public override void OnJoinedLobby()    
    {
        canvasRoom.sortingOrder = 1;
    }

    public override void OnConnectedToMaster()
    {
        //JoinLobby: 특정 로비를 생성하여 진입하는 방법
        //디폴트를 하면 트래픽을 많이 잡아먹음
        //로비 이름으로 접근함 (server.options)
        PhotonNetwork.JoinLobby(new TypedLobby(server.options[server.value].text, LobbyType.Default));
    }

}
