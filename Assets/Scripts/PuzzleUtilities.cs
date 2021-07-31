using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PuzzleUtilities {
    public static Puzzle LoadPuzzleFromFile(string fileName) {
        Puzzle puzzle = null;
        string streamingAssetsPath = Application.streamingAssetsPath;
        string path = Path.Combine(streamingAssetsPath, fileName + ".cube");

        if (!File.Exists(path)) {
            Debug.LogError(path + " doesn't exist.");
            return null;
        }

        using (StreamReader sr = new StreamReader(path)) {
            string s;
            s = sr.ReadToEnd();
            puzzle = LoadPuzzleFromJSON(s);
        }

        return puzzle;
    }

    public static Puzzle LoadPuzzleFromJSON(string jsonString) {

        // TODO Validity checks
        return JsonUtility.FromJson<Puzzle>(jsonString);
    }

    public static bool SavePuzzleToFile(Puzzle puzzle, string fileName) {
        // TODO CheckIfValidPuzzle(puzzle);
        string streamingAssetsPath = Application.streamingAssetsPath;
        string path = Path.Combine(streamingAssetsPath, fileName + ".cube");
        if (File.Exists(path)) {
            Debug.LogError(path + " already exists. Saving with date time suffix...");
            DateTime dt = DateTime.Now;
            string datetime = dt.ToString("yyyy-MM-dd hh-mm-ss");
            //path = path.Split('.')[0] + datetime + ".cube";
            path = Path.Combine(streamingAssetsPath, fileName + datetime + ".cube");
            using (StreamWriter sw = File.CreateText(path)) {
                sw.Write(JsonUtility.ToJson(puzzle, true));
            }
            Debug.Log(path + " saved successfully!");
            return false; // TODO return bool required?
        }
        else {
            //TODO check if file written successfully
            using (StreamWriter sw = File.CreateText(path)) {
                sw.Write(JsonUtility.ToJson(puzzle, true));
            }
            Debug.Log(path + " saved successfully!");
            return true;
        }
    }

    public static void ForceSavePuzzleToFile(Puzzle puzzle, string fileName) {
        string streamingAssetsPath = Application.streamingAssetsPath;
        string path = Path.Combine(streamingAssetsPath, fileName + ".cube");
        using (StreamWriter sw = File.CreateText(path)) {
            sw.Write(JsonUtility.ToJson(puzzle, true));
        }
    }

    public static bool SaveModifiedPuzzleToFile(List<PuzzleCell> modGrid, List<PuzzlePiece> modPieces, string fileName) {
        string streamingAssetsPath = Application.streamingAssetsPath;
        string path = Path.Combine(streamingAssetsPath, fileName + ".cube");
        Puzzle puzzle = null;
        if (File.Exists(path)) {
            puzzle = LoadPuzzleFromFile(path);
            puzzle.grid = modGrid;
            puzzle.pieces = modPieces;
            ForceSavePuzzleToFile(puzzle, path);
            return true;
        }

        return false;
    }

    public static string SerializeSamplePuzzleToJSON() {
        var piece1 = new PuzzlePiece("E39CAC", new List<PuzzleCell> { new PuzzleCell(new Int3(0, 0, 0)),
                                                                     new PuzzleCell(new Int3(1, 0, 0)),
                                                                     new PuzzleCell(new Int3(0, 1, 0))
                                                                   });
        var piece2 = new PuzzlePiece("ACF495", new List<PuzzleCell> { new PuzzleCell(new Int3(1, 1, 0)),
                                                                     new PuzzleCell(new Int3(1, 2, 0)),
                                                                     new PuzzleCell(new Int3(0, 2, 0))
                                                                   });

        var puzzleInfo = new PuzzleMetaInfo("Example", "08H8SVX1P3YDD7GL", "easy", 1, 0, "2020-12-12 10:46 AM", "Vanbo");


        var samplePuzzle = new Puzzle();
        samplePuzzle.meta = puzzleInfo;
        samplePuzzle.grid = new List<PuzzleCell> { new PuzzleCell(new Int3(0, 0, 0)),
                                             new PuzzleCell(new Int3(0, 1, 0)),
                                             new PuzzleCell(new Int3(0, 2, 0)),
                                             new PuzzleCell(new Int3(1, 0, 0)),
                                             new PuzzleCell(new Int3(1, 1, 0)),
                                             new PuzzleCell(new Int3(1, 2, 0))
        };
        samplePuzzle.pieces = new List<PuzzlePiece> { piece1, piece2 };
        // https://www.patrykgalach.com/2019/04/08/data-serialization-in-unity-json-friendly/
        string jsonString = JsonUtility.ToJson(samplePuzzle, true);
        Debug.Log(jsonString);
        return jsonString;
    }
}
