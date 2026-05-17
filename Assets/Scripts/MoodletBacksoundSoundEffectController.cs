using UnityEngine;
using UnityEngine.InputSystem;

public class MoodletBacksoundSoundEffectController : MonoBehaviour
{

    [Header("Ini Controller Dont destroy on load")]
    [SerializeField] private GameObject backgroundMoodlet;
    private RectTransform backgroundMoodletRect;

    // enum animasi (bcs i got confused with numbers)
    public enum MoodletState { happy, wave, smile, kaget, yawn, sad, ok, confetii };
    public MoodletState moodletState = MoodletState.yawn;

    // animasi say you remember me ambil animator
    [SerializeField] private GameObject Emoji;
    private Animator EmojiAnim;
    //public bool MouseTutorial = false;

    // audio    
    //[Header("Audio Moodlet - Audio Source Emoji")]
    //[SerializeField] private AudioSource srcEmoji;
    //[SerializeField] private AudioClip audioHappy, audioWave, audioSmile, audioKaget, audioYawn, audioSad, audioOk,audioConfetii;



    public static MoodletBacksoundSoundEffectController InstanceMoodlet;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        backgroundMoodletRect = backgroundMoodlet.GetComponent<RectTransform>();
        StartMelebar();
        //BacksoundLogin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        //if (InstanceMoodlet != null)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    InstanceMoodlet = this;
        //    DontDestroyOnLoad(this.gameObject);


        //    EmojiAnim = Emoji.GetComponent<Animator>();
        //    if (srcEmoji == null) srcEmoji = GetComponent<AudioSource>();

        //}
    }

    private void StartMelebar()
    {
        LeanTween.size(backgroundMoodletRect, new Vector2(315f, backgroundMoodletRect.sizeDelta.y), 1f)
            .setEaseOutBack();
    }


    // emo
    //0 = happy
    //1 = wave
    //2    smile
    // 3   kaget
    //  4  yawn

    //public void ChangeMoodlet(MoodletState newState)
    //{
    //    if (newState == moodletState) return;
    //    moodletState = newState;
    //    MoodletController();
    //}

    //private void MoodletController()
    //{
    //    switch (moodletState)
    //    {
    //        case MoodletState.yawn:
    //            //srcEmoji.clip = audioYawn;
    //            srcEmoji.PlayOneShot(audioYawn);
    //            EmojiAnim.SetInteger("emoNumber", 4);
    //            break;
    //        case MoodletState.smile:
    //            //srcEmoji.clip = audioSmile;
    //            srcEmoji.PlayOneShot(audioSmile);
    //            //srcEmoji.Play();
    //            EmojiAnim.SetInteger("emoNumber", 2);
    //            break;
    //        case MoodletState.happy:
    //            //srcEmoji.clip = audioHappy;
    //            srcEmoji.PlayOneShot(audioHappy);
    //            //srcEmoji.Play();
    //            EmojiAnim.SetInteger("emoNumber", 0);
    //            break;
    //        case MoodletState.wave:
    //            //srcEmoji.clip = audioWave;
    //            srcEmoji.PlayOneShot(audioWave);
    //            //srcEmoji.Play();
    //            EmojiAnim.SetInteger("emoNumber", 1);
    //            break;
    //        case MoodletState.kaget:
    //            //srcEmoji.clip = audioKaget;
    //            srcEmoji.PlayOneShot(audioKaget);
    //            //srcEmoji.Play();
    //            EmojiAnim.SetInteger("emoNumber", 3);
    //            break;
    //        case MoodletState.sad:
    //            //srcEmoji.clip = audioSad;
    //            srcEmoji.PlayOneShot(audioSad);
    //            //srcEmoji.Play();
    //            EmojiAnim.SetInteger("emoNumber", 5);
    //            break;
    //        case MoodletState.ok:
    //            //srcEmoji.clip = audioSad;
    //            srcEmoji.PlayOneShot(audioOk);
    //            //srcEmoji.Play();
    //            EmojiAnim.SetInteger("emoNumber", 6);
    //            break;
    //        case MoodletState.confetii:
    //            //srcEmoji.clip = audioSad;
    //            srcEmoji.PlayOneShot(audioConfetii);
    //            //srcEmoji.Play();
    //            EmojiAnim.SetInteger("emoNumber", 7);
    //            break;

    //    }
    //}


}
