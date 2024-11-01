using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSwitching : MonoBehaviour
{
    [SerializeField] private List<Gun> guns; // List of all guns
    [SerializeField] private int selectedGunIndex = 0;
    [SerializeField] private Button nextGunButton; // Button to switch to the next gun
    [SerializeField] private Button previousGunButton; // Button to switch to the previous gun

    private PlayerMovement playerMovement; // Reference to PlayerMovement script

    private void Start()
    {
        nextGunButton.onClick.AddListener(SwitchToNextGun);
        previousGunButton.onClick.AddListener(SwitchToPreviousGun);

        playerMovement = GetComponent<PlayerMovement>();

        SelectGun(selectedGunIndex); // Initialize with the first gun
    }

    void SwitchToNextGun()
    {
        selectedGunIndex = (selectedGunIndex + 1) % guns.Count;
        SelectGun(selectedGunIndex);
    }

    void SwitchToPreviousGun()
    {
        selectedGunIndex = (selectedGunIndex - 1 + guns.Count) % guns.Count;
        SelectGun(selectedGunIndex);
    }

    void SelectGun(int index)
    {
        // Deactivate all guns
        foreach (Gun gun in guns)
        {
            gun.gameObject.SetActive(false);
        }

        // Activate the selected gun and update PlayerMovement
        Gun selectedGun = guns[index];
        selectedGun.gameObject.SetActive(true);
        playerMovement.SetCurrentGun(selectedGun);
    }
}

