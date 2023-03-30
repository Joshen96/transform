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

        // 메소드 method , 함수 function 
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
    //상속(Inheritance)

    // Parent - Child 부모 - 자식
    // Super - Sub 슈퍼 - 서브
    // Base - Derived 기본 - 파생



    public class Parent
    {
        protected int parentVal;


        public Parent()
        {

            Debug.Log("++Parent 생성자");
        }
        ~Parent()
        {

            Debug.Log("--Parent 소멸자");
        }



        public virtual void ParentFunc()
        {
            Debug.Log("parentVal :" + parentVal);
        }
    }

    public abstract class AbstractParent
    {
        //추상클래스  순수가상함수
        public abstract void ParentFunc(); //정의가 없음 형태 x

    }

    public class APChild :AbstractParent  //추상클래스를 상속받는 클래스는 
    {
        public override void ParentFunc() // 재정의를 해야한다 override 
        {
            Debug.Log("APChild :AbstractParent");
        }
    }


    public abstract class Weapon //무기 추상클래스 예시
    {
        public abstract void Att(); //공격함수 추상함수 선언 기능은x

    }

    public class Gun : Weapon //무기 을 상속받는 건 
    { 
        public override void Att()  //Att 기능을 재정의 한다
        {
            Debug.Log("Gun shot");
        }
    }


    public interface IWeapon { }  //인터 페이스,  가이드라인만 
    public interface IWeapon2 { }

    public class  NewWeapon : IWeapon,IWeapon2  // 다중상속 가능 정의된것이 없어서 죽음의 다이아몬드 없다는걸 보장된다.
    {

    }


    public class Child:Parent
    {
        private int childVal;  


        public Child() 
        {
            Debug.Log("++Child 생성자");
        }
        public Child(int a)
        {
            Debug.Log("++Child 생성자");
        }
        ~Child()
        {
            Debug.Log("--Child 소멸자");
        }
        public void ChildFunc()
        {
            Debug.Log("childVal: " + childVal);
            parentVal = 10;
        }
        // Override  재정의 
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

        Parent obj = new Child();// new Child() // new Child(5) 가능 
        obj.ParentFunc();
        ((Child)obj).ParentFunc();
        
        //Child c = new Parent(); 안돰
        obj = new NewChild();

        obj.ParentFunc();


        //AbstractParent ap =new AbstractParent(); 추상클래스는 할당 x



        

    }
}
