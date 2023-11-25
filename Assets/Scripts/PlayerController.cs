using FMODUnity;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float acceleration = 5;
    [SerializeField] private StudioEventEmitter emitter;
    [SerializeField] private float bottomYLevel;
    [SerializeField] private float topYLevel;
    [SerializeField] private float tranSpeed = 3;

    private Vector3 _velocity;
    private float _currentYLevel;
    private float _targetYLevel;
    private bool _transitioning;
    private GroundLevel _currentLevel;

    private enum GroundLevel
    {
        Bottom,
        Top,
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = -1;
        _currentYLevel = topYLevel;
        _currentLevel = GroundLevel.Top;
        var pos = transform.position;
        pos.y = _currentYLevel;
        transform.position = pos;
    }

    private void Update()
    {
        if (!_transitioning)
        {
            // movement
            Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            Vector3 targetVel = direction * speed;
            _velocity = Vector3.MoveTowards(_velocity, targetVel, acceleration * Time.deltaTime);
            transform.position += _velocity * Time.deltaTime;
            
            // ground switch
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _transitioning = true;

                switch (_currentLevel)
                {
                    case GroundLevel.Top:
                        _targetYLevel = bottomYLevel;
                        _currentLevel = GroundLevel.Bottom;
                        emitter.SetParameter("Underground", 1);
                        break;
                    case GroundLevel.Bottom:
                        _targetYLevel = topYLevel;
                        _currentLevel = GroundLevel.Top;
                        emitter.SetParameter("Underground", 0);
                        break;
                }
            }
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, _targetYLevel, transform.position.z), tranSpeed*Time.deltaTime);

            if (Mathf.Abs(transform.position.y - _targetYLevel) < 0.1f)
            {
                _transitioning = false;
                _currentYLevel = _targetYLevel;
            }
        }
    }
}