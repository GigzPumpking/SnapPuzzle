using Unity.VisualScripting;
using UnityEngine;

public class ShapeMover : MonoBehaviour
{
    
    private GameObject selectedShape; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectedShape = null; 
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit; 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            if(Physics.Raycast(ray, out hit))
            {
                if(selectedShape == null)
                {
                    selectedShape = hit.collider.gameObject; 
                    selectedShape.SetActive(false); 
                }
                else
                {
                    Debug.Log("New target found"+ hit.collider.gameObject.transform.parent);
                    Vector3 newShapePosition = hit.collider.gameObject.transform.position + new Vector3(0, 0, 1.7f); 
                    /*if(Physics.OverlapBox(newShapePosition, new Vector3(0.5f, 0.5f, 0.5f)).Length > 1)
                    {
                        Debug.Log("Cannot place shape here");
                        newShapePosition = selectedShape.transform.position + new Vector3(0, 0, 1.7f);
                        selectedShape.transform.position = newShapePosition;
                    }*/
                    Collider[] overlaps = Physics.OverlapBox(newShapePosition, new Vector3(0.5f, 0.5f, 0.5f));
                    while (overlaps.Length > 0) 
                    {
                        Debug.Log("Cannot place shape here, overlaps detected with:");
                        foreach (Collider col in overlaps)
                        {
                            Debug.Log("Overlap with: " + col.gameObject.name);
                        }
                        // Adjust the position and check for overlaps again
                        newShapePosition += new Vector3(0, 0, 1.7f);
                        overlaps = Physics.OverlapBox(newShapePosition, new Vector3(0.5f, 0.5f, 0.5f));
                    } 
                    
                        selectedShape.transform.position = newShapePosition;
                        Debug.Log("Shape placed at: "+ newShapePosition);
                    
                    
                    selectedShape.SetActive(true);
                    selectedShape = null;
                }
            }
        }
    }
}
