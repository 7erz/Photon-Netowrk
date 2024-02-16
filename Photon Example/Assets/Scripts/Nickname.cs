using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class Nickname : MonoBehaviourPunCallbacks
{
    [SerializeField] TextMeshProUGUI nickName;

    private void Start()
    {
        nickName.text = photonView.Owner.NickName;
    }


}
