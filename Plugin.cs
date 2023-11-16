using System;
using BepInEx;
using UnityEngine;
using Utilla;
using GorillaLocomotion;
using UnityEngine.UI;
using System.Threading.Tasks;
using static FlagCauldronColorer;
using System.Xml.Linq;
using System.IO;
using System.Reflection;

namespace SizeMonke
{
	[ModdedGamemode]
	[BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
	[BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]


	public class Plugin : BaseUnityPlugin
	{
		bool GrrrWait = false;
		bool inRoom;
		Text COCText;
        Text alttext;
        GameObject obj;
        GameObject CanvasHolder;
        float size = 1f;
        Text text;


        // Mod by BasilLmao, UI By Octoburr/Bark
        async void Start()
		{
            Utilla.Events.GameInitialized += OnGameInitialized;
		}


		async void OnGameInitialized(object sender, EventArgs e)
		{

            CanvasHolder = new GameObject("CanvasHolder");

            CanvasHolder.transform.SetParent(GorillaLocomotion.Player.Instance.leftControllerTransform);
            CanvasHolder.transform.localPosition = new Vector3(0f, -0.091f, 0f);
            CanvasHolder.transform.localScale = new Vector3(0.0025f, 0.0025f, 0.0025f);
            CanvasHolder.transform.localRotation = Quaternion.Euler(75f, 0f, 0f);

            CanvasHolder.AddComponent<Canvas>();

            CanvasHolder.GetComponent<Canvas>().name = "SizeMonkeUI";

            GameObject TextObject = new GameObject("Text");
            TextObject.transform.SetParent(CanvasHolder.transform, false);

            text = TextObject.AddComponent<Text>();
            text.font = GorillaTagger.Instance.offlineVRRig.playerText.font;
            text.text = "SIZE: " + size;
            obj = GameObject.Find("SizeMonkeUI/Text");
            obj.name = "SizeMonkeUI Text";

            obj = GameObject.Find("SizeMonkeUI/SizeMonkeUI Text");


            COCText = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/CodeOfConduct/COC Text").GetComponent<Text>();
            alttext = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/UI/CodeOfConduct").GetComponent<Text>();
            COCText.text = "THANK YOU FOR USING SIZE MONKE!\nCONTROLS:\n\nA - SIZE UP\n\nX - SIZE DOWN\n\nY - GO BACK TO DEFAULT\n\n\nTHIS MOD ONLY WORKS IN MODDED LOBBIES.\nMOD BY BASILLMAO\nHAND UI BY OCTOBURR/BARK";
        }

        void Update()
        {
            GorillaLocomotion.Player playerInstance = GorillaLocomotion.Player.Instance;
            playerInstance.scale = size;
            alttext.text = "SIZE: " + size;
            text.text = "SIZE: " + size;
            if (inRoom)
            {
                if (ControllerInputPoller.instance.rightControllerPrimaryButton)
                {
                    if (!GrrrWait)
                    {
                        size += 0.5f;
                        GrrrWait = true;
                    }
                }
                else if (ControllerInputPoller.instance.leftControllerPrimaryButton)
                {
                    if (!GrrrWait && size > 0.1f)
                    {
                        size -= 0.1f;
                        GrrrWait = true;
                    }
                }
                else if (ControllerInputPoller.instance.leftControllerSecondaryButton)
                {
                    size = 1f;
                }
                else
                {
                    GrrrWait = false;
                }
            }
            else
            {
                size = 1f;
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
            GorillaLocomotion.Player playerInstance = GorillaLocomotion.Player.Instance;
            size = 1f;
            inRoom = false;
		}
    }
}


