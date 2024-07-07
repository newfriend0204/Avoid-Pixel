using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using Random = System.Random;

public class LifeGame : MonoBehaviour {
    public const int gridSize = 100;
    public GameObject planePrefab;
    private GameObject[,] planeGrid;
    private int[,] cellStates = new int[gridSize, gridSize];
    Random rand = new Random();
    void Start() {
        for (int i = 0; i < gridSize; i++) {
            for (int j = 0; j < gridSize; j++) {
                cellStates[i, j] = rand.Next(2);
            }
        }

        // 평면 그리드 생성
        planeGrid = new GameObject[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                if (cellStates[y, x] == 1) {
                    planeGrid[x, y] = Instantiate(planePrefab, new Vector3(x, 0, y), Quaternion.identity);
                }
            }
        }
        InvokeRepeating("ShowDisplay", 1f, 0.3f);
    }

    void ShowDisplay() {
        //랜덤으로 세포 생성
        for (int i = 0; i < gridSize * gridSize / 30; i++)
            cellStates[rand.Next(gridSize), rand.Next(gridSize)] = 1;
        // 다음 세대 계산
        int[,] nextCellStates = new int[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                int neighbors = GetLiveNeighborCount(x, y);
                if (cellStates[y, x] == 1) // 살아있는 셀
                {
                    if (neighbors < 2 || neighbors > 3)
                        nextCellStates[y, x] = 0; // 죽음
                    else
                        nextCellStates[y, x] = 1; // 생존
                } else // 죽은 셀
                  {
                    if (neighbors == 3)
                        nextCellStates[y, x] = 1; // 탄생
                    else
                        nextCellStates[y, x] = 0; // 죽음
                }
            }
        }
        // 셀 상태 업데이트
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                if (cellStates[y, x] != nextCellStates[y, x]) {
                    if (nextCellStates[y, x] == 1) {
                        // 새로운 셀 생성
                        planeGrid[x, y] = Instantiate(planePrefab, new Vector3(x, 0, y), Quaternion.identity);
                    } else {
                        // 셀 삭제
                        Destroy(planeGrid[x, y]);
                        planeGrid[x, y] = null;
                    }
                    cellStates[y, x] = nextCellStates[y, x];
                }
            }
        }
    }

    int GetLiveNeighborCount(int x, int y) {
        int count = 0;
        for (int i = x - 1; i <= x + 1; i++) {
            for (int j = y - 1; j <= y + 1; j++) {
                if (i >= 0 && i < gridSize && j >= 0 && j < gridSize && (i != x || j != y) && cellStates[j, i] == 1) {
                    count++;
                }
            }
        }
        return count;
    }
}
