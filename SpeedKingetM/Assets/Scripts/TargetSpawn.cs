using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawn : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPositionSet;
    [SerializeField] List<GameObject> targetPrefabs;
    private bool hasDoFirastSpawn = false;
    private GameObject o, r, l, r2, l2;
    private int index1 = 0, index2 = 1;
    [Header("target���`�}�a��")]
    public int destroyTargetSum = 0;

    // Start is called before the first frame update
    void Awake()
    {

    }

    private void Start()
    {
        Classification();
        //�Ĥ@��Target
        Instantiate(o, spawnPositionSet[0].transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDoFirastSpawn)
        {
            for (int i = 0; i < 5; i++)
            {
                SpawnTarget(index1, index2);
                index1++;
                index2++;
            }
            hasDoFirastSpawn = true;
        }
        if (spawnPositionSet[0].transform.childCount == 0)//�Ĥ@��Target�Q�}�a�F 
        {
            StartCoroutine(Resort());
            if (spawnPositionSet[0].transform.childCount != 0)
            {
                SpawnTarget(4, 5);
            }
        }


    }

    private void Classification()
    {
        foreach (GameObject t in targetPrefabs)
        {

            if (t.tag == "O")
            {
                o = t;
            }
            else if (t.tag == "R")
            {
                r = t;
            }
            else if (t.tag == "L")
            {
                l = t;
            }
            else if (t.tag == "LH")
            {
                l2 = t;
            }
            else if (t.tag == "RH")
            {
                r2 = t;
            }
        }
    }//����Target;

    private void SpawnTarget(int index1, int index2)
    {


        int randomNum = UnityEngine.Random.Range(0, targetPrefabs.Count);
        int randomNum2 = UnityEngine.Random.Range(0, targetPrefabs.Count - 2);

        //�p�GspawnPositionSet[index1]���l���󬰥k��ê,�h�u��Ͳ��k��ê,�εL��ê����
        if (spawnPositionSet[index1].transform.GetChild(0).tag == "R" || spawnPositionSet[index1].transform.GetChild(0).tag == "RH")
        {
            switch (randomNum2)
            {
                case 0:
                    Instantiate(o, spawnPositionSet[index2].transform);
                    break;
                case 1:
                    Instantiate(r, spawnPositionSet[index2].transform);
                    break;
                default:
                    Instantiate(r2, spawnPositionSet[index2].transform);
                    break;
            }

            /*if (randomNum2 == 0)
            {
                Instantiate(o, spawnPositionSet[index2].transform);
            }

            else if (randomNum2 == 1)
            {
                Instantiate(r, spawnPositionSet[index2].transform);
            }

            else
            {
                Instantiate(r2, spawnPositionSet[index2].transform);
            }*/
        }
        //�Ϥ�
        else if (spawnPositionSet[index1].transform.GetChild(0).tag == "L" || spawnPositionSet[index1].transform.GetChild(0).tag == "LH")
        {
            switch (randomNum2)
            {
                case 0:
                    Instantiate(o, spawnPositionSet[index2].transform);
                    break;
                case 1:
                    Instantiate(l, spawnPositionSet[index2].transform);
                    break;
                default:
                    Instantiate(l2, spawnPositionSet[index2].transform);
                    break;
            }

            /* if (randomNum2 == 0)
             {
                 Instantiate(o, spawnPositionSet[index2].transform);
             }

             else if (randomNum2 == 1)
             {
                 Instantiate(l, spawnPositionSet[index2].transform);
             }
             else
             {
                 Instantiate(l2, spawnPositionSet[index2].transform);
             }*/
        }
        //spawnPositionSet[index1]���L��ê����,�h�Ͳ����N����
        else
        {
            RandomSpawn(randomNum, index2);
        }





    }//�P�_�e�@��m,�B�Ͳ�

    private void RandomSpawn(int num, int index2)
    {
        if (num == 0)
        {
            Instantiate(o, spawnPositionSet[index2].transform);
        }
        else if (num == 1)
        {
            Instantiate(r, spawnPositionSet[index2].transform);
        }
        else
        {
            Instantiate(l, spawnPositionSet[index2].transform);
        }
    }//�H���Ͳ�    

    private IEnumerator Resort()
    {
        bool done = false;
        //�����U���@�Ӧ�m
        spawnPositionSet[1].transform.GetChild(0).parent = spawnPositionSet[0].transform;
        spawnPositionSet[0].transform.GetChild(0).gameObject.transform.position = spawnPositionSet[0].transform.position;

        spawnPositionSet[2].transform.GetChild(0).parent = spawnPositionSet[1].transform;
        spawnPositionSet[1].transform.GetChild(0).gameObject.transform.position = spawnPositionSet[1].transform.position;

        spawnPositionSet[3].transform.GetChild(0).parent = spawnPositionSet[2].transform;
        spawnPositionSet[2].transform.GetChild(0).gameObject.transform.position = spawnPositionSet[2].transform.position;

        spawnPositionSet[4].transform.GetChild(0).parent = spawnPositionSet[3].transform;
        spawnPositionSet[3].transform.GetChild(0).gameObject.transform.position = spawnPositionSet[3].transform.position;

        spawnPositionSet[5].transform.GetChild(0).parent = spawnPositionSet[4].transform;
        spawnPositionSet[4].transform.GetChild(0).gameObject.transform.position = spawnPositionSet[4].transform.position;
        done = true;
        yield return new WaitWhile(() => done == false);

    }//���s�Ƨ�

    public void ResetData()
    {
        destroyTargetSum = 0;
    }




}
