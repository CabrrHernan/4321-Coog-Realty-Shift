using System.Collections;
using UnityEngine;

public class ChemicalMixer : MonoBehaviour
{
    public string chemical1 = "";
    public string chemical2 = "";
    public GameObject reactionEffect;
    public string correctMix = "Red+Blue"; // Set the correct mixture
    public GameObject lockedContainer; // Assign a locked object that can be dissolved

    void OnTriggerEnter(Collider other)
    {
        if (chemical1 == "")
        {
            chemical1 = other.gameObject.tag; // Assign first chemical
        }
        else
        {
            chemical2 = other.gameObject.tag; // Assign second chemical
            CheckReaction();
        }
    }

    void CheckReaction()
    {
        string mixResult = chemical1 + "+" + chemical2;
        string reverseMix = chemical2 + "+" + chemical1;

        if (mixResult == correctMix || reverseMix == correctMix)
        {
            Debug.Log("Correct mixture! Unlocking next puzzle.");
            Instantiate(reactionEffect, transform.position, Quaternion.identity);
            lockedContainer.SetActive(false); // "Dissolve" locked container
        }
        else
        {
            Debug.Log("Wrong mixture! Explosion triggered.");
            StartCoroutine(ExplosionEffect());
        }

        ResetChemicals();
    }

    IEnumerator ExplosionEffect()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Explosion! Resetting puzzle.");
    }

    void ResetChemicals()
    {
        chemical1 = "";
        chemical2 = "";
    }
}
