using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MurakamiRyujirou.Cube;
using MurakamiRyujirou.Solver;
using System;

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
    public void OnClickButtonM() { cube.Rotate(Operations.M); }
    public void OnClickButtonE() { cube.Rotate(Operations.E); }
    public void OnClickButtonS() { cube.Rotate(Operations.S); }
    public void OnClickButtonX() { cube.Rotate(Operations.x); }
    public void OnClickButtonY() { cube.Rotate(Operations.y); }
    public void OnClickButtonZ() { cube.Rotate(Operations.z); }

    public void OnClickButtonRr() { cube.Rotate(Operations.R_); }
    public void OnClickButtonLr() { cube.Rotate(Operations.L_); }
    public void OnClickButtonUr() { cube.Rotate(Operations.U_); }
    public void OnClickButtonDr() { cube.Rotate(Operations.D_); }
    public void OnClickButtonFr() { cube.Rotate(Operations.F_); }
    public void OnClickButtonBr() { cube.Rotate(Operations.B_); }
    public void OnClickButtonMr() { cube.Rotate(Operations.M_); }
    public void OnClickButtonEr() { cube.Rotate(Operations.E_); }
    public void OnClickButtonSr() { cube.Rotate(Operations.S_); }
    public void OnClickButtonXr() { cube.Rotate(Operations.x_); }
    public void OnClickButtonYr() { cube.Rotate(Operations.y_); }
    public void OnClickButtonZr() { cube.Rotate(Operations.z_); }

    public void OnClickButtonR2() { cube.Rotate(Operations.R2); }
    public void OnClickButtonL2() { cube.Rotate(Operations.L2); }
    public void OnClickButtonU2() { cube.Rotate(Operations.U2); }
    public void OnClickButtonD2() { cube.Rotate(Operations.D2); }
    public void OnClickButtonF2() { cube.Rotate(Operations.F2); }
    public void OnClickButtonB2() { cube.Rotate(Operations.B2); }
    public void OnClickButtonM2() { cube.Rotate(Operations.M2); }
    public void OnClickButtonE2() { cube.Rotate(Operations.E2); }
    public void OnClickButtonS2() { cube.Rotate(Operations.S2); }
    public void OnClickButtonX2() { cube.Rotate(Operations.x2); }
    public void OnClickButtonY2() { cube.Rotate(Operations.y2); }
    public void OnClickButtonZ2() { cube.Rotate(Operations.z2); }

    public void OnClickButtonReset()
    {
        cube.Reset();
    }

    public void OnClickButtonScramble()
    {
        if (cube == null)
        {
            Cube cubeModel = new Cube();
            CubeView cubeView = factory.Create(cubeModel);
            cube = new CubeController(cubeModel, cubeView);
        }
        List<Operations> operList = new();
        int operationsNum = System.Enum.GetValues(typeof(Operations)).Length;
        for (int i = 0; i < 20; i++)
        {
            int rand = UnityEngine.Random.Range(0, operationsNum);
            Operations oper = (Operations)Enum.ToObject(typeof(Operations), rand);
            operList.Add(oper);
        }
        cube.AutoRotate(operList.ToArray());
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

