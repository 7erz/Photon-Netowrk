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
        nickName.text = photonView.Owner.NickName;
    }

    private void Update()
    {
        transform.forward = playerCamera.transform.forward;
    }

    private void OnBecameVisible()
    {
        
    }

}
