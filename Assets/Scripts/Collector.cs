using UnityEngine;

public class Collector : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        IItem item = collision.gameObject.GetComponent<IItem>();
        if (item != null)
        {
            item.Collect();
        }
    }

}
