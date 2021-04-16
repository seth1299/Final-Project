using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool hasBeatenTutorial, hasBeatenFirstLevel, hasBeatenSecondLevel, hasBeatenThirdLevel;

    public GameData (HasClearedLevelController controller)
    {
        hasBeatenTutorial = controller.hasBeatenTutorial;
        hasBeatenFirstLevel = controller.hasBeatenFirstLevel;
        hasBeatenSecondLevel = controller.hasBeatenSecondLevel;
        hasBeatenThirdLevel = controller.hasBeatenThirdLevel;
    }
}
