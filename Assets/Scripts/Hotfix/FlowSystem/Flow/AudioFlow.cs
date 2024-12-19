using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Flow;
using Audio;

public class AudioFlow : IFlowTask
{
    public  int layer => 0;
    public  int order => 22;

    public  async Task Logic()
    {
        Debug.Log("开始读取sound Asset");
        var asset=  await Addressables.LoadAssetAsync<AudioSoundData>("sound");
        Debug.Log("读取sound Asset完成");
    }
}
