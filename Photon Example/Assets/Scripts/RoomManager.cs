using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;  //어느 서버에 접속했을 때 이벤트를 호출하는 라이브러리

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomCreateButton;
    public Transform roomParentTransform;       //룸이 생성될 위치
    public TMP_InputField roomNameInputField;
    public TMP_InputField roomPersonlInputField;

    Dictionary<string,RoomInfo> roomDictionary = new Dictionary<string, RoomInfo>();
    void Update()
    {
        //작성한 글이 없으면 버튼 활성화 하지 못하게
        if(roomNameInputField.text.Length > 0 && roomPersonlInputField.text.Length > 0)
        {
            roomCreateButton.interactable = true;
        }
        else
        {
            roomCreateButton.interactable = false;
        }
    }

    //Room에 입장한 후 호출되는 콜백 함수
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void InstantiateRoom()
    {
        //룸 옵션 설정
        RoomOptions roomOptions = new RoomOptions();

        //최대 접속자 수 설정
        roomOptions.MaxPlayers = byte.Parse(roomPersonlInputField.text);
        
        //룸의 오픈 여부 설정
        roomOptions.IsOpen = true;

        //로비에서 룸 목록을 노출시킬 지 설정함
        roomOptions.IsVisible = true;

        //룸을 생성하는 함수
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
    }

    //해당 로비에 방 목록의 변경 사항이 있으면 호출(추가,  삭제, 참가)되는 함수
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //1. 룸 삭제
        RemoveRoom();
        //2. 룸 업데이트
        UpdateRoom(roomList);
        //3. 룸 생성
        RoomCreate();


    }

    //룸 삭제
    public void RemoveRoom()
    {
        foreach(Transform roomTransform in roomParentTransform)
        {
            //예를 들어 방 3개가 있다면 3개가 삭제 됨
            Destroy(roomTransform.gameObject);
        }
    }

    public void UpdateRoom(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            //해당 이름이 roomDictionary의 KEY 값으로 설정되어 있다면?
            if (roomDictionary.ContainsKey(roomList[i].Name))
            {
                //RemoveFromList : (true) 룸에서 삭제되었을 때 사용하는 프로퍼티
                if (roomList[i].RemovedFromList)
                {
                    roomDictionary.Remove(roomList[i].Name);
                }
                else
                {
                    roomDictionary[roomList[i].Name] = roomList[i];
                }
            }
            else
            {
                roomDictionary[roomList[i].Name] = roomList[i];
            }
        }
    }

    public void RoomCreate()
    {
        //1. roomDictionary에 여러개의 Values의 값이 들어 있다면 RoomInfo에 넣어줌
        foreach (RoomInfo roominfo in roomDictionary.Values)
        {
            //2. room 게임 오브젝트 생성
            GameObject room = Instantiate(Resources.Load<GameObject>("Room"));
            //3. room의 부모를 설정함
            room.transform.SetParent(roomParentTransform);
            //4. room에 대한 정보를 입력
            room.GetComponent<Information>().RoomData(roominfo.Name, roominfo.PlayerCount, roominfo.MaxPlayers);
        }
    }
}
