using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.Events;

public class MainLogic : MonoBehaviour
{
    public static UnityEvent startup = new UnityEvent();

    async void Start()
    {
        Debug.Log("检测资源是否有更新");

        //检查版本资源是否有更新
       // var   updateCatalog = this.gameObject.AddComponent<CheckUpdateCatalog>();
      //  await updateCatalog.CheckUpdte();

        //初始化资源加载
      //  await AssetLoader.instance.Init();

#if CLR
        var hotfix = this.gameObject.AddComponent<LoadHotfix>();
        await hotfix.Load();
#endif

        startup?.Invoke();
    }

}

