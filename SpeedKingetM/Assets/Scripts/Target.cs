using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] TargetSpawn targetSpawn;
    [SerializeField] InputController inputController;
    [SerializeField] Animator animator;
    [SerializeField] private int hitCount = 0;
    [SerializeField] int blood;
    public bool beenDestroied = false;
    private bool hasPlaySound = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        inputController = FindObjectOfType<InputController>();
        targetSpawn = gameObject.transform.parent.parent.GetComponent<TargetSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.parent.tag == "GroundPosition")
        {
            if((gameObject.tag == "L" || gameObject.tag == "LH") && inputController.curInLeft) 
            {
                inputController.playerBeenHit = true;
            }
            else if ((gameObject.tag == "R" || gameObject.tag == "RH") && inputController.curInRight) 
            {
                inputController.playerBeenHit = true;
            }
            else 
            {
                if (!hasPlaySound)
                {
                    FindObjectOfType<AudioManager>().Play("Grounded");
                    hasPlaySound = true;
                }
            }            

            if ((gameObject.tag == "L" || gameObject.tag == "LH") && inputController.left)
            {
                inputController.playerBeenHit = true;

            }
            else if ((gameObject.tag == "R" || gameObject.tag == "RH") && inputController.right)
            {
                inputController.playerBeenHit = true;

            }
            else if (inputController.beenTouch == true)
            {
                PlayHit();
                hitCount += 1;
            }
        }

        if (hitCount == blood)
        {
            FindObjectOfType<AudioManager>().Play("Destroy");
            targetSpawn.destroyTargetSum += 1;
            Destroy(gameObject);
        }

    }
    private void PlayHit()
    {
        if (gameObject.tag == "LH" || gameObject.tag == "RH")
        {
            if (blood > 1)
            {
                animator.SetTrigger("doHit");
            }
        }

    }

}
