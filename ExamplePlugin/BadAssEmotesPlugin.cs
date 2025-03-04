using BepInEx;
using BepInEx.Configuration;
using EmotesAPI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Networking;
using BepInEx.Bootstrap;
using BadAssEmotes;
using LethalEmotesAPI.ImportV2;

namespace ExamplePlugin
{
    [BepInDependency("com.weliveinasociety.CustomEmotesAPI")]
    [BepInDependency("com.valve.CSS", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    public class BadAssEmotesPlugin : BaseUnityPlugin
    {
        public const string PluginGUID = "com.weliveinasociety.badasscompany";
        public const string PluginAuthor = "Nunchuk";
        public const string PluginName = "BadAssCompany";
        public const string PluginVersion = "1.2.2";
        int stageInt = -1;
        int pressInt = -1;
        internal static GameObject stage;
        internal static GameObject press;
        internal static GameObject pressMechanism;
        internal static LivingParticleArrayController LPAC;
        public static BadAssEmotesPlugin instance;
        static List<string> HatKidDances = new List<string>();
        public static PluginInfo PInfo { get; private set; }

        //internal static void TestFunction(BoneMapper mapper)
        //{
        //    mapper.audioObjects[mapper.currentClip.syncPos].GetComponent<AudioSource>().PlayOneShot(BoneMapper.primaryAudioClips[mapper.currentClip.syncPos][mapper.currEvent]); //replace this with the audio manager eventually
        //}
        public void Awake()
        {
            instance = this;
            PInfo = Info;
            Assets.LoadAssetBundlesFromFolder("assetbundles");


            Settings.Setup();
            CustomEmoteParams param = new CustomEmoteParams();
            param.primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/Breakin.anim") };
            param.audioLoops = false;
            param.primaryAudioClips = [Assets.Load<AudioClip>($"assets/compressedaudio/breakin\'.ogg")];
            param.syncAnim = true;
            param.syncAudio = true;
            param.lockType = AnimationClipParams.LockType.headBobbing;
            param.primaryDMCAFreeAudioClips = [Assets.Load<AudioClip>($"assets/DMCAMusic/Breakin\'_NNTranscription.ogg")];
            param.displayName = "Breakin";
            EmoteImporter.ImportEmote(param);
            AddAnimation("Breakneck", new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/Breakneck.ogg") }, null, true, true, true, AnimationClipParams.LockType.headBobbing, "", new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/Breakneck.ogg") }, null, false, .7f);
            AddAnimation("Crabby", "Crabby", "", true, true, true, AnimationClipParams.LockType.rootMotion, "", "Crabby", "", false, .7f);
            AddAnimation("Dabstand", "Dabstand", "", false, false, false, AnimationClipParams.LockType.headBobbing, "", "Dabstand", "", false, .05f);
            AddAnimation("DanceMoves", "Fortnite default dance sound", "", false, true, true, AnimationClipParams.LockType.headBobbing, "Default Dance", "Fortnite default dance sound", "", false, .7f);
            AddAnimation("DanceTherapyIntro", "DanceTherapyLoop", "Dance Therapy Intro", "Dance Therapy Loop", true, true, true, AnimationClipParams.LockType.headBobbing, "Dance Therapy", "Dance Therapy Intro", "Dance Therapy Loop", false, .7f);
            AddAnimation("DeepDab", "Dabstand", "", false, false, false, AnimationClipParams.LockType.rootMotion, "Deep Dab", "Dabstand", "", false, .05f);
            AddAnimation("Droop", "Droop", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Droop", "", false, .7f);
            AddAnimation("ElectroSwing", "ElectroSwing", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Electro Swing", "ElectroSwing", "", false, .7f);
            AddAnimation("Extraterrestial", "Extraterestrial", "", true, true, true, AnimationClipParams.LockType.rootMotion, "", "Extraterestrial", "", false, .7f);
            AddAnimation("FancyFeet", "FancyFeet", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Fancy Feet", "FancyFeet", "", false, .7f);
            AddAnimation("FlamencoIntro", "FlamencoLoop", "Flamenco", "FlamencoLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "Flamenco", "FlamencoLoop", "", false, .7f);
            AddAnimation("Floss", "Floss", "", true, true, false, AnimationClipParams.LockType.rootMotion, "", "Floss", "", false, .5f);
            AddAnimation("Fresh", "Fresh", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Fresh", "", false, .7f);
            AddAnimation("Hype", "Hype", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Hype", "", false, .7f);
            AddAnimation("Infectious", "Infectious", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Infectious", "", false, .7f);
            AddAnimation("InfinidabIntro", "InfinidabLoop", "InfinidabIntro", "InfinidabLoop", true, true, false, AnimationClipParams.LockType.headBobbing, "Infinidab", "InfinidabLoop", "", false, .1f);
            //AddAnimation("IntensityIntro", "Intensity", "IntensityLoop", true, true, "");//TODO "Intensity" is not a wav file, get help
            AddAnimation("NeverGonna", "Never Gonna Give you Up", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Never Gonna", "Never Gonna Give you Up", "", true, .7f);
            AddAnimation("NinjaStyle", "NinjaStyle", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Ninja Style", "NinjaStyle", "", false, .7f);
            AddAnimation("OldSchool", "Old School", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Oldschool", "Old School", "", false, .7f);
            AddAnimation("OrangeJustice", "Orange Justice", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Orange Justice", "Orange Justice", "", false, .6f);
            AddAnimation("Overdrive", "Overdrive", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Overdrive", "", false, .7f);
            AddAnimation("PawsAndClaws", "im a cat", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Paws and Claws", "im a cat", "", false, .7f);
            AddAnimation("PhoneItIn", "Phone It In", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Phone It In", "Phone It In", "", false, .7f);
            AddAnimation("PopLock", "PopLock", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Pop Lock", "PopLock", "", false, .7f);
            AddAnimation("Scenario", "Scenario", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Scenario", "", false, .7f);
            AddAnimation("SpringLoaded", "SpringLoaded", "", true, false, false, AnimationClipParams.LockType.headBobbing, "Spring Loaded", "SpringLoaded", "", false, .1f);
            AddAnimation("Springy", "Springy", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Springy", "", false, .7f);
            //AddAnimation("SquatKickIntro", "SquatKick", "SquatKickLoop", true, true, "");//TODO "SquatKickLoop" is not a wav file


            //update 1
            AddAnimation("AnkhaZone", "AnkhaZone", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "AnkhaZone", "", true, .7f);
            AddAnimation("GangnamStyle", "GangnamStyle", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Gangnam Style", "GangnamStyle", "", true, .7f);
            AddAnimation("DontStart", "DontStart", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Don't Start", "DontStart", "", true, .7f);
            AddAnimation("BunnyHop", "BunnyHop", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Bunny Hop", "BunnyHop", "", false, .4f);
            AddAnimation("BestMates", "BestMates", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Best Mates", "BestMates", "", false, .7f);
            AddAnimation("JackOPose", "", "", true, false, false, AnimationClipParams.LockType.headBobbing, "Jack-O Crouch", "", "", false, 0f);
            AddAnimation("Crackdown", "Crackdown", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Crackdown", "", false, .7f);
            AddAnimation("Thicc", "Thicc", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Thicc", "", false, .7f);
            AddAnimation("TakeTheL", "TakeTheL", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Take The L", "TakeTheL", "", false, .8f);
            AddAnimation("LetsDanceBoys", "LetsDanceBoys", "", true, true, true, AnimationClipParams.LockType.rootMotion, "Let's Dance Boys", "LetsDanceBoys", "", false, .7f);
            AddAnimation("BlindingLightsIntro", "BlindingLights", "BlindingLightsIntro", "BlindingLightsLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "Blinding Lights", "BlindingLightsIntro", "BlindingLightsLoop", true, .7f);
            AddAnimation("ImDiamond", "ImDiamond", "", true, true, true, AnimationClipParams.LockType.headBobbing, "I'm Diamond", "ImDiamond", "", true, .7f);
            AddAnimation("ItsDynamite", "ItsDynamite", "", true, true, true, AnimationClipParams.LockType.headBobbing, "It's Dynamite", "ItsDynamite", "", true, .7f);
            AddAnimation("TheRobot", "TheRobot", "", true, true, true, AnimationClipParams.LockType.headBobbing, "The Robot", "TheRobot", "", false, .7f);
            AddAnimation("Cartwheelin", "Cartwheelin", "", true, false, false, AnimationClipParams.LockType.headBobbing, "", "Cartwheelin", "", false, .1f);
            AddAnimation("CrazyFeet", "CrazyFeet", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Crazy Feet", "CrazyFeet", "", false, .7f);
            AddAnimation("FullTilt", "FullTilt", "", true, true, true, AnimationClipParams.LockType.rootMotion, "Full Tilt", "FullTilt", "", false, .1f);
            AddAnimation("FloorSamus", "", "", true, false, false, AnimationClipParams.LockType.headBobbing, "Samus Crawl", "", "", false, 0f);
            AddAnimation("DEDEDE", "", "", true, false, false, AnimationClipParams.LockType.headBobbing, "", "", "", false, 0f);
            AddAnimation("Specialist", "Specialist", "", false, true, true, AnimationClipParams.LockType.rootMotion, "The Specialist", "Specialist", "", false, .7f);



            //Update 2
            AddStartAndJoinAnim(new string[] { "PPmusic", "PPmusicFollow" }, "PPmusic", true, true, true, AnimationClipParams.LockType.headBobbing, "Penis Music", "PPmusic", false, .7f);
            AddAnimation("GetDown", "GetDown", "", false, true, true, AnimationClipParams.LockType.rootMotion, "Get Down", "GetDown", "", true, .7f);
            AddAnimation("Yakuza", "Yakuza", "", true, true, true, AnimationClipParams.LockType.rootMotion, "Koi no Disco Queen", "Yakuza", "", false, .7f);
            AddAnimation("Miku", "Miku", "", true, true, true, AnimationClipParams.LockType.rootMotion, "", "Miku", "", true, .7f);
            AddAnimation("Horny", new string[] { "Horny", "TeddyLoid - ME!ME!ME! feat. Daoko" }, true, true, true, AnimationClipParams.LockType.headBobbing, "", new string[] { "Horny", "TeddyLoid - ME!ME!ME! feat. Daoko" }, true, .7f);
            AddAnimation("GangTorture", "GangTorture", "", false, true, true, AnimationClipParams.LockType.rootMotion, "", "GangTorture", "", true, .7f);
            AddAnimation("PoseBurter", "GinyuForce", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Burter Pose", "GinyuForce", "", false, .7f);
            AddAnimation("PoseGinyu", "GinyuForce", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Ginyu Pose", "GinyuForce", "", false, .7f);
            AddAnimation("PoseGuldo", "GinyuForce", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Guldo Pose", "GinyuForce", "", false, .7f);
            AddAnimation("PoseJeice", "GinyuForce", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Jeice Pose", "GinyuForce", "", false, .7f);
            AddAnimation("PoseRecoome", "GinyuForce", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Recoome Pose", "GinyuForce", "", false, .7f);
            //TODO come back to this
            //AddAnimation("StoodHere", new string[] { "Play_StandingHere2" }, "StandingHere2", true, true, false, new JoinSpot[] { new JoinSpot("StandingHereJoinSpot", new Vector3(0, 0, 2)) });
            //EmoteImporter.ImportEmote(Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/StandingHere.anim"), true, visible: false);
            //BoneMapper.animClips["StandingHere"].vulnerableEmote = true;
            AddAnimation("Carson", "Carson", "", false, true, true, AnimationClipParams.LockType.headBobbing, "", "Carson", "", true, .7f);
            AddAnimation("Frolic", "Frolic", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Frolic", "", true, .7f);
            AddAnimation("MoveIt", "MoveIt", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Move It", "MoveIt", "", true, .7f);
            AddStartAndJoinAnim(new string[] { "ByTheFireLead", "ByTheFirePartner" }, "ByTheFire", true, true, true, AnimationClipParams.LockType.headBobbing, "By The Fire", "ByTheFire", true, .7f);
            AddStartAndJoinAnim(new string[] { "SwayLead", "SwayPartner" }, "Sway", true, true, true, AnimationClipParams.LockType.headBobbing, "Sway", "Sway", true, .7f);
            AddAnimation("Macarena", "Macarena", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Macarena", "", true, .7f);
            AddAnimation("Thanos", "Thanos", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Thanos", "", true, .7f);
            AddAnimation("StarGet", new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/starget2.ogg"), Assets.Load<AudioClip>($"assets/compressedaudio/starget3.ogg"), Assets.Load<AudioClip>($"assets/compressedaudio/starget4.ogg"), Assets.Load<AudioClip>($"assets/compressedaudio/starget5.ogg"), Assets.Load<AudioClip>($"assets/compressedaudio/starget6.ogg"), Assets.Load<AudioClip>($"assets/compressedaudio/starget7.ogg") }, null, false, false, false, AnimationClipParams.LockType.rootMotion, "Star Get", new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/starget2_NNTranscription.ogg"), Assets.Load<AudioClip>($"assets/DMCAMusic/starget3_NNTranscription.ogg"), Assets.Load<AudioClip>($"assets/DMCAMusic/starget4_NNTranscription.ogg"), Assets.Load<AudioClip>($"assets/DMCAMusic/starget5_NNTranscription.ogg"), Assets.Load<AudioClip>($"assets/DMCAMusic/starget6_NNTranscription.ogg"), Assets.Load<AudioClip>($"assets/DMCAMusic/starget7_NNTranscription.ogg") }, null, false, .6f);
            AddAnimation("Poyo", "Poyo", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Poyo", "", false, .7f);
            //AddAnimation("Hi", "Hi", false, false, false);//TODO no hi wav
            AddAnimation("Chika", "Chika", "", false, true, true, AnimationClipParams.LockType.rootMotion, "", "Chika", "", true, .7f);
            AddAnimation("Goopie", "Goopie", "", false, true, true, AnimationClipParams.LockType.headBobbing, "", "Goopie", "", true, .7f);
            //AddAnimation("Sad", "Sad", false, true, true, "");//TODO no sad wav
            AddAnimation("Crossbounce", "Crossbounce", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Crossbounce", "", false, .7f);
            AddAnimation("Butt", "Butt", "", false, false, false, AnimationClipParams.LockType.lockHead, "", "Butt", "", false, .5f);



            //update 3
            //AddAnimation("SuperIdol", "SuperIdol", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Super Idol");//TODO second anim?
            AddAnimation("MakeItRainIntro", "MakeItRainLoop", "MakeItRainIntro", "MakeItRainLoop", true, true, true, AnimationClipParams.LockType.rootMotion, "Make it Rain", "MakeItRainLoop", "", true, .7f);
            AddAnimation("Penguin", "Penguin", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Club Penguin", "Penguin", "", false, .4f);
            AddAnimation("DesertRivers", "DesertRivers", "", false, true, true, AnimationClipParams.LockType.rootMotion, "Rivers in the Dessert", "DesertRivers", "", false, .7f);
            AddAnimation("HondaStep", "HondaStep", "", false, true, true, AnimationClipParams.LockType.rootMotion, "Step!", "HondaStep", "", true, .7f);
            AddAnimation("UGotThat", "UGotThat", "", false, true, true, AnimationClipParams.LockType.rootMotion, "U Got That", "UGotThat", "", true, .7f);



            //update 4
            AddAnimation("OfficerEarl", "", "", true, false, false, AnimationClipParams.LockType.headBobbing, "Officer Earl", "", "", false, 0f);
            AddAnimation("Cirno", "Cirno", "", false, true, true, AnimationClipParams.LockType.rootMotion, "", "Cirno", "", false, .7f);
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/Haruhi.anim") }, audioLoops = false, primaryAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/Haruhi.ogg"), Assets.Load<AudioClip>($"assets/compressedaudio/HaruhiYoung.ogg") }, syncAnim = true, syncAudio = true, startPref = 0, joinPref = 0, joinSpots = new JoinSpot[] { new JoinSpot("Yuki_Nagato", new Vector3(1.5f, 0, -1.5f), Vector3.zero, Vector3.one, true), new JoinSpot("Mikuru_Asahina", new Vector3(-1.5f, 0, -1.5f), Vector3.zero, Vector3.one, true) }, internalName = "Hare Hare Yukai", lockType = AnimationClipParams.LockType.rootMotion, willGetClaimedByDMCA = true, primaryDMCAFreeAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/Haruhi_NNTranscription.ogg"), Assets.Load<AudioClip>($"assets/DMCAMusic/HaruhiYoung_NNTranscription.ogg") }, audioLevel = .7f, displayName = "Hare Hare Yukai" });
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/Yuki_Nagato.anim") }, audioLoops = false, syncAnim = true, visible = false, lockType = AnimationClipParams.LockType.lockHead, audioLevel = 0, displayName = "Hare Hare Yukai" });
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/Mikuru_Asahina.anim") }, audioLoops = false, syncAnim = true, visible = false, lockType = AnimationClipParams.LockType.lockHead, audioLevel = 0, displayName = "Hare Hare Yukai" });

            BoneMapper.animClips[$"{PluginGUID}__Yuki_Nagato"].vulnerableEmote = true;
            BoneMapper.animClips[$"{PluginGUID}__Mikuru_Asahina"].vulnerableEmote = true;
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/GGGG.anim"), Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/GGGG2.anim"), Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/GGGG3.anim") }, audioLoops = false, primaryAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/GGGG.ogg") }, syncAnim = true, syncAudio = true, startPref = -2, joinPref = -2, lockType = AnimationClipParams.LockType.rootMotion, willGetClaimedByDMCA = true, primaryDMCAFreeAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/GGGG_NNTranscription.ogg") }, audioLevel = .7f, displayName = "GGGG" });
            AddAnimation("Shufflin", "Shufflin", "", false, true, true, AnimationClipParams.LockType.rootMotion, "", "Shufflin", "", true, .7f);
            //AddStartAndJoinAnim(new string[] { "Train", "TrainPassenger" }, "Train", new string[] { "Trainloop", "TrainPassenger" }, true, false, true, true, true);
            //BoneMapper.animClips["Train"].vulnerableEmote = true;
            //CustomEmotesAPI.BlackListEmote("Train");

            AddAnimation("BimBamBomTest", "BimBamBom", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Bim Bam Boom", "BimBamBom", "", true, .7f);
            AddAnimation("Savage", "Savage", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Savage", "", true, .7f);
            AddAnimation("Stuck", "Stuck", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Stuck", "", true, .7f);
            AddAnimation("Roflcopter", "Roflcopter", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Roflcopter", "", false, .7f);
            AddAnimation("Float", "Float", "", false, true, true, AnimationClipParams.LockType.headBobbing, "", "Float", "", false, .4f);
            AddAnimation("Rollie", "Rollie", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Rollie", "", true, .7f);
            //BoneMapper.animClips["Rollie"].localTransforms = true;
            //BoneMapper.animClips["Rollie"].soloIgnoredBones = [HumanBodyBones.Hips, HumanBodyBones.Spine, HumanBodyBones.Head];
            //BoneMapper.animClips["Rollie"].rootIgnoredBones = [HumanBodyBones.LeftUpperLeg, HumanBodyBones.RightUpperLeg];



            AddAnimation("GalaxyObservatory", new string[] { "GalaxyObservatory1", "GalaxyObservatory2", "GalaxyObservatory3" }, true, true, true, AnimationClipParams.LockType.rootMotion, "Galaxy Observatory", new string[] { "GalaxyObservatory1", "GalaxyObservatory2", "GalaxyObservatory3" }, false, .5f);
            AddAnimation("Markiplier", "Markiplier", "", false, false, false, AnimationClipParams.LockType.rootMotion, "", "Markiplier", "", true, .6f);
            AddAnimation("DevilSpawn", "DevilSpawn", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "DevilSpawn", "", true, .7f);
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/DuckThisOneIdle.anim") }, audioLoops = true, primaryAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/DuckThisOneIdle.ogg") }, joinSpots = new JoinSpot[] { new JoinSpot("DuckThisJoinSpot", new Vector3(0, 0, 2), Vector3.zero, Vector3.one, true) }, lockType = AnimationClipParams.LockType.lockHead, internalName = "Duck This One", primaryDMCAFreeAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/DuckThisOneIdle_NNTranscription.ogg") }, willGetClaimedByDMCA = false, audioLevel = .7f, displayName = "Duck This One", preventMovement = true });
            //CustomEmotesAPI.BlackListEmote("DuckThisOneIdle");
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/DuckThisOne.anim") }, audioLoops = false, primaryAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/DuckThisOne.ogg") }, visible = false, lockType = AnimationClipParams.LockType.lockHead, primaryDMCAFreeAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/DuckThisOne.ogg") }, displayName = "Duck This One" });
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/DuckThisOneJoin.anim") }, audioLoops = false, visible = false, lockType = AnimationClipParams.LockType.lockHead, displayName = "Duck This One" });
            BoneMapper.animClips[$"{PluginGUID}__Duck This One"].vulnerableEmote = true;
            BoneMapper.animClips[$"{PluginGUID}__DuckThisOne"].vulnerableEmote = true;
            BoneMapper.animClips[$"{PluginGUID}__DuckThisOneJoin"].vulnerableEmote = true;
            AddAnimation("Griddy", "Griddy", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Griddy", "", true, .7f);
            AddAnimation("Tidy", "Tidy", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Tidy", "", true, .7f);
            AddAnimation("Toosie", "Toosie", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Toosie", "", true, .7f);
            AddAnimation("INEEDAMEDICBAG", new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/INEEDAMEDICBAG1.ogg"), Assets.Load<AudioClip>($"assets/compressedaudio/INEEDAMEDICBAG2.ogg") }, null, false, false, false, AnimationClipParams.LockType.headBobbing, "I NEED A MEDIC BAG", new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/INEEDAMEDICBAG1.ogg"), Assets.Load<AudioClip>($"assets/compressedaudio/INEEDAMEDICBAG2.ogg") }, null, false, .9f);
            AddAnimation("Smoke", "Smoke", "", true, true, true, AnimationClipParams.LockType.rootMotion, "Ralsei Dies", "Smoke", "", false, .7f);
            AddAnimation("FamilyGuyDeath", "", "", true, false, false, AnimationClipParams.LockType.headBobbing, "Family Guy Death Pose", "", "", false, 0f);
            AddAnimation("Panda", "", "", false, false, false, AnimationClipParams.LockType.rootMotion, "", "", "", false, 0f);
            AddAnimation("Yamcha", "", "", true, false, false, AnimationClipParams.LockType.headBobbing, "Yamcha Death Pose", "", "", false, 0f);


            //update 5
            AddAnimation("Thriller", "Thriller", "", true, true, true, AnimationClipParams.LockType.rootMotion, "", "Thriller", "", true, .7f);
            AddAnimation("Wess", "Wess", "", false, true, true, AnimationClipParams.LockType.headBobbing, "", "Wess", "", true, .7f);
            AddAnimation("Distraction", "Distraction", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Distraction Dance", "Distraction", "", false, 1f);
            AddAnimation("Security", "Security", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Last Surprise", "Security", "", false, .7f);


            //update 6
            AddAnimation("KillMeBaby", "KillMeBaby", "", false, true, true, AnimationClipParams.LockType.headBobbing, "Kill Me Baby", "KillMeBaby", "", true, .7f);
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = [Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/MyWorld.anim")], audioLoops = true, primaryAudioClips = [Assets.Load<AudioClip>($"assets/compressedaudio/MyWorld.ogg")], syncAnim = true, syncAudio = true, joinSpots = new JoinSpot[] { new JoinSpot("MyWorldJoinSpot", new Vector3(2, 0, 0), Vector3.zero, Vector3.one, true) }, lockType = AnimationClipParams.LockType.headBobbing, primaryDMCAFreeAudioClips = [Assets.Load<AudioClip>($"assets/DMCAMusic/MyWorld_NNTranscription.ogg")], audioLevel = .7f, willGetClaimedByDMCA = true, displayName = "My World" });
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = [Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/MyWorldJoin.anim")], audioLoops = true, primaryAudioClips = [Assets.Load<AudioClip>($"assets/compressedaudio/MyWorld.ogg")], syncAnim = true, syncAudio = true, visible = false, lockType = AnimationClipParams.LockType.headBobbing, audioLevel = 0, primaryDMCAFreeAudioClips = [Assets.Load<AudioClip>($"assets/DMCAMusic/MyWorld_NNTranscription.ogg")], willGetClaimedByDMCA = true, displayName = "My World" });
            BoneMapper.animClips[$"{PluginGUID}__MyWorldJoin"].syncPos--;
            BoneMapper.animClips[$"{PluginGUID}__MyWorldJoin"].vulnerableEmote = true;
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/VSWORLD.anim") }, audioLoops = true, primaryAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/VSWORLD.ogg") }, syncAnim = true, syncAudio = true, joinSpots = new JoinSpot[] { new JoinSpot("VSWORLDJoinSpot", new Vector3(-2, 0, 0), Vector3.zero, Vector3.one, true) }, lockType = AnimationClipParams.LockType.headBobbing, primaryDMCAFreeAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/VSWORLD_NNTranscription.ogg") }, audioLevel = .7f, displayName = "VSWORLD" });
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/VSWORLDLEFT.anim") }, audioLoops = true, primaryAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/VSWORLD.ogg") }, syncAnim = true, syncAudio = true, visible = false, lockType = AnimationClipParams.LockType.headBobbing, primaryDMCAFreeAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/VSWORLD_NNTranscription.ogg") }, displayName = "VSWORLD" });
            BoneMapper.animClips[$"{PluginGUID}__VSWORLDLEFT"].syncPos--;
            BoneMapper.animClips[$"{PluginGUID}__VSWORLDLEFT"].vulnerableEmote = true;
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/ChugJug.anim") }, audioLoops = false, primaryAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/ChugJug.ogg"), Assets.Load<AudioClip>($"assets/compressedaudio/MikuJug.ogg") }, syncAnim = true, syncAudio = true, lockType = AnimationClipParams.LockType.headBobbing, primaryDMCAFreeAudioClips = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/ChugJug_NNTranscription.ogg"), Assets.Load<AudioClip>($"assets/DMCAMusic/MikuJug_NNTranscription.ogg") }, audioLevel = .7f, displayName = "Chug Jug", willGetClaimedByDMCA = true });

            //TODO IFU
            //CustomEmotesAPI.AddNonAnimatingEmote("IFU Stage");
            //CustomEmotesAPI.BlackListEmote("IFU Stage");
            //EmoteImporter.ImportEmote(new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/ifu.anim") }, false, new string[] { "Play_ifu", "Play_ifucover" }, new string[] { "Stop_ifu", "Stop_ifu" }, dimWhenClose: true, syncAnim: true, syncAudio: true, visible: false);
            //EmoteImporter.ImportEmote(new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/ifeleft.anim") }, false, new string[] { "Play_ifu", "Play_ifucover" }, new string[] { "Stop_ifu", "Stop_ifu" }, dimWhenClose: true, syncAnim: true, syncAudio: true, visible: false);
            //BoneMapper.animClips["ifeleft"].syncPos--;
            //EmoteImporter.ImportEmote(new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/ifuright.anim") }, false, new string[] { "Play_ifu", "Play_ifucover" }, new string[] { "Stop_ifu", "Stop_ifu" }, dimWhenClose: true, syncAnim: true, syncAudio: true, visible: false);
            //BoneMapper.animClips["ifuright"].syncPos -= 2;
            //BoneMapper.animClips["ifu"].vulnerableEmote = true;
            //BoneMapper.animClips["ifeleft"].vulnerableEmote = true;
            //BoneMapper.animClips["ifuright"].vulnerableEmote = true;
            //GameObject g2 = Assets.Load<GameObject>($"assets/prefabs/ifustagebasebased.prefab");
            //var g = g2.transform.Find("ifuStage").Find("GameObject").Find("LivingParticlesFloor11_Audio").gameObject;
            //g2.AddComponent<StageHandler>();
            //g2.transform.Find("ifuStage").Find("GameObject").Find("Plane").gameObject.AddComponent<SurfaceDefProvider>().surfaceDef = Addressables.LoadAssetAsync<SurfaceDef>("RoR2/Base/Common/sdMetal.asset").WaitForCompletion();
            //g2.transform.Find("ifuStage").Find("GameObject").Find("stairs front").gameObject.AddComponent<SurfaceDefProvider>().surfaceDef = Addressables.LoadAssetAsync<SurfaceDef>("RoR2/Base/Common/sdMetal.asset").WaitForCompletion();
            //g2.transform.Find("ifuStage").Find("GameObject").Find("stairs left").gameObject.AddComponent<SurfaceDefProvider>().surfaceDef = Addressables.LoadAssetAsync<SurfaceDef>("RoR2/Base/Common/sdMetal.asset").WaitForCompletion();
            //g2.transform.Find("ifuStage").Find("GameObject").Find("stairs right").gameObject.AddComponent<SurfaceDefProvider>().surfaceDef = Addressables.LoadAssetAsync<SurfaceDef>("RoR2/Base/Common/sdMetal.asset").WaitForCompletion();
            //g2.transform.Find("ifuStage").Find("GameObject").Find("stairs back").gameObject.AddComponent<SurfaceDefProvider>().surfaceDef = Addressables.LoadAssetAsync<SurfaceDef>("RoR2/Base/Common/sdMetal.asset").WaitForCompletion();
            //LivingParticlesAudioModule module = g.GetComponent<LivingParticlesAudioModule>();
            //module.audioPosition = g.transform;
            //g.GetComponent<ParticleSystemRenderer>().material.SetFloat("_DistancePower", .5f);
            //g.GetComponent<ParticleSystemRenderer>().material.SetFloat("_NoisePower", 8f);
            //g.GetComponent<ParticleSystemRenderer>().material.SetFloat("_AudioAmplitudeOffsetPower2", 1.5f);
            //stageInt = CustomEmotesAPI.RegisterWorldProp(g2, new JoinSpot[] { new JoinSpot("ifumiddle", new Vector3(0, .4f, 0)), new JoinSpot("ifeleft", new Vector3(-2, .4f, 0)), new JoinSpot("ifuright", new Vector3(2, .4f, 0)) });

            AddAnimation("Summertime", "Summertime", "", false, true, true, AnimationClipParams.LockType.headBobbing, "", "Summertime", "", true, .7f);
            AddAnimation("Dougie", "Dougie", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Dougie", "", true, .7f);



            //Update 7?
            AddAnimation("CaliforniaGirls", "CaliforniaGirls", "", true, true, true, AnimationClipParams.LockType.headBobbing, "California Gurls", "CaliforniaGirls", "", true, .7f);
            AddAnimation("SeeTinh", "SeeTinh", "", false, true, true, AnimationClipParams.LockType.headBobbing, "See T�nh", "SeeTinh", "", true, .7f);


            //Update 8ish
            //CustomEmotesAPI.AddNonAnimatingEmote("Hydraulic Press");
            //CustomEmotesAPI.BlackListEmote("Hydraulic Press");
            AddAnimation("VirtualInsanityIntro", "VirtualInsanityLoop", "VirtualInsanityStart", "VirtualInsanityLoop", false, true, true, AnimationClipParams.LockType.headBobbing, "Virtual Insanity", "VirtualInsanityStart", "VirtualInsanityLoop", true, .5f);


            //9?
            //AddAnimation("Terrestrial", "Terrestrial Start", "Terrestrial", true, true, true); //TODO anim missing? not even in unity
            AddAnimation("Im a Mystery", "Im a Mystery Loop", "Im a Mystery", "Im a Mystery Loop", true, true, true, AnimationClipParams.LockType.headBobbing, "Im a Mystery", "Im a Mystery", "Im a Mystery Loop", true, .5f);
            AddAnimation("Bird", "Bird", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Bird", "Bird", "", false, .7f);
            AddAnimation("Real Slim Shady", "Real Slim Shady Loop", "Real Slim Shady", "Real Slim Shady Loop", true, true, true, AnimationClipParams.LockType.headBobbing, "Real Slim Shady", "Real Slim Shady", "Real Slim Shady Loop", true, .7f);
            AddAnimation("Steady", "Steady", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Steady", "Steady", "", false, .7f);


            //Update 10
            AddAnimation2("AirShredder", "AirShredder", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Air Shredder", "AirShredder", "", false, .5f);
            AddAnimation2("ArmyBunny", "ArmyBunnyLoop", "ArmyBunny", "ArmyBunnyLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "Army Bunny", "ArmyBunny", "ArmyBunnyLoop", true, .5f);
            AddAnimation2("BoneyBounce", "BoneyBounceLoop", "BoneyBounce", "BoneyBounceLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "Boney Bounce", "BoneyBounce", "BoneyBounceLoop", true, .5f);
            AddAnimation2("BoldStance", "BoldStance", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Bold Stance", "BoldStance", "", false, .5f);
            AddAnimation2("Bombastic", "Bombastic", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Bombastic", "", false, .5f);
            AddAnimation2("BounceWit'It", "BounceWit'ItPoop", "BounceWit'It", "BounceWit'ItPoop", true, true, true, AnimationClipParams.LockType.headBobbing, "Bounce Wit'It", "BounceWitIt", "BounceWitItPoop", true, .5f);
            AddAnimation2("Boy'sALier", "Boy'sALier", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Boy's A Lier", "BoysALier", "", true, .5f);
            AddAnimation2("Caffeinated", "CaffeinatedLoop", "Caffeinated", "CaffeinatedLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Caffeinated", "CaffeinatedLoop", true, .5f);
            AddAnimation2("CelebrateMe", "CelebrateMe", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Celebrate Me", "CelebrateMe", "", false, .5f);
            AddAnimation2("CluckStrut", "CluckStrut", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Cluck Strut", "CluckStrut", "", false, .5f);
            AddAnimation2("CongaFT", "Conga", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Conga", "", false, .5f);
            AddAnimation2("Copines", "Copines", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Copines", "", true, .5f);
            AddAnimation2("Cupid'sArrow", "Cupid'sArrowLoop", "Cupid'sArrow", "Cupid'sArrowLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "Cupid's Arrow", "CupidsArrow", "CupidsArrowLoop", true, .5f);
            AddAnimation2("FastFlex", "FastFlexLoop", "FastFlex", "FastFlexLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "Fast Flex", "FastFlex", "FastFlexLoop", true, .5f);
            AddAnimation2("FeelTheFlow", "FeelTheFlow", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Feel The Flow", "FeelTheFlow", "", true, .5f);
            AddAnimation2("GetGone", "GetGone", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Get Gone", "GetGone", "", true, .5f);
            AddAnimation2("GetLoose", "GetLoose", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Get Loose", "GetLoose", "", false, .5f);
            AddAnimation2("Gloss", "Gloss", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Gloss", "", false, .5f);
            AddAnimation2("InHaMood", "InHaMoodLoop", "InHaMood", "InHaMoodLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "In Ha Mood", "InHaMood", "InHaMoodLoop", true, .5f);
            AddAnimation2("MakeSomeWaves", "MakeSomeWaves", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Make Some Waves", "MakeSomeWaves", "", true, .5f);
            AddAnimation2("NanaNana", "NanaNana", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Nana Nana", "NanaNana", "", false, .5f);
            AddAnimation2("NightOut", "NightOut", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Night Out", "NightOut", "", true, .5f);
            AddAnimation2("PointAndStrut", "PointAndStrutLoop", "PointAndStrut", "PointAndStrutLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "Point And Strut", "PointAndStrut", "PointAndStrutLoop", false, .5f);
            AddAnimation2("PrimoMoves", "PrimoMoves", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Primo Moves", "PrimoMoves", "", false, .5f);
            AddAnimation2("RapMonster", "RapMonsterLoop", "RapMonster", "RapMonsterLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "Rap Monster", "RapMonster", "RapMonsterLoop", true, .5f);
            AddAnimation2("RunItDown", "RunItDown", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Run It Down", "RunItDown", "", true, .5f);
            AddAnimation2("Rushin'Around", "Rushin'Around", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Rushin' Around", "RushinAround", "", true, .5f);
            AddAnimation2("ShowYa", "ShowYaLoop", "ShowYa", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Show Ya", "ShowYa", "", true, .5f);
            AddAnimation2("Ska-stra-terrestrial", "Ska-stra-terrestrial", "Ska-stra-terrestrialLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Ska-stra-terrestrial", "Ska-stra-terrestrialLoop", false, .5f);
            AddAnimation2("SugarRush", "SugarRush", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Sugar Rush", "SugarRush", "", false, .5f);
            AddAnimation2("Switchstep", "Switchstep", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Switchstep", "", false, .5f);
            AddAnimation2("YouShouldSeeMeInACrown", "YouShouldSeeMeInACrowLoop", "YouShouldSeeMeInACrown", "YouShouldSeeMeInACrownLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "You Should See Me In A Crown", "YouShouldSeeMeInACrown", "YouShouldSeeMeInACrownLoop", true, .5f);
            AddAnimation2("Zany", "Zany", "", true, true, true, AnimationClipParams.LockType.headBobbing, "", "Zany", "", false, .5f);


            //Update 11
            AddAnimation2("CheerUp", "CheerUp", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Cheer Up", "CheerUp", "", true, .5f);
            AddAnimation2("CrissCross", "CrissCross", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Criss Cross", "CrissCross", "", true, .5f);
            AddAnimation2("NuthinButAGThang", "NuthinButAGThangLoop", "NuthinButAGThang", "NuthinButAGThangLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "Nuthin But A G Thang", "NuthinButAGThang", "NuthinButAGThangLoop", true, .5f);
            AddAnimation2("JabbaSwitchway", "JabbaSwitchwaLoopy", "SwitchAway", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Jabba Switchway", "SwitchAway", "", true, .5f);
            AddAnimation2("ByeByeBye", "ByeByeByeLoop", "ByeByeBye", "ByeByeByeLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "Bye Bye Bye", "ByeByeBye", "ByeByeByeLoop", true, .5f);
            AddAnimation2("GoMufasa", "GoMufasa", "", true, true, true, AnimationClipParams.LockType.headBobbing, "Go Mufasa", "GoMufasa", "", true, .5f);
            AddAnimation2("Snoop'sWalk", "Snoop'sWalkLoop", "Snoop'sWalk", "Snoop'sWalkLoop", true, true, true, AnimationClipParams.LockType.headBobbing, "Snoop's Walk", "Snoop'sWalk", "SnoopsWalkLoop", true, .5f);

            //TODO
            //GameObject pressObject = Assets.Load<GameObject>($"assets/hydrolic/homedepot1.prefab");
            //foreach (var item in pressObject.GetComponentsInChildren<Renderer>())
            //{
            //    foreach (var mat in item.sharedMaterials)
            //    {
            //        //TODO addressables
            //        //mat.shader = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoBody.prefab").WaitForCompletion().GetComponentInChildren<SkinnedMeshRenderer>().material.shader;
            //    }
            //}
            //pressObject.AddComponent<HydrolicPressMechanism>();
            //pressInt = CustomEmotesAPI.RegisterWorldProp(pressObject, new JoinSpot[] { new JoinSpot("HydrolicPressJoinSpot", new Vector3(0, .1f, 0)) });

            CustomEmotesAPI.animChanged += CustomEmotesAPI_animChanged;
            CustomEmotesAPI.emoteSpotJoined_Body += CustomEmotesAPI_emoteSpotJoined_Body;
            CustomEmotesAPI.emoteSpotJoined_Prop += CustomEmotesAPI_emoteSpotJoined_Prop;
            CustomEmotesAPI.boneMapperCreated += CustomEmotesAPI_boneMapperCreated;

            //TODO game over
            //On.RoR2.Run.OnClientGameOver += Run_OnClientGameOver;
        }

        //TODO on all players dead
        //private void Run_OnClientGameOver(On.RoR2.Run.orig_OnClientGameOver orig, Run self, RunReport runReport)
        //{
        //    orig(self, runReport);
        //    if (NetworkServer.active)
        //    {
        //        if (UnityEngine.Random.Range(1, 101) < Settings.EnemyTauntOnDeathChance.Value + 1)
        //        {
        //            foreach (var item in CustomEmotesAPI.GetAllBoneMappers())
        //            {
        //                if (item.mapperBody.GetComponent<TeamComponent>().teamIndex != TeamIndex.Player)
        //                {
        //                    CustomEmotesAPI.PlayAnimation("DanceMoves", item);
        //                }
        //            }
        //        }
        //    }
        //}

        private void CustomEmotesAPI_boneMapperCreated(BoneMapper mapper)
        {
            //TODO foot effectors for IFU
            //if (LPAC && LPAC.affectors.Length != 0 && !mapper.worldProp)
            //{
            //    List<Transform> transforms = new List<Transform>(LPAC.affectors);
            //    foreach (var bone in mapper.smr2.bones)
            //    {
            //        if (bone.GetComponent<EmoteConstraint>() && (bone.GetComponent<EmoteConstraint>().emoteBone == mapper.a2.GetBoneTransform(HumanBodyBones.LeftFoot) || bone.GetComponent<EmoteConstraint>().emoteBone == mapper.a2.GetBoneTransform(HumanBodyBones.RightFoot)))
            //        {
            //            transforms.Add(bone);
            //        }
            //    }
            //    BadAssEmotesPlugin.LPAC.affectors = transforms.ToArray();
            //}
        }

        private void CustomEmotesAPI_emoteSpotJoined_Prop(GameObject emoteSpot, BoneMapper joiner, BoneMapper host)
        {
            string emoteSpotName = emoteSpot.name;
            GameObject g;
            switch (emoteSpotName)
            {
                case "ifumiddle":
                    host.GetComponentsInChildren<Animator>()[1].SetTrigger("Start");
                    joiner.PlayAnim("ifu", 0);
                    g = new GameObject();
                    g.name = "ifumiddle_JoinProp";
                    IFU(joiner, host, emoteSpot, g);
                    break;
                case "ifeleft":
                    host.GetComponentsInChildren<Animator>()[1].SetTrigger("Start");
                    joiner.PlayAnim("ifeleft", 0);
                    g = new GameObject();
                    g.name = "ifeleft_JoinProp";
                    IFU(joiner, host, emoteSpot, g);
                    break;
                case "ifuright":
                    host.GetComponentsInChildren<Animator>()[1].SetTrigger("Start");
                    joiner.PlayAnim("ifuright", 0);
                    g = new GameObject();
                    g.name = "ifuright_JoinProp";
                    IFU(joiner, host, emoteSpot, g);
                    break;
                case "HydrolicPressJoinSpot":
                    g = new GameObject();
                    g.name = "hydrolicpress_JoinProp";
                    HydrolicPress(joiner, host, emoteSpot, g);
                    break;
                default:
                    break;
            }
        }
        private void IFU(BoneMapper joiner, BoneMapper host, GameObject emoteSpot, GameObject g)
        {
            //TODO IFU
            //joiner.props.Add(g);
            //g.transform.SetParent(host.transform);
            //g.transform.localPosition = new Vector3(0, .5f, 0);
            //g.transform.localEulerAngles = Vector3.zero;
            //g.transform.localScale = host.transform.localScale;
            //joiner.AssignParentGameObject(g, true, true, true, true, true);
            //emoteSpot.GetComponent<EmoteLocation>().SetEmoterAndHideLocation(joiner);
            //if (joiner.local)
            //{
            //    localBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
            //    CharacterCameraParamsData data = new CharacterCameraParamsData();
            //    data.fov = 70f;
            //    data.idealLocalCameraPos = new Vector3(0, 1.5f, -16);
            //    if (!fovHandle.isValid)
            //    {
            //        fovHandle = localBody.GetComponentInChildren<EntityStateMachine>().commonComponents.cameraTargetParams.AddParamsOverride(new CameraTargetParams.CameraParamsOverrideRequest
            //        {
            //            cameraParamsData = data
            //        }, 1f);
            //    }
            //}
        }
        private void HydrolicPress(BoneMapper joiner, BoneMapper host, GameObject emoteSpot, GameObject g)
        {
            joiner.props.Add(g);
            g.transform.SetParent(host.transform);
            g.transform.localPosition = new Vector3(0, .03f, 0);
            g.transform.localEulerAngles = Vector3.zero;
            g.transform.localScale = Vector3.one;
            g.AddComponent<HydrolicPressComponent>().boneMapper = joiner;
            joiner.AssignParentGameObject(g, true, false, true, false, false);
            emoteSpot.GetComponent<EmoteLocation>().SetEmoterAndHideLocation(joiner);
            StartCoroutine(WaitForSecondsThenEndEmote(joiner, 10f, g));
        }
        IEnumerator WaitForSecondsThenEndEmote(BoneMapper mapper, float time, GameObject parent)
        {
            yield return new WaitForSeconds(time);
            if (mapper)
            {
                if (mapper.parentGameObject == parent)
                {
                    mapper.preserveProps = true;
                    mapper.AssignParentGameObject(mapper.parentGameObject, false, false, true, false, false);
                    mapper.preserveParent = true;
                    mapper.preserveProps = true;
                    mapper.PlayAnim("none", 0);
                }
            }
        }
        internal IEnumerator WaitForSecondsThenDeleteGameObject(GameObject obj, float time)
        {
            yield return new WaitForSeconds(time);
            if (obj)
            {
                GameObject.Destroy(obj);
            }
        }
        private void CustomEmotesAPI_emoteSpotJoined_Body(GameObject emoteSpot, BoneMapper joiner, BoneMapper host)
        {
            string emoteSpotName = emoteSpot.name;
            if (emoteSpotName == "StandingHereJoinSpot")
            {
                joiner.PlayAnim("StandingHere", 0);
                GameObject g = new GameObject();
                g.name = "StandingHereProp";
                joiner.props.Add(g);
                g.transform.SetParent(host.transform);
                Vector3 scal = host.transform.lossyScale;
                g.transform.localPosition = new Vector3(0, 0, .75f / scal.z);
                g.transform.localEulerAngles = new Vector3(0, 130, 0);
                g.transform.localScale = new Vector3(.8f, .8f, .8f);
                joiner.AssignParentGameObject(g, true, true, true, true, true);
                joiner.SetAnimationSpeed(2);
                emoteSpot.GetComponent<EmoteLocation>().SetEmoterAndHideLocation(joiner);
            }

            if (emoteSpotName == "DuckThisJoinSpot")
            {
                joiner.PlayAnim($"{PluginGUID}__DuckThisOneJoin", 0);

                GameObject g = new GameObject();
                g.name = "DuckThisOneJoinProp";
                joiner.props.Add(g);
                g.transform.SetParent(host.transform);
                Vector3 scal = host.transform.lossyScale;
                g.transform.localPosition = new Vector3(0, 0, 1 / scal.z);
                g.transform.localEulerAngles = new Vector3(0, 180, 0);
                g.transform.localScale = Vector3.one;
                joiner.AssignParentGameObject(g, true, true, true, true, true);


                host.PlayAnim($"{PluginGUID}__DuckThisOne", 0);
                joiner.currentlyLockedBoneMapper = host;


                g = new GameObject();
                g.name = "DuckThisOneHostProp";
                host.props.Add(g);
                g.transform.localPosition = host.transform.position;
                g.transform.localEulerAngles = host.transform.eulerAngles;
                g.transform.localScale = Vector3.one;
                g.transform.SetParent(host.mapperBodyTransform.parent);
                host.AssignParentGameObject(g, true, true, true, true, false);
            }

            if (emoteSpotName == "Yuki_Nagato")
            {
                joiner.PlayAnim($"{PluginGUID}__Yuki_Nagato", 0);
                DebugClass.Log($"{joiner.currentClip is not null}       {host.currentClip is not null}");
                DebugClass.Log($"{host.currentClip.syncPos}");
                DebugClass.Log($"{joiner.currentClip.syncPos}");
                CustomAnimationClip.syncTimer[joiner.currentClip.syncPos] = CustomAnimationClip.syncTimer[host.currentClip.syncPos];
                CustomAnimationClip.syncPlayerCount[joiner.currentClip.syncPos]++;
                joiner.PlayAnim($"{PluginGUID}__Yuki_Nagato", 0);

                CustomAnimationClip.syncPlayerCount[joiner.currentClip.syncPos]--;

                GameObject g = new GameObject();
                g.name = "Yuki_NagatoProp";
                joiner.props.Add(g);
                g.transform.SetParent(host.transform);
                Vector3 scal = host.transform.lossyScale;
                g.transform.localPosition = new Vector3(0, 0, 0);
                g.transform.localEulerAngles = new Vector3(0, 0, 0);
                g.transform.localScale = Vector3.one;
                joiner.AssignParentGameObject(g, true, true, true, true, true);
                joiner.currentlyLockedBoneMapper = host;
                emoteSpot.GetComponent<EmoteLocation>().SetEmoterAndHideLocation(joiner);
            }
            if (emoteSpotName == "Mikuru_Asahina")
            {
                joiner.PlayAnim($"{PluginGUID}__Mikuru_Asahina", 0);
                CustomAnimationClip.syncTimer[joiner.currentClip.syncPos] = CustomAnimationClip.syncTimer[host.currentClip.syncPos];
                CustomAnimationClip.syncPlayerCount[joiner.currentClip.syncPos]++;
                joiner.PlayAnim($"{PluginGUID}__Mikuru_Asahina", 0);
                CustomAnimationClip.syncPlayerCount[joiner.currentClip.syncPos]--;

                GameObject g = new GameObject();
                g.name = "Mikuru_AsahinaProp";
                joiner.props.Add(g);
                g.transform.SetParent(host.transform);
                Vector3 scal = host.transform.lossyScale;
                g.transform.localPosition = new Vector3(0, 0, 0);
                g.transform.localEulerAngles = new Vector3(0, 0, 0);
                g.transform.localScale = Vector3.one;
                joiner.AssignParentGameObject(g, true, true, true, true, true);
                joiner.currentlyLockedBoneMapper = host;
                emoteSpot.GetComponent<EmoteLocation>().SetEmoterAndHideLocation(joiner);
            }
            if (emoteSpotName == "MyWorldJoinSpot")
            {
                joiner.PlayAnim($"{PluginGUID}__MyWorldJoin", 0);

                GameObject g = new GameObject();
                g.name = "MyWorldJoinProp";
                joiner.props.Add(g);
                g.transform.SetParent(host.transform);
                g.transform.localPosition = new Vector3(2, 0, 0);
                g.transform.localEulerAngles = Vector3.zero;
                g.transform.localScale = Vector3.one;
                joiner.AssignParentGameObject(g, true, true, true, true, true);
                joiner.currentlyLockedBoneMapper = host;

                emoteSpot.GetComponent<EmoteLocation>().SetEmoterAndHideLocation(joiner);
            }
            if (emoteSpotName == "VSWORLDJoinSpot")
            {

                joiner.PlayAnim($"{PluginGUID}__VSWORLDLEFT", 0);
                GameObject g = new GameObject();
                g.name = "VSWORLDLEFTJoinProp";
                joiner.props.Add(g);
                Vector3 scale = host.transform.parent.localScale;
                host.transform.parent.localScale = Vector3.one;
                g.transform.SetParent(host.transform);
                g.transform.localPosition = new Vector3(-2, 0, 0);
                g.transform.localEulerAngles = new Vector3(0, 0, 0);
                g.transform.localScale = Vector3.one * (host.scale / joiner.scale);
                //g.transform.SetParent(null);
                host.transform.parent.localScale = scale;
                //g.transform.SetParent(host.transform.parent);
                joiner.AssignParentGameObject(g, true, true, false, false, true);
                joiner.currentlyLockedBoneMapper = host;

                emoteSpot.GetComponent<EmoteLocation>().SetEmoterAndHideLocation(joiner);
            }
        }

        int stand = -1;
        List<BoneMapper> punchingMappers = new List<BoneMapper>();
        int prop1 = -1;
        int prop2 = -1;
        private void CustomEmotesAPI_animChanged(string newAnimation, BoneMapper mapper)
        {
            if (!newAnimation.StartsWith(PluginGUID))
            {
                return;
            }
            newAnimation = newAnimation.Split("__")[1];
            prop1 = -1;
            try
            {
                if (newAnimation != "none")
                {
                    stand = mapper.currentClip.syncPos;
                }
            }
            catch (System.Exception e)
            {
            }
            if (punchingMappers.Contains(mapper))
            {
                punchingMappers.Remove(mapper);
            }
            if (punchingMappers.Count == 0)
            {
                //TODO audio
                //AkSoundEngine.SetRTPCValue("MetalGear_Vocals", 0);
            }
            if (newAnimation == "StandingHere")
            {
                punchingMappers.Add(mapper);
                //TODO audio
                //AkSoundEngine.SetRTPCValue("MetalGear_Vocals", 1);
            }
            if (newAnimation == "StoodHere")
            {
                GameObject g = new GameObject();
                g.name = "StoodHereProp";
                mapper.props.Add(g);
                g.transform.localPosition = mapper.transform.position;
                g.transform.localEulerAngles = mapper.transform.eulerAngles;
                g.transform.localScale = Vector3.one;
                mapper.AssignParentGameObject(g, false, false, true, true, false);
            }
            if (newAnimation == "Chika")
            {
                if (Settings.chikaProp.Value)
                {

                    prop1 = mapper.props.Count;
                    GameObject sex;
                    if (UnityEngine.Random.Range(0, 100) == 0)
                    {
                        sex = Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/models/desker.prefab");
                    }
                    else
                    {
                        sex = Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/prefabs/CSSDesker.prefab");
                    }
                    mapper.props.Add(GameObject.Instantiate(sex));
                    mapper.props[prop1].transform.SetParent(mapper.transform);
                    mapper.props[prop1].transform.localEulerAngles = Vector3.zero;
                    mapper.props[prop1].transform.localPosition = Vector3.zero;
                    mapper.ScaleProps();
                }
            }
            if (newAnimation == "Make it Rain")
            {
                if (Settings.moneyProp.Value)
                {
                    prop1 = mapper.props.Count;
                    mapper.props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/Prefabs/money.prefab")));
                    mapper.props[prop1].transform.SetParent(mapper.transform);
                    mapper.props[prop1].transform.localEulerAngles = Vector3.zero;
                    mapper.props[prop1].transform.localPosition = Vector3.zero;
                    mapper.ScaleProps();
                }
            }
            if (newAnimation == "Rivers in the Dessert" || newAnimation == "Cirno" || newAnimation == "Hare Hare Yukai" || newAnimation == "GGGG")
            {
                if (Settings.desertlightProp.Value)
                {

                    prop1 = mapper.props.Count;
                    mapper.props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/Prefabs/desertlight.prefab")));
                    mapper.props[prop1].transform.SetParent(mapper.transform);
                    mapper.props[prop1].transform.localEulerAngles = Vector3.zero;
                    mapper.props[prop1].transform.localPosition = Vector3.zero;
                }
            }
            if (newAnimation == "Step!")
            {
                prop1 = mapper.props.Count;
                GameObject myNutz = GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/Prefabs/hondastuff.prefab"));
                foreach (var item in myNutz.GetComponentsInChildren<ParticleSystem>())
                {
                    item.time = CustomAnimationClip.syncTimer[mapper.currentClip.syncPos];
                }
                Animator a = myNutz.GetComponentInChildren<Animator>();
                //a.Play("MusicSync", -1);
                a.Play("MusicSync", 0, (CustomAnimationClip.syncTimer[mapper.currentClip.syncPos] % a.GetCurrentAnimatorClipInfo(0)[0].clip.length) / a.GetCurrentAnimatorClipInfo(0)[0].clip.length);
                myNutz.transform.SetParent(mapper.transform);
                myNutz.transform.localEulerAngles = Vector3.zero;
                myNutz.transform.localPosition = Vector3.zero;
                mapper.props.Add(myNutz);
            }
            if (newAnimation == "Train")
            {
                prop1 = mapper.props.Count;
                if (CustomAnimationClip.syncPlayerCount[stand] == 1)
                {
                    mapper.props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/Prefabs/train.prefab")));
                }
                else
                {
                    mapper.props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/Prefabs/passenger.prefab")));
                }


                mapper.props[prop1].transform.SetParent(mapper.transform);
                mapper.props[prop1].transform.localEulerAngles = Vector3.zero;
                mapper.props[prop1].transform.localPosition = Vector3.zero;
                mapper.SetAutoWalk(1, false);
                mapper.ScaleProps();
            }
            if (newAnimation == "Bim Bam Boom")
            {
                if (Settings.BimBamBomProp.Value)
                {

                    prop1 = mapper.props.Count;
                    mapper.props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/Prefabs/BimBamBom.prefab")));
                    foreach (var item in mapper.props[prop1].GetComponentsInChildren<ParticleSystem>())
                    {
                        item.time = CustomAnimationClip.syncTimer[mapper.currentClip.syncPos];
                    }
                    mapper.props[prop1].transform.SetParent(mapper.transform);
                    mapper.props[prop1].transform.localEulerAngles = Vector3.zero;
                    mapper.props[prop1].transform.localPosition = Vector3.zero;
                    mapper.ScaleProps();
                }
            }
            if (newAnimation == "Summertime")
            {
                if (Settings.SummermogusProp.Value)
                {

                    prop1 = mapper.props.Count;
                    mapper.props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:Assets/Prefabs/Summermogus.prefab")));
                    foreach (var item in mapper.props[prop1].GetComponentsInChildren<Animator>())
                    {
                        item.Play("Summertime", 0, (CustomAnimationClip.syncTimer[mapper.currentClip.syncPos] % item.GetCurrentAnimatorClipInfo(0)[0].clip.length) / item.GetCurrentAnimatorClipInfo(0)[0].clip.length);
                    }
                    mapper.props[prop1].transform.SetParent(mapper.transform);
                    mapper.props[prop1].transform.localEulerAngles = Vector3.zero;
                    mapper.props[prop1].transform.localPosition = Vector3.zero;
                    mapper.ScaleProps();
                }
                //Animator a = mapper.props[prop1].GetComponentInChildren<Animator>();

                //a.Play("Summertime", 0, (CustomAnimationClip.syncTimer[mapper.currentClip.syncPos] % a.GetCurrentAnimatorClipInfo(0)[0].clip.length) / a.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            }
            if (newAnimation == "Float")
            {
                if (Settings.FloatLightProp.Value)
                {

                    prop1 = mapper.props.Count;
                    mapper.props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/Prefabs/FloatLight.prefab")));
                    mapper.props[prop1].transform.SetParent(mapper.transform);
                    mapper.props[prop1].transform.localEulerAngles = Vector3.zero;
                    mapper.props[prop1].transform.localPosition = Vector3.zero;
                    mapper.ScaleProps();
                }
            }
            if (newAnimation == "Markiplier")
            {
                if (Settings.Amogus.Value)
                {

                    prop1 = mapper.props.Count;
                    mapper.props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/Prefabs/Amogus.prefab")));
                    foreach (var item in mapper.props[prop1].GetComponentsInChildren<Animator>())
                    {
                        item.Play("Markimogus", 0, (CustomAnimationClip.syncTimer[mapper.currentClip.syncPos] % item.GetCurrentAnimatorClipInfo(0)[0].clip.length) / item.GetCurrentAnimatorClipInfo(0)[0].clip.length);
                    }
                    mapper.props[prop1].transform.SetParent(mapper.transform);
                    mapper.props[prop1].transform.localEulerAngles = Vector3.zero;
                    mapper.props[prop1].transform.localPosition = Vector3.zero;
                    mapper.ScaleProps();
                }
            }
            if (newAnimation == "Officer Earl")
            {
                mapper.SetAutoWalk(1, false);
            }
            if (newAnimation == "Virtual Insanity")
            {
                mapper.SetAutoWalk(.2f, false);
            }
            if (newAnimation == "Move It")
            {
                mapper.SetAutoWalk(.2f, false);
            }
            if (newAnimation == "Spring Loaded")
            {
                mapper.SetAutoWalk(-1, false);
            }
            if (newAnimation == "Samus Crawl")
            {
                mapper.SetAutoWalk(.5f, false);
            }
            if (newAnimation == "CluckStrut")
            {
                mapper.SetAutoWalk(.3f, false);
            }
            if (newAnimation == "Duck This One")
            {
                //GameObject g = new GameObject();
                //g.name = "DuckThisOneIdleProp";
                //mapper.props.Add(g);
                //g.transform.localPosition = mapper.transform.position;
                //g.transform.localEulerAngles = mapper.transform.eulerAngles + new Vector3(0, 0, 0);
                //g.transform.localScale = Vector3.one;
                //g.transform.SetParent(mapper.mapperBodyTransform.parent);
                //mapper.AssignParentGameObject(g, true, true, true, true, false);
            }
            if (newAnimation == "Full Tilt")
            {
                mapper.SetAutoWalk(1, false);
            }
            if (newAnimation == "Cartwheelin")
            {
                mapper.SetAutoWalk(.75f, false);
            }
            if (newAnimation == "Ralsei Dies")
            {
                if (Settings.BluntAnimatorProp.Value)
                {

                    prop1 = mapper.props.Count;
                    mapper.props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/Prefabs/BluntAnimator.prefab")));
                    foreach (var item in mapper.props[prop1].GetComponentsInChildren<Animator>())
                    {
                        item.Play("Blunt", 0, (CustomAnimationClip.syncTimer[mapper.currentClip.syncPos] % item.GetCurrentAnimatorClipInfo(0)[0].clip.length) / item.GetCurrentAnimatorClipInfo(0)[0].clip.length);
                    }
                    mapper.props[prop1].transform.SetParent(mapper.transform);
                    mapper.props[prop1].transform.localEulerAngles = Vector3.zero;
                    mapper.props[prop1].transform.localPosition = Vector3.zero;
                    mapper.props[prop1].GetComponentInChildren<ParticleSystem>().gravityModifier *= mapper.scale;
                    var velocity = mapper.props[prop1].GetComponentInChildren<ParticleSystem>().limitVelocityOverLifetime;
                    velocity.dampen *= mapper.scale;
                    velocity.limitMultiplier = mapper.scale;
                    mapper.ScaleProps();
                }
            }
            if (newAnimation == "Hare Hare Yukai")
            {
                GameObject g = new GameObject();
                g.name = "HaruhiProp";
                mapper.props.Add(g);
                g.transform.localPosition = mapper.transform.position;
                g.transform.localEulerAngles = mapper.transform.eulerAngles;
                g.transform.localScale = Vector3.one;
                mapper.AssignParentGameObject(g, false, false, true, true, false);
            }
            if (newAnimation == "Last Surprise")
            {
                if (mapper != CustomEmotesAPI.localMapper)
                {
                    if (Settings.neverseeProp.Value)
                    {

                        prop1 = mapper.props.Count;
                        mapper.props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/prefabs/neversee.prefab")));
                        mapper.props[prop1].transform.SetParent(mapper.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Spine));
                        mapper.props[prop1].transform.localEulerAngles = Vector3.zero;
                        mapper.props[prop1].transform.localPosition = Vector3.zero;
                        mapper.ScaleProps();
                    }
                }
            }
            if (newAnimation == "IFU Stage")
            {
                //TODO IFU networking
                //if (NetworkServer.active)
                //{
                //    if (stage)
                //    {
                //        NetworkServer.Destroy(stage);
                //    }
                //    stage = CustomEmotesAPI.SpawnWorldProp(stageInt);
                //    stage.transform.SetParent(mapper.transform.parent);
                //    stage.transform.localPosition = new Vector3(0, 0, 0);
                //    stage.transform.SetParent(null);
                //    stage.transform.localPosition += new Vector3(0, .5f, 0);
                //    NetworkServer.Spawn(stage);
                //}
            }
            if (newAnimation == "Hydraulic Press")
            {
                //TODO networking
                //if (NetworkServer.active)
                //{
                //    if (press)
                //    {
                //        NetworkServer.Destroy(press);
                //    }
                //    bool lowes = UnityEngine.Random.Range(0, 15) == 0;
                //    press = CustomEmotesAPI.SpawnWorldProp(pressInt);
                //    press.GetComponent<HydrolicPressMechanism>().lowes = lowes;
                //    press.transform.SetParent(mapper.transform.parent);
                //    press.transform.localPosition = new Vector3(0, 0, 0);
                //    press.transform.SetParent(null);
                //    //press.transform.localPosition += new Vector3(0, .5f, 0);
                //    NetworkServer.Spawn(press);
                //}
            }
            if (newAnimation == "Im a Mystery")
            {
                if (Settings.desertlightProp.Value)
                {

                    prop1 = mapper.props.Count;
                    mapper.props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/Prefabs/Im a Mystery.prefab")));
                    Animator a = mapper.props[prop1].GetComponentInChildren<Animator>();
                    if (CustomAnimationClip.syncTimer[mapper.currentClip.syncPos] < .4f)
                    {
                        a.Play("Mystery Model Intro", 0, (CustomAnimationClip.syncTimer[mapper.currentClip.syncPos] % .4f) / .4f);
                    }
                    else
                    {
                        a.Play("Mstery Model", 0, ((CustomAnimationClip.syncTimer[mapper.currentClip.syncPos] - .4f) % 7.333f) / 7.333f);
                    }
                    mapper.props[prop1].transform.SetParent(mapper.transform);
                    mapper.props[prop1].transform.localEulerAngles = Vector3.zero;
                    mapper.props[prop1].transform.localPosition = Vector3.zero;
                    mapper.ScaleProps();
                }
            }
            //if (newAnimation == "Sad")
            //{
            //    prop1 = mapper.props.Count;
            //    mapper.props.Add(GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:assets/models/trombone.prefab")));
            //    mapper.props[prop1].transform.SetParent(mapper.a2.GetBoneTransform(HumanBodyBones.RightHand));
            //    mapper.props[prop1].transform.localEulerAngles = new Vector3(0, 270, 0);
            //    mapper.props[prop1].transform.localPosition = Vector3.zero;
            //    mapper.props[prop1].transform.localScale = Vector3.one;
            //}
            if (newAnimation == "CheerUp")
            {
                var newPrefab = GameObject.Instantiate(Assets.Load<GameObject>("@BadAssEmotes_badassemotes:Assets/badassemotes/PomPom.prefab"));
                foreach (Renderer componentsInChild in newPrefab.GetComponentsInChildren<Renderer>())
                    componentsInChild.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                prop1 = mapper.props.Count;
                mapper.props.Add(GameObject.Instantiate(newPrefab));
                mapper.props[prop1].transform.SetParent(mapper.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftHand));
                mapper.props[prop1].transform.localEulerAngles = Vector3.zero;
                mapper.props[prop1].transform.localPosition = Vector3.zero;
                prop2 = mapper.props.Count;
                mapper.props.Add(GameObject.Instantiate(newPrefab));
                mapper.props[prop2].transform.SetParent(mapper.gameObject.GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightHand));
                mapper.props[prop2].transform.localEulerAngles = Vector3.zero;
                mapper.props[prop2].transform.localPosition = Vector3.zero;
                mapper.ScaleProps();
            }
        }
        internal void AddAnimation(string AnimClip, AudioClip[] startClips, AudioClip[] loopClips, bool looping, bool dimAudio, bool sync, AnimationClipParams.LockType lockType, string customName, AudioClip[] dmcaStartClips, AudioClip[] dmcaLoopClips, bool DMCA, float audio)
        {
            if (customName == "")
            {
                customName = Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip}.anim").name;
            }
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip}.anim") }, audioLoops = looping, primaryAudioClips = startClips, secondaryAudioClips = loopClips, syncAnim = sync, syncAudio = sync, lockType = lockType, internalName = customName, primaryDMCAFreeAudioClips = dmcaStartClips, secondaryDMCAFreeAudioClips = dmcaLoopClips, willGetClaimedByDMCA = DMCA, audioLevel = audio, displayName = customName });
        }
        internal void AddAnimation(string AnimClip, string startClip, string loopClip, bool looping, bool dimAudio, bool sync, AnimationClipParams.LockType lockType, string customName, string dmcaStartClip, string dmcaLoopClip, bool DMCA, float audio)
        {
            if (customName == "")
            {
                customName = Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip}.anim").name;
            }
            AudioClip[] startC;
            if (startClip == "")
            {
                startC = null;
            }
            else
            {
                startC = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/{startClip}.ogg") };
            }
            AudioClip[] loopC;
            if (loopClip == "")
            {
                loopC = null;
            }
            else
            {
                loopC = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/{loopClip}.ogg") };
            }

            AudioClip[] DMCAstartC;
            if (dmcaStartClip == "")
            {
                DMCAstartC = null;
            }
            else
            {
                DMCAstartC = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/{dmcaStartClip}_NNTranscription.ogg") };
            }
            AudioClip[] DMCAloopC;
            if (dmcaLoopClip == "")
            {
                DMCAloopC = null;
            }
            else
            {
                DMCAloopC = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/{dmcaLoopClip}_NNTranscription.ogg") };
            }
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip}.anim") }, audioLoops = looping, primaryAudioClips = startC, secondaryAudioClips = loopC, syncAnim = sync, syncAudio = sync, lockType = lockType, internalName = customName, primaryDMCAFreeAudioClips = DMCAstartC, secondaryDMCAFreeAudioClips = DMCAloopC, willGetClaimedByDMCA = DMCA, audioLevel = audio, displayName = customName });
        }
        internal void AddAnimation(string AnimClip, string AnimClip2, string startClip, string loopClip, bool looping, bool dimAudio, bool sync, AnimationClipParams.LockType lockType, string customName, string dmcaStartClip, string dmcaLoopClip, bool DMCA, float audio)
        {
            if (customName == "")
            {
                customName = Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip}.anim").name;
            }
            AudioClip[] startC;
            if (startClip == "")
            {
                startC = null;
            }
            else
            {
                startC = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/{startClip}.ogg") };
            }
            AudioClip[] loopC;
            if (loopClip == "")
            {
                loopC = null;
            }
            else
            {
                loopC = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/{loopClip}.ogg") };
            }

            AudioClip[] DMCAstartC;
            if (dmcaStartClip == "")
            {
                DMCAstartC = null;
            }
            else
            {
                DMCAstartC = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/{dmcaStartClip}_NNTranscription.ogg") };
            }
            AudioClip[] DMCAloopC;
            if (dmcaLoopClip == "")
            {
                DMCAloopC = null;
            }
            else
            {
                DMCAloopC = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/{dmcaLoopClip}_NNTranscription.ogg") };
            }
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip}.anim") }, secondaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip2}.anim") }, audioLoops = looping, primaryAudioClips = startC, secondaryAudioClips = loopC, syncAnim = sync, syncAudio = sync, lockType = lockType, internalName = customName, primaryDMCAFreeAudioClips = DMCAstartC, secondaryDMCAFreeAudioClips = DMCAloopC, willGetClaimedByDMCA = DMCA, audioLevel = audio, displayName = customName });
        }
        internal void AddAnimation(string AnimClip, string[] startClip, bool looping, bool dimaudio, bool sync, AnimationClipParams.LockType lockType, string customName, string[] dmcaStartClips, bool DMCA, float audio)
        {
            if (customName == "")
            {
                customName = Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip}.anim").name;
            }
            List<AudioClip> startC = new List<AudioClip>();
            foreach (var item in startClip)
            {

                startC.Add(Assets.Load<AudioClip>($"assets/compressedaudio/{item}.ogg"));
            }

            List<AudioClip> DMCAstartC = new List<AudioClip>();
            foreach (var item in dmcaStartClips)
            {
                if (item == "")
                {
                    DMCAstartC.Add(null);
                }
                else
                {
                    DMCAstartC.Add(Assets.Load<AudioClip>($"assets/DMCAMusic/{item}_NNTranscription.ogg"));
                }
            }
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip}.anim") }, audioLoops = looping, primaryAudioClips = startC.ToArray(), secondaryAudioClips = null, syncAnim = sync, syncAudio = sync, lockType = lockType, internalName = customName, primaryDMCAFreeAudioClips = DMCAstartC.ToArray(), willGetClaimedByDMCA = DMCA, audioLevel = audio, displayName = customName });
        }
        internal void AddStartAndJoinAnim(string[] AnimClip, string startClip, bool looping, bool dimaudio, bool sync, AnimationClipParams.LockType lockType, string customName, string dmcaStartClip, bool DMCA, float audio)
        {
            if (customName == "")
            {
                customName = Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip[0]}.anim").name;
            }
            AudioClip[] startC;
            if (startClip == "")
            {
                startC = null;
            }
            else
            {
                startC = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/{startClip}.ogg") };
            }

            AudioClip[] DMCAstartC;
            if (dmcaStartClip == "")
            {
                DMCAstartC = null;
            }
            else
            {
                DMCAstartC = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/{dmcaStartClip}_NNTranscription.ogg") };
            }
            List<AnimationClip> nuts = new List<AnimationClip>();
            foreach (var item in AnimClip)
            {
                nuts.Add(Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{item}.anim"));
            }
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = nuts.ToArray(), audioLoops = looping, primaryAudioClips = startC, secondaryAudioClips = null, syncAnim = sync, syncAudio = sync, startPref = 0, joinPref = 1, lockType = lockType, internalName = customName, primaryDMCAFreeAudioClips = DMCAstartC, willGetClaimedByDMCA = DMCA, audioLevel = audio, displayName = customName });
        }
        internal void AddStartAndJoinAnim(string[] AnimClip, string startClip, string[] AnimClip2, bool looping, bool dimaudio, bool sync, AnimationClipParams.LockType lockType, string customName, string dmcaStartClip, bool DMCA, float audio)
        {
            if (customName == "")
            {
                customName = Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip[0]}.anim").name;
            }
            AudioClip[] startC;
            if (startClip == "")
            {
                startC = null;
            }
            else
            {
                startC = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/{startClip}.ogg") };
            }
            AudioClip[] DMCAstartC;
            if (dmcaStartClip == "")
            {
                DMCAstartC = null;
            }
            else
            {
                DMCAstartC = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/{dmcaStartClip}.ogg") };
            }

            List<AnimationClip> nuts = new List<AnimationClip>();
            foreach (var item in AnimClip)
            {
                nuts.Add(Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/DMCAMusic/{item}_NNTranscription.anim"));
            }
            List<AnimationClip> nuts2 = new List<AnimationClip>();
            foreach (var item in AnimClip2)
            {
                nuts2.Add(Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/DMCAMusic/{item}_NNTranscription.anim"));
            }
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = nuts.ToArray(), secondaryAnimationClips = nuts2.ToArray(), audioLoops = looping, primaryAudioClips = startC, syncAnim = sync, syncAudio = sync, startPref = 0, joinPref = 1, lockType = lockType, internalName = customName, primaryDMCAFreeAudioClips = DMCAstartC, willGetClaimedByDMCA = DMCA, audioLevel = audio, displayName = customName });
        }
        internal void AddAnimation2(string AnimClip, string startClip, string loopClip, bool looping, bool dimAudio, bool sync, AnimationClipParams.LockType lockType, string customName, string dmcaStartClip, string dmcaLoopClip, bool DMCA, float audio)
        {
            string internalName = Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip}.anim").name;
            if (customName == "")
            {
                customName = internalName;
            }
            AudioClip[] startC;
            if (startClip == "")
            {
                startC = null;
            }
            else
            {
                startC = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/{startClip}.ogg") };
            }
            AudioClip[] loopC;
            if (loopClip == "")
            {
                loopC = null;
            }
            else
            {
                loopC = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/{loopClip}.ogg") };
            }

            AudioClip[] DMCAstartC;
            if (dmcaStartClip == "")
            {
                DMCAstartC = null;
            }
            else
            {
                DMCAstartC = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/{dmcaStartClip}_NNTranscription.ogg") };
            }
            AudioClip[] DMCAloopC;
            if (dmcaLoopClip == "")
            {
                DMCAloopC = null;
            }
            else
            {
                DMCAloopC = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/{dmcaLoopClip}_NNTranscription.ogg") };
            }
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip}.anim") }, audioLoops = looping, primaryAudioClips = startC, secondaryAudioClips = loopC, syncAnim = sync, syncAudio = sync, lockType = lockType, internalName = internalName, primaryDMCAFreeAudioClips = DMCAstartC, secondaryDMCAFreeAudioClips = DMCAloopC, willGetClaimedByDMCA = DMCA, audioLevel = audio, displayName = customName });
        }
        internal void AddAnimation2(string AnimClip, string AnimClip2, string startClip, string loopClip, bool looping, bool dimAudio, bool sync, AnimationClipParams.LockType lockType, string customName, string dmcaStartClip, string dmcaLoopClip, bool DMCA, float audio)
        {
            string internalName = Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip}.anim").name;
            if (customName == "")
            {
                customName = internalName;
            }
            AudioClip[] startC;
            if (startClip == "")
            {
                startC = null;
            }
            else
            {
                startC = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/{startClip}.ogg") };
            }
            AudioClip[] loopC;
            if (loopClip == "")
            {
                loopC = null;
            }
            else
            {
                loopC = new AudioClip[] { Assets.Load<AudioClip>($"assets/compressedaudio/{loopClip}.ogg") };
            }

            AudioClip[] DMCAstartC;
            if (dmcaStartClip == "")
            {
                DMCAstartC = null;
            }
            else
            {
                DMCAstartC = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/{dmcaStartClip}_NNTranscription.ogg") };
            }
            AudioClip[] DMCAloopC;
            if (dmcaLoopClip == "")
            {
                DMCAloopC = null;
            }
            else
            {
                DMCAloopC = new AudioClip[] { Assets.Load<AudioClip>($"assets/DMCAMusic/{dmcaLoopClip}_NNTranscription.ogg") };
            }
            EmoteImporter.ImportEmote(new CustomEmoteParams() { primaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip}.anim") }, secondaryAnimationClips = new AnimationClip[] { Assets.Load<AnimationClip>($"@ExampleEmotePlugin_badassemotes:assets/badassemotes/{AnimClip2}.anim") }, audioLoops = looping, primaryAudioClips = startC, secondaryAudioClips = loopC, syncAnim = sync, syncAudio = sync, lockType = lockType, internalName = internalName, primaryDMCAFreeAudioClips = DMCAstartC, secondaryDMCAFreeAudioClips = DMCAloopC, willGetClaimedByDMCA = DMCA, audioLevel = audio, displayName = customName });
        }
    }
}
