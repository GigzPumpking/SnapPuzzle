using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour
{
    private Camera camera;

    [SerializeField] private Vector3 upRotation = new Vector3(90, 0, 90);

    [SerializeField] private Vector3 downRotation = new Vector3(-90, 0, -90);

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        // Given WASD keys, rotate the camera around the X and Y axes respectively
        if (Input.GetKeyDown(KeyCode.W))
        {
            Rotate(upRotation);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Rotate(downRotation);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Rotate(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Rotate(Vector3.down);
        }
    }

    private void Rotate(Vector3 direction) {
        StartCoroutine(RotateCoroutine(direction));
    }

    private IEnumerator RotateCoroutine(Vector3 direction)
    {
        float angle = 0;
        while (angle < 90)
        {
            transform.RotateAround(Vector3.zero, direction, 90 * Time.deltaTime);
            angle += 90 * Time.deltaTime;
            yield return null;
        }
    }
}
