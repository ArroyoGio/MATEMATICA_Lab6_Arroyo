using UnityEngine;

public class Obstacule : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 50f;

    private void Update()
    {
        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, 0f);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}

