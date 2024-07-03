using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//구조체는 메모리의 스택 영역에 할당 
//성능이 클래스보다 좋음
//빠른 계산 및 활용이 필요할 경우 쓰면 좋음
[System.Serializable]
public struct PlayerSfx //구조체
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
    //현재 사용중인 무기 확인용 변수
    public WeaponType currWeapon = WeaponType.RIFLE;

    public PlayerSfx playerSfx;
    AudioSource _audio;


    public GameObject bulletPrefab;
    public Transform firePos;
    public ParticleSystem cartridge; //탄피 파티클시스템용 변수

    private ParticleSystem muzzleFlash;

    //카메라 흔드는 스크립트 가져오기
    Shake shake;
    void Start()
    {
        //firePos의 자식오브젝트 중에서 ParticleSystem 컴포넌트 획득
        //유니티의 모든 오브젝트는 상대성을 지님
        //따라서 스크립트의 위치나 오브젝트의 위치가 매우 중요함
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
        shake = GameObject.Find("CameraRig").GetComponent<Shake>();
    }

    // Update is called once per frame
    void Update()
    {
        //GetMouseButton 은 마우스 누르고 있는 동안 지속 발생
        //GetMouseButtonDown 은 누르는 순간 1번만
        //GetMouseButtonUp 은 때는 순간 1번만
        //0은 좌클릭 1은 우클릭
        if (Input.GetMouseButtonDown(0))
        {
            //공격 메소드 호출
            Fire();
        }
    }

    void Fire()
    {
        StartCoroutine(shake.ShakeCamera());

        //총알 프리팹을 총구의 위치와 회전값을 가지고 동적 생성
        Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        //파티클 재생
        cartridge.Play();
        muzzleFlash.Play();
        //발사 사운드 재생
        FireSfx();
    }

    void FireSfx()
    {
        //현재 들고 있는 무기의 enum 값을 int 로 변환해서 재생하고자 하는 무기의 오디오 클립을 가져옴
        var _sfx = playerSfx.fire[(int)currWeapon];
        //지정된 음원을, 1(100%) 볼륨으로 재생
        _audio.PlayOneShot(_sfx, 1f);
    }
}
