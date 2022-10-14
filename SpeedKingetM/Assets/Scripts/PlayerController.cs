using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] InputController inputController;
    public bool hasPlay = false;
    public bool isDead = false;
    public bool isDizzy = false;
    // Start is called before the first frame update
    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        inputController = gameObject.GetComponent<InputController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
        {
            animator.SetTrigger("doDie");
        }
        else
        {
            if (inputController.beenTouch)
            {
                animator.SetTrigger("doAttack");
            }
            if (inputController.left == true && inputController.right == false && transform.position.x == 1.81f)//往左
            {

                if (transform.rotation.y != 0f) //角色要面向目標物
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                transform.position = new Vector2(-1.81f, transform.position.y);
            }

            if (inputController.left == false && inputController.right == true && transform.position.x != 1.81f)//往右
            {

                if (transform.rotation.y != 180) //角色要面向目標物
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }

                transform.position = new Vector2(1.81f, transform.position.y);
            }
        }


    }

    public void ResetData()
    {
        isDead = false;
        isDizzy = false;
    }


}
