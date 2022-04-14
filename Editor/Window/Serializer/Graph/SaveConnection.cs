using UnityEngine;

// TODO get rid of these classes and just use the scriptable objects !! :)
public class SaveConnection 
{
    [SerializeField]
    private string guidA;

    [SerializeField]
    private string guidB;

    [SerializeField]
    private string portAName;

    [SerializeField]
    private string portBName;

    public string GuidA { get => guidA; set => guidA = value; }
    public string GuidB { get => guidB; set => guidB = value; }

    public string PortAName { get => portAName; set => portAName = value; }
    public string PortBName { get => portBName; set => portBName = value; }
}
