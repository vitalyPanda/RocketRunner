using System.Linq;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [Range(0f, 100f)]
    public float Chance;
    public Direction RotationTo = Direction.Forward;
    public DirToMove[] CurrentRotation;
    [HideInInspector]
    public SpriteRenderer sprite;
    public bool IsActive;
    public int index;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public Vector2 GetPos(Direction dir)
    {
        return CurrentRotation.First(x => x.direction == dir).PosTo + (Vector2)transform.position;
    }

    private void OnDrawGizmosSelected()
    {
       
        foreach (var item in CurrentRotation)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere((Vector2)transform.position + item.PosTo, 10);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere((Vector2)transform.position + item.PosToStack, 10);
        }
    }
}

[System.Serializable]
public class DirToMove
{
    public Direction direction;
    public Vector2 PosTo;
    public Vector2 PosToStack;
}
