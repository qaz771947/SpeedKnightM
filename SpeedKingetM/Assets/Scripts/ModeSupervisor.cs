using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSupervisor : MonoBehaviour
{
    public int modeNum = 1;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Mode1")
        {
            modeNum = 1;
        }
        else if (scene.name == "Mode2")
        {
            modeNum = 2;
        }
        else if (scene.name == "Mode3")
        {
            modeNum = 3;
        }
    }
}
