using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Game.Character;
using Game.Field;
using TouchScript.Examples.Cube;
using TouchScript.Gestures;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float movementSpeed = 20f;
    
    public GridPosition currentPosition;
    public InputHandler FlickGesture;
    public AnimationCurve EasingCurve;
    private Queue<MovementCommand> movementCommands;
    private IMovementConstraint movementConstraint;

    public void Init(GridPosition initialPos, IMovementConstraint movementConstraint)
    {
        this.movementConstraint = movementConstraint;

        currentPosition = initialPos;

        transform.position = movementConstraint.PositionForGrid(currentPosition);
    }

    public IEnumerator Move(MovementCommand command)
    {
        var destination = movementConstraint.MoveTo(currentPosition, command.Direction);

        bool completed = false;

        currentPosition = destination;
        
        var moveTween = transform.DOMove(
                movementConstraint.PositionForGrid(destination), 
                movementSpeed)
            .SetEase(EasingCurve)
            .SetSpeedBased()
            .Play().onComplete += () => { completed = true; };
        
        yield return new WaitUntil(() => completed);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        FlickGesture.OnFlick += OnFlick;
        
        movementCommands = new Queue<MovementCommand>();

        StartCoroutine(WatchCommandStack());
    }

    private void OnFlick(object sender, Direction direction)
    {
        movementCommands.Enqueue(new MovementCommand(direction));
        
//        var flick = sender as FlickGesture;
//        var vector = flick.ScreenFlickVector;
//
//        if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
//        {
//            movementCommands.Enqueue(
//                vector.x > 0 ? 
//                new MovementCommand(Direction.RIGHT) : 
//                new MovementCommand(Direction.LEFT));
//        }
//        else
//        {
//            movementCommands.Enqueue(
//                vector.y > 0 ? 
//                new MovementCommand(Direction.UP) : 
//                new MovementCommand(Direction.DOWN));
//        }
    }

    private IEnumerator WatchCommandStack()
    {
        while (true)
        {
            yield return new WaitUntil(() => movementCommands.Count > 0);

            Debug.Log("Stacked commands: " + movementCommands.Count);
            
            var command = movementCommands.Dequeue();

            yield return StartCoroutine(command.RunCommand(this));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            movementCommands.Enqueue(new MovementCommand(Direction.UP));
        } 
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movementCommands.Enqueue(new MovementCommand(Direction.LEFT));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            movementCommands.Enqueue(new MovementCommand(Direction.DOWN));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            movementCommands.Enqueue(new MovementCommand(Direction.RIGHT));
        } 
        
    }
}
