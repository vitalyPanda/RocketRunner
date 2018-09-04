using UnityEngine;

public class ChangeSpeedChunk : MonoBehaviour, IChunkAction
{
    [Range(0f, 100f)]
    public float LowerSpeed;
    public void Enter()
    {
        GameBiheviour.singleton.lockSpeed = true;
        GameBiheviour.singleton.Rocket.FlySpeed = GameBiheviour.singleton.CurrentSpeed / 100 * LowerSpeed;
    }

    public void Exit()
    {
        GameBiheviour.singleton.ChangeSpeed();
    }
}
