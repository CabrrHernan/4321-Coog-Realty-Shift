using UnityEngine;
using System.Linq;

public class Rock : MonoBehaviour
{
    public string rockType; // Type of rock (e.g., hematite, magnetite)
    public float weight;    // Weight of the rock
    private Color streakColor; // Color produced on the streak plate
    private string correctBin; // Correct bin for the rock ("Small" or "Big")

    // Custom colors for streaks
    public static readonly Color Brown = new Color(117f / 255f, 42f / 255f, 23f / 255f);
    public static readonly Color LightBlack = new Color(44f / 255f, 16f / 255f, 10f / 255f);

    [SerializeField] private int _penSize = 2;

    private Renderer _renderer;
    private Color[] _colors;

    private RaycastHit _touch;
    private StreakPlate _whiteboard;
    private bool _touchedLastFrame;
    private Vector2 _touchPos;
    private Vector2 _lastTouchPos;
    private Quaternion _lastTouchRot;

    void Start()
    {
        // Determine correct bin and streak color
        correctBin = weight <= 5 ? "Small" : "Big";
        streakColor = rockType == "hematite" ? Brown : LightBlack;

        _renderer = GetComponent<Renderer>();
        _colors = Enumerable.Repeat(streakColor, _penSize * _penSize).ToArray();
    }

    void Update()
    {
        Debug.Log("Update running");
        Draw();
    }

    private void Draw()
    {
        Vector3[] rayOrigins = {
            transform.position,
            transform.position + transform.forward * 0.1f,
            transform.position - transform.forward * 0.1f,
            transform.position + transform.right * 0.1f,
            transform.position - transform.right * 0.1f,
            transform.position + transform.up * 0.1f,
            transform.position - transform.up * 0.1f
        };

        bool hitSomething = false;

        foreach (var origin in rayOrigins)
        {
            Debug.DrawRay(origin, -transform.up * 0.2f, Color.red);

            if (Physics.Raycast(origin, -transform.up, out _touch, 0.05f))
            {
                if (_touch.transform.CompareTag("Streakplate"))
                {
                    if (_whiteboard == null)
                    {
                        _whiteboard = _touch.transform.GetComponent<StreakPlate>();
                    }

                    _touchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                    int x = (int)(_touchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                    int y = (int)(_touchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                    if (x < 0 || x >= _whiteboard.textureSize.x || y < 0 || y >= _whiteboard.textureSize.y)
                        continue;

                    if (_touchedLastFrame)
                    {
                        _whiteboard.texture.SetPixels(x, y, _penSize, _penSize, _colors);

                        for (float f = 0.01f; f < 1.00f; f += 0.01f)
                        {
                            int lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                            int lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                            _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colors);
                        }

                        _whiteboard.texture.Apply();
                    }

                    _lastTouchPos = new Vector2(x, y);
                    _lastTouchRot = transform.rotation;
                    _touchedLastFrame = true;
                    hitSomething = true;
                }
            }
        }

        if (!hitSomething)
        {
            _whiteboard = null;
            _touchedLastFrame = false;
        }
    }

    public Color GetStreakColor()
    {
        return streakColor;
    }

    public string GetCorrectBin()
    {
        return correctBin;
    }
}
