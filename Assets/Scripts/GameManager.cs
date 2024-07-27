using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Random rand = new Random();
    public const int gridSize = 100;
    public GameObject life;
    public GameObject[,] planeGrid;
    public int[,] cellStates = new int[gridSize, gridSize];
    public GameObject Pixels;
    public float getPixels = 0f;
    public Protect Protect_Script;
    public Image GetPixels_bar;
    public float currentFillAmount;
    public float needPixels = 5.0f;
    void Start() {
        for (int i = 0; i < 200; i++) {
            SpawnObject();
        }

        for (int i = 0; i < gridSize; i++) {
            for (int j = 0; j < gridSize; j++) {
                cellStates[i, j] = rand.Next(2);
            }
        }

        // ��� �׸��� ����
        planeGrid = new GameObject[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                if (cellStates[y, x] == 1) {
                    GameObject newLife = Instantiate(life, new Vector3(x, 0, y), Quaternion.identity);
                    Life Life_Script = newLife.GetComponent<Life>();
                    Life_Script.x = x;
                    Life_Script.y = y;
                    planeGrid[x, y] = newLife;
                }
            }
        }
        InvokeRepeating("ShowDisplay", 1f, 0.5f);
    }

    private void Update() {
        float targetFill = getPixels / needPixels * (1 - 0.163f) + 0.163f;
        if (currentFillAmount < targetFill) {
            currentFillAmount += 1 * Time.deltaTime;
        currentFillAmount = Mathf.Clamp(currentFillAmount, 0.163f, 1.0f);
        GetPixels_bar.fillAmount = currentFillAmount;
        }
        if (getPixels >= needPixels && currentFillAmount >= 1) {
            getPixels = 0;
            Protect_Script.check_protect = 1;
            currentFillAmount = 0.163f;
            GetPixels_bar.fillAmount = currentFillAmount;
        }
    }

    public void SpawnObject() {
        float randomX = (float)rand.NextDouble() * (98 - 2) + 2;
        float randomZ = (float)rand.NextDouble() * (98 - 2) + 2;
        Vector3 randomPosition = new Vector3(randomX, transform.position.y, randomZ);
        Instantiate(Pixels, randomPosition, Quaternion.identity);
    }

    void ShowDisplay() {
        //�������� ���� ����
        for (int i = 0; i < gridSize * gridSize / 28; i++)
            cellStates[rand.Next(gridSize), rand.Next(gridSize)] = 1;
        // ���� ���� ���
        int[,] nextCellStates = new int[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                int neighbors = GetLiveNeighborCount(x, y);
                if (cellStates[y, x] == 1) // ����ִ� ��
                {
                    if (neighbors < 2 || neighbors > 3)
                        nextCellStates[y, x] = 0; // ����
                    else
                        nextCellStates[y, x] = 1; // ����
                } else // ���� ��
                  {
                    if (neighbors == 3)
                        nextCellStates[y, x] = 1; // ź��
                    else
                        nextCellStates[y, x] = 0; // ����
                }
            }
        }
        // �� ���� ������Ʈ
        for (int x = 0; x < gridSize; x++) {
            for (int y = 0; y < gridSize; y++) {
                if (cellStates[y, x] != nextCellStates[y, x]) {
                    if (nextCellStates[y, x] == 1) {
                        // ���ο� �� ����
                        GameObject newLife = Instantiate(life, new Vector3(x, 0, y), Quaternion.identity);
                        Life Life_Script = newLife.GetComponent<Life>();
                        Life_Script.x = x;
                        Life_Script.y = y;
                        planeGrid[x, y] = newLife;
                    } else {
                        // �� ����
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
