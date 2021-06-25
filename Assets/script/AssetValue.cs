using UnityEngine;

[CreateAssetMenu(fileName = "New Value", menuName = "Value", order = 52)]
public class AssetValue : ScriptableObject, IValue
{
    public string value => _value;
    public Sprite icon => _icon;

    [SerializeField] private string _value;
    [SerializeField] private Sprite _icon;

    [Header("Использовать в случае нестандартных размеров спрайта !!!")]
    public Vector3 Scale=new Vector3(1,1,1);
    public Vector3 Rotation=new Vector3(0,0,0);
}
