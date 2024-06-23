#if UNITY_EDITOR
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace iOSKeyChainPlugin.iOS.Xcode
{
	public class iOSKeyChainPostprocessor
	{
		// support for version < Unity 5
#if !UNITY_5 && !UNITY_2017 && !UNITY_2018 && !UNITY_2019 && !UNITY_2020 && !UNITY_2021 && !UNITY_2022 && !UNITY_2023 && !UNITY_2024 && !UNITY_2025
		static readonly string[] sourceFiles = new[]
		{
			"KeyChainPlugin.h",
			"KeyChainPlugin.mm",
			"UICKeyChainStore.h",
			"UICKeyChainStore.mm"
		};

		[PostProcessBuild]
		public static void OnPostProcessBuild( BuildTarget buildTarget, string buildPath )
		{
			if (buildTarget == BuildTarget.iOS)
			{
				var projPath = PBXProject.GetPBXProjectPath( buildPath );
				PBXProject proj = new PBXProject();
				proj.ReadFromString( File.ReadAllText( projPath ) );
				var targetGuid = proj.TargetGuidByName( "Unity-iPhone" );

				var instance = ScriptableObject.CreateInstance<IKeyChainPluginPath>(); 
				var pluginPath = Path.GetDirectoryName( AssetDatabase.GetAssetPath( MonoScript.FromScriptableObject( instance ) ) );
				ScriptableObject.DestroyImmediate( instance );

				var targetPath = pluginPath.Replace("Assets", "Libraries");
				//var targetPath = "Libraries/iOSKeyChainPlugin/Plugins/iOS";
				Directory.CreateDirectory( Path.Combine( buildPath, targetPath ) );

				foreach (var fileName in sourceFiles)
				{
					var sourcePath = Path.Combine( pluginPath, fileName );
					var targetFile = Path.Combine( targetPath, fileName );
					File.Copy( sourcePath, Path.Combine( buildPath, targetFile ), true );
					proj.AddFileToBuild( targetGuid, proj.AddFile( targetFile, targetFile, PBXSourceTree.Source ) );
				}

				File.WriteAllText( projPath, proj.WriteToString() );
			}
		}
#endif
	}
}
#endif

