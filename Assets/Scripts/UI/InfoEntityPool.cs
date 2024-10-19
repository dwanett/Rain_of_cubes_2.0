using TMPro;
using UnityEngine;

public class InfoEntityPool<T> : MonoBehaviour where T : Entity
{
    [SerializeField] private TextMeshProUGUI _counterSpwan;
    [SerializeField] private TextMeshProUGUI _counterCreate;
    [SerializeField] private TextMeshProUGUI _counterActive;
    [SerializeField] private Spawner<T> _spawner;

    private string _defaultTextCounterSpwan;
    private string _defaultTextCounterCreate;
    private string _defaultTextCounterActive;
    
    private void OnEnable()
    {
        _spawner.Spawned += ChangeSpawnCount;
    }
    
    private void OnDisable()
    {
        _spawner.Spawned -= ChangeSpawnCount;
    }

    private void Start()
    {
        _defaultTextCounterSpwan = _counterSpwan.text;
        _defaultTextCounterCreate = _counterCreate.text;
        _defaultTextCounterActive = _counterActive.text;
        ChangeNumberInText(_counterSpwan, _defaultTextCounterSpwan, 0);
        ChangeNumberInText(_counterCreate, _defaultTextCounterCreate,  0);
        ChangeNumberInText(_counterActive, _defaultTextCounterActive, 0);
    }

    private void Update()
    {
        ChangeNumberInText(_counterCreate, _defaultTextCounterCreate, _spawner.CountInstance);
        ChangeNumberInText(_counterActive, _defaultTextCounterActive, _spawner.CountActive);
    }

    private void ChangeSpawnCount(int countSpawn)
    {
        ChangeNumberInText(_counterSpwan, _defaultTextCounterSpwan, countSpawn);
    }
    
    private void ChangeNumberInText(TextMeshProUGUI uiText, string defaultText, int number)
    {
        uiText.text = $"{defaultText} {number}";
    }
}
