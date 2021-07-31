using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    // TODO var keyword consistency in declarations
    public float cellSize = 1f;
    
    public InputField puzzleNameToLoad;

    private Material currentMat;
    private Puzzle currentPuzzle;
    private PuzzlePiece currentPuzzlePiece;

    public GameObject cursor;

    public Slider colorRSlider;
    public Slider colorGSlider;
    public Slider colorBSlider;

    public InputField colorRInput;
    public InputField colorGInput;
    public InputField colorBInput;
    public InputField colorHexInput;

    public RawImage colorSample;

    public Dropdown difficultyDropdown;
    public Dropdown angleDropdown;
    public InputField authorInput;
    public InputField puzzleNameInput;
    public Button saveModifiedButton;

    public Text logText;

    private void Awake() {
        Application.logMessageReceivedThreaded += OnLogMessageReceived;
    }

    private void Start() {
        StartNewPuzzle();
    }

    #region EditPuzzle
    private void Update() {
        UpdateCursor();
        if (Input.GetMouseButtonDown(0)) {
            if (cursor.activeSelf) {
                currentPuzzlePiece.segments.Add(SpawnCell(cursor.transform.position, Vector3.zero));
            }
        }
    }

    private void UpdateCursor() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit)) {
            Transform objectHit = hit.transform;
            if (objectHit.gameObject.name == "Cell") {
                if (!cursor.activeSelf) {
                    cursor.SetActive(true);
                }
                Vector3 localHitPoint = hit.point - objectHit.position;

                cursor.transform.position = objectHit.position + cellSize * DetectHitFace(localHitPoint);
                return;
            }
        }
        if (cursor.activeSelf) {
            cursor.SetActive(false);
        }
        return;
    }

    private PuzzleCell SpawnCell(Vector3 originInWorldPos, Vector3 relativeOrientation) {
        
        Transform newCell = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        newCell.parent = transform;
        newCell.localScale = Vector3.one * cellSize;
        newCell.position = originInWorldPos + cellSize * relativeOrientation;
        Int3 newLocation = new Int3((int)newCell.position.x, (int)newCell.position.y, (int)newCell.position.z);
        newCell.gameObject.name = "Cell";
        newCell.GetComponent<Renderer>().material = currentMat;

        var newPuzzleCell = new PuzzleCell(newLocation);
        currentPuzzle.grid.Add(newPuzzleCell);
        return newPuzzleCell;
    }

    private Vector3 DetectHitFace(Vector3 localHitPos) {
        localHitPos /= (cellSize*0.5f);
        if (localHitPos.x == 1) {
            return Vector3.right;
        }
        if (localHitPos.x == -1) {
            return Vector3.left;
        }
        if (localHitPos.y == 1) {
            return Vector3.up;
        }
        if (localHitPos.y == -1) {
            return Vector3.down;
        }
        if (localHitPos.z == 1) {
            return Vector3.forward;
        }
        if (localHitPos.z == -1) {
            return Vector3.back;
        }
        Debug.LogError("Error! Incorrect hit position");
        return Vector3.zero;
    }

    public void StartNewPiece() {
        currentMat = new Material(Shader.Find("Standard"));

        Color newColor = UnityEngine.Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f);
        
        currentMat.color = newColor;
        currentPuzzlePiece = new PuzzlePiece(ColorUtility.ToHtmlStringRGB(newColor),
                                             new List<PuzzleCell>());
        currentPuzzle.pieces.Add(currentPuzzlePiece);
        UpdateColorUI(newColor);
    }

    public void StartNewPuzzle() {
        // Clear existing cells
        ClearExistingCells();

        // Start new puzzle and piece
        currentPuzzle = new Puzzle();
        currentPuzzle.grid = new List<PuzzleCell>();
        currentPuzzle.pieces = new List<PuzzlePiece>();

        StartNewPiece();
        //currentPuzzle.pieces[0].segments.Add(SpawnCell(Vector3.left, Vector3.right));
        currentPuzzlePiece.segments.Add(SpawnCell(Vector3.zero, Vector3.zero));

        DateTime dt = DateTime.Now;
        string createdTime = dt.ToString("yyyy-MM-dd hh:mm tt");
        int defaultAngle = 0;
        string defaultDifficulty = "easy";
        currentPuzzle.meta = new PuzzleMetaInfo("Untitled", GenerateUniqueID(), defaultDifficulty, 1, defaultAngle, createdTime, "default_author");
    }

    public void ClearExistingCells() {
        foreach (Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
        currentMat = null;
        currentPuzzle = null;
        currentPuzzlePiece = null;
    }
    #endregion

    #region ColorPickerUI
    public void ColorRSlider() {
        int valueR = (int)colorRSlider.value;
        colorRInput.text = valueR.ToString();
        Color newColor = new Color((float)valueR/255, currentMat.color.g, currentMat.color.b);
        UpdateColor(newColor);
    }
    public void ColorGSlider() {
        int valueG = (int)colorGSlider.value;
        colorGInput.text = valueG.ToString();
        Color newColor = new Color(currentMat.color.r, (float)valueG / 255, currentMat.color.b);
        UpdateColor(newColor);
    }
    public void ColorBSlider() {
        int valueB = (int)colorBSlider.value;
        colorBInput.text = valueB.ToString();
        Color newColor = new Color(currentMat.color.r, currentMat.color.g, (float)valueB / 255);
        UpdateColor(newColor);
    }

    public void ColorRInput() {
        int valueR = int.Parse(colorRInput.text);
        colorRSlider.value = valueR;
        Color newColor = new Color((float)valueR / 255, currentMat.color.g, currentMat.color.b);
        UpdateColor(newColor);
    }
    public void ColorGInput() {
        int valueG = int.Parse(colorGInput.text);
        colorGSlider.value = valueG;
        Color newColor = new Color(currentMat.color.r, (float)valueG / 255, currentMat.color.b);
        UpdateColor(newColor);
    }
    public void ColorBInput() {
        int valueB = int.Parse(colorBInput.text);
        colorBSlider.value = valueB;
        Color newColor = new Color(currentMat.color.r, currentMat.color.g, (float)valueB / 255);
        UpdateColor(newColor);
    }

    public void ColorHexInput() {
        Color newColor;
        ColorUtility.TryParseHtmlString(colorHexInput.text, out newColor);

        colorSample.color = newColor;
        int red = (int)(newColor.r * 256);
        int green = (int)(newColor.g * 256);
        int blue = (int)(newColor.b * 256);

        colorRSlider.value = red;
        colorGSlider.value = green;
        colorBSlider.value = blue;

        colorRInput.text = red.ToString();
        colorGInput.text = green.ToString();
        colorBInput.text = blue.ToString();

        currentPuzzlePiece.color = colorHexInput.text;
        currentMat.color = newColor;
    }

    public void UpdateColor(Color newColor) {
        currentMat.color = newColor;
        colorSample.color = newColor;
        colorHexInput.text = ColorUtility.ToHtmlStringRGB(newColor);
        currentPuzzlePiece.color = ColorUtility.ToHtmlStringRGB(newColor);
    }

    public void UpdateColorUI(Color newColor) {
        colorSample.color = newColor;

        colorHexInput.text = ColorUtility.ToHtmlStringRGB(newColor);

        int red = (int)(newColor.r * 256);
        int green = (int)(newColor.g * 256);
        int blue = (int)(newColor.b * 256);

        colorRSlider.value = red;
        colorGSlider.value = green;
        colorBSlider.value = blue;

        colorRInput.text = red.ToString();
        colorGInput.text = green.ToString();
        colorBInput.text = blue.ToString();
    }
    #endregion


    #region OtherUI

    public void ClearButton() {
        ClearExistingCells();
        StartNewPuzzle();
    }

    public void StartNewPieceButton() {
        StartNewPiece();
    }

    public void LoadFromFileButton() {
        ClearExistingCells();

        string fileName = puzzleNameToLoad.text;

        currentPuzzle = PuzzleUtilities.LoadPuzzleFromFile(fileName);
        
        if (currentPuzzle == null) {
            Debug.LogError("Error! Unable to load puzzle " + fileName);
        } else {
            LoadCurrentPuzzle();
            UpdateUIAfterLoading();
        }
    }

    private void LoadCurrentPuzzle() {
        foreach (var piece in currentPuzzle.pieces) {
            currentMat = new Material(Shader.Find("Standard"));
            Color newColor;
            if (!ColorUtility.TryParseHtmlString("#" + piece.color, out newColor)) {
                Debug.LogError("Color not parsed");
            }
            currentMat.color = newColor;
            foreach (var cell in piece.segments) {
                SpawnCell(Vector3.zero, new Vector3(cell.localPos.x, cell.localPos.y, cell.localPos.z));
            }
        }
        StartNewPiece();
        Debug.Log("Done loading");
    }

    private void UpdateUIAfterLoading() {
        // Change the save file input text for easy saving modified puzzle over last used file
        puzzleNameInput.text = puzzleNameToLoad.text;

        for (int i = 0; i < difficultyDropdown.options.Count; i++) {
            if (difficultyDropdown.options[i].text == currentPuzzle.meta.difficulty) {
                difficultyDropdown.value = i;
                difficultyDropdown.RefreshShownValue();
            }
        }

        for (int i = 0; i < angleDropdown.options.Count; i++) {
            if (angleDropdown.options[i].text == currentPuzzle.meta.difficulty) {
                angleDropdown.value = i;
                angleDropdown.RefreshShownValue();
            }
        }

        authorInput.text = currentPuzzle.meta.author;
        saveModifiedButton.GetComponentInChildren<Text>().text = "Save modified puzzle to last file ( " + puzzleNameInput.text + " )";
    }

    public void SetDifficultyDropdown() {
        currentPuzzle.meta.difficulty = difficultyDropdown.options[difficultyDropdown.value].text;
    }

    public void SetAngleDropdown() {
        currentPuzzle.meta.angle = int.Parse(angleDropdown.options[angleDropdown.value].text);
    }

    public void SetAuthorInput() {
        currentPuzzle.meta.author = authorInput.text;
    }

    public void SetPuzzleNameInput() {
        currentPuzzle.meta.name = puzzleNameInput.text;
    }

    public void SaveToFileButton() {
        // TODO difficulty level enum
        
        string fileName;
        if (puzzleNameInput.text == "") {
            fileName = "Untitled";
        } else {
            fileName = puzzleNameInput.text;
            currentPuzzle.meta.name = puzzleNameInput.text;
        }

        //PuzzleUtilities.ForceSavePuzzleToFile(currentPuzzle, fileName);
        PuzzleUtilities.SavePuzzleToFile(currentPuzzle, fileName);
    }

    public void SaveModifiedPuzzleButton() {
        string fileName = puzzleNameInput.text;
        PuzzleUtilities.SaveModifiedPuzzleToFile(currentPuzzle.grid, currentPuzzle.pieces, fileName);
    }

    private string GenerateUniqueID() {
        // 16 alpha numeric like "08H8SVX1P3YDD7GL"
        // https://answers.unity.com/questions/965798/generate-a-random-string-from-a-specified-length.html
        string id = "";
        const string glyphs = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; //add the characters you want

        for (int i = 0; i < 16; i++) {
            id += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];
        }
        return id;
    }

    // adapted from https://gist.github.com/der-hugo/d511de7ba3164185f2f97b08428d31d3
    // scroll ui made using https://www.youtube.com/watch?v=ArH0S2Cdptk
    private void OnLogMessageReceived(string logString, string stackTrace, LogType type) {
        var time = DateTime.Now;
        // e.g. 04:02:01.023
        var timeStamp = $"{time.Hour:D2}:{time.Minute:D2}:{time.Second:D2}.{time.Millisecond:D3}";
        var fullMessage = $"\n\n## {timeStamp} {type}\n{logString}";

        logText.text += fullMessage;
    }
    #endregion
}

