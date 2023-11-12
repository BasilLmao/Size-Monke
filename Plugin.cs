using System;
using BepInEx;
using UnityEngine;
using Utilla;
using GorillaLocomotion;
using UnityEngine.UI;
using System.Threading.Tasks;

namespace SizeMonke
{
	/// <summary>
	/// This is your mod's main class.
	/// </summary>

	/* This attribute tells Utilla to look for [ModdedGameJoin] and [ModdedGameLeave] */
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
	public class Plugin : BaseUnityPlugin
	{
		bool GrrrWait = false;
		bool inRoom;
		Text COCText;

        async void Start()
		{

            Utilla.Events.GameInitialized += OnGameInitialized;
		}

		void OnEnable()
		{
			/* Set up your mod here */
			/* Code here runs at the start and whenever your mod is enabled*/

			HarmonyPatches.ApplyHarmonyPatches();
		}

		void OnDisable()
		{
			/* Undo mod setup here */
			/* This provides support for toggling mods with ComputerInterface, please implement it :) */
			/* Code here runs whenever your mod is disabled (including if it disabled on startup)*/

			HarmonyPatches.RemoveHarmonyPatches();
		}

		void OnGameInitialized(object sender, EventArgs e)
		{
            COCText = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/CodeOfConduct/COC Text").GetComponent<Text>();
        }

        void Update()
        {
			if (inRoom)
			{
				if (ControllerInputPoller.instance.rightControllerPrimaryButton)
				{
					Debug.Log("Sizing Monke Up!");
					GorillaLocomotion.Player playerInstance = GorillaLocomotion.Player.Instance;

					if (playerInstance != null)
					{
						playerInstance.scale += 0.5f;
					}

					GrrrWait = true;
				} else if (ControllerInputPoller.instance.leftControllerPrimaryButton)
				{
                    Debug.Log("Sizing Monke Down!");
                    GorillaLocomotion.Player playerInstance = GorillaLocomotion.Player.Instance;

					if (playerInstance != null)
					{
						playerInstance.scale -= 0.5f;
					}

					GrrrWait = true;
				} else
				{
					GrrrWait = false;
				}
			}
			else
			{
                if (ControllerInputPoller.instance.rightControllerPrimaryButton || ControllerInputPoller.instance.leftControllerPrimaryButton)
				{
					Debug.Log("This fella isnt in a MODDED room! mods kill his uncle");
				}

                
			}
            
        }


        [ModdedGamemodeJoin]
		public void OnJoin(string gamemode)
		{


			inRoom = true;
		}

		[ModdedGamemodeLeave]
		public void OnLeave(string gamemode)
		{


			inRoom = false;
		}
	}
}
