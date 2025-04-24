using UnityEngine;
using System.Collections;

public class HandSignController : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;
    private Collider leftHandCollider;
    private Collider rightHandCollider;

    private Animator leverAnimator;

    public ParticleSystem particlesLeft;
    public ParticleSystem particlesRight;

    private void Start()
    {
        if (leverAnimator == null)
        {
            leverAnimator = GetComponent<Animator>();
        }

        if (particlesLeft != null)
        {
            particlesLeft.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        if (particlesRight != null)
        {
            particlesRight.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        if (leftHand != null)
        {
            leftHandCollider = leftHand.GetComponent<Collider>();
            if (leftHandCollider == null)
            {
                leftHandCollider = leftHand.AddComponent<SphereCollider>();
                Debug.Log("Added collider to left hand");
            }
        }

        if (rightHand != null)
        {
            rightHandCollider = rightHand.GetComponent<Collider>();
            if (rightHandCollider == null)
            {
                rightHandCollider = rightHand.AddComponent<SphereCollider>();
                Debug.Log("Added collider to right hand");
            }
        }

        if (leftHandCollider == null || rightHandCollider == null)
        {
            Debug.LogWarning("Controller colliders not found");
        }

        StartCoroutine(StopParticlesShortly());
    
    }

    private IEnumerator StopParticlesShortly()
    {
        yield return new WaitForSeconds(0.1f);

        if (particlesLeft != null && particlesLeft.isPlaying)
        {
            particlesLeft.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        if (particlesRight != null && particlesRight.isPlaying)
        {
            particlesRight.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == leftHandCollider || other == rightHandCollider)
        {
            Debug.Log("Entered");
            leverAnimator.SetBool("HandSignIsPressed", true);
        }

        if (particlesLeft != null)
        {
            particlesLeft.Play();
        }
        if (particlesRight != null)
        {
            particlesRight.Play();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other == leftHandCollider || other == rightHandCollider)
        {
            Debug.Log("Exited");
            leverAnimator.SetBool("HandSignIsPressed", false);
        }

        if (particlesLeft != null)
        {
            particlesLeft.Stop();
        }
        if (particlesRight != null)
        {
            particlesRight.Stop();
        }
    }
}
