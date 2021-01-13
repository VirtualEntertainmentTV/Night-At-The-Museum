using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SmoothTurnProvider : LocomotionProvider
{
    // How much do we turn?
    public float turnSegment = 45.0f;

    // Hiw long does it take to turn
    public float turnTime = 3.0f;

    // Basic input - I'd recommend replacing with a input solution
    public InputHelpers.Button rightTurnButton = InputHelpers.Button.PrimaryAxis2DRight;
    public InputHelpers.Button leftTurnButton = InputHelpers.Button.PrimaryAxis2DLeft;

    // List of the controllers we're going to use
    public List<XRController> controllers = new List<XRController>();

    // The amount we're turning to
    private float targetTurnAmount = 0.0f;
    private void Update()
    {
        // Let's ask the lcomotion system if we can move
        if (CanBeginLocomotion())
            CheckForInput();
    }

    private void CheckForInput()
    {
        // Go through the controllers
        foreach (XRController controller in controllers)
        {
            // Check for input, return the segment with positive or negative
            targetTurnAmount = CheckForTurn(controller);

            // If we actually got a value from input, try to turn
            if (targetTurnAmount != 0.0f)
                TrySmoothTurn();
        }

    }

    // Check for inout, this can be done cleaner with a more robust input solution
    private float CheckForTurn(XRController controller)
    {
        if(controller.inputDevice.IsPressed(rightTurnButton, out bool rightPress))
        {
            // Check if we pressed right
            if (rightPress)
                return turnSegment;
        }

        if(controller.inputDevice.IsPressed(leftTurnButton, out bool leftPress))
        {
            //Check if we pressed left
            if (leftPress)
                return -turnSegment;
        }


        //If we hit nithing return 0
        return 0.0f;
    }

    private void TrySmoothTurn()
    {
        // Let's try turning with the amount we got
        StartCoroutine(TurnRoutine(targetTurnAmount));

        // Since the value has been used, let's clear it out
        targetTurnAmount = 0.0f;
    }

    private IEnumerator TurnRoutine(float turnAmount)
    {
        // We need to store this since we only want to pass the difference
        float previousTurnChange = 0.0f;

        // Record the whole time of the loop for proper lerp
        float elapsedTime = 0.0f;

        // Let the motion begin
        BeginLocomotion();

        while(elapsedTime <= turnTime)
        {
            // How far are we into the lerp?
            float blend = elapsedTime / turnTime;
            float turnChange = Mathf.Lerp(0, turnAmount, blend);

            // Figure out the difference and apply it
            float turnDifference = turnChange - previousTurnChange;
            system.xrRig.RotateAroundCameraUsingRigUp(turnDifference);

            // Save the current amount we've moved, and add to elapsed time
            previousTurnChange = turnChange;
            elapsedTime += Time.deltaTime;

            // Yield or we are crashing
            yield return null;

           
        }

        // Let the motion end
        EndLocomotion();
    }
}
