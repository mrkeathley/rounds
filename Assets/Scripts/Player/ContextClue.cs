using UnityEngine;

public class ContextClue : MonoBehaviour {
    public GameObject contextClue;
    public bool isVisible = false;

    public void ToggleContext() {
        isVisible = !isVisible;
        contextClue.SetActive(isVisible);
    }
}