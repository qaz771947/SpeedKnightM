using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;

public class ScoreUpdate : MonoBehaviour
{
    [SerializeField] Text loseMessage;
    [SerializeField] ModeSupervisor modeSupervisor;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator SubmitScoreRoutine(int score, int leaderboardID)//�N���ƤW�Ǩ�LooLocker���Ʀ�]
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, score, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("���\�W�Ǧ��Z");

                done = true;
            }
            else
            {
                Debug.Log("����" + response.Error);

            }
        });
        yield return new WaitWhile(() => done == false);
    }
    public IEnumerator GetPlayerPlace(int leaderboardID)//���o��e���a���̰��ƦW
    {
        bool done = false;
        LootLockerSDKManager.GetMemberRank(leaderboardID, PlayerPrefs.GetString("PlayerID"), (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("���\������w��������");
                Debug.Log(response.rank);
                Debug.Log(response.score);

                loseMessage.text = "Rank:" + response.rank + "\n" + "Score:" + response.score;

                


                done = true;
            }
            else
            {
                Debug.Log("����" + response.Error);

                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
