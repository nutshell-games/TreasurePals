﻿//----------------------------------------------
// Flip Web Apps: Beautiful Transitions
// Copyright © 2016 Flip Web Apps / Mark Hewitt
//
// Please direct any bugs/comments/suggestions to http://www.flipwebapps.com
// 
// The copyright owner grants to the end user a non-exclusive, worldwide, and perpetual license to this Asset
// to integrate only as incorporated and embedded components of electronic games and interactive media and 
// distribute such electronic game and interactive media. End user may modify Assets. End user may otherwise 
// not reproduce, distribute, sublicense, rent, lease or lend the Assets. It is emphasized that the end 
// user shall not be entitled to distribute or transfer in any way (including, without, limitation by way of 
// sublicense) the Assets in any other way than as integrated components of electronic games and interactive media. 

// The above copyright notice and this permission notice must not be removed from any files.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//----------------------------------------------

using FlipWebApps.BeautifulTransitions.Scripts.Transitions.TransitionSteps;
using UnityEngine;

namespace FlipWebApps.BeautifulTransitions.Scripts.Transitions.GameObject
{
    [AddComponentMenu("Beautiful Transitions/GameObject + UI/Fade")]
    [HelpURL("http://www.flipwebapps.com/beautiful-transitions/")]
    public class TransitionFade : TransitionGameObjectBase
    {
        [Header("Fade Specific")]
        public InSettings FadeInConfig;
        public OutSettings FadeOutConfig;

        float _originalTransparency;

        #region TransitionBase Overrides

        /// <summary>
        /// Gather any initial state - See TrtansitionBase for further details
        /// </summary>
        public override void SetupInitialState()
        {
            _originalTransparency = ((Fade)CreateTransitionStep()).OriginalValue;
        }

        /// <summary>
        /// Get an instance of the current transition item
        /// </summary>
        /// <returns></returns>
        public override TransitionStep CreateTransitionStep()
        {
            return new Fade(Target);
        }

        /// <summary>
        /// Add common values to the transitionStep for the in transition
        /// </summary>
        /// <param name="transitionStep"></param>
        public override void SetupTransitionStepIn(TransitionStep transitionStep)
        {
            var transitionStepFade = transitionStep as Fade;
            if (transitionStepFade != null)
            {
                transitionStepFade.StartValue = FadeInConfig.StartTransparency;
                transitionStepFade.EndValue = _originalTransparency;
            }
            base.SetupTransitionStepIn(transitionStep);
        }

        /// <summary>
        /// Add common values to the transitionStep for the out transition
        /// </summary>
        /// <param name="transitionStep"></param>
        public override void SetupTransitionStepOut(TransitionStep transitionStep)
        {
            var transitionStepFade = transitionStep as Fade;
            if (transitionStepFade != null)
            {
                transitionStepFade.StartValue = transitionStepFade.GetCurrent();
                transitionStepFade.EndValue = FadeOutConfig.EndTransparency;
            }
            base.SetupTransitionStepOut(transitionStep);
        }

        #endregion TransitionBase Overrides

        #region Transition specific settings
        [System.Serializable]
        public class InSettings
        {
            [Tooltip("Normalised transparency at the start of the transition (ends at the GameObjects initial transparency).")]
            public float StartTransparency = 0;
        }

        [System.Serializable]
        public class OutSettings
        {
            [Tooltip("Normalised transparency at the end of the transition (starts at the GameObjects current transparency).")]
            public float EndTransparency = 0;
        }
        #endregion Transition specific settings
    }
}
