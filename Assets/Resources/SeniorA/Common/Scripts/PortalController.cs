using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PortalController : MonoBehaviour
{
    public GameObject player;
    public GameObject targetPlace;
    private Transform pivot;
    private CharacterController characterController;
    private bool isTrigger;

    private Sequence portalSequence;
    void Start()
    {
        player=GameObject.FindGameObjectWithTag("Player");
        characterController=player.GetComponent<CharacterController>();
        pivot=this.gameObject.transform;

        portalSequence=DOTween.Sequence()
            .SetAutoKill(false)
            .Join(transform.DOLocalRotate(new Vector3(0,360*30, 0), 5f, RotateMode.FastBeyond360).SetEase(Ease.InOutSine))
            .Join(pivot.DOLocalRotate(new Vector3(0,0,80), 1f).SetEase(Ease.InSine))
            .Insert(3.5f, pivot.DOLocalRotate(new Vector3(0, 0, 0), 1.5f).SetEase(Ease.OutSine))
            .Pause();
    }

    void Update() 
    {
        if(!isTrigger)
            return;
        if(Input.GetKeyDown(KeyCode.P))
        {
            ActivatePortal();
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Enter");
        isTrigger=true;
    }    

    private void OnTriggerExit(Collider other) 
    {
        Debug.Log("Exit");
        isTrigger=false;
    }  

    private void ActivatePortal()
    {
        isTrigger=false;
        characterController.enabled=false;

        portalSequence.Rewind();
        portalSequence.Play();

         CharacterControllerThirdPerson characterAnim = characterController.GetComponent<CharacterControllerThirdPerson>();
        characterAnim.Teleport(characterController, transform.position, targetPlace.transform.position);
    }
}
