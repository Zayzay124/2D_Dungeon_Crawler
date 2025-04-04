using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CanvasLoadLevel : MonoBehaviour
{
    public float holdDuration = 1f;
    public Image filledCircle;
    float holdTimer = 0f;
    bool isHolding = false;

    private void Update()
    {
        if(isHolding)
        {
            holdTimer += Time.deltaTime;
            filledCircle.fillAmount = holdTimer / holdDuration;
            if(holdTimer >= holdDuration)
            {
                //Load next level
            }
        }
    }
    public void OnHold(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isHolding = true;
        }
        else if (context.canceled)
        {
            ResetHold();
        }
    }

    private void ResetHold()
    {
        isHolding = false;
        holdTimer = 0f;
        filledCircle.fillAmount=0f;
    }
}
