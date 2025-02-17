using UnityEngine;

public class punch_SMB : StateMachineBehaviour
{
    [SerializeField] private GameObject arm_Obj;
    private CapsuleCollider collider;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        collider = arm_Obj.GetComponent<CapsuleCollider>();
        collider.enabled = true;
    }



    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       collider.enabled = false;
    }


}
