using Unity.VisualScripting;
using UnityEngine;

public class ShapeMover : MonoBehaviour
{
    private GameObject selectedShape;

    private GameObject highlightedShape;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedShape = null;
    }

    // Update is called once per frame
    void Update()
    {

        RaycastMousePosition();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (selectedShape == null)
                {
                    selectedShape = hit.collider.gameObject;

                    shapeState(false);

                    selectedShape.SetActive(false);
                }
                else
                {
                    selectedShape.transform.position = returnNewShapePosition(hit);

                    shapeState(true);

                    selectedShape.SetActive(true);
                    selectedShape = null;
                }
            }
        }
    }

    void shapeState(bool state)
    {
        if (selectedShape == null) return;

        if (state) {

            // Set collider to true to allow raycast to hit the shape and player to walk on it
            selectedShape.GetComponent<Collider>().enabled = true;
        } else {

            // Set collider to false to prevent raycast from hitting the shape and player from walking on it
            selectedShape.GetComponent<Collider>().enabled = false;
        }
    }

    Vector3 returnNewShapePosition(RaycastHit hit)
    {
        // Calculate the new position based on the hit normal
        Vector3 newShapePosition = hit.collider.gameObject.transform.position + returnNewPosition(hit.normal);

        Collider[] overlaps = Physics.OverlapBox(newShapePosition, new Vector3(0.5f, 0.5f, 0.5f));
        while (overlaps.Length > 0)
        {
            newShapePosition += returnNewPosition(hit.normal);
            overlaps = Physics.OverlapBox(newShapePosition, new Vector3(0.5f, 0.5f, 0.5f));
        }

        return newShapePosition;
    }

    Vector3 returnNewPosition(Vector3 hitNormal)
    {
        if(hitNormal.x > 0){
            hitNormal.x = hitNormal.x * 1.7f; 
        }
        else if(hitNormal.y > 0){
            hitNormal.y = hitNormal.y * 1.7f; 
        }
        else if(hitNormal.z > 0){
            hitNormal.z = hitNormal.z * 1.7f; 
        }
        return hitNormal;
    }

    void RaycastMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (selectedShape == null)
        {
            HighlightShape(ray);
        } else {
            HighlightSpace(ray);
        }


    }

    void HighlightShape(Ray ray) {
        RaycastHit hit;

        // If the ray hits a shape, highlight it

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.tag == "Shape")
            {
                if (highlightedShape != null)
                {
                    highlightedShape.GetComponent<Renderer>().material.color = Color.white;
                }

                highlightedShape = hit.collider.gameObject;
                highlightedShape.GetComponent<Renderer>().material.color = Color.yellow;
            }
            else
            {
                if (highlightedShape != null)
                {
                    highlightedShape.GetComponent<Renderer>().material.color = Color.white;
                    highlightedShape = null;
                }
            }
        }
        else
        {
            if (highlightedShape != null)
            {
                highlightedShape.GetComponent<Renderer>().material.color = Color.white;
                highlightedShape = null;
            }
        }
    }

    void HighlightSpace(Ray ray) {
        // Highlights the location where the shape will be placed by placing the unactivated cube at the location
        
        RaycastHit hit;

        bool isHit = Physics.Raycast(ray, out hit);

        if (isHit && (hit.collider.gameObject == selectedShape)) {
            return;
        }

        if (isHit) {
            Vector3 newShapePosition = returnNewShapePosition(hit);

            selectedShape.transform.position = newShapePosition;

            selectedShape.SetActive(true);
        } else {
            selectedShape.SetActive(false);
        }

    }
}