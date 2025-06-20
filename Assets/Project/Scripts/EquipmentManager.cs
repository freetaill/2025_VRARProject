using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class EquipmentManager : MonoBehaviour
{
    public Transform cameratrans;

    [Header("Equipment Transform")]
    public Transform Weapon;
    public Transform Shield;
    public Transform EquipmentAnchor;

    int handMode = 0; // 0 = null, 1 = LHand, 2 = RHand

    GameObject hand;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentRotation = EquipmentAnchor.transform.localEulerAngles;

        // targetObject의 Y축 회전만 가져옵니다.
        float targetYRotation = cameratrans.localEulerAngles.y;

        // 현재 오브젝트의 Y축 회전을 targetObject의 Y축 회전으로 설정하고,
        // X와 Z축 회전은 원래대로 유지합니다.
        EquipmentAnchor.transform.localEulerAngles = new Vector3(currentRotation.x, targetYRotation, currentRotation.z);
    }

    public void HoveredHand(HoverEnterEventArgs args)
    {
        var interactor = args.interactorObject;

        if (interactor.transform.parent.CompareTag("LHand"))
        {
            //Debug.Log("Left hand!");
            handMode = 1;
            hand = interactor.transform.parent.gameObject;
        }
        else if (interactor.transform.parent.CompareTag("RHand"))
        {
            //Debug.Log("Right hand!");
            handMode = 2;
            hand = interactor.transform.parent.gameObject;
        }
    }
    public void Hovered(GameObject HoverObject)
    {
        if (HoverObject.transform.CompareTag("Weapon"))
        {
            GameObject attachPoint = HoverObject.transform.GetChild(1).gameObject;
            switch (handMode)
            {
                case 0:
                    //Debug.Log("null!");
                    break;
                case 1:
                    attachPoint.transform.localPosition = new Vector3(0.02f, 0.07f, 0.11f);
                    attachPoint.transform.localRotation = Quaternion.Euler(-10, 0, 0);
                    break;
                case 2:
                    attachPoint.transform.localPosition = new Vector3(-0.02f, 0.07f, 0.11f);
                    attachPoint.transform.localRotation = Quaternion.Euler(-10, 0, 0);
                    break;
            }
        }
        else if (HoverObject.transform.CompareTag("Shield"))
        {
            GameObject attachPoint = HoverObject.transform.GetChild(1).gameObject;
            switch (handMode)
            {
            case 0:
                //Debug.Log("null!");
                break;
            case 1:
                attachPoint.transform.localPosition = new Vector3(-0.06f, 0.09f, -0.15f);
                attachPoint.transform.localRotation = Quaternion.Euler(0, 170, 0);
                break;
            case 2:
                attachPoint.transform.localPosition = new Vector3(-0.06f, 0.09f, 0.15f);
                attachPoint.transform.localRotation = Quaternion.Euler(0, 10, 0);
                break;
            }
        }
    }

    public void UnHovered(GameObject HoverObject)
    {
        if (HoverObject.transform.CompareTag("Weapon"))
        {
            GameObject attachPoint = HoverObject.transform.GetChild(1).gameObject;
            attachPoint.transform.localPosition = new Vector3(0f, 0f, 0f);
            attachPoint.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (HoverObject.transform.CompareTag("Shield"))
        {
            GameObject attachPoint = HoverObject.transform.GetChild(1).gameObject;
            attachPoint.transform.localPosition = new Vector3(0f, 0f, 0f);
            attachPoint.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void Equipped(GameObject Equipment)
    {
        Equipment.GetComponent<Rigidbody>().useGravity = true;
        Equipment.GetComponent<Rigidbody>().isKinematic = false;
        Equipment.GetComponent<EquipmentHapticManager>().GetHapticController(hand);
    }

    public void UnEquipped(GameObject Equipment)
    {
        Equipment.GetComponent<Rigidbody>().isKinematic = true;

        if (Equipment.CompareTag("Weapon"))
        {
            if (Weapon.transform.childCount == 0)
            {
                Equipment.transform.SetParent(Weapon.transform, true);
                //Debug.Log("무기장착");
            }
            else
            {
                Weapon.GetChild(0).transform.SetParent(null, true);
                Equipment.transform.SetParent(Weapon.transform, true);
            }
            Equipment.transform.position = Weapon.position;
            Equipment.transform.rotation = Weapon.rotation;
        }
        else if (Equipment.CompareTag("Shield"))
        {
            if (Shield.transform.childCount == 0)
            {
                Equipment.transform.SetParent(Shield.transform, true);
                //Debug.Log("방패장착");
            }
            else
            {
                Shield.GetChild(0).transform.SetParent(null, true);
                Equipment.transform.SetParent(Shield.transform, true);
            }
            Equipment.transform.position = Shield.position;
            Equipment.transform.rotation = Shield.rotation;
        }
    }
}
