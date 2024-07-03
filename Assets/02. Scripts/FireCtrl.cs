using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����ü�� �޸��� ���� ������ �Ҵ� 
//������ Ŭ�������� ����
//���� ��� �� Ȱ���� �ʿ��� ��� ���� ����
[System.Serializable]
public struct PlayerSfx //����ü
{
    public AudioClip[] fire;
    public AudioClip[] reload;
}

public class FireCtrl : MonoBehaviour
{
    public enum WeaponType 
    {
        RIFLE = 0,
        SHOTGUN
            
    }
    //���� ������� ���� Ȯ�ο� ����
    public WeaponType currWeapon = WeaponType.RIFLE;

    public PlayerSfx playerSfx;
    AudioSource _audio;


    public GameObject bulletPrefab;
    public Transform firePos;
    public ParticleSystem cartridge; //ź�� ��ƼŬ�ý��ۿ� ����

    private ParticleSystem muzzleFlash;

    //ī�޶� ���� ��ũ��Ʈ ��������
    Shake shake;
    void Start()
    {
        //firePos�� �ڽĿ�����Ʈ �߿��� ParticleSystem ������Ʈ ȹ��
        //����Ƽ�� ��� ������Ʈ�� ��뼺�� ����
        //���� ��ũ��Ʈ�� ��ġ�� ������Ʈ�� ��ġ�� �ſ� �߿���
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
    }

    // Update is called once per frame
    void Update()
    {
        //GetMouseButton �� ���콺 ������ �ִ� ���� ���� �߻�
        //GetMouseButtonDown �� ������ ���� 1����
        //GetMouseButtonUp �� ���� ���� 1����
        //0�� ��Ŭ�� 1�� ��Ŭ��
        if (Input.GetMouseButtonDown(0))
        {
            //���� �޼ҵ� ȣ��
            Fire();
        }
    }

    void Fire()
    {
        StartCoroutine(shake.ShakeCamera());

        //�Ѿ� �������� �ѱ��� ��ġ�� ȸ������ ������ ���� ����
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        //��ƼŬ ���
        cartridge.Play();
        muzzleFlash.Play();
        //�߻� ���� ���
        FireSfx();
    }

    void FireSfx()
    {
        //���� ��� �ִ� ������ enum ���� int �� ��ȯ�ؼ� ����ϰ��� �ϴ� ������ ����� Ŭ���� ������
        var _sfx = playerSfx.fire[(int)currWeapon];
        //������ ������, 1(100%) �������� ���
        _audio.PlayOneShot(_sfx, 1f);
    }
}
