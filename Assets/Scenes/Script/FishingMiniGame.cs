using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;
using DG.Tweening;
public class FishingMiniGame: MonoBehaviour
{   
    public GameObject fishingGame;
    public GameObject office_panel;
    public Transform SuccessImage;
    public Transform FailedImage;

    [Header("Fishing Area")] 
    [SerializeField] Transform topBounds;
    [SerializeField] Transform bottomBounds;
    
    [Header("Fish Settings")] 
    [SerializeField] Transform fish; 
    [SerializeField] float smoothMotion = 3f; // smooth out fish movement
    [SerializeField] float fishTimeRandomizer = 3f; // how often the fish moves 
    float fishPosition;
    float fishSpeed;
    float fishTimer;
    float fishTargetPosition;

    [Header("Hook Settings")]
    [SerializeField] Transform hook;
    [SerializeField] float hookSize = .18f;
    [SerializeField] float hookSpeed = .1f;
    [SerializeField] float hookGravity = .05f;
    float hookPosition; 
    float hookPullVelocity;

    [Header("Progress Bar Settings")]
    [SerializeField] Transform progressBarContainer;
    [SerializeField] float hookPower;
    [SerializeField] float progressBarDecay;
    float catchProgress;
    
    private void Start() 
    {
        catchProgress = .3f;
    }

    private void FixedUpdate() 
    {
        MoveFish();
        MoveHook();
        CheckProgress();
    }
    
    private void CheckProgress() 
    {
        Vector3 progressBarScale = progressBarContainer.localScale;
        progressBarScale .y = catchProgress;
        progressBarContainer.localScale = progressBarScale; // Update the Y value of the parent object
        
        float min = hookPosition - hookSize / 2; 
        float max = hookPosition + hookSize / 2;
        
        if(min < fishPosition && fishPosition< max)
        {
            catchProgress += hookPower * Time.deltaTime;
            if(catchProgress >= 1) 
            {
                // Won the game
                Debug.Log("You win!");
                TweenResult(SuccessImage);
              StartCoroutine(WaitForNext());
               
            //    fishingGame.SetActive(false);
            //    office_panel.SetActive(true);
                // Do win logic here 
            }
        }
        else 
        {
            catchProgress -= progressBarDecay * Time.deltaTime;
            if(catchProgress <= 0) 
            {
                // We lost!
               Debug.Log("You lose!");
               TweenResult(FailedImage);
               StartCoroutine(WaitForNext());
               
               fishingGame.SetActive(false);
               office_panel.SetActive(true);

               
                // Lose logic here
            }
        }
        catchProgress = Mathf.Clamp(catchProgress, 0, 1);
    }

    private void MoveHook()
    {
        if (Input.GetMouseButton(0)) 
        {
            // increase our pull velocity 
            hookPullVelocity += hookSpeed * Time.deltaTime; // raises our hook
        }
        hookPullVelocity -= hookGravity * Time.deltaTime;
        
        hookPosition += hookPullVelocity; 
        
        if(hookPosition - hookSize / 2 <= 0 && hookPullVelocity < 0)
        {
            hookPullVelocity = 0;
        }
        
        if(hookPosition + hookSize / 2 >= 1 && hookPullVelocity > 0)
        {
            hookPullVelocity = 0; 
        }
        
        hookPosition = Mathf.Clamp(hookPosition, hookSize / 2, 1 - hookSize / 2); // Keep hook within bounds 
        hook.position = Vector3.Lerp(bottomBounds.position, topBounds.position, hookPosition);    
    }
    private void MoveFish() 
    {
        // based on a timer, pick random position
        // move fish to that position smoothly
        fishTimer -= Time.deltaTime;
        if(fishTimer < 0)
        {
            // pick a new target position 
            // and reset timer
            fishTimer = Random.value * fishTimeRandomizer;
            fishTargetPosition = Random.value; 
        }
        fishPosition = Mathf.SmoothDamp(fishPosition, fishTargetPosition, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(bottomBounds.position, topBounds.position, fishPosition);
    }
      void TweenResult(Transform resultTransform){
        Sequence result = DOTween.Sequence();
        result.Append(resultTransform.DOScale(1,.5f).SetEase(Ease.OutBack)); // Scale from 0 to 1
        result.AppendInterval(1f); // Wait for 1 second
        result.Append(resultTransform.DOScale(0,.2f).SetEase(Ease.Linear)); // Scale back down to 0
     
   }
         IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(3);
     
    }
}