#if CLR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LogInfo;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;

public class LoadHotfix : MonoBehaviour
{
    public static string AotDll = "AotDll";
    public static string HotDll = "HotDll";

    public static List<TextAsset> aotdllAssets = new List<TextAsset>();
    public static List<TextAsset> hotdllAssets = new List<TextAsset>();


    public async Task Load()
    {
        Info.Log("加载aot dll");
        await DownLoadLabelDlls(AotDll, aotdllAssets);

        Info.Log("加载hot dll");
        await DownLoadLabelDlls(HotDll, hotdllAssets);


        Info.Log("Assembly.Load => hot dll");

        foreach (var asset in hotdllAssets)
        {
            Info.Log("dll:"+asset.name);

            if (asset.name != "Assembly-CSharp.dll")
                continue;

            var assembly = Assembly.Load(asset.bytes);
            var program  = assembly.GetType("Program");

            Info.Log("hotfix程序：" + program);

            if (program != null)
            {
                Info.Log("实例化aot dll");
                program.GetMethod("LoadMetadataForAOTAssemblies")?.Invoke(null, null);

                Info.Log("实例化hot dll");
                program.GetMethod("Main")?.Invoke(null, null);

                break;
            }
        }
    }

    async Task DownLoadLabelDlls<T>(string label,List<T> assets)
    {
        var load = await Addressables.LoadAssetsAsync<T>( new AssetLabelReference() { labelString = label }
            ,(_) =>
            {
                if (!assets.Contains(_))
                {
                    assets.Add(_);
                }
            })
            .Task;
    }

    private void OnDestroy()
    {
        aotdllAssets.Clear();
        hotdllAssets.Clear();
    }
}
#endif
