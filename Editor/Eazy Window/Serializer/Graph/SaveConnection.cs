using UnityEngine;

public class SaveConnection 
{
    [SerializeField]
    private string guidA;

    [SerializeField]
    private string guidB;

    public string GuidA { get => guidA; set => guidA = value; }

    public string GuidB { get => guidB; set => guidB = value; }
}
