using System.Collections.Generic;
using UnityEngine;


    public static class CombatKnockBackUtilities
    {
        public static bool TryKnockBack(GameObject gameObject, CombatKnockbackData data, out Knockbackable knockBackable)
        {
            if (gameObject.TryGetComponentInChildren(out knockBackable))
            {
                knockBackable.Knockback(data);
                return true;
            }

            return false;
        }

        public static bool TryKnockBack(Component component, CombatKnockbackData data, out Knockbackable knockBackable)
        {
            return TryKnockBack(component.gameObject, data, out knockBackable);
        }

        public static bool TryKnockBack(IEnumerable<GameObject> gameObjects, CombatKnockbackData data,
            out List<Knockbackable> knockBackables)
        {
            var hasKnockedBack = false;
            knockBackables = new List<Knockbackable>();

            foreach (var gameObject in gameObjects)
            {
                if (TryKnockBack(gameObject, data, out var knockBackable))
                {
                    knockBackables.Add(knockBackable);
                    hasKnockedBack = true;
                }
            }

            return hasKnockedBack;
        }
        
        public static bool TryKnockBack(IEnumerable<Component> components, CombatKnockbackData data,
            out List<Knockbackable> knockBackables)
        {
            var hasKnockedBack = false;
            knockBackables = new List<Knockbackable>();

            foreach (var comp in components)
            {
                if (TryKnockBack(comp, data, out var knockBackable))
                {
                    knockBackables.Add(knockBackable);
                    hasKnockedBack = true;
                }
            }

            return hasKnockedBack;
        }
    }

