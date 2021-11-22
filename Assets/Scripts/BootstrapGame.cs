using Services;
using UnityEngine;
using Zenject;

public class BootstrapGame : MonoBehaviour
{
    private SceneLoaderService _sceneLoaderService;
    
    [Inject]
    private void Construct(SceneLoaderService sceneLoaderService)
    {
        _sceneLoaderService = sceneLoaderService;
    }

    private void Start()
    {
        _sceneLoaderService.LoadLevel(1);
    }
}