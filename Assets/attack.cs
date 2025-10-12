using UnityEngine;

public class attack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    public GameObject Melee;
    bool isattacking = false;
    float atkduration = 0.3f; 
    float atktimer =0f;

    // Update is called once per frame
    void Update()
    {
        CheckMeleeTimer();
        if(Input.GetKeyDown(KeyCode.Space)){
             OnAttack();
        }
    }
    void OnAttack(){
        if(!isattacking){
            Melee.SetActive(true);
            isattacking = true;
        }
    }
    void CheckMeleeTimer(){
        if(isattacking){
            atktimer += Time.deltaTime;
        
            if(atktimer >=atkduration){
                atktimer =0;
                isattacking = false;
                Melee.SetActive(false); 
            }
        }
    }
}
