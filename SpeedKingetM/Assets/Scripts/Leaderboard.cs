using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{

    [Header("��mTable")]
    [SerializeField] Transform rowsParent;

    [Header("��mRow �w�s����")]
    [SerializeField] GameObject rowPrefabs;

    private void Awake()
    {

        
    }

    public void GetTopTen(int num) //����e�Q�Ʀ�]
    {
        StartCoroutine(FetchTopHighScoresRoutine(num));
    }  

    public IEnumerator FetchTopHighScoresRoutine(int leaderboardID)//���oLooLocker���Ʀ�]
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
                Debug.Log("����" + response.Error);

                done = true;
            }


        });
        yield return new WaitWhile(() => done == false);
    }
}
