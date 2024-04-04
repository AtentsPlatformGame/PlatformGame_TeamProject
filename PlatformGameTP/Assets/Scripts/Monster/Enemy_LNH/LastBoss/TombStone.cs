using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TombStone : MonoBehaviour
{
    [SerializeField] bool isTombStoneGimicOn = false;
    [SerializeField] float patternTime;

    public Transform summonEffect;
    public Transform summonPoint;
    public Transform destroyPoint;

    [Header("�÷��̾� �����̰� �ϴ� �Լ��� ���ε�"), Space(5)]public UnityEvent playerMoveTrueAct;
    [Header("�÷��̾� �������̰� �ϴ� �Լ��� ���ε�"),Space(5)] public UnityEvent playerMoveFalseAct;
    [Header("������ �Ʒ��� ������ ������ �ɸ��� �ϴ� �Լ��� ���ε�"), Space(5)] public UnityEvent gimicClearAck;
    [Header("������ �Ʒ��� ������ ü���� ȸ���ϰ� �ϴ� �Լ��� ���ε�"), Space(5)] public UnityEvent gimicFailAck;

    public LayerMask playerMask;
    public GameObject GKeyPopUp;
    public GameObject GimicScreen;

    BoxCollider myCollider;
    
    float playTime = 0.0f;
    bool clearGimmic = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        myCollider = GetComponent<BoxCollider>();
        StartCoroutine(SummonTombStone());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SummonTombStone() // �� ��ȯ
    {
        Vector3 dir = summonPoint.position - this.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        float delta = Time.deltaTime * 20f;

        while (!Mathf.Approximately(dist, 0.0f))
        {
            if (dist < delta) dist = delta;
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return new WaitForSeconds(0.1f);
        }

        myCollider.enabled = true;
        summonEffect.gameObject.SetActive(true);

        yield return StartCoroutine(TombStonePattern());
    }

    IEnumerator DestroyTombStone() // �� �ı�
    {
        myCollider.enabled = false;
        summonEffect.gameObject.SetActive(false);

        Debug.Log("���� ����");
        Vector3 dir = destroyPoint.position - this.transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        float delta = Time.deltaTime * 20f;
        Debug.Log(dist);

        while (!Mathf.Approximately(dist, 0.0f))
        {
            if (dist < delta) dist = delta;
            dist -= delta;
            Debug.Log(dist);
            transform.Translate(dir * delta, Space.World);
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(this.gameObject);
    }

    IEnumerator TombStonePattern()
    {
        while(playTime < patternTime)
        {
            playTime += Time.deltaTime;
            if (clearGimmic) break;
            yield return null;
        }

        playerMoveTrueAct?.Invoke();
        if(GimicScreen.activeSelf) GimicScreen.gameObject.SetActive(false);

        if (clearGimmic)
        {
            // �ٽ� ������ �Ʒ��� ������ ������Ŵ
            Debug.Log("Ŭ����");
            gimicClearAck?.Invoke();// ������ �Ʒ��� ������ ������ �ɸ��� �ϴ� �ڷ�ƾ�� �����ϴ� �Լ�
        }
        else
        {
            // ������ �Ǹ� ������ ä��
            gimicFailAck?.Invoke(); // ������ �Ʒ��� ������ ü���� ȸ����Ű�� �ڷ�ƾ�� �����ϴ� �Լ�
            Debug.Log("����");
        }

        yield return StartCoroutine(DestroyTombStone());
    }

    private void OnTriggerEnter(Collider other)
    {
       if((1 << other.gameObject.layer & playerMask) != 0)
        {
            Debug.Log("�÷��̾� ����");
            GKeyPopUp.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            Debug.Log("�÷��̾� ����");
            if (!isTombStoneGimicOn)
            {
                Debug.Log("��� �����");
                if (Input.GetKeyDown(KeyCode.G))
                {
                    Debug.Log("��� ����");
                    playerMoveFalseAct?.Invoke();
                    isTombStoneGimicOn = true;
                    GKeyPopUp.gameObject.SetActive(false);
                    // ��� ����
                    GimicScreen.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        GKeyPopUp.gameObject.SetActive(false);
    }

    public void ClearGimic()
    {
        this.clearGimmic = true;
    }
}