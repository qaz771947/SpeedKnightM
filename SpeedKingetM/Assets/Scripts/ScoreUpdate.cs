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
    public IEnumerator SubmitScoreRoutine(int score, int leaderboardID)//將分數上傳到LooLocker的排行榜
    {
        bool done = false;
        string playerID = PlayerPrefs.GetString("PlayerID");
        LootLockerSDKManager.SubmitScore(playerID, score, leaderboardID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("成功上傳成績");

                done = true;
            }
            else
            {
                Debug.Log("失敗" + response.Error);

            }
        });
        yield return new WaitWhile(() => done == false);
    }
    public IEnumerator GetPlayerPlace(int leaderboardID)//取得當前玩家的最高排名
    {
        bool done = false;
        LootLockerSDKManager.GetMemberRank(leaderboardID, PlayerPrefs.GetString("PlayerID"), (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("成功獲取指定成員分數");
                Debug.Log(response.rank);
                Debug.Log(response.score);

                loseMessage.text = "Rank:" + response.rank + "\n" + "Score:" + response.score;

                


                done = true;
            }
            else
            {
                Debug.Log("失敗" + response.Error);

                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }
}
