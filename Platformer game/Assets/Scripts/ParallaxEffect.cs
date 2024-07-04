using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cameraObject;
    public Transform followTarget;

    private Vector2 startingPosition;

    private float startingZ;

    private Vector2 cameraMoveSinceStart => (Vector2)cameraObject.transform.position - startingPosition;

    private float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    private float clippingPlane => (cameraObject.transform.position.z +
                                    (zDistanceFromTarget > 0 ? cameraObject.farClipPlane : cameraObject.nearClipPlane));

    private float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    private void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 newPosition = startingPosition + cameraMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}