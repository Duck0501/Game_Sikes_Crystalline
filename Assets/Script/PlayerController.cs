using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rollDuration = 0.2f;

    private bool isMoving = false;

    void Update()
    {
        if (isMoving) return;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            StartCoroutine(Roll(Vector3.forward));

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            StartCoroutine(Roll(Vector3.back));

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            StartCoroutine(Roll(Vector3.left));

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            StartCoroutine(Roll(Vector3.right));
    }

    System.Collections.IEnumerator Roll(Vector3 direction)
    {
        isMoving = true;

        Vector3 pivot = transform.position + direction + Vector3.down * 1f;

        Vector3 axis = Vector3.Cross(Vector3.up, direction);

        float totalAngle = 0f;

        while (totalAngle < 90f)
        {
            float angle = Mathf.Min(Time.deltaTime * (90f / rollDuration), 90f - totalAngle);
            transform.RotateAround(pivot, axis, angle);
            totalAngle += angle;
            yield return null;
        }

        transform.position = RoundToGrid(transform.position);
        transform.rotation = Quaternion.Euler(RoundToGrid(transform.rotation.eulerAngles));

        isMoving = false;
    }

    Vector3 RoundToGrid(Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
    }
}
