using System;

namespace Game.Field
{
    public struct GridPosition
    {
        public int X;
        public int Y;

        public GridPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public GridPosition MoveInDirectionLocal(Direction direction)
        {
            return AddDirection(direction);
        }

        public GridPosition MoveInOppositeDirectionLocal(Direction direction)
        {
            return RollBackDirection(direction);
        }
        
        public GridPosition MoveInDirectionAlloc(Direction direction)
        {
            var newPos = new GridPosition(X, Y);

            return newPos.AddDirection(direction);   
        }

        private GridPosition AddDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    Y++;
                    break;
                case Direction.LEFT:
                    X--;
                    break;
                case Direction.DOWN:
                    Y--;
                    break;
                case Direction.RIGHT:
                    X++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }

            return this;
        }

        private GridPosition RollBackDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    return AddDirection(Direction.DOWN);
                case Direction.LEFT:
                    return AddDirection(Direction.RIGHT);
                case Direction.DOWN:
                    return AddDirection(Direction.UP);
                case Direction.RIGHT:
                    return AddDirection(Direction.LEFT);
                default:
                    throw new ArgumentOutOfRangeException("direction", direction, null);
            }
        }
    }
}