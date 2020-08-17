using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARPlaneTap : MonoBehaviour
{
  public GameObject objectToInstantiate;
  private GameObject spawnedObject;
  private ARRaycastManager _arRayCaster;
  private Vector2 touchPos;
  static List<ARRaycastHit> hits = new List<ARRaycastHit>();
  // Start is called before the first frame update
  void Start()
  {
    _arRayCaster = GetComponent<ARRaycastManager>();
  }

  bool TryGetTouchPosition(out Vector2 touchPos)
  {
    if (Input.touchCount > 0)
    {
      touchPos = Input.GetTouch(0).position;
      return true;
    }
    touchPos = default;
    return false;
  }

  // Update is called once per frame
  void Update()
  {
    if (!TryGetTouchPosition(out Vector2 touchPosition))
    {
      return;
    }
    if (_arRayCaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
    {
      var hitpose = hits[0].pose;
      if (spawnedObject == null)
      {
        spawnedObject = Instantiate(objectToInstantiate, hitpose.position, hitpose.rotation);
      }
      else
      {
        spawnedObject.transform.position = hitpose.position;
      }
    }
  }
}
