using UnityEngine;
using System.Collections;
public class RotateMap : MonoBehaviour
{
    [SerializeField] private bool isRotating = false;

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput() {
        if (isRotating) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotateMapDirection(new Vector3(0, -90, 0));
        }

        // When clicking right arrow key, rotate the map to the right

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RotateMapDirection(new Vector3(0, 90, 0));
        }

        // When clicking up arrow key, rotate the map to the up

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RotateMapDirection(new Vector3(90, 0, 0));
        }

        // When clicking down arrow key, rotate the map to the down

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RotateMapDirection(new Vector3(-90, 0, 0));
        }
    }

    // Function for rotating the map given a direction

    public void RotateMapDirection(Vector3 direction)
    {
        isRotating = true;
        StartCoroutine(RotateMapCoroutine(direction));
    }

    // Coroutine for lerping the rotation of the map

    private IEnumerator RotateMapCoroutine(Vector3 direction)
    {
        // Rotate the map by the given direction

        Quaternion targetRotation = transform.rotation * Quaternion.Euler(direction);

        while (transform.rotation != targetRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);

            // if the difference between the current rotation and the target rotation is less than 0.1, break the loop
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                break;
            }

            yield return null;
        }

        transform.rotation = targetRotation;

        isRotating = false;
    }
}
