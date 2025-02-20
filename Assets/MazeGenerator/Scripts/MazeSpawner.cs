using UnityEngine;
using System.Collections;

public class MazeSpawner : MonoBehaviour {
    public enum MazeGenerationAlgorithm {
        PureRecursive,
        RecursiveTree,
        RandomTree,
        OldestTree,
        RecursiveDivision,
    }

    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
    public bool FullRandom = false;
    public int RandomSeed = 12345;
    public GameObject[] FloorPrefabs;
    public GameObject[] WallPrefabs;
    public GameObject[] PillarPrefabs;
    public int Rows = 5;
    public int Columns = 5;
    public float CellWidth = 2;
    public float CellHeight = 2;
    public bool AddGaps = true;
    public GameObject GoalPrefab = null;

    private BasicMazeGenerator mMazeGenerator = null;

    void Start() {
        if (!FullRandom) {
            Random.InitState(RandomSeed);
        }

        switch (Algorithm) {
            case MazeGenerationAlgorithm.PureRecursive:
                mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveTree:
                mMazeGenerator = new RecursiveTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RandomTree:
                mMazeGenerator = new RandomTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.OldestTree:
                mMazeGenerator = new OldestTreeMazeGenerator(Rows, Columns);
                break;
            case MazeGenerationAlgorithm.RecursiveDivision:
                mMazeGenerator = new DivisionMazeGenerator(Rows, Columns);
                break;
        }

        mMazeGenerator.GenerateMaze();

        for (int row = 0; row < Rows; row++) {
            for (int column = 0; column < Columns; column++) {
                float x = column * (CellWidth + (AddGaps ? 0.2f : 0));
                float z = row * (CellHeight + (AddGaps ? 0.2f : 0));
                MazeCell cell = mMazeGenerator.GetMazeCell(row, column);
                GameObject tmp;

                tmp = Instantiate(GetRandomPrefab(FloorPrefabs), new Vector3(x, 0, z), Quaternion.identity);
                tmp.transform.parent = transform;

                if (cell.WallRight) {
                    tmp = Instantiate(GetRandomPrefab(WallPrefabs), new Vector3(x + CellWidth / 2, 0, z), Quaternion.Euler(0, 90, 0));
                    tmp.transform.parent = transform;
                }
                if (cell.WallFront) {
                    tmp = Instantiate(GetRandomPrefab(WallPrefabs), new Vector3(x, 0, z + CellHeight / 2), Quaternion.identity);
                    tmp.transform.parent = transform;
                }

                if (cell.IsGoal && GoalPrefab != null) {
                    tmp = Instantiate(GoalPrefab, new Vector3(x, 1, z), Quaternion.identity);
                    tmp.transform.parent = transform;
                }
            }
        }

        if (PillarPrefabs.Length > 0) {
            for (int row = 0; row < Rows + 1; row++) {
                for (int column = 0; column < Columns + 1; column++) {
                    float x = column * (CellWidth + (AddGaps ? 0.2f : 0)) - CellWidth / 2;
                    float z = row * (CellHeight + (AddGaps ? 0.2f : 0)) - CellHeight / 2;
                    GameObject tmp = Instantiate(GetRandomPrefab(PillarPrefabs), new Vector3(x, 0, z), Quaternion.identity);
                    tmp.transform.parent = transform;
                }
            }
        }
    }

    private GameObject GetRandomPrefab(GameObject[] prefabs) {
        if (prefabs == null || prefabs.Length == 0) return null;
        return prefabs[Random.Range(0, prefabs.Length)];
    }
}