using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField nicknameInputField;
    [SerializeField] GameObject nicknamePanel;
    private void Awake()
    {
        CreatePlayer();
        CheckNickname();
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
        //�������� �÷��̾�� ������ �ѱ�
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }

    public void CreateNickname()
    {
        PlayerPrefs.SetString("Nick Name", nicknameInputField.text);
        PhotonNetwork.NickName = nicknameInputField.text;
        nicknamePanel.SetActive(false);
    }

    public void CheckNickname()
    {
        string nickname = PlayerPrefs.GetString("Nick Name");
        PhotonNetwork.NickName = nickname;
        if (string.IsNullOrEmpty(nickname))
        {
            nicknamePanel.SetActive(true);
        }
        else
        {
            nicknamePanel.SetActive(false);
        }
    }

    

}