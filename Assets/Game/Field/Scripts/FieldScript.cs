using System.Collections;
using System.Collections.Generic;
using Game.Field;
using UnityEngine;

public class FieldScript : MonoBehaviour, IMovementConstraint
{

    public const int FIELD_WIDTH = 9;
    public const int FIELD_HEIGHT = 19;

    public CharacterScript character;

    public FieldConfig FieldConfig;
    
    // Start is called before the first frame update
    void Start()
    {
        BuildField(FieldConfig);
        
        BuildExternalWalls();
        
        character.Init(new GridPosition(FieldConfig.initialX, FieldConfig.initialY), this);
    }

    private void BuildField(FieldConfig config)
    {
        for (int i = 0; i < FIELD_WIDTH; i++)
        {
            for (int j = 0; j < FIELD_HEIGHT; j++)
            {
                if (config.GetMatrixValue(i, j))
                {
                    var wallInstance = Instantiate(FieldConfig.WallPrefab);

                    wallInstance.transform.position = PositionForGrid(i, j);
                }
            }
        }
    }

    private void BuildExternalWalls()
    {
        for (int i = -1; i < FIELD_WIDTH + 1; i++)
        {
            var wallInstance = Instantiate(FieldConfig.WallPrefab);

            wallInstance.transform.position = PositionForGrid(i, -1);
            
            wallInstance = Instantiate(FieldConfig.WallPrefab);

            wallInstance.transform.position = PositionForGrid(i, FIELD_HEIGHT);
        }
        
        for (int i = -1; i < FIELD_HEIGHT + 1; i++)
        {
            var wallInstance = Instantiate(FieldConfig.WallPrefab);

            wallInstance.transform.position = PositionForGrid(-1, i);
            
            wallInstance = Instantiate(FieldConfig.WallPrefab);

            wallInstance.transform.position = PositionForGrid(FIELD_WIDTH, i);
        }
    }

    public Vector3 PositionForGrid(int x, int y)
    {
        var offset = new Vector3((float) - FIELD_WIDTH / 2, (float) - FIELD_HEIGHT / 2);

        var xPos = x + 0.5f;
        var yPos = y + 0.5f;
        
        return new Vector3(xPos, yPos, -0.5f) + offset;
    }

    public Vector3 PositionForGrid(GridPosition position)
    {
        return PositionForGrid(position.X, position.Y);
    }

    public GridPosition MoveTo(GridPosition origin, Direction direction)
    {
        if (!CanMoveInDirection(origin, direction))
        {
            return origin;
        }

        var pos = origin;

        var counter = 0; 
        do
        {
            pos.MoveInDirectionLocal(direction);
            counter++;
            if (counter > 13)
            {
                break;
            }
        } while (IsFreePosition(pos));

        pos.MoveInOppositeDirectionLocal(direction);

        return pos;
    }

    public bool CanMoveInDirection(GridPosition origin, Direction direction)
    {
        return IsFreePosition(origin.MoveInDirectionAlloc(direction));
    }

    public bool IsWall(GridPosition position)
    {
        return FieldConfig.GetMatrixValue(position.X, position.Y);
    }

    public bool IsFreePosition(GridPosition position)
    {
        return !IsWall(position);
    }
}
