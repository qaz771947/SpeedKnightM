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

    [Header("�C�������ɶ����W�[�q")]
    public float addTime;

    [Header("�ɶ�����ֶq")]
    public float reduceTime;

    [Header("�ɶ�������")]
    public float time;

    [Header("�ɶ���")]
    public bool timesUp = false;

    [Header("�C���}�l")]
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
            else//��L�Ҧ�
            {

                if (GameStart)
                {
                    if (time <= 0) //�p�G�ɶ����p�󵥩�0,�ɶ���
                    {
                        timesUp = true;
                    }
                    else if (!inputController.playerBeenHit) //�_�h�~���֮ɶ����q
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

    private void Countdown()//�C�����0.1f;
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
