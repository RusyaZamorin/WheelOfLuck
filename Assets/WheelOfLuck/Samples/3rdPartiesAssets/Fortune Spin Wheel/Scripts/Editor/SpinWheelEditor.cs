using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpinWheelManager), true)]
public class SpinWheelEditor : Editor
{

    private readonly string version = "1.1";
    private SerializedProperty items, wheel, handle, startButton, happyParticle, beepAudio, winAudio,
        reverseWheelRotation, reverseHandleRotation, spinSpeed, minSpinSpeed, spinRounds,
        autoGenerateParent, circleSprite, pinSprite, randomColor, hasItemSpace, spaceSize, spaceColor,
        generateItemsContent, generateItemsText, generateItemsIcon, itemsTextPosition, itemsTextColor,
        itemsTextSize, itemsTextAlignment, itemsHasOutline, itemsOutlineColor, itemsIconPosition, itemsIconSize;

    private bool showMainSettings = true;
    private bool showMovementSettings = true;
    private bool showAutoGenerate = true;

    private SpinWheelManager spinWheel;

    private void OnEnable()
    {
        spinWheel = target as SpinWheelManager;

        items = serializedObject.FindProperty("items");
        wheel = serializedObject.FindProperty("wheel");
        handle = serializedObject.FindProperty("handle");
        startButton = serializedObject.FindProperty("startButton");
        happyParticle = serializedObject.FindProperty("happyParticle");
        beepAudio = serializedObject.FindProperty("beepAudio");
        winAudio = serializedObject.FindProperty("winAudio");
        reverseWheelRotation = serializedObject.FindProperty("reverseWheelRotation");
        reverseHandleRotation = serializedObject.FindProperty("reverseHandleRotation");
        spinSpeed = serializedObject.FindProperty("spinSpeed");
        minSpinSpeed = serializedObject.FindProperty("minSpinSpeed");
        spinRounds = serializedObject.FindProperty("spinRounds");
        autoGenerateParent = serializedObject.FindProperty("autoGenerateParent");
        circleSprite = serializedObject.FindProperty("circleSprite");
        pinSprite = serializedObject.FindProperty("pinSprite");
        randomColor = serializedObject.FindProperty("randomColor");
        hasItemSpace = serializedObject.FindProperty("hasItemSpace");
        spaceSize = serializedObject.FindProperty("spaceSize");
        spaceColor = serializedObject.FindProperty("spaceColor");
        generateItemsContent = serializedObject.FindProperty("generateItemsContent");
        generateItemsText = serializedObject.FindProperty("generateItemsText");
        generateItemsIcon = serializedObject.FindProperty("generateItemsIcon");
        itemsTextPosition = serializedObject.FindProperty("itemsTextPosition");
        itemsTextColor = serializedObject.FindProperty("itemsTextColor");
        itemsTextSize = serializedObject.FindProperty("itemsTextSize");
        itemsTextAlignment = serializedObject.FindProperty("itemsTextAlignment");
        itemsHasOutline = serializedObject.FindProperty("itemsHasOutline");
        itemsOutlineColor = serializedObject.FindProperty("itemsOutlineColor");
        itemsIconPosition = serializedObject.FindProperty("itemsIconPosition");
        itemsIconSize = serializedObject.FindProperty("itemsIconSize");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        MainSettings();
        MovementSettings();
        AutoGeneration();
        FooterInformation();

        serializedObject.ApplyModifiedProperties();
    }

    private void MainSettings()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorStyles.foldout.fontStyle = FontStyle.Bold;
        showMainSettings = EditorGUILayout.Foldout(showMainSettings, new GUIContent("Main Settings"), true);
        EditorStyles.foldout.fontStyle = FontStyle.Normal;

        if (showMainSettings)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(items, new GUIContent("Items", "Define your items count, chances and color."), true);

            EditorGUILayout.PropertyField(wheel, new GUIContent("Wheel"));
            EditorGUILayout.PropertyField(handle, new GUIContent("Handle"));
            EditorGUILayout.PropertyField(startButton, new GUIContent("Start Button"));
            EditorGUILayout.PropertyField(happyParticle, new GUIContent("Win Particle"));
            EditorGUILayout.PropertyField(beepAudio, new GUIContent("Beep Audio"));
            EditorGUILayout.PropertyField(winAudio, new GUIContent("Win Audio"));

            EditorGUI.indentLevel--;
        }
    }

    private void MovementSettings()
    {
        EditorStyles.foldout.fontStyle = FontStyle.Bold;
        showMovementSettings = EditorGUILayout.Foldout(showMovementSettings, new GUIContent("Movement Settings"), true);
        EditorStyles.foldout.fontStyle = FontStyle.Normal;

        if (showMovementSettings)
        {
            EditorGUI.indentLevel++;
            
            EditorGUILayout.PropertyField(reverseWheelRotation, new GUIContent("Reverse Wheel Rotation"));
            EditorGUILayout.PropertyField(reverseHandleRotation, new GUIContent("Reverse Handle Rotation"));
            EditorGUILayout.PropertyField(spinSpeed, new GUIContent("Spin Speed"));
            EditorGUILayout.PropertyField(minSpinSpeed, new GUIContent("Minimum Speed"));
            EditorGUILayout.IntSlider(spinRounds, 3, 50, new GUIContent("Spin Rounds", "How many times the wheel should to spin."));

            EditorGUI.indentLevel--;
        }
    }

    private void AutoGeneration()
    {
        EditorStyles.foldout.fontStyle = FontStyle.Bold;
        showAutoGenerate = EditorGUILayout.Foldout(showAutoGenerate, new GUIContent("Auto Generate Items"), true);
        EditorStyles.foldout.fontStyle = FontStyle.Normal;

        if (showAutoGenerate)
        {
            EditorGUI.indentLevel++;

            EditorGUILayout.PropertyField(autoGenerateParent, new GUIContent("Auto Generate Parent"));
            EditorGUILayout.PropertyField(circleSprite, new GUIContent("Circle Sprite"));
            EditorGUILayout.PropertyField(pinSprite, new GUIContent("Pin Sprite"));
            EditorGUILayout.PropertyField(randomColor, new GUIContent("Random Color", "If checked, the color of items will be random."));
            EditorGUILayout.PropertyField(hasItemSpace, new GUIContent("Has Item Space"));

            if (spinWheel.hasItemSpace)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(spaceSize, new GUIContent("Space Size"));
                EditorGUILayout.PropertyField(spaceColor, new GUIContent("Space Color"));
                EditorGUI.indentLevel--;
            }
            
            EditorGUILayout.PropertyField(generateItemsContent, new GUIContent("Generate Items Content", "Items Content including text and icon will be generated."));

            if (spinWheel.generateItemsContent)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(generateItemsText, new GUIContent("Text"));

                if (spinWheel.generateItemsText)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(itemsTextPosition, new GUIContent("Text Position"));
                    EditorGUILayout.PropertyField(itemsTextColor, new GUIContent("Text Color"));
                    EditorGUILayout.PropertyField(itemsTextSize, new GUIContent("Text Size"));
                    EditorGUILayout.PropertyField(itemsTextAlignment, new GUIContent("Text Alignment"));
                    EditorGUILayout.PropertyField(itemsHasOutline, new GUIContent("Has Outline"));
                    EditorGUILayout.PropertyField(itemsOutlineColor, new GUIContent("itemsOutlineColor"));
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.PropertyField(generateItemsIcon, new GUIContent("Icon"));

                if (spinWheel.generateItemsIcon)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(itemsIconPosition, new GUIContent("Icon Position"));
                    EditorGUILayout.PropertyField(itemsIconSize, new GUIContent("Icon Size"));
                    EditorGUI.indentLevel--;
                }

                EditorGUI.indentLevel--;
            }
            
            GUILayout.BeginHorizontal();
            GUILayout.Space(EditorGUI.indentLevel * 16);
            if (GUILayout.Button("Auto Generate (Changes will lost)"))
            {
                spinWheel.AutoGenerateSpin();
            }
            GUILayout.EndHorizontal();
            
            EditorGUILayout.Space();

            EditorGUI.indentLevel--;
        }
    }

    private void FooterInformation()
    {
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        GUILayout.BeginVertical("HelpBox");

        GUIStyle style = new GUIStyle(EditorStyles.label);
        style.normal.textColor = Color.black;
        style.fontSize = 20;
        style.alignment = TextAnchor.MiddleLeft;
        GUILayout.Label("Fortune Spin Wheel", style);

        EditorGUILayout.Space();
        style.normal.textColor = Color.gray;
        style.fontSize = 18;
        GUILayout.Label("Version: " + version, style);

        EditorGUILayout.Space();
        style.normal.textColor = Color.gray;
        GUILayout.Label("Author: Hojjat.Reyhane", style);
        GUILayout.EndVertical();
    }
}
