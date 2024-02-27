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
            //�ٱ��� �ִ� ī�޶� ���� ī�޶�� ����, �� ī�޶�� ���� ī�޶� ��.
            //�÷��̾�� 1��Ī���� ȭ���� �ٶ󺸰� ��
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            temporaryCamera.enabled = false;
            //�����ʰ� �ΰ� �̻� ������ �ȵ�
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

        //TransformDirection : �ڱⰡ �ٶ󺸰� �ִ� �������� �̵��ϴ� �Լ�
        transform.position += transform.TransformDirection(direction * speed * Time.deltaTime);

    }

    public void Rotation()
    {
        mouseX += Input.GetAxisRaw("Mouse X") * rotateSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    /*public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //���� ������Ʈ��� ���� �κ� ����
        if(stream.IsWriting)
        {
            //��ũ��ũ�� ���� �����͸� ����
            stream.SendNext(score);
        }
        else    //���� ������Ʈ��� �б� �κ��� ����
        {
            try
            {
                score = (float)stream.ReceiveNext();
            }
            catch
            {

            }
            //��Ʈ��ũ�� ���ؼ� �����͸� ����
            
        }

    }*/
}
