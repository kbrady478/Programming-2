using UnityEngine;

public class throw_SMB : StateMachineBehaviour
{
    private AudioSource audio;
    private GameObject camera;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        audio = camera.GetComponent<AudioSource>();
        audio.Play();
    }
    

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       audio.Stop();
    }
    
}
