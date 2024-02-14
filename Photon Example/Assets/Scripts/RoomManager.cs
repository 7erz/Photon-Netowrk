using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;  //��� ������ �������� �� �̺�Ʈ�� ȣ���ϴ� ���̺귯��

public class RoomManager : MonoBehaviourPunCallbacks
{
    public Button roomCreateButton;
    public Transform roomParentTransform;
    public TMP_InputField roomNameInputField;
    public TMP_InputField roomPersonlInputField;

    Dictionary<string,RoomInfo> roomDictionary = new Dictionary<string, RoomInfo>();
    void Update()
    {
        //�ۼ��� ���� ������ ��ư Ȱ��ȭ ���� ���ϰ�
        if(roomNameInputField.text.Length > 0 && roomPersonlInputField.text.Length > 0)
        {
            roomCreateButton.interactable = true;
        }
        else
        {
            roomCreateButton.interactable = false;
        }
    }

    //Room�� ������ �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Photon Game");
    }

    public void InstantiateRoom()
    {
        //�� �ɼ� ����
        RoomOptions roomOptions = new RoomOptions();

        //�ִ� ������ �� ����
        roomOptions.MaxPlayers = byte.Parse(roomPersonlInputField.text);
        
        //���� ���� ���� ����
        roomOptions.IsOpen = true;

        //�κ񿡼� �� ����� �����ų �� ������
        roomOptions.IsVisible = true;

        //���� �����ϴ� �Լ�
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions);
    }

    //�ش� �κ� �� ����� ���� ������ ������ ȣ��(�߰�,  ����, ����)�Ǵ� �Լ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //1. �� ����

        //2. �� ������Ʈ

        //3. �� ����


    }

    //�� ����
    public void RemoveRoom()
    {
        foreach(Transform roomTransform in roomParentTransform)
        {
            //���� ��� �� 3���� �ִٸ� 3���� ���� ��
            Destroy(roomTransform.gameObject);
        }
    }

    public void UpdateRoom(List<RoomInfo> roomList)
    {
        for(int i = 0; i < roomList.Count; i++)
        {
            //�ش� �̸��� roomDictionary�� KEY ������ �����Ǿ� �ִٸ�?
            if (roomDictionary.ContainsKey(roomList[i].Name))
            {
                //RemoveFromList : (true) �뿡�� �����Ǿ��� �� ����ϴ� ������Ƽ
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
}