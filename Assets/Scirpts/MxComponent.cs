using UnityEngine;
using ActUtlType64Lib;
using System;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class MxComponent : MonoBehaviour
{
    [SerializeField] TMP_Text logTxt;
    ActUtlType64 mxComponent;
    bool isConnected;
    bool isPoewerOnCliked;
    bool isStopCliked;
    bool isEStopCliked;

    const string X_START_UNITY2PLC = "X0";  // Unity�� ��ư ������ PLC�� ������ ���� X����̽� ����Ʈ �ּ�
    const string X_START_PLC2UNITY = "X10"; // PLC�� ��ư ������ Unity�� ������ ���� X����̽� ����Ʈ �ּ�
    const string Y_START_PLC2UNITY = "Y0"; // Unity�� ��ư ������ PLC�� ������ ���� Y����̽� ����Ʈ �ּ�
    const int X_BLOCKCNT_UNITY2PLC = 1;     // Unity�� ��ư ������ PLC�� ������ X����̽� ��� ����
    const int X_BLOCKCNT_PLC2UNITY = 1;     // PLC�� ��ư ������ Unity�� ������ X����̽� ��� ����
    const int Y_BLOCKCNT_PLC2UNITY = 1;     // PLC�� ��ư ������ Unity�� ������ Y����̽� ��� ����
    
    // Y����̽��� �޴� ����� ����
    public List<Cylinder> cylinders;
    public Conveyor conveyor;
    public TowerManager towerManager;
    public List<Sensor> sensors;
    WaitForSeconds updateInterval = new WaitForSeconds(0.5f);

    private void Awake()
    {
        mxComponent = new ActUtlType64();

        mxComponent.ActLogicalStationNumber = 1;

        logTxt.text = "Please connect the PLC..";
        logTxt.color = Color.red;
    }

    // Ư�� �ð��� �ѹ��� �ݺ��Ͽ� PLC �����͸� �о�´�.
    IEnumerator UpdatePLCData()
    {
        while (isConnected)
        {
            ReadDeviceBlock(X_START_PLC2UNITY, X_BLOCKCNT_PLC2UNITY); // X0 ���� 2���
            ReadDeviceBlock(Y_START_PLC2UNITY, Y_BLOCKCNT_PLC2UNITY);  // Y0 ���� 1���

            // X0���� 1��� �߿��� ��ư��(����, ����, �������) ������ PLC�� �ݿ�
            WriteDeviceBlock(X_START_UNITY2PLC, X_BLOCKCNT_UNITY2PLC); 

            yield return updateInterval;
        }
    }

    private void WriteDeviceBlock(string inputStartDevice, int inputBlockCnt)
    {
        // ����, ����, ������� ������ ù��° ��Ͽ� ���� (010 -> ������ 2)
        char power = (isPoewerOnCliked == true ? '1' : '0');
        char stop = (isStopCliked == true ? '1' : '0');
        char eStop = (isEStopCliked == true ? '1' : '0');
        string binaryStr = $"{eStop}{stop}{power}"; // "010"
        int decimalX = Convert.ToInt32(binaryStr, 2); // 10 -> 
        print(decimalX);

        int[] data = new int[inputBlockCnt];
        data[0] = decimalX;
        int iRet = mxComponent.WriteDeviceBlock(inputStartDevice, inputBlockCnt, ref data[0]);

        if(iRet == 0)
        {
            print("Unity -> PLC:" + decimalX);
        }
        else
        {
            ShowError(iRet);
        }
    }

    
    private void OnDestroy()
    {
        if (isConnected)
            Close();
    }

    public void Open()
    {
        int iRet = mxComponent.Open();

        if (iRet == 0)
        {
            isConnected = true;

            StartCoroutine(UpdatePLCData()); // ������ ��� �ҷ�����

            logTxt.text = "PLC is connected!";
            logTxt.color = Color.green;
            print("�� ������ �Ǿ����ϴ�.");
        }
        else
        {
            // �����ڵ� ��ȯ(16����)
            ShowError(iRet);
            print(Convert.ToString(iRet, 16));
        }
    }

    private void ShowError(int iRet)
    {
        logTxt.text = "Error: 0x " + Convert.ToString(iRet, 16);
        logTxt.color = Color.red;
    }

    public void Close()
    {
        if (!isConnected)
        {
            logTxt.text = "PLC is already disconnected.";
            logTxt.color = Color.red;
            print("�̹� �������� �����Դϴ�.");

            return;
        }

        int iRet = mxComponent.Close();

        if (iRet == 0)
        {
            isConnected = false;

            logTxt.text = "PLC is disconnected completely.";
            logTxt.color = Color.red;
            print("�� ������ �Ǿ����ϴ�.");
        }
        else
        {
            // �����ڵ� ��ȯ(16����)
            ShowError(iRet);
            print(Convert.ToString(iRet, 16));
        }
    }

    /*
    X����̽� ����(13��) -> X0 ���� 2�� ��� ���
    ������ư 1��(X0)
    ������ư 1��
    ���������ư 1��
    ���� LS 2��(X10)
    ���� LS 2��
    ���� LS 2��
    ���� LS 2��
    �������� 1��
    �ݼӼ��� 1��

    Y����̽� ����(13��) -> Y0 ���� 1�� ��� ���
    ���� Syl ����/���� 2��
    ���� Syl ����/���� 2��
    ���� Syl ����/���� 2��
    ���� Syl ����/���� 2��
    �����̾� CW/CCW 2��
    ���� 3��
    */

    public void ReadDeviceBlock(string startDevice, int blockCnt)
    {
        // { 336, 55 } -> 0001/1100/1110/0000
        int[] data = new int[blockCnt];
        int iRet = mxComponent.ReadDeviceBlock(startDevice, blockCnt, out data[0]);

        if (iRet == 0)
        {
            // { 0001110011100000, 0001110011100000 }
            string[] result = ConvertDecimalToBinary(data); // 336 -> 0001/1100/1110/0000


            // ���� ���� data�� ����
            // cylinders[0].isForward = data[0]
            // 1. Input X Device ���� ���
            if (startDevice.Contains("X"))
            {
                // LS, ������ ���� ���¸� �������ִ� �κ�
                // string fisrtX = result[0];
                string secondX = result[0];  // X10���� 1�� ���

                cylinders[0].isFrontLimitSWON = secondX[0] is '1' ? true : false;
                cylinders[0].isBackLimitSWON = secondX[1] is '1' ? true : false;
                cylinders[1].isFrontLimitSWON = secondX[2] is '1' ? true : false;
                cylinders[1].isBackLimitSWON = secondX[3] is '1' ? true : false;
                cylinders[2].isFrontLimitSWON = secondX[4] is '1' ? true : false;
                cylinders[2].isBackLimitSWON = secondX[5] is '1' ? true : false;
                cylinders[3].isFrontLimitSWON = secondX[6] is '1' ? true : false;
                cylinders[3].isBackLimitSWON = secondX[7] is '1' ? true : false;
                sensors[0].isActive = secondX[8] is '1' ? true : false;
                sensors[1].isActive = secondX[9] is '1' ? true : false;

                print("PLC -> Unity(X decice):" + secondX);
            }
            // 2. output Y Device ���� ���: 1�� ��ϸ� ���
            else if (startDevice.Contains("Y"))
            {
                string y = result[0]; // 001110011100000
                bool isActive = y[0] is '1' ? true : false;
                cylinders[0].isForward = isActive;
                cylinders[0].isBackward = y[1] is '1' ? true : false;
                cylinders[1].isForward = y[2] is '1' ? true : false;
                cylinders[1].isBackward = y[3] is '1' ? true : false;
                cylinders[2].isForward = y[4] is '1' ? true : false;
                cylinders[2].isBackward = y[5] is '1' ? true : false;
                cylinders[3].isForward = y[6] is '1' ? true : false;
                cylinders[3].isBackward = y[7] is '1' ? true : false;
                conveyor.isCW = y[8] is '1' ? true : false;
                conveyor.isCCW = y[9] is '1' ? true : false;
                towerManager.isRedLampOn = y[10] is '1' ? true : false;
                towerManager.isYellowLampOn = y[11] is '1' ? true : false;
                towerManager.isGreenLampOn = y[12] is '1' ? true : false;

                print("PLC -> Unity(Y device)" + y);
            }

        }
        else
        {
            ShowError(iRet);
        }
    }

    // { 336, 55 } -> { 0001110011100000, 0001110011100000 }
    private string[] ConvertDecimalToBinary(int[] data)
    {
        string[] result = new string[data.Length];

        for (int i = 0; i < data.Length; i++)
        {
            // 1. 10���� 336 -> 2���� 101010000
            string binary = Convert.ToString(data[i], 2);

            // 2. ���ư� ������Ʈ �߰� 1/0101/0000 -> 0000/0010/1010/0000
            int upBitCnt = 16 - binary.Length;

            // 3. ������(������Ʈ �ε��� ���� ���) 1/0101/0000 -> 0000/1010/1
            string reversedBinary = new string(binary.Reverse().ToArray());

            // 4. ������Ʈ ���̱� 0000/1010/1 + 000/0000 = 0000/1010/1000/0000
            for (int j = 0; j < upBitCnt; j++)
            {
                reversedBinary += "0";
            }

            result[i] = reversedBinary;
        }

        return result;
    }

    public void OnPowerBtnClkEvent()
    {
        isPoewerOnCliked = true;
        isEStopCliked = false;
    }
    public void OnStopBtnClkEvent()
    {
        isStopCliked = true;
        isPoewerOnCliked = false;
    }
    public void OnEStopBtnClkEvent()
    {
        isEStopCliked = !isEStopCliked;
    }
}