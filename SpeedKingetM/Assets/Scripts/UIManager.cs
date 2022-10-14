using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    [SerializeField] Text scores;
    [SerializeField] TargetSpawn targetSpawn;
    [SerializeField] TimeBar timeBar;
    [SerializeField] InputController inputController;
    [SerializeField] PlayerController playerController;
    [SerializeField] ScoreUpdate scoreUpdate;
    [SerializeField] GameObject loseMessageBox;
    [SerializeField] ModeSupervisor modeSupervisor;
    [SerializeField] bool hasDoneScore = false;
    private void Awake()
    {
        StartCoroutine(GetScript());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scores.text = targetSpawn.destroyTargetSum.ToString();
        if (inputController == null || playerController == null)
        {
            StartCoroutine(GetScript());
        }
        else 
        {
            if (playerController.isDead && !hasDoneScore)
            {
                FindObjectOfType<AudioManager>().Play("Defeat");
                loseMessageBox.SetActive(true);
                if (modeSupervisor.modeNum == 1)
                {
                    StartCoroutine(scoreUpdate.GetPlayerPlace(5317));
                    StartCoroutine(scoreUpdate.SubmitScoreRoutine(targetSpawn.destroyTargetSum, 5317));
                }
                else if (modeSupervisor.modeNum == 2)
                {
                    StartCoroutine(scoreUpdate.GetPlayerPlace(5555));
                    StartCoroutine(scoreUpdate.SubmitScoreRoutine(targetSpawn.destroyTargetSum, 5555));
                }
                hasDoneScore = true;

            }
        }
        

    }

    public void Restart()
    {
        StartCoroutine(ResetGame());
    }

    IEnumerator ResetGame()
    {
        bool done = false;
        timeBar.ResetDeata();
        targetSpawn.ResetData();
        ResetTarget();
        inputController.ResetData();
        yield return new WaitForSeconds(0.1f);
        playerController.ResetData();
        loseMessageBox.SetActive(false);
        hasDoneScore = false;
        done = true;
        yield return new WaitWhile(() => done == false);
    }

    IEnumerator GetScript()
    {
        yield return new WaitForSeconds(0.1f);
        inputController = FindObjectOfType<InputController>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void ResetTarget()
    {
        if (targetSpawn.transform.GetChild(0).gameObject.transform.GetChild(0).tag != "O") 
        {
            Destroy(targetSpawn.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(0).gameObject);
            targetSpawn.transform.GetChild(0).gameObject.transform.GetChild(0).tag = "O";
        }
        
    }

    public void Leave()
    {
        SceneManager.LoadScene("Menu");
    }


}
