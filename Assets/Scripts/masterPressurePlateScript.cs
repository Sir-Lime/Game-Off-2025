using UnityEngine;

public class masterPressurePlateScript : MonoBehaviour
{
    private PressurePlateScript[] plates;
    private bool completed = false;

    void Start()
    {
        plates = GetComponentsInChildren<PressurePlateScript>();
    }

    void Update()
    {
        if (completed) return;

        int activeCount = 0;

        for (int i = 0; i < plates.Length; i++)
        {
            if (plates[i].gameObject.CompareTag("Activated"))
                activeCount++;
        }

        if (activeCount == plates.Length)
        {
            CompletePuzzle();
        }
    }

    void CompletePuzzle()
    {
        completed = true;
        gameObject.tag = "Activated";

        foreach (var plate in plates)
        {
            plate.LockActivatedState();
        }

        SFXScript.instance.pickUpSFX();
    }
}
