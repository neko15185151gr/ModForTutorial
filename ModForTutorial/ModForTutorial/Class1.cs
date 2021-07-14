using System;
using System.Collections.Generic;
using HarmonyLib;
using BepInEx;
using UnityEngine;
using System.Reflection;
using UnityEngine.XR;
using Photon.Pun;
using System.IO;
using System.Net;
using Photon.Realtime;
using UnityEngine.Rendering;

namespace ModForTutorial
{
    [BepInPlugin("org.fchb1239.monkeytag.RightTriggerNoGrav", "Right Trigger No Grav", "0.0.6.9")]
    public class MyPatcher : BaseUnityPlugin
    {
        public void Awake()
        {
            var harmony = new Harmony("com.fchb1239.monkeytag.RightTriggerNoGrav");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Update", MethodType.Normal)]
    public class Class1
    {
        static bool noGrav = false;
        static void Postfix(GorillaLocomotion.Player __instance)
        {
            {
                List<InputDevice> list = new List<InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller, list);
                list[0].TryGetFeatureValue(CommonUsages.triggerButton, out noGrav);

                if(noGrav)
                {
                    __instance.bodyCollider.attachedRigidbody.useGravity = false;
                }
                else
                {
                    __instance.bodyCollider.attachedRigidbody.useGravity = true;
                }
            }
        }
    }
}
