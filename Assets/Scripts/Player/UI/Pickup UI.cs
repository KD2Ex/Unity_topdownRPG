using TMPro;
using UnityEngine;

public class PickupUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPrefab;

    public void ShowPickupItem(PickupInfo info)
    {
        var inst = Instantiate(textPrefab, transform);
        inst.text = $"+{info.Count} {info.Name}";

        StartCoroutine(Coroutines.WaitFor(1f, null, () => Destroy(inst.gameObject)));
    }

}
