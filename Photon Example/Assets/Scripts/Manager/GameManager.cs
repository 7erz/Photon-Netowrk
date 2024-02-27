using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_InputField nicknameInputField;
    [SerializeField] GameObject nicknamePanel;
    [SerializeField] float timer;
    [SerializeField] int minute, second;
    private void Awake()
    {
        CreatePlayer();
        CheckNickname();
        
    }

    private void Start()
    {
        CheckPlayer();
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

        direction.y = 1;

        return direction;
    }

    public void Update()
    {

    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        //먼저들어온 플레이어에게 방장을 넘김
        PhotonNetwork.SetMasterClient(PhotonNetwork.PlayerList[0]);
    }

    public void CreateNickname()
    {
        PlayerPrefs.SetString("Nick Name", nicknameInputField.text);
        PhotonNetwork.NickName = nicknameInputField.text;
        nicknamePanel.SetActive(false);
    }

    public void CheckPlayer()
    {
        Room currentRoom = PhotonNetwork.CurrentRoom;
        if(currentRoom.PlayerCount >= currentRoom.MaxPlayers)
        {
            photonView.RPC("RemoteTimer",RpcTarget.All);
        }
    }

    [PunRPC]
    public void RemoteTimer()
    {
        StartCoroutine(StartTimer());
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

    public void Timer()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                timer = 0;
            }


            minute = (int)timer / 60;
            second = (int)timer % 60;

            Debug.Log(minute + " " + second);

            timerText.text = minute + " : " + second;
        }
    }

    IEnumerator StartTimer()
    {
        while (true)
        {
            timer -= Time.deltaTime;

            minute = (int)timer / 60;
            second = (int)timer % 60;


            timerText.text = minute.ToString("00") + " : " + second.ToString("00");

            yield return null;

            if(timer <= 0)
            {
                timer = 0;
                yield break;
            }
        }
    }

   
    

}
