using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool hasBeatenTutorial, hasBeatenFirstLevel, hasBeatenSecondLevel, hasBeatenThirdLevel, playerIsInvincible, playerHasInfiniteAmmo, playerHasInfiniteMana, isFullscreen = true;

    public int quality;

    public GameData (HasClearedLevelController controller)
    {
        hasBeatenTutorial = controller.hasBeatenTutorial;
        hasBeatenFirstLevel = controller.hasBeatenFirstLevel;
        hasBeatenSecondLevel = controller.hasBeatenSecondLevel;
        hasBeatenThirdLevel = controller.hasBeatenThirdLevel;
        playerIsInvincible = controller.playerIsInvincible;
        playerHasInfiniteAmmo = controller.playerHasInfiniteAmmo;
        playerHasInfiniteMana = controller.playerHasInfiniteMana;
        isFullscreen = controller.isFullscreen;
        quality = controller.GetQuality();
    }
}
