using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{

    public GameObject roadChunk;

    //public Transform endOfChunk;

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Chunk Trigger"))
        {
            // Gets the end of the current chunk of road and puts a new chunk at the end of it
            Vector3 endOfCurrentChunk = new Vector3(roadChunk.transform.position.x, roadChunk.transform.position.y, other.transform.parent.position.z + other.transform.parent.localScale.z);

            Instantiate(roadChunk, endOfCurrentChunk, Quaternion.identity);
        }
    }

}
