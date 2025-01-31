using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour {
    
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;

    void Start() {
        for (int i = 0; i < heartContainers.initialValue; i++) {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts() {
        float tmpHealth = playerCurrentHealth.runtimeValue / 2;
        for (int i = 0; i < heartContainers.initialValue; i++) {
            if (i <= tmpHealth - 1) {
                hearts[i].sprite = fullHeart;
            } else if (i >= tmpHealth) {
                hearts[i].sprite = emptyHeart;
            } else {
                hearts[i].sprite = halfHeart;
            }
        }
    }
    
}
