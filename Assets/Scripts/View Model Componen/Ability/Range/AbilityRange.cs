using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public abstract class AbilityRange : MonoBehaviour
{
    public int horizontal = 1;
    public int vertical = int.MaxValue; // don't used
    public virtual bool positionOriented { get { return true; } }
    public virtual bool directionOriented { get { return false; } } //should be true when the range is a pattern like a cone or line. When it is true, we will use the movement input buttons to change the user’s facing direction so that the effected tiles change. When the directionOriented property is false, you may move the cursor to select tiles within the highlighted range.
    protected Unit unit { get { return GetComponentInParent<Unit>(); } }
    public abstract List<Tile> GetTilesInRange(Board board); //List of Tile(s) which can be reached by the selected ability. Like Chess
}
