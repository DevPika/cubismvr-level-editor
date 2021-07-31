using System;
using System.Collections.Generic;

[Serializable]
public class Puzzle {
    public PuzzleMetaInfo meta;
    public List<PuzzleCell> grid;
    public List<PuzzlePiece> pieces;
}

[Serializable]
public class PuzzleMetaInfo {
    public string name;
    public string id;
    public string difficulty;
    public int puzzleFormatVersion;
    public int angle;
    public string created;
    public string author;

    public PuzzleMetaInfo(string name, string id, string difficulty, int puzzleFormatVersion, int angle, string created, string author) {
        this.name = name;
        this.id = id;
        this.difficulty = difficulty;
        this.puzzleFormatVersion = puzzleFormatVersion;
        this.angle = angle;
        this.created = created;
        this.author = author;
    }
}

[Serializable]
public class PuzzlePiece {
    public string color;
    public List<PuzzleCell> segments;

    public PuzzlePiece(string color, List<PuzzleCell> segments) {
        this.color = color;
        this.segments = segments;
    }
}

[Serializable]
public class PuzzleCell {
    public Int3 localPos;

    public PuzzleCell(Int3 localPos) {
        this.localPos = localPos;
    }
}

[Serializable]
public class Int3 {
    public int x;
    public int y;
    public int z;

    public Int3(int x, int y, int z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}

//TODO enum PuzzleDifficulty
//Difficulty that shows up when hovering over the puzzle in the menu. Possible values: easy, normal, hard, expert, master



