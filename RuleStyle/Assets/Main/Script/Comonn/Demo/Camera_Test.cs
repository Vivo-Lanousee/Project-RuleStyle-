using UnityEngine;

public class Camera_Test : MonoBehaviour
{
    //�J�����̈ʒu
    public Transform Camera;

    /// <summary>
    /// ���S
    /// </summary>
    public Vector3 Cont = new Vector3(0, 0, 0);

    /// <summary>
    /// �ύX���鐔�l
    /// </summary>
    public int ChangeMeter=0;

    public int Dist=10;

    private void Update()
    {
        //����l�ɍs���ΐ��l��߂�
        if (ChangeMeter >=361)
        {
            ChangeMeter = 0;
        }
        if (ChangeMeter < 0)
        {
            ChangeMeter = 360;
        }
        //���l��ύX
        if (Input.GetMouseButton(2))
        {
            ChangeMeter++;
        }
        if (Input.GetMouseButton(1))
        {
            ChangeMeter--;
        }

        //���W�A���ϊ�
        float test=ChangeMeter * Mathf.Deg2Rad;
        //�ړ�����ׂ��n�_���Z�o
        float b = Mathf.Cos(test);
        float a=Mathf.Sin(test);
        //�n�_�쐬
        Vector3 t = new Vector3(b*Dist, 10, a*Dist);
        //�J�����̈ʒu��ύX
        Camera.transform.position = t;
        
        Vector3 direction=Cont - t;
        Debug.Log(direction);
        // �J�����̉�]���v�Z
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // X ���̉�]�������I�ɐݒ�
        Vector3 eulerRotation = targetRotation.eulerAngles;
        eulerRotation.x = 30f; // �C�ӂ̊p�x�ɌŒ�A�Ⴆ��30�x
        targetRotation = Quaternion.Euler(eulerRotation);

        // ��]��K�p
        Camera.rotation = targetRotation;

    }
}
