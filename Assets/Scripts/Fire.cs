using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class Fire : MonoBehaviour
{
    public Transform firePos;
    public GameObject bulletPrefab;

    private ParticleSystem muzzleFlash;

    private PhotonView pv;

    private bool isMouseClick => Input.GetMouseButtonDown(0);

    void Start()
    {
        pv = GetComponent<PhotonView>();
        muzzleFlash = firePos.Find("MuzzleFlash").GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (pv.IsMine && isMouseClick)
        {
            FireBullet();
            pv.RPC("FireBullet", RpcTarget.Others, null);
        }
    }

    [PunRPC]
    private void FireBullet()
    {
        Debug.Log(muzzleFlash);
        if (!muzzleFlash.isPlaying) muzzleFlash.Play(true);

        GameObject bullet = Instantiate(bulletPrefab,
                                      firePos.position,
                                      firePos.rotation);
    }
}
