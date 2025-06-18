using MelonLoader;
using HarmonyLib;
using UnityEngine;
using Il2Cpp;
using Il2CppHutongGames.PlayMaker;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem;
using Il2CppSystem.Collections;
using System.Collections;
using Il2CppSystem.Reflection;
using System.Reflection;
using System.Linq;
using Il2CppInterop.Runtime.Runtime;

namespace ArchipelaBlue
{
    public class ArchipelaBlueMod : MelonMod
    {
        static bool isInitialized = false;
        static int deckCount = 0;
        static List<RoomDatabase> roomDecks = new List<RoomDatabase>();
        public override void OnInitializeMelon()
        {
            base.LoggerInstance.Msg("ArchipelaBlue initialized successfully!");
            //GameObject gameObject = GameObject.Find("/__SYSTEM/THE DRAFT");
        }

        [HarmonyPatch(typeof(RoomDatabase), nameof(RoomDatabase.AddRoom))]
        public class RoomDatabaseAddRoomPatch
        {
            public static void Postfix(RoomDatabase __instance, RoomCard template)
            {
                if (!roomDecks.Contains(__instance))
                {
                    roomDecks.Add(__instance);
                }
                //MelonLogger.Msg($"Room added: {card.Template.Prefab.name} PoolCount: {card.Template.PoolCount} RemoveFromPool: {card.Template.RemoveFromPool}");
            }
        }

        [HarmonyPatch(typeof(RoomDraftHelper), nameof(RoomDraftHelper.StartDraft))]
        public class RoomDraftHelperInitializeDraftPatch
        {
            public static void Postfix(RoomDraftHelper __instance)
            {
                MelonLogger.Msg($"Object type: {__instance.Common.arrayList._items[0].GetType()}");
                foreach (Il2CppSystem.Object item in __instance.Common.arrayList._items)
                {
                    if (item == null)
                        continue;
                    MelonLogger.Msg(item.Cast<GameObject>().name);
                }
            }
        }

        /*[HarmonyPatch]
        public static class CreateCardPatch
        {
            public static System.Collections.Generic.IEnumerable<System.Reflection.MethodBase> TargetMethods()
            {
                yield return AccessTools.Method(typeof(RoomDraftContext), nameof(RoomDraftContext.CreateCard), new[] { typeof(GameObject) });
                yield return AccessTools.Method(typeof(RoomDraftContext), nameof(RoomDraftContext.CreateCard), new[] { typeof(RoomTemplate) });
            }
            public static void Postfix(ref RoomCard __result)
            {
                deckCount++;
                MelonLogger.Msg($"RoomCard created: {__result.Template.Prefab.name} PoolCount: {__result.Template.PoolCount} RemoveFromPool: {__result.Template.RemoveFromPool}");
                MelonLogger.Msg($"Cards Created: {deckCount}");
            }
        }*/

        /*[HarmonyPatch(typeof(RoomDraftContext), nameof(RoomDraftContext.StartDraft))]
        public class StartDraftPatch
        {
            public static void Postfix(RoomDraftContext __instance)
            {
                if (!isInitialized)
                {
                    RoomDraftRound draft = __instance.CurrentPlans;
                    if (draft == null)
                        return;
                    isInitialized = true;
                    foreach (RoomTemplate card in draft.PulledCards)
                    {
                        MelonLogger.Msg($"Draft Card: {card.Prefab.name} PoolCount: {card.PoolCount} RemoveFromPool: {card.RemoveFromPool}");
                    }
                    RoomDeck common = __instance.GetCorrectDeck(RoomDraftType.COMMON);
                    foreach (RoomCard card in common.Cards)
                    {
                        MelonLogger.Msg($"Common Card: {card.Template.Prefab.name} PoolCount: {card.Template.PoolCount} RemoveFromPool: {card.Template.RemoveFromPool}");
                    }
                    RoomDeck common_gems = __instance.GetCorrectDeck(RoomDraftType.COMMON_GEMS);
                    foreach (RoomCard card in common_gems.Cards)
                    {
                        MelonLogger.Msg($"Common Gem Card: {card.Template.Prefab.name} PoolCount: {card.Template.PoolCount} RemoveFromPool: {card.Template.RemoveFromPool}");
                    }
                    RoomDeck rare = __instance.GetCorrectDeck(RoomDraftType.RARE);
                    foreach (RoomCard card in rare.Cards)
                    {
                        MelonLogger.Msg($"Rare Card: {card.Template.Prefab.name} PoolCount: {card.Template.PoolCount} RemoveFromPool: {card.Template.RemoveFromPool}");
                    }
                    RoomDeck rare_gems = __instance.GetCorrectDeck(RoomDraftType.RARE_GEMS);
                    foreach (RoomCard card in rare_gems.Cards)
                    {
                        MelonLogger.Msg($"Rare Gem Card: {card.Template.Prefab.name} PoolCount: {card.Template.PoolCount} RemoveFromPool: {card.Template.RemoveFromPool}");
                    }
                }
            }
        }*/

        /*[HarmonyPatch(typeof(RoomDraftContext), nameof(RoomDraftContext.CreateDeck), new []{ typeof(PlayMakerArrayListProxy), typeof(string) })]
        public class CreateDeckPatch
        {
            public static void Postfix(RoomDraftContext __instance, ref RoomDeck __result)
            {
                if (!isInitialized)
                {
                    isInitialized = true;
                    RoomDeck common = __instance.GetCorrectDeck(RoomDraftType.COMMON);
                    foreach (RoomCard card in common.Cards)
                    {
                        MelonLogger.Msg($"Common Card: {card.Template.Prefab.name} PoolCount: {card.Template.PoolCount} RemoveFromPool: {card.Template.RemoveFromPool}");
                    }
                    RoomDeck common_gems = __instance.GetCorrectDeck(RoomDraftType.COMMON_GEMS);
                    foreach (RoomCard card in common_gems.Cards)
                    {
                        MelonLogger.Msg($"Common Gem Card: {card.Template.Prefab.name} PoolCount: {card.Template.PoolCount} RemoveFromPool: {card.Template.RemoveFromPool}");
                    }
                    RoomDeck rare = __instance.GetCorrectDeck(RoomDraftType.RARE);
                    foreach (RoomCard card in rare.Cards)
                    {
                        MelonLogger.Msg($"Rare Card: {card.Template.Prefab.name} PoolCount: {card.Template.PoolCount} RemoveFromPool: {card.Template.RemoveFromPool}");
                    }
                    RoomDeck rare_gems = __instance.GetCorrectDeck(RoomDraftType.RARE_GEMS);
                    foreach (RoomCard card in rare_gems.Cards)
                    {
                        MelonLogger.Msg($"Rare Gem Card: {card.Template.Prefab.name} PoolCount: {card.Template.PoolCount} RemoveFromPool: {card.Template.RemoveFromPool}");
                    }
                }
            }
        }

        [HarmonyPatch(typeof(RoomDraftContext), nameof(RoomDraftContext.CreateDeck), new[] { typeof(int), typeof(bool) , typeof(PlayMakerArrayListProxy[])})]
        public class CreateDeckPatch2
        {
            public static void Postfix(RoomDraftContext __instance, ref RoomDeck __result)
            {
                if (!isInitialized)
                {
                    isInitialized = true;
                    RoomDeck common = __instance.GetCorrectDeck(RoomDraftType.COMMON);
                    foreach (RoomCard card in common.Cards)
                    {
                        MelonLogger.Msg($"Common Card: {card.Template.Prefab.name} PoolCount: {card.Template.PoolCount} RemoveFromPool: {card.Template.RemoveFromPool}");
                    }
                    RoomDeck common_gems = __instance.GetCorrectDeck(RoomDraftType.COMMON_GEMS);
                    foreach (RoomCard card in common_gems.Cards)
                    {
                        MelonLogger.Msg($"Common Gem Card: {card.Template.Prefab.name} PoolCount: {card.Template.PoolCount} RemoveFromPool: {card.Template.RemoveFromPool}");
                    }
                    RoomDeck rare = __instance.GetCorrectDeck(RoomDraftType.RARE);
                    foreach (RoomCard card in rare.Cards)
                    {
                        MelonLogger.Msg($"Rare Card: {card.Template.Prefab.name} PoolCount: {card.Template.PoolCount} RemoveFromPool: {card.Template.RemoveFromPool}");
                    }
                    RoomDeck rare_gems = __instance.GetCorrectDeck(RoomDraftType.RARE_GEMS);
                    foreach (RoomCard card in rare_gems.Cards)
                    {
                        MelonLogger.Msg($"Rare Gem Card: {card.Template.Prefab.name} PoolCount: {card.Template.PoolCount} RemoveFromPool: {card.Template.RemoveFromPool}");
                    }
                }
            }
        }*/

        /*[HarmonyPatch(typeof(RoomDeck), new System.Type[] { })]
        public class RoomDeckConstructorPatch
        {
            public static void Postfix(RoomDeck __instance)
            {
                roomDecks.Add(__instance);
                deckCount++;
                MelonLogger.Msg($"RoomDeck created. Total decks: {deckCount}");
            }
        }*/

        [HarmonyPatch(typeof(Room), nameof(Room.Initialize))]
        public class RoomInitializePatch
        {
            public static void Postfix(Room __instance)
            {
                MelonLogger.Msg($"Room initialized: {__instance.name} - {__instance.gameObject.name}");
                if (roomDecks.Count > 0 && __instance.name.ToLower().Contains("entrance hall"))
                {
                    //ArchipelaBlueMod.isInitialized = true;
                    foreach (RoomDatabase room in roomDecks)
                    {
                        MelonLogger.Msg($"Room Count: {room.Rooms.Count}");
                        /*foreach (RoomTemplate template in room.Rooms)
                        {
                            MelonLogger.Msg($"RoomCard: {template.Prefab.name} - PoolCount: {template.PoolCount} - RemoveFromPool: {template.RemoveFromPool}");
                        }
                        PlayMakerFSM planManagement = null;
                        foreach (PlayMakerFSM fsm in PlayMakerFSM.fsmList)
                        {
                            if (fsm.gameObject.name == "PLAN MANAGEMENT")
                            {
                                planManagement = fsm;
                                break;
                            }
                            MelonLogger.Msg($"GameObject: {fsm.gameObject.name}");
                            // You can add custom logic here if needed
                        }
                        foreach (NamedVariable variable in planManagement.FsmVariables.allVariables)
                        {
                            MelonLogger.Msg($"Variable: {variable.Name} - Type: {variable.VariableType}");
                        }*/
                    }
                }
            }
        }
    }
}
