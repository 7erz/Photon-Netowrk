using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    [SerializeField] Camera cam;
    [SerializeField] Transform gunFireTransform;
    [SerializeField] int attack;
    [SerializeField] LayerMask layerMask;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Launch();
    }

    public void Launch()
    {
        if (Input.GetButtonDown("Fire1"))   
        {
            ray = new Ray(cam.transform.position, cam.transform.forward.normalized);
            Debug.DrawRay(gunFireTransform.position, ray.direction * 100f, Color.red, 2f);
            if (Physics.Raycast(gunFireTransform.position, ray.direction, out hit, Mathf.Infinity, layerMask))
            {
                PhotonView photonView = hit.collider.GetComponent<PhotonView>();
                photonView.GetComponent<Metalon>().Health -= attack;

                if(photonView.GetComponent<Metalon>().Health <= 0)
                {
                    if (photonView.IsMine)
                    {
                        PhotonNetwork.Destroy(hit.collider.gameObject);
                    }
                }
            }
        }

    }
}
