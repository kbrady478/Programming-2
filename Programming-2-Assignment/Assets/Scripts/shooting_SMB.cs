using Unity.VisualScripting;
using UnityEngine;

public class shooting_SMB : StateMachineBehaviour
{
    private Burst_RIfle rifle_Script;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rifle_Script = GameObject.FindGameObjectWithTag("Player Rifle").GetComponent<Burst_RIfle>();
    }
    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rifle_Script.can_Fire = true;
        animator.SetBool("is_Shooting", false);
    }
    
}
