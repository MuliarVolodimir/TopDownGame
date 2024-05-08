using System;
using UnityEngine;

public class SlowMove : MonoBehaviour
{
    [SerializeField] int _toSlow;
    [SerializeField] Vector3 _areaSize;
    [SerializeField] LayerMask _charactersLayer;

    private void Update()
    {
        FindCharacters();
    }

    private void FindCharacters()
    {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, _areaSize, transform.forward, transform.rotation, Mathf.Infinity, _charactersLayer);

        foreach (RaycastHit hit in hits)
        {
            //var character = hit.collider.GetComponent<IControllable>();
        }
    }
}
