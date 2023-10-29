using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowSkill : MonoBehaviour
{
    public PetBehavior yellowPet;
    public int petSkillLevel = 0;
    public int numberAoe = 2;

    public GameObject chose;
    public GameObject skillBtn;
    public GameObject unSkillBtn;
    public GameObject CloneYellow;
    public Color cl1;
    public Color cl2;
    public Transform red;
    public Transform blue;
    public void Update()
    {
        numberAoe = 2 + petSkillLevel + yellowPet.aoeBuff;
    }
    public void UpdatePetSkillLevel()
    {

        petSkillLevel = (int)yellowPet.petLevel / 5;
        if (petSkillLevel > 5)
        {
            petSkillLevel = 5;
        }
        numberAoe = 2 + petSkillLevel + yellowPet.aoeBuff;
    }
    public void ClickSkillBtn()
    {
        skillBtn.gameObject.SetActive(false);
        chose.gameObject.SetActive(true);
    }
    public void ClickXBtn()
    {
        skillBtn.gameObject.SetActive(true);
        chose.gameObject.SetActive(false);
    }
    public void ClickRedBtn()
    {
        //skillBtn.gameObject.SetActive(true);
        chose.gameObject.SetActive(false);
        unSkillBtn.gameObject.SetActive(true);
        RedChose();
    }
    public void ClickBlueBtn()
    {
        //skillBtn.gameObject.SetActive(true);
        chose.gameObject.SetActive(false);
        unSkillBtn.gameObject.SetActive(true);

        BLueChose();
    }
    public void RedChose()
    {
        gameObject.GetComponent<SpriteRenderer>().color = cl1;
        StartCoroutine(0.3f.Tweeng((p) => CloneYellow.transform.position = p,
            CloneYellow.transform.position,
            new Vector3(0.875f, 2.263f, 0)));
        StartCoroutine(0.3f.Tweeng((p) => CloneYellow.transform.localScale = p,
            CloneYellow.transform.localScale,
            new Vector3(0.3f, 0.3f, 0.3f)));
        CloneYellow.transform.SetParent(red);

        yellowPet.inBuffStatus = true;

        red.gameObject.GetComponent<PetBehavior>().haveYellowBuff = true;
    }
    public void BLueChose()
    {
        gameObject.GetComponent<SpriteRenderer>().color = cl1;
        StartCoroutine(0.3f.Tweeng((p) => CloneYellow.transform.position = p,
            CloneYellow.transform.position,
            new Vector3(-2.702f, 2.263f, 0)));
        StartCoroutine(0.3f.Tweeng((p) => CloneYellow.transform.localScale = p,
            CloneYellow.transform.localScale,
            new Vector3(0.3f, 0.3f, 0.3f)));
        CloneYellow.transform.SetParent(blue);

        yellowPet.inBuffStatus = true;

        blue.gameObject.GetComponent<PetBehavior>().haveYellowBuff = true;
    }
    public void BackBtn()
    {
        var aaa = gameObject.transform.localToWorldMatrix.GetPosition();
        StartCoroutine(0.3f.Tweeng((p) => CloneYellow.transform.position = p,
            CloneYellow.transform.position,
            aaa));
        StartCoroutine(0.3f.Tweeng((p) => CloneYellow.transform.localScale = p,
            CloneYellow.transform.localScale,
            new Vector3(1, 1, 1)));
        CloneYellow.transform.SetParent(gameObject.transform);
        unSkillBtn.gameObject.SetActive(false);
        skillBtn.gameObject.SetActive(true);
        StartCoroutine(comeback());

        red.gameObject.GetComponent<PetBehavior>().haveYellowBuff = false;
        blue.gameObject.GetComponent<PetBehavior>().haveYellowBuff = false;
    }
    public IEnumerator comeback()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().color = cl2;
        yellowPet.inBuffStatus = false;

    }

}
