using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor.Validation;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityServiceLocator;

[RequireComponent(typeof(ServiceLocator))]
[RequireComponent(typeof(ServiceLocatorGlobal))]
public class PersistentInitializer : MonoBehaviour
{
    private ServiceLocator _serviceLocator;
    [SerializeField] private AudioSystem audioSystem;

    [SerializeField] private SceneReference sceneReference;
    
    private void Awake()
    {
        _serviceLocator = GetComponent<ServiceLocator>();
        
        // init and register audio system
        _serviceLocator.Register<IAudioService>(audioSystem); // Global Scope
        audioSystem.Initialize();
    }

    private void EnterMainMenu()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
