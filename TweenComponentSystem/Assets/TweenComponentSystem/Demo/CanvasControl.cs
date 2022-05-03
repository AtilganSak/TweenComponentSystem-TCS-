using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TweenComponentSystem;
using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    public DOBase doBase;

    [Header("Inputs")]
    public InputField durationInput;
    public InputField revertDurationInput;
    public InputField delayInput;
    public InputField revertDelayInput;

    [Header("Dropdowns")]
    public Dropdown loopTypeDrop;
    public Dropdown easeDrop;

    [Header("Action Buttons")]
    public Button doButton;
    public Button doRevertButton;
    public Button resetButton;
    public Button loopButton;
    public Button playPauseButton;

    [Header("Scenario")]
    public Button nextScenarioButton;
    public Button previousScenarioButton;
    public Text scenarioNameText;
    public Transform scenarioParent;
    private int curScenarioIndex;

    private bool pause;

    private void Start()
    {
        loopTypeDrop.options.Clear();
        string[] names = Enum.GetValues(typeof(LoopType)).Cast<LoopType>().Select(v => v.ToString()).ToArray();
        for (int i = 0; i < names.Length; i++)
        {
            loopTypeDrop.options.Add(new Dropdown.OptionData { text = names[i] });
        }
        easeDrop.options.Clear();
        names = Enum.GetValues(typeof(Ease)).Cast<Ease>().Select(v => v.ToString()).ToArray();
        for (int i = 0; i < names.Length; i++)
        {
            easeDrop.options.Add(new Dropdown.OptionData { text = names[i] });
        }

        GetComponentSettings();

        doButton.onClick.AddListener(Pressed_DO_Button);
        doRevertButton.onClick.AddListener(Pressed_DORevert_Button);
        resetButton.onClick.AddListener(Pressed_Reset_Button);
        loopButton.onClick.AddListener(Pressed_Loop_Button);
        playPauseButton.onClick.AddListener(Pressed_PlayPause_Button);

        durationInput.onValueChanged.AddListener(DurationInputChanged);
        revertDurationInput.onValueChanged.AddListener(RevertDurationInputChanged);
        delayInput.onValueChanged.AddListener(DelayInputChanged);
        revertDelayInput.onValueChanged.AddListener(RevertDelayInputChanged);
        loopTypeDrop.onValueChanged.AddListener(LoopTypeChanged);
        easeDrop.onValueChanged.AddListener(EaseChanged);

        playPauseButton.GetComponentInChildren<Text>().text = "Play";

        nextScenarioButton.onClick.AddListener(Pressed_NextScenario_Button);
        previousScenarioButton.onClick.AddListener(Pressed_PreviousScenario_Button);
        curScenarioIndex = 0;
        scenarioNameText.text = scenarioParent.GetChild(curScenarioIndex).name;
        scenarioParent.GetChild(curScenarioIndex).gameObject.SetActive(true);
    }
    private void Pressed_DO_Button()
    {
        doBase.DO();
    }
    private void Pressed_DORevert_Button()
    {
        doBase.DORevert();
    }
    private void Pressed_Reset_Button()
    {
        doBase.ResetDO();
    }
    private void Pressed_Loop_Button()
    {
        doBase.DOLoop();
    }
    private void Pressed_PlayPause_Button()
    {
        if (!pause)
        {
            doBase.DOPause();
            playPauseButton.GetComponentInChildren<Text>().text = "Play";
        }
        else
        {
            doBase.DOPlay();
            playPauseButton.GetComponentInChildren<Text>().text = "Pause";
        }
    }

    private void DurationInputChanged(string input)
    {
        int value = int.Parse(input);
        doBase.doDuration = value;
    }
    private void RevertDurationInputChanged(string input)
    {
        int value = int.Parse(input);
        doBase.revertDuration = value;
    }
    private void DelayInputChanged(string input)
    {
        int value = int.Parse(input);
        doBase.doDelay = value;
    }
    private void RevertDelayInputChanged(string input)
    {
        int value = int.Parse(input);
        doBase.revertDelay = value;
    }
    private void LoopTypeChanged(int input)
    {
        doBase.loopType = (LoopType)input;
    }
    private void EaseChanged(int input)
    {
        doBase.doEase = (Ease)input;
    }

    private void Pressed_NextScenario_Button()
    {
        doBase.ResetDO();
        scenarioParent.GetChild(curScenarioIndex).gameObject.SetActive(false);
        if (curScenarioIndex + 1 > scenarioParent.childCount - 1)
            curScenarioIndex = 0;
        else
            curScenarioIndex++;
        scenarioParent.GetChild(curScenarioIndex).gameObject.SetActive(true);
        scenarioNameText.text = scenarioParent.GetChild(curScenarioIndex).name;

        doBase = scenarioParent.GetChild(curScenarioIndex).GetComponentInChildren<DOBase>();
        GetComponentSettings();
    }
    private void Pressed_PreviousScenario_Button()
    {
        doBase.ResetDO();
        scenarioParent.GetChild(curScenarioIndex).gameObject.SetActive(false);
        if (curScenarioIndex - 1 < 0)
            curScenarioIndex = scenarioParent.childCount - 1;
        else
            curScenarioIndex--;
        scenarioParent.GetChild(curScenarioIndex).gameObject.SetActive(true);
        scenarioNameText.text = scenarioParent.GetChild(curScenarioIndex).name;

        doBase = scenarioParent.GetChild(curScenarioIndex).GetComponentInChildren<DOBase>();
        GetComponentSettings();
    }

    private void GetComponentSettings()
    {
        durationInput.text = doBase.doDuration.ToString();
        revertDurationInput.text = doBase.revertDuration.ToString();
        delayInput.text = doBase.doDelay.ToString();
        revertDelayInput.text = doBase.revertDelay.ToString();
        loopTypeDrop.value = (int)doBase.loopType;
        loopTypeDrop.RefreshShownValue();
        easeDrop.value = (int)doBase.doEase;
        easeDrop.RefreshShownValue();
    }
}
