using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using PlayFab.ClientModels;

public class Nickname : MonoBehaviourPunCallbacks
{
    GetAccountInfoResult getAccountInfoResult;

    [SerializeField] TextMeshProUGUI nickName;
    [SerializeField] Camera playerCamera;

    private void Start()
    {
        //Debug.Log(getAccountInfoResult.AccountInfo.TitleInfo.DisplayName);
        nickName.text = photonView.Owner.NickName;
        //nickName.text = PhotonNetwork.LocalPlayer.NickName;
    }

    private void Update()
    {
        transform.forward = playerCamera.transform.forward;
    }

    private void OnBecameVisible()
    {
        
    }

}
