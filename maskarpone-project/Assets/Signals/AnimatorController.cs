using UnityEngine;
using UnityEngine.Assertions;

public class AnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator = null!;

    private void Awake()
    {
        Assert.IsNotNull(m_animator);
    }

    public void SetIdle() => m_animator.SetInteger("State", 0);
    public void SetWalking() => m_animator.SetInteger("State", 1);
    public void StopTime() => m_animator.speed = 0;
    public void PlayTime() => m_animator.speed = 1;
}
