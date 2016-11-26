using System;
using System.Collections.Generic;
using Ink.Runtime;

namespace GameFrame.Ink
{
    public class GameFrameStory : ICompleteAble
    {
        private readonly Story _story;
        private readonly HashSet<string> _functionNames;
        public List<Choice> Choices => _story.currentChoices;
        public bool Complete => !_story.canContinue && string.IsNullOrEmpty(CurrentText);
        public string CurrentText { get; internal set; }

        public bool CanContinue => _story.canContinue;

        public void Continue()
        {
            if (CanContinue)
            {
                _story.Continue();
                CurrentText = _story.currentText;
            }
            else
            {
                CurrentText = null;
            }
        }

        public T GetVariableState<T>(string variableName)
        {
            return (T)_story.variablesState[variableName];
        }

        public void SetVariableState(string variableName, int value)
        {
            _story.variablesState[variableName] = value;
        }

        public void ObserveVariable(string variableName, Story.VariableObserver observer)
        {
            _story.ObserveVariable(variableName, observer);
        }

        public GameFrameStory(string jsonString)
        {
            _functionNames = new HashSet<string>();
            _story = new Story(jsonString);
        }

        public void ChooseChoiceIndex(int index)
        {
            _story.ChooseChoiceIndex(index);
        }

        public void BindFunction<T1, T2>(string functionName, Action<T1, T2> action)
        {
            if (!_functionNames.Contains(functionName))
            {
                _story.BindExternalFunction(functionName, action);
                _functionNames.Add(functionName);
            }
        }

        public void BindFunction<T1>(string functionName, Action<T1> action)
        {
            if (!_functionNames.Contains(functionName))
            {
                _story.BindExternalFunction(functionName, action);
                _functionNames.Add(functionName);
            }
        }

        public void ChoosePathString(string fishVariable)
        {
            _story.ChoosePathString(fishVariable);
        }
    }
}
