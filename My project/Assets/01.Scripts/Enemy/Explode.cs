using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destorythisobject());
    }

	IEnumerator Destorythisobject()
    {
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
