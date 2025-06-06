﻿using UnityEngine;

/// <summary>
///     This class holds some useful functions pertaining to LayerMasks
/// </summary>
public static class LayerMaskUtilities
{
    /// <summary>
    ///     This function uses bit shifting to determine if a particular layer is contained within a LayerMask
    /// </summary>
    public static bool IsLayerInMask(int layer, LayerMask mask)
    {
        return ((1 << layer) & mask) > 0;
    }

    public static bool IsLayerInMask(RaycastHit2D hit, LayerMask mask)
    {
        return IsLayerInMask(hit.collider.gameObject.layer, mask);
    }
}