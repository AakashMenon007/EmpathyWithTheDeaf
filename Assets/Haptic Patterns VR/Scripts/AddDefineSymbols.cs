using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace HapticPatterns
{
    /// <summary>
    /// Adds the "HAPTIC_PATTERNS" define symbol to PlayerSettings define symbols.
    /// </summary>
    [InitializeOnLoad]
    public class AddDefineSymbols : Editor
    {
 
        /// <summary>
        /// Symbols that will be added to the editor
        /// </summary>
        public static readonly string [] Symbols = new string[] {
            "HAPTIC_PATTERNS",
        };
 
        /// <summary>
        /// Add define symbols as soon as Unity gets done compiling.
        /// </summary>
        static AddDefineSymbols ()
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup ( EditorUserBuildSettings.selectedBuildTargetGroup );
            List<string> allDefines = definesString.Split ( ';' ).ToList ();
            allDefines.AddRange ( Symbols.Except ( allDefines ) );
            PlayerSettings.SetScriptingDefineSymbolsForGroup (
                EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join ( ";", allDefines.ToArray () ) );
        }
 
    }
}
#endif