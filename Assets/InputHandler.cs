using System;
using System.Collections.Generic;
using Game.Field;
using TouchScript.Gestures;
using TouchScript.Pointers;
using UnityEngine;

public class InputHandler : Gesture
{
    public event EventHandler<Direction> OnFlick
    {
        add { onFlickInvoker += value; }
        remove { onFlickInvoker -= value; }
    }

    private event EventHandler<Direction> onFlickInvoker;

    private Vector2 mainPointerPosition;
    private Pointer mainPointer;
    
    protected override void pointersPressed(IList<Pointer> pointers)
    {
        if (State == GestureState.Idle)
        {
            mainPointerPosition = pointers[0].Position;
            mainPointer = pointers[0];
            setState(GestureState.Began);
        }
    }

    protected override void pointersUpdated(IList<Pointer> pointers)
    {
        
    }

    protected override void pointersReleased(IList<Pointer> pointers)
    {
        setState(GestureState.Ended);
    }

    protected override void pointersCancelled(IList<Pointer> pointers)
    {
        setState(GestureState.Cancelled);
    }

    protected override void onRecognized()
    {
        if (onFlickInvoker != null)
        {
            onFlickInvoker(this, DirectionFromPositionDiff(mainPointerPosition, mainPointer.Position));
        }
    }

    private void Reset()
    {
        mainPointerPosition = Vector2.zero;
    }

    private Direction DirectionFromPositionDiff(Vector2 origin, Vector2 destination)
    {

        var diffVector = destination - origin;
        
        if (Mathf.Abs(diffVector.x) > Mathf.Abs(diffVector.y))
        {

            return diffVector.x > 0 ? Direction.RIGHT : Direction.LEFT;
        }
        else
        {
            return diffVector.y > 0 ? Direction.UP : Direction.DOWN;
        }
    }
}
