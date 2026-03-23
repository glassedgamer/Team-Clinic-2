using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{

    public GameObject roadChunk;

    //public Transform endOfChunk;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Chunk Trigger"))
        {
            Instantiate(roadChunk, roadChunk.transform.position, Quaternion.identity);
        }
    }

}
