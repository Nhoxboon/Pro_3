using UnityEngine;

public class Bobber : NhoxBehaviour
{
    [SerializeField] protected float yOffset = 0.2f;
    [SerializeField] protected float bobDuration = 1f;
    [SerializeField] protected float stopMultiplier = 4f;

    [SerializeField] protected AnimationCurve bobCurve =
        new(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0f));

    protected Vector3 currentPosition;

    protected Vector3 initialPosition;

    protected bool isBobbing;
    protected bool shouldStopBobbing;

    protected float t;

    protected override void Awake()
    {
        base.Awake();
        initialPosition = transform.localPosition;
    }

    protected void Update()
    {
        if (!isBobbing) return;

        if (shouldStopBobbing && t <= 0f)
        {
            StopCompletely();
        }

        UpdateCurvePosition();

        UpdateTime();
    }

    public void StartBobbing()
    {
        isBobbing = true;
        shouldStopBobbing = false;
        t = 0f;
    }

    public void StopBobbing()
    {
        shouldStopBobbing = true;
    }

    protected void StopCompletely()
    {
        isBobbing = false;
        transform.localPosition = initialPosition;
    }
    
    protected void UpdateCurvePosition()
    {
        var curveValue = bobCurve.Evaluate(t / bobDuration);

        currentPosition = transform.localPosition;
        currentPosition.y = initialPosition.y + yOffset * curveValue;

        transform.localPosition = currentPosition;
    }
    
    protected void UpdateTime()
    {
        if (!shouldStopBobbing)
        {
            t += Time.deltaTime;
            t %= bobDuration;
        }
        else
        {
            if (t > bobDuration / 2f) t = bobDuration - t;

            t -= Time.deltaTime * stopMultiplier;
        }
    }
}