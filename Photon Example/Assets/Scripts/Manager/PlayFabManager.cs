using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using PlayFab.ClientModels;
using UnityEngine.UI;
using PlayFab;

public class PlayFabManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField email;
    [SerializeField] InputField password;

    public void Success(LoginResult result)
    {
        //������ �÷��̾ �̵��Ҷ� ������ ����� ���� ������ �̵��ϴ°�?
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = "1.0f";
        //���� �ڵ带 �����صα� ���� �� �ڵ带 ����� (�� �̵�)
        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void Success(RegisterPlayFabUserResult result)   //�Լ� �̸��� ����. �����ε�
    {
        Debug.Log(result.ToString()); 
        Debug.Log(result);
        Alarm.Show(result.ToString() + "ȸ������ �Ϸ�.", AlarmType.Alarm);
    }

    public void Failure(PlayFabError error) 
    {
        Alarm.Show(error.GenerateErrorReport(), AlarmType.Alarm);
    }

    public void SignUp()
    {
        //var �� ������ ���� ����
        var request = new RegisterPlayFabUserRequest
        {
            Email = email.text,
            Password = password.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request,Success,Failure);
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email.text,
            Password = password.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress(request,Success,Failure);
    }

}
