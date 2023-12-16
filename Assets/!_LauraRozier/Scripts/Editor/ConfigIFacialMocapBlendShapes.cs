using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VRM;

public class ConfigIFacialMocapBlendShapes : EditorWindow
{
    [MenuItem("Tools/LauraRozier/Configure IFacialMocap BlendShapes")]
    public static void ShowWindow() =>
        GetWindow<ConfigIFacialMocapBlendShapes>(true, "Configure IFacialMocap BlendShapes", true);

    private const float CGUISpacing = 12f;
    private Vector2 scrollPosition = Vector2.zero;

    private VRMBlendShapeProxy blendShapeProxy = null;
    private int blendshapeRelPathIdx = 0;
    private int blendshapeRelPathIdxOld = -1;
    private SkinnedMeshRenderer blendshapeRenderer = null;
    private string blendshapeRelPath = string.Empty;

    #region Blendshape Settings
    private const string CStr_browInnerUp = "browInnerUp";
    private bool browInnerUp_ena = true;
    private string browInnerUp_blendName = CStr_browInnerUp;
    private float browInnerUp_blendWeight = 100f;

    private const string CStr_browDownLeft = "browDownLeft";
    private bool browDownLeft_ena = true;
    private string browDownLeft_blendName = CStr_browDownLeft;
    private float browDownLeft_blendWeight = 100f;

    private const string CStr_browDownRight = "browDownRight";
    private bool browDownRight_ena = true;
    private string browDownRight_blendName = CStr_browDownRight;
    private float browDownRight_blendWeight = 100f;

    private const string CStr_browOuterUpLeft = "browOuterUpLeft";
    private bool browOuterUpLeft_ena = true;
    private string browOuterUpLeft_blendName = CStr_browOuterUpLeft;
    private float browOuterUpLeft_blendWeight = 100f;

    private const string CStr_browOuterUpRight = "browOuterUpRight";
    private bool browOuterUpRight_ena = true;
    private string browOuterUpRight_blendName = CStr_browOuterUpRight;
    private float browOuterUpRight_blendWeight = 100f;

    private const string CStr_eyeLookUpLeft = "eyeLookUpLeft";
    private bool eyeLookUpLeft_ena = true;
    private string eyeLookUpLeft_blendName = CStr_eyeLookUpLeft;
    private float eyeLookUpLeft_blendWeight = 100f;

    private const string CStr_eyeLookUpRight = "eyeLookUpRight";
    private bool eyeLookUpRight_ena = true;
    private string eyeLookUpRight_blendName = CStr_eyeLookUpRight;
    private float eyeLookUpRight_blendWeight = 100f;

    private const string CStr_eyeLookDownLeft = "eyeLookDownLeft";
    private bool eyeLookDownLeft_ena = true;
    private string eyeLookDownLeft_blendName = CStr_eyeLookDownLeft;
    private float eyeLookDownLeft_blendWeight = 100f;

    private const string CStr_eyeLookDownRight = "eyeLookDownRight";
    private bool eyeLookDownRight_ena = true;
    private string eyeLookDownRight_blendName = CStr_eyeLookDownRight;
    private float eyeLookDownRight_blendWeight = 100f;

    private const string CStr_eyeLookInLeft = "eyeLookInLeft";
    private bool eyeLookInLeft_ena = true;
    private string eyeLookInLeft_blendName = CStr_eyeLookInLeft;
    private float eyeLookInLeft_blendWeight = 100f;

    private const string CStr_eyeLookInRight = "eyeLookInRight";
    private bool eyeLookInRight_ena = true;
    private string eyeLookInRight_blendName = CStr_eyeLookInRight;
    private float eyeLookInRight_blendWeight = 100f;

    private const string CStr_eyeLookOutLeft = "eyeLookOutLeft";
    private bool eyeLookOutLeft_ena = true;
    private string eyeLookOutLeft_blendName = CStr_eyeLookOutLeft;
    private float eyeLookOutLeft_blendWeight = 100f;

    private const string CStr_eyeLookOutRight = "eyeLookOutRight";
    private bool eyeLookOutRight_ena = true;
    private string eyeLookOutRight_blendName = CStr_eyeLookOutRight;
    private float eyeLookOutRight_blendWeight = 100f;

    private const string CStr_eyeBlinkLeft = "eyeBlinkLeft";
    private bool eyeBlinkLeft_ena = true;
    private string eyeBlinkLeft_blendName = CStr_eyeBlinkLeft;
    private float eyeBlinkLeft_blendWeight = 100f;

    private const string CStr_eyeBlinkRight = "eyeBlinkRight";
    private bool eyeBlinkRight_ena = true;
    private string eyeBlinkRight_blendName = CStr_eyeBlinkRight;
    private float eyeBlinkRight_blendWeight = 100f;

    private const string CStr_eyeSquintRight = "eyeSquintRight";
    private bool eyeSquintRight_ena = true;
    private string eyeSquintRight_blendName = CStr_eyeSquintRight;
    private float eyeSquintRight_blendWeight = 100f;

    private const string CStr_eyeSquintLeft = "eyeSquintLeft";
    private bool eyeSquintLeft_ena = true;
    private string eyeSquintLeft_blendName = CStr_eyeSquintLeft;
    private float eyeSquintLeft_blendWeight = 100f;

    private const string CStr_eyeWideLeft = "eyeWideLeft";
    private bool eyeWideLeft_ena = true;
    private string eyeWideLeft_blendName = CStr_eyeWideLeft;
    private float eyeWideLeft_blendWeight = 100f;

    private const string CStr_eyeWideRight = "eyeWideRight";
    private bool eyeWideRight_ena = true;
    private string eyeWideRight_blendName = CStr_eyeWideRight;
    private float eyeWideRight_blendWeight = 100f;

    private const string CStr_cheekPuff = "cheekPuff";
    private bool cheekPuff_ena = true;
    private string cheekPuff_blendName = CStr_cheekPuff;
    private float cheekPuff_blendWeight = 100f;

    private const string CStr_cheekSquintLeft = "cheekSquintLeft";
    private bool cheekSquintLeft_ena = true;
    private string cheekSquintLeft_blendName = CStr_cheekSquintLeft;
    private float cheekSquintLeft_blendWeight = 100f;

    private const string CStr_cheekSquintRight = "cheekSquintRight";
    private bool cheekSquintRight_ena = true;
    private string cheekSquintRight_blendName = CStr_cheekSquintRight;
    private float cheekSquintRight_blendWeight = 100f;

    private const string CStr_noseSneerLeft = "noseSneerLeft";
    private bool noseSneerLeft_ena = true;
    private string noseSneerLeft_blendName = CStr_noseSneerLeft;
    private float noseSneerLeft_blendWeight = 100f;

    private const string CStr_noseSneerRight = "noseSneerRight";
    private bool noseSneerRight_ena = true;
    private string noseSneerRight_blendName = CStr_noseSneerRight;
    private float noseSneerRight_blendWeight = 100f;

    private const string CStr_jawOpen = "jawOpen";
    private bool jawOpen_ena = true;
    private string jawOpen_blendName = CStr_jawOpen;
    private float jawOpen_blendWeight = 100f;

    private const string CStr_jawForward = "jawForward";
    private bool jawForward_ena = true;
    private string jawForward_blendName = CStr_jawForward;
    private float jawForward_blendWeight = 100f;

    private const string CStr_jawLeft = "jawLeft";
    private bool jawLeft_ena = true;
    private string jawLeft_blendName = CStr_jawLeft;
    private float jawLeft_blendWeight = 100f;

    private const string CStr_jawRight = "jawRight";
    private bool jawRight_ena = true;
    private string jawRight_blendName = CStr_jawRight;
    private float jawRight_blendWeight = 100f;

    private const string CStr_mouthFunnel = "mouthFunnel";
    private bool mouthFunnel_ena = true;
    private string mouthFunnel_blendName = CStr_mouthFunnel;
    private float mouthFunnel_blendWeight = 100f;

    private const string CStr_mouthPucker = "mouthPucker";
    private bool mouthPucker_ena = true;
    private string mouthPucker_blendName = CStr_mouthPucker;
    private float mouthPucker_blendWeight = 100f;

    private const string CStr_mouthLeft = "mouthLeft";
    private bool mouthLeft_ena = true;
    private string mouthLeft_blendName = CStr_mouthLeft;
    private float mouthLeft_blendWeight = 100f;

    private const string CStr_mouthRight = "mouthRight";
    private bool mouthRight_ena = true;
    private string mouthRight_blendName = CStr_mouthRight;
    private float mouthRight_blendWeight = 100f;

    private const string CStr_mouthRollUpper = "mouthRollUpper";
    private bool mouthRollUpper_ena = true;
    private string mouthRollUpper_blendName = CStr_mouthRollUpper;
    private float mouthRollUpper_blendWeight = 100f;

    private const string CStr_mouthRollLower = "mouthRollLower";
    private bool mouthRollLower_ena = true;
    private string mouthRollLower_blendName = CStr_mouthRollLower;
    private float mouthRollLower_blendWeight = 100f;

    private const string CStr_mouthShrugUpper = "mouthShrugUpper";
    private bool mouthShrugUpper_ena = true;
    private string mouthShrugUpper_blendName = CStr_mouthShrugUpper;
    private float mouthShrugUpper_blendWeight = 100f;

    private const string CStr_mouthShrugLower = "mouthShrugLower";
    private bool mouthShrugLower_ena = true;
    private string mouthShrugLower_blendName = CStr_mouthShrugLower;
    private float mouthShrugLower_blendWeight = 100f;

    private const string CStr_mouthClose = "mouthClose";
    private bool mouthClose_ena = true;
    private string mouthClose_blendName = CStr_mouthClose;
    private float mouthClose_blendWeight = 100f;

    private const string CStr_mouthSmileLeft = "mouthSmileLeft";
    private bool mouthSmileLeft_ena = true;
    private string mouthSmileLeft_blendName = CStr_mouthSmileLeft;
    private float mouthSmileLeft_blendWeight = 100f;

    private const string CStr_mouthSmileRight = "mouthSmileRight";
    private bool mouthSmileRight_ena = true;
    private string mouthSmileRight_blendName = CStr_mouthSmileRight;
    private float mouthSmileRight_blendWeight = 100f;

    private const string CStr_mouthFrownLeft = "mouthFrownLeft";
    private bool mouthFrownLeft_ena = true;
    private string mouthFrownLeft_blendName = CStr_mouthFrownLeft;
    private float mouthFrownLeft_blendWeight = 100f;

    private const string CStr_mouthFrownRight = "mouthFrownRight";
    private bool mouthFrownRight_ena = true;
    private string mouthFrownRight_blendName = CStr_mouthFrownRight;
    private float mouthFrownRight_blendWeight = 100f;

    private const string CStr_mouthDimpleLeft = "mouthDimpleLeft";
    private bool mouthDimpleLeft_ena = true;
    private string mouthDimpleLeft_blendName = CStr_mouthDimpleLeft;
    private float mouthDimpleLeft_blendWeight = 100f;

    private const string CStr_mouthDimpleRight = "mouthDimpleRight";
    private bool mouthDimpleRight_ena = true;
    private string mouthDimpleRight_blendName = CStr_mouthDimpleRight;
    private float mouthDimpleRight_blendWeight = 100f;

    private const string CStr_mouthUpperUpLeft = "mouthUpperUpLeft";
    private bool mouthUpperUpLeft_ena = true;
    private string mouthUpperUpLeft_blendName = CStr_mouthUpperUpLeft;
    private float mouthUpperUpLeft_blendWeight = 100f;

    private const string CStr_mouthUpperUpRight = "mouthUpperUpRight";
    private bool mouthUpperUpRight_ena = true;
    private string mouthUpperUpRight_blendName = CStr_mouthUpperUpRight;
    private float mouthUpperUpRight_blendWeight = 100f;

    private const string CStr_mouthLowerDownLeft = "mouthLowerDownLeft";
    private bool mouthLowerDownLeft_ena = true;
    private string mouthLowerDownLeft_blendName = CStr_mouthLowerDownLeft;
    private float mouthLowerDownLeft_blendWeight = 100f;

    private const string CStr_mouthLowerDownRight = "mouthLowerDownRight";
    private bool mouthLowerDownRight_ena = true;
    private string mouthLowerDownRight_blendName = CStr_mouthLowerDownRight;
    private float mouthLowerDownRight_blendWeight = 100f;

    private const string CStr_mouthPressLeft = "mouthPressLeft";
    private bool mouthPressLeft_ena = true;
    private string mouthPressLeft_blendName = CStr_mouthPressLeft;
    private float mouthPressLeft_blendWeight = 100f;

    private const string CStr_mouthPressRight = "mouthPressRight";
    private bool mouthPressRight_ena = true;
    private string mouthPressRight_blendName = CStr_mouthPressRight;
    private float mouthPressRight_blendWeight = 100f;

    private const string CStr_mouthStretchLeft = "mouthStretchLeft";
    private bool mouthStretchLeft_ena = true;
    private string mouthStretchLeft_blendName = CStr_mouthStretchLeft;
    private float mouthStretchLeft_blendWeight = 100f;

    private const string CStr_mouthStretchRight = "mouthStretchRight";
    private bool mouthStretchRight_ena = true;
    private string mouthStretchRight_blendName = CStr_mouthStretchRight;
    private float mouthStretchRight_blendWeight = 100f;

    private const string CStr_tongueOut = "tongueOut";
    private bool tongueOut_ena = true;
    private string tongueOut_blendName = CStr_tongueOut;
    private float tongueOut_blendWeight = 100f;
    #endregion Blendshape Settings

    void OnGUI()
    {
        EditorGUILayout.LabelField("Configure IFacialMocap BlendShapes", EditorStyles.boldLabel);
        EditorGUILayout.Space(CGUISpacing);

        EditorGUI.BeginChangeCheck();
        {
            blendShapeProxy = EditorGUILayout.ObjectField("VRM Blend Shape Proxy", blendShapeProxy, typeof(VRMBlendShapeProxy), true) as VRMBlendShapeProxy;

            if (blendShapeProxy != null)
            {
                var validMeshes = blendShapeProxy.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>(true)
                    .Where(x => x.sharedMesh.blendShapeCount > 0).ToImmutableArray();
                var options = new List<string>();

                foreach (var mesh in validMeshes)
                {
                    options.Add(mesh.gameObject.name);
                }

                blendshapeRelPathIdx = EditorGUILayout.Popup("Face Mesh", blendshapeRelPathIdx, options.ToArray());

                if (blendshapeRelPathIdx != blendshapeRelPathIdxOld)
                {
                    blendshapeRenderer = validMeshes[blendshapeRelPathIdx];
                    blendshapeRelPath = GetGameObjectPath(blendshapeRenderer.gameObject);
                    blendshapeRelPathIdxOld = blendshapeRelPathIdx;
                }
            }
        }
        EditorGUI.EndChangeCheck();

        EditorGUILayout.Space(CGUISpacing);

        if (blendShapeProxy == null)
        {
            EditorGUILayout.HelpBox("Kindly set the `VRM Blend Shape Proxy` setting by draging your avatar descriptor into the setting field.", MessageType.Warning);
            return;
        }

        if (string.IsNullOrWhiteSpace(blendshapeRelPath))
        {
            EditorGUILayout.HelpBox("Kindly set the `Face Mesh` setting by selecting the face blend shape mesh in the setting field.", MessageType.Warning);
            return;
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, false, true);
        {
            browInnerUp_ena = EditorGUILayout.BeginToggleGroup("Enable browInnerUp", browInnerUp_ena);
            {
                browInnerUp_blendName = EditorGUILayout.TextField("Blendshape Name", browInnerUp_blendName);
                browInnerUp_blendWeight = EditorGUILayout.Slider("Blendshape Weight", browInnerUp_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            browDownLeft_ena = EditorGUILayout.BeginToggleGroup("Enable browDownLeft", browDownLeft_ena);
            {
                browDownLeft_blendName = EditorGUILayout.TextField("Blendshape Name", browDownLeft_blendName);
                browDownLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", browDownLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            browDownRight_ena = EditorGUILayout.BeginToggleGroup("Enable browDownRight", browDownRight_ena);
            {
                browDownRight_blendName = EditorGUILayout.TextField("Blendshape Name", browDownRight_blendName);
                browDownRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", browDownRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            browOuterUpLeft_ena = EditorGUILayout.BeginToggleGroup("Enable browOuterUpLeft", browOuterUpLeft_ena);
            {
                browOuterUpLeft_blendName = EditorGUILayout.TextField("Blendshape Name", browOuterUpLeft_blendName);
                browOuterUpLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", browOuterUpLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            browOuterUpRight_ena = EditorGUILayout.BeginToggleGroup("Enable browOuterUpRight", browOuterUpRight_ena);
            {
                browOuterUpRight_blendName = EditorGUILayout.TextField("Blendshape Name", browOuterUpRight_blendName);
                browOuterUpRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", browOuterUpRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeLookUpLeft_ena = EditorGUILayout.BeginToggleGroup("Enable eyeLookUpLeft", eyeLookUpLeft_ena);
            {
                eyeLookUpLeft_blendName = EditorGUILayout.TextField("Blendshape Name", eyeLookUpLeft_blendName);
                eyeLookUpLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeLookUpLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeLookUpRight_ena = EditorGUILayout.BeginToggleGroup("Enable eyeLookUpRight", eyeLookUpRight_ena);
            {
                eyeLookUpRight_blendName = EditorGUILayout.TextField("Blendshape Name", eyeLookUpRight_blendName);
                eyeLookUpRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeLookUpRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeLookDownLeft_ena = EditorGUILayout.BeginToggleGroup("Enable eyeLookDownLeft", eyeLookDownLeft_ena);
            {
                eyeLookDownLeft_blendName = EditorGUILayout.TextField("Blendshape Name", eyeLookDownLeft_blendName);
                eyeLookDownLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeLookDownLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeLookDownRight_ena = EditorGUILayout.BeginToggleGroup("Enable eyeLookDownRight", eyeLookDownRight_ena);
            {
                eyeLookDownRight_blendName = EditorGUILayout.TextField("Blendshape Name", eyeLookDownRight_blendName);
                eyeLookDownRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeLookDownRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeLookInLeft_ena = EditorGUILayout.BeginToggleGroup("Enable eyeLookInLeft", eyeLookInLeft_ena);
            {
                eyeLookInLeft_blendName = EditorGUILayout.TextField("Blendshape Name", eyeLookInLeft_blendName);
                eyeLookInLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeLookInLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeLookInRight_ena = EditorGUILayout.BeginToggleGroup("Enable eyeLookInRight", eyeLookInRight_ena);
            {
                eyeLookInRight_blendName = EditorGUILayout.TextField("Blendshape Name", eyeLookInRight_blendName);
                eyeLookInRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeLookInRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeLookOutLeft_ena = EditorGUILayout.BeginToggleGroup("Enable eyeLookOutLeft", eyeLookOutLeft_ena);
            {
                eyeLookOutLeft_blendName = EditorGUILayout.TextField("Blendshape Name", eyeLookOutLeft_blendName);
                eyeLookOutLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeLookOutLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeLookOutRight_ena = EditorGUILayout.BeginToggleGroup("Enable eyeLookOutRight", eyeLookOutRight_ena);
            {
                eyeLookOutRight_blendName = EditorGUILayout.TextField("Blendshape Name", eyeLookOutRight_blendName);
                eyeLookOutRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeLookOutRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeBlinkLeft_ena = EditorGUILayout.BeginToggleGroup("Enable eyeBlinkLeft", eyeBlinkLeft_ena);
            {
                eyeBlinkLeft_blendName = EditorGUILayout.TextField("Blendshape Name", eyeBlinkLeft_blendName);
                eyeBlinkLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeBlinkLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeBlinkRight_ena = EditorGUILayout.BeginToggleGroup("Enable eyeBlinkRight", eyeBlinkRight_ena);
            {
                eyeBlinkRight_blendName = EditorGUILayout.TextField("Blendshape Name", eyeBlinkRight_blendName);
                eyeBlinkRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeBlinkRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeSquintRight_ena = EditorGUILayout.BeginToggleGroup("Enable eyeSquintRight", eyeSquintRight_ena);
            {
                eyeSquintRight_blendName = EditorGUILayout.TextField("Blendshape Name", eyeSquintRight_blendName);
                eyeSquintRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeSquintRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeSquintLeft_ena = EditorGUILayout.BeginToggleGroup("Enable eyeSquintLeft", eyeSquintLeft_ena);
            {
                eyeSquintLeft_blendName = EditorGUILayout.TextField("Blendshape Name", eyeSquintLeft_blendName);
                eyeSquintLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeSquintLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeWideLeft_ena = EditorGUILayout.BeginToggleGroup("Enable eyeWideLeft", eyeWideLeft_ena);
            {
                eyeWideLeft_blendName = EditorGUILayout.TextField("Blendshape Name", eyeWideLeft_blendName);
                eyeWideLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeWideLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            eyeWideRight_ena = EditorGUILayout.BeginToggleGroup("Enable eyeWideRight", eyeWideRight_ena);
            {
                eyeWideRight_blendName = EditorGUILayout.TextField("Blendshape Name", eyeWideRight_blendName);
                eyeWideRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", eyeWideRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            cheekPuff_ena = EditorGUILayout.BeginToggleGroup("Enable cheekPuff", cheekPuff_ena);
            {
                cheekPuff_blendName = EditorGUILayout.TextField("Blendshape Name", cheekPuff_blendName);
                cheekPuff_blendWeight = EditorGUILayout.Slider("Blendshape Weight", cheekPuff_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            cheekSquintLeft_ena = EditorGUILayout.BeginToggleGroup("Enable cheekSquintLeft", cheekSquintLeft_ena);
            {
                cheekSquintLeft_blendName = EditorGUILayout.TextField("Blendshape Name", cheekSquintLeft_blendName);
                cheekSquintLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", cheekSquintLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            cheekSquintRight_ena = EditorGUILayout.BeginToggleGroup("Enable cheekSquintRight", cheekSquintRight_ena);
            {
                cheekSquintRight_blendName = EditorGUILayout.TextField("Blendshape Name", cheekSquintRight_blendName);
                cheekSquintRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", cheekSquintRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            noseSneerLeft_ena = EditorGUILayout.BeginToggleGroup("Enable noseSneerLeft", noseSneerLeft_ena);
            {
                noseSneerLeft_blendName = EditorGUILayout.TextField("Blendshape Name", noseSneerLeft_blendName);
                noseSneerLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", noseSneerLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            noseSneerRight_ena = EditorGUILayout.BeginToggleGroup("Enable noseSneerRight", noseSneerRight_ena);
            {
                noseSneerRight_blendName = EditorGUILayout.TextField("Blendshape Name", noseSneerRight_blendName);
                noseSneerRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", noseSneerRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            jawOpen_ena = EditorGUILayout.BeginToggleGroup("Enable jawOpen", jawOpen_ena);
            {
                jawOpen_blendName = EditorGUILayout.TextField("Blendshape Name", jawOpen_blendName);
                jawOpen_blendWeight = EditorGUILayout.Slider("Blendshape Weight", jawOpen_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            jawForward_ena = EditorGUILayout.BeginToggleGroup("Enable jawForward", jawForward_ena);
            {
                jawForward_blendName = EditorGUILayout.TextField("Blendshape Name", jawForward_blendName);
                jawForward_blendWeight = EditorGUILayout.Slider("Blendshape Weight", jawForward_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            jawLeft_ena = EditorGUILayout.BeginToggleGroup("Enable jawLeft", jawLeft_ena);
            {
                jawLeft_blendName = EditorGUILayout.TextField("Blendshape Name", jawLeft_blendName);
                jawLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", jawLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            jawRight_ena = EditorGUILayout.BeginToggleGroup("Enable jawRight", jawRight_ena);
            {
                jawRight_blendName = EditorGUILayout.TextField("Blendshape Name", jawRight_blendName);
                jawRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", jawRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthFunnel_ena = EditorGUILayout.BeginToggleGroup("Enable mouthFunnel", mouthFunnel_ena);
            {
                mouthFunnel_blendName = EditorGUILayout.TextField("Blendshape Name", mouthFunnel_blendName);
                mouthFunnel_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthFunnel_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthPucker_ena = EditorGUILayout.BeginToggleGroup("Enable mouthPucker", mouthPucker_ena);
            {
                mouthPucker_blendName = EditorGUILayout.TextField("Blendshape Name", mouthPucker_blendName);
                mouthPucker_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthPucker_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthLeft_ena = EditorGUILayout.BeginToggleGroup("Enable mouthLeft", mouthLeft_ena);
            {
                mouthLeft_blendName = EditorGUILayout.TextField("Blendshape Name", mouthLeft_blendName);
                mouthLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthRight_ena = EditorGUILayout.BeginToggleGroup("Enable mouthRight", mouthRight_ena);
            {
                mouthRight_blendName = EditorGUILayout.TextField("Blendshape Name", mouthRight_blendName);
                mouthRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthRollUpper_ena = EditorGUILayout.BeginToggleGroup("Enable mouthRollUpper", mouthRollUpper_ena);
            {
                mouthRollUpper_blendName = EditorGUILayout.TextField("Blendshape Name", mouthRollUpper_blendName);
                mouthRollUpper_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthRollUpper_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthRollLower_ena = EditorGUILayout.BeginToggleGroup("Enable mouthRollLower", mouthRollLower_ena);
            {
                mouthRollLower_blendName = EditorGUILayout.TextField("Blendshape Name", mouthRollLower_blendName);
                mouthRollLower_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthRollLower_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthShrugUpper_ena = EditorGUILayout.BeginToggleGroup("Enable mouthShrugUpper", mouthShrugUpper_ena);
            {
                mouthShrugUpper_blendName = EditorGUILayout.TextField("Blendshape Name", mouthShrugUpper_blendName);
                mouthShrugUpper_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthShrugUpper_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthShrugLower_ena = EditorGUILayout.BeginToggleGroup("Enable mouthShrugLower", mouthShrugLower_ena);
            {
                mouthShrugLower_blendName = EditorGUILayout.TextField("Blendshape Name", mouthShrugLower_blendName);
                mouthShrugLower_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthShrugLower_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthClose_ena = EditorGUILayout.BeginToggleGroup("Enable mouthClose", mouthClose_ena);
            {
                mouthClose_blendName = EditorGUILayout.TextField("Blendshape Name", mouthClose_blendName);
                mouthClose_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthClose_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthSmileLeft_ena = EditorGUILayout.BeginToggleGroup("Enable mouthSmileLeft", mouthSmileLeft_ena);
            {
                mouthSmileLeft_blendName = EditorGUILayout.TextField("Blendshape Name", mouthSmileLeft_blendName);
                mouthSmileLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthSmileLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthSmileRight_ena = EditorGUILayout.BeginToggleGroup("Enable mouthSmileRight", mouthSmileRight_ena);
            {
                mouthSmileRight_blendName = EditorGUILayout.TextField("Blendshape Name", mouthSmileRight_blendName);
                mouthSmileRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthSmileRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthFrownLeft_ena = EditorGUILayout.BeginToggleGroup("Enable mouthFrownLeft", mouthFrownLeft_ena);
            {
                mouthFrownLeft_blendName = EditorGUILayout.TextField("Blendshape Name", mouthFrownLeft_blendName);
                mouthFrownLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthFrownLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthFrownRight_ena = EditorGUILayout.BeginToggleGroup("Enable mouthFrownRight", mouthFrownRight_ena);
            {
                mouthFrownRight_blendName = EditorGUILayout.TextField("Blendshape Name", mouthFrownRight_blendName);
                mouthFrownRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthFrownRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthDimpleLeft_ena = EditorGUILayout.BeginToggleGroup("Enable mouthDimpleLeft", mouthDimpleLeft_ena);
            {
                mouthDimpleLeft_blendName = EditorGUILayout.TextField("Blendshape Name", mouthDimpleLeft_blendName);
                mouthDimpleLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthDimpleLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthDimpleRight_ena = EditorGUILayout.BeginToggleGroup("Enable mouthDimpleRight", mouthDimpleRight_ena);
            {
                mouthDimpleRight_blendName = EditorGUILayout.TextField("Blendshape Name", mouthDimpleRight_blendName);
                mouthDimpleRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthDimpleRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthUpperUpLeft_ena = EditorGUILayout.BeginToggleGroup("Enable mouthUpperUpLeft", mouthUpperUpLeft_ena);
            {
                mouthUpperUpLeft_blendName = EditorGUILayout.TextField("Blendshape Name", mouthUpperUpLeft_blendName);
                mouthUpperUpLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthUpperUpLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthUpperUpRight_ena = EditorGUILayout.BeginToggleGroup("Enable mouthUpperUpRight", mouthUpperUpRight_ena);
            {
                mouthUpperUpRight_blendName = EditorGUILayout.TextField("Blendshape Name", mouthUpperUpRight_blendName);
                mouthUpperUpRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthUpperUpRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthLowerDownLeft_ena = EditorGUILayout.BeginToggleGroup("Enable mouthLowerDownLeft", mouthLowerDownLeft_ena);
            {
                mouthLowerDownLeft_blendName = EditorGUILayout.TextField("Blendshape Name", mouthLowerDownLeft_blendName);
                mouthLowerDownLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthLowerDownLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthLowerDownRight_ena = EditorGUILayout.BeginToggleGroup("Enable mouthLowerDownRight", mouthLowerDownRight_ena);
            {
                mouthLowerDownRight_blendName = EditorGUILayout.TextField("Blendshape Name", mouthLowerDownRight_blendName);
                mouthLowerDownRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthLowerDownRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthPressLeft_ena = EditorGUILayout.BeginToggleGroup("Enable mouthPressLeft", mouthPressLeft_ena);
            {
                mouthPressLeft_blendName = EditorGUILayout.TextField("Blendshape Name", mouthPressLeft_blendName);
                mouthPressLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthPressLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthPressRight_ena = EditorGUILayout.BeginToggleGroup("Enable mouthPressRight", mouthPressRight_ena);
            {
                mouthPressRight_blendName = EditorGUILayout.TextField("Blendshape Name", mouthPressRight_blendName);
                mouthPressRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthPressRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthStretchLeft_ena = EditorGUILayout.BeginToggleGroup("Enable mouthStretchLeft", mouthStretchLeft_ena);
            {
                mouthStretchLeft_blendName = EditorGUILayout.TextField("Blendshape Name", mouthStretchLeft_blendName);
                mouthStretchLeft_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthStretchLeft_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            mouthStretchRight_ena = EditorGUILayout.BeginToggleGroup("Enable mouthStretchRight", mouthStretchRight_ena);
            {
                mouthStretchRight_blendName = EditorGUILayout.TextField("Blendshape Name", mouthStretchRight_blendName);
                mouthStretchRight_blendWeight = EditorGUILayout.Slider("Blendshape Weight", mouthStretchRight_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);

            tongueOut_ena = EditorGUILayout.BeginToggleGroup("Enable tongueOut", tongueOut_ena);
            {
                tongueOut_blendName = EditorGUILayout.TextField("Blendshape Name", tongueOut_blendName);
                tongueOut_blendWeight = EditorGUILayout.Slider("Blendshape Weight", tongueOut_blendWeight, 0f, 100f);
            }
            EditorGUILayout.EndToggleGroup();
            EditorGUILayout.Space(CGUISpacing);
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space(CGUISpacing);

        if (GUILayout.Button("Apply", GUILayout.Height(30)))
            ApplyBlendshapes();
    }

    private void ApplyBlendshapes()
    {
        if (blendShapeProxy.BlendShapeAvatar == null || string.IsNullOrWhiteSpace(blendshapeRelPath))
            return;

        var bsa = blendShapeProxy.BlendShapeAvatar;
        var path = AssetDatabase.GetAssetPath(bsa);

        if (browInnerUp_ena)
            ProcessClip(bsa, path, CStr_browInnerUp, browInnerUp_blendName, browInnerUp_blendWeight);

        if (browDownLeft_ena)
            ProcessClip(bsa, path, CStr_browDownLeft, browDownLeft_blendName, browDownLeft_blendWeight);

        if (browDownRight_ena)
            ProcessClip(bsa, path, CStr_browDownRight, browDownRight_blendName, browDownRight_blendWeight);

        if (browOuterUpLeft_ena)
            ProcessClip(bsa, path, CStr_browOuterUpLeft, browOuterUpLeft_blendName, browOuterUpLeft_blendWeight);

        if (browOuterUpRight_ena)
            ProcessClip(bsa, path, CStr_browOuterUpRight, browOuterUpRight_blendName, browOuterUpRight_blendWeight);

        if (eyeLookUpLeft_ena)
            ProcessClip(bsa, path, CStr_eyeLookUpLeft, eyeLookUpLeft_blendName, eyeLookUpLeft_blendWeight);

        if (eyeLookUpRight_ena)
            ProcessClip(bsa, path, CStr_eyeLookUpRight, eyeLookUpRight_blendName, eyeLookUpRight_blendWeight);

        if (eyeLookDownLeft_ena)
            ProcessClip(bsa, path, CStr_eyeLookDownLeft, eyeLookDownLeft_blendName, eyeLookDownLeft_blendWeight);

        if (eyeLookDownRight_ena)
            ProcessClip(bsa, path, CStr_eyeLookDownRight, eyeLookDownRight_blendName, eyeLookDownRight_blendWeight);

        if (eyeLookInLeft_ena)
            ProcessClip(bsa, path, CStr_eyeLookInLeft, eyeLookInLeft_blendName, eyeLookInLeft_blendWeight);

        if (eyeLookInRight_ena)
            ProcessClip(bsa, path, CStr_eyeLookInRight, eyeLookInRight_blendName, eyeLookInRight_blendWeight);

        if (eyeLookOutLeft_ena)
            ProcessClip(bsa, path, CStr_eyeLookOutLeft, eyeLookOutLeft_blendName, eyeLookOutLeft_blendWeight);

        if (eyeLookOutRight_ena)
            ProcessClip(bsa, path, CStr_eyeLookOutRight, eyeLookOutRight_blendName, eyeLookOutRight_blendWeight);

        if (eyeBlinkLeft_ena)
            ProcessClip(bsa, path, CStr_eyeBlinkLeft, eyeBlinkLeft_blendName, eyeBlinkLeft_blendWeight);

        if (eyeBlinkRight_ena)
            ProcessClip(bsa, path, CStr_eyeBlinkRight, eyeBlinkRight_blendName, eyeBlinkRight_blendWeight);

        if (eyeSquintRight_ena)
            ProcessClip(bsa, path, CStr_eyeSquintRight, eyeSquintRight_blendName, eyeSquintRight_blendWeight);

        if (eyeSquintLeft_ena)
            ProcessClip(bsa, path, CStr_eyeSquintLeft, eyeSquintLeft_blendName, eyeSquintLeft_blendWeight);

        if (eyeWideLeft_ena)
            ProcessClip(bsa, path, CStr_eyeWideLeft, eyeWideLeft_blendName, eyeWideLeft_blendWeight);

        if (eyeWideRight_ena)
            ProcessClip(bsa, path, CStr_eyeWideRight, eyeWideRight_blendName, eyeWideRight_blendWeight);

        if (cheekPuff_ena)
            ProcessClip(bsa, path, CStr_cheekPuff, cheekPuff_blendName, cheekPuff_blendWeight);

        if (cheekSquintLeft_ena)
            ProcessClip(bsa, path, CStr_cheekSquintLeft, cheekSquintLeft_blendName, cheekSquintLeft_blendWeight);

        if (cheekSquintRight_ena)
            ProcessClip(bsa, path, CStr_cheekSquintRight, cheekSquintRight_blendName, cheekSquintRight_blendWeight);

        if (noseSneerLeft_ena)
            ProcessClip(bsa, path, CStr_noseSneerLeft, noseSneerLeft_blendName, noseSneerLeft_blendWeight);

        if (noseSneerRight_ena)
            ProcessClip(bsa, path, CStr_noseSneerRight, noseSneerRight_blendName, noseSneerRight_blendWeight);

        if (jawOpen_ena)
            ProcessClip(bsa, path, CStr_jawOpen, jawOpen_blendName, jawOpen_blendWeight);

        if (jawForward_ena)
            ProcessClip(bsa, path, CStr_jawForward, jawForward_blendName, jawForward_blendWeight);

        if (jawLeft_ena)
            ProcessClip(bsa, path, CStr_jawLeft, jawLeft_blendName, jawLeft_blendWeight);

        if (jawRight_ena)
            ProcessClip(bsa, path, CStr_jawRight, jawRight_blendName, jawRight_blendWeight);

        if (mouthFunnel_ena)
            ProcessClip(bsa, path, CStr_mouthFunnel, mouthFunnel_blendName, mouthFunnel_blendWeight);

        if (mouthPucker_ena)
            ProcessClip(bsa, path, CStr_mouthPucker, mouthPucker_blendName, mouthPucker_blendWeight);

        if (mouthLeft_ena)
            ProcessClip(bsa, path, CStr_mouthLeft, mouthLeft_blendName, mouthLeft_blendWeight);

        if (mouthRight_ena)
            ProcessClip(bsa, path, CStr_mouthRight, mouthRight_blendName, mouthRight_blendWeight);

        if (mouthRollUpper_ena)
            ProcessClip(bsa, path, CStr_mouthRollUpper, mouthRollUpper_blendName, mouthRollUpper_blendWeight);

        if (mouthRollLower_ena)
            ProcessClip(bsa, path, CStr_mouthRollLower, mouthRollLower_blendName, mouthRollLower_blendWeight);

        if (mouthShrugUpper_ena)
            ProcessClip(bsa, path, CStr_mouthShrugUpper, mouthShrugUpper_blendName, mouthShrugUpper_blendWeight);

        if (mouthShrugLower_ena)
            ProcessClip(bsa, path, CStr_mouthShrugLower, mouthShrugLower_blendName, mouthShrugLower_blendWeight);

        if (mouthClose_ena)
            ProcessClip(bsa, path, CStr_mouthClose, mouthClose_blendName, mouthClose_blendWeight);

        if (mouthSmileLeft_ena)
            ProcessClip(bsa, path, CStr_mouthSmileLeft, mouthSmileLeft_blendName, mouthSmileLeft_blendWeight);

        if (mouthSmileRight_ena)
            ProcessClip(bsa, path, CStr_mouthSmileRight, mouthSmileRight_blendName, mouthSmileRight_blendWeight);

        if (mouthFrownLeft_ena)
            ProcessClip(bsa, path, CStr_mouthFrownLeft, mouthFrownLeft_blendName, mouthFrownLeft_blendWeight);

        if (mouthFrownRight_ena)
            ProcessClip(bsa, path, CStr_mouthFrownRight, mouthFrownRight_blendName, mouthFrownRight_blendWeight);

        if (mouthDimpleLeft_ena)
            ProcessClip(bsa, path, CStr_mouthDimpleLeft, mouthDimpleLeft_blendName, mouthDimpleLeft_blendWeight);

        if (mouthDimpleRight_ena)
            ProcessClip(bsa, path, CStr_mouthDimpleRight, mouthDimpleRight_blendName, mouthDimpleRight_blendWeight);

        if (mouthUpperUpLeft_ena)
            ProcessClip(bsa, path, CStr_mouthUpperUpLeft, mouthUpperUpLeft_blendName, mouthUpperUpLeft_blendWeight);

        if (mouthUpperUpRight_ena)
            ProcessClip(bsa, path, CStr_mouthUpperUpRight, mouthUpperUpRight_blendName, mouthUpperUpRight_blendWeight);

        if (mouthLowerDownLeft_ena)
            ProcessClip(bsa, path, CStr_mouthLowerDownLeft, mouthLowerDownLeft_blendName, mouthLowerDownLeft_blendWeight);

        if (mouthLowerDownRight_ena)
            ProcessClip(bsa, path, CStr_mouthLowerDownRight, mouthLowerDownRight_blendName, mouthLowerDownRight_blendWeight);

        if (mouthPressLeft_ena)
            ProcessClip(bsa, path, CStr_mouthPressLeft, mouthPressLeft_blendName, mouthPressLeft_blendWeight);

        if (mouthPressRight_ena)
            ProcessClip(bsa, path, CStr_mouthPressRight, mouthPressRight_blendName, mouthPressRight_blendWeight);

        if (mouthStretchLeft_ena)
            ProcessClip(bsa, path, CStr_mouthStretchLeft, mouthStretchLeft_blendName, mouthStretchLeft_blendWeight);

        if (mouthStretchRight_ena)
            ProcessClip(bsa, path, CStr_mouthStretchRight, mouthStretchRight_blendName, mouthStretchRight_blendWeight);

        if (tongueOut_ena)
            ProcessClip(bsa, path, CStr_tongueOut, tongueOut_blendName, tongueOut_blendWeight);

        EditorUtility.SetDirty(bsa);
    }

    private void ProcessClip(BlendShapeAvatar bsa, string bsaPath, string clipName, string blendShapeName, float weight)
    {
        int idx = -1;

        try
        {
            idx = blendshapeRenderer.sharedMesh.GetBlendShapeIndex(blendShapeName);
        }
        catch { }

        if (idx <= -1)
            return;

        BlendShapeKey key = BlendShapeKey.CreateUnknown(clipName);
        BlendShapeClip clip = bsa.GetClip(key);

        if (clip == null || clip == default(BlendShapeClip))
        {
            var clipPath = Path.Combine(Path.GetDirectoryName(bsaPath), $"{clipName}.asset");
            clip = BlendShapeAvatar.CreateBlendShapeClip(clipPath);
        }

        clip.MaterialValues = Array.Empty<MaterialValueBinding>();
        clip.Values = new BlendShapeBinding[] {
            new() {
                RelativePath = blendshapeRelPath,
                Index = idx,
                Weight = weight
            }
        };
        EditorUtility.SetDirty(clip);
        bsa.SetClip(key, clip);
    }

    private string GetGameObjectPath(GameObject obj)
    {
        string path = obj.name;

        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = $"{obj.name}/{path}";
        }

        return path[(path.IndexOf('/') + 1)..];
    }
}
