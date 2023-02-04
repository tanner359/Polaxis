using UnityEngine;
using UnityEngine.UI;

public class Static_Animator : MonoBehaviour
{
    public Sprite[] images;
    
    public float speed;

    private SpriteRenderer srTarget;
    private Image imgTarget;

    private void Awake(){
        if(gameObject.TryGetComponent(out SpriteRenderer sr)){
            srTarget = sr;
        }
        else if(gameObject.TryGetComponent(out Image img)){
            imgTarget = img;
        }
    }

    private int i;
    private float t;
   
    private void LateUpdate(){
        t += Time.unscaledDeltaTime;
        if(t>=speed){
            if(srTarget != null){
                srTarget.sprite = images[i];
                i = i != images.Length-1 ? i+=1 : 0;
            }
            else{
                imgTarget.sprite = images[i];
                i = i != images.Length-1 ? i+=1 : 0;
            }
            t = 0;
        }
    }
}
