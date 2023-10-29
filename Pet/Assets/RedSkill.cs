using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RedSkill : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedCut = 0.5f;
    public GameObject Cut;
    public PetBehavior RedPet;
    public int petSkillLevel = 0;
    public bool DoSpecialSkill = false;
    public bool InNormalSkill = false;

    public float timeCdSkillReal = 0;
    public float timeCdSkill = 0;
    public GameObject timeCdObj;
    public TextMeshProUGUI timeCd;

    public bool haveYellowBuff = false;
    public PetBehavior yellow;
    private void Update()
    {
        timeCdSkill = timeCdSkill - Time.deltaTime;
        if (timeCdSkill <= 0)
        {
            timeCdObj.gameObject.SetActive(false);
        }
        else
        {
            timeCdObj.gameObject.SetActive(true);
        }
        var inttime = (int)timeCdSkill;
        timeCd.text = inttime.ToString();
    }

    public void UpdatePetSkillLevel()
    {
       
        petSkillLevel = (int)RedPet.petLevel / 5;
        if(petSkillLevel > 5)
        {
            petSkillLevel = 5;
        }
        ChangeSizeCut();
    }
   
    public void RedCut()
    {
        if (!DoSpecialSkill)
        {
            StartCoroutine(speedCut.Tweeng((p) => transform.eulerAngles = p,
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 180)));
            Cut.SetActive(true);
            StartCoroutine(afterCut());
            InNormalSkill = true;

        }

    }
    public IEnumerator afterCut()
    {
        yield return new WaitForSeconds(speedCut+0.1f);
        Cut.SetActive(false);
        InNormalSkill = false;
        if (DoSpecialSkill)
        {
            SpecialSkill();
        }


    }
    public void ChangeSizeCut()
    {
        Debug.Log("cut");
        Cut.transform.localPosition = new Vector3(1.6f, 0, 0) +
            new Vector3(0.1f * petSkillLevel, 0, 0);
        Cut.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f) +
            new Vector3(0.2f * petSkillLevel, 0.2f * petSkillLevel, 0.2f * petSkillLevel);
        for (int i = 0; i < Cut.transform.childCount; i++)
        {
            if (i != petSkillLevel)
            {
                Cut.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                Cut.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    public void SpecialSkillController()
    {
        if(timeCdSkill <= 0)
        {
            timeCdSkill = timeCdSkillReal;
            Debug.Log("dospecialskill");
            if (InNormalSkill)
            {
                DoSpecialSkill = true;
            }
            else
            {
                SpecialSkill();
            }
        }
        
        
    }
    public void SpecialSkill()
    {
        StartCoroutine(0.5f.Tweeng((p) => RedPet.gameObject.transform.position = p,
            new Vector3(0, 1.815f, 0),
            new Vector3(0, 4.815f, 0)));
        StartCoroutine(JumpUp());
    }
    public IEnumerator JumpUp()
    {
        yield return new WaitForSeconds(0.5f);
        Cut.SetActive(true);
        StartCoroutine(speedCut.Tweeng((p) => transform.eulerAngles = p,
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 360)));
        StartCoroutine(JumpDown());
    }
    public IEnumerator JumpDown()
    {
        yield return new WaitForSeconds(speedCut+0.1f);
        StartCoroutine(0.5f.Tweeng((p) => RedPet.gameObject.transform.position = p,
            new Vector3(0, 4.815f, 0),
            new Vector3(0, 1.815f, 0)));
        Cut.SetActive(false);
        DoSpecialSkill = false;

    }
}
