using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Na pok�j
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //Za graczem
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private float cameraY;
    private float lookAhead;

    private void Update()
    {
        //na pok�j
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

        //Za graczem
        
         if (player.position.y >= -0.5f)
            transform.position = new Vector3(player.position.x + lookAhead, player.position.y, transform.position.z); 
        else
            transform.position = new Vector3(player.position.x + lookAhead, -0.5f, transform.position.z);

        lookAhead = Mathf.Lerp(lookAhead,(aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        currentPosX = _newRoom.position.x;  
    }
}
