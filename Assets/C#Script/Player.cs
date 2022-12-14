using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{   
    public int maxEnergy = 500;
    public int maxHappy = 500;
    public int maxCoins = 15000;
    public int currentMinute;
    public int currentHour;
    public int currentDay;
    public int currentEnergy;
    public int currentHappy;
    public int currentCoins;
    public int currentMoneyInBank;
  
    public BarScript energyBar;
    public BarScript happyBar;
    
    public CharacterDatabase characterDB;

    public SpriteRenderer artworkSprite;

    public int selectedOption = 0;
    //public Quest quest;
    
   // public ShopManager shopManager;
    // Start is called before the first frame update

    void Start()
    {   
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
        currentHappy = maxHappy;
        happyBar.SetMaxHappy(maxHappy);
        currentMinute = 0;
        currentHour = 0;
          if(!PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = 0;
        }

        else
        {
            Load();
        }
        UpdateCharacter(selectedOption);
        
    }
    // Update is called once per frame
    void Update()
    {
   

    }

      private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;

    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption"); 
    }

    public void SetCoins(int valueCoins){
        currentCoins = valueCoins;
    }
    public void SetEnergy(int valueEnergy){
       // Debug.Log(valueEnergy);
     currentEnergy = valueEnergy;
       
 if(currentEnergy>maxEnergy){
    currentEnergy = maxEnergy;
      }
         energyBar.SetEnergy(currentEnergy);
        
    }
    

    public void SetHappy(int valueHappy){
        
        // Debug.Log(valueEnergy);
         currentHappy = valueHappy;
        if(currentHappy>maxHappy){
      
        currentHappy = maxHappy;
       }
     happyBar.SetHappy(currentHappy);
    }

    
      public void SetMinute(int valueMinute){
        // Debug.Log(valueEnergy);
    
       currentMinute = valueMinute;
      
   
      }

         
        
    
      public void SetHour(int valueHour){
            currentHour = valueHour;
        // Debug.Log(valueEnergy);
         
        
    }
}