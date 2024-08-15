using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class �~�H��� : MonoBehaviour
{
    NavMeshAgent �ɯ�;
    public Transform �ؼ�;
    GameObject[] ���~�I;

    public int hp = 30;

    public float �g�{�Z�� = 2f;
    public Transform �̪�ĤH;
    Animator �ʵe;

    public float �}�����Z = 1.6f;
    float fireTime;

    Text ��q;
    GameObject ���;
    int OriHP;

    public Rig rig;

    // Start is called before the first frame update
    void Start()
    {
        ��q = transform.Find("Canvas/��q").gameObject.GetComponent<Text>();
        ��q.text = hp.ToString();
        ��� = transform.Find("Canvas/���").gameObject;
        OriHP = hp;
        rig.weight = 1;

        fireTime = �}�����Z;
        �ʵe = GetComponent<Animator>();
        �ɯ� = GetComponent<NavMeshAgent>();
        �j�ؼ�();

    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("/target") == null)
        {
            �ɯ�.SetDestination(this.transform.position);
            �ɯ�.isStopped = true;
            �ʵe.SetTrigger("WIN");
            return;
        }

        if (�̪�ĤH!=null)
        {
           
            if (Vector3.Distance(this.transform.position, �ؼ�.transform.position) < �g�{�Z��)
            {
                �ʵe.SetBool("Run", false);
                �ɯ�.isStopped=true;
                this.transform.LookAt(�̪�ĤH);
                this.transform.eulerAngles = new Vector3(0, this.transform.eulerAngles.y, 0);
                //�}�����Z
                if (Time.time > fireTime)
                {
                    
                    �ʵe.SetTrigger("FIRE");
                    fireTime = Time.time + �}�����Z;
                }
            }
        }
        else
        {
            �j�ؼ�();
        }
        if((�ؼ�.tag == "���~�I")||(�ؼ�.name=="target"))
        {
            //�ʵe.SetBool("FIRE", false);
            if (Vector3.Distance(this.transform.position, �ؼ�.position) < 0.4f)
            {
                Destroy(�ؼ�.gameObject);
            }

        }
    }
    void �j�ؼ�() {
        //�p�G ������ ���u���~�I�v
        //���h�� ���~�I�A�R����A�~��䤤�~�I�A����S��
        //�p�G�A�ĤH�����A���������ĤH
        //�A�h��ؼСK
        
        ���~�I = GameObject.FindGameObjectsWithTag("���~�I");
        if (���~�I.Length == 0)
        {
            //�p�G���~�I�S�F�A�٬O�n����ĤH�H
            if(��ĤH()==0)
            {
                if (GameObject.Find("/target") != null)
                {
                    �ؼ� = GameObject.Find("/target").transform;
                }
            }
        }
        else
        {
            int r = Random.Range(0, ���~�I.Length - 1);
            �ؼ� = ���~�I[r].transform;
        }

        //�o��ӧ�ĤH�a�I
        ��ĤH();

        �ɯ�.isStopped = false;
        �ɯ�.SetDestination(�ؼ�.position);
        �ʵe.SetBool("Run", true);
        �ǥؼе�����();
    }
    int ��ĤH()
    {
        
        float �ؼжZ�� = 0;
        if (�ؼ� == null)
        {
            �ؼжZ�� = 5;
        }
        else
        {
            �ؼжZ�� = Vector3.Distance(this.transform.position, �ؼ�.position);
        }

        GameObject[] �ĤH = GameObject.FindGameObjectsWithTag("������");
        float dist;
        foreach (GameObject t in �ĤH)
        {
            dist = Vector3.Distance(this.transform.position, t.transform.position);
            if (dist < �ؼжZ��)
            {
                �ؼ� = t.transform;
                �̪�ĤH = t.transform;
                
            }
        }

        return �ĤH.Length;
    }
    void �ǥؼе�����()
    {
        //�N�ؼжǨ�g�b�欰�A������i�H�ݵۥؼСC
        if (this.transform.name == "�~�H_�A��_�S�Y(Clone)") 
        {
            GetComponent<�~�H��A>().�ؼ� = �ؼ�;
        }
        else if(this.transform.name == "�~�H-�}�b�� F1(Clone)") 
        {
            GetComponent<�~�H�g�b>().�ؼ� = �ؼ�;
        }

        
        transform.Find("Rig 1/HeadAim").gameObject.GetComponent<MultiAimConstraint>().data.sourceObjects
            = new WeightedTransformArray { new WeightedTransform(�ؼ�, 1) };
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "�������Z��") 
        {
            Destroy(other.gameObject);
            hp--;
            ��q.text = hp.ToString();
            float blood = (float)hp / (float)OriHP;
            ���.transform.localScale = new Vector3(blood, 1, 1);

            if (hp <= 0) { Destroy(this.gameObject); }
        }
    }
}
