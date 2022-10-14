using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    [SerializeField] ModeSupervisor modeSupervisor;
    [SerializeField] InputController inputController;
    [SerializeField] PlayerController playerController;
    [SerializeField] Slider timeBar;

    [Header("每次擊打時間條增加量")]
    public float addTime;

    [Header("時間條減少量")]
    public float reduceTime;

    [Header("時間條長度")]
    public float time;

    [Header("時間到")]
    public bool timesUp = false;

    [Header("遊戲開始")]
    public bool GameStart = false;

    private void Awake()
    {
        StartCoroutine(GetScript());
    }

    private void Update()
    {
        if (inputController != null)
        {
            if (inputController.beenTouch)
            {
                GameStart = true;
            }
            if (modeSupervisor.modeNum == 1 || modeSupervisor.modeNum == 3)
            {
                if (!timesUp)
                {
                    if (inputController.beenTouch)
                    {
                        if (time + addTime <= 1f)
                        {
                            time += addTime;
                        }
                        else
                        {
                            time = 1f;
                        }

                    }
                    if (GameStart && !inputController.playerBeenHit)
                    {
                        Countdown();
                    }
                }



            }
            else//其他模式
            {

                if (GameStart)
                {
                    if (time <= 0) //如果時間條小於等於0,時間到
                    {
                        timesUp = true;
                    }
                    else if (!inputController.playerBeenHit) //否則繼續減少時間條量
                    {
                        Countdown();
                    }
                }

            }

            if (time <= 0)
            {
                timesUp = true;
            }
            timeBar.value = time;

            if (timesUp)
            {
                playerController.isDead = true;
            }

        }
        else
        {
            StartCoroutine(GetScript());
        }


    }

    private void Countdown()//每偵減少0.1f;
    {
        time -= reduceTime * Time.deltaTime;
    }

    IEnumerator GetScript()
    {
        yield return new WaitForSeconds(0.1f);
        inputController = FindObjectOfType<InputController>();
        playerController = FindObjectOfType<PlayerController>();
    }

    public void ResetDeata()
    {
        GameStart = false;
        timesUp = false;
        time = 1;
    }
}
