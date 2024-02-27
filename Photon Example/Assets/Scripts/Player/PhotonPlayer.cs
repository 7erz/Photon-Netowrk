using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhotonPlayer : MonoBehaviourPunCallbacks//, IPunObservable
{
    [SerializeField]float speed;
    [SerializeField]float mouseX;
    [SerializeField]float rotateSpeed;
    //[SerializeField]float score;

    [SerializeField] Vector3 direction;
    [SerializeField] Camera temporaryCamera;
    


    private void Start()
    {
        if (photonView.IsMine)
        {
            //바깥에 있는 카메라를 메인 카메라로 설정, 이 카메라는 룸의 카메라를 끔.
            //플레이어는 1인칭으로 화면을 바라보게 됨
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            temporaryCamera.enabled = false;
            //리스너가 두개 이상 있으면 안됨
            GetComponentInChildren<AudioListener>().enabled = true;
        }
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Master Client");
        }

        if(photonView.IsMine == false)
        {
            return;
        }

        Movement();
        Rotation();
    }

    public void Movement()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");
        direction.Normalize();

        //TransformDirection : 자기가 바라보고 있는 방향으로 이동하는 함수
        transform.position += transform.TransformDirection(direction * speed * Time.deltaTime);

    }

    public void Rotation()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * rotateSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //로컬 오브젝트라면 쓰기 부분 실행
        if(stream.IsWriting)
        {
            //네크워크를 통해 데이터를 보냄
            stream.SendNext(score);
        }
        else    //원격 오브젝트라면 읽기 부분을 실행
        {
            try
            {
                score = (float)stream.ReceiveNext();
            }
            catch
            {

            }
            //네트워크를 통해서 데이터를 받음
            
        }

    }*/
}
