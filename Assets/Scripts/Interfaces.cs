using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public interface ISwitchable
    {
        public bool SwitchedOn { get; set; }
        void SwitchOn();
        void SwitchOff();
    }    
}