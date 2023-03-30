using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSharp : MonoBehaviour
{
     //OOP : Objcet - Oriented Programming 
    public class ClassExam
    {
        int defVal;
        private int priVal;
        public int pubVal;

        // �޼ҵ� method , �Լ� function 
        public void SetPriVal(int _priVal)
        {
            if (_priVal > 10) return; 
            priVal = _priVal; 
        }
        public int GetPriVal() {
            if (priVal < 0) return 0;
            return priVal; 
        }

        public int PriVal
        { 
            get { return priVal; } 
            set {
                if (value > 10) return;
                priVal = value; } 
        }


        public int val { get; set; }

    }
    //���(Inheritance)

    // Parent - Child �θ� - �ڽ�
    // Super - Sub ���� - ����
    // Base - Derived �⺻ - �Ļ�



    public class Parent
    {
        protected int parentVal;


        public Parent()
        {

            Debug.Log("++Parent ������");
        }
        ~Parent()
        {

            Debug.Log("--Parent �Ҹ���");
        }



        public virtual void ParentFunc()
        {
            Debug.Log("parentVal :" + parentVal);
        }
    }

    public abstract class AbstractParent
    {
        //�߻�Ŭ����  ���������Լ�
        public abstract void ParentFunc(); //���ǰ� ���� ���� x

    }

    public class APChild :AbstractParent  //�߻�Ŭ������ ��ӹ޴� Ŭ������ 
    {
        public override void ParentFunc() // �����Ǹ� �ؾ��Ѵ� override 
        {
            Debug.Log("APChild :AbstractParent");
        }
    }


    public abstract class Weapon //���� �߻�Ŭ���� ����
    {
        public abstract void Att(); //�����Լ� �߻��Լ� ���� �����x

    }

    public class Gun : Weapon //���� �� ��ӹ޴� �� 
    { 
        public override void Att()  //Att ����� ������ �Ѵ�
        {
            Debug.Log("Gun shot");
        }
    }


    public interface IWeapon { }  //���� ���̽�,  ���̵���θ� 
    public interface IWeapon2 { }

    public class  NewWeapon : IWeapon,IWeapon2  // ���߻�� ���� ���ǵȰ��� ��� ������ ���̾Ƹ�� ���ٴ°� ����ȴ�.
    {

    }


    public class Child:Parent
    {
        private int childVal;  


        public Child() 
        {
            Debug.Log("++Child ������");
        }
        public Child(int a)
        {
            Debug.Log("++Child ������");
        }
        ~Child()
        {
            Debug.Log("--Child �Ҹ���");
        }
        public void ChildFunc()
        {
            Debug.Log("childVal: " + childVal);
            parentVal = 10;
        }
        // Override  ������ 
        public override void ParentFunc()
        {
            Debug.Log("Child - ParentFunc");
        }
    }


    public class NewChild : Parent
    {
        public override void ParentFunc()
        {
            base.ParentFunc();
            Debug.Log("NewChild ParentFunc");
        }
    }
    private void Start()
    {
        ClassExam ce = new ClassExam();
        ce.SetPriVal(10);
        ce.val = 12;
        ce.PriVal = 10;

        Parent obj = new Child();// new Child() // new Child(5) ���� 
        obj.ParentFunc();
        ((Child)obj).ParentFunc();
        
        //Child c = new Parent(); �ȉ�
        obj = new NewChild();

        obj.ParentFunc();


        //AbstractParent ap =new AbstractParent(); �߻�Ŭ������ �Ҵ� x



        

    }
}
