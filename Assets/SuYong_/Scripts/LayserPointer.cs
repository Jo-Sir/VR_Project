using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayserPointer : MonoBehaviour
{
    private LineRenderer layser;         //���̴� ������
    private RaycastHit collidedObject;   //�浹�� ��ü
    private GameObject currentObject;    //�ֱٿ� �浹�� ��ü�� �����ϱ� ���� ��ü

    public float raycastDistance = 100f; //������ ������ ���� �Ÿ� �ӽ÷� 100 ����

    // Start is called before the first frame update
    void Start()
    {
        //�� ��ũ��Ʈ ���Ե� ��ü�� LineRenderer ������Ʈ �߰�
        layser = this.gameObject.AddComponent<LineRenderer>();

        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 195, 225, 0.5f);
        layser.material = material;

        layser.positionCount = 2;

        //������ ���� �ӽ� ����
        layser.startWidth = 0.01f;
        layser.endWidth = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        layser.SetPosition(0, transform.position); //ù��° ������ ��ġ, ������ �־� �÷��̾� �̵��� ���� �̵�
        Debug.DrawRay(transform.position, transform.forward, Color.green, 0.5f);

        //�浹����
        if (Physics.Raycast(transform.position, transform.forward, out collidedObject, raycastDistance))
        {
            layser.SetPosition(1, collidedObject.point);

            if (collidedObject.collider.gameObject.CompareTag("Button"))
            {
                if (Input.GetKeyDown(KeyCode.Space))    // ��Ʈ�ѷ��� �Է��� Ű�� �����ؾ���
                {
                    collidedObject.collider.gameObject.GetComponent<Button>().onClick.Invoke();
                }
                else
                {
                    collidedObject.collider.gameObject.GetComponent<Button>().OnPointerEnter(null);
                    currentObject = collidedObject.collider.gameObject;
                }
            }
        }

        else
        {
            //�������� ������ ���� ���� �ʱ� ���� ���̸�ŭ ��� ǥ��
            layser.SetPosition(1, transform.position + (transform.forward * raycastDistance));

            if (currentObject != null)
            {
                //�ֱ� ������ ������Ʈ�� button�� ���
                //��ư�� ���� �����ִ� �����̹Ƿ� Ǯ����
                currentObject.GetComponent<Button>().OnPointerExit(null);
                currentObject = null;
            }
        }
    }


    private void LateUpdate()
    {
        //��ư�� ���� ���
        /*if(OVRInput.GetDown(OVRInput.Button.One))
        {
            layser.material.color = new Color(255, 255, 255, 0.5f);
        }

        //��ư�� �� ���
        else if (OVRInput.GetUp(OVRInput.Button.One))
        {
            layser.material.color = new Color(0, 195, 225, 0.5f);
        }*/
    }
}
