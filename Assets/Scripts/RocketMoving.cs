using System.Collections;
using UnityEngine;

public class RocketMoving : MonoBehaviour
{
    Vector2 startVector;
    Vector2 StartVector
    {
        get
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began) startVector = Input.GetTouch(0).position;
            return startVector;
        }
    }
    public float Divider;
    public float RotateSpeed;
    public float FlySpeed;
    public TrailRenderer[] trails;
    public bool Revers;

    public float min, max, curr, cur, add;
    private void Update()
    {
        #region Moving
        transform.position += transform.up * FlySpeed * Time.deltaTime;

        if (Input.touchCount == 0) return;

        #endregion Moving


        #region Rotation

        Vector2 start = StartVector;
        float angle = transform.eulerAngles.z;
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
            if (Revers)
                angle = Mathf.Lerp(transform.eulerAngles.z, transform.eulerAngles.z - (start - Input.GetTouch(0).position).x * Divider, RotateSpeed);

            else
                angle = Mathf.Lerp(transform.eulerAngles.z, transform.eulerAngles.z + (start - Input.GetTouch(0).position).x * Divider, RotateSpeed);

        if ((angle < 180 && angle > 0) && angle > 135) angle = 135;
        if ((angle > 180 && angle < 360) && angle < 225) angle = 225;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        curr = angle;


        startVector = Input.GetTouch(0).position;
        #endregion Rotation
    }

    public float NormalizeAngle(float angle)
    {
        while (angle > 360)
            angle -= 360;
        while (angle < 0)
            angle += 360;
        return angle;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameBiheviour.singleton.Boom();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("ActionChunk")) return;
        collision.GetComponent<IChunkAction>().Enter();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("ActionChunk")) return;
        collision.GetComponent<IChunkAction>().Exit();
    }

    public void ResetTrails()
    {
        StartCoroutine("rResetTrails");

    }
    IEnumerator rResetTrails()
    {
        foreach (var item in trails)
        {
            item.time = 0;
            item.sortingOrder = -3;
        }

        yield return new WaitForSeconds(.3f);

        foreach (var item in trails)
        {
            item.time = 0.35f;
            item.sortingOrder = 9;
        }
    }
}
