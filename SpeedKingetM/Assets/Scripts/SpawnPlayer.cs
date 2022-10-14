using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] List<GameObject> characterPrefabs;
    [SerializeField] Transform spawnPoint;
    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        bool done = false;
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        GameObject prefabs = characterPrefabs[selectedCharacter];
        GameObject clone = Instantiate(prefabs, spawnPoint.position, Quaternion.identity);
        done = true;
        yield return new WaitWhile(() => done == false);
    }
}
