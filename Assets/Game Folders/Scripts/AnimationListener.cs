using System;
using UnityEngine;

public class AnimationListener : MonoBehaviour
{
    public event Action<string> OnAnimationPlayed;
    public void Die()
    {
        OnAnimationPlayed?.Invoke("Die");
    }

    public void BossDie()
    {
        OnAnimationPlayed?.Invoke("Boss Die");
    }
}
