using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Flow;
public class SceneFlow : IFlowTask
{
    public int layer => 0;

    public int order => 10;

    public  async Task Logic()
    {
        Debug.Log("���س���");

        await Addressables.LoadSceneAsync("Assets/Scenes/hotfix");

        Debug.Log("�����������");
    }

}
