using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public const string healthBarGameObjectName = "healthBar";

    public const string selectionBoxGameObjectName = "selectionBox";
    public const string selectionBoxColliderGameObjectName = "selectionBoxCollider";
    public const float selectionBoxPivot = 0.5f;
    public const float selectionBoxMinimumAnchor = 0.5f;
    public const float selectionBoxMaximumAnchor = 0.5f;

    public const int mouseLeftButtonId = 0;
    public const int mouseRightButtonId = 1;
    public const int mouseMiddleButtonId = 2;
    public const float mouseLongestClick = 0.25f;
    public const float mouseDragThreshold = 5f;

    public const int maxSelectedSettlers = 12;

    public const string terrainGameObjectTag = "Terrain";
}
