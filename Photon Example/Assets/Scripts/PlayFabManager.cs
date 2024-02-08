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
        //마스터 플레이어가 이동할때 나머지 사람도 같은 씬으로 이동하는가?
        PhotonNetwork.AutomaticallySyncScene = false;

        PhotonNetwork.GameVersion = "1.0f";
        //위의 코드를 설정해두기 위해 이 코드를 사용함 (씬 이동)
        PhotonNetwork.LoadLevel("Photon Lobby");
    }

    public void Success(RegisterPlayFabUserResult result)   //함수 이름이 같음. 오버로딩
    {
        Debug.Log(result.ToString()); 
        Debug.Log(result);
        Alarm.Show(result.ToString() + "회원가입 완료.", AlarmType.Alarm);
    }

    public void Failure(PlayFabError error) 
    {
        Alarm.Show(error.GenerateErrorReport(), AlarmType.Alarm);
    }

    public void SignUp()
    {
        //var 로 데이터 유추 가능
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
