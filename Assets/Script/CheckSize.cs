using UnityEngine;

public class CheckSize : MonoBehaviour
{
    void Start()
    {
        // Akan mencetak ukuran objek ke Console saat game dimulai
        Debug.Log(gameObject.name + " size is: " + GetComponent<MeshRenderer>().bounds.size);
    }
}