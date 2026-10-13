using UnityEngine;

public class ControlScrambler : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private float _damageThreshold = 30f;

    [Header("Scramble Settings")]
    [SerializeField] private bool _permanent = false;
    [SerializeField] private float _scrambleDuration = 8f;

    private float _damageTaken;
    private bool _swapAxes;
    private bool _invertX;
    private bool _invertY;

    public bool IsScrambled { get; private set; }

    // called by the health code when the player gets hit
    public void AddDamage(float amount)
    {
        _damageTaken += amount;

        if (_damageTaken >= _damageThreshold)
        {
            _damageTaken = 0f;
            Scramble();
        }
    }

    [ContextMenu("Scramble Now")]
    public void Scramble()
    {
        // keep rolling until at least one thing changes, otherwise the controls would stay normal
        do
        {
            _swapAxes = Random.value > 0.5f;
            _invertX = Random.value > 0.5f;
            _invertY = Random.value > 0.5f;
        }
        while (!_swapAxes && !_invertX && !_invertY);

        IsScrambled = true;
        Debug.Log($"Controls scrambled! swap: {_swapAxes}, invertX: {_invertX}, invertY: {_invertY}");

        CancelInvoke(nameof(ResetControls));
        if (!_permanent) Invoke(nameof(ResetControls), _scrambleDuration);
    }

    public void ResetControls()
    {
        _swapAxes = false;
        _invertX = false;
        _invertY = false;
        IsScrambled = false;
    }

    // PlayerMove passes its input through this before using it
    public Vector2 ApplyScramble(Vector2 input)
    {
        if (!IsScrambled) return input;

        if (_swapAxes) input = new Vector2(input.y, input.x);
        if (_invertX) input.x = -input.x;
        if (_invertY) input.y = -input.y;

        return input;
    }
}
