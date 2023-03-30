using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


//[RequireComponent(typeof(VideoPlayer))]// 스크립트를 붙힐때 추가를 해주는기능 기획자한테 좋은기능 (기능보장)

public class Screen : MonoBehaviour
{
   //비디오플레이어 컴포넌트 추가 제어

    private VideoPlayer vp = null;
    private void Awake()
    {
        vp = GetComponent<VideoPlayer>();
        if(vp == null)
        {
            //인스펙터 창에 비디오플레이어가 없는상태
            vp = gameObject.AddComponent<VideoPlayer>(); //컴포넌트를 추가하기 추가할때는 gameObject에 붙힘
            //어떤오브젝트에 붙힐지 선언하는것이기 때문
            //만약 비디오플레이어가 이미추가되어있다면 null이 나옴
            //런타임때 생기고 사라지는 특징!

        }
        vp.playOnAwake = true;
        vp.isLooping = true;
    }
    public void SetVideoClip(VideoClip _clip)
    {
        vp.clip = _clip;

    }
    public void Play()
    {

        if (vp.isPlaying) return; //이미 플레이중 예외처리
        vp.Play();
    }
    public void Pause()
    {
        if(vp.isPaused) return;
        vp.Pause();
    }
}
