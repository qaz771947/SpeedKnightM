using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;


public class InputController : MonoBehaviour
{
    public bool test = false;
    [SerializeField] PlayerController playerController;
    public bool left = false, right = false, beenTouch = false;
    public bool curInLeft = false, curInRight = false;
    public bool playerBeenHit = false;
    private float width = Screen.width;
    private TouchControls touchControls;
    private void Awake()
    {
        touchControls = new TouchControls();
        playerController = gameObject.GetComponent<PlayerController>();
    }
    private void OnEnable()
    {
        touchControls.Enable();
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }
    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        TouchSimulation.Disable();
        touchControls.Disable();
    }
    private void Update()
    {
        
        if (!playerBeenHit && !playerController.isDead)        
        {
            
            foreach (UnityEngine.InputSystem.EnhancedTouch.Touch touch in UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches)
            {
                
                if (touch.phase == UnityEngine.InputSystem.TouchPhase.Began)
                {
                    if (touch.screenPosition.x > width / 2)
                    {
                        FindObjectOfType<AudioManager>().Play("Attack");
                        right = true;
                        left = false;
                        beenTouch = true;
                        curInRight = true;
                        curInLeft = false;
                        Debug.Log("еk");
                    }
                    else
                    {
                        FindObjectOfType<AudioManager>().Play("Attack");
                        left = true;
                        right = false;
                        beenTouch = true;
                        curInRight = false;
                        curInLeft = true;
                        Debug.Log("ек");
                    }
                }
                else 
                {
                    left = false;
                    right = false;
                    beenTouch = false;
                }

            }
        }


        else if (playerBeenHit && !test)
        {
            playerController.isDead = true;
        }
    }

    public void ResetData()
    {
        left = false;
        right = false;
        beenTouch = false;
        playerBeenHit = false;
    }

}
