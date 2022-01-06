using Snake.Core;

namespace Snake.Components
{
    internal class CollisionChecker : Component
    {
        GameObject first;
        public CollisionChecker(GameObject arg)
        {
            first = arg;
        }
        public bool Check(GameObject second)
        {
            return first.GetComponent<PositionComponent>().RoundedPosition.Equals(second.GetComponent<PositionComponent>().RoundedPosition);
        }
    }
}
