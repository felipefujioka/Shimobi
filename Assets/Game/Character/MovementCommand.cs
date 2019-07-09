using System.Collections;
using DG.Tweening;
using Game.Field;

namespace Game.Character
{
    public class MovementCommand
    {
        public Direction Direction;

        public MovementCommand(Direction dir)
        {
            Direction = dir;
        }

        public IEnumerator RunCommand(CharacterScript character)
        {
            yield return character.Move(this);
        } 
    }
}