using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class GameManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        CreatePlayer();
    }
    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate("Player",RandomPosition(5f),Quaternion.identity);
    }

    public Vector3 RandomPosition(float distance)
    {
        Vector3 direction = Random.insideUnitSphere;

        direction.Normalize();

        direction = direction * distance;

        direction.y = 0;

        return direction;
    }

    public void ExitGame()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        //먼저들어온 플레이어에게 방장을 넘김
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }

}
