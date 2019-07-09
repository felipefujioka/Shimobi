using UnityEngine;

namespace Game.Field
{
    public interface IMovementConstraint
    {
        GridPosition MoveTo(GridPosition origin, Direction direction);
        bool CanMoveInDirection(GridPosition origin, Direction direction);
        bool IsWall(GridPosition position);
        bool IsFreePosition(GridPosition position);
        Vector3 PositionForGrid(int x, int y);
        Vector3 PositionForGrid(GridPosition position);

    }
}