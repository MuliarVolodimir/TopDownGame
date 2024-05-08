using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _cameraTargetPos;
    [SerializeField] Vector3 _targetOffset;
    [SerializeField] float _smooth;

    private void Start()
    {
        _cameraTargetPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Following();
    }

    private void Following()
    {
        transform.position = Vector3.Lerp(transform.position, _cameraTargetPos.position + _targetOffset, _smooth * Time.deltaTime);
        transform.rotation = Quaternion.Euler(45, 0, 0);
    }
}
