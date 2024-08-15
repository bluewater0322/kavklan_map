using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class enemyMove : MonoBehaviour
{
    NavMeshAgent �N�z�H;
    public Transform �ؼ�;
    GameObject[] ���~�I;

    public int hp = 10;

    public float �g�{�Z�� = 2f;
    float �}�b��Z�� = 2f;
    public Transform �̪�}�b��;
    Animator �ʵe;

    public Transform �o�g�I;
    public GameObject �l�u;
    public float �}�����Z = 0.3f;
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
        rig.weight = 0;

        fireTime = �}�����Z;
        �ʵe = GetComponent<Animator>();
        �N�z�H = GetComponent<NavMeshAgent>();
        �j�ؼ�();

    }
    void �j�ؼ�() {
        //�p�G ������ ���u���~�I�v
        //���h�� ���~�I�A�R����A�~��䤤�~�I�A����S��
        //�p�G�A�}�b������A���������}�b��
        //�A�h��ؼСK
        
        ���~�I = GameObject.FindGameObjectsWithTag("���~�I");
        if (���~�I.Length == 0)
        {
            if(GameObject.Find("/target") != null)
            {
                �ؼ� = GameObject.Find("/target").transform;
            }
            else
            {
                return;
            }
                
        }
        else
        {
            int r = Random.Range(0, ���~�I.Length - 1);
            �ؼ� = ���~�I[r].transform;
        }
        �N�z�H.SetDestination(�ؼ�.position);
    }

    bool ��}�b��() {
        if (�̪�}�b�� != null) {
            this.transform.LookAt(�̪�}�b��); 
            return true; 
        }
        GameObject[] �}�b�� = GameObject.FindGameObjectsWithTag("�}�b��");
        float dist;
        foreach (GameObject t in �}�b��)
        {
            dist = Vector3.Distance(this.transform.position, t.transform.position);
            if (dist < �g�{�Z��)
            {
                if (dist < �}�b��Z��)
                {
                    �}�b��Z�� = dist;
                    �̪�}�b�� = t.transform;
                    �N�z�H.isStopped = true;
                    rig.weight = 1;
                    �ʵe.SetBool("FIRE", true);
                    this.transform.LookAt(�̪�}�b��);
                    �ؼ� = �̪�}�b��;
                    //return true;
                }
            }
        }
        if (�̪�}�b�� != null)
        {
            return true;
        }
        else {
            �N�z�H.isStopped = false;
            rig.weight = 0;
            �ʵe.SetBool("FIRE", false);
            return false;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        //cv.transform.LookAt(Camera.main.transform);
        if (GameObject.Find("/target") == null)
        {
            �N�z�H.SetDestination(this.transform.position);
            �N�z�H.isStopped = true;
            �ʵe.SetTrigger("WIN");
            return;
        }

        if (�ؼ� == null)
        {
            �j�ؼ�();
        }

        if (!   ��}�b��())
        {
            �ʵe.SetBool("FIRE", false);
            if (Vector3.Distance(this.transform.position, �ؼ�.position) < 0.4f)
            {
                //if (�ؼ�.name != "target")
                Destroy(�ؼ�.gameObject);
            }
        }
        else 
        {
            if (Vector3.Distance(this.transform.position, �ؼ�.transform.position) > �g�{�Z��) {
                �ؼ� = null;
                �̪�}�b�� = null;
                return;
            }
        }        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "�ڤ�l�u") 
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
