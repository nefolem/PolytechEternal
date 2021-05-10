using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followObject;
    public Vector2 followOffset; // смещение камеры
    public float speed = 3f;
    private Vector2 bound;
    private Rigidbody rb;

    void Start()
    {
       bound = calcBound();
       rb = followObject.GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        Vector2 follow = followObject.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);
        Vector3 newPosition = transform.position;
        if(Mathf.Abs(xDifference) >= bound.x)
        {
            newPosition.x = follow.x;
        }
        if(Mathf.Abs(yDifference) >= bound.y)
        {
            newPosition.y = follow.y;
        }
        float moveSpeed = rb.velocity.magnitude > speed ? rb.velocity.magnitude : speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);

    }

    private Vector3 calcBound()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.blue;
        Vector2 border = calcBound();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y *2, 1));
    }
}
