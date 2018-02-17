using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transferAbility : MonoBehaviour {

    //todo - add max range
    //      - add other abilities, speed, jump etc
    //      - add switch statement to determen the ability
    //      - add vision check
    // BUG TEST!


    public GameObject p1;
    public GameObject p2;

    public Vector3 player_originalScale;

    Vector3 player1_originalScale;
    Vector3 player2_originalScale;

    bool isScaled = false;
    bool canCast = true;
    bool inDistance = false;
    bool canSee = false;

    public GameObject activeOn;

    public ParticleSystem particles;

    public bool activeOn_P1;
    public bool activeOn_P2;

    public Slider abilityTransferBar;
    public Text abilityText;

    public float maxDistance = 25f;

    public float transferSpeed = 1f;
    public float maxTime = 5f;
    public float regenRate = 10f;
    float transferTime = 0f;
    float transferCooldown = 0f;

    public LineRenderer lineRend;

    public bool increaseSize = true;
    public float sizeIncrease = 0.5f;
    public Vector3 MaxScale = new Vector3(1f, 1f, 1f);

    public bool ability_DoubleJump = false;
    public bool ability_PassThrough = false;
    public bool ability_WalkOnCertainObj = false;

    public int currentAbilityID = 0;
    public int maxAbilities = 3;

    private void Awake()
    {
        lineRend = GetComponent<LineRenderer>();
        abilityTransferBar.maxValue = maxTime;
        abilityTransferBar.value = maxTime;
    }

    void Start () 
    {
        activeOn_P1 = true;
        activeOn_P2 = false;

        player_originalScale = p1.transform.localScale;
        player1_originalScale = p1.transform.localScale;
        player2_originalScale = p2.transform.localScale;
	}
	
	void Update () 
    {
        if(Vector3.Distance(p1.transform.position, p2.transform.position) < maxDistance)
        {
            inDistance = true;
        }
        else if(Vector3.Distance(p1.transform.position, p2.transform.position) > maxDistance)
        {
            inDistance = false;
        }

        /*
        RaycastHit hit;

        if (Physics.Raycast(transform.position, (p2.transform.position - transform.position), out hit, maxDistance))
        {
            if (hit.transform == p2)
            {
                canSee = true;
            }
        }
////////////
        if(Physics.Linecast(p1.transform.position, p2.transform.position))
        {
            canSee = true;
        }
        else
        {
            canSee = false;
        }
*/
        if(Input.GetKey(KeyCode.B) && canCast && inDistance && canSee)
        {
            lineRend.enabled = true;
            castLine(p2.transform.position, p1.transform.position);
            abilityTransferBar.value -= transferSpeed * Time.deltaTime;
        }
        else
        {
            abilityTransferBar.value += regenRate * Time.deltaTime;
            lineRend.enabled = false;

            if (abilityTransferBar.value >= maxTime)
            {
                abilityTransferBar.value = maxTime;
                canCast = true;
            }
        }

        if(abilityTransferBar.value <= 0 && canCast)
        {
            canCast = false;
            CastAbility(activeOn_P1);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            CycleAbility();
        }
	}

    void CastAbility(bool fromPlayerOne)
    {
        if(!fromPlayerOne)
        {
            IncreaseScale(p1, p2);
            activeOn_P1 = true;
            activeOn_P2 = false;
            CastParticleEffect(p1);
        }
        else if(fromPlayerOne)
        {
            IncreaseScale(p2, p1);
            activeOn_P1 = false;
            activeOn_P2 = true;
            CastParticleEffect(p2);
        }
    }

    public void updateAbilityText(string text)
    {
        abilityText.text = text;
    }

    public void CycleAbility()
    {
        if(currentAbilityID >= maxAbilities)
        {
            currentAbilityID = 0;
        }
        else
        {
            currentAbilityID++;
        }

        switch (currentAbilityID)
        {
            case 3:
                print("ABILITY_ID = 3");
                updateAbilityText("ABILITY_ID = 3");
                break;
            case 2:
                print("ABILITY_ID = 2");
                updateAbilityText("ABILITY_ID = 2");
                break;
            case 1:
                print("ABILITY_ID = 1");
                updateAbilityText("ABILITY_ID = 1");
                break;
            default:
                print("DEFAULT ABILITY SELECTED");
                updateAbilityText("ABILITY_ID = DEFAULT");
                break;
        } 
    }

    void IncreaseScale(GameObject playerToScale, GameObject playerCastFrom)
    {
        if(playerToScale != activeOn)
        {
            playerToScale.transform.localScale += new Vector3(sizeIncrease, sizeIncrease, sizeIncrease);
        }

        playerCastFrom.transform.localScale = new Vector3(player_originalScale.x, player_originalScale.y, player_originalScale.z);
        activeOn = playerToScale;
    }

    void CastParticleEffect(GameObject playerCastOn)
    {
        particles.transform.position = playerCastOn.transform.position;
        particles.Play();
    }

    void castLine(Vector3 start, Vector3 end)
    {
        lineRend.SetPosition(0, start);
        lineRend.SetPosition(1, end);
    }
}
