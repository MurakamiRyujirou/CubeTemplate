using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MurakamiRyujirou.Cube;
using MurakamiRyujirou.Solver;

public class SampleSceneManager : MonoBehaviour
{
    public GameObject cubeViewFactoryPrefab;

    private CubeViewFactory factory;
    private CubeController cube;

    void Start()
    {
        factory = Instantiate(cubeViewFactoryPrefab).GetComponent<CubeViewFactory>();
        Cube cubeModel = new Cube();
        CubeView cubeView = factory.Create(cubeModel);
        cube = new CubeController(cubeModel, cubeView);
        cube.SetRotateSpeed(10f);
    }

    void Update()
    {
        cube.OnUpdate();
    }

    public void OnClickButtonR() { cube.Rotate(Operations.R); }
    public void OnClickButtonL() { cube.Rotate(Operations.L); }
    public void OnClickButtonU() { cube.Rotate(Operations.U); }
    public void OnClickButtonD() { cube.Rotate(Operations.D); }
    public void OnClickButtonF() { cube.Rotate(Operations.F); }
    public void OnClickButtonB() { cube.Rotate(Operations.B); }

    public void OnClickButtonReset()
    {
        cube.Reset();
    }

    public void OnClickButtonScramble()
    {
    }

    public void OnClickButtonSolve()
    {
        CubeModel cubeModel = CubeConverter.Convert(cube.Cube);
        CubeSolver solver = new();
        string[] solutions = solver.Solution(cubeModel);
        Operations[] operations = OperationConverter.Convert(solutions);
        cube.AutoRotate(operations);
    }

}

