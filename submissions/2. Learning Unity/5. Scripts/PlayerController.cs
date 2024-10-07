
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    private UnityEngine.Vector2 movementVector;
    private float movementX;
    private float movementY;

    private float loseHeight = -90f;

    private int count;

    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public RectTransform loseTextTransform;

    public AudioSource initialMusic;
    public AudioSource secondaryMusic;
    public float fadeoutTime = 2.0f;

    public int requiredCollectibles = 4;

    public GameObject finalSetObject;

    public GameObject checkPointObject;

    // Start is called before the first frame update
    void Start()
    {
        SetCountText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        finalSetObject.SetActive(false);
        checkPointObject.SetActive(false);
        rb = GetComponent<Rigidbody>();    
    }

    void OnMove(InputValue momentValue)
    {
        UnityEngine.Vector2 movementVector = momentValue.Get<UnityEngine.Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    void Update(){
        UnityEngine.Vector3 playerPosition = transform.position;
        UnityEngine.Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position); 

        if (transform.position.y <= loseHeight){
            countText.SetText("You Lost!");
            Time.timeScale = 0;
        }
    }

    private void FixedUpdate()
    {
        UnityEngine.Vector3 movement = new UnityEngine.Vector3(movementX, 0.0f, movementY);
        UnityEngine.Vector3 position = transform.position;
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")){
            other.gameObject.SetActive(false);
            count++;
            speed += 10;
            SetCountText();

        }

        if (other.gameObject.CompareTag("TargetObjTag")){
            countText.SetText("You Win!");
            Time.timeScale = 0;
        }

        if (other.gameObject.CompareTag("RobotBall")){
            rb.isKinematic = true;
            countText.SetText("You Lost");
            Time.timeScale = 0;
        }

        if (other.gameObject.CompareTag("Checkpoint")){
            {
                other.gameObject.SetActive(false);
                StartSequence();
                //finalSetObject.SetActive(true);
            }
                
        }

    
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count == requiredCollectibles){
            checkPointObject.SetActive(true);
        }

    }

    public void StartSequence()
    {
        StartCoroutine(ActionSequence());
    }
 
    private IEnumerator ActionSequence()
    {
        FadeOutMusic();
        yield return new WaitForSeconds(3); // Wait for 3 seconds after fading out the music
        SwitchMusic();
        finalSetObject.SetActive(true);
    }

    void FadeOutMusic(){
        StartCoroutine(FadeOut(initialMusic, fadeoutTime));
    }

    private IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        //audioSource.volume = startVolume; // Reset volume to initial value if you need to play it again later
    }

    void SwitchMusic(){
        if (initialMusic.isPlaying){
            initialMusic.Stop();
        }
        secondaryMusic.Play();
    }

    

}
