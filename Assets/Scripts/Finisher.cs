using System;
using UnityEngine;

public class Finisher : MonoBehaviour
{
    public static event Action Finished;
    
    [SerializeField] private Animator doorsAnimator;
    
    private static readonly int Open = Animator.StringToHash("Open");

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        
        doorsAnimator.SetTrigger(Open);
    }

    public void Finish()
    {
        Finished?.Invoke();
    }
}
