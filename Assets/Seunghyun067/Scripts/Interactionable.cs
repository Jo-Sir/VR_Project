using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH
{
    public class Interactionable : MonoBehaviour, IInteraction
    {
        [SerializeField] private GameObject canvers;
        [SerializeField] private Renderer[] myRenderers;

        [SerializeField, Range(0, 0.01f)] private float outlineWidth;

        private Vector3 preVector3;
        public Vector3 PreVector3 { get { return preVector3; } }
        protected void Awake()
        {
            preVector3 = this.transform.position;

            if (myRenderers.Length == 0)
                myRenderers = GetComponentsInChildren<Renderer>();


            foreach (var render in myRenderers)
                render.materials[1].SetFloat("_Outline", 0f);
        }

        public virtual void HoverOn()
        {
            foreach (var render in myRenderers)
                render.materials[1].SetFloat("_Outline", outlineWidth);
        }
        public void HoverOff()
        {
            foreach (var render in myRenderers)
                render.materials[1].SetFloat("_Outline", 0f);
        }
        public virtual void Interaction()
        {
            this.gameObject.layer = 6;// ���̾� ��ȣ Ȯ�� �� �� ����
            canvers.SetActive(true);
            // ������Ʈ ��ġ ȭ�� �߾�����
            this.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0.5f);
            // �̻����϶� �÷��̾� ��ǲ �ٲ��ֱ�
        }

        public virtual void UnInteraction()
        {
            this.gameObject.layer = 0;// 0�� Default
            canvers.SetActive(false);
            this.transform.position = PreVector3;
        }

        
    }

}
