using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{

    [Header("放置Table")]
    [SerializeField] Transform rowsParent;

    [Header("放置Row 預製物件")]
    [SerializeField] GameObject rowPrefabs;

    private void Awake()
    {

        
    }

    public void GetTopTen(int num) //獲取前十排行榜
    {
        StartCoroutine(FetchTopHighScoresRoutine(num));
    }  

    public IEnumerator FetchTopHighScoresRoutine(int leaderboardID)//取得LooLocker的排行榜
    {
        bool done = false;
        LootLockerSDKManager.GetScoreList(leaderboardID, 10, 0, (response) =>
        {
            if (response.success)
            {
                foreach (Transform item in rowsParent)
                {
                    Destroy(item.gameObject);
                }



                LootLockerLeaderboardMember[] members = response.items;

                for (int i = 0; i < members.Length; i++)
                {
                    GameObject newItem = Instantiate(rowPrefabs, rowsParent);
                    Text[] texts = newItem.GetComponentsInChildren<Text>();
                    texts[0].text = members[i].rank.ToString();
                    if (members[i].player.name != "")
                    {
                        texts[1].text = members[i].player.name;
                    }
                    else
                    {
                        texts[1].text = members[i].player.id.ToString();
                    }
                    texts[2].text = members[i].score.ToString();

                }

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
